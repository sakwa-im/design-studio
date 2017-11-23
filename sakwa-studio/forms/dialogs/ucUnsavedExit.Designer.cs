namespace sakwa
{
    partial class ucUnsavedExit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucUnsavedExit));
            this.roundedPanel1 = new sakwa.RoundedPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.roundedPanel2 = new sakwa.RoundedPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
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
            this.roundedPanel1.Size = new System.Drawing.Size(536, 69);
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
            this.label2.Size = new System.Drawing.Size(306, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Unsaved decision model(s)";
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.BorderColor = System.Drawing.Color.Black;
            this.roundedPanel2.Controls.Add(this.button2);
            this.roundedPanel2.Controls.Add(this.button3);
            this.roundedPanel2.Controls.Add(this.button1);
            this.roundedPanel2.Corners = sakwa.RoundedPanel.RoundedTypes.Bottom;
            this.roundedPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.roundedPanel2.FillColor = System.Drawing.Color.White;
            this.roundedPanel2.Location = new System.Drawing.Point(0, 267);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Radius = 20;
            this.roundedPanel2.Size = new System.Drawing.Size(536, 67);
            this.roundedPanel2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(26, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 41);
            this.button2.TabIndex = 0;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button3.Location = new System.Drawing.Point(218, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 41);
            this.button3.TabIndex = 0;
            this.button3.Text = "Save and exit";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(377, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Exit anyway";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.counterPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 198);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Press";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 85);
            this.label1.TabIndex = 5;
            this.label1.Text = "\'Cancel\' and continue working\r\n  - or -\r\n\'Save and exit\' to preserve you\'re chang" +
    "es\r\n  - or -\r\n\'Exit anyway\' and loose all changes\r\n";
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
            this.counterPanel1.Location = new System.Drawing.Point(25, 17);
            this.counterPanel1.Name = "counterPanel1";
            this.counterPanel1.OnCounterChanged = null;
            this.counterPanel1.Radius = 5;
            this.counterPanel1.Size = new System.Drawing.Size(373, 30);
            this.counterPanel1.TabIndex = 1;
            this.counterPanel1.Text = "Changes in the decision model(s) will be lost";
            // 
            // ucUnsavedExit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.roundedPanel2);
            this.Controls.Add(this.roundedPanel1);
            this.Name = "ucUnsavedExit";
            this.Size = new System.Drawing.Size(536, 334);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private CounterPanel counterPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}
