namespace sakwa
{
    partial class ucSubModelNotFound
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSubModelNotFound));
            this.panel1 = new System.Windows.Forms.Panel();
            this.missingDirectoryLabel = new System.Windows.Forms.Label();
            this.missingFileNameLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.counterPanel1 = new sakwa.CounterPanel();
            this.roundedPanel2 = new sakwa.RoundedPanel();
            this.browseButton = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.roundedPanel1 = new sakwa.RoundedPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.selectedDirectoryLabel = new System.Windows.Forms.Label();
            this.selectedFileLabel = new System.Windows.Forms.Label();
            this.selectedDirectoryLabelContent = new System.Windows.Forms.Label();
            this.selectedFileLabelContent = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.roundedPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.selectedDirectoryLabelContent);
            this.panel1.Controls.Add(this.selectedFileLabelContent);
            this.panel1.Controls.Add(this.selectedDirectoryLabel);
            this.panel1.Controls.Add(this.selectedFileLabel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.missingDirectoryLabel);
            this.panel1.Controls.Add(this.missingFileNameLabel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.counterPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 193);
            this.panel1.TabIndex = 2;
            // 
            // missingDirectoryLabel
            // 
            this.missingDirectoryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.missingDirectoryLabel.AutoSize = true;
            this.missingDirectoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missingDirectoryLabel.Location = new System.Drawing.Point(88, 57);
            this.missingDirectoryLabel.Name = "missingDirectoryLabel";
            this.missingDirectoryLabel.Size = new System.Drawing.Size(79, 13);
            this.missingDirectoryLabel.TabIndex = 9;
            this.missingDirectoryLabel.Text = "DIRECTORY";
            // 
            // missingFileNameLabel
            // 
            this.missingFileNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.missingFileNameLabel.AutoSize = true;
            this.missingFileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missingFileNameLabel.Location = new System.Drawing.Point(88, 44);
            this.missingFileNameLabel.Name = "missingFileNameLabel";
            this.missingFileNameLabel.Size = new System.Drawing.Size(68, 13);
            this.missingFileNameLabel.TabIndex = 8;
            this.missingFileNameLabel.Text = "FILENAME";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Missing file: ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Press";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 52);
            this.label1.TabIndex = 4;
            this.label1.Text = "\'Browse...\' to select the missing Sub Model file and \'Ok\' to use the file.\r\n-or-\r" +
    "\n\'Cancel\' to ignore the missing Sub Model file\r\n\r\n";
            // 
            // counterPanel1
            // 
            this.counterPanel1.BorderColor = System.Drawing.Color.Black;
            this.counterPanel1.Corners = sakwa.RoundedPanel.RoundedTypes.Transparent;
            this.counterPanel1.Counter = 0;
            this.counterPanel1.CounterAccessibleName = null;
            this.counterPanel1.CounterVisible = false;
            this.counterPanel1.FillColor = System.Drawing.Color.Transparent;
            this.counterPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.counterPanel1.ForeColor = System.Drawing.Color.Red;
            this.counterPanel1.Image = ((System.Drawing.Bitmap)(resources.GetObject("counterPanel1.Image")));
            this.counterPanel1.Location = new System.Drawing.Point(19, 11);
            this.counterPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.counterPanel1.Name = "counterPanel1";
            this.counterPanel1.OnCounterChanged = null;
            this.counterPanel1.Radius = 5;
            this.counterPanel1.Size = new System.Drawing.Size(305, 24);
            this.counterPanel1.TabIndex = 0;
            this.counterPanel1.Text = "The following file cannot be found in directory:";
            this.counterPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.counterPanel1_Paint);
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.BorderColor = System.Drawing.Color.Black;
            this.roundedPanel2.Controls.Add(this.browseButton);
            this.roundedPanel2.Controls.Add(this.btnCancel);
            this.roundedPanel2.Controls.Add(this.btnOk);
            this.roundedPanel2.Corners = sakwa.RoundedPanel.RoundedTypes.Bottom;
            this.roundedPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.roundedPanel2.FillColor = System.Drawing.Color.White;
            this.roundedPanel2.Location = new System.Drawing.Point(0, 249);
            this.roundedPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Radius = 20;
            this.roundedPanel2.Size = new System.Drawing.Size(396, 54);
            this.roundedPanel2.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(180, 11);
            this.browseButton.Margin = new System.Windows.Forms.Padding(2);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(80, 33);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(20, 11);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 33);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(297, 11);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 33);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BorderColor = System.Drawing.Color.Black;
            this.roundedPanel1.Controls.Add(this.label2);
            this.roundedPanel1.Corners = sakwa.RoundedPanel.RoundedTypes.Top;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundedPanel1.FillColor = System.Drawing.SystemColors.Control;
            this.roundedPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Radius = 20;
            this.roundedPanel1.Size = new System.Drawing.Size(396, 56);
            this.roundedPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(16, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "File Not Found";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "In directory:";
            // 
            // selectedDirectoryLabel
            // 
            this.selectedDirectoryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectedDirectoryLabel.AutoSize = true;
            this.selectedDirectoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedDirectoryLabel.Location = new System.Drawing.Point(16, 101);
            this.selectedDirectoryLabel.Name = "selectedDirectoryLabel";
            this.selectedDirectoryLabel.Size = new System.Drawing.Size(62, 13);
            this.selectedDirectoryLabel.TabIndex = 12;
            this.selectedDirectoryLabel.Text = "In directory:";
            this.selectedDirectoryLabel.Visible = false;
            // 
            // selectedFileLabel
            // 
            this.selectedFileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectedFileLabel.AutoSize = true;
            this.selectedFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedFileLabel.Location = new System.Drawing.Point(16, 88);
            this.selectedFileLabel.Name = "selectedFileLabel";
            this.selectedFileLabel.Size = new System.Drawing.Size(71, 13);
            this.selectedFileLabel.TabIndex = 11;
            this.selectedFileLabel.Text = "Selected file: ";
            this.selectedFileLabel.Visible = false;
            // 
            // selectedDirectoryLabelContent
            // 
            this.selectedDirectoryLabelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectedDirectoryLabelContent.AutoSize = true;
            this.selectedDirectoryLabelContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedDirectoryLabelContent.Location = new System.Drawing.Point(88, 101);
            this.selectedDirectoryLabelContent.Name = "selectedDirectoryLabelContent";
            this.selectedDirectoryLabelContent.Size = new System.Drawing.Size(79, 13);
            this.selectedDirectoryLabelContent.TabIndex = 14;
            this.selectedDirectoryLabelContent.Text = "DIRECTORY";
            this.selectedDirectoryLabelContent.Visible = false;
            // 
            // selectedFileLabelContent
            // 
            this.selectedFileLabelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectedFileLabelContent.AutoSize = true;
            this.selectedFileLabelContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedFileLabelContent.Location = new System.Drawing.Point(88, 88);
            this.selectedFileLabelContent.Name = "selectedFileLabelContent";
            this.selectedFileLabelContent.Size = new System.Drawing.Size(68, 13);
            this.selectedFileLabelContent.TabIndex = 13;
            this.selectedFileLabelContent.Text = "FILENAME";
            this.selectedFileLabelContent.Visible = false;
            // 
            // ucSubModelNotFound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.roundedPanel2);
            this.Controls.Add(this.roundedPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucSubModelNotFound";
            this.Size = new System.Drawing.Size(396, 303);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.roundedPanel2.ResumeLayout(false);
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Label label2;
        private RoundedPanel roundedPanel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private CounterPanel counterPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label missingFileNameLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label missingDirectoryLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label selectedDirectoryLabel;
        private System.Windows.Forms.Label selectedFileLabel;
        private System.Windows.Forms.Label selectedDirectoryLabelContent;
        private System.Windows.Forms.Label selectedFileLabelContent;
    }
}
