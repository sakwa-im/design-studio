using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace configuration
{
    public enum ePersistence { None, Direct, Delayed }

    public interface IConfigurationControl
    {
        ePersistence Persistence { get; set; }
        string Prefix { get; set; }
        string Name { get; set; }

        string ConfigurationName();
        string ConfigurationName(string name);

    }

    public class IConfigurationControlImpl : IConfigurationControl
    {
        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines if the settings can be persisted")]
        public ePersistence Persistence
        {
            get { return _Persistence; }
            set { _Persistence = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines a mandatory persistance name")]
        public string ConfigurationName
        {
            get { return _ConfigurationName; }
            set { _ConfigurationName = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines an optional prefix")]
        public string Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }

        ePersistence IConfigurationControl.Persistence
        {
            get { return _Persistence; }
            set { _Persistence = value; }
        }
        string IConfigurationControl.Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }
        string IConfigurationControl.Name
        {
            get { return _ConfigurationName; }
            set { _ConfigurationName = value; }
        }
        string IConfigurationControl.ConfigurationName()
        {
            return _Prefix != "" ? string.Format("{0}.{1}", _Prefix, _ConfigurationName) : _ConfigurationName;
        }
        string IConfigurationControl.ConfigurationName(string name)
        {
            return _Prefix != "" ? string.Format("{0}.{1}", _Prefix, name) : name;
        }
        private ePersistence _Persistence = ePersistence.None;
        private string _Prefix = "";
        private string _ConfigurationName = "";

    }
}

