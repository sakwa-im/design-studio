namespace sakwa
{
    partial class ucDecisionModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDecisionModel));
            this.mnuModel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModelCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModelToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.picModel = new ProPictureBox();
            this.mnuModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picModel)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuModel
            // 
            this.mnuModel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuModel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveModelToolStripMenuItem,
            this.mnuModelCopy,
            this.mnuModelToFile});
            this.mnuModel.Name = "mnuModel";
            this.mnuModel.Size = new System.Drawing.Size(202, 100);
            // 
            // saveModelToolStripMenuItem
            // 
            this.saveModelToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.saveModelToolStripMenuItem.Enabled = false;
            this.saveModelToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveModelToolStripMenuItem.Name = "saveModelToolStripMenuItem";
            this.saveModelToolStripMenuItem.Size = new System.Drawing.Size(201, 32);
            this.saveModelToolStripMenuItem.Text = "Save model";
            // 
            // mnuModelCopy
            // 
            this.mnuModelCopy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuModelCopy.Name = "mnuModelCopy";
            this.mnuModelCopy.Size = new System.Drawing.Size(201, 32);
            this.mnuModelCopy.Text = "To Clipboard";
            this.mnuModelCopy.Click += new System.EventHandler(this.mnuModelCopy_Click);
            // 
            // mnuModelToFile
            // 
            this.mnuModelToFile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuModelToFile.Name = "mnuModelToFile";
            this.mnuModelToFile.Size = new System.Drawing.Size(201, 32);
            this.mnuModelToFile.Text = "To file ...";
            this.mnuModelToFile.Click += new System.EventHandler(this.mnuModelToFile_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "*.png";
            this.saveFileDialog.Filter = "Image files (*.png)|*.png";
            this.saveFileDialog.Title = "Save model to";
            // 
            // picModel
            // 
            this.picModel.BackColor = System.Drawing.Color.White;
            this.picModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picModel.Location = new System.Drawing.Point(0, 0);
            this.picModel.Name = "picModel";
            this.picModel.Size = new System.Drawing.Size(398, 294);
            this.picModel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picModel.TabIndex = 0;
            this.picModel.TabStop = false;
            this.picModel.ModelLocationUpdate += new ModelLocation(this.picModel_ModelLocationUpdate);
            this.picModel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picModel_MouseDown);
            // 
            // ucDecisionModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.picModel);
            this.Name = "ucDecisionModel";
            this.Size = new System.Drawing.Size(398, 294);
            this.mnuModel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picModel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ProPictureBox picModel;
        private System.Windows.Forms.ContextMenuStrip mnuModel;
        private System.Windows.Forms.ToolStripMenuItem mnuModelCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuModelToFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveModelToolStripMenuItem;
    }
}
