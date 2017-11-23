using System;
using System.Collections.Generic;

namespace sakwa
{
    public interface IRootNode : IBaseNode
    {
        string DomainTemplate { get; set; }
    }
}
