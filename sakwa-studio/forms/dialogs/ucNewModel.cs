using configuration;
using System;
using System.IO;
using System.Windows.Forms;

namespace sakwa
{
    public partial class ucNewModel : UserControl
    {
        private string selectedDomainModelFile = "";

        // Default cunstructor, needed for the VS Designer initialization 
        public ucNewModel()
        {
            InitializeComponent();
        }

        public string SelectedDomainModelFile
        {
            get { return selectedDomainModelFile; }
        }

        private void browseButton_Click(object sender, System.EventArgs e)
        {
            openFileDialog.InitialDirectory = SakwaSupport.InitialFolder(null, SakwaSupport.eInitialFolder.Template, selectedDomainModelFile);

            if (openFileDialog.ShowDialog(this) == DialogResult.OK )
            {
                selectedDomainModelFile = openFileDialog.FileName;
                SetTextBoxContent(selectedDomainModelFile);
                
            }
        }

        public static void DefineConfiguration()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.RecentDomainTemplates, "", UI_Constants.ConfigurationSource));
        }

        private void ucNewModel_Load(object sender, EventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            if (conf.GetConfigurationItem(UI_Constants.RecentDomainTemplates) == null)
                DefineConfiguration();

            RecentFilesListView.Items.Clear();
            IConfigurationItem recentDomainTemplates = conf.GetConfigurationItem(UI_Constants.RecentDomainTemplates);
        
            foreach (IConfigurationItem item in recentDomainTemplates.ConfigurationItems)
            {
                ListViewItem lvi = new ListViewItem(Path.GetFileName(item.Value), 0);
                lvi.Tag = item.Value;
                RecentFilesListView.Items.Add(lvi);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;

            IConfigurationItem recentDomainTemplates = conf.GetConfigurationItem(UI_Constants.RecentDomainTemplates);
            
            // truncate actual config file storage
            recentDomainTemplates.ConfigurationItems.Clear();

            // Fill the new config file storage
            if (selectedDomainModelFile != "")
            {
                string itemName = string.Format("item-{0}", recentDomainTemplates.ConfigurationItems.Count + 1);
                recentDomainTemplates.AddConfigurationItem(new IConfigurationItemImpl(itemName, selectedDomainModelFile, UI_Constants.ConfigurationSource));

                ListViewItem item = GetListViewItem(selectedDomainModelFile);
                if (item != null)
                    RecentFilesListView.Items.Remove(item);
            }

            int index = 0;
            while (recentDomainTemplates.ConfigurationItems.Count < 10 && RecentFilesListView.Items.Count > index)
            {
                string domainTemplate = RecentFilesListView.Items[index++].Tag.ToString();
                string itemName = string.Format("item-{0}", recentDomainTemplates.ConfigurationItems.Count + 1);
                recentDomainTemplates.AddConfigurationItem(new IConfigurationItemImpl(itemName, domainTemplate, UI_Constants.ConfigurationSource));

            }

            conf.Save();
        }

        private ListViewItem GetListViewItem(string name)
        {
            foreach (ListViewItem item in RecentFilesListView.Items)
                if (item.Tag.ToString() == name)
                    return item;

            return null;
        }
        private void RecentFilesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selectedDomainModelFile = e.Item.Tag.ToString();
            SetTextBoxContent(e.Item.Tag.ToString());
        }

        private void SetTextBoxContent(string filename)
        {
            SelectedDomainTemplateTextbox.Text = Path.GetFileName(filename);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            selectedDomainModelFile = "";
            SetTextBoxContent("");
        }
    }
}
