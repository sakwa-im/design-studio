using System;
using System.Xml;
using System.Collections.Generic;

namespace configuration
{
    public enum eConfigurationSource { 
        Undefined, User = 1, 
        CmdLine = 2, 
        UserAppData = 4, 
        AllUsersAppData = 8, 
        UserServerSide = 16,
        GroupServerSide = 32,
        ServerSide = 64, 
        AppConfig = 128, 
        NonPersistent = 256,
        AllAllowed = 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 + 256,
        Construction = 512}

    public enum eConfigurationItemValueType { plain, xml, list }

    public interface IConfigurationItem
    {
        string Name { get; set; }
        string Value { get; set; }
        eConfigurationSource Source { get; set; }
        eConfigurationSource SourceAllowed { get; set; }
        eConfigurationSource Target { get; set; }
        IConfigurationItem Parent { get; set; }

        bool SetValue(string newValue, eConfigurationSource source);
        bool SetValue(XmlNode newValue, eConfigurationSource source);
        bool SaveToTarget(eConfigurationSource target);
        bool SaveTo(XmlDocument doc, eConfigurationSource target);
        bool SaveTo(IConfigurationSource source);

        bool AddConfigurationItem(string path, IConfigurationItem item);
        bool AddConfigurationItem(IConfigurationItem item);
        bool RemoveConfigurationItem(IConfigurationItem item);
        bool RemoveConfigurationItem(string path);
        IConfigurationItem GetConfigurationItem(string path);
        List<IConfigurationItem> ConfigurationItems { get; }

        void Clear();
        bool IsDirty { get; }
        void Reset();

        string GetValue(string defaultValue);
        void SetValue(string newValue);

        int GetValue(int defaultValue);
        void SetValue(int newValue);

        bool GetValue(bool defaultValue);
        void SetValue(bool newValue);

        float GetValue(float defaultValue);
        void SetValue(float newValue);

        string ToString();
        string ToString(eConfigurationSource SourceOrTarget);

        XmlNode GetXmlNode(XmlDocument doc, XmlNode node = null);
        eConfigurationItemValueType ConfigurationItemValueType { get; set; }

        string StorageKey { set; get; }
        string Type { get; set; }

        IConfiguration Configuration { get; set; }

    } //public interface IConfigurationItem
}
