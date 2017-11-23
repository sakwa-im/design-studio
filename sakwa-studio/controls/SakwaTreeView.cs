using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public class SakwaTreeView : MultiTreeView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SakwaTreeView));
        public SakwaTreeView() : base() { }

        protected override string GetTreeNodeLabel(TreeNode node)
        {
            if (node == null || node.Tag == null)
                return "";

            IBaseNode baseNode = TreeNodeAsBaseNode(node);
            return baseNode.Name;

        }

        public static IBaseNode TreeNodeAsBaseNode(TreeNode node)
        {
            return node.Tag != null ? node.Tag as IBaseNode : null;
        }
    }
}
