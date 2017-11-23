using configuration;
using kms;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace sakwa
{
    [Export]
    public partial class MainForm : Form, IApplication, IPartImportsSatisfiedNotification
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainForm));
        public MainForm()
        {
            InitializeComponent();
        }

        IConfiguration conf = ConfigurationRepository.IConfiguration;

        [Import]
        public IDataSourceManager DataSourceManager { get; set; }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text += version.ToString(3);

            #region Setup Configuration
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.MainFormState, "", UI_Constants.ConfigurationSource));

            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.DebugEnabled, "False", UI_Constants.NoConfigSource));

            IConfigurationItemObject<Size> sizeMainForm = new ConfigurationItemObject<Size>(UI_Constants.MainFormSize, new Size(686, 432), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", sizeMainForm as IConfigurationItem);
            IConfigurationItemObject<Point> locationMainForm = new ConfigurationItemObject<Point>(UI_Constants.MainFormLocation, new Point(100, 50), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationMainForm as IConfigurationItem);

            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.RecentFiles, "", UI_Constants.ConfigurationSource));
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.RecentProjects, "", UI_Constants.ConfigurationSource));
            conf.AddConfigurationItem("", new IConfigurationItemImpl(UI_Constants.RecentNode, "", UI_Constants.ConfigurationSource));

            if (conf.GetConfigurationItem(UI_Constants.SakwaModelOnStart) == null)
                ConfigurationForm.DefineConfigurationItems();

            #endregion
            #region App configuration
            switch (conf.GetConfigurationValue(UI_Constants.MainFormState))
            {
                case "Maximized":
                    WindowState = FormWindowState.Maximized;
                    break;

                case "Normal":
                    IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.MainFormSize);
                    this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

                    IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.MainFormLocation);
                    this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);
                    if (this.Location.X < 0)
                        this.Location = new Point(0, 0);

                    //Make sure the form is shown on the visible screen
                    if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                        this.Location = new Point(100, 100);

                    break;

                default:
                    size = conf.GetConfigurationItem(UI_Constants.MainFormSize);
                    this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

                    location = conf.GetConfigurationItem(UI_Constants.MainFormLocation);
                    this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);

                    //Make sure the form is shown on the visible screen
                    if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                        this.Location = new Point(100, 100);

                    break;

            } //switch(conf.GetConfigurationValue("window-state"))
            #endregion

            InitializeControl();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool CanExit = true;
            DialogResult UserSelected = DialogResult.None;

            foreach (IDecisionTree tree in ctrl.Trees)
            {
                if (tree.IsDirty)
                {
                    UserSelected = IApplicationInterface.GetFloatingForm(
                        eFloatReason.NotSet, 
                        new ucUnsavedExit()).ShowDialog();

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
                conf.SetConfigurationValue(UI_Constants.MainFormState, this.WindowState.ToString());
                IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.MainFormSize);
                (size as IConfigurationItemObject<Size>).SetValue(this.Size);

                IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.MainFormLocation);
                (location as IConfigurationItemObject<Point>).SetValue(this.Location);

                IConfigurationItem recentProjects = conf.GetConfigurationItem(UI_Constants.RecentProjects);
                if (recentProjects != null)
                {
                    bool savedFiles = false;
                    recentProjects.Clear();
                    foreach (IDecisionTree tree in ctrl.Trees)
                    {
                        if (File.Exists(tree.FullPath))
                        {
                            string itemName = string.Format("item-{0}", recentProjects.ConfigurationItems.Count + 1);
                            recentProjects.AddConfigurationItem(new IConfigurationItemImpl(itemName, tree.FullPath, UI_Constants.ConfigurationSource));
                            savedFiles = true;

                        }
                    }

                    if(savedFiles)
                    {
                        string path = ctrl.SelectedPath;
                        conf.SetConfigurationValue(UI_Constants.RecentNode, path);

                    }
                }

                try
                {
                    conf.Save();
                }
                catch (Exception ex)
                {
                    log.Debug(ex.ToString());
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        object IApplication.SelectedObject {
            get
            {
                return props != null ? props.SelectedObject : null;
            }
            set
            {
                if (props != null)
                    props.SelectedObject = value;
            }
        }
        IDecisionTree IApplication.SelectedTree
        {
            get { return model.Tree; }
            set { model.Tree = value; }
        }

        string IApplication.StatusLine
        {
            get { return lblStatusBar.Text; }
            set
            {
                lblStatusBar.Text = value;
                if(value != "")
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

        ucDecisionTree IApplication.ucDecisionTree { get { return ctrl; } }
        ucDecisionModel IApplication.ucDecisionModel {  get { return model; } }
        void IApplication.LoadModel(string fileName)
        {
            LoadFile(fileName);
        }

        string IApplication.GetResourceString(string keyValue)
        {
            if (keyValue == null || keyValue == "")
                return "";

            if (resourceManager == null)
                resourceManager = new ResourceManager(GetType().Namespace + ".Properties.Resources", Assembly.GetExecutingAssembly());

            string key = keyValue.Replace(' ', '_');
            key = key.Replace('-', '_');

            string result = key != "" ? resourceManager.GetString(key) : null;
            return result != null ? result : keyValue;

        }
        Bitmap IApplication.GetResourceBitmap(string name)
        {
            Stream configStream = GetType().Assembly.GetManifestResourceStream(GetType().Namespace + ".resources." + name);
            return (configStream != null) ? new Bitmap(configStream) : null;
        }
        Stream IApplication.GetResourceStream(string name)
        {
            Stream configStream = GetType().Assembly.GetManifestResourceStream(GetType().Namespace + ".resources." + name);
            configStream.Position = 0;
            return configStream;

        }

        private ResourceManager resourceManager = null;
        /// <summary>
        /// Opens a browser containing h application's help file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="hlpevent"></param>
        void IApplication.ShowHelp(object sender, string page)
        {
            string uri = conf.GetConfigurationValue(UI_Constants.FullHelpPath, "");

            if (uri == "")
            {
                uri = conf.GetConfigurationValue(UI_Constants.ProgramFolder) + "Help";

                uri += Path.DirectorySeparatorChar;
                uri += page.EndsWith(".htm") ? page : page + ".htm";

            }

            try
            {
                if(!uri.EndsWith(page))
                    uri = uri.Substring(0, uri.LastIndexOf(Path.DirectorySeparatorChar) + 1) + page;

                Help.ShowHelp(this, uri);

            }
            catch (Exception exc)
            {
                log.Debug(exc.ToString());
            }
        } //void IApplication.ShowHelp(HelpEventArgs hlpevent)

        /// <summary>
        /// Function creates a dialogbox floating over the main screen
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="userControl"></param>
        /// <returns></returns>
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

        } //FloatingForm IApplication.GetFloatingForm( ...

        IDecisionTree IApplication.NewDescisionTree(string rootName, PostNodeInitialize nodeInitialization)
        {
            Assembly asm = Assembly.GetCallingAssembly();

            SakwaMapping mapping = new SakwaMapping("0.0");
            mapping.SakwaObjectMapping.Add(typeof(IBaseNodeImpl).ToString(), typeof(UI_BaseNode).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IRootNodeImpl).ToString(), typeof(UI_RootNode).ToString());

            mapping.SakwaObjectMapping.Add(typeof(CharVariableImpl).ToString(), typeof(UI_CharVariable).ToString());
            mapping.SakwaObjectMapping.Add(typeof(NumericVariableImpl).ToString(), typeof(UI_NumericVariable).ToString());
            mapping.SakwaObjectMapping.Add("sakwa.IntVariableImpl", typeof(UI_NumericVariable).ToString());
            mapping.SakwaObjectMapping.Add(typeof(EnumVariableImpl).ToString(), typeof(UI_EnumVariable).ToString());
            mapping.SakwaObjectMapping.Add(typeof(BoolVariableImpl).ToString(), typeof(UI_BoolVariable).ToString());
            mapping.SakwaObjectMapping.Add(typeof(DateVariableImpl).ToString(), typeof(UI_DateVariable).ToString());

            mapping.SakwaObjectMapping.Add(typeof(IDomainObjectImpl).ToString(), typeof(UI_DomainObject).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IExpressionImpl).ToString(), typeof(UI_Expression).ToString());
            mapping.SakwaObjectMapping.Add("sakwa.IAssignmentImpl", typeof(UI_Expression).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IBranchImpl).ToString(), typeof(UI_Branch).ToString());

            mapping.SakwaObjectMapping.Add(typeof(IDataObjectImpl).ToString(), typeof(UI_DataObject).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IDataSourceImpl).ToString(), typeof(UI_DataSource).ToString());
            mapping.SakwaObjectMapping.Add(typeof(IDataDefinitionImpl).ToString(), typeof(UI_DataDefinition).ToString());

            if (nodeInitialization == null)
                nodeInitialization = Result_PostNodeInitialize;

            IDecisionTree result = rootName != "" 
                ? new IDecisionTreeImpl(asm, mapping, nodeInitialization, rootName) 
                : new IDecisionTreeImpl(asm, mapping, nodeInitialization);

            result.LinkSubtreeError += LinkSubtreeError;

            return result;

        } //IApplication.NewDescisionTree
        private void Result_PostNodeInitialize(IBaseNode node)
        {
            if (node is UI_RootNode)
            {
                (node as UI_RootNode).DefineProperties(UI_Constants.ePropertyCategories.DomainTemplate);
                return;
            }
        }

        string IApplication.LastUsedFolder { get { return LastUsedFolder; } set { LastUsedFolder = value; } }

        void IApplication.ShowHideTemplateEditor(bool show, bool removeOnHide)
        {
            if(show)
            {
                if (formTemplates == null)
                    formTemplates = new TemplateForm(this);

                formTemplates.Show(this);

            }
            else
            {
                if (formTemplates != null)
                    formTemplates.Hide();

                if (removeOnHide)
                {
                    formTemplates = null;
                    btnEditTemplate.Checked = false;
                    this.Select();

                }
            }
        } //IApplication.ShowHideTemplateEditor

        List<IDataSourceFactory> IApplication.DataSources
        {
            get
            {
                return DataSourceManager != null
                  ? DataSourceManager.DataSourceFactories
                  : new List<IDataSourceFactory>();
            }
        }
        IDataSourceManager IApplication.DataSourceManager
        {
            get { return DataSourceManager; }
        }

        private void LinkSubtreeError(LinkSubTree subTree)
        {
            // Open SubModel error 
            IApplicationInterface.GetFloatingForm(eFloatReason.NotSet, new ucSubModelNotFound(subTree))
                .ShowDialog();     
        }

        private void btnEnlargeText_Click(object sender, EventArgs e)
        {
            ctrl.Font = SetFont(ctrl.Font, 1.0F);
            props.Font = SetFont(props.Font, 1.0F);

        }

        private void btnReduceText_Click(object sender, EventArgs e)
        {
            ctrl.Font = SetFont(ctrl.Font, -1.0F);
            props.Font = SetFont(props.Font, -1.0F);

        }

        private void InitializeControl()
        {
            pnlMain.Controls.Clear();
            //because tere is a dependency from ucDecisionTree on ucDecisionModel
            //first create ucDecisionModel
            model = new ucDecisionModel(this as IApplication);
            model.Dock = DockStyle.Fill;

            ctrl = new ucDecisionTree(this as IApplication);
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

            pnlMain.Controls.AddRange(new Control[] { model, split, ctrl, split2, props });

            if(conf.GetConfigurationValue(UI_Constants.SakwaModelOnStart, false))
            {
                IConfigurationItem recentProjects = conf.GetConfigurationItem(UI_Constants.RecentProjects);
                if(recentProjects != null)
                    foreach (IConfigurationItem it in recentProjects.ConfigurationItems)
                        LoadFile(it.Value);


                ctrl.SelectedPath = conf.GetConfigurationValue(UI_Constants.RecentNode, "");

            }
        }
        private Font SetFont(Font font, float delta)
        {
            return new Font(font.FontFamily, font.Size + delta, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            if(Control.ModifierKeys == (Keys.Control | Keys.Shift))
            {
                IApplicationInterface.GetFloatingForm(
                    eFloatReason.NotSet,
                    new ucPropertyEditor(DataSourceManager.DataSourceFactories[0]))
                    .ShowDialog(this);

                return;

            }

            // Open NewModel UserControl 
            ucNewModel newModel = new ucNewModel();
            string rootName = string.Format("Decision model-{0}", ctrl.Trees.Count + 1);
            if (IApplicationInterface.GetFloatingForm(eFloatReason.NotSet, newModel)
                .ShowDialog() == DialogResult.OK)
            {
                IDecisionTree tree = IApplicationInterface.NewDescisionTree(rootName);
                tree.Persistence = new XmlPersistenceImpl();
                (tree.RootNode as IRootNode).DomainTemplate = newModel.SelectedDomainModelFile;

                ctrl.AddTree(tree);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = SakwaSupport.InitialFolder(null, SakwaSupport.eInitialFolder.Model, LastUsedFolder);
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                LoadFile(openFileDialog.FileName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (IDecisionTree tree in ctrl.Trees)
                if (tree.FullPath != "")
                    SaveTreeFile(tree, tree.FullPath);
                else
                {
                    saveFileDialog.InitialDirectory = SakwaSupport.InitialFolder(tree, SakwaSupport.eInitialFolder.Model, LastUsedFolder);
                    saveFileDialog.FileName = tree.RootNode.Name;
                    saveFileDialog.Title = string.Format(UI_Constants.File_Save, tree.RootNode.Name);
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                        SaveTreeFile(tree, saveFileDialog.FileName);
                }
        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            foreach (IDecisionTree tree in ctrl.Trees)
            {
                saveFileDialog.InitialDirectory = SakwaSupport.InitialFolder(tree, SakwaSupport.eInitialFolder.Model, LastUsedFolder);
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
                ctrl.AddTree(tree);
                IApplicationInterface.StatusLine = string.Format("Opened {0} from disk", Path.GetFileName(fileName)) + UI_Constants.ZoomPanHint;
            }
        }
        private void SaveTreeFile(IDecisionTree tree, string fileName)
        {
            IPersistence persistence = new XmlPersistenceImpl(fileName, true);
            if (tree.Save(persistence, fileName))
                if (persistence.SaveAs(fileName))
                    IApplicationInterface.StatusLine = string.Format("{0} saved to disk", Path.GetFileName(fileName));
        }

        private string UpsertBackslash(string path)
        {
            if(path != "")
                if (path[path.Length - 1] != Path.DirectorySeparatorChar)
                    path += Path.DirectorySeparatorChar;

            return path;

        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            ConfigurationForm frm = new ConfigurationForm(0, this);

            frm.ShowDialog(this);
               
        }

        private void lblVersion_Click(object sender, EventArgs e)
        {
            IApplicationInterface.ShowHelp(sender, UI_Constants.HelpChangeLog);
        }

        protected IApplication IApplicationInterface {  get { return this as IApplication; } }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            IApplicationInterface.ShowHelp(sender, UI_Constants.HelpIndex);
        }


        #region Last resort Exception Handling
        public static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                String errmessage = "Fatal Error.\n"
                                    + "Exception Message: " + ex.Message + "\n"
                                    + "Exception Source: " + ex.Source + "\n"
                                    + "Exception TargetSite: " + ex.TargetSite + "\n"
                                    + "Exception StackTrace: " + ex.StackTrace + "\n";
                log.Error(errmessage);

                errmessage = "An application error occurred. Please contact the adminstrator with the following information:\n\n";
                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("sakwa UnhandledException"))
                {
                    EventLog.CreateEventSource("sakwa UnhandledException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog errLog = new EventLog();
                errLog.Source = "sakwa UnhandledException";
                errLog.WriteEntry(errmessage + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace, EventLogEntryType.Error);

            }
            catch (Exception exc)
            {
                try
                {
                    log.Debug(exc.ToString());
                }
                finally
                {
                    Application.Exit();
                }
            }
        } //public static void UnhandledException( ...
        public static void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.Exception;
                String errorMsg = "An application error occurred. Please contact the adminstrator with the following information:\n\n";
                errorMsg = errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace;
                log.Error(errorMsg);
            }
            catch
            {
                try
                {
                }
                finally
                {
                    Application.Exit();
                }
            }
        } //public static void ThreadException( ...
        #endregion

        private void MainForm_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            log.Debug("MainForm_HelpButtonClicked");
        }

        private void MainForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            log.Debug("MainForm_HelpRequested");
        }

        private void btnTreeModel_Click(object sender, EventArgs e)
        {
            ctrl.Visible = btnTreeModel.Checked;
            split.Visible = btnTreeModel.Checked;

        }

        
        private void btnGraphicModel_Click(object sender, EventArgs e)
        {
            model.Visible = btnGraphicModel.Checked;

        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            props.Visible = btnProperties.Checked;
            split2.Visible = btnProperties.Checked;
        }

        private void btnEditTemplate_Click(object sender, EventArgs e)
        {
            IApplicationInterface.ShowHideTemplateEditor(btnEditTemplate.Checked);
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
            log.Debug(DataSourceManager.DataSourceFactories.Count.ToString());
            log.Debug("Parts loaded");
        }

        private ucProperties props = null;
        private ucDecisionTree ctrl = null;
        private ucDecisionModel model = null;
        private Splitter split = null;
        private Splitter split2 = null;
        
        private TemplateForm formTemplates = null;
        private string LastUsedFolder = "";

    }
}
