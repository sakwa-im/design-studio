using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using pgDatabase;
using sakwa;
using pgDataSource;
using System.ComponentModel.Composition;
using SourceMeta;
using log4net;

namespace pgDataSource
{
    public partial class ucPostgreslEditor : ucDataConnectionEditor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ucPostgreslEditor));
        [Import(typeof(IDataSourceMetadata))]

        private IPgConnection _currentConnection = null;

        public ucPostgreslEditor()
        {
            InitializeComponent();
            ucDatabase.Configuration = conf;

        }

        public ucPostgreslEditor(IBaseNode node)
            : base(node)
        {
            InitializeComponent();

            ucDatabase.Configuration = conf;
            ucDatabase.ConfigurationSource = Constants.ConfigurationSource;

            IDataSource parent = node.Parent as IDataSource;
            if (parent != null)
            {
                IPgConnection conn = new PgConnection();
                conn.Name = node.Parent.Name;
                conn.Server = parent.GetPropertyValue("dbHost");
                conn.Database = parent.GetPropertyValue("dbDatabase");
                conn.User = parent.GetPropertyValue("dbUser");
                conn.Password = parent.GetPropertyValue("dbPassword");

                ucDatabase.AddConnection(conn);

            }

            ucDatabase.LoadConfiguration();
        }

        private void InitTree()
        {
            IPgDatabase db = PgDatabase.Interface;
            db.PgConnection = findConnection();

            if (!db.IsConnectionLife)
                return;

            IDataSourceMetadata meta = new SqlMetadataImpl(db);
            MetaObject root = meta.GetRoot().First<MetaObject>();

            DbTreeNode rootNode = new DbTreeNode(root.type, root.value);
            rootNode.metaObj = root;
            this.updateTreeNodes(rootNode, true);            
            this.addPanel();

            // foreach schema get tables.
            dbStructure.Nodes.Add(rootNode);
        }


        private void addPanel()
        {
            ucQueryComposer uc = new ucQueryComposer();
            uc.Dock = DockStyle.Top;
            pnlComposer.Controls.Add(uc);

            //panelMetaObject = new System.Windows.Forms.Panel();
            //this.panelMetaObject.BackColor = System.Drawing.SystemColors.Info;
            //this.panelMetaObject.Dock = System.Windows.Forms.DockStyle.Left;
            //this.panelMetaObject.Location = new System.Drawing.Point(245, 10);
            //this.panelMetaObject.Margin = new System.Windows.Forms.Padding(2);
            //this.panelMetaObject.Name = "panelMetaObject";
            //this.panelMetaObject.Size = new System.Drawing.Size(120, 310);
            //this.panelMetaObject.TabIndex = 1;

            ////this.panelMetaObject.AutoSize = true;
            ////this.panelMetaObject.AutoSizeMode = GrowOnly;

            //this.tpgDefinition.Controls.Add(this.panelMetaObject);
            //this.tpgDefinition.Controls.SetChildIndex(panelMetaObject,0);
            //this.tpgDefinition.Refresh();
        }

        private IPgConnection findConnection(string name = "last-database")
        {
            if (_currentConnection == null)
            {
                if (hasConnection())
                {
                    IPgConnection con = ucDatabase.Stash.GetConnection(name);
                    return con;
                }
            }
            return _currentConnection;
        }

        private bool hasConnection(string name = "last-database")
        {
            bool result = false;
            if (name == "last-database")
            {
                result = (ucDatabase.Stash.Connections.Count > 0);
            }
            else
            {
                result = ucDatabase.Stash.Contains(name);
            }
            return result;
        }

        // callbacks/events
        private void ucDatabase_OnDatabaseChanged(object sender, pgDatabase.DatabaseEventArgs e)
        {
            _currentConnection = e.PgConnection;
            ucDatabase.SaveConfiguration();
            conf.Save();

            dbStructure.Nodes.Clear();
            InitTree();
            dbStructure.Refresh();
        }

        private void dbStructure_AfterExpand(Object sender, TreeViewEventArgs e)
        {

            this.dbStructure_AfterSelect(sender, e);
        }

        private void dbStructure_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            DbTreeNode node = e.Node as DbTreeNode;
            updateTreeNodes(node, false);
            dbStructure.Refresh();
        }


        private void updateTreeNodes(DbTreeNode node, bool childnode)
        {
            IPgDatabase db = PgDatabase.Interface;
            db.PgConnection = this.findConnection();

            if (node.Nodes.Count > 0)
            {
                if (!childnode)
                {
                    updateNodes(node,childnode);
                }
            }
            else
            {
                node.Nodes.AddRange(this.createNodesFromMetaObject(node.metaObj, childnode).ToArray());
            }
        }

        private void updateNodes(DbTreeNode node, bool childnode)
        {
            foreach (DbTreeNode cNode in node.Nodes)
            {
                updateTreeNodes(cNode, true);
            }
        }

        private List<DbTreeNode> createNodesFromMetaObject(MetaObject metaObject, bool childnode)
        {
            List<DbTreeNode> Nodes = new List<DbTreeNode>(); 
            foreach (MetaObject mo in metaObject.children)
            {
                DbTreeNode cNode = new DbTreeNode(mo.type, mo.value);

                cNode.metaObj = mo;

                if (!childnode)
                {
                    foreach (MetaObject childMo in mo.children)
                    {
                        DbTreeNode gcNode = new DbTreeNode(childMo.type, childMo.value);
                        gcNode.metaObj = childMo;
                        cNode.Nodes.Add(gcNode);
                    }
                }
                Nodes.Add(cNode);
            }
            return Nodes;
        }

        private void updateMappingInfo(DbTreeNode node)
        {
            if (node.metaObj.mappable)
            {
                // do sjizzle
                
            }
        }

        private void dbStructure_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dbStructure_MultipleItemDrag(object sender, MultiItemDragEventArgs e)
        {
            log.Debug(e.Items.Length.ToString());
            if (e.Button == MouseButtons.Left)
                DoDragDrop(e.Items, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);


        }


        private void dbStructure_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //    DoDragDrop(e.Item, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);


        }
    }
}
