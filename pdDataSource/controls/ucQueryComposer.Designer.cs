namespace pgDataSource
{
    partial class ucQueryComposer
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DgColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgColType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgColValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgColRelation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSoure1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSoure1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(931, 383);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AllowDrop = true;
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(923, 357);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabPage1_DragDrop);
            this.tabPage1.DragOver += new System.Windows.Forms.DragEventHandler(this.tabPage1_DragOver);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(923, 357);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgColName,
            this.DgColType,
            this.DgColValue,
            this.DgColRelation,
            this.DgColDescription});
            this.dataGridView1.DataSource = this.bindingSoure1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(917, 351);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.dataGridView1.DragOver += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragOver);
            // 
            // DgColName
            // 
            this.DgColName.DataPropertyName = "Name";
            this.DgColName.HeaderText = "Name";
            this.DgColName.Name = "DgColName";
            this.DgColName.ReadOnly = true;
            // 
            // DgColType
            // 
            this.DgColType.DataPropertyName = "Type";
            this.DgColType.HeaderText = "Type";
            this.DgColType.Name = "DgColType";
            this.DgColType.ReadOnly = true;
            this.DgColType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DgColValue
            // 
            this.DgColValue.DataPropertyName = "Value";
            this.DgColValue.HeaderText = "Value";
            this.DgColValue.Name = "DgColValue";
            // 
            // DgColRelation
            // 
            this.DgColRelation.DataPropertyName = "Relation";
            this.DgColRelation.HeaderText = "Relation";
            this.DgColRelation.Name = "DgColRelation";
            this.DgColRelation.ReadOnly = true;
            // 
            // DgColDescription
            // 
            this.DgColDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DgColDescription.DataPropertyName = "Description";
            this.DgColDescription.HeaderText = "Description";
            this.DgColDescription.Name = "DgColDescription";
            this.DgColDescription.ReadOnly = true;
            // 
            // ucQueryComposer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucQueryComposer";
            this.Size = new System.Drawing.Size(931, 383);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSoure1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSoure1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgColType;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgColValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgColRelation;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgColDescription;
    }
}
