using configuration;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace sakwa
{
    public partial class TemplateForm : Form, IApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TemplateForm));
        public TemplateForm()
        {
            InitializeComponent();
        }

        public TemplateForm(IApplication app)
        {
            InitializeComponent();
            _IApplication = app;

            LastUsedFolder = _IApplication.LastUsedFolder;

        }

        IConfiguration conf = ConfigurationRepository.IConfiguration;

        private void InitializeControl()
        {
            pnlMain.Controls.Clear();

            ctrl = new ucDecisionTree(this as IApplication, ucDecisionTree.eDecisionTreeMode.DomainTemplate);
            ctrl.Dock = DockStyle.Left;

            split = new Splitter();
            split.Dock = DockStyle.Left;
            split.Width = 5;
            split.BackColor = Color.DarkBlue;

            props = new ucProperties(this as IApplication);
            props.Dock = DockStyle.Right;

            split2 = new Splitter();
            split2.Dock = DockStyle.Right;
            split2.Width = 5;
            split2.BackColor = Color.DarkBlue;

            editor = new Panel();
            editor.Dock = DockStyle.Fill;
            editor.BackColor = Color.AliceBlue;

            pnlMain.Controls.AddRange(new Control[] { editor, split2, props, split, ctrl });

            if (conf.GetConfigurationValue(UI_Constants.SakwaModelOnStart, false))
            {
                IConfigurationItem recentProjects = conf.GetConfigurationItem(UI_Constants.TemplateRecentProjects);
                if (recentProjects != null)
                    foreach (IConfigurationItem it in recentProjects.ConfigurationItems)
                        LoadFile(it.Value);
            }
            ctrl.SelectedPath = conf.GetConfigurationValue(UI_Constants.TemplateRecentNode, "");

        }

        IApplication _IApplication = null;
        ucDecisionTree ctrl = null;
        Panel editor = null;
        private ucProperties props = null;
        Splitter split = null;
        Splitter split2 = null;

        private void TemplateForm_Load(object sender, EventArgs e)
        {
            #region Setup Configuration
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.TemplateFormState, "", UI_Constants.ConfigurationSource));

            IConfigurationItemObject<Size> sizeForm = new ConfigurationItemObject<Size>(UI_Constants.TemplateFormSize, new Size(686, 432), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", sizeForm as IConfigurationItem);
            IConfigurationItemObject<Point> locationForm = new ConfigurationItemObject<Point>(UI_Constants.TemplateFormLocation, new Point(100, 50), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationForm as IConfigurationItem);

            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.TemplateRecentFiles, "", UI_Constants.ConfigurationSource));
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.TemplateRecentProjects, "", UI_Constants.ConfigurationSource));
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.TemplateRecentNode, "", UI_Constants.ConfigurationSource));

            if (conf.GetConfigurationItem(UI_Constants.SakwaModelOnStart) == null)
                ConfigurationForm.DefineConfigurationItems();

            #endregion
            #region App configuration
            switch (conf.GetConfigurationValue(UI_Constants.TemplateFormState))
            {
                case "Maximized":
                    WindowState = FormWindowState.Maximized;
                    break;

                case "Normal":
                    IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.TemplateFormSize);
                    this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

                    IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.TemplateFormLocation);
                    this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);
                    if (this.Location.X < 0)
                        this.Location = new Point(0, 0);

                    //Make sure the form is shown on the visible screen
                    if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                        this.Location = new Point(100, 100);

                    break;

                default:
                    size = conf.GetConfigurationItem(UI_Constants.TemplateFormSize);
                    this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

                    location = conf.GetConfigurationItem(UI_Constants.TemplateFormLocation);
                    this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);

                    //Make sure the form is shown on the visible screen
                    if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                        this.Location = new Point(100, 100);

                    break;

            } //switch(conf.GetConfigurationValue("window-state"))
            #endregion

            InitializeControl();

        }
        private void TemplateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool CanExit = true;
            DialogResult UserSelected = DialogResult.None;

            SakwaUserControl oldEditor = editor.Controls.Count > 0 ? editor.Controls[0] as SakwaUserControl : null;
            if (oldEditor != null)
                oldEditor.OnDeactivate();

            foreach (IDecisionTree tree in ctrl.Trees)
            {
                if (tree.IsDirty)
                {
                    UserSelected = IApplicationInterface.GetFloatingForm(
                        eFloatReason.NotSet, 
                        new ucUnsavedExit(true)).ShowDialog();

                    switch (UserSelected)
                    {
                        case DialogResult.OK:
                            CanExit = true;
                            break;

                        case DialogResult.Yes:
                            btnSave_Click(this, new EventArgs());
                            CanExit = true;
                            break;

                        default:
                            CanExit = false;
                            break;

                    }

                    break;

                }
            }

            if (CanExit)
            {
                conf.SetConfigurationValue(UI_Constants.TemplateFormState, this.WindowState.ToString());
                IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.TemplateFormSize);
                (size as IConfigurationItemObject<Size>).SetValue(this.Size);

                IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.TemplateFormLocation);
                (location as IConfigurationItemObject<Point>).SetValue(this.Location);

                IConfigurationItem recentProjects = conf.GetConfigurationItem(UI_Constants.TemplateRecentProjects);
                if (recentProjects != null)
                {
                    recentProjects.Clear();
                    foreach (IDecisionTree tree in ctrl.Trees)
                    {
                        if (File.Exists(tree.FullPath))
                        {
                            string itemName = string.Format("item-{0}", recentProjects.ConfigurationItems.Count + 1);
                            recentProjects.AddConfigurationItem(new IConfigurationItemImpl(itemName, tree.FullPath, UI_Constants.ConfigurationSource));
                        }
                    }

                    string path = ctrl.SelectedPath;
                    conf.SetConfigurationValue(UI_Constants.TemplateRecentNode, path);

                }

                try
                {
                    conf.Save();
                }
                catch (Exception ex)
                {
                    log.Debug(ex.ToString());
                }

                _IApplication.ShowHideTemplateEditor(false, true);

            }
            else
            {
                e.Cancel = true;
            }
        }

        #region IApplication implementation
        object IApplication.SelectedObject
        {
            get
            {
                return props != null ? props.SelectedObject : null;
            }

            set
            {
                if (props != null)
                    props.SelectedObject = value;

                IBaseNode node = value as IBaseNode;
                switch(node.NodeType)
                {
                    case eNodeType.DataDefinition:
                        IDataDefinition dataDefinition = value as IDataDefinition;
                        if(dataDefinition != null)
                            ReplaceEditor(dataDefinition.Editor);

                        break;

                    case eNodeType.DataObject:
                        ReplaceEditor(new ucDataObjectEditor(node));
                        break;

                    default:
                        ReplaceEditor();
                        break;

                }
            }
        }
        private void ReplaceEditor(SakwaUserControl newEditor = null)
        {
            SakwaUserControl oldEditor = editor.Controls.Count > 0 ? editor.Controls[0] as SakwaUserControl : null;
            if (oldEditor != null)
                oldEditor.OnDeactivate();

            editor.Controls.Clear();

            if (newEditor != null)
            {
                newEditor.Dock = DockStyle.Fill;
                editor.Controls.Add(newEditor);

                newEditor.OnActivate();

                newEditor.Font = ctrl.Font;

            }
        }
        IDecisionTree IApplication.SelectedTree
        {
            get { return null; }
            set { }
        }

        string IApplication.StatusLine
        {
            get
            {
                return lblStatusBar.Text;
            }
            set
            {
                lblStatusBar.Text = value;
                if (value != "")
                {
                    StatusTextTimer.Interval = UI_Constants.StatusTextDuration * 1000;
                    StatusTextTimer.Enabled = true;
                    StatusTextTimer.Start();
                }
            }
        }
        private void StatusTextTimer_Tick(object sender, EventArgs e)
        {
            lblStatusBar.Text = "";
            StatusTextTimer.Stop();
            StatusTextTimer.Enabled = false;

        }

        ucDecisionTree IApplication.ucDecisionTree
        {
            get
            {
                return ctrl;
            }
        }

        ucDecisionModel IApplication.ucDecisionModel
        {
            get
            {
                return null;
            }
        }

        string IApplication.GetResourceString(string keyValue)
        {
            return _IApplication.GetResourceString(keyValue);
        }

        Bitmap IApplication.GetResourceBitmap(string name)
        {
            return _IApplication.GetResourceBitmap(name);
        }

        Stream IApplication.GetResourceStream(string name)
        {
            return _IApplication.GetResourceStream(name);
        }

        void IApplication.ShowHelp(object sender, string page)
        {
            _IApplication.ShowHelp(sender, page);
        }

        FloatingForm IApplication.GetFloatingForm(eFloatReason reason, UserControl userControl)
        {
            float opacity = 1.0F;
            switch (reason)
            {
                case eFloatReason.Login:
                    opacity = 0.70F;
                    break;

                case eFloatReason.Abort:
                    opacity = 0.50F;
                    break;

                case eFloatReason.Other:
                case eFloatReason.NotSet:
                    opacity = 0.80F;
                    break;

            } //switch (reason)

            int HeaderHeight = 0; //Hight of the app that should remain bright
            GraphicsPath path = new GraphicsPath();
            path.AddLines(new Point[] {
            new Point(0, HeaderHeight),
            new Point(Width, HeaderHeight),
            new Point(Width, Height),
            new Point(0, Height)});
            path.CloseFigure();

            return new FloatingForm(this, opacity, userControl, path);

        }
        void IApplication.LoadModel(string fullPath)
        {
        }
        IDecisionTree IApplication.NewDescisionTree(string rootName, PostNodeInitialize nodeInitialization)
        {
            IDecisionTree result = _IApplication.NewDescisionTree(rootName, Result_PostNodeInitialize);
            return result;

        }
        private void Result_PostNodeInitialize(IBaseNode node)
        {
            #region Variable definitions
            if (node is UI_DomainObject)
            {
                (node as UI_DomainObject).DefineProperties(UI_Constants.ePropertyCategories.DataInformation);
                return;
            }
            if (node is UI_BoolVariable)
            {
                (node as UI_BoolVariable).DefineProperties(UI_Constants.ePropertyCategories.DataInformation);
                return;
            }
            if (node is UI_CharVariable)
            {
                (node as UI_CharVariable).DefineProperties(UI_Constants.ePropertyCategories.DataInformation);
            }
            if (node is UI_EnumVariable)
            {
                (node as UI_EnumVariable).DefineProperties(UI_Constants.ePropertyCategories.DataInformation);
            }
            if (node is UI_NumericVariable)
            {
                (node as UI_NumericVariable).DefineProperties(UI_Constants.ePropertyCategories.DataInformation);
            }
            #endregion
            if (node is IDataObject)
            {
                (node as UI_DataObject).DefineProperties(UI_Constants.ePropertyCategories.DataInformation);
            }
            if (node is IDataSource)
            {
                (node as IDataSource).DataSourceManager = _IApplication.DataSourceManager;
                (node as UI_DataSource).DefineProperties(UI_Constants.ePropertyCategories.DataInformation);
            }
        }

        string IApplication.LastUsedFolder
        {
            get { return _IApplication.LastUsedFolder; }
            set { _IApplication.LastUsedFolder = value; }
        }

        void IApplication.ShowHideTemplateEditor(bool show, bool removeOnHide)
        {
        }
        List<IDataSourceFactory> IApplication.DataSources
        {
            get { return _IApplication.DataSources; }
        }
        IDataSourceManager IApplication.DataSourceManager
        {
            get { return _IApplication.DataSourceManager; }
        }
        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {
            string rootName = string.Format("Domain template-{0}", ctrl.Trees.Count + 1);
            IDecisionTree tree = IApplicationInterface.NewDescisionTree(rootName);

            IBaseNode dataObjects = tree.CreateNewNode(eNodeType.DataObjects, null, Constants.DataNodesTreeName);
            tree.RootNode.AddNode(dataObjects);

            IBaseNode dataSources = tree.CreateNewNode(eNodeType.DataSources, null, Constants.DataSourcesTreeName);
            tree.RootNode.AddNode(dataSources);

            ctrl.AddTree(tree);

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = SakwaSupport.InitialFolder(null, SakwaSupport.eInitialFolder.Template, LastUsedFolder);
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                LoadFile(openFileDialog.FileName);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SakwaUserControl oldEditor = editor.Controls.Count > 0 ? editor.Controls[0] as SakwaUserControl : null;
            if (oldEditor != null)
                oldEditor.OnDeactivate();

            foreach (IDecisionTree tree in ctrl.Trees)
                if (tree.FullPath != "")
                    SaveTreeFile(tree, tree.FullPath);
                else
                {
                    saveFileDialog.InitialDirectory = SakwaSupport.InitialFolder(tree, SakwaSupport.eInitialFolder.Template, LastUsedFolder);
                    saveFileDialog.FileName = tree.RootNode.Name;
                    saveFileDialog.Title = string.Format(UI_Constants.File_Save, tree.RootNode.Name);
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                        SaveTreeFile(tree, saveFileDialog.FileName);
                }
        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SakwaUserControl oldEditor = editor.Controls.Count > 0 ? editor.Controls[0] as SakwaUserControl : null;
            if (oldEditor != null)
                oldEditor.OnDeactivate();

            foreach (IDecisionTree tree in ctrl.Trees)
            {
                saveFileDialog.InitialDirectory = SakwaSupport.InitialFolder(tree, SakwaSupport.eInitialFolder.Template, LastUsedFolder);
                saveFileDialog.Title = string.Format(UI_Constants.File_SaveAs, tree.RootNode.Name);
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    SaveTreeFile(tree, saveFileDialog.FileName);
            }
        }
        private void LoadFile(string fileName)
        {
            LastUsedFolder = fileName;
            IDecisionTree tree = IApplicationInterface.NewDescisionTree();
            IPersistence persistence = new XmlPersistenceImpl(fileName);

            if (tree.Load(persistence, fileName))
            {
                if (tree.RootNode.GetNode(eNodeType.DataObjects) == null)
                {
                    IBaseNode dataObjects = tree.CreateNewNode(eNodeType.DataObjects, null, Constants.DataNodesTreeName);
                    tree.RootNode.AddNode(dataObjects);

                }

                if (tree.RootNode.GetNode(eNodeType.DataSources) == null)
                { 
                    IBaseNode dataSources = tree.CreateNewNode(eNodeType.DataSources, null, Constants.DataSourcesTreeName);
                    tree.RootNode.AddNode(dataSources);

                }

                ctrl.AddTree(tree);
                IApplicationInterface.StatusLine = string.Format("Opened {0} from disk", Path.GetFileName(fileName)) + UI_Constants.ZoomPanHint;

            }
        }

        private string LastUsedFolder = "";

        private void SaveTreeFile(IDecisionTree tree, string fileName)
        {
            IPersistence persistence = new XmlPersistenceImpl(fileName, true);
            if (tree.Save(persistence, fileName))
                if (persistence.SaveAs(fileName))
                    IApplicationInterface.StatusLine = string.Format("{0} saved to disk", Path.GetFileName(fileName));
        }

        private void btnTreeModel_Click(object sender, EventArgs e)
        {
            ctrl.Visible = btnTreeModel.Checked;
            split.Visible = btnTreeModel.Checked;

        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            props.Visible = btnProperties.Checked;
            split2.Visible = btnProperties.Checked;

        }

        private IApplication IApplicationInterface {  get { return this as IApplication; } }

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            IApplicationInterface.ShowHelp(this, "templates.htm");
        }

        private void btnEnlargeText_Click(object sender, EventArgs e)
        {
            ctrl.Font = SetFont(ctrl.Font, 1.0F);
            props.Font = SetFont(props.Font, 1.0F);

            SakwaUserControl oldEditor = editor.Controls.Count > 0 ? editor.Controls[0] as SakwaUserControl : null;
            if (oldEditor != null)
                oldEditor.Font = SetFont(oldEditor.Font, 1.0F);

        }

        private void btnReduceText_Click(object sender, EventArgs e)
        {
            ctrl.Font = SetFont(ctrl.Font, -1.0F);
            props.Font = SetFont(props.Font, -1.0F);

            SakwaUserControl oldEditor = editor.Controls.Count > 0 ? editor.Controls[0] as SakwaUserControl : null;
            if (oldEditor != null)
                oldEditor.Font = SetFont(oldEditor.Font, -1.0F);

        }
        private Font SetFont(Font font, float delta)
        {
            return new Font(font.FontFamily, font.Size + delta, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
        }
    }
}
