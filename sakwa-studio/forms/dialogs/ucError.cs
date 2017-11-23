using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using configuration;
using System.IO;

namespace sakwa
{
    public partial class ucError : UserControl
    {
        public ucError()
        {
            InitializeComponent();

            IConfiguration conf = ConfigurationRepository.IConfiguration;
            string logFile = conf.GetConfigurationValue("LogFile");
                if(logFile != "" && File.Exists(logFile))
                    log = File.ReadAllText(logFile);

            btnMailLog.Enabled = log != "";

        }

        string log = "";

        private void btnMailLog_Click(object sender, EventArgs e)
        {
            string command = "mailto:michel.roovers@telfortglasvezel.nl?subject=Sakwa log&body=" + log;
            Process.Start(command);

            btnMailLog.Enabled = false;

        }

    }
}
