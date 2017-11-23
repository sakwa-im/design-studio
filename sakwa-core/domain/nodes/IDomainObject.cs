using System;
using System.Collections.Generic;

namespace sakwa
{
    public interface IDomainObject : IBaseNode
    {
        List<string> Methods { get; }
        IDataPersistence DataPersistence { get; }

    }
}
