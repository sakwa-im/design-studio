namespace sakwa
{
    partial class ucDecisionTree
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDecisionTree));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.mnuNodes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.headerAdd = new sakwa.ToolstripMenuHeader();
            this.mnuNodesAddDecision = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesAddChoice = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesAddAssignment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesAddVarDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesAddDomainObjectDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesAddDataObject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesAddDataSource = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesAddDataDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesBulkOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.headerBulkAdd = new sakwa.ToolstripMenuHeader();
            this.mnuNodesBulkAddChoices = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesBulkAddExpressions = new System.Windows.Forms.ToolStripMenuItem();
            this.headerBulkMiscellaneous = new sakwa.ToolstripMenuHeader();
            this.mnuNodesImport = new System.Windows.Forms.ToolStripMenuItem();
            this.headerRemove = new sakwa.ToolstripMenuHeader();
            this.mnuNodesRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.headerMiscellaneous = new sakwa.ToolstripMenuHeader();
            this.mnuNodesExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNodesReload = new System.Windows.Forms.ToolStripMenuItem();
            this.blaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvDecisionTree = new sakwa.SakwaTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mnuNodes.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "blank-48x48.png");
            this.imageList.Images.SetKeyName(1, "model-root-48x48.png");
            this.imageList.Images.SetKeyName(2, "variable-definitions-48x48.png");
            this.imageList.Images.SetKeyName(3, "variable-definition-48x48.png");
            this.imageList.Images.SetKeyName(4, "assignment-48x48.png");
            this.imageList.Images.SetKeyName(5, "decision-48x48.png");
            this.imageList.Images.SetKeyName(6, "decision-48x48-linked.png");
            this.imageList.Images.SetKeyName(7, "Choice-48x48.png");
            this.imageList.Images.SetKeyName(8, "Choices-48x48.png");
            this.imageList.Images.SetKeyName(9, "connection-48x48.png");
            this.imageList.Images.SetKeyName(10, "connections-48x48.png");
            this.imageList.Images.SetKeyName(11, "sources-48x48.png");
            this.imageList.Images.SetKeyName(12, "source-48x48.png");
            this.imageList.Images.SetKeyName(13, "definition-48x48.png");
            // 
            // mnuNodes
            // 
            this.mnuNodes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuNodes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuNodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.headerAdd,
            this.mnuNodesAddDecision,
            this.mnuNodesAddChoice,
            this.mnuNodesAddAssignment,
            this.mnuNodesAddVarDefinition,
            this.mnuNodesAddDomainObjectDefinition,
            this.mnuNodesAddDataObject,
            this.mnuNodesAddDataSource,
            this.mnuNodesAddDataDefinition,
            this.mnuNodesBulkOperations,
            this.headerRemove,
            this.mnuNodesRemove,
            this.headerMiscellaneous,
            this.mnuNodesExpand,
            this.mnuNodesCollapse,
            this.mnuNodesReload});
            this.mnuNodes.Name = "mnuRoot";
            this.mnuNodes.Size = new System.Drawing.Size(349, 544);
            // 
            // headerAdd
            // 
            this.headerAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.headerAdd.Enabled = false;
            this.headerAdd.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Bold);
            this.headerAdd.ForeColor = System.Drawing.Color.DimGray;
            this.headerAdd.Name = "headerAdd";
            this.headerAdd.Size = new System.Drawing.Size(348, 32);
            this.headerAdd.Text = "Add";
            // 
            // mnuNodesAddDecision
            // 
            this.mnuNodesAddDecision.Image = ((System.Drawing.Image)(resources.GetObject("mnuNodesAddDecision.Image")));
            this.mnuNodesAddDecision.Name = "mnuNodesAddDecision";
            this.mnuNodesAddDecision.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesAddDecision.Tag = "1";
            this.mnuNodesAddDecision.Text = "Decision";
            this.mnuNodesAddDecision.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // mnuNodesAddChoice
            // 
            this.mnuNodesAddChoice.Image = ((System.Drawing.Image)(resources.GetObject("mnuNodesAddChoice.Image")));
            this.mnuNodesAddChoice.Name = "mnuNodesAddChoice";
            this.mnuNodesAddChoice.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesAddChoice.Tag = "2";
            this.mnuNodesAddChoice.Text = "Branch";
            this.mnuNodesAddChoice.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // mnuNodesAddAssignment
            // 
            this.mnuNodesAddAssignment.Image = ((System.Drawing.Image)(resources.GetObject("mnuNodesAddAssignment.Image")));
            this.mnuNodesAddAssignment.Name = "mnuNodesAddAssignment";
            this.mnuNodesAddAssignment.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesAddAssignment.Tag = "4";
            this.mnuNodesAddAssignment.Text = "Expression";
            this.mnuNodesAddAssignment.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // mnuNodesAddVarDefinition
            // 
            this.mnuNodesAddVarDefinition.Image = ((System.Drawing.Image)(resources.GetObject("mnuNodesAddVarDefinition.Image")));
            this.mnuNodesAddVarDefinition.Name = "mnuNodesAddVarDefinition";
            this.mnuNodesAddVarDefinition.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesAddVarDefinition.Tag = "5";
            this.mnuNodesAddVarDefinition.Text = "Variable definition";
            this.mnuNodesAddVarDefinition.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // mnuNodesAddDomainObjectDefinition
            // 
            this.mnuNodesAddDomainObjectDefinition.Image = ((System.Drawing.Image)(resources.GetObject("mnuNodesAddDomainObjectDefinition.Image")));
            this.mnuNodesAddDomainObjectDefinition.Name = "mnuNodesAddDomainObjectDefinition";
            this.mnuNodesAddDomainObjectDefinition.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesAddDomainObjectDefinition.Tag = "6";
            this.mnuNodesAddDomainObjectDefinition.Text = "Domain object";
            this.mnuNodesAddDomainObjectDefinition.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // mnuNodesAddDataObject
            // 
            this.mnuNodesAddDataObject.Image = ((System.Drawing.Image)(resources.GetObject("mnuNodesAddDataObject.Image")));
            this.mnuNodesAddDataObject.Name = "mnuNodesAddDataObject";
            this.mnuNodesAddDataObject.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesAddDataObject.Tag = "7";
            this.mnuNodesAddDataObject.Text = "Data object";
            this.mnuNodesAddDataObject.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // mnuNodesAddDataSource
            // 
            this.mnuNodesAddDataSource.Name = "mnuNodesAddDataSource";
            this.mnuNodesAddDataSource.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesAddDataSource.Tag = "9";
            this.mnuNodesAddDataSource.Text = "Data source";
            this.mnuNodesAddDataSource.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // mnuNodesAddDataDefinition
            // 
            this.mnuNodesAddDataDefinition.Name = "mnuNodesAddDataDefinition";
            this.mnuNodesAddDataDefinition.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesAddDataDefinition.Tag = "10";
            this.mnuNodesAddDataDefinition.Text = "Data definition";
            this.mnuNodesAddDataDefinition.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // mnuNodesBulkOperations
            // 
            this.mnuNodesBulkOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.headerBulkAdd,
            this.mnuNodesBulkAddChoices,
            this.mnuNodesBulkAddExpressions,
            this.headerBulkMiscellaneous,
            this.mnuNodesImport});
            this.mnuNodesBulkOperations.Name = "mnuNodesBulkOperations";
            this.mnuNodesBulkOperations.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesBulkOperations.Text = "Bulk operations";
            // 
            // headerBulkAdd
            // 
            this.headerBulkAdd.BackColor = System.Drawing.SystemColors.ControlLight;
            this.headerBulkAdd.Enabled = false;
            this.headerBulkAdd.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Bold);
            this.headerBulkAdd.ForeColor = System.Drawing.Color.DimGray;
            this.headerBulkAdd.Name = "headerBulkAdd";
            this.headerBulkAdd.Size = new System.Drawing.Size(220, 32);
            this.headerBulkAdd.Text = "Add";
            // 
            // mnuNodesBulkAddChoices
            // 
            this.mnuNodesBulkAddChoices.Image = ((System.Drawing.Image)(resources.GetObject("mnuNodesBulkAddChoices.Image")));
            this.mnuNodesBulkAddChoices.Name = "mnuNodesBulkAddChoices";
            this.mnuNodesBulkAddChoices.Size = new System.Drawing.Size(220, 32);
            this.mnuNodesBulkAddChoices.Tag = "3";
            this.mnuNodesBulkAddChoices.Text = "Branches";
            this.mnuNodesBulkAddChoices.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // mnuNodesBulkAddExpressions
            // 
            this.mnuNodesBulkAddExpressions.Image = ((System.Drawing.Image)(resources.GetObject("mnuNodesBulkAddExpressions.Image")));
            this.mnuNodesBulkAddExpressions.Name = "mnuNodesBulkAddExpressions";
            this.mnuNodesBulkAddExpressions.Size = new System.Drawing.Size(220, 32);
            this.mnuNodesBulkAddExpressions.Tag = "8";
            this.mnuNodesBulkAddExpressions.Text = "Expressions";
            this.mnuNodesBulkAddExpressions.Click += new System.EventHandler(this.mnuNodesAdd_Click);
            // 
            // headerBulkMiscellaneous
            // 
            this.headerBulkMiscellaneous.BackColor = System.Drawing.SystemColors.ControlLight;
            this.headerBulkMiscellaneous.Enabled = false;
            this.headerBulkMiscellaneous.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Bold);
            this.headerBulkMiscellaneous.ForeColor = System.Drawing.Color.DimGray;
            this.headerBulkMiscellaneous.Name = "headerBulkMiscellaneous";
            this.headerBulkMiscellaneous.Size = new System.Drawing.Size(220, 32);
            this.headerBulkMiscellaneous.Text = "Miscellaneous";
            // 
            // mnuNodesImport
            // 
            this.mnuNodesImport.Name = "mnuNodesImport";
            this.mnuNodesImport.Size = new System.Drawing.Size(220, 32);
            this.mnuNodesImport.Text = "Import ...";
            this.mnuNodesImport.Click += new System.EventHandler(this.mnuNodesImport_Click);
            // 
            // headerRemove
            // 
            this.headerRemove.BackColor = System.Drawing.SystemColors.ControlLight;
            this.headerRemove.Enabled = false;
            this.headerRemove.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Bold);
            this.headerRemove.ForeColor = System.Drawing.Color.DimGray;
            this.headerRemove.Name = "headerRemove";
            this.headerRemove.Size = new System.Drawing.Size(348, 32);
            this.headerRemove.Text = "Remove";
            // 
            // mnuNodesRemove
            // 
            this.mnuNodesRemove.Name = "mnuNodesRemove";
            this.mnuNodesRemove.ShortcutKeyDisplayString = "Delete";
            this.mnuNodesRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuNodesRemove.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesRemove.Text = "Element";
            this.mnuNodesRemove.Click += new System.EventHandler(this.mnuNodesRemove_Click);
            // 
            // headerMiscellaneous
            // 
            this.headerMiscellaneous.BackColor = System.Drawing.SystemColors.ControlLight;
            this.headerMiscellaneous.Enabled = false;
            this.headerMiscellaneous.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Bold);
            this.headerMiscellaneous.ForeColor = System.Drawing.Color.DimGray;
            this.headerMiscellaneous.Name = "headerMiscellaneous";
            this.headerMiscellaneous.Size = new System.Drawing.Size(348, 32);
            this.headerMiscellaneous.Text = "Miscellaneous";
            // 
            // mnuNodesExpand
            // 
            this.mnuNodesExpand.Name = "mnuNodesExpand";
            this.mnuNodesExpand.ShortcutKeyDisplayString = "";
            this.mnuNodesExpand.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.mnuNodesExpand.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesExpand.Text = "Expand from here";
            this.mnuNodesExpand.Click += new System.EventHandler(this.mnuNodesExpand_Click);
            // 
            // mnuNodesCollapse
            // 
            this.mnuNodesCollapse.Name = "mnuNodesCollapse";
            this.mnuNodesCollapse.ShortcutKeyDisplayString = "";
            this.mnuNodesCollapse.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.mnuNodesCollapse.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesCollapse.Text = "Collapse from here";
            this.mnuNodesCollapse.Click += new System.EventHandler(this.mnuNodesCollapse_Click);
            // 
            // mnuNodesReload
            // 
            this.mnuNodesReload.Name = "mnuNodesReload";
            this.mnuNodesReload.Size = new System.Drawing.Size(348, 32);
            this.mnuNodesReload.Text = "Reload decision model";
            this.mnuNodesReload.Click += new System.EventHandler(this.mnuNodesReload_Click);
            // 
            // blaToolStripMenuItem
            // 
            this.blaToolStripMenuItem.Name = "blaToolStripMenuItem";
            this.blaToolStripMenuItem.Size = new System.Drawing.Size(181, 32);
            this.blaToolStripMenuItem.Text = "bla";
            // 
            // tvDecisionTree
            // 
            this.tvDecisionTree.AllowDrop = true;
            this.tvDecisionTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDecisionTree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvDecisionTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvDecisionTree.FullRowSelect = true;
            this.tvDecisionTree.HideSelection = false;
            this.tvDecisionTree.ImageIndex = 0;
            this.tvDecisionTree.ImageList = this.imageList;
            this.tvDecisionTree.Location = new System.Drawing.Point(0, 0);
            this.tvDecisionTree.Name = "tvDecisionTree";
            this.tvDecisionTree.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.tvDecisionTree.SelectedImageIndex = 0;
            this.tvDecisionTree.ShowNodeToolTips = true;
            this.tvDecisionTree.Size = new System.Drawing.Size(445, 492);
            this.tvDecisionTree.TabIndex = 0;
            this.tvDecisionTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvDecisionTree_ItemDrag);
            this.tvDecisionTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDecisionTree_AfterSelect);
            this.tvDecisionTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvDecisionTree_DragDrop);
            this.tvDecisionTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvDecisionTree_DragEnter);
            this.tvDecisionTree.DragOver += new System.Windows.Forms.DragEventHandler(this.tvDecisionTree_DragOver);
            this.tvDecisionTree.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.tvDecisionTree_GiveFeedback);
            this.tvDecisionTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvDecisionTree_KeyDown);
            this.tvDecisionTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvDecisionTree_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ucDecisionTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvDecisionTree);
            this.Name = "ucDecisionTree";
            this.Size = new System.Drawing.Size(445, 492);
            this.Load += new System.EventHandler(this.ucDecisionTree_Load);
            this.mnuNodes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SakwaTreeView tvDecisionTree;
        //private System.Windows.Forms.TreeView tvDecisionTree;
        private System.Windows.Forms.ContextMenuStrip mnuNodes;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesExpand;
        private ToolstripMenuHeader headerAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesAddDecision;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesAddChoice;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesAddAssignment;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesAddVarDefinition;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesAddDomainObjectDefinition;
        private ToolstripMenuHeader headerRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesRemove;
        private ToolstripMenuHeader headerMiscellaneous;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesBulkAddChoices;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesImport;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesBulkOperations;
        private System.Windows.Forms.ToolStripMenuItem blaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesBulkAddExpressions;
        private ToolstripMenuHeader headerBulkMiscellaneous;
        private ToolstripMenuHeader headerBulkAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesReload;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesCollapse;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesAddDataObject;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesAddDataSource;
        private System.Windows.Forms.ToolStripMenuItem mnuNodesAddDataDefinition;
        private System.Windows.Forms.ImageList imageList1;
    }
}
