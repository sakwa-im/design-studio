namespace pgDatabase
{
    partial class ucDatabase
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDatabase));
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDatabase = new System.Windows.Forms.ComboBox();
            this.picEdit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database";
            this.label1.BackColor = System.Drawing.Color.Transparent;
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDatabase.FormattingEnabled = true;
            this.cbxDatabase.Location = new System.Drawing.Point(58, 2);
            this.cbxDatabase.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(161, 21);
            this.cbxDatabase.TabIndex = 1;
            this.cbxDatabase.TextChanged += new System.EventHandler(this.cbxDatabase_TextChanged);
            // 
            // picEdit
            // 
            this.picEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picEdit.Image = ((System.Drawing.Image)(resources.GetObject("picEdit.Image")));
            this.picEdit.Location = new System.Drawing.Point(223, 2);
            this.picEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picEdit.Name = "picEdit";
            this.picEdit.Size = new System.Drawing.Size(18, 20);
            this.picEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEdit.TabIndex = 2;
            this.picEdit.TabStop = false;
            this.picEdit.Click += new System.EventHandler(this.picEdit_Click);
            // 
            // ucDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picEdit);
            this.Controls.Add(this.cbxDatabase);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(243, 32);
            this.Name = "ucDatabase";
            this.Size = new System.Drawing.Size(243, 32);
            this.Load += new System.EventHandler(this.ucDatabase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            //
            // DPI scaling setting, needed to properly scale and match the resolution within an WPF WindowsFormsHost. 
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDatabase;
        private System.Windows.Forms.PictureBox picEdit;
    }
}
