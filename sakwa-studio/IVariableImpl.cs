using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace sakwa
{
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// </history>
    public class IPersoIntAttributeImpl : IPersoAttributeImpl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPersoIntAttributeImpl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="constraint">The constraint.</param>
        /// <param name="persoAttributeType">Type of the perso attribute.</param>
        /// <history>
        /// 09-02-2011  Michel Roovers          Added
        /// </history>
        public IPersoIntAttributeImpl(string caption, string name, string owner, 
            Constraint<int> constraint, 
            PersoAttributeTarget persoAttributeType = PersoAttributeTarget.PersoCommand,
            PersoAttributeRequirement persoAttributeRequirement = PersoAttributeRequirement.User | PersoAttributeRequirement.Optional,
            int defaultValue = 0, string group = "")
            : base(caption, name, owner, persoAttributeType, persoAttributeRequirement)
        {
            _Domain.Constraint = constraint;

            if (constraint != null)
                switch (constraint.ConstraintType)
                {
                    case ConstraintTest.InRange:
                        _DomainType = PersoAttributeDomainType.Numerical | PersoAttributeDomainType.Enumeration;
                        break;

                    case ConstraintTest.MinMax:
                        _DomainType = PersoAttributeDomainType.Numerical | PersoAttributeDomainType.Ordinal;
                        break;

                    default:
                        _DomainType = PersoAttributeDomainType.Numerical;
                        break;

                } //switch(constraint.ConstraintType)
            else
                _DomainType = PersoAttributeDomainType.Numerical;

            Value = defaultValue.ToString();
            _Group = group;

        } //public IPersoIntAttributeImpl(

        protected override string Value
        {
            get { return _Domain.Value.ToString(); }
            set { if(value != "") _Domain.Value = Convert.ToInt32(value); }
        }
        protected override string UserValue
        {
            get { return _Domain.Value.ToString(); }
            set { if(value != "") _Domain.Value = Convert.ToInt32(value); }
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
        } //protected override string[] Range
        protected override IPersoAttribute Clone() 
        {
            IPersoAttribute result = new IPersoIntAttributeImpl(_Caption, _Name, _Owner, _Domain.Constraint, _PersoAttributeTarget, _PersoAttributeRequirement);
            result.Value = Value;
            result.Group = _Group;
            result.Tooltip = _Tooltip;

            return result;

        }
        protected override bool Equals(IPersoAttribute persoAttribute)
        {
            return persoAttribute != null ? Value != persoAttribute.Value : false;

        } //protected override bool Equals(IPersoAttribute persoAttribute)


        private Domain<int> _Domain = new Domain<int>();

    } //public class IPersoIntAttributeImpl

    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// </history>
    public class IPersoStringAttributeImpl : IPersoAttributeImpl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPersoStringAttributeImpl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="constraint">The constraint.</param>
        /// <param name="persoAttributeType">Type of the perso attribute.</param>
        /// <history>
        /// 09-02-2011  Michel Roovers          Added
        /// </history>
        public IPersoStringAttributeImpl(string caption, string name, string owner, 
            Constraint<string> constraint, 
            PersoAttributeTarget persoAttributeType = PersoAttributeTarget.PersoCommand,
            PersoAttributeRequirement persoAttributeRequirement = PersoAttributeRequirement.User | PersoAttributeRequirement.Optional,
            string defaultValue = "", string group = "")
            : base(caption, name, owner, persoAttributeType, persoAttributeRequirement)
        {
            _Domain.Constraint = constraint;

            if(constraint != null)
                switch(constraint.ConstraintType)
                {
                    case ConstraintTest.InRange:
                        _DomainType = PersoAttributeDomainType.AlfaNumerical | PersoAttributeDomainType.Enumeration;
                        break;

                    case ConstraintTest.MinMax:
                        _DomainType = PersoAttributeDomainType.AlfaNumerical | PersoAttributeDomainType.Ordinal;
                        break;

                    case ConstraintTest.ValidPath:
                    case ConstraintTest.ValidPathFileName:
                        _DomainType = PersoAttributeDomainType.AlfaNumerical | PersoAttributeDomainType.URI;
                        break;

                    default:
                        _DomainType = PersoAttributeDomainType.AlfaNumerical;
                        break;

                } //switch(constraint.ConstraintType)
            else
                _DomainType = PersoAttributeDomainType.AlfaNumerical;

            Value = defaultValue;
            _Group = group;

        } //public IPersoStringAttributeImpl(

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
        } //protected override string[] Range
        protected override IPersoAttribute Clone()
        {
            IPersoAttribute result = new IPersoStringAttributeImpl(_Caption, _Name, _Owner, _Domain.Constraint, _PersoAttributeTarget, _PersoAttributeRequirement);
            result.Value = Value;
            result.Group = _Group;
            result.Tooltip = _Tooltip;

            return result;

        }
        protected override bool Equals(IPersoAttribute persoAttribute)
        {
            bool result = false;
            if (persoAttribute != null && _Domain.Constraint != null)
                switch (_Domain.Constraint.ConstraintType)
                {
                    case ConstraintTest.ValidPathFileName:
                    case ConstraintTest.ValidURL:
                    case ConstraintTest.ValidEmail:
                        result = Value.ToLower() == persoAttribute.Value.ToLower();
                        break;

                    default:
                        result = Value == persoAttribute.Value;
                        break;

                } //switch (_Domain.Constraint.ConstraintType)

            return result;

        } //protected override bool Equals(IPersoAttribute persoAttribute)

        protected Domain<string> _Domain = new Domain<string>();

    } //public class IPersoStringAttributeImpl

    public class IPersoUriFileOrPathAttributeImpl : IPersoStringAttributeImpl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPersoStringAttributeImpl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="constraint">The constraint.</param>
        /// <param name="persoAttributeType">Type of the perso attribute.</param>
        /// <history>
        /// 09-02-2011  Michel Roovers          Added
        /// </history>
        public IPersoUriFileOrPathAttributeImpl(string caption, string name, string owner,
            Constraint<string> constraint,
            PersoAttributeTarget persoAttributeType = PersoAttributeTarget.PersoCommand,
            PersoAttributeRequirement persoAttributeRequirement = PersoAttributeRequirement.User | PersoAttributeRequirement.Optional,
            PersoAttributeDomainType persoAttributeDomainType = PersoAttributeDomainType.URI,
            string defaultValue = "", string group = "")
            : base(caption, name, owner, constraint, persoAttributeType, persoAttributeRequirement)
        {
            _DomainType = persoAttributeDomainType;

            Value = defaultValue;
            _Group = group;

        } //public IPersoStringAttributeImpl(
        #region IPersoDeviceAttribute implementation
        protected override string Value
        {
            get { return _Domain.Value; }
            set
            {
                _Domain.Value = value; 
            }
        }
        protected override string UserValue
        {
            get
            {
                if (_Domain.Value != null)
                {
                    string path = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar;
                    return (_Domain.Value.IndexOf(path) == 0) ? _Domain.Value.Substring(path.Length) : _Domain.Value;

                }

                return "";

            }
            set
            {
                _Domain.Value = value; 
            }
        }
        protected override IPersoAttribute Clone()
        {
            IPersoUriFileOrPathAttributeImpl result = new IPersoUriFileOrPathAttributeImpl(_Caption, _Name, _Owner, _Domain.Constraint, _PersoAttributeTarget, _PersoAttributeRequirement, _DomainType);
            result.Extension = _Extension;
            result.Filter = _Filter;

            result.Value = Value;
            result.Group = _Group;

            return result;

        }
        #endregion
        public string Extension { get { return _Extension; } set { _Extension = value; } }
        public string Filter { get { return _Filter; } set { _Filter = value; } }
        private string _Filter = "";
        private string _Extension = "";

    } //public class IPersoURIAttributeImpl

    public class IPersoBoolAttributeImpl : IPersoAttributeImpl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPersoIntAttributeImpl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="constraint">The constraint.</param>
        /// <param name="persoAttributeType">Type of the perso attribute.</param>
        /// <history>
        /// 09-02-2011  Michel Roovers          Added
        /// </history>
        public IPersoBoolAttributeImpl(string caption, string name, string owner,
            Constraint<bool> constraint,
            PersoAttributeTarget persoAttributeType = PersoAttributeTarget.PersoCommand,
            PersoAttributeRequirement persoAttributeRequirement = PersoAttributeRequirement.User | PersoAttributeRequirement.Optional,
            bool defaultValue = false, string group = "")
            : base(caption, name, owner, persoAttributeType, persoAttributeRequirement)
        {
            _Domain.Constraint = constraint;

            if (constraint != null)
                switch (constraint.ConstraintType)
                {
                    case ConstraintTest.InRange:
                        _DomainType = PersoAttributeDomainType.Numerical | PersoAttributeDomainType.Enumeration;
                        break;

                    case ConstraintTest.MinMax:
                        _DomainType = PersoAttributeDomainType.Numerical | PersoAttributeDomainType.Ordinal;
                        break;

                    default:
                        _DomainType = PersoAttributeDomainType.Numerical;
                        break;

                } //switch(constraint.ConstraintType)
            else
                _DomainType = PersoAttributeDomainType.Numerical;

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
        protected override IPersoAttribute Clone()
        {
            IPersoAttribute result = new IPersoBoolAttributeImpl(_Caption, _Name, _Owner, _Domain.Constraint, _PersoAttributeTarget, _PersoAttributeRequirement);
            result.Value = Value;
            result.Group = _Group;
            result.Tooltip = _Tooltip;

            return result;

        }
        protected override bool Equals(IPersoAttribute persoAttribute)
        {
            return persoAttribute != null ? Value != persoAttribute.Value : false;

        } //protected override bool Equals(IPersoAttribute persoAttribute)


        private Domain<bool> _Domain = new Domain<bool>();

    } //public class IPersoIntAttributeImpl

} //namespace sakwa
