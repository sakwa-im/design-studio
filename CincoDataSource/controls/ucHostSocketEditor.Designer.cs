namespace HostDataSource
{
    partial class ucCincoSocketEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCincoSocketEditor));
            this.dgDefinitions = new System.Windows.Forms.DataGridView();
            this.purpose = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSocket = new System.Windows.Forms.Button();
            this.pnlExports = new System.Windows.Forms.Panel();
            this.lbxExports = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlAvailable = new System.Windows.Forms.Panel();
            this.lbxAvailable = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgExports.SuspendLayout();
            this.tpgDefinition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDefinitions)).BeginInit();
            this.pnlExports.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlAvailable.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnSocket);
            this.pnlTop.Controls.SetChildIndex(this.lblTitle, 0);
            this.pnlTop.Controls.SetChildIndex(this.btnSocket, 0);
            // 
            // tabControl1
            // 
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpgExports
            // 
            this.tpgExports.Controls.Add(this.pnlAvailable);
            this.tpgExports.Controls.Add(this.splitter1);
            this.tpgExports.Controls.Add(this.pnlExports);
            // 
            // tpgDefinition
            // 
            this.tpgDefinition.Controls.Add(this.dgDefinitions);
            // 
            // dgDefinitions
            // 
            this.dgDefinitions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDefinitions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.purpose,
            this.name,
            this.type,
            this.description});
            this.dgDefinitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDefinitions.Location = new System.Drawing.Point(3, 3);
            this.dgDefinitions.Name = "dgDefinitions";
            this.dgDefinitions.RowTemplate.Height = 24;
            this.dgDefinitions.Size = new System.Drawing.Size(771, 421);
            this.dgDefinitions.TabIndex = 1;
            this.dgDefinitions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgDefinitions_DataError);
            this.dgDefinitions.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgDefinitions_UserAddedRow);
            // 
            // purpose
            // 
            dataGridViewCellStyle1.NullValue = "Variable";
            this.purpose.DefaultCellStyle = dataGridViewCellStyle1;
            this.purpose.HeaderText = "Purpose";
            this.purpose.Items.AddRange(new object[] {
            "Variable",
            "Method"});
            this.purpose.Name = "purpose";
            this.purpose.Width = 125;
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.Width = 200;
            // 
            // type
            // 
            dataGridViewCellStyle2.NullValue = "string";
            this.type.DefaultCellStyle = dataGridViewCellStyle2;
            this.type.FillWeight = 50F;
            this.type.HeaderText = "Type";
            this.type.Items.AddRange(new object[] {
            "string",
            "int",
            "bool",
            "double",
            "date",
            "void"});
            this.type.Name = "type";
            this.type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.type.Width = 150;
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            // 
            // btnSocket
            // 
            this.btnSocket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSocket.Location = new System.Drawing.Point(640, 6);
            this.btnSocket.Name = "btnSocket";
            this.btnSocket.Size = new System.Drawing.Size(131, 38);
            this.btnSocket.TabIndex = 1;
            this.btnSocket.Text = "Create socket";
            this.btnSocket.UseVisualStyleBackColor = true;
            // 
            // pnlExports
            // 
            this.pnlExports.Controls.Add(this.lbxExports);
            this.pnlExports.Controls.Add(this.panel2);
            this.pnlExports.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlExports.Location = new System.Drawing.Point(3, 3);
            this.pnlExports.Name = "pnlExports";
            this.pnlExports.Size = new System.Drawing.Size(381, 421);
            this.pnlExports.TabIndex = 1;
            // 
            // lbxExports
            // 
            this.lbxExports.AllowDrop = true;
            this.lbxExports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxExports.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxExports.FormattingEnabled = true;
            this.lbxExports.ItemHeight = 25;
            this.lbxExports.Location = new System.Drawing.Point(0, 42);
            this.lbxExports.Name = "lbxExports";
            this.lbxExports.Size = new System.Drawing.Size(381, 379);
            this.lbxExports.TabIndex = 2;
            this.lbxExports.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxIDataDefinitionExport_DrawItem);
            this.lbxExports.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbxExports_DragDrop);
            this.lbxExports.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbxExports_DragEnter);
            this.lbxExports.DragOver += new System.Windows.Forms.DragEventHandler(this.lbxExports_DragOver);
            this.lbxExports.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.lbxExports_GiveFeedback);
            this.lbxExports.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbxExports_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(381, 42);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Export";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.splitter1.Location = new System.Drawing.Point(384, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 421);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // pnlAvailable
            // 
            this.pnlAvailable.Controls.Add(this.lbxAvailable);
            this.pnlAvailable.Controls.Add(this.panel3);
            this.pnlAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAvailable.Location = new System.Drawing.Point(389, 3);
            this.pnlAvailable.Name = "pnlAvailable";
            this.pnlAvailable.Size = new System.Drawing.Size(385, 421);
            this.pnlAvailable.TabIndex = 3;
            // 
            // lbxAvailable
            // 
            this.lbxAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxAvailable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxAvailable.FormattingEnabled = true;
            this.lbxAvailable.ItemHeight = 25;
            this.lbxAvailable.Location = new System.Drawing.Point(0, 42);
            this.lbxAvailable.Name = "lbxAvailable";
            this.lbxAvailable.Size = new System.Drawing.Size(385, 379);
            this.lbxAvailable.TabIndex = 2;
            this.lbxAvailable.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxIDataDefinitionExport_DrawItem);
            this.lbxAvailable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbxAvailable_MouseDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(385, 42);
            this.panel3.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Available";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "variable.png");
            this.imageList.Images.SetKeyName(1, "method.png");
            // 
            // ucCincoSocketEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucCincoSocketEditor";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpgExports.ResumeLayout(false);
            this.tpgDefinition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDefinitions)).EndInit();
            this.pnlExports.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlAvailable.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSocket;
        private System.Windows.Forms.DataGridView dgDefinitions;
        private System.Windows.Forms.Panel pnlAvailable;
        private System.Windows.Forms.ListBox lbxAvailable;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlExports;
        private System.Windows.Forms.ListBox lbxExports;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewComboBoxColumn purpose;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewComboBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
    }
}
