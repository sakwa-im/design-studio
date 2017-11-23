using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace sakwa
{
    public partial class ucSubModelNotFound : UserControl
    {
        private OpenFileDialog openFileDialog = null;
        private LinkSubTree subTree = null;
        private ToolTip missingFileToolTip = null;
        private ToolTip missingDirectoryToolTip = null;
        private string selectedFile = null;

        public ucSubModelNotFound(LinkSubTree subTree)
        {
            InitializeComponent();
            this.subTree = subTree;

            missingFileToolTip = new ToolTip();
            missingDirectoryToolTip = new ToolTip();
            openFileDialog = new OpenFileDialog();

            missingFileNameLabel.Text = Path.GetFileName(this.subTree.Link);
            missingDirectoryLabel.Text = string.Concat((Path.GetDirectoryName(this.subTree.Link).Take(this.Size.Width - missingDirectoryLabel.Left - 10)));
            missingFileToolTip.SetToolTip(missingFileNameLabel, this.subTree.Link);
            missingDirectoryToolTip.SetToolTip(missingDirectoryLabel, this.subTree.Link);
        }

        private void counterPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Path.GetDirectoryName(subTree.Link);
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                selectedFile = openFileDialog.FileName;

                selectedFileLabelContent.Text = Path.GetFileName(selectedFile);
                selectedDirectoryLabelContent.Text = string.Concat((Path.GetDirectoryName(selectedFile).Take(this.Size.Width - selectedDirectoryLabel.Left - 10)));

                selectedDirectoryLabel.Visible = true;
                selectedDirectoryLabelContent.Visible = true;
                selectedFileLabel.Visible = true;
                selectedFileLabelContent.Visible = true;

                subTree.Link = selectedFile;
                btnOk.Enabled = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
                subTree.Action = LinkSubTree.eLinkAction.Update;
        }
    }
}
