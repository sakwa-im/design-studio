using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public enum eNodeEquality { equal = 0, basetype = 1, reference = 2}
    public enum eVariableEquality { equal = 0, basetype = 1, reference = 2, type = 4, value = 8, domain = 16, min = 32, max = 64}
    public enum eDomainObjectEquality { equal = 0, basetype = 1, reference = 2, datapersistence = 128, methods = 256, submodel = 512}

    public enum eCompareMode { Default, Full }

    public interface INodeEquality
    {
        eNodeEquality NodeEquality { get; }
        eVariableEquality VariableEquality { get; }
        eDomainObjectEquality DomainObjectEquality { get; }
        int Equality { get; }
        bool Major { get; }
        bool Blocking { get; }
        string Remarks { get; }

    }
}
