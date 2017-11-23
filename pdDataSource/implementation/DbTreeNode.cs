using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using pgDataSource;
using SourceMeta;

namespace pgDataSource
{
    class DbTreeNode : TreeNode, IDbTreeNode
    {
        protected string _DbType = "";

        protected List<IDbTreeNode> _childNodes = null;

        string IDbTreeNode.DbType
        {
            get { return this._DbType; }
            set { this._DbType = value; }
        }
        
        public DbTreeNode(string type) : base()
        {
            this._DbType = type;
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Forms.TreeNode class with the
        //     specified label text.
        //
        // Parameters:
        //   text:
        //     The label System.Windows.Forms.TreeNode.Text of the new tree node.
        
        public DbTreeNode(string type, string text) : base(text)
        {
            this._DbType = type;
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Forms.TreeNode class with the
        //     specified label text and child tree nodes.
        //
        // Parameters:
        //   text:
        //     The label System.Windows.Forms.TreeNode.Text of the new tree node.
        //
        //   children:
        //     An array of child System.Windows.Forms.TreeNode objects.
        public DbTreeNode(string type, string text, TreeNode[] children) : base(text,children)
        {
            this._DbType = type;
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Forms.TreeNode class with the
        //     specified label text and images to display when the tree node is in a selected
        //     and unselected state.
        //
        // Parameters:
        //   text:
        //     The label System.Windows.Forms.TreeNode.Text of the new tree node.
        //
        //   imageIndex:
        //     The index value of System.Drawing.Image to display when the tree node is unselected.
        //
        //   selectedImageIndex:
        //     The index value of System.Drawing.Image to display when the tree node is selected.
        public DbTreeNode(string type, string text, int imageIndex, int selectedImageIndex) : base(text,imageIndex,selectedImageIndex)
        {
            this._DbType = type;
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Forms.TreeNode class with the
        //     specified label text, child tree nodes, and images to display when the tree node
        //     is in a selected and unselected state.
        //
        // Parameters:
        //   text:
        //     The label System.Windows.Forms.TreeNode.Text of the new tree node.
        //
        //   imageIndex:
        //     The index value of System.Drawing.Image to display when the tree node is unselected.
        //
        //   selectedImageIndex:
        //     The index value of System.Drawing.Image to display when the tree node is selected.
        //
        //   children:
        //     An array of child System.Windows.Forms.TreeNode objects.
        public DbTreeNode(string type, string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : base(text,imageIndex,selectedImageIndex,children)
        {
            this._DbType = type;
        }
        
        public List<IDbTreeNode> childNodes
        {
            get
            {
                if (this._childNodes == null)
                {
                    this.fillChildNodes();
                }
                return this._childNodes;
            }
            set
            {
                this._childNodes = value;
            }
        } 

        private void fillChildNodes()
        {
            this._childNodes = new List<IDbTreeNode>();

        }

        public MetaObject metaObj = null;

        public string getFqn()
        {
            DbTreeNode dbNode = this;
            
            string name = "";
            while (dbNode != null)
            {
                if (dbNode.metaObj.mappable)
                {
                    name = dbNode.Text + "." + name;
                }
                dbNode = (dbNode.Parent as DbTreeNode);
            }
            name = name.TrimEnd(new char[] { '.' , ' ' });
            return name;
        }
    }

}
