using System;

namespace configuration
{
    public enum eConfigurationSourceTypes { None, AppConfig, Xml, Proprietary }

    public interface IConfigurationSource
    {
        string Name { get; set; }
        eConfigurationSource Source { get; }
        eConfigurationSourceTypes ConfigurationSourceType { get; }
        string URI { get; set; }
        string GetURIPath(eConfigurationSource source);
        bool ReadOnly { get; }

        bool SetConfigurationItem(IConfigurationItem item);
        bool UpdateConfigurationItem(IConfigurationItem item);

        bool Load(IConfiguration Items = null);
        bool IsDirty();

        bool New();
        bool Save();
        bool SaveAs(string newName);

    } //public interface IConfigurationSource
}
