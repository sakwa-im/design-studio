using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace sakwa
{
    public class IDecisionTreeImpl : IBaseNodeImpl, IDecisionTree
    {
        public IDecisionTreeImpl()
        {
            NodeType = eNodeType.Tree;

            InitMapping();

        }
        public IDecisionTreeImpl(Assembly assembly, SakwaMapping mapping, PostNodeInitialize nodeInitialization)
        {
            NodeType = eNodeType.Tree;
            appAssembly = assembly;

            InitMapping();
            if(mapping != null)
                SakwaMappings.Insert(0, mapping);

            PostNodeInitialize = nodeInitialization;

        }
        public IDecisionTreeImpl(Assembly assembly, SakwaMapping mapping, PostNodeInitialize nodeInitialization, string rootName)
        {
            NodeType = eNodeType.Tree;
            appAssembly = assembly;

            InitMapping();
            if(mapping != null)
                this.SakwaMappings.Insert(0, mapping);

            PostNodeInitialize = nodeInitialization;

            IDecisionTreeInterface.RootNode = IDecisionTreeInterface.CreateNewNode(eNodeType.Root, null, rootName);

        }

        public event LinkSubtreeError LinkSubtreeError;
        public event PostNodeInitialize PostNodeInitialize;

        IBaseNode IDecisionTree.RootNode
        {
            get { return RootNode; }
            set
            {
                if(RootNode != value)
                {
                    RootNode = value;
                    RootNode.Parent = this;
                    RootNode.Tree = this;

                    IBaseNode varDefs = IDecisionTreeInterface.CreateNewNode(eNodeType.VariableDefinitions, null, Constants.VariableTreeName);

                    RootNode.AddNode(varDefs);

                    OnUpdated();
                }
            }
        }

        IVariableDef IDecisionTree.ChangeVariable(IVariableDef previousVar)
        {
            IVariableDef result = null;

            switch(previousVar.VariableType)
            {
                case eVariableType.character:
                    result = IDecisionTreeInterface.CreateNewNode(typeof(CharVariableImpl).ToString()) as IVariableDef;
                    break;

                case eVariableType.numeric:
                    result = IDecisionTreeInterface.CreateNewNode(typeof(NumericVariableImpl).ToString()) as IVariableDef;
                    break;

                case eVariableType.enumeration:
                    result = IDecisionTreeInterface.CreateNewNode(typeof(EnumVariableImpl).ToString()) as IVariableDef;
                    break;

                case eVariableType.date:
                    result = IDecisionTreeInterface.CreateNewNode(typeof(DateVariableImpl).ToString()) as IVariableDef;
                    break;

                case eVariableType.boolean:
                    result = IDecisionTreeInterface.CreateNewNode(typeof(BoolVariableImpl).ToString()) as IVariableDef;
                    break;
            }

            if (result != null)
            {
                result.Name = previousVar.Name;

                int index = previousVar.Parent.Nodes.IndexOf(previousVar);
                previousVar.Parent.Nodes.Remove(previousVar);
                previousVar.Parent.Nodes.Insert(index, result);

                result.Parent = previousVar.Parent;
                (result as IBaseNode).Tree = this;

                OnUpdated();

            }

            return result;

        }
        IBaseNode IDecisionTree.GetVariableByName(string name)
        {
            List<IBaseNode> variables = IDecisionTreeInterface.GetVariables();
            foreach (IBaseNode node in variables)
                if (node.Name == name)
                    return node as IVariableDef;

            return null;

        }
        IBaseNode IDecisionTree.GetVariableByReference(string reference)
        {
            List<IBaseNode> variables = IDecisionTreeInterface.GetVariables();
            foreach (IBaseNode node in variables)
                if (node.Reference == reference)
                    return node;

            return null;

        }
        IBaseNode IDecisionTree.GetNodeByReference(string reference)
        {
            return RootNode != null
                ? GetNodeByReference(RootNode, reference)
                : null; 
        }
        protected IBaseNode GetNodeByReference(IBaseNode node, string reference)
        {
            if (node.Reference == reference)
                return node;

            foreach(IBaseNode n in node.Nodes)
            {
                IBaseNode result = GetNodeByReference(n, reference);
                if (result != null)
                    return result;
            }

            return null;

        }
        IBaseNode IDecisionTree.GetDomainObjectByReference(string reference)
        {
            List<IBaseNode> domainObjects = IDecisionTreeInterface.GetDomainObjects();
            foreach (IBaseNode node in domainObjects)
                if (node.Reference == reference)
                    return node;

            return null;

        }
        List<IBaseNode> IDecisionTree.GetVariables()
        {
            IBaseNode variables = RootNode.GetNode(eNodeType.VariableDefinitions);
            return variables != null ? variables.Nodes : new List<IBaseNode>();

        }
        List<IBaseNode> IDecisionTree.GetVariables(IBaseNode linkedTo)
        {
            List<IBaseNode> result = new List<IBaseNode>();
            IBaseNode variables = RootNode.GetNode(eNodeType.VariableDefinitions);
            if (variables != null)
                foreach (IBaseNode node in variables.Nodes)
                    LinkedToDomainObject(node, linkedTo, result);

            return result;

        }
        private void LinkedToDomainObject(IBaseNode Node, IBaseNode linkedTo, List<IBaseNode> list)
        {
            IDataPersistence dataPersistence = GetIDataPersistence(Node);
            if (dataPersistence != null && dataPersistence.DataConnections.Contains(linkedTo.Reference))
                list.Add(Node);

            foreach (IBaseNode n in Node.Nodes)
                LinkedToDomainObject(n, linkedTo, list);

        }
        public static IDataPersistence GetIDataPersistence(IBaseNode node)
        {
            IDataPersistence result = null;
            if (node is IDomainObject)
                result = (node as IDomainObject).DataPersistence;

            if (node is IVariableDef)
                result = (node as IVariableDef).DataPersistence;

            return result;

        }
        List<IBaseNode> IDecisionTree.GetDomainObjects()
        {
            List<IBaseNode> result = new List<IBaseNode>();

            IBaseNode variables = RootNode.GetNode(eNodeType.VariableDefinitions);
            if(variables != null)
                result = GetDomainObjectsFromList(variables.Nodes);

            return result;

        }
        private List<IBaseNode> GetDomainObjectsFromList(List<IBaseNode> list)
        {
            List<IBaseNode> result = new List<IBaseNode>();
            foreach (IBaseNode node in list)
                if (node is IDomainObject)
                {
                    result.Add(node);
                    result.AddRange(GetDomainObjectsFromList(node.Nodes).ToArray());

                }

            return result;

        }
        void IDecisionTree.RemoveVariable(IBaseNode removeNode)
        {
            IBaseNode variables = RootNode.GetNode(eNodeType.VariableDefinitions);
            variables.Nodes.Remove(removeNode);
            foreach (IBaseNode node in RootNode.Nodes)
                node.RemoveNode(removeNode);
        }
        void IDecisionTree.ImportVariables(IBaseNode importVariables)
        {
            IBaseNode variables = IDecisionTreeInterface.RootNode.GetNode(eNodeType.VariableDefinitions);
            if(variables != null)
                if(variables.Nodes.Count == 0)
                {
                    foreach (IBaseNode var in importVariables.Nodes)
                        variables.Nodes.Add(var);
                }
                else
                {
                    // Will not occur (yet)
                }
        }
        //Used when Domain template is added later
        void IDecisionTree.ImportVariables(List<IBaseNode> importVariables)
        {
            IBaseNode variables = IDecisionTreeInterface.RootNode.GetNode(eNodeType.VariableDefinitions);
            if (variables != null)
                if (variables.Nodes.Count == 0)
                {
                    foreach (IBaseNode var in importVariables)
                        variables.Nodes.Add(var);
                }
                else
                {
                    //TODO Slim algorithme om variabelen met dezelfde naam 
                    //en pad naar root te vervangen
                    //Domain object-1
                    //      Variable-1
                    //Variabele-1
                    //
                    //In het beslismodel zelf alle referenties van de oude variabele . domain object
                    //ook veranderen

                    IBaseNode source = IDecisionTreeInterface.CreateNewNode(eNodeType.VariableDefinitions);
                    source.Nodes.AddRange(importVariables.ToArray());
                    DomainTemplateMerger.Merge(variables, source);
                }
            (RootNode as IBaseNodeImpl).OnUpdatedAndRefresh();
        }

        IBaseNode IDecisionTree.CreateNewNode(eNodeType nodeType, IBaseNode parent, string name)
        {
            IBaseNode result = null;

            if (name == "")
                name = GetNodeName(nodeType.ToString(), parent);

            switch (nodeType)
            {
                case eNodeType.Root:
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IRootNodeImpl).ToString());
                    break;

                case eNodeType.VariableDefinitions:
                    name = Constants.VariableTreeName;
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IBaseNodeImpl).ToString());
                    break;

                case eNodeType.VarDefinition:
                    name = GetNodeName("variable", parent);
                    result = IDecisionTreeInterface.CreateNewNode(typeof(CharVariableImpl).ToString());
                    break;

                case eNodeType.DomainObject:
                    name = GetNodeName("domain object", parent);
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IDomainObjectImpl).ToString());
                    break;

                case eNodeType.Expression:
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IExpressionImpl).ToString());
                    break;

                case eNodeType.Branch:
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IBranchImpl).ToString());
                    break;

                case eNodeType.DataObjects:
                    name = Constants.DataNodesTreeName;
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IBaseNodeImpl).ToString());
                    result.NodeType = eNodeType.DataObjects;
                    break;

                case eNodeType.DataObject:
                    name = GetNodeName("data object", parent);
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IDataObjectImpl).ToString());
                    break;

                case eNodeType.DataSources:
                    name = Constants.DataSourcesTreeName;
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IBaseNodeImpl).ToString());
                    result.NodeType = eNodeType.DataSources;
                    break;

                case eNodeType.DataSource:
                    name = GetNodeName("data source", parent);
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IDataSourceImpl).ToString());
                    break;

                case eNodeType.DataDefinition:
                    name = GetNodeName("data definition", parent);
                    result = IDecisionTreeInterface.CreateNewNode(typeof(IDataDefinitionImpl).ToString());
                    break;

            }

            if (result != null)
            {
                result.Parent = parent;

                result.NodeType = nodeType;
                result.Name = name;
                result.Tree = this;

                if (parent != null)
                    parent.AddNode(result);
            }

            return result;

        }
        IBaseNode IDecisionTree.CreateNewNode(string type)
        {
            SakwaMapping mapping = Mapping;
            if (mapping != null)
                type = mapping.StringAsBaseNode(type);

            IBaseNode result = null;

            if (appAssembly != null)
                result = (IBaseNode)appAssembly.CreateInstance(type);

            if (result == null)
                result = (IBaseNode)Assembly.GetCallingAssembly().CreateInstance(type);

            if (result != null)
            {
                result.Tree = this;

                if (PostNodeInitialize != null)
                    PostNodeInitialize.Invoke(result);

            }

            return result;

        }
        string IDecisionTree.BaseNodePersistName(IBaseNode baseNode)
        {
            SakwaMapping mapping = Mapping;
            if (mapping != null)
                return mapping.BaseNodeAsString(baseNode);

            return typeof(IBaseNodeImpl).ToString();

        }

        bool IDecisionTree.Load(IPersistence persistence, string fullName)
        {
            if(IBaseNodeInterface.Retrieve(persistence))
            {
                FullPath = persistence.Name != "" ? persistence.Name : fullName;
                return true;

            }

            return false;

        }
        bool IDecisionTree.Save(IPersistence persistence, string fullName)
        {
            FullPath = persistence.Name != "" ? persistence.Name : fullName;
            return IBaseNodeInterface.Persist(persistence);
        }
        string IDecisionTree.FullPath { get { return FullPath; } }

        IBaseNode IDecisionTree.LoadVariables(IPersistence persistence)
        {
            if (persistence.NextRecord())
            {
                string type = persistence.GetFieldValue(Constants.BaseNode_ClassType, Constants.BaseNode_ClassType);
                IBaseNode node = IDecisionTreeInterface.CreateNewNode(type);

                node.Reference = persistence.GetFieldValue(Constants.BaseNode_Reference, new Guid().ToString());
                int count = persistence.GetFieldValue(Constants.BaseNode_NodeCount, 0);
                node.NodeType = (eNodeType)Enum.Parse(typeof(eNodeType), persistence.GetFieldValue(Constants.BaseNode_Type, eNodeType.unknown.ToString()), true);
                node.Name = persistence.GetFieldValue(Constants.BaseNode_Name, "");

                node.Parent = this;
                node.Tree = this;

                if (persistence.NextRecord())
                {
                    type = persistence.GetFieldValue(Constants.BaseNode_ClassType, Constants.BaseNode_ClassType);
                    IBaseNode variables = IDecisionTreeInterface.CreateNewNode(type);

                    variables.Parent = node;
                    variables.Tree = this;
                    variables.Retrieve(persistence);

                    return variables;

                }
            }

            return null;

        }
        Dictionary<IBaseNode, string> IDecisionTree.SubModels { get { return SubModels; } }
        void IDecisionTree.AddSubModel(IDomainObject domainObject)
        {
            if(domainObject != null)
            {
                string model = (domainObject as IDomainObjectImpl).FullModelName;
                if (model != "")
                {
                    SubModels.Add(domainObject, model);
                }
            }
        }
        List<SakwaMapping> IDecisionTree.SakwaMappings { get { return SakwaMappings; } }

        bool IDecisionTree.LoadDomainTemplate(IRootNode rootNode)
        {
            IPersistence persistence = Persistence.Clone(rootNode.DomainTemplate);
            if (persistence != null)
            {
                IDecisionTree subTree = IDecisionTreeInterface.Clone();

                IBaseNode variables = subTree.LoadVariables(persistence);
                IDecisionTreeInterface.ImportVariables(variables);

                return true;

            }

            return false;

        }


        bool IDecisionTree.IsDirty
        {
            get
            {
                bool result = true;

                if (FullPath != "")
                    result = IsDifferent();

                if (NodeCount(RootNode) == 1)
                    return false;

                return result;

            }
        }

        IPersistence IDecisionTree.Persistence
        {
            get { return Persistence; }
            set { Persistence = value; }
        }

        IDecisionTree IDecisionTree.Clone()
        {
            SakwaMapping mapping = SakwaMappings.Count > 1 ? SakwaMappings[0] : null;
            IDecisionTree result = new IDecisionTreeImpl(appAssembly, mapping, PostNodeInitialize);
            return result;
        }


        protected override List<eNodeType> AllowedAddNodes
        {
            get
            {
                List<eNodeType> result = new List<eNodeType>();
                if (RootNode == null)
                    result.Add(eNodeType.Root);

                return result;
            }
        }
        protected override List<eNodeType> AllowedRemoveNodes
        {
            get
            {
                List<eNodeType> result = new List<eNodeType>();
                return result;
            }
        }
        protected string GetNodeName(string name, IBaseNode node)
        {
            if(node != null)
            {
                int index = 1;
                bool found = false;
                string newName = name;

                while (!found)
                {
                    newName = string.Format("{0}-{1}", name, index++);
                    found = node.GetNode(newName) == null;

                }

                return newName;

            }

            return name;
        }

        protected override void RemoveNode(IBaseNode nodeRemoved)
        {
            foreach(IBaseNode node in RootNode.Nodes)
                node.RemoveNode(nodeRemoved);

            base.RemoveNode(nodeRemoved);

        }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            if (persistence.AddRecord())
            {
                string type = IDecisionTreeInterface.BaseNodePersistName(RootNode);
                persistence.UpsertField(Constants.BaseNode_ClassType, type);

                phase = ePersistence.Final;

                return RootNode.Persist(persistence);
                
            }

            return false;

        }
        protected override bool Retrieve(IPersistence persistence,ref ePersistence phase)
        {
            Persistence = persistence;

            while (persistence.NextRecord())
            {
                string type = persistence.GetFieldValue(Constants.BaseNode_ClassType, Constants.BaseNode_Type);
                if (type == "sakwa.IBaseNodeImpl")
                    type = "sakwa.IRootNodeImpl";

                RootNode = IDecisionTreeInterface.CreateNewNode(type);
                RootNode.Parent = this;
                RootNode.Tree = this;

                phase = ePersistence.Final;

                RootNode.NodeLoaded += RootNode_NodeLoaded;

                return RootNode.Retrieve(persistence);

            }

            return false;

        }

        private void RootNode_NodeLoaded(object sender, EventArgs e)
        {
            IBaseNode node = sender as IBaseNode;
            if (node != null && node.NodeType == eNodeType.VariableDefinitions)
            {
                if(Persistence != null)
                {
                    int i = 0;
                    while (i < SubModels.Keys.Count)
                    {
                        IPersistence persistence = getSubModel(i);

                        if (persistence != null)
                        {
                            IDecisionTree subTree = IDecisionTreeInterface.Clone();

                            IBaseNode variables = subTree.LoadVariables(persistence);
                            foreach (IBaseNode subModel in subTree.SubModels.Keys)
                                IDecisionTreeInterface.AddSubModel(subModel as IDomainObject);

                            IBaseNode baseNode = SubModels.Keys.ElementAt(i);

                            List<IBaseNode> linkedVariables = LinkVariables(node, variables);
                            foreach (IBaseNode linkedNode in linkedVariables)
                                baseNode.AddNode(linkedNode);

                        }

                        i++;

                    }
                }
            }
        }

        protected IPersistence getSubModel(int index)
        {
            IDomainObjectImpl domainObject = SubModels.Keys.ElementAt(index) as IDomainObjectImpl;
            IPersistence persistence = Persistence.Clone(SubModels[domainObject]);

            if (persistence == null && LinkSubtreeError != null)
            {
                LinkSubTree subTree = new LinkSubTree(SubModels[domainObject]);
                LinkSubtreeError.Invoke(subTree);

                if (subTree.Action == LinkSubTree.eLinkAction.Update)
                {
                    domainObject.FullModelName = subTree.Link;
                    SubModels[domainObject] = subTree.Link;

                    return getSubModel(index);

                }
            }

            return persistence;

        }

        protected List<IBaseNode> LinkVariables(IBaseNode DestVariables, IBaseNode SourceVariables)
        {
            int i = 0;
            while (i < SourceVariables.Nodes.Count)
            {
                IBaseNode src = SourceVariables.Nodes[i];
                IBaseNode dest = FindNode(DestVariables, src);
                if (dest != null)
                    SourceVariables.Nodes.Remove(src);
                else
                    i++;

            }

            return SourceVariables.Nodes;

        }

        protected IBaseNode FindNode(IBaseNode haystack, IBaseNode needle)
        {
            if (haystack != null)
                foreach (IBaseNode n in haystack.Nodes)
                    if (n.NodeType == needle.NodeType && n.Name == needle.Name)
                        return n;

            return null;

        }

        protected int NodeCount(IBaseNode node = null)
        {
            int result = node.Nodes.Count;

            foreach (IBaseNode n in node.Nodes)
                result += NodeCount(n);

            return result;

        }
        protected bool IsDifferent()
        {
            IPersistence current = new XmlPersistenceImpl(FullPath, true);
            IBaseNodeInterface.Persist(current);

            IPersistence existing = new XmlPersistenceImpl(FullPath);

            return current.RawContent != existing.RawContent;

        }

        protected SakwaMapping Mapping
        {
            get
            {
                string fileVersion = Persistence != null ? Persistence.FileVersion : "0.0";
                foreach (SakwaMapping mapping in SakwaMappings)
                    if (mapping.FileVersion == fileVersion)
                        return mapping;

                return null;

            }
        }

        protected string FullPath = "";
        public IBaseNode RootNode = null;

        protected IPersistence Persistence = null;

        protected Dictionary<IBaseNode, string> SubModels = new Dictionary<IBaseNode, string>();

        protected IDecisionTree IDecisionTreeInterface {  get { return this as IDecisionTree; } }

        protected List<SakwaMapping> SakwaMappings = new List<SakwaMapping>();

        protected void InitMapping()
        {
            SakwaMapping mapping = new SakwaMapping("0.0");
            mapping.SakwaObjectMapping.Add(typeof(IBaseNodeImpl).ToString(), typeof(IBaseNodeImpl).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IRootNodeImpl).ToString(), typeof(IRootNodeImpl).ToString());

            mapping.SakwaObjectMapping.Add(typeof(CharVariableImpl).ToString(), typeof(CharVariableImpl).ToString());
            mapping.SakwaObjectMapping.Add(typeof(NumericVariableImpl).ToString(), typeof(NumericVariableImpl).ToString());
            mapping.SakwaObjectMapping.Add("sakwa.IntVariableImpl", typeof(NumericVariableImpl).ToString());
            mapping.SakwaObjectMapping.Add(typeof(EnumVariableImpl).ToString(), typeof(EnumVariableImpl).ToString());
            mapping.SakwaObjectMapping.Add(typeof(BoolVariableImpl).ToString(), typeof(BoolVariableImpl).ToString());
            mapping.SakwaObjectMapping.Add(typeof(DateVariableImpl).ToString(), typeof(DateVariableImpl).ToString());

            mapping.SakwaObjectMapping.Add(typeof(IDomainObjectImpl).ToString(), typeof(IDomainObjectImpl).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IExpressionImpl).ToString(), typeof(IExpressionImpl).ToString());
            mapping.SakwaObjectMapping.Add("sakwa.IAssignmentImpl", typeof(IExpressionImpl).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IBranchImpl).ToString(), typeof(IBranchImpl).ToString());

            mapping.SakwaObjectMapping.Add(typeof(IDataObjectImpl).ToString(), typeof(IDataObjectImpl).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IDataSourceImpl).ToString(), typeof(IDataSourceImpl).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IDataDefinitionImpl).ToString(), typeof(IDataDefinitionImpl).ToString());

            IDecisionTreeInterface.SakwaMappings.Add(mapping);

        }

        public void setAssembly(Assembly assembly) { appAssembly = assembly; }
        Assembly appAssembly = null;
    }


}
