using configuration;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace sakwa
{
    static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region ConfigurationSources
            //Create Configuration Sources for app.config, and CommonAppDataPath and UserAppDataPath for the Solution
            string CommonAppFolder = Application.CommonAppDataPath.Substring(0, Application.CommonAppDataPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            string UserAppFolder = Application.UserAppDataPath.Substring(0, Application.UserAppDataPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);

            ConfigurationRepository.IConfiguration.AddConfigurationSource(
                new IConfigurationSourceImpl("AppConfig", eConfigurationSource.AppConfig,
                    Application.ExecutablePath + ".config", eConfigurationSourceTypes.AppConfig, true));


            ConfigurationRepository.IConfiguration.AddConfigurationSource(
                new IConfigurationSourceImpl("UserAppDataPath", eConfigurationSource.UserAppData,
                    UserAppFolder + "config.xml"));

            ConfigurationRepository.IConfiguration.AddConfigurationItem(new IConfigurationItemImpl("CommonAppDataPath", CommonAppFolder, eConfigurationSource.NonPersistent));
            ConfigurationRepository.IConfiguration.AddConfigurationItem(new IConfigurationItemImpl("UserAppDataPath", UserAppFolder, eConfigurationSource.NonPersistent));
            #endregion
            #region Logging
            string logFolder = Application.CommonAppDataPath;
            logFolder = logFolder.Substring(0, logFolder.LastIndexOf(Path.DirectorySeparatorChar) + 1);

            string LogFileName = Application.ProductName + ".log";
            string LogConfigFileName = LogFileName + "-log.xml";

            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

            if (!File.Exists(logFolder + LogConfigFileName))
            {
                XmlDocument logConfig = new XmlDocument();

                logConfig.InnerXml = LogFileDefinition(logFolder, LogFileName, "debug");
                logConfig.Save(logFolder + LogConfigFileName);

            } //if (!File.Exists(logFolder + Constants.LogConfigFileName))

            XmlConfigurator.ConfigureAndWatch(new FileInfo(logFolder + LogConfigFileName));

            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static string LogFileDefinition(string folder, string fileName, string level = "error")
        {
            string xml = "";
            xml += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;
            xml += "<log4net>" + Environment.NewLine;
            xml += "<root>" + Environment.NewLine;
            xml += "<level value=\"" + level + "\" />" + Environment.NewLine;
            xml += "<appender-ref ref=\"LogFile Each Run\" />" + Environment.NewLine;
            xml += "</root>" + Environment.NewLine;

            xml += "<appender name=\"One LogFile\" type=\"log4net.Appender.FileAppender\">" + Environment.NewLine;
            xml += "<param name=\"File\" value=\"" + folder + @"log\" + fileName + "\" />" + Environment.NewLine;
            xml += "<param name=\"AppendToFile\" value=\"true\" />" + Environment.NewLine;
            xml += "<layout type=\"log4net.Layout.PatternLayout\">" + Environment.NewLine;
            xml += "<param name=\"ConversionPattern\" value=\"%date{dd MMM yyyy HH:mm:ss,fff} [%thread] %-5level %logger{2} - %message%newline%exception\" />" + Environment.NewLine;
            xml += "</layout>" + Environment.NewLine;
            xml += "</appender>" + Environment.NewLine;

            xml += "<appender name=\"LogFile Each Run\" type=\"log4net.Appender.FileAppender\">" + Environment.NewLine;
            xml += "<param name=\"File\" value=\"" + folder + @"log\" + fileName + "\" />" + Environment.NewLine;
            xml += "<param name=\"AppendToFile\" value=\"false\" />" + Environment.NewLine;
            xml += "<layout type=\"log4net.Layout.PatternLayout\">" + Environment.NewLine;
            xml += "<param name=\"ConversionPattern\" value=\"%date{dd MMM yyyy HH:mm:ss,fff} [%thread] %-5level %logger{2} - %message%newline%exception\" />" + Environment.NewLine;
            xml += "</layout>" + Environment.NewLine;
            xml += "</appender>" + Environment.NewLine;

            xml += "<appender name=\"LogFile Each Day\" type=\"log4net.Appender.RollingFileAppender\">" + Environment.NewLine;
            xml += "<param name=\"File\" value=\"" + folder + @"log\" + fileName + "\" />" + Environment.NewLine;
            xml += "<param name=\"AppendToFile\" value=\"true\" />" + Environment.NewLine;
            xml += "<param name=\"rollingStyle\" value=\"Date\" />" + Environment.NewLine;
            xml += "<param name=\"datePatern\" value=\"yyyyMMdd\" />" + Environment.NewLine;
            xml += "<layout type=\"log4net.Layout.PatternLayout\">" + Environment.NewLine;
            xml += "<param name=\"ConversionPattern\" value=\"%date{dd MMM yyyy HH:mm:ss,fff} [%thread] %-5level %logger{2} - %message%newline%exception\" />" + Environment.NewLine;
            xml += "</layout>" + Environment.NewLine;
            xml += "</appender>" + Environment.NewLine;

            xml += "</log4net>" + Environment.NewLine;

            return xml;

        } //public static string LogFileDefinition(string folder, string fileName)

    }
}
