using configuration;
using kms;
using log4net;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace sakwa
{
    public partial class ucDataConnectionEditor : SakwaUserControl, IDataDefinitionEditor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ucDataConnectionEditor));
        protected const string SelectedTab = "selected-tab";
        public ucDataConnectionEditor()
        {
            InitializeComponent();
        }
        public ucDataConnectionEditor(IBaseNode node)
        {
            InitializeComponent();

            baseNode = node;
            lblTitle.Text = node.Name;

            string UserAppFolder = ConfigurationRepository.IConfiguration.GetConfigurationValue("UserAppDataPath", "");
            rootPath = string.Format("{0}{1}-{2}-config.xml", UserAppFolder,
                node.Parent.Name.ToLower(),
                node.Name.ToLower());

            conf.AddConfigurationSource(
                new IConfigurationSourceImpl("UserAppDataPath", Constants.ConfigurationSource, rootPath));

            string keyId = string.Format("{0}-{1}", node.Parent.Name.ToLower(), node.Name.ToLower());
            IKey key = new IKeyImpl(keyId);
            key.keyBytes = KeyUtils.GetBytes(node.Reference.Replace("-", ""));
            conf.IKms.AddKey(key);

            IConfigurationItem item = new IConfigurationItemImpl("secret", "", Constants.ConfigurationSource);
            item.StorageKey = keyId;
            conf.AddConfigurationItem(item);

            string plain = item.GetValue("");
            log.Debug(plain);

        }

        protected string rootPath = "";
        protected IConfiguration conf = new ConfigurationStorage();
        protected IBaseNode baseNode = null;

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            conf.SetConfigurationValue(SelectedTab, tabControl1.SelectedIndex);
            conf.Save();
        }

        protected override void DoActivate()
        {
            if (conf.GetConfigurationItem(SelectedTab) == null)
                DefineConfiguration();

            tabControl1.SelectedIndex = conf.GetConfigurationValue(SelectedTab, 0);

        }

        protected override void DoDeactivate()
        {
            conf.SetConfigurationValue(SelectedTab, tabControl1.SelectedIndex);
            conf.Save();
        }

        protected override void DefineConfiguration()
        {
            conf.AddConfigurationItem(new IConfigurationItemImpl(SelectedTab, "0", Constants.ConfigurationSource));

        }
    }
}
