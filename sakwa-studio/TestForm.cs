using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public partial class TestForm : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TestForm));
        public TestForm()
        {
            InitializeComponent();

            TreeNode root = new TreeNode("Root");
            root.ImageIndex = root.SelectedImageIndex = 0;

            TreeNode first = new TreeNode("First");
            first.ImageIndex = first.SelectedImageIndex = 1;
            root.Nodes.Add(first);

            TreeNode second = new TreeNode("Second");
            second.ImageIndex = second.SelectedImageIndex = 2;
            root.Nodes.Add(second);

            TreeNode second_sibling = new TreeNode("First sibling");
            second_sibling.ImageIndex = second_sibling.SelectedImageIndex = 0;
            second.Nodes.Add(second_sibling);

            second_sibling = new TreeNode("Second sibling");
            second_sibling.ImageIndex = second_sibling.SelectedImageIndex = 1;
            second.Nodes.Add(second_sibling);
            TreeNode third = new TreeNode("Third");
            third.ImageIndex = third.SelectedImageIndex = 0;
            root.Nodes.Add(third);

            multiTreeView1.Nodes.Add(root);
            root.ExpandAll();

        }

        private void multiTreeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;

        }

        private void multiTreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                DoDragDrop(e.Item, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);
        }

        private void multiTreeView1_DragDrop(object sender, DragEventArgs e)
        {
            log.Debug("multiTreeView1_DragDrop");
            TreeNode targetNode = multiTreeView1.GetNodeAt(multiTreeView1.PointToClient(new Point(e.X, e.Y)));
            log.Debug(targetNode.Text);

            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            log.Debug(draggedNode != null ? draggedNode.Text : "Nothing");

        }
    }
}
