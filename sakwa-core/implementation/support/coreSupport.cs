using configuration;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class coreSupport
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(coreSupport));
        public static string PropertiesAsJson(List<IProperty> list, bool safeString = false)
        {
            string json = "";

            if (list.Count > 0)
            {
                Dictionary<string, string> props = new Dictionary<string, string>();
                foreach (IProperty prop in list)
                {
                    string propValue = (prop.AttributeRequirement & eAttributeRequirement.Password) == eAttributeRequirement.Password && safeString
                        ? prop.Value != "" ? "****" : ""
                        : prop.Value;

                    props.Add(prop.Name, propValue);

                }

                try
                {
                    json = JsonConvert.SerializeObject(props);
                }
                catch (Exception ex)
                {
                    log.Debug(ex.ToString());
                }
            }

            return json;

        }

        public static List<IProperty> JsonAsProperties(string json)
        {
            List<IProperty> result = new List<sakwa.IProperty>();
            if (json != "")
            {
                try
                {
                    Dictionary<string, string> props = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    foreach (string name in props.Keys)
                    {
                        IProperty prop = getProperty(name, result);
                        if (prop != null)
                            prop.Value = props[name];

                    }
                }
                catch (Exception ex)
                {
                    log.Debug(ex.ToString());
                }
            }

            return result;

        }

        public static IProperty getProperty(string name, List<IProperty> list)
        {
            foreach (IProperty prop in list)
                if (prop.Name == name)
                    return prop;

            return null;

        }

        public static bool RenameNodeConfig(string oldName, string newName)
        {
            if (File.Exists(oldName))
                File.Move(oldName, newName);

            return true;

        }

        public static string NameNodeConfig(IBaseNode node, string newNodeName = "")
        {
            string UserAppFolder = ConfigurationRepository.IConfiguration.GetConfigurationValue("UserAppDataPath", "");

            if (newNodeName == "")
                newNodeName = node.Name;

            return string.Format("{0}{1}-{2}-config.xml", UserAppFolder,
                node.Parent.Name.ToLower(),
                newNodeName.ToLower());

        }
        public static string NameNodeConfig(string parentName, IBaseNode node)
        {
            string UserAppFolder = ConfigurationRepository.IConfiguration.GetConfigurationValue("UserAppDataPath", "");

            return string.Format("{0}{1}-{2}-config.xml", UserAppFolder,
                parentName.ToLower(),
                node.Name.ToLower());

        }

    }
}
