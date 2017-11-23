using sakwa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public interface IValueEntity
    {
        string Value { get; set; }
        string Reference { get; set; }
        int Sequence { get; set; }
        IValueEntity Clone();
    }
}
