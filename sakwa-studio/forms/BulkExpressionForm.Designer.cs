namespace sakwa
{
    partial class BulkExpressionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkExpressionForm));
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbxSelected = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnMoveUp = new System.Windows.Forms.PictureBox();
            this.btnMoveDown = new System.Windows.Forms.PictureBox();
            this.btnRemove = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.lbxAvailable = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbxSelected);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.splitter1);
            this.panel3.Controls.Add(this.lbxAvailable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(555, 312);
            this.panel3.TabIndex = 5;
            // 
            // lbxSelected
            // 
            this.lbxSelected.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSelected.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxSelected.FormattingEnabled = true;
            this.lbxSelected.ItemHeight = 20;
            this.lbxSelected.Location = new System.Drawing.Point(301, 0);
            this.lbxSelected.Name = "lbxSelected";
            this.lbxSelected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxSelected.Size = new System.Drawing.Size(254, 312);
            this.lbxSelected.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnMoveUp);
            this.panel4.Controls.Add(this.btnMoveDown);
            this.panel4.Controls.Add(this.btnRemove);
            this.panel4.Controls.Add(this.btnAdd);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(236, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(65, 312);
            this.panel4.TabIndex = 7;
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.Location = new System.Drawing.Point(15, 143);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(38, 38);
            this.btnMoveUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMoveUp.TabIndex = 1;
            this.btnMoveUp.TabStop = false;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.Location = new System.Drawing.Point(15, 203);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(38, 38);
            this.btnMoveDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMoveDown.TabIndex = 1;
            this.btnMoveDown.TabStop = false;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(15, 83);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(38, 38);
            this.btnRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRemove.TabIndex = 1;
            this.btnRemove.TabStop = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(15, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(38, 38);
            this.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAdd.TabIndex = 1;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter1.Location = new System.Drawing.Point(231, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 312);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // lbxAvailable
            // 
            this.lbxAvailable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxAvailable.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbxAvailable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxAvailable.FormattingEnabled = true;
            this.lbxAvailable.ItemHeight = 20;
            this.lbxAvailable.Location = new System.Drawing.Point(0, 0);
            this.lbxAvailable.Name = "lbxAvailable";
            this.lbxAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxAvailable.Size = new System.Drawing.Size(231, 312);
            this.lbxAvailable.TabIndex = 5;
            this.lbxAvailable.SizeChanged += new System.EventHandler(this.lbxAvailable_SizeChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(555, 37);
            this.panel2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(303, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Selected expressions";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Available";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 349);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 71);
            this.panel1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(12, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 43);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(435, 14);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(108, 43);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Create";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "variable-definition-48x48-transp.png");
            // 
            // BulkExpressionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 420);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "BulkExpressionForm";
            this.Text = "Bulk expression creation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BulkAssignmentForm_FormClosing);
            this.Load += new System.EventHandler(this.BulkAssignmentForm_Load);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox lbxSelected;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox btnMoveUp;
        private System.Windows.Forms.PictureBox btnMoveDown;
        private System.Windows.Forms.PictureBox btnRemove;
        private System.Windows.Forms.PictureBox btnAdd;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListBox lbxAvailable;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolTip toolTip;
    }
}