using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public enum eAttributeDomainType { Undefined, Numerical = 1, AlfaNumerical = 2, Ordinal = 4, Enumeration = 8, URI = 16, FileOrPath = 32, Boolean = 64 }
    public enum eAttributeTarget { NotSet = 0, DataSource = 1, DataConnection = 2}
    public enum eAttributeRequirement { Mandatory = 1, Optional = 2, System = 4, User = 8, Delayed = 16, Password = 32 }

    public interface IProperty
    {
        string Caption { get; set; }
        string Name { get; }
        string Owner { get; }
        string Group { get; set; }
        string Tooltip { get; set; }

        eAttributeTarget AttributeTarget { get; }
        eAttributeRequirement AttributeRequirement { get; }
        eAttributeDomainType AttributeDomainType { get; }

        string Value { get; set; }
        string UserValue { get; set; }
        string[] Range { get; }

        IProperty Clone();
        bool Equals(IProperty property);

    } //public interface IPersoAttribute

}
