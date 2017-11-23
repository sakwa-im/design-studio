using System.Collections.Generic;

namespace sakwa
{
    public class EnumVariableImpl : IVariableDefImpl
    {
        public EnumVariableImpl() : base()
        {
            _VariableType = eVariableType.enumeration;
        }
        public EnumVariableImpl(string name) : base(name, eVariableType.enumeration)
        { }

        protected bool isEqual(string[] input)
        {
            if (input.Length != _Elements.Count)
                return false;

            for (int i = 0; i < input.Length; i++)
                if (input[i] != _Elements[i])
                    return false;

            return true;

        }
        protected bool Matches(string input)
        {
            return _Elements.Contains(input);
        }

        protected override string GetValue() { return _Value; }
        protected override void SetValue(string input)
        {
            if (_Value != input && Matches(input))
            {
                _Value = input;
                DoUpdated();
            }
        }
        protected override NodeEqualityCollection Compare(IBaseNode compareWith, eCompareMode mode)
        {
            NodeEqualityCollection result = base.Compare(compareWith, mode);
            if (compareWith is EnumVariableImpl)
            {
                List<string> equal = new List<string>();
                foreach (string elem in (compareWith as EnumVariableImpl).GetElements())
                    if (_Elements.Contains(elem))
                        equal.Add(elem);

                if (_Elements.Count != equal.Count || (compareWith as EnumVariableImpl).GetElements().Count != equal.Count)
                    result.Add(eVariableEquality.domain, false, false, "Different number of domain elements");

            }

            return result;

        }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    persistence.UpsertField(Constants.EnumVariable_Value, _Value.ToString());
                    persistence.UpsertFieldArray(Constants.EnumVariable_Choices, _Elements.ToArray());
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
                    _Value = persistence.GetFieldValue(Constants.EnumVariable_Value, "");
                    _Elements.Clear();
                    _Elements.AddRange(persistence.GetFieldValues(Constants.EnumVariable_Choices, ""));
                    break;
            }

            return true;

        }

        public List<string> GetElements() { return _Elements; }
        protected string _Value = "";
        protected List<string> _Elements = new List<string>();

    }
}
