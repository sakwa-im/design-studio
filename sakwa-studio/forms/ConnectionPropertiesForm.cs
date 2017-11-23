using configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public partial class ConnectionPropertiesForm : Form
    {
        public ConnectionPropertiesForm(IBaseNode selectedNode)
        {
            InitializeComponent();

            ucPropertyEditor uc = new sakwa.ucPropertyEditor(selectedNode as IDataSource);
            uc.Dock = DockStyle.Fill;
            Controls.Add(uc);

        }

        private void ConnectionPropertiesForm_Load(object sender, EventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            if (conf.GetConfigurationItem(UI_Constants.ConnectionPropertiesFormSize) == null)
                DefineConfigurationItems();

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.ConnectionPropertiesFormSize);
            this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.ConnectionPropertiesFormLocation);
            this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);

            //Make sure the form is shown on the visible screen
            if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                this.Location = new Point(100, 100);

        }

        private void ConnectionPropertiesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.ConnectionPropertiesFormSize);
            (size as IConfigurationItemObject<Size>).SetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.ConnectionPropertiesFormLocation);
            (location as IConfigurationItemObject<Point>).SetValue(this.Location);

            conf.Save();

        }

        public static void DefineConfigurationItems()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            IConfigurationItemObject<Size> sizeForm =
                 new ConfigurationItemObject<Size>(UI_Constants.ConnectionPropertiesFormSize, new Size(478, 437), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", sizeForm as IConfigurationItem);

            IConfigurationItemObject<Point> locationForm =
                new ConfigurationItemObject<Point>(UI_Constants.ConnectionPropertiesFormLocation, new Point(100, 50), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationForm as IConfigurationItem);

        }

    }
}
