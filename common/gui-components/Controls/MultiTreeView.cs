using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public delegate void MultipleItemDrag(object sender, MultiItemDragEventArgs e);
    public class MultiItemDragEventArgs : ItemDragEventArgs
    {
        public MultiItemDragEventArgs(MouseButtons button) : base(button)
        {
        }
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public MultiItemDragEventArgs(MouseButtons button, object item): base(button, item)
        {
        }

        public object[] Items { get; set; }

    }
    public class MultiTreeView : TreeView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MultiTreeView));
        public MultiTreeView() : base()
        {
            base.MouseClick += MultiTreeView_MouseClick;
            base.DrawMode = TreeViewDrawMode.OwnerDrawText;
            base.ItemDrag += MultiTreeView_ItemDrag; 

        }

        private void MultiTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _DragedNodes.Clear();
            _DragedNodes.AddRange(_SelectedNodes.ToArray());

            MultiItemDragEventArgs newEvent = new MultiItemDragEventArgs(e.Button);
            newEvent.Items = _DragedNodes.ToArray();

            if(MultipleItemDrag != null)
                MultipleItemDrag.Invoke(sender, newEvent);

        }

        [CategoryAttribute("MultiTreeView")]
        public event MultipleItemDrag MultipleItemDrag;

        [CategoryAttribute("Global settings")]
        public Color SelectedColor { get { return _SelectedColor; } set { _SelectedColor = value; } }
        private void MultiTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode node = GetNodeAt(e.Location);
            //SelectedNode = node;
            if (node != null)
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control && ParentsAreEqual(node))
                {
                    if (!_SelectedNodes.Contains(node))
                        _SelectedNodes.Add(node);
                    else
                        _SelectedNodes.Remove(node);

                }
                else
                {
                    _SelectedNodes.Clear();
                    _SelectedNodes.Add(node);

                }

                Refresh();

            }
        }

        protected bool ParentsAreEqual(TreeNode node)
        {
            foreach (TreeNode n in _SelectedNodes)
                if (n.Parent != node.Parent)
                    return false;

            return true;
        }
        protected virtual string GetTreeNodeLabel(TreeNode node)
        {
            return node != null ? node.Text : "";
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (e.Bounds.X >= 0)
            {
                Color backcolor = BackColor;
                Color forecolor = ForeColor;

                if (_SelectedNodes.Contains(e.Node))
                {
                    backcolor = _SelectedColor;
                    forecolor = Color.White;
                }

                Rectangle rect = e.Bounds;
                if (FullRowSelect)
                    rect.Width = this.Bounds.Width - rect.Location.X;

                e.Graphics.FillRectangle(new SolidBrush(backcolor), rect);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = this.Font;

                string label = GetTreeNodeLabel(e.Node);

                e.Graphics.DrawString(label, nodeFont, new SolidBrush(forecolor), rect);

            }
        }

        [CategoryAttribute("MultiTreeView")]
        public List<TreeNode> SelectedNodes {  get { return _SelectedNodes; } }
        [CategoryAttribute("MultiTreeView")]
        public List<TreeNode> DragedNodes {  get { return _DragedNodes; } }

        public new TreeNode SelectedNode
        {
            get { return base.SelectedNode; }
            set
            {
                base.SelectedNode = value;

                SelectedNodes.Clear();
                if(value != null)
                    SelectedNodes.Add(value);

                Refresh();

            }
        }
        protected List<TreeNode> _SelectedNodes = new List<TreeNode>();
        protected List<TreeNode> _DragedNodes = new List<TreeNode>();

        protected Color _SelectedColor = Color.FromArgb(255, 51, 153, 255);

    }
}
