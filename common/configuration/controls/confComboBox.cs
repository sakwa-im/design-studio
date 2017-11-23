using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace configuration
{
    public class confComboBox : ComboBox, IConfigurationControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(confComboBox));

        public confComboBox()
            : base()
        {
            //this.TextUpdate += confComboBox_TextUpdate;
            this.KeyPress += confComboBox_KeyPress;
            this.SelectedIndexChanged += confComboBox_SelectedIndexChanged;
        }

        void confComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && Text != "" && !Items.Contains(Text))
            {
                Items.Insert(0, Text);
                if (_ClearText != "" && !Items.Contains(_ClearText))
                    Items.Add(_ClearText);

            }
        }

        void confComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Text != "" && Text == _ClearText)
            {
                Items.Clear();
                Text = "";

            }
            OnTextChanged(e);

        }

        public void setIConfiguration(IConfiguration configuration)
        {
            conf = configuration;
        }

        protected override void Dispose(bool disposing)
        {
            if (!DesignMode && _Persistence != ePersistence.None && conf != null)
            {
                IConfigurationItem list = conf.GetConfigurationItem(Name);
                if (list == null)
                {
                    DefineConfiguration();
                    list = conf.GetConfigurationItem(Name);

                }

                if (list != null)
                {
                    list.Clear();
                    foreach (string elem in Items)
                        list.AddConfigurationItem(new IConfigurationItemImpl("item" + Convert.ToString(list.ConfigurationItems.Count + 1), elem, _ConfigurationSource));

                }

                if ((this as IConfigurationControl).ConfigurationName() != "")
                    ConfigurationRepository.IConfiguration.SetConfigurationValue((this as IConfigurationControl).ConfigurationName(), SelectedIndex);

                conf.Save();

            }

 	        base.Dispose(disposing);

        }

        protected int WM_CREATE      = 0x0001;
        protected int WM_DESTROY     = 0x0002;
        protected int WM_ENABLE      = 0x000A;
        protected int WM_CLOSE       = 0x0010;
        protected int WM_SHOWWINDOW  = 0x0018;
        protected int WM_ACTIVATEAPP = 0x001C;
        protected int WM_CANCELMODE  = 0x001F;

        protected override void WndProc(ref Message m)
        {
            //log.Debug(string.Format("0x{0}", m.Msg.ToString("X4")));

            base.WndProc(ref m);
            if (m.Msg == WM_SHOWWINDOW)
            {
                if (!DesignMode && _Persistence != ePersistence.None && conf != null)
                {
                    IConfigurationItem item = conf.GetConfigurationItem(Name);
                    if (item == null)
                    {
                        DefineConfiguration();
                        item = conf.GetConfigurationItem(Name);

                    }

                    List<string> items = new List<string>();
                    foreach (IConfigurationItem val in item.ConfigurationItems)
                        items.Add(val.Value);

                    Items.AddRange(items.ToArray());

                    SelectedIndex = conf.GetConfigurationValue((this as IConfigurationControl).ConfigurationName(), -1);

                    OnTextChanged(new EventArgs());

                }            
            }
        }

        protected void DefineConfiguration()
        {
            if (conf != null)
            {
                IConfigurationItem item = new IConfigurationItemImpl(Name, "", _ConfigurationSource);
                conf.AddConfigurationItem(_Prefix, item);
                conf.AddConfigurationItem(_Prefix, new IConfigurationItemImpl(
                    (this as IConfigurationControl).ConfigurationName(), "", _ConfigurationSource));

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
            get { return _Name; }
            set { _Name = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines an optional prefix")]
        public string Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines a mandatory persistance name")]
        public string ClearText
        {
            get { return _ClearText; }
            set { _ClearText = value; }
        }

        [Category("Persistence"), 
        RefreshProperties(RefreshProperties.All), 
        Description("Defines the persistence destination")]
        [TypeConverter(typeof(StringConverter))]
        public string ConfigurationSource
        {
            get { return _ConfigurationSource.ToString(); }
            set { _ConfigurationSource = (eConfigurationSource)Enum.Parse(typeof(eConfigurationSource), value); }
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
            get { return _Name; }
            set { _Name = value; }
        }
        string IConfigurationControl.ConfigurationName()
        {
            return _Prefix != "" ? string.Format("{0}.{1}", _Prefix, _Name) : _Name;
        }
        string IConfigurationControl.ConfigurationName(string name)
        {
            return _Prefix != "" ? string.Format("{0}.{1}", _Prefix, name) : name;
        }

        private ePersistence _Persistence = ePersistence.None;
        private string _Prefix = "";
        private string _Name = "";
        private string _ClearText = "Clear";
        private eConfigurationSource _ConfigurationSource = eConfigurationSource.UserAppData;
        private IConfiguration conf = null;
    }

}
