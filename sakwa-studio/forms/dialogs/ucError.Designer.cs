namespace sakwa
{
    partial class ucError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucError));
            this.roundedPanel1 = new sakwa.RoundedPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.roundedPanel2 = new sakwa.RoundedPanel();
            this.btnMailLog = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.counterPanel1 = new sakwa.CounterPanel();
            this.roundedPanel1.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BorderColor = System.Drawing.Color.Black;
            this.roundedPanel1.Controls.Add(this.label2);
            this.roundedPanel1.Corners = sakwa.RoundedPanel.RoundedTypes.Top;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundedPanel1.FillColor = System.Drawing.SystemColors.Control;
            this.roundedPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Radius = 20;
            this.roundedPanel1.Size = new System.Drawing.Size(445, 69);
            this.roundedPanel1.TabIndex = 1;
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
            this.label2.Size = new System.Drawing.Size(259, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Something went wrong";
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.BorderColor = System.Drawing.Color.Black;
            this.roundedPanel2.Controls.Add(this.btnMailLog);
            this.roundedPanel2.Controls.Add(this.btOk);
            this.roundedPanel2.Corners = sakwa.RoundedPanel.RoundedTypes.Bottom;
            this.roundedPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.roundedPanel2.Enabled = false;
            this.roundedPanel2.FillColor = System.Drawing.Color.White;
            this.roundedPanel2.Location = new System.Drawing.Point(0, 206);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Radius = 20;
            this.roundedPanel2.Size = new System.Drawing.Size(445, 67);
            this.roundedPanel2.TabIndex = 2;
            // 
            // btnMailLog
            // 
            this.btnMailLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMailLog.Location = new System.Drawing.Point(26, 13);
            this.btnMailLog.Name = "btnMailLog";
            this.btnMailLog.Size = new System.Drawing.Size(106, 41);
            this.btnMailLog.TabIndex = 1;
            this.btnMailLog.Text = "Mail logs";
            this.btnMailLog.UseVisualStyleBackColor = true;
            this.btnMailLog.Click += new System.EventHandler(this.btnMailLog_Click);
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(313, 13);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(106, 41);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "Close";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.counterPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 137);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(370, 51);
            this.label1.TabIndex = 5;
            this.label1.Text = "\'Cancel\' and the \'save\' button to preserve you\'re changes\r\n  - or -\r\n\'Ok\' and loo" +
    "se the changes\r\n";
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
            this.counterPanel1.Location = new System.Drawing.Point(25, 13);
            this.counterPanel1.Name = "counterPanel1";
            this.counterPanel1.OnCounterChanged = null;
            this.counterPanel1.Radius = 5;
            this.counterPanel1.Size = new System.Drawing.Size(353, 30);
            this.counterPanel1.TabIndex = 1;
            this.counterPanel1.Text = "Changes in the decision model will be lost";
            this.counterPanel1.Visible = false;
            // 
            // ucError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.roundedPanel2);
            this.Controls.Add(this.roundedPanel1);
            this.Name = "ucError";
            this.Size = new System.Drawing.Size(445, 273);
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel1.PerformLayout();
            this.roundedPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Label label2;
        private RoundedPanel roundedPanel2;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Panel panel1;
        private CounterPanel counterPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMailLog;
    }
}
