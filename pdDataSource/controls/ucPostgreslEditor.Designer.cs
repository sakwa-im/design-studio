namespace pgDataSource
{
    partial class ucPostgreslEditor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucDatabase = new pgDatabase.ucDatabase();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dbStructure = new sakwa.MultiTreeView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.pnlComposer = new System.Windows.Forms.Panel();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgDefinition.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.splitter1);
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Size = new System.Drawing.Size(504, 41);
            this.pnlTop.Controls.SetChildIndex(this.lblTitle, 0);
            this.pnlTop.Controls.SetChildIndex(this.panel1, 0);
            this.pnlTop.Controls.SetChildIndex(this.splitter1, 0);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(0, 315);
            this.pnlBottom.Size = new System.Drawing.Size(504, 43);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(504, 274);
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(504, 274);
            // 
            // tpgExports
            // 
            this.tpgExports.Location = new System.Drawing.Point(4, 35);
            this.tpgExports.Size = new System.Drawing.Size(496, 235);
            // 
            // tpgDefinition
            // 
            this.tpgDefinition.Controls.Add(this.pnlComposer);
            this.tpgDefinition.Controls.Add(this.splitter2);
            this.tpgDefinition.Controls.Add(this.dbStructure);
            this.tpgDefinition.Location = new System.Drawing.Point(4, 35);
            this.tpgDefinition.Size = new System.Drawing.Size(496, 235);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.ucDatabase);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(247, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 41);
            this.panel1.TabIndex = 1;
            // 
            // ucDatabase
            // 
            this.ucDatabase.Configuration = null;
            this.ucDatabase.ConfigurationName = "databases";
            this.ucDatabase.ConfigurationSource = configuration.eConfigurationSource.AllUsersAppData;
            this.ucDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDatabase.LastValue = "last-database";
            this.ucDatabase.Location = new System.Drawing.Point(0, 10);
            this.ucDatabase.Margin = new System.Windows.Forms.Padding(2);
            this.ucDatabase.MinimumSize = new System.Drawing.Size(243, 32);
            this.ucDatabase.Name = "ucDatabase";
            this.ucDatabase.Persistence = configuration.ePersistence.None;
            this.ucDatabase.Prefix = "";
            this.ucDatabase.SelectedDatabase = null;
            this.ucDatabase.Size = new System.Drawing.Size(245, 32);
            this.ucDatabase.TabIndex = 2;
            this.ucDatabase.OnDatabaseChanged += new pgDatabase.DatabaseEventHandler(this.ucDatabase_OnDatabaseChanged);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(245, 10);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(12, 31);
            this.panel3.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(257, 10);
            this.panel2.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(243, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 41);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // dbStructure
            // 
            this.dbStructure.Dock = System.Windows.Forms.DockStyle.Left;
            this.dbStructure.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.dbStructure.FullRowSelect = true;
            this.dbStructure.Location = new System.Drawing.Point(2, 2);
            this.dbStructure.Name = "dbStructure";
            this.dbStructure.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.dbStructure.Size = new System.Drawing.Size(164, 231);
            this.dbStructure.TabIndex = 0;
            this.dbStructure.MultipleItemDrag += new sakwa.MultipleItemDrag(this.dbStructure_MultipleItemDrag);
            this.dbStructure.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.dbStructure_AfterExpand);
            this.dbStructure.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.dbStructure_ItemDrag);
            this.dbStructure.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.dbStructure_AfterSelect);
            this.dbStructure.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dbStructure_MouseDown);
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.splitter2.Location = new System.Drawing.Point(166, 2);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(5, 231);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // pnlComposer
            // 
            this.pnlComposer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlComposer.Location = new System.Drawing.Point(171, 2);
            this.pnlComposer.Name = "pnlComposer";
            this.pnlComposer.Size = new System.Drawing.Size(323, 231);
            this.pnlComposer.TabIndex = 2;
            // 
            // ucPostgreslEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucPostgreslEditor";
            this.Size = new System.Drawing.Size(504, 358);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpgDefinition.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private pgDatabase.ucDatabase ucDatabase;
        private System.Windows.Forms.Panel panel3;
        private sakwa.MultiTreeView dbStructure;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panelMetaObject;
        private System.Windows.Forms.Panel pnlComposer;
    }
}
