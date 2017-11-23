using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class IPropertyBoolImpl : IPropertyImpl
    {
        public IPropertyBoolImpl(string caption, string name, string owner,
            Constraint<bool> constraint,
            eAttributeTarget attributeType = eAttributeTarget.DataConnection,
            eAttributeRequirement attributeRequirement = eAttributeRequirement.User | eAttributeRequirement.Optional,
            bool defaultValue = false, string group = "")
            : base(caption, name, owner, attributeType, attributeRequirement)
        {
            _Domain.Constraint = constraint;

            if (constraint != null)
                switch (constraint.ConstraintType)
                {
                    case ConstraintTest.InRange:
                        _DomainType = eAttributeDomainType.Numerical | eAttributeDomainType.Enumeration;
                        break;

                    case ConstraintTest.MinMax:
                        _DomainType = eAttributeDomainType.Numerical | eAttributeDomainType.Ordinal;
                        break;

                    default:
                        _DomainType = eAttributeDomainType.Numerical;
                        break;

                } //switch(constraint.ConstraintType)
            else
                _DomainType = eAttributeDomainType.Numerical;

            Value = defaultValue.ToString();
            _Group = group;

        } //public IPersoIntAttributeImpl(

        protected override string Value
        {
            get { return _Domain.Value.ToString(); }
            set { if (value != "") _Domain.Value = Convert.ToBoolean(value); }
        }
        protected override string UserValue
        {
            get { return _Domain.Value.ToString(); }
            set { if (value != "") _Domain.Value = Convert.ToBoolean(value); }
        }
        protected override string[] Range
        {
            get
            {
                string[] result = null;
                if (_Domain.Constraint != null)
                {
                    result = new string[_Domain.Constraint.Range.Length];
                    for (int i = 0; i < result.Length; i++)
                        result[i] = _Domain.Constraint.Range[i].ToString();

                }

                return result;
            }
        } //protected override string[] Range
        protected override IProperty Clone()
        {
            IProperty result = new IPropertyBoolImpl(_Caption, _Name, _Owner, 
                _Domain.Constraint, _AttributeTarget, _AttributeRequirement);
            result.Value = Value;
            result.Group = _Group;
            result.Tooltip = _Tooltip;

            return result;

        }
        protected override bool Equals(IProperty property)
        {
            return property != null ? Value != property.Value : false;

        }

        private Domain<bool> _Domain = new Domain<bool>();

    }
}
