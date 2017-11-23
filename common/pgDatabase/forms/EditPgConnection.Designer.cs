namespace pgDatabase
{
    partial class EditPgConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPgConnection));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chxKeepSettings = new System.Windows.Forms.CheckBox();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.tbxUser = new System.Windows.Forms.TextBox();
            this.tbxDatabase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.picTest = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.picTrash = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTrash)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(134, 167);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 26);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(9, 167);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 26);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chxKeepSettings
            // 
            this.chxKeepSettings.AutoSize = true;
            this.chxKeepSettings.Location = new System.Drawing.Point(67, 131);
            this.chxKeepSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chxKeepSettings.Name = "chxKeepSettings";
            this.chxKeepSettings.Size = new System.Drawing.Size(90, 17);
            this.chxKeepSettings.TabIndex = 5;
            this.chxKeepSettings.Text = "Keep settings";
            this.chxKeepSettings.UseVisualStyleBackColor = true;
            // 
            // tbxPassword
            // 
            this.tbxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxPassword.Location = new System.Drawing.Point(67, 105);
            this.tbxPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '*';
            this.tbxPassword.Size = new System.Drawing.Size(138, 20);
            this.tbxPassword.TabIndex = 4;
            // 
            // tbxUser
            // 
            this.tbxUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxUser.Location = new System.Drawing.Point(67, 82);
            this.tbxUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxUser.Name = "tbxUser";
            this.tbxUser.Size = new System.Drawing.Size(138, 20);
            this.tbxUser.TabIndex = 3;
            this.tbxUser.TextChanged += new System.EventHandler(this.tbx_TextChanged);
            // 
            // tbxDatabase
            // 
            this.tbxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxDatabase.Location = new System.Drawing.Point(67, 55);
            this.tbxDatabase.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxDatabase.Name = "tbxDatabase";
            this.tbxDatabase.Size = new System.Drawing.Size(138, 20);
            this.tbxDatabase.TabIndex = 2;
            this.tbxDatabase.TextChanged += new System.EventHandler(this.tbx_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 105);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password";
            // 
            // tbxName
            // 
            this.tbxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxName.Location = new System.Drawing.Point(67, 8);
            this.tbxName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(138, 20);
            this.tbxName.TabIndex = 0;
            this.tbxName.TextChanged += new System.EventHandler(this.tbx_TextChanged);
            // 
            // tbxServer
            // 
            this.tbxServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxServer.Location = new System.Drawing.Point(67, 32);
            this.tbxServer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxServer.Name = "tbxServer";
            this.tbxServer.Size = new System.Drawing.Size(138, 20);
            this.tbxServer.TabIndex = 1;
            this.tbxServer.TextChanged += new System.EventHandler(this.tbx_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "User";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Database";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Server";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "grey-led.png");
            this.imageList.Images.SetKeyName(1, "red-led.png");
            this.imageList.Images.SetKeyName(2, "green-led.png");
            this.imageList.Images.SetKeyName(3, "grey-trash.png");
            this.imageList.Images.SetKeyName(4, "trash.png");
            // 
            // picTest
            // 
            this.picTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picTest.Location = new System.Drawing.Point(184, 128);
            this.picTest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picTest.Name = "picTest";
            this.picTest.Size = new System.Drawing.Size(19, 20);
            this.picTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTest.TabIndex = 18;
            this.picTest.TabStop = false;
            this.picTest.Click += new System.EventHandler(this.picTest_Click);
            // 
            // picTrash
            // 
            this.picTrash.Location = new System.Drawing.Point(9, 131);
            this.picTrash.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picTrash.Name = "picTrash";
            this.picTrash.Size = new System.Drawing.Size(19, 20);
            this.picTrash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTrash.TabIndex = 18;
            this.picTrash.TabStop = false;
            this.picTrash.Click += new System.EventHandler(this.picTrash_Click);
            // 
            // EditPgConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 201);
            this.Controls.Add(this.picTrash);
            this.Controls.Add(this.picTest);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chxKeepSettings);
            this.Controls.Add(this.tbxPassword);
            this.Controls.Add(this.tbxUser);
            this.Controls.Add(this.tbxDatabase);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.tbxServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "EditPgConnection";
            this.Text = "Postgresql connection";
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTrash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chxKeepSettings;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.TextBox tbxUser;
        private System.Windows.Forms.TextBox tbxDatabase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.PictureBox picTest;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox picTrash;
    }
}