using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace sakwa
{
    public class IDomainObjectImpl : IBaseNodeImpl, IDomainObject
    {
        public IDomainObjectImpl() : base()
        {
            NodeType = eNodeType.DomainObject;
        }
        public IDomainObjectImpl(string name, eNodeType nodeType) : base(name, eNodeType.Expression)
        {
            NodeType = eNodeType.DomainObject;
        }

        List<string> IDomainObject.Methods { get { return _Methods; } }


        IDataPersistence IDomainObject.DataPersistence { get { return DataPersistence; } }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    persistence.UpsertField(Constants.Domain_Short_Name, _ShortName);

                    string relativePath = persistence.GetRelativePath(_Model);
                    persistence.UpsertField(Constants.Domain_Sub_Model, relativePath);

                    persistence.UpsertFieldArray(Constants.Domain_Methods, _Methods.ToArray());

                    DataPersistence.Persist(persistence);

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
                    _ShortName = persistence.GetFieldValue(Constants.Domain_Short_Name, "");

                    string relativePath = persistence.GetFieldValue(Constants.Domain_Sub_Model, "");
                    _Model = persistence.GetFullPath(relativePath);

                    if (_Model != "")
                        Tree.AddSubModel(this);

                    _Methods.Clear();
                    _Methods.AddRange(persistence.GetFieldValues(Constants.Domain_Methods, ""));

                    DataPersistence.Retrieve(persistence);

                    break;
            }

            return true;

        }
        protected override bool IsReadOnly() { return _Model != ""; }

        protected override NodeEqualityCollection Compare(IBaseNode compareWith, eCompareMode mode)
        {
            NodeEqualityCollection result = base.Compare(compareWith, mode);
            if(!result.HasEqualityType(eNodeEquality.basetype))
            {
                if (this._Model != (compareWith as IDomainObjectImpl).FullModelName)
                    result.Add(eDomainObjectEquality.submodel, false, false, "Different sub models");

                if (compareWith is EnumVariableImpl)
                {
                    List<string> equal = new List<string>();
                    foreach (string elem in (compareWith as IDomainObject).Methods)
                        if (_Methods.Contains(elem))
                            equal.Add(elem);

                    if (_Methods.Count != equal.Count || (compareWith as IDomainObject).Methods.Count != equal.Count)
                        result.Add(eDomainObjectEquality.methods, false, false, "Different number of methods");

                }
            }

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
        protected override string GetName()
        {
            if (_ShortName != "")
                return _ShortName;

            string result = _Name;

            IBaseNode parent = Parent;
            while(parent != null && parent.NodeType == eNodeType.DomainObject)
            {
                string[] parentNames = (parent as IBaseNode).Name.Split(new char[] { '.'});
                
                result = parentNames[parentNames.Length -1] + "." + result;
                parent = parent.Parent;
            }
            
            return result;

        }

        protected string _ShortName = "";
        protected string _Model = "";
        protected List<string> _Methods = new List<string>();
        protected IDataPersistence DataPersistence = new IDataPersistenceImpl();

        [Browsable(false)]
        public string FullModelName { get { return _Model; } set { _Model = value; } }

    }
}
