using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kms;
using System.Xml;
using log4net;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;

namespace configuration
{
    public class ConfigurationStorage : IConfiguration
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigurationStorage));

        public ConfigurationStorage()
        {
            _RootConfigurationItem.Configuration = this;
            InitializeKMS();

        }

        #region ConfigurationSource support
        IConfigurationSource IConfiguration.AddConfigurationSource(IConfigurationSource source, bool addPath)
        {
            _ConfigurationSources.Add(source);
            if(addPath)
                Interface.AddConfigurationItem(new IConfigurationItemImpl(source.Name, Path.GetDirectoryName(source.URI), eConfigurationSource.NonPersistent));

            if (_AutoName)
            {
                source.Load(this);
                source.New();

            }
            else
                Interface.Refresh();

            return source;

        } //void IConfiguration.AddConfigurationSource( ...
        IConfigurationSource IConfiguration.GetConfigurationSource(eConfigurationSource source)
        {
            foreach (IConfigurationSource src in _ConfigurationSources)
                if (src.Source == source)
                    return src;

            return null;

        }

        List<IConfigurationSource> IConfiguration.ConfigurationSources { get { return _ConfigurationSources; } }

        private List<IConfigurationSource> _ConfigurationSources = new List<IConfigurationSource>();
        #endregion

        #region IConfigurationItem support
        bool IConfiguration.AutoName { get { return _AutoName; } set { _AutoName = value; } }

        List<IConfigurationItem> IConfiguration.Items { get { return _RootConfigurationItem.ConfigurationItems; } }

        IConfigurationItem IConfiguration.AddConfigurationItem(IConfigurationItem item)
        {
            if (_AutoName)
                item.Name = string.Format("item-{0}", _RootConfigurationItem.ConfigurationItems.Count + 1);

            IConfigurationItem result = _RootConfigurationItem.GetConfigurationItem(item.Name);
            if (result == null && _RootConfigurationItem.AddConfigurationItem(item) && !_AutoName)
            {
                Interface.Refresh(item);

                return item;

            } //if(_RootConfigurationItem.AddConfigurationItem(item))

            return result;

        } //bool IConfiguration.AddConfigurationItem( ...
        IConfigurationItem IConfiguration.AddConfigurationItem(string path, IConfigurationItem item)
        {
            IConfigurationItem result = _RootConfigurationItem.GetConfigurationItem(
                path != ""
                ? string.Format("{0}.{1}", path, item.Name)
                : item.Name);

            if (result == null && _RootConfigurationItem.AddConfigurationItem(path, item))
            {
                Interface.Refresh(item);

                return item;

            } //if(_RootConfigurationItem.AddConfigurationItem(path, item))

            return result;

        } //bool IConfiguration.AddConfigurationItem( ...
        bool IConfiguration.RemoveConfigurationItem(string path)
        {
            return _RootConfigurationItem.RemoveConfigurationItem(path);
        }
        bool IConfiguration.RemoveConfigurationItem(IConfigurationItem item)
        {
            return _RootConfigurationItem.RemoveConfigurationItem(item);
        }
        IConfigurationItem IConfiguration.GetConfigurationItem(string path)
        {
            return _RootConfigurationItem.GetConfigurationItem(path);
        }
        protected IConfigurationItem _RootConfigurationItem = new IConfigurationItemImpl("root");
        #endregion

        #region Value setters/getters
        string IConfiguration.GetConfigurationValue(string path)
        {
            IConfigurationItem item = _RootConfigurationItem.GetConfigurationItem(path);
            return (item != null) ? item.Value : "";

        }
        string IConfiguration.GetConfigurationValue(string path, string defaultValue)
        {
            IConfigurationItem item = _RootConfigurationItem.GetConfigurationItem(path);
            return (item != null) ? item.GetValue(defaultValue) : defaultValue;

        }
        void IConfiguration.SetConfigurationValue(string path, string newValue)
        {
            IConfigurationItem item = _RootConfigurationItem.GetConfigurationItem(path);
            if (item != null)
                item.SetValue(newValue);

        }

        int IConfiguration.GetConfigurationValue(string path, int defaultValue)
        {
            IConfigurationItem item = _RootConfigurationItem.GetConfigurationItem(path);
            return (item != null) ? item.GetValue(defaultValue) : defaultValue;

        }
        void IConfiguration.SetConfigurationValue(string path, int newValue)
        {
            IConfigurationItem item = _RootConfigurationItem.GetConfigurationItem(path);
            if (item != null)
                item.SetValue(newValue);

        }

        float IConfiguration.GetConfigurationValue(string path, float defaultValue)
        {
            IConfigurationItem item = _RootConfigurationItem.GetConfigurationItem(path);
            return (item != null) ? item.GetValue(defaultValue) : defaultValue;

        }
        void IConfiguration.SetConfigurationValue(string path, float newValue)
        {
            IConfigurationItem item = _RootConfigurationItem.GetConfigurationItem(path);
            if (item != null)
                item.SetValue(newValue);

        }

        bool IConfiguration.GetConfigurationValue(string path, bool defaultValue)
        {
            IConfigurationItem item = _RootConfigurationItem.GetConfigurationItem(path);
            return (item != null) ? item.GetValue(defaultValue) : defaultValue;

        }
        void IConfiguration.SetConfigurationValue(string path, bool newValue)
        {
            IConfigurationItem item = _RootConfigurationItem.GetConfigurationItem(path);
            if (item != null)
                item.SetValue(newValue);

        }
        #endregion

        #region Generic support
        bool IConfiguration.Refresh(IConfigurationItem item)
        {
            if (item != null)
            {
                foreach (IConfigurationSource source in _ConfigurationSources)
                    source.SetConfigurationItem(item);

                #region Set ConfigurationItem Value from Command line
                foreach (ICommandlineItem cli in _CommandlineItems)
                    if (cli.Name == item.Name && cli.Found)
                    {
                        item.SetValue(cli.Value, eConfigurationSource.CmdLine);
                        break;

                    } //if (cli.Name == item.Name)
                #endregion

            } //if (item != null)
            else
                foreach (IConfigurationItem ci in _RootConfigurationItem.ConfigurationItems)
                    Interface.Refresh(ci);

            return false;

        } //bool IConfiguration.Refresh(IConfigurationItem item)

        bool IConfiguration.AddCommandlineItem(ICommandlineItem commandLineItem)
        {
            bool result = true;
            foreach (ICommandlineItem cli in _CommandlineItems)
                if (cli.Name == commandLineItem.Name)
                {
                    result = false;
                    break;

                } //if (cli.Name == commandLineItem.Name)

            if (result)
            {
                _CommandlineItems.Add(commandLineItem);

                foreach (string cmdArg in _cmdLine)
                    if (commandLineItem.HasSwitch(cmdArg))
                    {
                        //log.Debug("CONFIGURATION::Command Line {" + commandLineItem.Name + ", " + commandLineItem.Value + "}");
                        break;

                    } //if (it.HasSwitch(cmdArg))

                Interface.Refresh();

            } //if (result)

            return result;

        } //bool IConfiguration.AddCommandlineItem(ICommandlineItem commandLineItem)
        List<ICommandlineItem> IConfiguration.CommandLineItems { get { return _CommandlineItems; } }

        bool IConfiguration.Save()
        {
            bool result = false;

            foreach (IConfigurationItem item in _RootConfigurationItem.ConfigurationItems)
                foreach (IConfigurationSource source in _ConfigurationSources)
                    item.SaveTo(source);

            try
            {
                foreach (IConfigurationSource source in _ConfigurationSources)
                    source.Save();

                _RootConfigurationItem.Reset();

                result = true;

            }
            catch (Exception exc)
            {
                //log.Debug("ConfigurationRepository.Save" + exc.ToString());
            }

            return result;

        } //bool IConfiguration.Save()
        bool IConfiguration.SaveTo(XmlDocument doc, eConfigurationSource target)
        {
            if (doc.InnerXml == "")
                doc.InnerXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration></configuration>";

            foreach (IConfigurationItem item in _RootConfigurationItem.ConfigurationItems)
                foreach (IConfigurationSource source in _ConfigurationSources)
                    item.SaveTo(doc, target);

            return true;

        } //bool IConfiguration.Save()
        bool IConfiguration.IsDirty
        {
            get
            {
                foreach (IConfigurationItem item in _RootConfigurationItem.ConfigurationItems)
                    if (item.IsDirty)
                        return true;

                return false;

            }
        } //bool IConfiguration.IsDirty

        string IConfiguration.ToString()
        {
            string result = "";

            if (_RootConfigurationItem != null)
                result += Environment.NewLine + _RootConfigurationItem.ToString();

            return result;

        }
        #endregion

        #region Key management
        IKms IConfiguration.IKms { get { return Kms; } }
        IKey IConfiguration.GetKeyById(string id)
        {
            return Kms != null
                ? Kms.GetKeyById(id)
                : null;
        }
        protected void InitializeKMS()
        {
            log.Debug("Creating system key");
            NetworkInterface nic = null;
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                if(adapter.Name == "LAN-verbinding")
                {
                    nic = adapter;
                    break;

                }

            if(nic != null)
            {
                IKey key = new IKeyImpl("system");
                if(key.KeyValueFromGuid(nic.Id))
                    Kms.AddKey(key);

                log.Debug("System key created");
                return;

            }

            log.Debug("System key not created");

        }

        #endregion
        IConfiguration Interface { get { return this; } }

        protected bool _AutoName = false;
        protected IKms Kms = IKmsImpl.IKms;

        #region Command line parameters
        protected List<ICommandlineItem> _CommandlineItems = new List<ICommandlineItem>();
        protected List<string> _cmdLine = new List<string>();
        #endregion
    }
}
