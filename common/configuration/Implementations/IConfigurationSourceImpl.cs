using System;
using System.Xml;
using log4net;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace configuration
{
    public class IConfigurationSourceImpl : IConfigurationSource
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(IConfigurationSourceImpl));

        protected Dictionary<string, string> SourceMapping = new Dictionary<string, string>()
        {
            { eConfigurationSource.AppConfig.ToString(), "program-folder"},
            { eConfigurationSource.UserAppData.ToString(), "UserAppDataPath"},
            { eConfigurationSource.AllUsersAppData.ToString(), "CommonAppDataPath"},
            { eConfigurationSource.User.ToString(), "UserLocal"}
        };

        public IConfigurationSourceImpl() { }
        public IConfigurationSourceImpl(eConfigurationSource source)
        {
            _Source = source;

            switch (source)
            {
                case eConfigurationSource.AppConfig:
                    _Name = "AppConfig";
                    _URI = Application.ExecutablePath + ".config";
                    _ConfigurationSourceType = eConfigurationSourceTypes.AppConfig;
                    _ReadOnly = true;
                    break;

                case eConfigurationSource.UserAppData:
                    _Name = "UserAppDataPath";
                    _URI = GetURIPath(Environment.SpecialFolder.ApplicationData) + "config.xml";

                    _ConfigurationSourceType = eConfigurationSourceTypes.Xml;
                    _ReadOnly = false;
                    break;

                case eConfigurationSource.AllUsersAppData:
                    _Name = "CommonAppDataPath";
                    _URI = GetURIPath(Environment.SpecialFolder.CommonApplicationData) + "config.xml";

                    _ConfigurationSourceType = eConfigurationSourceTypes.Xml;
                    _ReadOnly = false;
                    break;

                case eConfigurationSource.User:
                    _Name = "UserLocal";
                    _URI = GetURIPath(Environment.SpecialFolder.LocalApplicationData) + "config.xml";

                    _ConfigurationSourceType = eConfigurationSourceTypes.Xml;
                    _ReadOnly = false;
                    break;

            }

            string folder = _URI.Substring(0, _URI.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            ConfigurationRepository.IConfiguration.AddConfigurationItem(
                new IConfigurationItemImpl(
                    SourceMapping[_Source.ToString()],
                    folder, 
                    eConfigurationSource.NonPersistent)
                );

            if (!Load(null))
            {
                _Configuration = new XmlDocument();
                _Configuration.InnerXml = _OrgConfiguration;

            } //if (!Load())
        }
        string IConfigurationSource.GetURIPath(eConfigurationSource source)
        {
            string result = "";
            switch (source)
            {
                case eConfigurationSource.AppConfig:
                    result = Application.ExecutablePath;
                    result = result.Substring(0, result.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    break;

                case eConfigurationSource.UserAppData:
                    result = GetURIPath(Environment.SpecialFolder.LocalApplicationData);
                    break;

                case eConfigurationSource.AllUsersAppData:
                    result = GetURIPath(Environment.SpecialFolder.CommonApplicationData);
                    break;

                case eConfigurationSource.User:
                    result = GetURIPath(Environment.SpecialFolder.UserProfile);
                    break;

            }
            return result;
        }

        private string GetURIPath(Environment.SpecialFolder specialFolder)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            return Environment.GetFolderPath(specialFolder) + Path.DirectorySeparatorChar + 
                versionInfo.CompanyName + Path.DirectorySeparatorChar + 
                versionInfo.ProductName + Path.DirectorySeparatorChar;

        }
        public IConfigurationSourceImpl(string name, eConfigurationSource source, string uri, 
            eConfigurationSourceTypes sourceType = eConfigurationSourceTypes.Xml, bool readOnly = false)
        {
            _Name = name;
            _Source = source;
            _URI = uri;
            _ConfigurationSourceType = sourceType;
            _ReadOnly = readOnly;

            if (!Load(null))
            {
                _Configuration = new XmlDocument();
                _Configuration.InnerXml = _OrgConfiguration;

            } //if (!Load())
        } //public IConfigurationSourceImpl( ...

        #region DeveloperPage support
        public string Name { get { return _Name; } }
        public string URI { get { return _URI; } }
        public string Source { get { return _Source.ToString(); } }
        public string ConfigurationType{get{return _ConfigurationSourceType.ToString(); } }
        public bool ReadOnly{get{return _ReadOnly; } }
        #endregion

        #region IConfigurationSource implementation
        string IConfigurationSource.Name { get { return _Name; } set { _Name = value; } }
        eConfigurationSource IConfigurationSource.Source { get { return _Source; } }
        eConfigurationSourceTypes IConfigurationSource.ConfigurationSourceType { get { return _ConfigurationSourceType; } }
        string IConfigurationSource.URI { get { return _URI; } set { _URI = value; } }
        bool IConfigurationSource.ReadOnly { get { return _ReadOnly; } }

        bool IConfigurationSource.SetConfigurationItem(IConfigurationItem item)
        {
            string rootName = _ConfigurationSourceType == eConfigurationSourceTypes.AppConfig ? "appSettings" : "";
            XmlNode node = GetConfigNode(_Configuration, item.Name, rootName);
            if (node != null)
            {
                //log.Debug(item.Name + ".SetValue(" + node.InnerText + ", " + _Source.ToString() + ")");
                return item.SetValue(node, _Source);
            }

            return false;

        } //bool IConfigurationSource.SetConfigurationItem( ...
        bool IConfigurationSource.UpdateConfigurationItem(IConfigurationItem item)
        {
            return UpdateConfiguration(item);
        }

        bool IConfigurationSource.Load(IConfiguration Items)
        {
            return Load(Items);
        }
        bool IConfigurationSource.IsDirty()
        {
            return IsDirty;
        }
        bool IConfigurationSource.New()
        {
            return New();
        }

        bool IConfigurationSource.Save()
        {
            return Save();
        }
        bool IConfigurationSource.SaveAs(string newName)
        {
            return SaveAs(newName);
        }

        protected eConfigurationSource _Source = eConfigurationSource.Undefined;        
        protected eConfigurationSourceTypes _ConfigurationSourceType = eConfigurationSourceTypes.None;
        protected string _Name = "";
        protected string _URI = "";
        protected bool _ReadOnly = false;

        #endregion
        #region protected members
        protected XmlDocument _Configuration = null;
        protected string _OrgConfiguration = "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration></configuration>";
        protected const string defTag = "def";
        protected const string ifDefTag = "ifdef";
        protected const string condTag = "condition";

        protected const string addTag = "add";
        protected const string appTag = "app";

        /// <summary>
        /// GetConfigNode
        /// </summary>
        /// <param name="config"></param>
        /// <param name="name"></param>
        /// <param name="rootName"></param>
        /// <history>
        /// 02-14-2013  m.roovers       Added conditional define to config file for development purpose.
        /// </history>
        /// <returns>XmlNode</returns>
        protected XmlNode GetConfigNode(XmlDocument config, string name, string rootName = "")
        {
            if (config != null && name != null)
            {
                XmlNode root = rootName != "" ? config.DocumentElement[rootName] : config.DocumentElement;
                if (root != null)
                    if (rootName == "appSettings")
                    {
                        foreach (XmlNode node in root)
                            if (node.Name == addTag && node.Attributes["key"] != null && node.Attributes["key"].InnerText == name)
                                return node;

                    } //if (rootName == "appSettings")
                    else
                    {
                        foreach (XmlNode node in root)
                            if (node.Name == defTag)
                                foreach(XmlNode n in root)
                                    if(n.Name == ifDefTag && GetAttributeValue(n, condTag) == node.InnerText)
                                    {
                                        root = n;
                                        goto Found;

                                    } //if(n.Name == ifDefTag && GetAttributeValue(n, condTag) == node.InnerText)

                        Found:
                        foreach (XmlNode node in root)
                            if (node.Name == name)
                                if (node.Attributes[appTag] == null)
                                    return node;
                                else
                                {
                                    string appName = Process.GetCurrentProcess().ProcessName;
                                    if (appName.IndexOf('.') >= 0)
                                        appName = appName.Substring(0, appName.IndexOf('.'));

                                    if (node.Attributes[appTag].InnerText == appName)
                                        return node;

                                } //else, if (node.Attributes["app"] == null)
                    } //else, if (rootName == "appSettings")
            } //if (config != null && name != null)

            return null;

        } //private XmlNode GetConfigNode( ...
        protected bool UpdateConfiguration(IConfigurationItem item)
        {
            return _Configuration != null && item != null ? item.SaveTo(_Configuration, _Source) : false;

        }
        #endregion

        protected virtual bool Load(IConfiguration Items)
        {
            //log.Debug("Load configuration: " + _URI);

            if (_URI != "" && File.Exists(_URI))
            {
                _Configuration = new XmlDocument();
                try
                {
                    _Configuration.Load(_URI);
                    if (Items != null)
                    {
                        foreach (XmlNode node in _Configuration.DocumentElement.ChildNodes)
                        {
                            IConfigurationItem newItem = IConfigurationItemImpl.FromXml(node, _Source);
                            newItem.Name = string.Format("item-{0}", Items.Items.Count + 1);
                            Items.Items.Add(newItem);
                        }
                    }
                    return true;

                }
                catch (Exception exc)
                {
                    log.Debug(exc.ToString());
                }
            } //if (_URI != "" && File.Exists(_URI))

            return false;

        } //protected virtual bool Load()
        protected virtual bool IsDirty
        {
            get
            {
                return !_ReadOnly && _Configuration != null &&
                    _Configuration.InnerXml != _OrgConfiguration;

            }
        } //protected virtual bool IsDirty
        protected virtual bool New()
        {
            _Configuration = new XmlDocument();
            _Configuration.InnerXml = _OrgConfiguration;
            
            return true;
        }
        protected virtual bool Save()
        {
            return SaveAs(_URI);

        } //protected virtual bool Save()
        protected virtual bool SaveAs(string newName)
        {
            if (IsDirty && newName != "")
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(newName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(newName));

                    _Configuration.Save(newName);
                    _OrgConfiguration = _Configuration.InnerXml;
                    _URI = newName;

                    return true;

                }
                catch (Exception exc)
                {
                    //log.Debug(exc.ToString());
                }

            return false;

        } //protected virtual bool SaveAs(string newName)
        protected string GetAttributeValue(XmlNode node, string name, string defaultValue = "")
        {
            return node.Attributes[name] != null ? node.Attributes[name].InnerText : defaultValue;
        }

    } //public class IConfigurationSourceImpl
}
