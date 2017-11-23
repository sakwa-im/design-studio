namespace sakwa
{
    partial class ConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgGeneral = new System.Windows.Forms.TabPage();
            this.chxOpenOnStart = new System.Windows.Forms.CheckBox();
            this.grpModelColors = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tvModel = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.p6nlBack = new System.Windows.Forms.Panel();
            this.pnlBack5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlBack4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlBack3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlText6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlBack2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlText5 = new System.Windows.Forms.Panel();
            this.pnlText1 = new System.Windows.Forms.Panel();
            this.pnlText4 = new System.Windows.Forms.Panel();
            this.pnlBack1 = new System.Windows.Forms.Panel();
            this.pnlText3 = new System.Windows.Forms.Panel();
            this.pnlText2 = new System.Windows.Forms.Panel();
            this.btnBrowseHelpFile = new System.Windows.Forms.Button();
            this.btnBrowseModelFolder = new System.Windows.Forms.Button();
            this.tbxHelpFile = new System.Windows.Forms.TextBox();
            this.tbxModelFolder = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblModelFolder = new System.Windows.Forms.Label();
            this.tpgSpecial = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbMethods = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbxMethod = new System.Windows.Forms.TextBox();
            this.tpgInferenceConfig = new System.Windows.Forms.TabPage();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblTemplateFolder = new System.Windows.Forms.Label();
            this.tbxTemplateFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseTemplateFolder = new System.Windows.Forms.Button();
            this.grpTemplateColors = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tvTemplate = new System.Windows.Forms.TreeView();
            this.pnlBack12 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlBack11 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlBack10 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnlText12 = new System.Windows.Forms.Panel();
            this.pnlText87 = new System.Windows.Forms.Panel();
            this.pnlBack8 = new System.Windows.Forms.Panel();
            this.pnlText11 = new System.Windows.Forms.Panel();
            this.pnlText10 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgGeneral.SuspendLayout();
            this.grpModelColors.SuspendLayout();
            this.tpgSpecial.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpTemplateColors.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 410);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 56);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(12, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 39);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(474, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(109, 39);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgGeneral);
            this.tabControl1.Controls.Add(this.tpgSpecial);
            this.tabControl1.Controls.Add(this.tpgInferenceConfig);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(595, 410);
            this.tabControl1.TabIndex = 1;
            // 
            // tpgGeneral
            // 
            this.tpgGeneral.Controls.Add(this.chxOpenOnStart);
            this.tpgGeneral.Controls.Add(this.grpModelColors);
            this.tpgGeneral.Controls.Add(this.btnBrowseHelpFile);
            this.tpgGeneral.Controls.Add(this.btnBrowseTemplateFolder);
            this.tpgGeneral.Controls.Add(this.btnBrowseModelFolder);
            this.tpgGeneral.Controls.Add(this.tbxHelpFile);
            this.tpgGeneral.Controls.Add(this.tbxTemplateFolder);
            this.tpgGeneral.Controls.Add(this.tbxModelFolder);
            this.tpgGeneral.Controls.Add(this.label11);
            this.tpgGeneral.Controls.Add(this.label10);
            this.tpgGeneral.Controls.Add(this.lblTemplateFolder);
            this.tpgGeneral.Controls.Add(this.lblModelFolder);
            this.tpgGeneral.Location = new System.Drawing.Point(4, 25);
            this.tpgGeneral.Name = "tpgGeneral";
            this.tpgGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpgGeneral.Size = new System.Drawing.Size(587, 381);
            this.tpgGeneral.TabIndex = 0;
            this.tpgGeneral.Text = "General settings";
            this.tpgGeneral.UseVisualStyleBackColor = true;
            // 
            // chxOpenOnStart
            // 
            this.chxOpenOnStart.AutoSize = true;
            this.chxOpenOnStart.Location = new System.Drawing.Point(168, 76);
            this.chxOpenOnStart.Name = "chxOpenOnStart";
            this.chxOpenOnStart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chxOpenOnStart.Size = new System.Drawing.Size(18, 17);
            this.chxOpenOnStart.TabIndex = 9;
            this.chxOpenOnStart.UseVisualStyleBackColor = true;
            // 
            // grpModelColors
            // 
            this.grpModelColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpModelColors.Controls.Add(this.label9);
            this.grpModelColors.Controls.Add(this.label8);
            this.grpModelColors.Controls.Add(this.tvModel);
            this.grpModelColors.Controls.Add(this.label6);
            this.grpModelColors.Controls.Add(this.p6nlBack);
            this.grpModelColors.Controls.Add(this.pnlBack5);
            this.grpModelColors.Controls.Add(this.label5);
            this.grpModelColors.Controls.Add(this.pnlBack4);
            this.grpModelColors.Controls.Add(this.label4);
            this.grpModelColors.Controls.Add(this.pnlBack3);
            this.grpModelColors.Controls.Add(this.label3);
            this.grpModelColors.Controls.Add(this.pnlText6);
            this.grpModelColors.Controls.Add(this.label2);
            this.grpModelColors.Controls.Add(this.pnlBack2);
            this.grpModelColors.Controls.Add(this.label7);
            this.grpModelColors.Controls.Add(this.label1);
            this.grpModelColors.Controls.Add(this.pnlText5);
            this.grpModelColors.Controls.Add(this.pnlText1);
            this.grpModelColors.Controls.Add(this.pnlText4);
            this.grpModelColors.Controls.Add(this.pnlBack1);
            this.grpModelColors.Controls.Add(this.pnlText3);
            this.grpModelColors.Controls.Add(this.pnlText2);
            this.grpModelColors.Location = new System.Drawing.Point(14, 138);
            this.grpModelColors.Name = "grpModelColors";
            this.grpModelColors.Size = new System.Drawing.Size(565, 233);
            this.grpModelColors.TabIndex = 8;
            this.grpModelColors.TabStop = false;
            this.grpModelColors.Text = "Model colors";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(342, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "Preview";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(243, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Background";
            // 
            // tvModel
            // 
            this.tvModel.ImageIndex = 0;
            this.tvModel.ImageList = this.imageList;
            this.tvModel.Location = new System.Drawing.Point(345, 100);
            this.tvModel.Name = "tvModel";
            this.tvModel.SelectedImageIndex = 0;
            this.tvModel.Size = new System.Drawing.Size(211, 118);
            this.tvModel.TabIndex = 3;
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
            this.imageList.Images.SetKeyName(6, "Choices-48x48.png");
            this.imageList.Images.SetKeyName(7, "decision-48x48-linked.png");
            this.imageList.Images.SetKeyName(8, "connection-48x48.png");
            this.imageList.Images.SetKeyName(9, "connections-48x48.png");
            this.imageList.Images.SetKeyName(10, "sources-48x48.png");
            this.imageList.Images.SetKeyName(11, "source-48x48.png");
            this.imageList.Images.SetKeyName(12, "definition-48x48.png");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Branch";
            // 
            // p6nlBack
            // 
            this.p6nlBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p6nlBack.Location = new System.Drawing.Point(279, 193);
            this.p6nlBack.Name = "p6nlBack";
            this.p6nlBack.Size = new System.Drawing.Size(25, 25);
            this.p6nlBack.TabIndex = 6;
            this.p6nlBack.Tag = "6";
            this.p6nlBack.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // pnlBack5
            // 
            this.pnlBack5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack5.Location = new System.Drawing.Point(279, 131);
            this.pnlBack5.Name = "pnlBack5";
            this.pnlBack5.Size = new System.Drawing.Size(25, 25);
            this.pnlBack5.TabIndex = 6;
            this.pnlBack5.Tag = "5";
            this.pnlBack5.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Domain object";
            // 
            // pnlBack4
            // 
            this.pnlBack4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack4.Location = new System.Drawing.Point(279, 162);
            this.pnlBack4.Name = "pnlBack4";
            this.pnlBack4.Size = new System.Drawing.Size(25, 25);
            this.pnlBack4.TabIndex = 6;
            this.pnlBack4.Tag = "4";
            this.pnlBack4.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Expression";
            // 
            // pnlBack3
            // 
            this.pnlBack3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack3.Location = new System.Drawing.Point(279, 100);
            this.pnlBack3.Name = "pnlBack3";
            this.pnlBack3.Size = new System.Drawing.Size(25, 25);
            this.pnlBack3.TabIndex = 6;
            this.pnlBack3.Tag = "3";
            this.pnlBack3.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Variable definition";
            // 
            // pnlText6
            // 
            this.pnlText6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText6.Location = new System.Drawing.Point(178, 193);
            this.pnlText6.Name = "pnlText6";
            this.pnlText6.Size = new System.Drawing.Size(25, 25);
            this.pnlText6.TabIndex = 7;
            this.pnlText6.Tag = "6";
            this.pnlText6.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Variable definitions";
            // 
            // pnlBack2
            // 
            this.pnlBack2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack2.Location = new System.Drawing.Point(279, 69);
            this.pnlBack2.Name = "pnlBack2";
            this.pnlBack2.Size = new System.Drawing.Size(25, 25);
            this.pnlBack2.TabIndex = 6;
            this.pnlBack2.Tag = "2";
            this.pnlBack2.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(160, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Text color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Decision model";
            // 
            // pnlText5
            // 
            this.pnlText5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText5.Location = new System.Drawing.Point(178, 131);
            this.pnlText5.Name = "pnlText5";
            this.pnlText5.Size = new System.Drawing.Size(25, 25);
            this.pnlText5.TabIndex = 7;
            this.pnlText5.Tag = "5";
            this.pnlText5.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // pnlText1
            // 
            this.pnlText1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText1.Location = new System.Drawing.Point(178, 41);
            this.pnlText1.Name = "pnlText1";
            this.pnlText1.Size = new System.Drawing.Size(25, 25);
            this.pnlText1.TabIndex = 5;
            this.pnlText1.Tag = "1";
            this.pnlText1.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // pnlText4
            // 
            this.pnlText4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText4.Location = new System.Drawing.Point(178, 162);
            this.pnlText4.Name = "pnlText4";
            this.pnlText4.Size = new System.Drawing.Size(25, 25);
            this.pnlText4.TabIndex = 7;
            this.pnlText4.Tag = "4";
            this.pnlText4.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // pnlBack1
            // 
            this.pnlBack1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack1.Location = new System.Drawing.Point(279, 41);
            this.pnlBack1.Name = "pnlBack1";
            this.pnlBack1.Size = new System.Drawing.Size(25, 25);
            this.pnlBack1.TabIndex = 5;
            this.pnlBack1.Tag = "1";
            this.pnlBack1.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // pnlText3
            // 
            this.pnlText3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText3.Location = new System.Drawing.Point(178, 100);
            this.pnlText3.Name = "pnlText3";
            this.pnlText3.Size = new System.Drawing.Size(25, 25);
            this.pnlText3.TabIndex = 7;
            this.pnlText3.Tag = "3";
            this.pnlText3.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // pnlText2
            // 
            this.pnlText2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText2.Location = new System.Drawing.Point(178, 69);
            this.pnlText2.Name = "pnlText2";
            this.pnlText2.Size = new System.Drawing.Size(25, 25);
            this.pnlText2.TabIndex = 7;
            this.pnlText2.Tag = "2";
            this.pnlText2.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // btnBrowseHelpFile
            // 
            this.btnBrowseHelpFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseHelpFile.Location = new System.Drawing.Point(543, 102);
            this.btnBrowseHelpFile.Margin = new System.Windows.Forms.Padding(0);
            this.btnBrowseHelpFile.Name = "btnBrowseHelpFile";
            this.btnBrowseHelpFile.Size = new System.Drawing.Size(36, 30);
            this.btnBrowseHelpFile.TabIndex = 2;
            this.btnBrowseHelpFile.Text = "...";
            this.btnBrowseHelpFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowseHelpFile.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnBrowseHelpFile.UseVisualStyleBackColor = true;
            this.btnBrowseHelpFile.Click += new System.EventHandler(this.btnBrowseHelpFile_Click);
            // 
            // btnBrowseModelFolder
            // 
            this.btnBrowseModelFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseModelFolder.Location = new System.Drawing.Point(543, 7);
            this.btnBrowseModelFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btnBrowseModelFolder.Name = "btnBrowseModelFolder";
            this.btnBrowseModelFolder.Size = new System.Drawing.Size(36, 30);
            this.btnBrowseModelFolder.TabIndex = 2;
            this.btnBrowseModelFolder.Text = "...";
            this.btnBrowseModelFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowseModelFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnBrowseModelFolder.UseVisualStyleBackColor = true;
            this.btnBrowseModelFolder.Click += new System.EventHandler(this.btnBrowseModelFolder_Click);
            // 
            // tbxHelpFile
            // 
            this.tbxHelpFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxHelpFile.Location = new System.Drawing.Point(168, 106);
            this.tbxHelpFile.Name = "tbxHelpFile";
            this.tbxHelpFile.Size = new System.Drawing.Size(372, 22);
            this.tbxHelpFile.TabIndex = 1;
            // 
            // tbxModelFolder
            // 
            this.tbxModelFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxModelFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tbxModelFolder.Location = new System.Drawing.Point(168, 11);
            this.tbxModelFolder.Name = "tbxModelFolder";
            this.tbxModelFolder.Size = new System.Drawing.Size(372, 22);
            this.tbxModelFolder.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Help file";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(154, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Open model(s) on start";
            // 
            // lblModelFolder
            // 
            this.lblModelFolder.AutoSize = true;
            this.lblModelFolder.Location = new System.Drawing.Point(8, 14);
            this.lblModelFolder.Name = "lblModelFolder";
            this.lblModelFolder.Size = new System.Drawing.Size(86, 17);
            this.lblModelFolder.TabIndex = 0;
            this.lblModelFolder.Text = "Model folder";
            // 
            // tpgSpecial
            // 
            this.tpgSpecial.Controls.Add(this.grpTemplateColors);
            this.tpgSpecial.Controls.Add(this.groupBox1);
            this.tpgSpecial.Location = new System.Drawing.Point(4, 25);
            this.tpgSpecial.Name = "tpgSpecial";
            this.tpgSpecial.Size = new System.Drawing.Size(587, 381);
            this.tpgSpecial.TabIndex = 1;
            this.tpgSpecial.Text = "Special settings";
            this.tpgSpecial.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lbMethods);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.tbxMethod);
            this.groupBox1.Location = new System.Drawing.Point(8, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 192);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Standard methods";
            // 
            // lbMethods
            // 
            this.lbMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMethods.FormattingEnabled = true;
            this.lbMethods.ItemHeight = 16;
            this.lbMethods.Location = new System.Drawing.Point(6, 62);
            this.lbMethods.Name = "lbMethods";
            this.lbMethods.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbMethods.Size = new System.Drawing.Size(197, 116);
            this.lbMethods.TabIndex = 2;
            this.lbMethods.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbMethods_KeyDown);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(210, 59);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(34, 26);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(210, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(34, 26);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbxMethod
            // 
            this.tbxMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxMethod.Location = new System.Drawing.Point(6, 29);
            this.tbxMethod.Name = "tbxMethod";
            this.tbxMethod.Size = new System.Drawing.Size(198, 22);
            this.tbxMethod.TabIndex = 0;
            this.tbxMethod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxMethod_KeyDown);
            // 
            // tpgInferenceConfig
            // 
            this.tpgInferenceConfig.Location = new System.Drawing.Point(4, 25);
            this.tpgInferenceConfig.Name = "tpgInferenceConfig";
            this.tpgInferenceConfig.Size = new System.Drawing.Size(587, 381);
            this.tpgInferenceConfig.TabIndex = 2;
            this.tpgInferenceConfig.Text = "Inference configuration";
            this.tpgInferenceConfig.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Help files (*.htm)|*.htm";
            // 
            // lblTemplateFolder
            // 
            this.lblTemplateFolder.AutoSize = true;
            this.lblTemplateFolder.Location = new System.Drawing.Point(8, 45);
            this.lblTemplateFolder.Name = "lblTemplateFolder";
            this.lblTemplateFolder.Size = new System.Drawing.Size(107, 17);
            this.lblTemplateFolder.TabIndex = 0;
            this.lblTemplateFolder.Text = "Template folder";
            // 
            // tbxTemplateFolder
            // 
            this.tbxTemplateFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxTemplateFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tbxTemplateFolder.Location = new System.Drawing.Point(168, 42);
            this.tbxTemplateFolder.Name = "tbxTemplateFolder";
            this.tbxTemplateFolder.Size = new System.Drawing.Size(372, 22);
            this.tbxTemplateFolder.TabIndex = 1;
            // 
            // btnBrowseTemplateFolder
            // 
            this.btnBrowseTemplateFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseTemplateFolder.Location = new System.Drawing.Point(543, 38);
            this.btnBrowseTemplateFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btnBrowseTemplateFolder.Name = "btnBrowseTemplateFolder";
            this.btnBrowseTemplateFolder.Size = new System.Drawing.Size(36, 30);
            this.btnBrowseTemplateFolder.TabIndex = 2;
            this.btnBrowseTemplateFolder.Text = "...";
            this.btnBrowseTemplateFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowseTemplateFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnBrowseTemplateFolder.UseVisualStyleBackColor = true;
            this.btnBrowseTemplateFolder.Click += new System.EventHandler(this.btnBrowseTemplateFolder_Click);
            // 
            // grpTemplateColors
            // 
            this.grpTemplateColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTemplateColors.Controls.Add(this.label12);
            this.grpTemplateColors.Controls.Add(this.label13);
            this.grpTemplateColors.Controls.Add(this.tvTemplate);
            this.grpTemplateColors.Controls.Add(this.pnlBack12);
            this.grpTemplateColors.Controls.Add(this.label15);
            this.grpTemplateColors.Controls.Add(this.pnlBack11);
            this.grpTemplateColors.Controls.Add(this.label17);
            this.grpTemplateColors.Controls.Add(this.label18);
            this.grpTemplateColors.Controls.Add(this.pnlBack10);
            this.grpTemplateColors.Controls.Add(this.label19);
            this.grpTemplateColors.Controls.Add(this.label20);
            this.grpTemplateColors.Controls.Add(this.pnlText12);
            this.grpTemplateColors.Controls.Add(this.pnlText87);
            this.grpTemplateColors.Controls.Add(this.pnlBack8);
            this.grpTemplateColors.Controls.Add(this.pnlText11);
            this.grpTemplateColors.Controls.Add(this.pnlText10);
            this.grpTemplateColors.Location = new System.Drawing.Point(8, 12);
            this.grpTemplateColors.Name = "grpTemplateColors";
            this.grpTemplateColors.Size = new System.Drawing.Size(565, 168);
            this.grpTemplateColors.TabIndex = 9;
            this.grpTemplateColors.TabStop = false;
            this.grpTemplateColors.Text = "Template colors";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(345, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 17);
            this.label12.TabIndex = 9;
            this.label12.Text = "Preview";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(243, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 17);
            this.label13.TabIndex = 8;
            this.label13.Text = "Background";
            // 
            // tvTemplate
            // 
            this.tvTemplate.ImageIndex = 0;
            this.tvTemplate.ImageList = this.imageList;
            this.tvTemplate.Location = new System.Drawing.Point(348, 41);
            this.tvTemplate.Name = "tvTemplate";
            this.tvTemplate.SelectedImageIndex = 0;
            this.tvTemplate.Size = new System.Drawing.Size(211, 118);
            this.tvTemplate.TabIndex = 3;
            // 
            // pnlBack12
            // 
            this.pnlBack12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack12.Location = new System.Drawing.Point(279, 131);
            this.pnlBack12.Name = "pnlBack12";
            this.pnlBack12.Size = new System.Drawing.Size(25, 25);
            this.pnlBack12.TabIndex = 6;
            this.pnlBack12.Tag = "12";
            this.pnlBack12.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(22, 131);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 17);
            this.label15.TabIndex = 4;
            this.label15.Text = "Data definition";
            // 
            // pnlBack11
            // 
            this.pnlBack11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack11.Location = new System.Drawing.Point(279, 100);
            this.pnlBack11.Name = "pnlBack11";
            this.pnlBack11.Size = new System.Drawing.Size(25, 25);
            this.pnlBack11.TabIndex = 6;
            this.pnlBack11.Tag = "11";
            this.pnlBack11.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(22, 100);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(85, 17);
            this.label17.TabIndex = 4;
            this.label17.Text = "Data source";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(22, 69);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 17);
            this.label18.TabIndex = 4;
            this.label18.Text = "Data sources";
            // 
            // pnlBack10
            // 
            this.pnlBack10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack10.Location = new System.Drawing.Point(279, 69);
            this.pnlBack10.Name = "pnlBack10";
            this.pnlBack10.Size = new System.Drawing.Size(25, 25);
            this.pnlBack10.TabIndex = 6;
            this.pnlBack10.Tag = "10";
            this.pnlBack10.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(160, 18);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 17);
            this.label19.TabIndex = 4;
            this.label19.Text = "Text color";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(22, 41);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 17);
            this.label20.TabIndex = 4;
            this.label20.Text = "Data object";
            // 
            // pnlText12
            // 
            this.pnlText12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText12.Location = new System.Drawing.Point(178, 131);
            this.pnlText12.Name = "pnlText12";
            this.pnlText12.Size = new System.Drawing.Size(25, 25);
            this.pnlText12.TabIndex = 7;
            this.pnlText12.Tag = "12";
            this.pnlText12.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // pnlText87
            // 
            this.pnlText87.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText87.Location = new System.Drawing.Point(178, 41);
            this.pnlText87.Name = "pnlText87";
            this.pnlText87.Size = new System.Drawing.Size(25, 25);
            this.pnlText87.TabIndex = 5;
            this.pnlText87.Tag = "8";
            this.pnlText87.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // pnlBack8
            // 
            this.pnlBack8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack8.Location = new System.Drawing.Point(279, 41);
            this.pnlBack8.Name = "pnlBack8";
            this.pnlBack8.Size = new System.Drawing.Size(25, 25);
            this.pnlBack8.TabIndex = 5;
            this.pnlBack8.Tag = "8";
            this.pnlBack8.Click += new System.EventHandler(this.pnlBackColor_Click);
            // 
            // pnlText11
            // 
            this.pnlText11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText11.Location = new System.Drawing.Point(178, 100);
            this.pnlText11.Name = "pnlText11";
            this.pnlText11.Size = new System.Drawing.Size(25, 25);
            this.pnlText11.TabIndex = 7;
            this.pnlText11.Tag = "11";
            this.pnlText11.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // pnlText10
            // 
            this.pnlText10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlText10.Location = new System.Drawing.Point(178, 69);
            this.pnlText10.Name = "pnlText10";
            this.pnlText10.Size = new System.Drawing.Size(25, 25);
            this.pnlText10.TabIndex = 7;
            this.pnlText10.Tag = "10";
            this.pnlText10.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 466);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(610, 511);
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Configuration_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpgGeneral.ResumeLayout(false);
            this.tpgGeneral.PerformLayout();
            this.grpModelColors.ResumeLayout(false);
            this.grpModelColors.PerformLayout();
            this.tpgSpecial.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpTemplateColors.ResumeLayout(false);
            this.grpTemplateColors.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgGeneral;
        private System.Windows.Forms.Button btnBrowseModelFolder;
        private System.Windows.Forms.TextBox tbxModelFolder;
        private System.Windows.Forms.Label lblModelFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TreeView tvModel;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpModelColors;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel p6nlBack;
        private System.Windows.Forms.Panel pnlBack5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlBack4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlBack3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlText6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlBack2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlText5;
        private System.Windows.Forms.Panel pnlText1;
        private System.Windows.Forms.Panel pnlText4;
        private System.Windows.Forms.Panel pnlBack1;
        private System.Windows.Forms.Panel pnlText3;
        private System.Windows.Forms.Panel pnlText2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.CheckBox chxOpenOnStart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnBrowseHelpFile;
        private System.Windows.Forms.TextBox tbxHelpFile;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabPage tpgSpecial;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbMethods;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox tbxMethod;
        private System.Windows.Forms.TabPage tpgInferenceConfig;
        private System.Windows.Forms.Button btnBrowseTemplateFolder;
        private System.Windows.Forms.TextBox tbxTemplateFolder;
        private System.Windows.Forms.Label lblTemplateFolder;
        private System.Windows.Forms.GroupBox grpTemplateColors;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TreeView tvTemplate;
        private System.Windows.Forms.Panel pnlBack12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pnlBack11;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlBack10;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel pnlText12;
        private System.Windows.Forms.Panel pnlText87;
        private System.Windows.Forms.Panel pnlBack8;
        private System.Windows.Forms.Panel pnlText11;
        private System.Windows.Forms.Panel pnlText10;
    }
}