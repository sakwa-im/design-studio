using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using configuration;
using log4net;

namespace sakwa
{
    public partial class ucDataObjectEditor : SakwaUserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ucDataObjectEditor));

        protected const int EXPORT_VARIABLE_MAPPING = 0;
        protected const int EXPORT_METHOD_MAPPING = 1;
        protected const int DOMAINOBJECT_MAPPING = 2;
        protected const int VARIABLE_MAPPING = 3;
        protected const int BLANK_MAPPING = 4;

        public ucDataObjectEditor()
        {
            InitializeComponent();
        }
        public ucDataObjectEditor(IBaseNode node)
        {
            InitializeComponent();
            _DataObject = node as IDataObject;

            List<IBaseNode> linkedNodes = node.Tree.GetVariables(node);
            foreach (IBaseNode n in linkedNodes)
            {
                lbxLinkedNodes.Items.Add(new ListBoxItem(n));
            }

            foreach (string reference in _DataObject.DataPersistence.DataConnections)
            {
                IBaseNode link = _DataObject.Tree.GetNodeByReference(reference);
                if (link != null)
                {
                    IDataDefinition datadef = link as IDataDefinition;
                    if (datadef != null)
                        foreach (IDataDefinitionExport export in datadef.Exports)
                        {
                            lbxExports.Items.Add(export);
                        }
                }
            }

            Root = new TreeNode(Constants.ModelDatasourceMapping);
            Root.SelectedImageIndex = Root.ImageIndex = BLANK_MAPPING;
            tvModelDatasources.Nodes.Add(Root);

            foreach (IMapping mapping in _DataObject.DecisionModelDataSources)
                AddMapping(Root, mapping);

            Root.ExpandAll();

        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            lbxLinkedNodes.Font = Font;
            lbxExports.Font = Font;
            tvModelDatasources.Font = Font;
        }
        protected void AddMapping(TreeNode node, IMapping mapping)
        {
            TreeNode newNode = TreeNodeFromBaseNode(mapping.DecisionModelNode);

            Root.Nodes.Add(newNode);

            foreach (IDataDefinitionExport export in mapping.ExportMaps)
                newNode.Nodes.Add(TreeNodeFromDataDefinitionExport(export));

        }
        protected override void DoActivate()
        {
            if (conf.GetConfigurationItem("") == null)
                DefineConfiguration();

            //tabControl1.SelectedIndex = conf.GetConfigurationValue(SelectedTab, 0);

        }

        protected override void DoDeactivate()
        {
            _DataObject.DecisionModelDataSources.Clear();
            foreach(TreeNode node in Root.Nodes)
            {
                IMapping mapping = new IMappingImpl();
                mapping.DecisionModelNode = node.Tag as IBaseNode;

                foreach (TreeNode tn in node.Nodes)
                    mapping.ExportMaps.Add(tn.Tag as IDataDefinitionExport);

                _DataObject.DecisionModelDataSources.Add(mapping);

            }

            conf.Save();

        }

        protected override void DefineConfiguration()
        {
            //conf.AddConfigurationItem(new IConfigurationItemImpl(SelectedTab, "0", Constants.ConfigurationSource));

        }

        private void lbxIDataDefinitionExport_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lbx = sender as ListBox;
            if (lbx.Items.Count == 0)
                return;

            IDataDefinitionExport lbi = lbx.Items[e.Index] as IDataDefinitionExport;
            int imageIndex = Convert.ToInt32(lbi.ExportType);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[imageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbi.Name, lbx.Font, new SolidBrush(SystemColors.ButtonHighlight), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[imageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbi.Name, lbx.Font, new SolidBrush(SystemColors.MenuText), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }

            e.DrawFocusRectangle();

        }
        private void lbxExports_MouseDown(object sender, MouseEventArgs e)
        {
            lbxExports.Refresh();
            lbxExports.DoDragDrop(lbxExports.SelectedItem, DragDropEffects.Copy);

        }

        private void Listbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lbx = sender as ListBox;
            ListBoxItem lbi = lbx.Items[e.Index] as ListBoxItem;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[lbi.ImageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbi.Name, lbx.Font, new SolidBrush(SystemColors.ButtonHighlight), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                e.Graphics.DrawImage(imageList.Images[lbi.ImageIndex], e.Bounds.X, e.Bounds.Y, lbx.ItemHeight, lbx.ItemHeight);
                e.Graphics.DrawString(lbi.Name, lbx.Font, new SolidBrush(SystemColors.MenuText), e.Bounds.X + lbx.ItemHeight, e.Bounds.Y);

            }

            e.DrawFocusRectangle();

        }
        private void lbxLinkedNodes_MouseDown(object sender, MouseEventArgs e)
        {
            lbxLinkedNodes.Refresh();
            lbxLinkedNodes.DoDragDrop(lbxLinkedNodes.SelectedItem, DragDropEffects.Copy);

        }

        protected IConfiguration conf = new ConfigurationStorage();
        protected IDataObject _DataObject = null;

        protected TreeNode Root = null;

        protected IDataDefinitionExport getIDataDefinitionExport(DragEventArgs e)
        {
            return e.Data.GetData(typeof(IDataDefinitionExportImpl)) as IDataDefinitionExport;
        }
        protected IBaseNode getIBaseNode(DragEventArgs e)
        {
            ListBoxItem lbi = e.Data.GetData(typeof(ListBoxItem)) as ListBoxItem;
            return lbi != null ? lbi.Node : null;
        }
        private void tvModelDatasources_DragEnter(object sender, DragEventArgs e)
        {
            //if (getIDataDefinitionExport(e) != null)
            //{
            //    e.Effect = DragDropEffects.Copy;
            //    return;
            //}

            //if (getIBaseNode(e) != null)
            //{
            //    e.Effect = DragDropEffects.Copy;
            //    return;
            //}

            //e.Effect = DragDropEffects.None;

        }

        private void tvModelDatasources_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode treeNode = null;
            IBaseNode baseNode = null;
            IDataDefinitionExport export = null;

            eDropMode dropMode = DropStrategy(e, ref treeNode, ref baseNode, ref export);
            if (treeNode != null)
            {
                tvModelDatasources.SelectedNode = treeNode;
                switch (dropMode)
                {
                    case eDropMode.DropBaseNode:
                        TreeNode newBaseNode = TreeNodeFromBaseNode(baseNode);
                        Root.Nodes.Add(newBaseNode);

                        tvModelDatasources.SelectedNode = newBaseNode;
                        break;

                    case eDropMode.DropExportMethod:
                    case eDropMode.DropExportVariable:
                        TreeNode newExport = TreeNodeFromDataDefinitionExport(export);

                        tvModelDatasources.SelectedNode.Nodes.Add(newExport);
                        tvModelDatasources.SelectedNode = newExport;
                        break;

                }
            }
        }
        private TreeNode TreeNodeFromBaseNode(IBaseNode baseNode)
        {
            int imageIndex = baseNode is IDomainObject
                ? DOMAINOBJECT_MAPPING
                : VARIABLE_MAPPING;

            TreeNode result = new TreeNode(baseNode.Name);
            result.SelectedImageIndex = result.ImageIndex = imageIndex;
            result.Tag = baseNode;

            return result;
        }
        private TreeNode TreeNodeFromDataDefinitionExport(IDataDefinitionExport export)
        {
            int imageIndex = export.ExportType == eExportType.Variable
                ? EXPORT_VARIABLE_MAPPING
                : EXPORT_METHOD_MAPPING;

            TreeNode result = new TreeNode(export.Name);
            result.SelectedImageIndex = result.ImageIndex = imageIndex;
            result.Tag = export;

            return result;
        }
        private void tvModelDatasources_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                tvModelDatasources.Nodes.Remove(tvModelDatasources.SelectedNode);
                e.Handled = true;

            }
        }
        private void tvModelDatasources_DragOver(object sender, DragEventArgs e)
        {
            TreeNode treeNode = null;
            IBaseNode baseNode = null;
            IDataDefinitionExport export = null;

            eDropMode dropMode = DropStrategy(e, ref treeNode, ref baseNode, ref export);

            e.Effect = DragDropEffects.None;
            if (treeNode != null)
            {
                tvModelDatasources.SelectedNode = treeNode;
                switch (dropMode)
                {
                    case eDropMode.DropBaseNode:
                    case eDropMode.DropExportMethod:
                    case eDropMode.DropExportVariable:
                        e.Effect = DragDropEffects.Copy;
                        break;

                }
            }
        }
        protected enum eDropMode { Undefined, DropBaseNode, DropExportVariable, DropExportMethod }
        protected eDropMode DropStrategy(DragEventArgs e, ref TreeNode treeNode, ref IBaseNode baseNode, ref IDataDefinitionExport export)
        {
            Point targetPoint = tvModelDatasources.PointToClient(new Point(e.X, e.Y));
            treeNode = tvModelDatasources.GetNodeAt(targetPoint);

            if(treeNode != null)
            {
                baseNode = getIBaseNode(e);
                if (baseNode != null)
                {
                    if (TreeNodeContains(Root, baseNode))
                        return  eDropMode.Undefined;

                    return eDropMode.DropBaseNode;

                }

                export = getIDataDefinitionExport(e);
                eDropMode result = eDropMode.Undefined;

                if (export != null)
                {
                    if (isDomainObject(treeNode) && export.ExportType == eExportType.Method)
                        result = eDropMode.DropExportMethod;

                    if (isVariableDef(treeNode) && export.ExportType == eExportType.Variable)
                        result = eDropMode.DropExportVariable;
                }

                TreeNode tn = treeNode.Tag is IDataDefinitionExport
                    ? treeNode.Parent
                    : treeNode;

                if (TreeNodeContains(tn, export))
                    result = eDropMode.Undefined;

                return result;

            }

            return eDropMode.Undefined;

        }
        protected bool TreeNodeContains(TreeNode treeNode, IDataDefinitionExport obj)
        {
            IDataDefinitionExport tag = treeNode.Tag as IDataDefinitionExport;
            if (tag != null && tag.Equals(obj))
                return true;

            foreach (TreeNode node in treeNode.Nodes)
                if (TreeNodeContains(node, obj))
                    return true;

            return false;

        }
        protected bool TreeNodeContains(TreeNode treeNode, IBaseNode obj)
        {
            IBaseNode tag = treeNode.Tag as IBaseNode;
            if (tag != null && tag.Equals(obj))
                return true;

            foreach (TreeNode node in treeNode.Nodes)
                if (TreeNodeContains(node, obj))
                    return true;

            return false;

        }
        protected bool isVariableDef(TreeNode treeNode)
        {
            return treeNode != null &&
                treeNode.Tag != null &&
                treeNode.Tag is IVariableDef;
        }
        protected bool isDomainObject(TreeNode treeNode)
        {
            return treeNode != null &&
                treeNode.Tag != null &&
                treeNode.Tag is IDomainObject;
        }
        private class ListBoxItem
        {
            public ListBoxItem(string name, int imageIndex = 0)
            {
                Name = name;
                ImageIndex = imageIndex;
            }

            public ListBoxItem(IBaseNode node, int imageIndex = 0)
            {
                Name = node.Name;
                ImageIndex = node.NodeType == eNodeType.DomainObject
                    ? 2
                    : 3;

                Node = node;

            }

            public string Name = "";
            public int ImageIndex = 0;
            public IBaseNode Node = null;

            public ListBoxItem Clone()
            {
                return Node != null ? new ListBoxItem(Node, ImageIndex) : new ListBoxItem(Name, ImageIndex);
            }

        }

    }
}
