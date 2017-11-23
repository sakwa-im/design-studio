using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace sakwa
{
    public class IRootNodeImpl : IBaseNodeImpl, IRootNode
    {
        public IRootNodeImpl() : base()
        {
            NodeType = eNodeType.Root;
        }
        public IRootNodeImpl(string name, eNodeType nodeType) : base(name, eNodeType.Expression)
        {
            NodeType = eNodeType.Root;
        }

        string IRootNode.DomainTemplate
        {
            get { return _DomainTemplate; }
            set
            {
                if(value != _DomainTemplate)
                {
                    _DomainTemplate = value;
                    OnUpdated();

                    if (_DomainTemplate != "")
                        Tree.LoadDomainTemplate(this);

                }
            }
        }
        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    string relativePath = persistence.GetRelativePath(_DomainTemplate);
                    persistence.UpsertField(Constants.Root_Template, relativePath);

                    break;
            }

            return true;

        }
        protected override bool Retrieve(IPersistence persistence, ref ePersistence phase)
        {
            base.Retrieve(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    string relativePath = persistence.GetFieldValue(Constants.Root_Template, "");
                    _DomainTemplate = persistence.GetFullPath(relativePath);

                    break;
            }

            return true;

        }

        protected override NodeEqualityCollection Compare(IBaseNode compareWith, eCompareMode mode)
        {
            NodeEqualityCollection result = base.Compare(compareWith, mode);

            return result;

        }
        protected bool isEqual(List<string> list, string[] input)
        {
            if (input.Length != list.Count)
                return false;

            for (int i = 0; i < input.Length; i++)
                if (input[i] != list[i])
                    return false;

            return true;

        }

        protected string _DomainTemplate = "";
        protected IRootNode IRootNodeInterface {  get { return this; } }

    }
}
