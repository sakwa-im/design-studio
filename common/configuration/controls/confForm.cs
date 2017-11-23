using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace configuration
{
    public class confForm : Form, IConfigurationControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(confForm));

        public confForm()
            : base()
        {
            this.Load += confForm_Load;
            this.FormClosing += confForm_FormClosing;
        }

        void confForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DesignMode && _Persistence != ePersistence.None)
            {
                IConfiguration conf = ConfigurationRepository.IConfiguration;

                string name = (this as IConfigurationControl).ConfigurationName("size-" + _ConfigurationName);
                IConfigurationItem size = conf.GetConfigurationItem(name);
                (size as IConfigurationItemObject<Size>).SetValue(this.Size);

                name = (this as IConfigurationControl).ConfigurationName("location-" + _ConfigurationName);
                IConfigurationItem location = conf.GetConfigurationItem(name);
                (location as IConfigurationItemObject<Point>).SetValue(this.Location);

                if (_Persistence == ePersistence.Direct)
                    conf.Save();

            }
        }

        void confForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode && _Persistence != ePersistence.None)
            {
                IConfiguration conf = ConfigurationRepository.IConfiguration;

                string name = "size-" + _ConfigurationName;

                IConfigurationItemObject<Size> size = null;
                size = conf.GetConfigurationItem((this as IConfigurationControl).ConfigurationName(name)) as IConfigurationItemObject<Size>;

                if (size == null)
                {
                    size = new ConfigurationItemObject<Size>(name, new Size(390, 300), eConfigurationSource.AllUsersAppData);
                    conf.AddConfigurationItem(_Prefix, size as IConfigurationItem);
                }
                this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

                name = "location-" + _ConfigurationName;

                IConfigurationItemObject<Point> location = null;
                location = conf.GetConfigurationItem((this as IConfigurationControl).ConfigurationName(name)) as IConfigurationItemObject<Point>;

                if(location == null)
                {
                    location = new ConfigurationItemObject<Point>(name, new Point(100, 50), eConfigurationSource.AllUsersAppData);
                    conf.AddConfigurationItem(_Prefix, location as IConfigurationItem);
                }
                this.Location = LocationInView(location.GetValue(this.Location));

            }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines if the settings can be persisted")]
        public ePersistence Persistence
        {
            get { return _Persistence; }
            set { _Persistence = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines a mandatory persistance name")]
        public string ConfigurationName
        {
            get { return _ConfigurationName; }
            set { _ConfigurationName = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines an optional prefix")]
        public string Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }

        ePersistence IConfigurationControl.Persistence
        {
            get { return _Persistence; }
            set { _Persistence = value; }
        }
        string IConfigurationControl.Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }
        string IConfigurationControl.Name
        {
            get { return _ConfigurationName; }
            set { _ConfigurationName = value; }
        }
        string IConfigurationControl.ConfigurationName()
        {
            return _Prefix != "" ? string.Format("{0}.{1}", _Prefix, _ConfigurationName) : _ConfigurationName;
        }
        string IConfigurationControl.ConfigurationName(string name)
        {
            return _Prefix != "" ? string.Format("{0}.{1}", _Prefix, name) : name;
        }
        private ePersistence _Persistence = ePersistence.None;
        private string _Prefix = "";
        private string _ConfigurationName = "";

        protected Point LocationInView(Point point)
        {
            this.Location = point;
            Screen screen = Screen.FromControl(this);
            if (screen != null)
                log.Debug(string.Format("current: {0}, primary: {1}", screen.ToString(), Screen.PrimaryScreen.ToString()));
            else
                log.Debug(string.Format("The {0} is not visible", this.ToString()));

            if (!screen.Primary)
            {
            }
            return point;
        }

    }
}
