using System.Collections.Generic;

namespace sakwa
{
    public interface IVariableEntity
    {
        IVariableDef IVariableDef { get; set; }
        string GetValue(string reference);
        bool SetValue(string newValue, string reference);

    }
}
