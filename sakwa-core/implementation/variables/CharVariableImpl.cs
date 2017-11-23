namespace sakwa
{
    public class CharVariableImpl : IVariableDefImpl
    {
        public CharVariableImpl() : base()
        {
            _VariableType = eVariableType.character;
        }
        public CharVariableImpl(string name) : base(name, eVariableType.character)
        { }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    persistence.UpsertField(Constants.CharVariable_Value, _Value);
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
                    _Value = persistence.GetFieldValue(Constants.CharVariable_Value, "");
                    break;
            }

            return true;

        }

        protected override string GetValue() { return _Value; }
        protected override void SetValue(string newValue)
        {
            if (_Value != newValue)
            {
                _Value = newValue;
                DoUpdated();
            }
        }

        protected string _Value = "";

    }
}

