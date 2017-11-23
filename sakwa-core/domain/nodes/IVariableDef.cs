using System;

namespace sakwa
{
    public enum eVariableType {  numeric, character, enumeration, date, boolean }

    public interface IVariableDef : IBaseNode
    {
        eVariableType VariableType { get; set; }
        string Value { get; set; }

        event EventHandler VariableTypeChanged;

        IDataPersistence DataPersistence { get; }

    }
}
