namespace sakwa
{
    partial class BulkImportForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkImportForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlStep2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.propSource = new System.Windows.Forms.PropertyGrid();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.propDestination = new System.Windows.Forms.PropertyGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblSource = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tvImport = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.pnlStep1 = new System.Windows.Forms.Panel();
            this.picClear = new System.Windows.Forms.PictureBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbxSourceFile = new System.Windows.Forms.TextBox();
            this.lblSourceFile = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mnuImport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuImportMine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportTheirs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuImportSkip = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlStep2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlStep1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClear)).BeginInit();
            this.mnuImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 263);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 68);
            this.panel1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(460, 14);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(108, 43);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Import";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(12, 14);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 43);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlStep2);
            this.panel2.Controls.Add(this.pnlStep1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(577, 263);
            this.panel2.TabIndex = 1;
            // 
            // pnlStep2
            // 
            this.pnlStep2.Controls.Add(this.panel3);
            this.pnlStep2.Controls.Add(this.splitter1);
            this.pnlStep2.Controls.Add(this.tvImport);
            this.pnlStep2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStep2.Location = new System.Drawing.Point(0, 50);
            this.pnlStep2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlStep2.Name = "pnlStep2";
            this.pnlStep2.Size = new System.Drawing.Size(577, 213);
            this.pnlStep2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.propSource);
            this.panel3.Controls.Add(this.splitter2);
            this.panel3.Controls.Add(this.propDestination);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(190, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(387, 213);
            this.panel3.TabIndex = 2;
            // 
            // propSource
            // 
            this.propSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propSource.Location = new System.Drawing.Point(200, 39);
            this.propSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.propSource.Name = "propSource";
            this.propSource.Size = new System.Drawing.Size(187, 174);
            this.propSource.TabIndex = 3;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter2.Location = new System.Drawing.Point(195, 39);
            this.splitter2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(5, 174);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            // 
            // propDestination
            // 
            this.propDestination.Dock = System.Windows.Forms.DockStyle.Left;
            this.propDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propDestination.Location = new System.Drawing.Point(0, 39);
            this.propDestination.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.propDestination.Name = "propDestination";
            this.propDestination.Size = new System.Drawing.Size(195, 174);
            this.propDestination.TabIndex = 1;
            this.propDestination.SizeChanged += new System.EventHandler(this.propDestination_SizeChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblSource);
            this.panel4.Controls.Add(this.lblDestination);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(387, 39);
            this.panel4.TabIndex = 0;
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSource.Location = new System.Drawing.Point(196, 14);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(62, 20);
            this.lblSource.TabIndex = 0;
            this.lblSource.Text = "Source";
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestination.Location = new System.Drawing.Point(0, 14);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(94, 20);
            this.lblDestination.TabIndex = 0;
            this.lblDestination.Text = "Destination";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter1.Location = new System.Drawing.Point(185, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 213);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // tvImport
            // 
            this.tvImport.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvImport.ImageIndex = 0;
            this.tvImport.ImageList = this.imageList;
            this.tvImport.ItemHeight = 20;
            this.tvImport.Location = new System.Drawing.Point(0, 0);
            this.tvImport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvImport.Name = "tvImport";
            this.tvImport.SelectedImageIndex = 0;
            this.tvImport.ShowNodeToolTips = true;
            this.tvImport.Size = new System.Drawing.Size(185, 213);
            this.tvImport.TabIndex = 0;
            this.tvImport.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvImport_AfterSelect);
            this.tvImport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvImport_MouseDown);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "blank-48x48.png");
            this.imageList.Images.SetKeyName(1, "mine-48x48.png");
            this.imageList.Images.SetKeyName(2, "theirs-48x48.png");
            this.imageList.Images.SetKeyName(3, "minor-conflict-48x48.png");
            this.imageList.Images.SetKeyName(4, "minor-conflict-use-mine-48x48.png");
            this.imageList.Images.SetKeyName(5, "minor-conflict-use-theirs-48x48.png");
            this.imageList.Images.SetKeyName(6, "major-conflict-48x48.png");
            this.imageList.Images.SetKeyName(7, "major-conflict-use-mine-48x48.png");
            this.imageList.Images.SetKeyName(8, "major-conflict-use-theirs-48x48.png");
            this.imageList.Images.SetKeyName(9, "theirs-skip-48x48.png");
            // 
            // pnlStep1
            // 
            this.pnlStep1.Controls.Add(this.picClear);
            this.pnlStep1.Controls.Add(this.btnBrowse);
            this.pnlStep1.Controls.Add(this.tbxSourceFile);
            this.pnlStep1.Controls.Add(this.lblSourceFile);
            this.pnlStep1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep1.Location = new System.Drawing.Point(0, 0);
            this.pnlStep1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlStep1.Name = "pnlStep1";
            this.pnlStep1.Size = new System.Drawing.Size(577, 50);
            this.pnlStep1.TabIndex = 0;
            // 
            // picClear
            // 
            this.picClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClear.Image = ((System.Drawing.Image)(resources.GetObject("picClear.Image")));
            this.picClear.Location = new System.Drawing.Point(501, 15);
            this.picClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picClear.Name = "picClear";
            this.picClear.Size = new System.Drawing.Size(27, 25);
            this.picClear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picClear.TabIndex = 4;
            this.picClear.TabStop = false;
            this.picClear.Click += new System.EventHandler(this.picClear_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(532, 11);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(0);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(36, 30);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "...";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbxSourceFile
            // 
            this.tbxSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSourceFile.Location = new System.Drawing.Point(89, 15);
            this.tbxSourceFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxSourceFile.Name = "tbxSourceFile";
            this.tbxSourceFile.Size = new System.Drawing.Size(416, 22);
            this.tbxSourceFile.TabIndex = 1;
            this.tbxSourceFile.TextChanged += new System.EventHandler(this.tbxSourceFile_TextChanged);
            // 
            // lblSourceFile
            // 
            this.lblSourceFile.AutoSize = true;
            this.lblSourceFile.Location = new System.Drawing.Point(8, 17);
            this.lblSourceFile.Name = "lblSourceFile";
            this.lblSourceFile.Size = new System.Drawing.Size(75, 17);
            this.lblSourceFile.TabIndex = 0;
            this.lblSourceFile.Text = "Source file";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "Descision models (*.sdm)|*.sdm";
            this.openFileDialog.Title = "Select decision model";
            // 
            // mnuImport
            // 
            this.mnuImport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuImport.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuImport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImportMine,
            this.mnuImportTheirs,
            this.toolStripSeparator1,
            this.mnuImportSkip});
            this.mnuImport.Name = "mnuImport";
            this.mnuImport.Size = new System.Drawing.Size(174, 106);
            // 
            // mnuImportMine
            // 
            this.mnuImportMine.Image = ((System.Drawing.Image)(resources.GetObject("mnuImportMine.Image")));
            this.mnuImportMine.Name = "mnuImportMine";
            this.mnuImportMine.Size = new System.Drawing.Size(173, 32);
            this.mnuImportMine.Text = "Use mine";
            this.mnuImportMine.Click += new System.EventHandler(this.mnuImportMine_Click);
            // 
            // mnuImportTheirs
            // 
            this.mnuImportTheirs.Image = ((System.Drawing.Image)(resources.GetObject("mnuImportTheirs.Image")));
            this.mnuImportTheirs.Name = "mnuImportTheirs";
            this.mnuImportTheirs.Size = new System.Drawing.Size(173, 32);
            this.mnuImportTheirs.Text = "Use theirs";
            this.mnuImportTheirs.Click += new System.EventHandler(this.mnuImportTheirs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // mnuImportSkip
            // 
            this.mnuImportSkip.Image = ((System.Drawing.Image)(resources.GetObject("mnuImportSkip.Image")));
            this.mnuImportSkip.Name = "mnuImportSkip";
            this.mnuImportSkip.Size = new System.Drawing.Size(173, 32);
            this.mnuImportSkip.Text = "Skip";
            this.mnuImportSkip.Click += new System.EventHandler(this.mnuImportSkip_Click);
            // 
            // BulkImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 331);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(494, 316);
            this.Name = "BulkImportForm";
            this.Text = "Import variable definitions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BulkImportForm_FormClosing);
            this.Load += new System.EventHandler(this.BulkImportForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlStep2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlStep1.ResumeLayout(false);
            this.pnlStep1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClear)).EndInit();
            this.mnuImport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvImport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlStep1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox tbxSourceFile;
        private System.Windows.Forms.Label lblSourceFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel pnlStep2;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PropertyGrid propSource;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.PropertyGrid propDestination;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.ContextMenuStrip mnuImport;
        private System.Windows.Forms.ToolStripMenuItem mnuImportSkip;
        private System.Windows.Forms.ToolStripMenuItem mnuImportMine;
        private System.Windows.Forms.ToolStripMenuItem mnuImportTheirs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox picClear;
    }
}