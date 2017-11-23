using System;

namespace sakwa
{
    public class BoolVariableImpl : IVariableDefImpl
    {
        public BoolVariableImpl() : base()
        {
            _VariableType = eVariableType.boolean;
        }
        public BoolVariableImpl(string name) : base(name, eVariableType.boolean)
        { }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    persistence.UpsertField(Constants.IntVariable_Value, _Value.ToString());
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
                    _Value = persistence.GetFieldValue(Constants.IntVariable_Value, false);
                    break;
            }

            return true;

        }

        protected override string GetValue() { return _Value.ToString(); }
        protected override void SetValue(string input)
        {
            try
            {
                bool newValue = bool.Parse(input);
                if (_Value != newValue)
                {
                    _Value = newValue;
                    DoUpdated();
                }
            }
            catch(Exception ex) 
            {
                log.Debug(ex.ToString());
            }
        }

        protected bool _Value = false;
    }
}
