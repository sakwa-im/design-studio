using System;
using System.Collections.Generic;
using log4net;
using System.Xml;
using kms;

namespace configuration
{
    public sealed class ConfigurationRepository : ConfigurationStorage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigurationRepository));

        public static IConfiguration IConfiguration { get { return _this; } }
        private static IConfiguration _this = new ConfigurationRepository();


        internal ConfigurationRepository()
        {
            #region Sort from long to short
            foreach (string arg in Environment.GetCommandLineArgs())
                if (_cmdLine.Count == 0)
                    _cmdLine.Add(arg);
                else
                {
                    int index = 0;
                    bool doAdd = true;
                    foreach (string s in _cmdLine)
                    {
                        if (arg.Length > s.Length)
                        {
                            _cmdLine.Insert(index, arg);

                            doAdd = false;
                            index = 0;
                            break;

                        } //if (arg.Length > s.Length)
                        else
                            index++;

                    } //foreach(string s in cmdLine)

                    if (doAdd)
                        _cmdLine.Add(arg);

                } //foreach (string arg in Environment.GetCommandLineArgs())
            #endregion

            _RootConfigurationItem.Configuration = this;
            InitializeKMS();

        } //public sealed class ConfigurationRepository

    } //public sealed class ConfigurationRepository
}
