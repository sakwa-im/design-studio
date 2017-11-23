namespace sakwa
{
    public class NumericVariableImpl : IVariableDefImpl
    {
        public NumericVariableImpl() : base()
        {
            _VariableType = eVariableType.numeric;
        }
        public NumericVariableImpl(string name) : base(name, eVariableType.numeric)
        { }


        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    persistence.UpsertField(Constants.IntVariable_Value, _Value.ToString());
                    persistence.UpsertField(Constants.IntVariable_MinValue, MinValue.ToString());
                    persistence.UpsertField(Constants.IntVariable_MaxValue, MaxValue.ToString());
                    persistence.UpsertField(Constants.IntVariable_StepValue, StepValue.ToString());
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
                    _Value = persistence.GetFieldValue(Constants.IntVariable_Value, 0m);
                    MinValue = persistence.GetFieldValue(Constants.IntVariable_MinValue, 0m);
                    MaxValue = persistence.GetFieldValue(Constants.IntVariable_MaxValue, 0m);
                    StepValue = persistence.GetFieldValue(Constants.IntVariable_StepValue, 1m);
                    break;
            }

            return true;

        }

        protected override string GetValue() { return _Value.ToString(); }
        protected override void SetValue(string input)
        {
            decimal newValue = decimal.Parse(input, System.Globalization.NumberStyles.AllowDecimalPoint);
            if (_Value != newValue && newValue >= MinValue && newValue <= MaxValue)
            {
                _Value = newValue;
                DoUpdated();
            }
        }

        protected decimal _Value = 0m;
        protected decimal MinValue = 0m;
        protected decimal MaxValue = 10000m;
        protected decimal StepValue = 1m;
    }
}
