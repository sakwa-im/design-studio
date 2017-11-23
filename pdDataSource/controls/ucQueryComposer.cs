using System.Drawing;
using System.Windows.Forms;
using log4net;
using SourceMeta;

namespace pgDataSource
{
    public partial class ucQueryComposer : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ucQueryComposer));
        public ucQueryComposer()
        {
            InitializeComponent();
        }

        private void tabPage1_DragOver(object sender, DragEventArgs e)
        {
            TreeNode[] nodes = getTreeNodesFromDragEventArgs(e);
            e.Effect = DragDropEffects.None;

            foreach (TreeNode elem in nodes)
            {
                log.Debug(elem.Text);
                e.Effect = DragDropEffects.Copy;
            }
        }

        private TreeNode[] getTreeNodesFromDragEventArgs(DragEventArgs e)
        {
            string[] stringData = e.Data.GetFormats();
            object data = e.Data.GetData(stringData[0]);
            if (data != null && data is TreeNode[])
                return (TreeNode[])data;

            return new TreeNode[0];
        }

        private void tabPage1_DragDrop(object sender, DragEventArgs e)
        {
            TabPage tab = sender as TabPage;
            TreeNode[] nodes = getTreeNodesFromDragEventArgs(e);
            if (nodes.Length > 0)
            {
                foreach (TreeNode node in nodes)
                {
                    DbTreeNode dbNode = (node as DbTreeNode);

                    int index = tab.Controls.Count + 1;
                    Label lbl = new Label();
                    lbl.AutoSize = true;
                    lbl.Name = string.Format("Label-{0}", index);
                    lbl.Text = dbNode.getFqn();
                    lbl.Location = new Point(20, index * 20);

                    tab.Controls.Add(lbl);
                }
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode[] nodes = getTreeNodesFromDragEventArgs(e);
            if (nodes.Length > 0)
            {
                foreach (TreeNode node in nodes)
                {
                    DbTreeNode dbNode = (node as DbTreeNode);

                    foreach (MetaInfoViewRow row in dbNode.metaObj.MetaInfo)
                    {
                        if (!this.bindingSoure1.Contains(row)) {
                            this.bindingSoure1.Add(row);
                        }
                    }
                }
            }
        }
        
        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            TreeNode[] nodes = getTreeNodesFromDragEventArgs(e);
            e.Effect = DragDropEffects.None;

            foreach (TreeNode elem in nodes)
            {
                log.Debug(elem.Text);
                e.Effect = DragDropEffects.Copy;
            }
        }

    }
}
