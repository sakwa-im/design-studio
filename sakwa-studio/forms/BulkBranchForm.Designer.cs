namespace sakwa
{
    partial class BulkBranchForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCreateBranches = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.lbxValues = new System.Windows.Forms.ListBox();
            this.lbxVariables = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCreateBranches);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 292);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 59);
            this.panel1.TabIndex = 0;
            // 
            // btnCreateBranches
            // 
            this.btnCreateBranches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateBranches.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreateBranches.Location = new System.Drawing.Point(181, 9);
            this.btnCreateBranches.Name = "btnCreateBranches";
            this.btnCreateBranches.Size = new System.Drawing.Size(155, 38);
            this.btnCreateBranches.TabIndex = 0;
            this.btnCreateBranches.Text = "Create branch(es)";
            this.btnCreateBranches.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(12, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 38);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.lbxValues);
            this.panel2.Controls.Add(this.lbxVariables);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(348, 292);
            this.panel2.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(157, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 292);
            this.splitter1.TabIndex = 10;
            this.splitter1.TabStop = false;
            // 
            // lbxValues
            // 
            this.lbxValues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxValues.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbxValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxValues.FormattingEnabled = true;
            this.lbxValues.ItemHeight = 20;
            this.lbxValues.Location = new System.Drawing.Point(162, 0);
            this.lbxValues.Name = "lbxValues";
            this.lbxValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxValues.Size = new System.Drawing.Size(186, 292);
            this.lbxValues.TabIndex = 9;
            // 
            // lbxVariables
            // 
            this.lbxVariables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxVariables.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxVariables.FormattingEnabled = true;
            this.lbxVariables.ItemHeight = 20;
            this.lbxVariables.Location = new System.Drawing.Point(0, 0);
            this.lbxVariables.Name = "lbxVariables";
            this.lbxVariables.Size = new System.Drawing.Size(348, 292);
            this.lbxVariables.TabIndex = 11;
            this.lbxVariables.SelectedIndexChanged += new System.EventHandler(this.lbxVariables_SelectedIndexChanged);
            // 
            // BulkBranchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 351);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(366, 391);
            this.Name = "BulkBranchForm";
            this.Text = "Bulk branch creation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BulkBranchForm_FormClosing);
            this.Load += new System.EventHandler(this.BulkBranchForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCreateBranches;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListBox lbxValues;
        private System.Windows.Forms.ListBox lbxVariables;
    }
}