using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace configuration
{
    public class ConfigFile : ICommandlineItemImpl
    {
        public ConfigFile() : base("configFile", "conf=", null)
        {
        }

        protected override bool HasSwitch(string arg)
        {
            bool result = base.HasSwitch(arg);

            if (result && File.Exists(_Value))
            {
                ConfigurationRepository.IConfiguration.AddConfigurationSource(
                    new IConfigurationSourceImpl("configFile", eConfigurationSource.CmdLine, _Value, 
                        eConfigurationSourceTypes.Xml, true));

            }

            return result;

        } //protected override bool HasSwitch(string arg)
 
    }
}
