namespace sakwa
{
    partial class ucDataConnectionEditor
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
            this.pnlTop = new sakwa.RoundedPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBottom = new sakwa.RoundedPanel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgExports = new System.Windows.Forms.TabPage();
            this.tpgDefinition = new System.Windows.Forms.TabPage();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BorderColor = System.Drawing.Color.Black;
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Corners = sakwa.RoundedPanel.RoundedTypes.Top;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.FillColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Radius = 20;
            this.pnlTop.Size = new System.Drawing.Size(785, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(13, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(64, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label1";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.BorderColor = System.Drawing.Color.Black;
            this.pnlBottom.Corners = sakwa.RoundedPanel.RoundedTypes.Bottom;
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.FillColor = System.Drawing.Color.White;
            this.pnlBottom.Location = new System.Drawing.Point(0, 516);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Radius = 20;
            this.pnlBottom.Size = new System.Drawing.Size(785, 53);
            this.pnlBottom.TabIndex = 1;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.tabControl1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 50);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(785, 466);
            this.pnlMain.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgExports);
            this.tabControl1.Controls.Add(this.tpgDefinition);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(65, 31);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(785, 466);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpgMapping
            // 
            this.tpgExports.Location = new System.Drawing.Point(4, 35);
            this.tpgExports.Name = "tpgMapping";
            this.tpgExports.Padding = new System.Windows.Forms.Padding(3);
            this.tpgExports.Size = new System.Drawing.Size(777, 427);
            this.tpgExports.TabIndex = 0;
            this.tpgExports.Text = "Exports";
            this.tpgExports.UseVisualStyleBackColor = true;
            // 
            // tpgDefinition
            // 
            this.tpgDefinition.Location = new System.Drawing.Point(4, 35);
            this.tpgDefinition.Name = "tpgDefinition";
            this.tpgDefinition.Padding = new System.Windows.Forms.Padding(3);
            this.tpgDefinition.Size = new System.Drawing.Size(777, 427);
            this.tpgDefinition.TabIndex = 1;
            this.tpgDefinition.Text = "Definition";
            this.tpgDefinition.UseVisualStyleBackColor = true;
            // 
            // ucDataConnectionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucDataConnectionEditor";
            this.Size = new System.Drawing.Size(785, 569);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected RoundedPanel pnlTop;
        protected System.Windows.Forms.Label lblTitle;
        protected RoundedPanel pnlBottom;
        protected System.Windows.Forms.Panel pnlMain;
        protected System.Windows.Forms.TabControl tabControl1;
        protected System.Windows.Forms.TabPage tpgExports;
        protected System.Windows.Forms.TabPage tpgDefinition;
    }
}
