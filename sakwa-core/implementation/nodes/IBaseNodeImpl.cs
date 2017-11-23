using log4net;
using System;
using System.Collections.Generic;

namespace sakwa
{
    public class IBaseNodeImpl : IBaseNode
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(IBaseNodeImpl));

        public IBaseNodeImpl() { }
        public IBaseNodeImpl(string name) { _Name = name; }
        public IBaseNodeImpl(string name, eNodeType nodeType)
        {
            _Name = name;
            NodeType = nodeType;

        }
        string IBaseNode.Name
        {
            get { return GetName(); } 
            set
            {
                if (_Name != value)
                {
                    if (OnBeforeNameChange != null)
                        OnBeforeNameChange.Invoke(value);

                    _Name = value;
                    OnUpdated();

                }
            }
        }
        string IBaseNode.Description
        {
            get { return _Description; } 
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnUpdated();
                }
            }
        }
        string IBaseNode.Reference { get { return Reference; } set { Reference = value; } }
        IBaseNode IBaseNode.AddNode(IBaseNode node)
        {
            node.Parent = this;

            if (!Nodes.Contains(node))
                Nodes.Add(node);
            else
            {
                int index = Nodes.IndexOf(node);
                Nodes.RemoveAt(index);
                Nodes.Insert(index, node);

            }

            return node;

        }
        IBaseNode IBaseNode.RemoveNode(IBaseNode node)
        {
            RemoveNode(node);
            return node;
        }

        IBaseNode IBaseNode.GetNode(eNodeType nodeType)
        {
            foreach (IBaseNode node in Nodes)
                if (node.NodeType == nodeType)
                    return node;

            return null;

        }
        IBaseNode IBaseNode.GetNode(string name)
        {
            foreach (IBaseNode node in Nodes)
                if (node.Name == name)
                    return node;

            return null;

        }
        List<IBaseNode> IBaseNode.Nodes { get { return GetNodes(); } }
        eNodeType IBaseNode.NodeType {
            get { return NodeType; }
            set
            {
                if (NodeType != value)
                {
                    NodeType = value;
                    OnUpdated();
                }
            }
        }
        IBaseNode IBaseNode.Parent
        {
            get { return Parent; }
            set
            {
                if (Parent != value)
                {
                    Parent = value;
                    OnUpdated();
                }
            }
        }
        IDecisionTree IBaseNode.Tree { get { return Tree; } set { Tree = value; } }
        List<eNodeType> IBaseNode.AllowedAddNodes { get { return AllowedAddNodes; } }
        List<eNodeType> IBaseNode.AllowedRemoveNodes { get { return AllowedRemoveNodes; } }

        public event EventHandler Updated;
        public event EventHandler UpdatedAndRefresh;

        public event EventHandler NodeLoaded;

        bool IBaseNode.Persist(IPersistence persistence)
        {
            ePersistence phase = ePersistence.Initial;

            if (Persist(persistence, ref phase))
            {
                if (phase == ePersistence.Initial)
                {
                    phase = ePersistence.Final;
                    return Persist(persistence, ref phase);

                }

                return true;

            }

            return false;

        }
        bool IBaseNode.Retrieve(IPersistence persistence)
        {
            ePersistence phase = ePersistence.Initial;
            if (Retrieve(persistence, ref phase))
            {
                if (phase == ePersistence.Initial)
                {
                    phase = ePersistence.Final;
                    return Retrieve(persistence, ref phase);

                }

                return true;

            }

            return false;

        }
        NodeEqualityCollection IBaseNode.Compare(IBaseNode compareWith, eCompareMode mode)
        {
            if (compareWith != null)
                return Compare(compareWith, mode);

            NodeEqualityCollection result = new NodeEqualityCollection();
            result.Add(eNodeEquality.basetype, true, true, "Compare with null value");
            return result;
        }
        bool IBaseNode.ReadOnly { get { return IsReadOnly(); } }
        IBaseNode IBaseNode.MergeNode(IBaseNode baseNode) { return MergeNode(baseNode); }
        IBaseNode IBaseNode.Clone() { return Clone(); }
        public event BeforeNameChange OnBeforeNameChange;

        protected enum ePersistence {Variables, Initial, Final}
        protected virtual bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            switch (phase)
            {
                case ePersistence.Initial:
                    persistence.UpsertField(Constants.BaseNode_Reference, Reference);
                    persistence.UpsertField(Constants.BaseNode_NodeCount, IsReadOnly() ? "0" : Nodes.Count.ToString());
                    persistence.UpsertField(Constants.BaseNode_Type, NodeType.ToString());
                    persistence.UpsertField(Constants.BaseNode_Name, _Name);
                    persistence.UpsertField(Constants.BaseNode_Description, _Description);
                    break;

                case ePersistence.Final:
                    if (!IsReadOnly())
                    {
                        foreach (IBaseNode node in Nodes)
                            if (persistence.AddRecord())
                            {
                                string type = Tree.BaseNodePersistName(node);
                                persistence.UpsertField(Constants.BaseNode_ClassType, type);
                                node.Persist(persistence);

                            }
                    }
                    break;
            }

            return true;

        }
        protected virtual bool Retrieve(IPersistence persistence, ref ePersistence phase)
        {
            switch (phase)
            {
                case ePersistence.Initial:
                    Reference = persistence.GetFieldValue(Constants.BaseNode_Reference, new Guid().ToString());
                    count = persistence.GetFieldValue(Constants.BaseNode_NodeCount, 0);
                    if(NodeType == eNodeType.unknown)
                        NodeType = (eNodeType)Enum.Parse(typeof(eNodeType), persistence.GetFieldValue(Constants.BaseNode_Type, eNodeType.unknown.ToString()), true);
                    _Name = persistence.GetFieldValue(Constants.BaseNode_Name, "");
                    _Description = persistence.GetFieldValue(Constants.BaseNode_Description, "");
                    break;

                case ePersistence.Final:
                    if (!IsReadOnly())
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (persistence.NextRecord())
                            {
                                string type = persistence.GetFieldValue(Constants.BaseNode_ClassType, Constants.BaseNode_ClassType);

                                IBaseNode node = Tree.CreateNewNode(type);
                                node.NodeLoaded += NodeLoaded;
                                node.Parent = this;

                                Nodes.Add(node);

                                node.Retrieve(persistence);

                            }
                        }

                        OnNodeLoaded();

                    }
                    break;
            }

            return true;

        }
        protected virtual NodeEqualityCollection Compare(IBaseNode compareWith, eCompareMode mode = eCompareMode.Default)
        {
            NodeEqualityCollection result = new NodeEqualityCollection();

            if (NodeType != compareWith.NodeType)
                result.Add(eNodeEquality.basetype, false, true, "Different types");

            if(mode == eCompareMode.Full && Reference != compareWith.Reference)
                result.Add(eNodeEquality.reference, false, false, "Different references");

            return result;

        }

        protected virtual IBaseNode MergeNode(IBaseNode baseNode)
        {
            return this;
        }

        protected virtual void RemoveNode(IBaseNode nodeRemoved)
        {
            if(Nodes.Contains(nodeRemoved))
                Nodes.Remove(nodeRemoved);

            foreach (IBaseNode node in Nodes)
                node.RemoveNode(nodeRemoved);

        }

        protected virtual List<eNodeType> AllowedAddNodes
        {
            get
            {
                List<eNodeType> result = new List<eNodeType>();
                switch (NodeType)
                {
                    case eNodeType.Root:
                    case eNodeType.Branch:
                        result.Add(eNodeType.Expression);
                        result.Add(eNodeType.Branch);
                        break;

                    case eNodeType.VariableDefinitions:
                        result.Add(eNodeType.VarDefinition);
                        result.Add(eNodeType.DomainObject);
                        break;

                    case eNodeType.DomainObject:
                        result.Add(eNodeType.VarDefinition);
                        result.Add(eNodeType.DomainObject);
                        break;

                    case eNodeType.DataObjects:
                        result.Add(eNodeType.DataObject);
                        break;

                    case eNodeType.DataSources:
                        result.Add(eNodeType.DataSource);
                        break;

                    case eNodeType.DataSource:
                        result.Add(eNodeType.DataDefinition);
                        break;
                }
                return result;
            }
        }
        protected virtual List<eNodeType> AllowedRemoveNodes
        {
            get
            {
                List<eNodeType> result = new List<eNodeType>();
                switch (NodeType)
                {
                    case eNodeType.Root:
                        break;

                    case eNodeType.VariableDefinitions:
                    case eNodeType.DataObjects:
                    case eNodeType.DataSources:
                        break;

                    default:
                        result.Add(NodeType);
                        break;
                }
                return result;
            }
        }

        protected virtual bool IsReadOnly() { return false; }

        protected virtual string GetName()
        {
            if (this is IExpression)
                return _Name;

            return _Name;
        }

        protected virtual List<IBaseNode> GetNodes()
        {
            return Nodes;
        }
        protected virtual IBaseNode Clone()
        {
            IBaseNode result = new IBaseNodeImpl(_Name, NodeType);
            result.Tree = Tree;

            return result;

        }
        protected virtual void FinalizeClone(IBaseNode node)
        {
            node.Updated += Updated;
        }

        protected void OnUpdated() { Updated?.Invoke(this, new EventArgs()); }
        public void OnUpdatedAndRefresh() { UpdatedAndRefresh?.Invoke(this, new EventArgs()); }
        protected void OnNodeLoaded() { NodeLoaded?.Invoke(this, new EventArgs()); }

        protected IDecisionTree Tree = null;
        protected IBaseNode Parent = null;
        
        protected string _Name = "";
        protected string _Description = "";
        protected string Reference = Guid.NewGuid().ToString();
        protected List<IBaseNode> Nodes = new List<IBaseNode>();
        protected eNodeType NodeType = eNodeType.unknown;
        protected int count = 0;
        protected IBaseNode IBaseNodeInterface {  get { return this as IBaseNode; } }

    }
}
