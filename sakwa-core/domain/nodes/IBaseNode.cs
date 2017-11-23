using System;
using System.Collections.Generic;

namespace sakwa
{
    public enum eNodeType { unknown, Tree, Root, VariableDefinitions,
        VarDefinition, DomainObject,
        Expression, Branch,
        DataObjects, DataObject,
        DataSources, DataSource,
        DataDefinition }

    public delegate void BeforeNameChange(string newName);

    public interface IBaseNode
    {
        string Name { get; set; }
        string Description { get; set; }
        string Reference { get; set; }
        List<IBaseNode> Nodes { get; }
        //IVariableDef Variable { get; set; }
        eNodeType NodeType { get; set; }

        IBaseNode Parent { get; set; }

        IDecisionTree Tree { get; set; }

        IBaseNode AddNode(IBaseNode node);
        IBaseNode RemoveNode(IBaseNode node);
        IBaseNode GetNode(eNodeType nodeType);
        IBaseNode GetNode(string name);
        List<eNodeType> AllowedAddNodes { get; }
        List<eNodeType> AllowedRemoveNodes { get; }

        event EventHandler Updated;
        event EventHandler UpdatedAndRefresh;

        event EventHandler NodeLoaded;

        bool Persist(IPersistence persistence);
        bool Retrieve(IPersistence persistence);

        NodeEqualityCollection Compare(IBaseNode compareWith, eCompareMode mode = eCompareMode.Default);

        IBaseNode MergeNode(IBaseNode baseNode);

        bool ReadOnly { get; }

        IBaseNode Clone();

        event BeforeNameChange OnBeforeNameChange;

    }
}
