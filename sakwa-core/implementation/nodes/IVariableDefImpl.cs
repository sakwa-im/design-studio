using System;

namespace sakwa
{
    public class IVariableDefImpl : IBaseNodeImpl, IVariableDef
    {
        public IVariableDefImpl()
        {
            NodeType = eNodeType.VarDefinition;
        }
        public IVariableDefImpl(string name, eVariableType variableType)
            : base(name, eNodeType.VarDefinition)
        {
            _Name = name;
            (this as IVariableDef).VariableType = variableType;

        }
        eVariableType IVariableDef.VariableType
        {
            get { return _VariableType; }
            set
            {
                if(_VariableType != value)
                {
                    _VariableType = value;
                    DoVariableTypeChanged();
                }
            }
        }
        string IVariableDef.Value { get { return GetValue(); } set { SetValue(value); } }
        IDataPersistence IVariableDef.DataPersistence { get { return DataPersistence; } }

        protected override NodeEqualityCollection Compare(IBaseNode compareWith, eCompareMode mode)
        {
            NodeEqualityCollection result = new NodeEqualityCollection();
            if (NodeType != compareWith.NodeType)
                result.Add(eNodeEquality.basetype, false, true, "Different types");

            if (_VariableType != (compareWith as IVariableDef).VariableType)
                result.Add(eVariableEquality.type, false, true, "Different variable type");

            if (GetValue() != (compareWith as IVariableDef).Value)
                result.Add(eVariableEquality.value, false, false, "Different variable values");

            return result;

        }

        public event EventHandler VariableTypeChanged;

        public void DoUpdated()
        {
            OnUpdated();
        }
        public void DoVariableTypeChanged()
        {
            VariableTypeChanged?.Invoke(this, new EventArgs());
        }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    persistence.UpsertField(Constants.Variable_Type, _VariableType.ToString());
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
                    _VariableType = (eVariableType)Enum.Parse(typeof(eVariableType), persistence.GetFieldValue(Constants.Variable_Type, eVariableType.character.ToString()), true);
                    DataPersistence.Retrieve(persistence);

                    break;
            }

            return true;

        }

        protected virtual string GetValue() { return ""; }
        protected virtual void SetValue(string newValue) { }

        protected override void FinalizeClone(IBaseNode node)
        {
            base.FinalizeClone(node);
            (node as IVariableDef).VariableTypeChanged += VariableTypeChanged;

        }

        protected eVariableType _VariableType = eVariableType.character;
        protected IDataPersistence DataPersistence = new IDataPersistenceImpl();

    }

}
