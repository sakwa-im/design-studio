namespace sakwa
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainWindowLedStrip1 = new sakwa.ToolLedStrip();
            this.ledSeverityImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.lblVersion = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReduceText = new System.Windows.Forms.ToolStripButton();
            this.btnEnlargeText = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTools = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTreeModel = new sakwa.ToggleToolstripButton();
            this.btnGraphicModel = new sakwa.ToggleToolstripButton();
            this.btnProperties = new sakwa.ToggleToolstripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditTemplate = new sakwa.ToggleToolstripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.StatusTextTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusBar,
            this.MainWindowLedStrip1});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 353);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(684, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusBar
            // 
            this.lblStatusBar.Name = "lblStatusBar";
            this.lblStatusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // MainWindowLedStrip1
            // 
            this.MainWindowLedStrip1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MainWindowLedStrip1.AutoSize = false;
            this.MainWindowLedStrip1.CurrentSeverity = sakwa.ToolLedStrip.ToolLedIndicatorState.Clear;
            this.MainWindowLedStrip1.ImageList = this.ledSeverityImageList;
            this.MainWindowLedStrip1.Name = "MainWindowLedStrip1";
            this.MainWindowLedStrip1.Size = new System.Drawing.Size(80, 17);
            // 
            // ledSeverityImageList
            // 
            this.ledSeverityImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ledSeverityImageList.ImageStream")));
            this.ledSeverityImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ledSeverityImageList.Images.SetKeyName(0, "GrayLed.png");
            this.ledSeverityImageList.Images.SetKeyName(1, "OrangeLed.png");
            this.ledSeverityImageList.Images.SetKeyName(2, "GreenLed.png");
            this.ledSeverityImageList.Images.SetKeyName(3, "RedLed.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator2,
            this.btnHelp,
            this.lblVersion,
            this.toolStripSeparator3,
            this.btnReduceText,
            this.btnEnlargeText,
            this.toolStripSeparator1,
            this.btnTools,
            this.btnSaveAs,
            this.toolStripSeparator4,
            this.btnTreeModel,
            this.btnGraphicModel,
            this.btnProperties,
            this.toolStripSeparator5,
            this.btnEditTemplate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(684, 55);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(52, 52);
            this.btnNew.Text = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(52, 52);
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(52, 52);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // btnHelp
            // 
            this.btnHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(52, 52);
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(52, 52);
            this.lblVersion.Text = "Version: ";
            this.lblVersion.Click += new System.EventHandler(this.lblVersion_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 55);
            // 
            // btnReduceText
            // 
            this.btnReduceText.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnReduceText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReduceText.Image = ((System.Drawing.Image)(resources.GetObject("btnReduceText.Image")));
            this.btnReduceText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReduceText.Name = "btnReduceText";
            this.btnReduceText.Size = new System.Drawing.Size(52, 52);
            this.btnReduceText.Text = "toolStripButton1";
            this.btnReduceText.ToolTipText = "Decrease font size";
            this.btnReduceText.Click += new System.EventHandler(this.btnReduceText_Click);
            // 
            // btnEnlargeText
            // 
            this.btnEnlargeText.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnEnlargeText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEnlargeText.Image = ((System.Drawing.Image)(resources.GetObject("btnEnlargeText.Image")));
            this.btnEnlargeText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnlargeText.Name = "btnEnlargeText";
            this.btnEnlargeText.Size = new System.Drawing.Size(52, 52);
            this.btnEnlargeText.Text = "toolStripButton2";
            this.btnEnlargeText.ToolTipText = "Increase font size";
            this.btnEnlargeText.Click += new System.EventHandler(this.btnEnlargeText_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 55);
            // 
            // btnTools
            // 
            this.btnTools.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTools.Image = ((System.Drawing.Image)(resources.GetObject("btnTools.Image")));
            this.btnTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTools.Name = "btnTools";
            this.btnTools.Size = new System.Drawing.Size(52, 52);
            this.btnTools.Text = "toolStripButton1";
            this.btnTools.ToolTipText = "Configuration";
            this.btnTools.Click += new System.EventHandler(this.btnTools_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(52, 52);
            this.btnSaveAs.Text = "Save as";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 55);
            // 
            // btnTreeModel
            // 
            this.btnTreeModel.Checked = true;
            this.btnTreeModel.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnTreeModel.CheckedImage")));
            this.btnTreeModel.CheckOnClick = true;
            this.btnTreeModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnTreeModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTreeModel.Image = ((System.Drawing.Image)(resources.GetObject("btnTreeModel.Image")));
            this.btnTreeModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTreeModel.Name = "btnTreeModel";
            this.btnTreeModel.Size = new System.Drawing.Size(52, 52);
            this.btnTreeModel.ToolTipText = "Show/hide decision editor";
            this.btnTreeModel.UncheckedImage = ((System.Drawing.Image)(resources.GetObject("btnTreeModel.UncheckedImage")));
            this.btnTreeModel.Click += new System.EventHandler(this.btnTreeModel_Click);
            // 
            // btnGraphicModel
            // 
            this.btnGraphicModel.Checked = true;
            this.btnGraphicModel.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnGraphicModel.CheckedImage")));
            this.btnGraphicModel.CheckOnClick = true;
            this.btnGraphicModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnGraphicModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGraphicModel.Image = ((System.Drawing.Image)(resources.GetObject("btnGraphicModel.Image")));
            this.btnGraphicModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGraphicModel.Name = "btnGraphicModel";
            this.btnGraphicModel.Size = new System.Drawing.Size(52, 52);
            this.btnGraphicModel.Text = "Show/hide graphic model";
            this.btnGraphicModel.UncheckedImage = ((System.Drawing.Image)(resources.GetObject("btnGraphicModel.UncheckedImage")));
            this.btnGraphicModel.Click += new System.EventHandler(this.btnGraphicModel_Click);
            // 
            // btnProperties
            // 
            this.btnProperties.Checked = true;
            this.btnProperties.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnProperties.CheckedImage")));
            this.btnProperties.CheckOnClick = true;
            this.btnProperties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProperties.Image = ((System.Drawing.Image)(resources.GetObject("btnProperties.Image")));
            this.btnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(52, 52);
            this.btnProperties.Text = "Show/hide property editor";
            this.btnProperties.UncheckedImage = ((System.Drawing.Image)(resources.GetObject("btnProperties.UncheckedImage")));
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 55);
            // 
            // btnEditTemplate
            // 
            this.btnEditTemplate.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnEditTemplate.CheckedImage")));
            this.btnEditTemplate.CheckOnClick = true;
            this.btnEditTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnEditTemplate.Image")));
            this.btnEditTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditTemplate.Name = "btnEditTemplate";
            this.btnEditTemplate.Size = new System.Drawing.Size(52, 52);
            this.btnEditTemplate.Text = "Show/hide domain template editor";
            this.btnEditTemplate.UncheckedImage = ((System.Drawing.Image)(resources.GetObject("btnEditTemplate.UncheckedImage")));
            this.btnEditTemplate.Click += new System.EventHandler(this.btnEditTemplate_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 55);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(684, 298);
            this.pnlMain.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Descision models (*.sdm)|*.sdm";
            this.openFileDialog.Title = "Select decision model";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Descision models (*.sdm)|*.sdm";
            // 
            // StatusTextTimer
            // 
            this.StatusTextTimer.Tick += new System.EventHandler(this.StatusTextTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 375);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Sakwa, descision modeling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.MainForm_HelpRequested);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStripButton btnReduceText;
        private System.Windows.Forms.ToolStripButton btnEnlargeText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnTools;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.Timer StatusTextTimer;
        private ToggleToolstripButton btnTreeModel;
        private ToggleToolstripButton btnGraphicModel;
        private ToggleToolstripButton btnProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private ToggleToolstripButton btnEditTemplate;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusBar;
        private System.Windows.Forms.ImageList ledSeverityImageList;
        private ToolLedStrip MainWindowLedStrip1;
    }
}

