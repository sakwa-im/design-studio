using System;

namespace sakwa
{
    public class DateVariableImpl : IVariableDefImpl
    {
        public DateVariableImpl() : base()
        {
            _VariableType = eVariableType.date;
        }
        public DateVariableImpl(string name) : base(name, eVariableType.date)
        { }

        protected override string GetValue() { return _Input; }
        protected override void SetValue(string input)
        {
            try
            {
                DateTime newValue = DateTime.Parse(input);
                if (_Value != newValue)
                {
                    _Value = newValue;
                    _Input = input;

                    DoUpdated();
                }
            }
            catch(Exception){ }
        }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    persistence.UpsertField(Constants.Variable_Type, _VariableType.ToString());
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
                    _VariableType = (eVariableType)Enum.Parse(typeof(eVariableType), persistence.GetFieldValue(Constants.Variable_Type, eVariableType.character.ToString()), true);
                    break;
            }

            return true;

        }

        public string Date {  get { return _Value.ToString(); } }

        protected DateTime _Value = new  DateTime();
        protected string _Input = "";

    }
}
