using configuration;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace sakwa
{
    public partial class ConfigurationForm : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainForm));
        public ConfigurationForm(int selectedTab = 0, IApplication app = null)
        {
            InitializeComponent();
            App = app;

        }
        private IApplication App = null;

        private void btnOk_Click(object sender, EventArgs e)
        {
            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.ConfigurationFormSize);
            (size as IConfigurationItemObject<Size>).SetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.ConfigurationFormLocation);
            (location as IConfigurationItemObject<Point>).SetValue(this.Location);

            conf.SetConfigurationValue(UI_Constants.SakwaModelPath, tbxModelFolder.Text);
            conf.SetConfigurationValue(UI_Constants.SakwaTemplatePath, tbxTemplateFolder.Text);
            conf.SetConfigurationValue(UI_Constants.SakwaModelOnStart, chxOpenOnStart.Checked.ToString());

            conf.SetConfigurationValue(UI_Constants.FullHelpPath, tbxHelpFile.Text);

            modelGUIConfig.Save();

            IConfigurationItem stockMethods = conf.GetConfigurationItem(UI_Constants.StockMethods);
            if (stockMethods != null)
            {
                eConfigurationSource src = stockMethods.Source;
                stockMethods.Clear();
                stockMethods.Source = src;

                foreach (string mthd in lbMethods.Items)
                {
                    string name = string.Format("item-{0}", stockMethods.ConfigurationItems.Count + 1);
                    stockMethods.AddConfigurationItem(new IConfigurationItemImpl(name, mthd, UI_Constants.ConfigurationSource));

                }
            }

            string json = (tpgInferenceConfig.Controls[0] as ucPropertyEditor).PropertiesAsJson;
            conf.SetConfigurationValue(UI_Constants.GlobalConnectionProperties, json);

            conf.Save();

        }

        IConfiguration conf = ConfigurationRepository.IConfiguration;

        private void Configuration_Load(object sender, EventArgs e)
        {
            if (conf.GetConfigurationItem(UI_Constants.SakwaModelPath) == null)
                DefineConfigurationItems();

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.ConfigurationFormSize);
            this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.ConfigurationFormLocation);
            this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);

            //Make sure the form is shown on the visible screen
            if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                this.Location = new Point(100, 100);

            tbxModelFolder.Text = conf.GetConfigurationValue(UI_Constants.SakwaModelPath);
            tbxTemplateFolder.Text = conf.GetConfigurationValue(UI_Constants.SakwaTemplatePath, tbxModelFolder.Text);
            chxOpenOnStart.Checked = conf.GetConfigurationValue(UI_Constants.SakwaModelOnStart, false);

            tbxHelpFile.Text = conf.GetConfigurationValue(UI_Constants.FullHelpPath, "");

            modelGUIConfig = App != null ? App.ucDecisionTree.ModelGUIConfig : new ModelGUIConfig();
            InitializeModelGUI();

            IConfigurationItem stockMethods = conf.GetConfigurationItem(UI_Constants.StockMethods);
            if (stockMethods != null)
                foreach (IConfigurationItem it in stockMethods.ConfigurationItems)
                    lbMethods.Items.Add(it.Value);
            else
                lbMethods.Items.AddRange(new string[] { "Current", "Contains", "Excludes", "Next", "Add", "Remove"});

            string json = conf.GetConfigurationValue(UI_Constants.GlobalConnectionProperties, "");
            Dictionary<string, string> props = new Dictionary<string, string>();
            if (json != "")
            {
                try
                {
                    props = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
                catch(Exception ex)
                {
                    log.Debug(ex.ToString());
                }
            }

            ucPropertyEditor uc = new ucPropertyEditor(App.DataSourceManager, props);
            uc.Dock = DockStyle.Fill;
            tpgInferenceConfig.Controls.Add(uc);

        }

        ModelGUIConfig modelGUIConfig = null;
        private void InitializeModelGUI()
        {
            for (int i = 1; i <= 12; i++)
            {
                Control parent = i < 8 ? grpModelColors : grpTemplateColors;
                Control ctrl = GetControlByName(parent, "pnlText" + i.ToString());
                if (ctrl != null)
                    ctrl.BackColor = modelGUIConfig.ForeColors[i];

                ctrl = GetControlByName(parent, "pnlBack" + i.ToString());
                if (ctrl != null)
                    ctrl.BackColor = modelGUIConfig.BackColors[i];
            }

            ShowModelPreview();
            ShowTemplatePreview();

        }
        private void ShowModelPreview()
        { 
            tvModel.Nodes.Clear();
            IDecisionTree tree = App.NewDescisionTree();

            IBaseNode baseNode = tree.CreateNewNode(eNodeType.Root, null, "Decision model");
            TreeNode rootNode = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            modelGUIConfig.ConfigureTreeNode(rootNode);
            tvModel.Nodes.Add(modelGUIConfig.ConfigureTreeNode(rootNode));

            baseNode = tree.CreateNewNode(eNodeType.VariableDefinitions, null, "Variable definitions");
            TreeNode vars = modelGUIConfig.BaseNodeAsTreeNode(baseNode);

            rootNode.Nodes.Add(modelGUIConfig.ConfigureTreeNode(vars));

            baseNode = tree.CreateNewNode(eNodeType.VarDefinition, null, "Variable definition");
            TreeNode node = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            vars.Nodes.Add(modelGUIConfig.ConfigureTreeNode(node));

            baseNode = tree.CreateNewNode(eNodeType.DomainObject, null, "Domain object");
            TreeNode decision = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            vars.Nodes.Add(modelGUIConfig.ConfigureTreeNode(decision));


            baseNode = tree.CreateNewNode(eNodeType.Expression, null, "Expression");
            node = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            rootNode.Nodes.Add(modelGUIConfig.ConfigureTreeNode(node));

            baseNode = tree.CreateNewNode(eNodeType.Branch, null, "Branch");
            node = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            rootNode.Nodes.Add(modelGUIConfig.ConfigureTreeNode(node));

            tvModel.ExpandAll();

        }
        private void ShowTemplatePreview()
        {
            tvTemplate.Nodes.Clear();
            IDecisionTree tree = App.NewDescisionTree();

            IBaseNode baseNode = tree.CreateNewNode(eNodeType.Root, null, "Domain template");
            TreeNode rootNode = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            modelGUIConfig.ConfigureTreeNode(rootNode);
            tvTemplate.Nodes.Add(modelGUIConfig.ConfigureTreeNode(rootNode));

            baseNode = tree.CreateNewNode(eNodeType.DataObjects, null, "Data objects");
            TreeNode dataobjs = modelGUIConfig.BaseNodeAsTreeNode(baseNode);

            rootNode.Nodes.Add(modelGUIConfig.ConfigureTreeNode(dataobjs));

            baseNode = tree.CreateNewNode(eNodeType.DataObject, null, "Data object");
            TreeNode node = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            dataobjs.Nodes.Add(modelGUIConfig.ConfigureTreeNode(node));

            baseNode = tree.CreateNewNode(eNodeType.DataSources, null, "Data sources");
            TreeNode dataSources = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            rootNode.Nodes.Add(modelGUIConfig.ConfigureTreeNode(dataSources));

            baseNode = tree.CreateNewNode(eNodeType.DataSource, null, "Data source");
            TreeNode dataSource = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            dataSources.Nodes.Add(modelGUIConfig.ConfigureTreeNode(dataSource));

            baseNode = tree.CreateNewNode(eNodeType.DataDefinition, null, "Data definition");
            node = modelGUIConfig.BaseNodeAsTreeNode(baseNode);
            dataSource.Nodes.Add(modelGUIConfig.ConfigureTreeNode(node));

            tvTemplate.ExpandAll();

        }
        private Control GetControlByTag(Control parent, int tag)
        {
            foreach (Control ctrl in parent.Controls)
                if (ctrl.Tag != null && Convert.ToInt16(ctrl.Tag) == tag)
                    return ctrl;

            return null;

        }
        private Control GetControlByName(Control parent, string name)
        {
            foreach (Control ctrl in parent.Controls)
                if (ctrl.Name.StartsWith(name))
                    return ctrl;

            return null;

        }
        public static void DefineConfigurationItems()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            IConfigurationItemObject<Size> sizeMainForm = new ConfigurationItemObject<Size>(UI_Constants.ConfigurationFormSize, new Size(503, 363), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", sizeMainForm as IConfigurationItem);
            IConfigurationItemObject<Point> locationMainForm = new ConfigurationItemObject<Point>(UI_Constants.ConfigurationFormLocation, new Point(100, 50), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationMainForm as IConfigurationItem);

            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.SakwaModelPath, "", UI_Constants.ConfigurationSource));
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.SakwaTemplatePath, "", UI_Constants.ConfigurationSource));
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.SakwaModelOnStart, "", UI_Constants.ConfigurationSource)); 

            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.FullHelpPath, "", UI_Constants.ConfigurationSource));

            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.StockMethods, "", UI_Constants.ConfigurationSource));
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.GlobalConnectionProperties, "", UI_Constants.ConfigurationSource));
            
        }

        private void btnBrowseModelFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = tbxModelFolder.Text;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                tbxModelFolder.Text = folderBrowserDialog.SelectedPath;

        }
        private void btnBrowseTemplateFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = tbxTemplateFolder.Text;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                tbxTemplateFolder.Text = folderBrowserDialog.SelectedPath;

        }

        private void pnlColor_Click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            colorDialog.Color = ctrl.BackColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                ctrl.BackColor = colorDialog.Color;
                if (ctrl.Tag != null)
                {
                    int index = Convert.ToInt16(ctrl.Tag);
                    if (index >= 1 && index <= modelGUIConfig.ForeColors.Length)
                        modelGUIConfig.ForeColors[index] = ctrl.BackColor;

                }

                ShowModelPreview();
                ShowTemplatePreview();

            }
        }
        private void pnlBackColor_Click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            colorDialog.Color = ctrl.BackColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                ctrl.BackColor = colorDialog.Color;
                if (ctrl.Tag != null)
                {
                    int index = Convert.ToInt16(ctrl.Tag);
                    if (index >= 1 && index <= modelGUIConfig.BackColors.Length)
                        modelGUIConfig.BackColors[index] = ctrl.BackColor;

                }

                ShowModelPreview();
                ShowTemplatePreview();

            }
        }

        private void btnBrowseHelpFile_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = tbxHelpFile.Text;
            if(openFileDialog.ShowDialog(this) == DialogResult.OK)
                tbxHelpFile.Text = openFileDialog.FileName;

        }

        private void tbxMethod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnAdd_Click(sender, new EventArgs());

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!lbMethods.Items.Contains(tbxMethod.Text))
                lbMethods.Items.Add(tbxMethod.Text);

            tbxMethod.SelectAll();

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<string> toRemove = new List<string>();
            foreach (string mthd in lbMethods.SelectedItems)
                toRemove.Add(mthd);

            foreach (string mthd in toRemove)
                lbMethods.Items.Remove(mthd);

        }

        private void lbMethods_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnRemove_Click(sender, new EventArgs());

        }

    }
}
