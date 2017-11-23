using System.Collections.Generic;

namespace sakwa
{
    public delegate IBaseNode CreateBaseNode(string type);

    public delegate void LinkSubtreeError(LinkSubTree subTree);

    public delegate void PostNodeInitialize(IBaseNode node);

    public class LinkSubTree
    {
        public LinkSubTree() { }
        public LinkSubTree(string link)
        {
            Link = link;
        }

        public enum eLinkAction { Ignore, Update }

        public eLinkAction Action = eLinkAction.Ignore;
        public string Link = "";

    }

    public interface IDecisionTree : IBaseNode
    {
        IBaseNode RootNode { get; set; }
        IBaseNode CreateNewNode(eNodeType nodeType, IBaseNode parent = null, string name = "");
        IBaseNode CreateNewNode(string type);
        string BaseNodePersistName(IBaseNode baseNode);

        IBaseNode GetVariableByName(string name);
        IBaseNode GetVariableByReference(string reference);
        IBaseNode GetNodeByReference(string reference);
        IBaseNode GetDomainObjectByReference(string reference);
        List<IBaseNode> GetVariables();
        List<IBaseNode> GetVariables(IBaseNode linkedTo);
        List<IBaseNode> GetDomainObjects();
        void RemoveVariable(IBaseNode removeNode);
        IVariableDef ChangeVariable(IVariableDef previousVar);

        void ImportVariables(IBaseNode importVariables);
        void ImportVariables(List<IBaseNode> importVariables);

        string FullPath { get; }
        bool Load(IPersistence persistence, string fullName);
        bool Save(IPersistence persistence, string fullName);
        bool IsDirty { get; }

        IBaseNode LoadVariables(IPersistence persistence);

        Dictionary<IBaseNode, string> SubModels { get; }
        void AddSubModel(IDomainObject domainObject);
        bool LoadDomainTemplate(IRootNode rootNode);

        List<SakwaMapping> SakwaMappings { get; }

        IPersistence Persistence { get; set; }

        new IDecisionTree Clone();

        event LinkSubtreeError LinkSubtreeError;

        event PostNodeInitialize PostNodeInitialize;

    }

    public class SakwaMapping
    {
        public SakwaMapping(string fileVersion)
        {
            _FileVersion = fileVersion;
        }

        public string FileVersion { get { return _FileVersion; } }

        //Key = base type, value = base type to create
        public Dictionary<string, string> SakwaObjectMapping = new Dictionary<string, string>();

        public string BaseNodeAsString(IBaseNode baseNode)
        {
            string result = baseNode != null ? baseNode.GetType().ToString() : typeof(IBaseNodeImpl).ToString();

            foreach (string key in SakwaObjectMapping.Keys)
            {
                if (SakwaObjectMapping[key] == result)
                {
                    result = key;
                    break;

                }
            }

            return result;

        }

        public string StringAsBaseNode(string baseNode)
        {
            string result = typeof(IBaseNodeImpl).ToString();

            foreach (string key in SakwaObjectMapping.Keys)
            {
                if (key == baseNode)
                {
                    result = SakwaObjectMapping[key];
                    break;

                }
            }

            return result;

        }

        private string _FileVersion = "0.0";

    }

}
