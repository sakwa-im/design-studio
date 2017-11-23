using configuration;
using System;
using System.Windows.Forms;
using log4net;

namespace pgDatabase
{
    public partial class EditPgConnection : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EditPgConnection));

        public EditPgConnection()
        {
            InitializeComponent();

            picTest.Image = imageList.Images[0];
            toolTip.SetToolTip(picTest, "Test connection");

            picTrash.Image = imageList.Images[3];
            toolTip.SetToolTip(picTrash, "Remove connections");

        }

        public IPgConnection CincoConnection
        {
            get
            {
                IPgConnection result = new PgConnection();
                result.Name = tbxName.Text;
                result.Server = tbxServer.Text;
                //result.Port = "";
                result.Database = tbxDatabase.Text;
                result.User = tbxUser.Text;
                result.Password = tbxPassword.Text;
                result.Persist = chxKeepSettings.Checked;
                return result;

            }
            set
            {
                if (value != null)
                {
                    tbxName.Text = value.Name;
                    tbxServer.Text = value.Server;
                    tbxDatabase.Text = value.Database;
                    tbxUser.Text = value.User;
                    tbxPassword.Text = value.Password;
                    chxKeepSettings.Checked = false;
                }

                _OrgConnection = value;

            }
        }
        public IPgConnectionStash CincoConnectionStash
        {
            get { return stash; }
            set
            {
                stash = value;
                if (stash != null)
                    picTrash.Image = imageList.Images[4];

            }
        }

        private void tbx_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = tbxName.Text != "" &&
                tbxServer.Text != "" &&
                tbxDatabase.Text != "" &&
                tbxUser.Text != "";

            if (!CincoConnection.Equals(_OrgConnection))
                picTest.Image = imageList.Images[0];

        }

        private void picTest_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            IPgDatabase db = PgDatabase.Interface;
            IPgConnection tmp = db.PgConnection;
            db.PgConnection = CincoConnection;

            if (db.IsConnectionLife)
                picTest.Image = imageList.Images[2];
            else
                picTest.Image = imageList.Images[1];

            _OrgConnection = CincoConnection;
            db.PgConnection = tmp;

            Cursor = Cursors.Default;

        }

        private void picTrash_Click(object sender, EventArgs e)
        {
            ConnectionTrash frm = new ConnectionTrash(stash);
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                IConfiguration conf = ConfigurationRepository.IConfiguration;
                IConfigurationItem dbConfigs = conf.GetConfigurationItem("databases");
                dbConfigs.Clear();
                stash.PersistConnections(dbConfigs);

                conf.Save();

            }
        }
 
        private IPgConnection _OrgConnection = null;
        private IPgConnectionStash stash = null;

    }
}
