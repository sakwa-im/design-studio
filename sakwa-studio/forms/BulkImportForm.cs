using configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public enum eImportAction { Default = 0, Skip = 1, Mine = 2, Theirs = 4, All = 7 }
    public enum eImportMode { Import, Link, Template }

    public partial class BulkImportForm : Form
    {
        public BulkImportForm(IBaseNode variables, IApplication app)
        {
            App = app;

            InitializeComponent();
            DestVariables = variables;

        }

        public BulkImportForm(IBaseNode variables, IApplication app, string modelFile, eImportMode mode = eImportMode.Link)
        {
            App = app;

            ImportMode = mode;

            InitializeComponent();
            DestVariables = variables;
            tbxSourceFile.Text = modelFile;

            switch(ImportMode)
            {
                case eImportMode.Import:
                    Text = UI_Constants.BulkImportText;
                    btnOk.Text = UI_Constants.BulkImportButton;
                    break;

                case eImportMode.Link:
                    Text = UI_Constants.BulkLinkText;
                    btnOk.Text = UI_Constants.BulkLinkButton;
                    break;

                case eImportMode.Template:
                    Text = UI_Constants.BulkTemplateText;
                    btnOk.Text = UI_Constants.BulkTemplateButton;
                    break;
            }

        }

        public string ModelFile
        {
            get { return tbxSourceFile.Text; }
        }

        private IBaseNode DestVariables = null;
        private IApplication App = null;

        public List<IBaseNode> ImportVariables
        {
            get
            {
                List<IBaseNode> result = new List<IBaseNode>();

                if (tvImport.Nodes.Count > 0)
                {
                    switch (ImportMode)
                    {
                        case eImportMode.Import:
                            TreeNode rootNode = tvImport.Nodes[0];
                            foreach (TreeNode node in rootNode.Nodes)
                            {
                                ImportVariable iv = node.Tag as ImportVariable;
                                if (iv != null)
                                {
                                    IBaseNode importNode = null;

                                    switch (iv.ImportAction)
                                    {
                                        case eImportAction.Mine:
                                            importNode = iv.Destination;
                                            break;

                                        case eImportAction.Theirs:
                                            importNode = iv.Source;
                                            importNode.Tree = DestVariables.Tree;
                                            importNode.Parent = DestVariables;
                                            break;

                                        case eImportAction.Default:
                                            if (iv.Destination != null)
                                            {
                                                iv.Destination.MergeNode(iv.Source);
                                                importNode = iv.Destination;
                                            }
                                            else
                                            {
                                                importNode = iv.Source;
                                                importNode.Tree = DestVariables.Tree;
                                                importNode.Parent = DestVariables;
                                            }
                                            break;
                                    }

                                    if (importNode != null)
                                    {
                                        result.Add(importNode);
                                        foreach (TreeNode n in node.Nodes)
                                            AddImportNode(n, importNode);

                                    }
                                }
                            }
                            break;

                        case eImportMode.Template:
                            rootNode = tvImport.Nodes[0];
                            foreach(TreeNode node in rootNode.Nodes)
                                AddTemplateNode(node, result, DestVariables);
                            break;

                        case eImportMode.Link:
                            AddLinkNode(tvImport.Nodes[0], ref result);
                            break;
                    }
                }

                return result;
            }
        }
        private void AddTemplateNode(TreeNode node, List<IBaseNode> result, IBaseNode parent)
        {
            ImportVariable iv = node.Tag as ImportVariable;
            if (iv != null)
            {
                IBaseNode importNode = null;
                switch (iv.ImportAction)
                {
                    case eImportAction.Mine:
                        importNode = iv.Destination.Clone();;
                        importNode.Reference = iv.Destination.Reference;
                        importNode.Tree = parent.Tree;
                        importNode.Parent = parent;
                        break;

                    case eImportAction.Theirs:
                        importNode = iv.Source.Clone();
                        importNode.Reference = iv.Source.Reference;
                        importNode.Tree = parent.Tree;
                        importNode.Parent = parent;
                        break;

                    case eImportAction.Default:
                        if (iv.Destination != null)
                        {
                            iv.Destination.MergeNode(iv.Source);
                            importNode = iv.Destination;
                        }
                        else
                        {
                            importNode = iv.Source.Clone();
                            importNode.Reference = iv.Source.Reference;
                            importNode.Tree = parent.Tree;
                            importNode.Parent = parent;
                        }
                        break;
                }

                if (importNode != null)
                {
                    result.Add(importNode);
                    foreach (TreeNode n in node.Nodes)
                        AddTemplateNode(n, importNode.Nodes, importNode);

                }
            }
        }
        private void AddLinkNode(TreeNode node, ref List<IBaseNode> result)
        {
            ImportVariable iv = node.Tag as ImportVariable;
            if (iv != null && iv.ImportAction == eImportAction.Mine)
                result.Add(iv.Source);

            foreach (TreeNode n in node.Nodes)
                AddLinkNode(n, ref result);

        }
        private void AddImportNode(TreeNode node, IBaseNode baseNode)
        {
            ImportVariable iv = node.Tag as ImportVariable;
            if (iv != null)
            {
                IBaseNode importNode = null;
                switch (iv.ImportAction)
                {
                    case eImportAction.Mine:
                        importNode = iv.Destination;
                        break;

                    case eImportAction.Skip:
                        break;

                    case eImportAction.Theirs:
                        int index = baseNode.Nodes.IndexOf(iv.Destination);
                        baseNode.Nodes.Remove(iv.Destination);
                        baseNode.Nodes.Insert(index, iv.Source);

                        importNode = iv.Source;
                        importNode.Tree = baseNode.Tree;
                        importNode.Parent = baseNode;
                        break;

                    case eImportAction.Default:
                        if (iv.Destination != null)
                        {
                            iv.Destination.MergeNode(iv.Source);
                            importNode = iv.Destination;
                        }
                        else
                        {
                            baseNode.Nodes.Add(iv.Source);

                            importNode = iv.Source;
                            importNode.Tree = baseNode.Tree;
                            importNode.Parent = baseNode;
                        }
                        break;

                }

                if (importNode != null)
                {
                    foreach (TreeNode n in node.Nodes)
                        AddImportNode(n, importNode);

                }
            }
        }

        public TreeNode RootNode {  get { return tvImport.Nodes.Count > 0 ? tvImport.Nodes[0] : null; } }

        private void Analyze()
        {
            tvImport.Nodes.Clear();
            TreeNode root = new TreeNode(UI_Constants.VariableTreeName);
            tvImport.Nodes.Add(root);

            switch (ImportMode)
            {
                case eImportMode.Import:
                    AddLevel(root, DestVariables, SrcVariables);
                    break;

                case eImportMode.Link:
                case eImportMode.Template:
                    AddLinkLevel(root, DestVariables, SrcVariables);
                    break;
            }

            root.Expand();
            tvImport.SelectedNode = root;

            btnOk.Enabled = CanImport;

        }
        private ImportVariable GetImportVariable(IBaseNode dest)
        {
            ImportVariable result = null;
            switch(ImportMode)
            {
                case eImportMode.Import:
                    result = new ImportVariable(dest);
                    break;

                case eImportMode.Link:
                    result = new LinkVariable(dest);
                    break;

                case eImportMode.Template:
                    result = new TemplateVariable(dest);
                    break;
            }

            return result;

        }
        private void AddLinkLevel(TreeNode node, IBaseNode destination, IBaseNode source)
        {
            foreach (IBaseNode src in source.Nodes)
            {
                IBaseNode dest = FindNode(destination, src);
                ImportVariable iv = GetImportVariable(dest);
                iv.Source = src;

                TreeNode newNode = iv.GetTreeNode();
                node.Nodes.Add(newNode);

                if (!iv.Equality.HasEqualityType(eVariableEquality.equal))
                    node.Expand();

                if (iv.Source.NodeType == eNodeType.DomainObject)
                    AddLinkLevel(newNode, iv.Destination, iv.Source);

            }
        }

        private void AddLevel(TreeNode node, IBaseNode destination, IBaseNode source)
        {
            if (destination != null)
            {
                foreach (IBaseNode dest in destination.Nodes)
                {
                    ImportVariable iv = new ImportVariable(dest);
                    iv.Source = FindNode(source, dest);

                    TreeNode newNode = iv.GetTreeNode();
                    node.Nodes.Add(newNode);

                    if (!iv.Equality.HasEqualityType(eVariableEquality.equal))
                        node.Expand();

                    if (iv.Destination.NodeType == eNodeType.DomainObject)
                        AddLevel(newNode, iv.Destination, iv.Source);

                }
            }

            if (source != null)
            {
                foreach (IBaseNode src in source.Nodes)
                    if (FindNode(destination, src) == null)
                    {
                        ImportVariable iv = new ImportVariable();
                        iv.Source = src;

                        TreeNode newNode = iv.GetTreeNode();
                        node.Nodes.Add(newNode);

                        if (iv.Source.NodeType == eNodeType.DomainObject)
                            AddLevel(newNode, iv.Destination, iv.Source);

                    }
            }
        }

        private static IBaseNode FindNode(IBaseNode haystack, IBaseNode needle)
        {
            if(haystack != null)
                foreach (IBaseNode n in haystack.Nodes)
                    if (n.NodeType == needle.NodeType && n.Name == needle.Name)
                        return n;

            return null;

        }

        public bool CanImport
        {
            get
            {
                return tvImport.Nodes.Count > 0 
                    ? CanMerge(tvImport.Nodes[0])
                    : false;
            }
        }
        protected bool CanMerge(TreeNode node)
        {
            if(node.Tag != null && !(node.Tag as ImportVariable).CanMerge)
                return false;

            bool result = true;
            foreach (TreeNode n in node.Nodes)
            {
                result &= CanMerge(n);
                if (!result)
                    return false;
            }

            return result;

        }

        private void tvImport_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode currentNode = tvImport.GetNodeAt(e.Location);
                tvImport.SelectedNode = currentNode;

                switch (ImportMode)
                {
                    case eImportMode.Import:
                    case eImportMode.Template:
                        ImportVariable lbItem = currentNode.Tag as ImportVariable;
                        if (lbItem != null)
                        {
                            mnuImportMine.Enabled = lbItem.CanUseMine;
                            mnuImportTheirs.Enabled = lbItem.CanUseTheirs;
                            mnuImportSkip.Enabled = !lbItem.CanUseMine;

                            mnuImport.Show(tvImport.PointToScreen(e.Location));
                        }
                        break;
                }
            }
        }
        private void tvImport_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ImportVariable impVar = tvImport.SelectedNode.Tag as ImportVariable;
            propDestination.SelectedObject = impVar != null ? impVar.Destination : null;
            propSource.SelectedObject = impVar != null ? impVar.Source : null;

        }
        private void mnuImportMine_Click(object sender, EventArgs e)
        {
            ImportVariable impVar = tvImport.SelectedNode.Tag as ImportVariable;
            if(impVar != null)
            {
                if(impVar.CanUseMine)
                    impVar.ToggleImportAction(eImportAction.Mine,
                        impVar.CanUseTheirs ? eImportAction.Theirs : eImportAction.Default);

                tvImport.SelectedNode.ImageIndex = impVar.ImageIndex;
                tvImport.SelectedNode.SelectedImageIndex = impVar.ImageIndex;
                tvImport.Refresh();

                btnOk.Enabled = CanImport;

            }
        }
        private void mnuImportTheirs_Click(object sender, EventArgs e)
        {
            ImportVariable impVar = tvImport.SelectedNode.Tag as ImportVariable;
            if (impVar != null)
            {
                if (impVar.ImportAction == eImportAction.Skip)
                    ExecuteUnskip(tvImport.SelectedNode);

                if (impVar.CanUseTheirs)
                    impVar.ToggleImportAction(eImportAction.Theirs, 
                        impVar.CanUseMine ? eImportAction.Mine : eImportAction.Default);

                tvImport.SelectedNode.ImageIndex = impVar.ImageIndex;
                tvImport.SelectedNode.SelectedImageIndex = impVar.ImageIndex;
                tvImport.Refresh();

                btnOk.Enabled = CanImport;

            }
        }
        private void mnuImportSkip_Click(object sender, EventArgs e)
        {
            ImportVariable impVar = tvImport.SelectedNode.Tag as ImportVariable;
            if (impVar != null)
            {
                if (impVar.ImportAction == eImportAction.Skip)
                    ExecuteUnskip(tvImport.SelectedNode);
                else
                    ExecuteSkip(tvImport.SelectedNode);

                tvImport.Refresh();

                btnOk.Enabled = CanImport;
            }
        }
        private void ExecuteSkip(TreeNode node)
        {
            ImportVariable impVar = node != null ? node.Tag as ImportVariable : null;
            if (impVar != null)
            {
                impVar.SkipImportAction();

                node.ImageIndex = impVar.ImageIndex;
                node.SelectedImageIndex = impVar.ImageIndex;

                foreach (TreeNode n in node.Nodes)
                    ExecuteSkip(n);

            }
        }
        private void ExecuteUnskip(TreeNode node)
        {
            ImportVariable impVar = node != null ? node.Tag as ImportVariable : null;
            if (impVar != null)
            {
                impVar.UnskipImportAction();

                node.ImageIndex = impVar.ImageIndex;
                node.SelectedImageIndex = impVar.ImageIndex;

                foreach (TreeNode n in node.Nodes)
                    ExecuteUnskip(n);

            }
        }
        private void Listbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lbx = sender as ListBox;
            ImportVariable lbItem = lbx.Items[e.Index] as ImportVariable;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[lbItem.ImageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbItem.Name, lbx.Font, new SolidBrush(SystemColors.ButtonHighlight), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[lbItem.ImageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbItem.Name, lbx.Font, new SolidBrush(SystemColors.MenuText), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }

            int imageIndex = 0;
            switch(lbItem.ImportAction)
            {
                case eImportAction.Mine: imageIndex = 5; break; 
                case eImportAction.Theirs: imageIndex = 6; break; 
                case eImportAction.Skip: imageIndex = 7; break;
            }

            if (imageIndex > 0)
                e.Graphics.DrawImage(imageList.Images[imageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);

            e.DrawFocusRectangle();

        }
        private void tvConflicts_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {

            TreeView tv = sender as TreeView;
            if(e.Node == null || e.Node.Tag == null)
            {
                e.DrawDefault = true;
                return;

            }

            ImportVariable tvItem = e.Node.Tag as ImportVariable;
            int offs = e.Node.Level * tv.Indent;
            Rectangle bounds = e.Bounds;
            bounds.X += offs;

            if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), bounds);
                e.Graphics.DrawImage(imageList.Images[tvItem.ImageIndex], bounds.X , bounds.Y, tv.ItemHeight, tv.ItemHeight);
                //e.Graphics.DrawString(tvItem.Name, tv.Font, new SolidBrush(SystemColors.ButtonHighlight), bounds.X + tv.ItemHeight, bounds.Y);

            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), bounds);
                e.Graphics.DrawImage(imageList.Images[tvItem.ImageIndex], bounds.X, bounds.Y, tv.ItemHeight, tv.ItemHeight);
                e.Graphics.DrawString(tvItem.Name, tv.Font, new SolidBrush(SystemColors.MenuText), bounds.X + tv.ItemHeight, bounds.Y);

            }

            int imageIndex = 0;
            switch (tvItem.ImportAction)
            {
                case eImportAction.Mine: imageIndex = 5; break;
                case eImportAction.Theirs: imageIndex = 6; break;
                case eImportAction.Skip: imageIndex = 7; break;
            }

            if (imageIndex > 0)
                e.Graphics.DrawImage(imageList.Images[imageIndex], bounds.X, bounds.Y, tv.ItemHeight, tv.ItemHeight);

            //e.DrawFocusRectangle();

        }

        private void BulkImportForm_Load(object sender, EventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            if (conf.GetConfigurationItem(UI_Constants.BulkImportFormSize) == null)
                DefineConfigurationItems();

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.BulkImportFormSize);
            this.Size = (size as IConfigurationItemObject<Size>).GetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.BulkImportFormLocation);
            this.Location = (location as IConfigurationItemObject<Point>).GetValue(this.Location);

            //Make sure the form is shown on the visible screen
            if (!Screen.GetWorkingArea(this).IntersectsWith(new Rectangle(this.Location, this.Size)))
                this.Location = new Point(100, 100);

            size = conf.GetConfigurationItem(UI_Constants.BulkImportFormSplitter1Location);
            tvImport.Size = (size as IConfigurationItemObject<Size>).GetValue(tvImport.Size);

            size = conf.GetConfigurationItem(UI_Constants.BulkImportFormSplitter2Location);
            propDestination.Size = (size as IConfigurationItemObject<Size>).GetValue(propDestination.Size);

        }

        private void BulkImportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;

            IConfigurationItem size = conf.GetConfigurationItem(UI_Constants.BulkImportFormSize);
            (size as IConfigurationItemObject<Size>).SetValue(this.Size);

            IConfigurationItem location = conf.GetConfigurationItem(UI_Constants.BulkImportFormLocation);
            (location as IConfigurationItemObject<Point>).SetValue(this.Location);

            size = conf.GetConfigurationItem(UI_Constants.BulkImportFormSplitter1Location);
            (size as IConfigurationItemObject<Size>).SetValue(tvImport.Size);

            size = conf.GetConfigurationItem(UI_Constants.BulkImportFormSplitter2Location);
            (size as IConfigurationItemObject<Size>).SetValue(propDestination.Size);

            conf.Save();

        }

        public static void DefineConfigurationItems()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            IConfigurationItemObject<Size> sizeForm =
                 new ConfigurationItemObject<Size>(UI_Constants.BulkImportFormSize, new Size(478, 437), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", sizeForm as IConfigurationItem);

            IConfigurationItemObject<Point> locationForm =
                new ConfigurationItemObject<Point>(UI_Constants.BulkImportFormLocation, new Point(100, 50), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationForm as IConfigurationItem);

            IConfigurationItemObject<Size> locationSplitter1 =
                new ConfigurationItemObject<Size>(UI_Constants.BulkImportFormSplitter1Location, new Size(190, 207), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationSplitter1 as IConfigurationItem);

            IConfigurationItemObject<Size> locationSplitter2 =
                new ConfigurationItemObject<Size>(UI_Constants.BulkImportFormSplitter2Location, new Size(194, 168), UI_Constants.ConfigurationSource);
            conf.AddConfigurationItem("", locationSplitter2 as IConfigurationItem);

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            switch (ImportMode)
            {
                case eImportMode.Import:
                    openFileDialog.Filter = UI_Constants.BulkImportFilter;
                    break;

                case eImportMode.Link:
                    openFileDialog.Filter = UI_Constants.BulkLinkFilter;
                    break;

                case eImportMode.Template:
                    openFileDialog.Filter = UI_Constants.BulkTemplateFilter;
                    break;
            }

            openFileDialog.FileName = tbxSourceFile.Text;
            if(openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                tbxSourceFile.Text = openFileDialog.FileName;

                if (tbxSourceFile.Text == DestVariables.Tree.FullPath)
                {
                    App.GetFloatingForm(eFloatReason.NotSet, new ucIllegalModel(tbxSourceFile.Text)).ShowDialog();
                    tbxSourceFile.Text = "";
                }
            }
        }

        private void tbxSourceFile_TextChanged(object sender, EventArgs e)
        {
            if(File.Exists(tbxSourceFile.Text))
            {
                SourceTree = App.NewDescisionTree();
                IPersistence persistence = new XmlPersistenceImpl(tbxSourceFile.Text);
                SrcVariables = SourceTree.LoadVariables(persistence);
                if(SrcVariables != null && SrcVariables.Nodes.Count > 0)
                    Analyze();

            }
        }

        protected IDecisionTree SourceTree = null;
        protected IBaseNode SrcVariables = null;

        protected eImportMode ImportMode = eImportMode.Import;

        private void propDestination_SizeChanged(object sender, EventArgs e)
        {
            lblSource.Left = propDestination.Width + splitter2.Width;
        }

        private void picClear_Click(object sender, EventArgs e)
        {
            tbxSourceFile.Text = "";
        }
    }

    public class ImportVariable
    {
        protected const int MinorConflict = 3;
        protected const int MajorConflict = 6;

        public ImportVariable() {}
        public ImportVariable(IBaseNode dest)
        {
            Destination = dest;
            _ImageIndex = 0;
            _ImportAction = dest == null ? eImportAction.Mine : eImportAction.Theirs;
            _DefaultAction = _ImportAction;

        }
        public string Name
        {
            get
            {
                if(Destination != null)
                    return Destination.Name;

                return Source != null ? Source.Name : "error";

            }
        }

        public IBaseNode Destination = null;
        public IBaseNode Source
        {
            get { return _Source; }
            set
            {
                _Source = value;
                OnSourceSet();

            }
        }
        protected virtual void OnSourceSet()
        {
            if (_Source != null)
            {
                if (Destination != null)
                {
                    _Equality = Destination.Compare(_Source);
                    if (_Equality.Blocking)
                    {
                        _ImageIndex = MajorConflict;
                        return;
                    }

                    if (_Equality.Major)
                    {
                        _ImageIndex = MinorConflict;
                        return;

                    }
                }

                if (Destination == null)
                    _ImageIndex = 2;

            }
        }

        public eImportAction ImportAction
        {
            get { return _ImportAction; }
            set { _ImportAction = value; }
        }
        public static IVariableDef NoBasedeAsVariableDef(IBaseNode node)
        {
            return node as IVariableDef;
        }

        public NodeEqualityCollection Equality {  get { return _Equality;  } }

        public int ImageIndex
        {
            get
            {
                int result = _ImageIndex;

                switch(_ImportAction)
                {
                    case eImportAction.Mine: result++; break;
                    case eImportAction.Theirs: result += 2; break;
                    case eImportAction.Skip: result = 9; break;

                }

                return result;

            }
        }

        public TreeNode GetTreeNode()
        {
            TreeNode result = new TreeNode(Name);

            result.Tag = this;
            result.ImageIndex = ImageIndex;
            result.SelectedImageIndex = ImageIndex;

            result.ToolTipText = toolTip;

            return result;

        }
        protected string toolTip
        {
            get
            {
                string tip = "";
                if (_Equality.HasEqualityType(eNodeEquality.basetype))
                    tip += tip == "" ? UI_Constants.BulkImportBaseTypeError : Environment.NewLine + UI_Constants.BulkImportBaseTypeError;

                if (_Equality.HasEqualityType(eVariableEquality.type))
                    tip += tip == "" ? UI_Constants.BulkImportTypeError : Environment.NewLine + UI_Constants.BulkImportTypeError;

                if (_Equality.HasEqualityType(eVariableEquality.domain))
                    tip += tip == "" ? UI_Constants.BulkImportDomainError : Environment.NewLine + UI_Constants.BulkImportDomainError;

                if (_Equality.HasEqualityType(eVariableEquality.min))
                    tip += tip == "" ? UI_Constants.BulkImportMinError : Environment.NewLine + UI_Constants.BulkImportMinError;

                if (_Equality.HasEqualityType(eVariableEquality.max))
                    tip += tip == "" ? UI_Constants.BulkImportMaxError : Environment.NewLine + UI_Constants.BulkImportMaxError;

                if (_Equality.HasEqualityType(eVariableEquality.value))
                    tip += tip == "" ? UI_Constants.BulkImportValueError : Environment.NewLine + UI_Constants.BulkImportValueError;

                return tip;

            }
        }
        public bool CanMerge
        {
            get
            {
                bool result = true;

                if (_ImportAction == eImportAction.Mine)
                    return true;

                if (_Equality.HasEqualityType(eVariableEquality.basetype | eVariableEquality.type))
                    result = _ImportAction == eImportAction.Theirs || _ImportAction == eImportAction.Mine;

                return result;

            }
        }

        public bool CanUseMine { get { return Destination != null; } }
        public bool CanUseTheirs
        {
            get
            {
                return Destination == null || Source != null;
            }
        }
        public void ToggleImportAction(eImportAction action, eImportAction fallBack)
        {
            if ((_ImportAction & action) == action)
                _ImportAction = _DefaultAction;
            else
                _ImportAction = action;

        }
        public void SkipImportAction()
        {
            if (_ImportAction != eImportAction.Skip)
            {
                _PrevAction = _ImportAction;
                _ImportAction = eImportAction.Skip;
            }
        }
        public void UnskipImportAction()
        {
            if (_ImportAction == eImportAction.Skip)
                _ImportAction = _PrevAction;
        }
        protected IBaseNode _Source = null;
        protected eImportAction _ImportAction = eImportAction.Default;
        protected eImportAction _DefaultAction = eImportAction.Default;
        protected eImportAction _PrevAction = eImportAction.Default;
        protected int _ImageIndex = 0;
        protected NodeEqualityCollection _Equality = new NodeEqualityCollection();

    }
    public class LinkVariable : ImportVariable
    {
        public LinkVariable() : base() { }
        public LinkVariable(IBaseNode dest)
            : base(dest)
        {
        }

    }
    public class TemplateVariable : ImportVariable
    {
        public TemplateVariable() : base() { }
        public TemplateVariable(IBaseNode dest)
            : base(dest)
        {
            _ImportAction = dest == null ? eImportAction.Theirs : eImportAction.Mine;
            _DefaultAction = _ImportAction;

        }

        protected override void OnSourceSet()
        {
            if (_Source != null)
            {
                if (Destination != null)
                {
                    _Equality = Destination.Compare(_Source, eCompareMode.Full);
                    if (_Equality.Blocking)
                    {
                        _ImageIndex = MajorConflict;
                        return;
                    }

                    if (_Equality.Major)
                    {
                        _ImageIndex = MinorConflict;
                        return;

                    }

                    if (_Equality.HasEqualityType(eNodeEquality.reference))
                    {
                        _ImageIndex = 0;
                        _ImportAction = eImportAction.Theirs;
                        return;

                    }
                }
            }
        }

    }
}
