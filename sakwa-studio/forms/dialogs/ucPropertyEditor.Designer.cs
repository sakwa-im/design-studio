namespace sakwa
{
    partial class ucPropertyEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPropertyEditor));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.roundedPanel1 = new sakwa.RoundedPanel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.roundedPanel2 = new sakwa.RoundedPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.roundedPanel3 = new sakwa.RoundedPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.mnuInput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInputGlobal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInputUser = new System.Windows.Forms.ToolStripMenuItem();
            this.roundedPanel1.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.roundedPanel3.SuspendLayout();
            this.mnuInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "mandatory.png");
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BorderColor = System.Drawing.Color.Black;
            this.roundedPanel1.Controls.Add(this.pnlMain);
            this.roundedPanel1.Controls.Add(this.roundedPanel2);
            this.roundedPanel1.Controls.Add(this.roundedPanel3);
            this.roundedPanel1.Corners = sakwa.RoundedPanel.RoundedTypes.Full;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundedPanel1.FillColor = System.Drawing.Color.White;
            this.roundedPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Radius = 20;
            this.roundedPanel1.Size = new System.Drawing.Size(693, 583);
            this.roundedPanel1.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMain.Location = new System.Drawing.Point(0, 69);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(693, 447);
            this.pnlMain.TabIndex = 7;
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.BorderColor = System.Drawing.Color.Black;
            this.roundedPanel2.Controls.Add(this.btnCancel);
            this.roundedPanel2.Controls.Add(this.btnOk);
            this.roundedPanel2.Corners = sakwa.RoundedPanel.RoundedTypes.Bottom;
            this.roundedPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.roundedPanel2.FillColor = System.Drawing.Color.White;
            this.roundedPanel2.Location = new System.Drawing.Point(0, 516);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Radius = 20;
            this.roundedPanel2.Size = new System.Drawing.Size(693, 67);
            this.roundedPanel2.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(26, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 41);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(561, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(106, 41);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // roundedPanel3
            // 
            this.roundedPanel3.BorderColor = System.Drawing.Color.Black;
            this.roundedPanel3.Controls.Add(this.label2);
            this.roundedPanel3.Corners = sakwa.RoundedPanel.RoundedTypes.Top;
            this.roundedPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundedPanel3.FillColor = System.Drawing.SystemColors.Control;
            this.roundedPanel3.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel3.Name = "roundedPanel3";
            this.roundedPanel3.Radius = 20;
            this.roundedPanel3.Size = new System.Drawing.Size(693, 69);
            this.roundedPanel3.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(21, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(424, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Configure properties of the datasource";
            // 
            // mnuInput
            // 
            this.mnuInput.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInputGlobal,
            this.mnuInputUser});
            this.mnuInput.Name = "mnuInput";
            this.mnuInput.Size = new System.Drawing.Size(165, 56);
            // 
            // mnuInputGlobal
            // 
            this.mnuInputGlobal.Name = "mnuInputGlobal";
            this.mnuInputGlobal.Size = new System.Drawing.Size(164, 26);
            this.mnuInputGlobal.Text = "Use global";
            this.mnuInputGlobal.Click += new System.EventHandler(this.mnuInputGlobal_Click);
            // 
            // mnuInputUser
            // 
            this.mnuInputUser.Name = "mnuInputUser";
            this.mnuInputUser.Size = new System.Drawing.Size(164, 26);
            this.mnuInputUser.Text = "Prompt user";
            this.mnuInputUser.Click += new System.EventHandler(this.mnuInputUser_Click);
            // 
            // ucPropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.roundedPanel1);
            this.Name = "ucPropertyEditor";
            this.Size = new System.Drawing.Size(693, 583);
            this.Load += new System.EventHandler(this.ucPropertyEditor_Load);
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel2.ResumeLayout(false);
            this.roundedPanel3.ResumeLayout(false);
            this.roundedPanel3.PerformLayout();
            this.mnuInput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList1;
        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Panel pnlMain;
        private RoundedPanel roundedPanel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private RoundedPanel roundedPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip mnuInput;
        private System.Windows.Forms.ToolStripMenuItem mnuInputGlobal;
        private System.Windows.Forms.ToolStripMenuItem mnuInputUser;
    }
}
