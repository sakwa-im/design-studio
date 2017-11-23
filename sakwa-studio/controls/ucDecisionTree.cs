using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using log4net;
using configuration;

namespace sakwa
{
    public partial class ucDecisionTree : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ucDecisionTree));

        public enum eDecisionTreeMode { DecisionModel, DomainTemplate}

        public static IBaseNode SelectedBaseNode = null;

        public ModelGUIConfig ModelGUIConfig { get { return modelGUIConfig;} }

        private ModelGUIConfig modelGUIConfig = new ModelGUIConfig();

        public ucDecisionTree(IApplication app, eDecisionTreeMode mode = eDecisionTreeMode.DecisionModel)
        {
            InitializeComponent();

            App = app;
            if (App.ucDecisionModel != null)
            {
                App.ucDecisionModel.SelectedNodeChanged += UcDecisionModel_SelectedNodeChanged;
            }

            Mode = mode;

        }
        public string SelectedPath
        {
            get
            {
                string path = "";
                if (SelectedBaseNode != null)
                {
                    path = SelectedBaseNode.Name;
                    IBaseNode parent = SelectedBaseNode.Parent;
                    while (parent != null)
                    {
                        if (parent.Name != "")
                            path = string.Format("{0}\0{1}", parent.Name, path);

                        parent = parent.Parent;

                    }
                }
                return path;
            }
            set
            {
                string[] pathParts = value.Split(new char[] { '\0' });
                TreeNodeCollection treeNodes = tvDecisionTree.Nodes;
                foreach(string part in pathParts)
                {
                    TreeNode node = FindNode(treeNodes, part);
                    if(node != null)
                    {
                        tvDecisionTree.SelectedNode = node;
                        treeNodes = node.Nodes;

                    }
                }
            }
        }

        private void UcDecisionModel_SelectedNodeChanged(object sender, SelectedNodeEventArgs e)
        {
            TreeNode node = FindNode(tvDecisionTree.Nodes, e.IBaseNode);
            if (node != null)
                tvDecisionTree.SelectedNode = node;

        }

        public new Font Font { get { return tvDecisionTree.Font; }
            set
            {
                tvDecisionTree.Font = value;
                mnuNodes.Font = value;

            }
        }

        public void AddTree(IDecisionTree tree)
        {
            Trees.Add(tree);
            RefreshTree(tvDecisionTree);
            tree.RootNode.UpdatedAndRefresh += UpdateAdRefresh;
            tree.UpdatedAndRefresh += UpdateAdRefresh;
            App.SelectedTree = tree.RootNode.Nodes.Count > 1 ? tree : null;

        }
        public void RemoveTree(IDecisionTree tree)
        {
            bool CanDelete = false;

            if (tree.IsDirty)
                CanDelete = App.GetFloatingForm(
                    eFloatReason.NotSet, 
                    new ucConfirmDropModel(tree, Mode == eDecisionTreeMode.DecisionModel))
                    .ShowDialog() == DialogResult.OK;
            else
                CanDelete = true;

            if (CanDelete)
            {
                Trees.Remove(tree);
                RefreshTree(tvDecisionTree);

                App.SelectedTree = Trees.Count > 0 ? Trees[0] : null;
                App.SelectedObject = Trees.Count > 0 ? Trees[0] : null;

            }
        }

        protected bool RefreshTree(TreeView ctrl)
        {
            ctrl.Nodes.Clear();
            foreach (IDecisionTree tree in Trees)
                ctrl.Nodes.Add(AddTreeNode(null, tree.RootNode));

            return true;
        }
        protected TreeNode AddTreeNode(TreeNode treeNode, IBaseNode baseNode)
        {
            TreeNode newNode = BaseNodeAsTreeNode(baseNode);
            if(treeNode != null)
                treeNode.Nodes.Add(newNode);

            foreach (IBaseNode node in baseNode.Nodes)
                AddTreeNode(newNode, node);

            return newNode;

        }

        private void tvDecisionTree_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                TreeNode currentNode = tvDecisionTree.GetNodeAt(e.Location);

                tvDecisionTree.SelectedNode = currentNode;
                if (currentNode == null)
                    return;

                IBaseNode node = currentNode.Tag != null ? currentNode.Tag as IBaseNode : null;

                string removeText = "";
                string reloadText = "";

                switch (Mode)
                {
                    case eDecisionTreeMode.DecisionModel:
                        removeText = UI_Constants.NodesRemoveTree;
                        reloadText = UI_Constants.NodesReloadTree;
                        headerAdd.Visible = true;
                        headerBulkAdd.Visible = true;
                        mnuNodesAddAssignment.Visible = true;
                        mnuNodesAddChoice.Visible = true;
                        mnuNodesAddDecision.Visible = false;
                        mnuNodesAddDataObject.Visible = false;
                        mnuNodesBulkAddExpressions.Visible = true;
                        mnuNodesBulkAddChoices.Visible = true;
                        break;

                    case eDecisionTreeMode.DomainTemplate:
                        removeText = UI_Constants.NodesRemoveTemplate;
                        reloadText = UI_Constants.NodesReloadTemplate;
                        headerAdd.Visible = false;
                        headerBulkAdd.Visible = false;
                        mnuNodesAddAssignment.Visible = false;
                        mnuNodesAddChoice.Visible = false;
                        mnuNodesAddDataObject.Visible = true;
                        mnuNodesAddDecision.Visible = false;
                        mnuNodesBulkAddExpressions.Visible = false;
                        mnuNodesBulkAddChoices.Visible = false;
                        break;

                }

                mnuNodesRemove.Text = removeText;
                mnuNodesReload.Text = reloadText;

                if (node != null)
                {
                    foreach (ToolStripItem mnuItem in mnuNodes.Items)
                        mnuItem.Enabled = false;

                    foreach (ToolStripItem mnuItem in mnuNodesBulkOperations.DropDownItems)
                        mnuItem.Enabled = false;

                    foreach (eNodeType nodeType in node.AllowedAddNodes)
                        switch(nodeType)
                        {
                            case eNodeType.Expression:
                                mnuNodesAddAssignment.Enabled = true;
                                mnuNodesBulkAddExpressions.Enabled = true;
                                break;

                            case eNodeType.Branch:
                                mnuNodesAddChoice.Enabled = true;
                                mnuNodesBulkAddChoices.Enabled = HasEnumvariables(node);
                                break;

                            case eNodeType.VarDefinition:
                                mnuNodesAddVarDefinition.Enabled = true;
                                break;

                            case eNodeType.DomainObject:
                                mnuNodesAddDomainObjectDefinition.Enabled = true;
                                break;

                            case eNodeType.DataObject:
                                mnuNodesAddDataObject.Enabled = true;
                                break;

                            case eNodeType.DataSource:
                                mnuNodesAddDataSource.Enabled = true;
                                break;

                            case eNodeType.DataDefinition:
                                mnuNodesAddDataDefinition.Enabled = true;
                                break;
                        }


                    switch (node.NodeType)
                    {
                        case eNodeType.Root:
                            mnuNodesRemove.Text = removeText;
                            mnuNodesRemove.Enabled = true;
                            mnuNodesReload.Text = reloadText;
                            mnuNodesReload.Enabled = true;
                            break;

                        case eNodeType.VariableDefinitions:
                            mnuNodesImport.Enabled = true;
                            mnuNodesRemove.Text = UI_Constants.NodesCantRemove;
                            break;

                        case eNodeType.VarDefinition:
                            mnuNodesRemove.Text = UI_Constants.NodesRemoveVariable;
                            mnuNodesRemove.Enabled = true;
                            break;

                        case eNodeType.DomainObject:
                            mnuNodesRemove.Text = UI_Constants.NodesRemoveDomainObject;
                            mnuNodesRemove.Enabled = true;
                            break;

                        case eNodeType.Expression:
                            mnuNodesRemove.Text = UI_Constants.NodesRemoveAssignment;
                            mnuNodesRemove.Enabled = true;
                            break;

                        case eNodeType.DataObjects:
                        case eNodeType.DataSources:
                            mnuNodesRemove.Text = UI_Constants.NodesCantRemove;
                            break;

                        case eNodeType.DataObject:
                            mnuNodesRemove.Text = UI_Constants.NodesRemoveDataObject;
                            mnuNodesRemove.Enabled = true;
                            break;

                        case eNodeType.DataSource:
                            mnuNodesRemove.Text = UI_Constants.NodesRemoveDataSource;
                            mnuNodesRemove.Enabled = true;
                            break;

                        case eNodeType.DataDefinition:
                            mnuNodesRemove.Text = UI_Constants.NodesRemoveDataDefinition;
                            mnuNodesRemove.Enabled = true;
                            break;

                        default:
                            mnuNodesRemove.Text = UI_Constants.NodesRemoveNode;
                            mnuNodesRemove.Enabled = true;
                            break;
                    }

                    mnuNodesExpand.Enabled = true;
                    mnuNodesCollapse.Enabled = currentNode.IsExpanded;

                    mnuNodesBulkOperations.Enabled = 
                        mnuNodesBulkAddExpressions.Enabled || 
                        mnuNodesBulkAddChoices.Enabled ||
                        mnuNodesImport.Enabled;

                    mnuNodes.Show(tvDecisionTree.PointToScreen(e.Location));

                }
            }
        }
        protected bool HasEnumvariables(IBaseNode node)
        {
            IBaseNode variables = node.Tree.RootNode.GetNode(eNodeType.VariableDefinitions);
            foreach (IBaseNode var in variables.Nodes)
                if (var is IVariableDef && (var as IVariableDef).VariableType == eVariableType.enumeration)
                    return true;

            return false;
        }

        private void mnuNodesAdd_Click(object sender, EventArgs e)
        {
            ToolStripItem mnuItem = sender as ToolStripItem;
            int index = Convert.ToInt32(mnuItem.Tag);
            switch(index)
            {
                //case 1: AddNode(eNodeType.Decision); break;
                case 2: AddNode(eNodeType.Branch); break;
                case 3: AddBulkBranches();  break;
                case 4: AddNode(eNodeType.Expression); break;
                case 5: AddNode(eNodeType.VarDefinition); break;
                case 6: AddNode(eNodeType.DomainObject); break;
                case 7: AddNode(eNodeType.DataObject); break;
                case 8: AddBulkAssignments(); break;
                case 9: AddNode(eNodeType.DataSource); break;
                case 10: AddNode(eNodeType.DataDefinition); break;
            }
        }
        private void mnuNodesImport_Click(object sender, EventArgs e)
        {
            IBaseNode currentNode = TreeNodeAsBaseNode(tvDecisionTree.SelectedNode);
            if (currentNode != null)
            {
                BulkImportForm frm = new BulkImportForm(currentNode, App);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    currentNode.Nodes.Clear();
                    foreach (IBaseNode baseNode in frm.ImportVariables)
                        currentNode.AddNode(baseNode);

                    tvDecisionTree.SelectedNode.Nodes.Clear();
                    RefreshTree(tvDecisionTree.SelectedNode, currentNode);

                }
            }
        }

        private void mnuNodesRemove_Click(object sender, EventArgs e)
        {
            RemoveNode();
        }
        private void mnuNodesReload_Click(object sender, EventArgs e)
        {
            IBaseNode currentNode = TreeNodeAsBaseNode(tvDecisionTree.SelectedNode);
            if (currentNode != null)
            {
                IDecisionTree tree = currentNode.Tree;

                bool CanDelete = false;

                if (tree.IsDirty)
                    CanDelete = App.GetFloatingForm(eFloatReason.NotSet, new ucConfirmDropModel(tree)).ShowDialog() == DialogResult.OK;
                else
                    CanDelete = true;

                if (CanDelete)
                {
                    int treeIndex = Trees.IndexOf(tree);
                    IDecisionTree newTree = App.NewDescisionTree();
                    newTree.Load(tree.Persistence.Clone(), tree.FullPath);

                    Trees.Remove(tree);
                    Trees.Insert(treeIndex, newTree);
                    RefreshTree(tvDecisionTree);

                    App.SelectedTree = Trees.Count > 0 ? Trees[0] : null;

                    App.SelectedObject = Trees.Count > 0 ? Trees[0] : null;

                }
            }
        }

        private void AddNode(eNodeType nodeType)
        {
            IBaseNode currentNode = TreeNodeAsBaseNode(tvDecisionTree.SelectedNode);
            IDecisionTree tree = currentNode.Tree;
            if (tree != null)
            {
                IBaseNode newNode = tree.CreateNewNode(nodeType, currentNode);
                newNode.Updated += Node_Updated;
                if (newNode.NodeType == eNodeType.VarDefinition)
                    (newNode as IVariableDef).VariableTypeChanged += VariableTypeChanged;

                if (newNode.NodeType == eNodeType.DomainObject)
                    (newNode as IDomainObject).UpdatedAndRefresh += UpdateAdRefresh;
                
                TreeNode node = modelGUIConfig.BaseNodeAsTreeNode(newNode); 

                tvDecisionTree.SelectedNode.Nodes.Add(node);
                tvDecisionTree.SelectedNode.Expand();
                tvDecisionTree.SelectedNode = node;

            }

            App.SelectedTree = tree;

        }
        private void AddBulkBranches()
        {
            IBaseNode currentNode = TreeNodeAsBaseNode(tvDecisionTree.SelectedNode);
            IDecisionTree tree = currentNode.Tree;
            if (tree != null)
            {
                BulkBranchForm frm = new sakwa.BulkBranchForm(tree.RootNode.GetNode(eNodeType.VariableDefinitions));
                if(frm.ShowDialog(this) == DialogResult.OK)
                {
                    string[] elems = frm.SelectedValues;
                    foreach(string elem in elems)
                    {
                        IBaseNode newNode = tree.CreateNewNode(eNodeType.Branch, currentNode, elem);
                        newNode.Updated += Node_Updated;

                        TreeNode node = modelGUIConfig.BaseNodeAsTreeNode(newNode);

                        tvDecisionTree.SelectedNode.Nodes.Add(node);
                        tvDecisionTree.SelectedNode.Expand();

                    }
                }
            }

            App.SelectedTree = tree;

        }
        private void AddBulkAssignments()
        {
            IBaseNode currentNode = TreeNodeAsBaseNode(tvDecisionTree.SelectedNode);
            IDecisionTree tree = currentNode.Tree;
            if (tree != null)
            {
                BulkExpressionForm frm = new BulkExpressionForm(tree.RootNode.GetNode(eNodeType.VariableDefinitions));
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (IVariableDef assignment in frm.SelectedValues)
                    {
                        IBaseNode newNode = tree.CreateNewNode(eNodeType.Expression, currentNode, assignment.Value);
                        if (assignment.Parent != null && assignment.Parent.NodeType == eNodeType.DomainObject)
                            (newNode as IExpression).Domain = assignment.Parent as IDomainObject;

                        (newNode as IExpression).Variable = assignment;

                        newNode.Updated += Node_Updated;

                        TreeNode node = modelGUIConfig.BaseNodeAsTreeNode(newNode);

                        tvDecisionTree.SelectedNode.Nodes.Add(node);
                        tvDecisionTree.SelectedNode.Expand();

                    }
                }
            }

            App.SelectedTree = tree;

        }
        private void RemoveNode()
        {
            List<TreeNode> removeNodes = new List<TreeNode>();
            foreach (TreeNode n in tvDecisionTree.SelectedNodes)
                removeNodes.Add(n);

            while(removeNodes.Count > 0)
            {
                TreeNode n = removeNodes[0];
                removeNodes.Remove(n);
                IBaseNode node = TreeNodeAsBaseNode(n);

                switch (node.NodeType)
                {
                    case eNodeType.VarDefinition:
                        node.Tree.RemoveVariable(node);
                        App.SelectedTree = node.Tree.RootNode.Nodes.Count > 1 ? node.Tree : null;

                        RemoveAndSelect(n);
                        break;

                    case eNodeType.Root:
                        RemoveTree(node.Tree);
                        break;

                    case eNodeType.VariableDefinitions:
                        break;

                    default:
                        node.Parent.RemoveNode(node);
                        App.SelectedTree = node.Tree.RootNode.Nodes.Count > 1 ? node.Tree : null;

                        RemoveAndSelect(n);
                        break;
                }
            }
        }
        private void RemoveVarDefinition()
        {
            IBaseNode node = TreeNodeAsBaseNode(tvDecisionTree.SelectedNode);
            switch(node.NodeType)
            {
                case eNodeType.VarDefinition:
                    break;
            }

            RemoveAndSelect(tvDecisionTree.SelectedNode);

        }
        private void tvDecisionTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveNode();
                e.Handled = true;

            }

            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Right)
            {
                mnuNodesExpand_Click(sender, new EventArgs());
                e.Handled = true;

            }

            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Left)
            {
                mnuNodesCollapse_Click(sender, new EventArgs());
                e.Handled = true;

            }
        }
        private TreeNode RemoveAndSelect(TreeNode node)
        {
            TreeNode parent = node.Parent;
            if(parent != null)
            {
                if(parent.Nodes.Count == 1)
                {
                    parent.Nodes.Clear();
                    tvDecisionTree.SelectedNode = parent;
                    if (App != null)
                        App.SelectedObject = parent.Tag;
                }
                else
                {
                    int index = parent.Nodes.IndexOf(node);
                    parent.Nodes.RemoveAt(index);
                    tvDecisionTree.SelectedNode = parent.Nodes[Math.Min(index, parent.Nodes.Count - 1)];

                }
            }
            return node;

        }

        private void tvDecisionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IBaseNode node = TreeNodeAsBaseNode(e.Node);
            SelectedBaseNode = node;
            if (node != null)
            {
                switch (node.NodeType)
                {
                    case eNodeType.VarDefinition:
                        if (App != null)
                            App.SelectedObject = node;

                        break;

                    default:
                        App.SelectedObject = node;
                        if(App.SelectedTree != node.Tree)
                            App.SelectedTree = node.Tree.RootNode.Nodes.Count > 1 ? node.Tree : null;
                        break;
                }
            }
            else
                if (App != null)
                    App.SelectedObject = null;

        }

        private void Node_Updated(object sender, EventArgs e)
        {
            IBaseNode node = sender as IBaseNode;
            if (tvDecisionTree.SelectedNode.Tag == node)
            {
                tvDecisionTree.SelectedNode.Text = node.Name;
                int imageIndex = modelGUIConfig.NodeImageIndex(node);
                tvDecisionTree.SelectedNode.ImageIndex = imageIndex;
                tvDecisionTree.SelectedNode.SelectedImageIndex = imageIndex;
            }

            if (App != null)
                App.SelectedObject = node;

            App.SelectedTree = node.Tree;

        }
        private void VariableTypeChanged(object sender, EventArgs e)
        {
            IBaseNode node = TreeNodeAsBaseNode(tvDecisionTree.SelectedNode);
            if(node != null)
            {
                IVariableDef oldVariable = sender as IVariableDef;
                IVariableDef newVariable = node.Tree.ChangeVariable(oldVariable);

                if (newVariable != null)
                {
                    (newVariable as IBaseNode).Updated += Node_Updated;
                    newVariable.VariableTypeChanged += VariableTypeChanged;

                    tvDecisionTree.SelectedNode.Tag = newVariable;

                }

                if (App != null)
                    App.SelectedObject = newVariable;

                oldVariable = null;

            }
        }


        private void UpdateAdRefresh(object sender, EventArgs e)
        {
            IBaseNode baseNode = sender as IBaseNode;
            if (baseNode != null)
            {
                TreeNode node = this.FindNode(tvDecisionTree.Nodes, baseNode);
                if(node != null)
                {
                    node.Nodes.Clear();
                    RefreshTree(node, baseNode);
                    node.Expand();

                    node.ImageIndex = modelGUIConfig.NodeImageIndex(baseNode);
                    node.SelectedImageIndex = modelGUIConfig.NodeImageIndex(baseNode);

                }
            }
        }

        private void RefreshTree(TreeNode node, IBaseNode baseNode)
        {
            foreach(IBaseNode bn in baseNode.Nodes)
            {
                TreeNode newNode = BaseNodeAsTreeNode(bn);
                node.Nodes.Add(newNode);

                RefreshTree(newNode, bn);

            }
        }

        protected IBaseNode TreeNodeAsBaseNode(TreeNode node)
        {
            return node.Tag != null ? node.Tag as IBaseNode : null;
        }
        protected TreeNode BaseNodeAsTreeNode(IBaseNode node)
        {
            TreeNode result = new TreeNode(node.Name);
            result.Tag = node;
            modelGUIConfig.ConfigureTreeNode(result);

            node.Updated += Node_Updated;
            if(node.NodeType == eNodeType.VarDefinition)
                (node as IVariableDef).VariableTypeChanged += VariableTypeChanged;

            if(node.NodeType == eNodeType.DomainObject)
                (node as IBaseNode).UpdatedAndRefresh += UpdateAdRefresh;

            if (node.NodeType == eNodeType.Root)
                (node as IBaseNode).UpdatedAndRefresh += UpdateAdRefresh;

            return result;

        }

        protected TreeNode TreeNodeByName(string name)
        {
            TreeNode[] nodes = tvDecisionTree.Nodes.Find(name, true);
            return nodes.Length > 0 ? nodes[0] : null;

        }
        protected TreeNode TreeNodeByBaseNode(IBaseNode baseNode)
        {
            if (baseNode != null)
                return FindNode(tvDecisionTree.Nodes, baseNode);

            return null;

        }
        protected TreeNode FindNode(TreeNodeCollection treeNodes, IBaseNode baseNode)
        {
            TreeNode result = null;
            foreach (TreeNode node in treeNodes)
            {
                if (node.Tag == baseNode)
                    return node;

                if (node.Nodes.Count > 0)
                    result = FindNode(node.Nodes, baseNode);

                if (result != null)
                    return result;

            }

            return null;

        }
        protected TreeNode FindNode(TreeNodeCollection treeNodes, string name)
        {
            TreeNode result = null;
            foreach (TreeNode node in treeNodes)
            {
                if (node.Text == name)
                    return node;

                if (node.Nodes.Count > 0)
                    result = FindNode(node.Nodes, name);

                if (result != null)
                    return result;

            }

            return null;

        }

        private void mnuNodesExpand_Click(object sender, EventArgs e)
        {
            if (tvDecisionTree.SelectedNode != null)
            {
                tvDecisionTree.SuspendLayout();
                tvDecisionTree.SelectedNode.ExpandAll();
                tvDecisionTree.ResumeLayout();

            }
        }

        private void mnuNodesCollapse_Click(object sender, EventArgs e)
        {
            if (tvDecisionTree.SelectedNode != null)
            {
                tvDecisionTree.SuspendLayout();
                tvDecisionTree.SelectedNode.Collapse();
                tvDecisionTree.ResumeLayout();

            }
        }

        private IBaseNode draggedBaseNode = null;
        private void tvDecisionTree_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeNode draggedNode = (TreeNode) e.Data.GetData(typeof(TreeNode));
                draggedBaseNode = this.TreeNodeAsBaseNode(draggedNode);

                e.Effect = draggedBaseNode != null ? e.AllowedEffect : DragDropEffects.None;

                return;

            }

            if(IsModelDrop(e))
            {
                e.Effect = e.AllowedEffect;

            }
        }
        private bool IsModelDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach(string fileName in fileNames)
                {
                    if (fileName.EndsWith(UI_Constants.ModelExtension) && File.Exists(fileName))
                        return true;
                    
                }
            }

            return false;

        }

        private void tvDecisionTree_DragDrop(object sender, DragEventArgs e)
        {
            if (IsModelDrop(e))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach(string fileName in fileNames)
                { 
                    if (fileName.EndsWith(UI_Constants.ModelExtension) && File.Exists(fileName))
                        App.LoadModel(fileName);

                }
            }

            Point targetPoint = tvDecisionTree.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = tvDecisionTree.GetNodeAt(targetPoint);
            IBaseNode targetBaseNode = TreeNodeAsBaseNode(targetNode);

            List<TreeNode> selectedNodes = new List<TreeNode>(tvDecisionTree.DragedNodes.ToArray());

            foreach (TreeNode draggedNode in selectedNodes)
            {
                if (!draggedNode.Equals(targetNode))
                {
                    // If it is a move operation, remove the node from its current 
                    // location and add it to the node at the drop location.
                    if (e.Effect == DragDropEffects.Link)
                    {
                        draggedNode.Remove();
                        if (targetNode.Parent != null)
                            targetNode.Parent.Nodes.Add(draggedNode);
                        else
                            targetNode.Nodes.Insert(0, draggedNode);

                        draggedBaseNode.Parent.RemoveNode(draggedBaseNode);

                        if (targetBaseNode.Parent != targetBaseNode.Tree)
                        {
                            int index = targetBaseNode.Parent.Nodes.IndexOf(targetBaseNode);
                            if (index > 0)
                                targetBaseNode.Parent.Nodes.Insert(index, draggedBaseNode);
                            else
                                targetBaseNode.Parent.Nodes.Add(draggedBaseNode);
                        }
                        else
                            targetBaseNode.Tree.RootNode.Nodes.Add(draggedBaseNode);
                    }

                    if (e.Effect == DragDropEffects.Move)
                    {
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);

                        draggedBaseNode.Parent.RemoveNode(draggedBaseNode);
                        draggedBaseNode.Parent = targetBaseNode;
                        targetBaseNode.Nodes.Add(draggedBaseNode);

                        targetNode.Expand();

                    }

                    // If it is a copy operation, clone the dragged node 
                    // and add it to the node at the drop location.
                    else if (e.Effect == DragDropEffects.Copy)
                    {
                        TreeNode newNode = draggedNode.Clone() as TreeNode;
                        targetNode.Nodes.Add(newNode);
                        CloneBaseNodes(newNode);

                    }
                }
            } //foreach (TreeNode draggedNode in selectedNodes)

            //tvDecisionTree.SelectedNode = draggedNode;
            App.SelectedTree = targetBaseNode.Tree;

            draggedBaseNode = null;

        }
        private void CloneBaseNodes(TreeNode node)
        {
            IBaseNode parentBaseNode = TreeNodeAsBaseNode(node.Parent);
            IBaseNode newBaseNode = TreeNodeAsBaseNode(node).Clone();

            node.Tag = newBaseNode;
            newBaseNode.Parent = parentBaseNode;
            parentBaseNode.Nodes.Add(newBaseNode);

            foreach (TreeNode n in node.Nodes)
                CloneBaseNodes(n);

        }

        private void tvDecisionTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                DoDragDrop(e.Item, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);

            }

        private void tvDecisionTree_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = tvDecisionTree.PointToClient(new Point(e.X, e.Y));
            TreeNode node = tvDecisionTree.GetNodeAt(targetPoint);

            if (node != null)
            {
                e.Effect = DragDropEffects.None;

                tvDecisionTree.SelectedNode = node;

                IBaseNode targetBaseNode = TreeNodeAsBaseNode(tvDecisionTree.SelectedNode);
                if (targetBaseNode != null && draggedBaseNode.NodeType != eNodeType.Root)
                {
                    switch (Control.ModifierKeys)
                    {
                        case Keys.Alt:
                            if (targetBaseNode.Parent.AllowedAddNodes.Contains(draggedBaseNode.NodeType))
                                e.Effect = DragDropEffects.Link;
                            break;

                        case Keys.Control:
                            if (targetBaseNode.AllowedAddNodes.Contains(draggedBaseNode.NodeType))
                                e.Effect = DragDropEffects.Copy;
                            break;

                        default:
                            if (targetBaseNode.AllowedAddNodes.Contains(draggedBaseNode.NodeType))
                                e.Effect = DragDropEffects.Move;
                            break;
                    }
                }

                return;

            }

            if (IsModelDrop(e))
            {
                e.Effect = DragDropEffects.Copy;
                return;

            }

            e.Effect = DragDropEffects.None;

        }

        private void ucDecisionTree_Load(object sender, EventArgs e)
        {
            modelGUIConfig.Load();
            modelGUIConfig.OnUpdated += ModelGUIConig_Updated;
        }
        public void ModelGUIConig_Updated(object sender, EventArgs e)
        {
            foreach (TreeNode node in tvDecisionTree.Nodes)
                UpdateTreeNodes(node);
        }

        protected void UpdateTreeNodes(TreeNode parentNode)
        {
            ModelGUIConfig.ConfigureTreeNode(parentNode);
            foreach (TreeNode node in parentNode.Nodes)
                UpdateTreeNodes(node);

        }

        private void tvDecisionTree_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if ((e.Effect & (DragDropEffects.Copy | DragDropEffects.Link)) == (DragDropEffects.Copy | DragDropEffects.Link))
            {
                e.UseDefaultCursors = false;
                Cursor.Current = Cursors.Cross;
            }
            else
            {
                e.UseDefaultCursors = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void tvDecisionTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                // Draw the background of the selected node. The NodeBounds
                // method makes the highlight rectangle large enough to
                // include the text of a node tag, if one is present.
                Rectangle rect = NodeBounds(e.Node);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 51, 153, 255)), rect);

                // Retrieve the node font. If the node font has not been set,
                // use the TreeView font.
                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;

                // Draw the node text.
                IBaseNode baseNode = TreeNodeAsBaseNode(e.Node);
                e.Graphics.DrawString(baseNode.Name, nodeFont, Brushes.White, rect);

            }

            // Use the default background and node text.
            else
            {
                e.DrawDefault = true;
            }

        }
        // Returns the bounds of the specified node, including the region 
        // occupied by the node label and any node tag displayed.
        private Rectangle NodeBounds(TreeNode node)
        {
            // Set the return value to the normal node bounds.
            Rectangle bounds = node.Bounds;
            if (node.Tag != null)
            {
                IBaseNode baseNode = TreeNodeAsBaseNode(node);
                // Retrieve a Graphics object from the TreeView handle
                // and use it to calculate the display width of the tag.
                Graphics g = tvDecisionTree.CreateGraphics();
                int tagWidth = (int)g.MeasureString(baseNode.Name, tvDecisionTree.Font).Width + 6;

                // Adjust the node bounds using the calculated value.
                bounds.Offset(tagWidth / 2, 0);
                bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);
                g.Dispose();
            }

            return bounds;

        }

        protected string FullPath = "";
        public List<IDecisionTree> Trees = new List<IDecisionTree>();
        protected List<IVariableDef> Variables = new List<IVariableDef>();

        protected IApplication App = null;
        protected eDecisionTreeMode Mode = eDecisionTreeMode.DecisionModel;

    } //class ucDecisionTree

    public class ModelGUIConfig
    {
        public ModelGUIConfig()
        {
            for(int i = 0; i < ForeColors.Length; i++)
                ForeColors[i] = Color.Black;

            for (int i = 0; i < BackColors.Length; i++)
                BackColors[i] = Color.White;
        }

        public Color[] ForeColors = new Color[13];
        public Color[] BackColors = new Color[13];

        public EventHandler OnUpdated;
        public void Load()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            if (conf.GetConfigurationItem(UI_Constants.ForeColor1) == null)
                DefineConfiguration();

            for (int i = 1; i < ForeColors.Length; i++)
            {
                IConfigurationItem color = null;

                color = conf.GetConfigurationItem(UI_Constants.ForeColor + i.ToString());
                ForeColors[i] = (color as IConfigurationItemObject<Color>).GetValue(Color.Black);

                color = conf.GetConfigurationItem(UI_Constants.BackColor + i.ToString());
                BackColors[i] = (color as IConfigurationItemObject<Color>).GetValue(Color.White);
            }
        }
        public void Save()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            if (conf.GetConfigurationItem(UI_Constants.ForeColor1) == null)
                DefineConfiguration();

            for (int i = 1; i < ForeColors.Length; i++)
            {
                IConfigurationItem color = null;

                color = conf.GetConfigurationItem(UI_Constants.ForeColor + i.ToString());
                (color as IConfigurationItemObject<Color>).SetValue(ForeColors[i]);

                color = conf.GetConfigurationItem(UI_Constants.BackColor + i.ToString());
                (color as IConfigurationItemObject<Color>).SetValue(BackColors[i]);

            }

            if (OnUpdated != null)
                OnUpdated(this, new EventArgs());

        }
        protected void DefineConfiguration()
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            for (int i = 1; i < ForeColors.Length; i++)
            {
                IConfigurationItemObject<Color> color = null;

                color = new ConfigurationItemObject<Color>(UI_Constants.ForeColor + i.ToString(), Color.Black, UI_Constants.ConfigurationSource);
                conf.AddConfigurationItem("", color as IConfigurationItem);

                color = new ConfigurationItemObject<Color>(UI_Constants.BackColor + i.ToString(), Color.White, UI_Constants.ConfigurationSource);
                conf.AddConfigurationItem("", color as IConfigurationItem);

            }
        }

        public TreeNode ConfigureTreeNode(TreeNode node)
        {
            IBaseNode baseNode = TreeNodeAsBaseNode(node);
            if (baseNode != null)
            {
                int index = NodeIndex(baseNode.NodeType);
                switch (baseNode.NodeType)
                {
                    case eNodeType.VariableDefinitions:
                        node.ToolTipText = UI_Constants.VariableTreeName;
                        break;

                    case eNodeType.DataObjects:
                        node.ToolTipText = UI_Constants.DataNodesTreeName;
                        break;

                    case eNodeType.Root:
                        node.ToolTipText = baseNode.Tree.FullPath != ""
                            ? Path.GetFileName(baseNode.Tree.FullPath)
                            : baseNode.NodeType.ToString();
                        break;

                    default:
                        node.ToolTipText = baseNode.NodeType.ToString();
                        break;

                }

                node.ImageIndex = NodeImageIndex(baseNode);
                node.SelectedImageIndex = NodeImageIndex(baseNode);

                node.BackColor = BackColors[index];
                node.ForeColor = ForeColors[index];
            }

            return node;

        }

        public static IBaseNode TreeNodeAsBaseNode(TreeNode node)
        {
            return node.Tag != null
                ? node.Tag as IBaseNode
                : null;
        }
        public TreeNode BaseNodeAsTreeNode(IBaseNode node)
        {
            TreeNode result = new TreeNode(node.Name);
            result.Name = node.Reference;
            result.Tag = node;
            ConfigureTreeNode(result);

            return result;

        }

        public int NodeIndex(eNodeType nodeType)
        {
            int result = 0;
            switch(nodeType)
            {
                case eNodeType.Root:                result = 1; break;
                case eNodeType.VariableDefinitions: result = 2; break;
                case eNodeType.VarDefinition:       result = 3; break;
                case eNodeType.Expression:          result = 4; break;
                case eNodeType.DomainObject:        result = 5; break;
                case eNodeType.Branch:              result = 6; break;
                case eNodeType.DataObject:          result = 8; break;
                case eNodeType.DataObjects:         result = 9; break;
                case eNodeType.DataSources:         result = 10; break;
                case eNodeType.DataSource:          result = 11; break;
                case eNodeType.DataDefinition:      result = 12; break;
            }

            return result;
        }

        public int NodeImageIndex(IBaseNode baseNode)
        {
            int result = 0;
            switch (baseNode.NodeType)
            {
                case eNodeType.Root:                result = 1; break;
                case eNodeType.VariableDefinitions: result = 2; break;
                case eNodeType.VarDefinition:       result = 3; break;
                case eNodeType.Expression:          result = 4; break;
                case eNodeType.DomainObject:        result = baseNode.ReadOnly ? 6 : 5; break;
                case eNodeType.Branch:
                    result = (baseNode as IBranch).BranchEvaluation == BranchEvaluation.Once 
                        ? 7
                        : 8;
                    break;
                case eNodeType.DataObject:          result = 9; break;
                case eNodeType.DataObjects:         result = 10; break;
                case eNodeType.DataSources:         result = 11; break;
                case eNodeType.DataSource:          result = 12; break;
                case eNodeType.DataDefinition:      result = 13; break;
            }

            return result;
        }

    } //class ModelGUIConfig

}
