namespace sakwa
{
    partial class ucDataObjectEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDataObjectEditor));
            this.lbxLinkedNodes = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.lbxExports = new System.Windows.Forms.ListBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tvModelDatasources = new sakwa.MultiTreeView();
            this.SuspendLayout();
            // 
            // lbxLinkedNodes
            // 
            this.lbxLinkedNodes.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbxLinkedNodes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxLinkedNodes.FormattingEnabled = true;
            this.lbxLinkedNodes.ItemHeight = 25;
            this.lbxLinkedNodes.Location = new System.Drawing.Point(0, 0);
            this.lbxLinkedNodes.Name = "lbxLinkedNodes";
            this.lbxLinkedNodes.Size = new System.Drawing.Size(238, 405);
            this.lbxLinkedNodes.TabIndex = 1;
            this.lbxLinkedNodes.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Listbox_DrawItem);
            this.lbxLinkedNodes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbxLinkedNodes_MouseDown);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter1.Location = new System.Drawing.Point(238, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 405);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // lbxExports
            // 
            this.lbxExports.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbxExports.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbxExports.FormattingEnabled = true;
            this.lbxExports.ItemHeight = 25;
            this.lbxExports.Location = new System.Drawing.Point(465, 0);
            this.lbxExports.Name = "lbxExports";
            this.lbxExports.Size = new System.Drawing.Size(235, 405);
            this.lbxExports.TabIndex = 3;
            this.lbxExports.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxIDataDefinitionExport_DrawItem);
            this.lbxExports.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbxExports_MouseDown);
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(460, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(5, 405);
            this.splitter2.TabIndex = 4;
            this.splitter2.TabStop = false;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "variable.png");
            this.imageList.Images.SetKeyName(1, "method.png");
            this.imageList.Images.SetKeyName(2, "decision-48x48-transp.png");
            this.imageList.Images.SetKeyName(3, "variable-definition-48x48-transp.png");
            this.imageList.Images.SetKeyName(4, "blank-48x48.png");
            // 
            // tvModelDatasources
            // 
            this.tvModelDatasources.AllowDrop = true;
            this.tvModelDatasources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvModelDatasources.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvModelDatasources.ImageIndex = 0;
            this.tvModelDatasources.ImageList = this.imageList;
            this.tvModelDatasources.ItemHeight = 25;
            this.tvModelDatasources.Location = new System.Drawing.Point(243, 0);
            this.tvModelDatasources.Name = "tvModelDatasources";
            this.tvModelDatasources.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.tvModelDatasources.SelectedImageIndex = 0;
            this.tvModelDatasources.Size = new System.Drawing.Size(217, 405);
            this.tvModelDatasources.TabIndex = 5;
            this.tvModelDatasources.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvModelDatasources_DragDrop);
            this.tvModelDatasources.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvModelDatasources_DragEnter);
            this.tvModelDatasources.DragOver += new System.Windows.Forms.DragEventHandler(this.tvModelDatasources_DragOver);
            this.tvModelDatasources.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvModelDatasources_KeyDown);
            // 
            // ucDataObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvModelDatasources);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.lbxExports);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lbxLinkedNodes);
            this.Name = "ucDataObjectEditor";
            this.Size = new System.Drawing.Size(700, 405);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxLinkedNodes;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListBox lbxExports;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ImageList imageList;
        private MultiTreeView tvModelDatasources;
    }
}
