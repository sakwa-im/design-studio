using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pgDatabase
{
    public partial class ConnectionTrash : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ConnectionTrash));

        public ConnectionTrash(IPgConnectionStash stash)
        {
            InitializeComponent();

            this.stash = stash;

            dt = new DataTable();
            dt.Columns.Add(new DataColumn("selected", typeof(bool)));
            dt.Columns.Add(new DataColumn("name", typeof(string)));
            dt.Columns.Add(new DataColumn("server", typeof(string)));
            dt.Columns.Add(new DataColumn("database", typeof(string)));
            dt.Columns.Add(new DataColumn("cinco", typeof(IPgConnection)));

            foreach (IPgConnection con in stash.Connections)
            {
                DataRow dr = dt.NewRow();
                dr["selected"] = false;
                dr["name"] = con.Name;
                dr["server"] = con.Server;
                dr["database"] = con.Database;

                dr["cinco"] = con;

                dt.Rows.Add(dr);

            }

            dgConnections.AutoGenerateColumns = false;
            dgConnections.DataSource = dt;

        }

        DataTable dt = null;
        IPgConnectionStash stash = null;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string txt = "You're about to remove Cinco connections from your application." + Environment.NewLine + Environment.NewLine +
                "Do you really want to do this";

            DialogResult = MessageBox.Show(txt, "Trash", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (dt != null)
                    for (int row = 0; row < dgConnections.Rows.Count; row++)
                    {
                        DataGridViewRow dr = dgConnections.Rows[row];
                        if (dr.Cells["selected"].Value.ToString() == true.ToString())
                        {
                            IPgConnection con = dt.Rows[row]["cinco"] as IPgConnection;
                            if (con != null)
                            {
                                log.Debug(string.Format("Removing connection {0}", con.Name));
                                stash.RemoveConnection(con);

                            }
                        }
                    }
            }
        }

    }
}
