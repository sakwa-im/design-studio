namespace sakwa
{
    partial class TestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.multiTreeView1 = new sakwa.MultiTreeView();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 60);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.multiTreeView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(258, 409);
            this.panel2.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "assignment-48x48-transp.png");
            this.imageList1.Images.SetKeyName(1, "Choices-48x48.png");
            this.imageList1.Images.SetKeyName(2, "decision-48x48.png");
            this.imageList1.Images.SetKeyName(3, "blank-48x48.png");
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter1.Location = new System.Drawing.Point(258, 60);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 409);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // multiTreeView1
            // 
            this.multiTreeView1.AllowDrop = true;
            this.multiTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.multiTreeView1.FullRowSelect = true;
            this.multiTreeView1.HideSelection = false;
            this.multiTreeView1.ImageIndex = 0;
            this.multiTreeView1.ImageList = this.imageList1;
            this.multiTreeView1.Location = new System.Drawing.Point(0, 0);
            this.multiTreeView1.Name = "multiTreeView1";
            this.multiTreeView1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.multiTreeView1.SelectedImageIndex = 0;
            this.multiTreeView1.Size = new System.Drawing.Size(258, 409);
            this.multiTreeView1.TabIndex = 0;
            this.multiTreeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.multiTreeView1_ItemDrag);
            this.multiTreeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.multiTreeView1_DragDrop);
            this.multiTreeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.multiTreeView1_DragEnter);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 469);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private MultiTreeView multiTreeView1;
        private System.Windows.Forms.ImageList imageList1;
    }
}