using kms;
using System;
using System.Collections.Generic;
using System.Xml;

namespace configuration
{
    public interface IConfiguration
    {
        #region ConfigurationSource support
        IConfigurationSource AddConfigurationSource(IConfigurationSource source, bool AddPath = false);
        IConfigurationSource GetConfigurationSource(eConfigurationSource source);
        List<IConfigurationSource> ConfigurationSources { get; }
        #endregion
        #region Configuration Item support
        bool AutoName { get; set; }
        List<IConfigurationItem> Items { get; }

        IConfigurationItem AddConfigurationItem(IConfigurationItem item);
        IConfigurationItem AddConfigurationItem(string path, IConfigurationItem item);
        bool RemoveConfigurationItem(string path);
        bool RemoveConfigurationItem(IConfigurationItem item);
        IConfigurationItem GetConfigurationItem(string path);
        #endregion
        #region Value setters/getters
        string GetConfigurationValue(string path);
        string GetConfigurationValue(string path, string defaultValue);
        void SetConfigurationValue(string path, string newValue);

        int GetConfigurationValue(string path, int defaultValue);
        void SetConfigurationValue(string path, int newValue);

        bool GetConfigurationValue(string path, bool defaultValue);
        void SetConfigurationValue(string path, bool newValue);

        float GetConfigurationValue(string path, float defaultValue);
        void SetConfigurationValue(string path, float newValue);
        #endregion
        #region Generic support
        bool Refresh(IConfigurationItem item = null);
        bool AddCommandlineItem(ICommandlineItem commandLineItem);
        List<ICommandlineItem> CommandLineItems { get; }
        bool Save();
        bool SaveTo(XmlDocument doc, eConfigurationSource target);
        bool IsDirty { get; }

        string ToString();
        #endregion
        #region Key management
        IKms IKms { get; }
        IKey GetKeyById(string id);
        #endregion
    } //public interface IConfiguration
}
