using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public class IPropertyUriFileOrPathImpl : IPropertyStringImpl
    { 
        public IPropertyUriFileOrPathImpl(string caption, string name, string owner,
            Constraint<string> constraint,
            eAttributeTarget attributeType = eAttributeTarget.DataConnection,
            eAttributeRequirement attributeRequirement = eAttributeRequirement.User | eAttributeRequirement.Optional,
            eAttributeDomainType attributeDomainType = eAttributeDomainType.URI,
            string defaultValue = "", string group = "")
            : base(caption, name, owner, constraint, attributeType, attributeRequirement)
        {
            _DomainType = attributeDomainType;

            Value = defaultValue;
            _Group = group;

        }
        #region IProperty implementation
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
        protected override IProperty Clone()
        {
            IPropertyUriFileOrPathImpl result = new IPropertyUriFileOrPathImpl(_Caption, _Name, _Owner, 
                _Domain.Constraint, _AttributeTarget, _AttributeRequirement, _DomainType);
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

    }

}
