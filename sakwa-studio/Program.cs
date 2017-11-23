using configuration;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace sakwa
{
    static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        public static string ProgramFolder = "";
        public static string DataSourceFolder = "";
        public static DirectoryCatalog Catalog;
        public static CompositionContainer Container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Logging
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            string logFolder = string.Format(@"{0}\{1}\{2}\",
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                versionInfo.CompanyName,
                versionInfo.ProductName);

            string LogFileName = Application.ProductName + ".log";
            string LogConfigFileName = Application.ProductName + "-log.xml";

            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

            if (!logFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                logFolder += Path.DirectorySeparatorChar;

            if (!File.Exists(logFolder + LogConfigFileName))
            {
                XmlDocument logConfig = new XmlDocument();

                logConfig.InnerXml = LogFileDefinition(logFolder + "log" + Path.DirectorySeparatorChar,
                    LogFileName, "debug");
                logConfig.Save(logFolder + LogConfigFileName);

            } //if (!File.Exists(logFolder + Constants.LogConfigFileName))

            XmlConfigurator.ConfigureAndWatch(new FileInfo(logFolder + Path.DirectorySeparatorChar + LogConfigFileName));
            log.Debug("Logging configured");
            #endregion
            #region ConfigurationSources
            //Create Configuration Sources for app.config, and CommonAppDataPath and UserAppDataPath for the Solution
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            conf.AddConfigurationSource(new IConfigurationSourceImpl(eConfigurationSource.AppConfig));
            conf.AddConfigurationSource(new IConfigurationSourceImpl(eConfigurationSource.UserAppData));
            #endregion

            #region Managed Extensible Framework
            ProgramFolder = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar;
            DataSourceFolder = ProgramFolder + "data-sources" + Path.DirectorySeparatorChar;

            if (Directory.Exists(DataSourceFolder))
            {
                Catalog = new DirectoryCatalog(DataSourceFolder);
                var catalog = new AggregateCatalog(
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                    new DirectoryCatalog(ProgramFolder),
                    Catalog);

                Container = new CompositionContainer(catalog);
            }
            else
            {
                var catalog = new AggregateCatalog(
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                    new DirectoryCatalog(ProgramFolder));

                Container = new CompositionContainer(catalog);

            }
            #endregion

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(MainForm.ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MainForm.UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Bootstrap managed extensible framework
            MainForm mainForm = Container.GetExportedValue<MainForm>();
            Application.Run(mainForm);

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
