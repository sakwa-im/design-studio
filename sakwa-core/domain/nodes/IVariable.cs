using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public enum eVariableScope { lVal, rVal }
    public interface IVariable
    {
        eVariableScope VariableScope { get; set; }
        IDomainObject Domain { get; set; }
        IVariableDef Variable { get; set; }
        string Value { get; set; }
        IVariable Clone();
        bool Empty { get; }

    }
}
