using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class IPropertyStringImpl : IPropertyImpl
    {
        public IPropertyStringImpl(string caption, string name, string owner,
            Constraint<string> constraint,
            eAttributeTarget attributeType = eAttributeTarget.DataConnection,
            eAttributeRequirement attributeRequirement = eAttributeRequirement.User | eAttributeRequirement.Optional,
            string defaultValue = "", string group = "")
            : base(caption, name, owner, attributeType, attributeRequirement)
        {
            _Domain.Constraint = constraint;

            if (constraint != null)
                switch (constraint.ConstraintType)
                {
                    case ConstraintTest.InRange:
                        _DomainType = eAttributeDomainType.AlfaNumerical | eAttributeDomainType.Enumeration;
                        break;

                    case ConstraintTest.MinMax:
                        _DomainType = eAttributeDomainType.AlfaNumerical | eAttributeDomainType.Ordinal;
                        break;

                    case ConstraintTest.ValidPath:
                    case ConstraintTest.ValidPathFileName:
                        _DomainType = eAttributeDomainType.AlfaNumerical | eAttributeDomainType.URI;
                        break;

                    default:
                        _DomainType = eAttributeDomainType.AlfaNumerical;
                        break;

                } //switch(constraint.ConstraintType)
            else
                _DomainType = eAttributeDomainType.AlfaNumerical;

            Value = defaultValue;
            _Group = group;

        }

        protected override string Value
        {
            get { return _Domain.Value; }
            set { _Domain.Value = value; }
        }
        protected override string UserValue
        {
            get { return _Domain.Value; }
            set { _Domain.Value = value; }
        }
        protected override string[] Range
        {
            get
            {
                string[] result = null;
                if (_Domain.Constraint != null && _Domain.Constraint.Range != null)
                {
                    result = new string[_Domain.Constraint.Range.Length];
                    for (int i = 0; i < result.Length; i++)
                        result[i] = _Domain.Constraint.Range[i].ToString();

                }

                return result;
            }
        }
        protected override IProperty Clone()
        {
            IProperty result = new IPropertyStringImpl(_Caption, _Name, _Owner, 
                _Domain.Constraint, _AttributeTarget, _AttributeRequirement);
            result.Value = Value;
            result.Group = _Group;
            result.Tooltip = _Tooltip;

            return result;

        }
        protected override bool Equals(IProperty property)
        {
            bool result = false;
            if (property != null && _Domain.Constraint != null)
                switch (_Domain.Constraint.ConstraintType)
                {
                    case ConstraintTest.ValidPathFileName:
                    case ConstraintTest.ValidURL:
                    case ConstraintTest.ValidEmail:
                        result = Value.ToLower() == property.Value.ToLower();
                        break;

                    default:
                        result = Value == property.Value;
                        break;

                } //switch (_Domain.Constraint.ConstraintType)

            return result;

        }

        protected Domain<string> _Domain = new Domain<string>();

    }
}
