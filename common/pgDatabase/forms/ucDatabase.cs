using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using configuration;

namespace pgDatabase
{
    public partial class ucDatabase : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ucDatabase));

        private IConfiguration conf = null;
        private eConfigurationSource configurationSource = eConfigurationSource.AllUsersAppData;

        public ucDatabase()
        {
            InitializeComponent();
        }

        public IConfiguration Configuration
        {
            get { return conf; }
            set { conf = value; }

        }

        public eConfigurationSource ConfigurationSource
        {
            get { return configurationSource; }
            set { configurationSource = value; }
        }
        public void SaveConfiguration()
        {
            DefineConfiguration();

            log.Debug(_ConfigurationName);

            IConfigurationItem dbNode = conf.GetConfigurationItem(_ConfigurationName);
            dbNode.Clear();
            _stash.PersistConnections(dbNode, configurationSource);
            conf.SetConfigurationValue(_PersistanceLastValue, cbxDatabase.Text);

        }
        public void LoadConfiguration()
        {
            DefineConfiguration();

            _stash.RetreiveConnections(conf.GetConfigurationItem(_ConfigurationName));

            cbxDatabase.Items.Clear();
            cbxDatabase.Items.AddRange(_stash.PgConnections.ToArray());
            
            string lastDb = conf.GetConfigurationValue(_PersistanceLastValue);
            if (cbxDatabase.Items.Contains(lastDb))
                cbxDatabase.Text = lastDb;

        }

        private void DefineConfiguration()
        {
            if (conf == null)
            {
                conf = ConfigurationRepository.IConfiguration;
            }

            conf.AddConfigurationItem("", new IConfigurationItemImpl(_ConfigurationName, "", configurationSource));
            conf.AddConfigurationItem("", new IConfigurationItemImpl(_PersistanceLastValue, "", configurationSource));
        }

        private void ucDatabase_Load(object sender, EventArgs e)
        {

        }
        
        private void picEdit_Click(object sender, EventArgs e)
        {
            EditPgConnection frm = new EditPgConnection();
            frm.CincoConnection = _stash.GetConnection(cbxDatabase.Text);
            frm.CincoConnectionStash = _stash;

            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                IPgConnection dbCon = frm.CincoConnection;
                _stash.AddConnection(dbCon);
                cbxDatabase.Text = dbCon.Name;

                log.Debug("Adding: " + dbCon.ToString());

            }

            cbxDatabase.Items.Clear();
            cbxDatabase.Items.AddRange(_stash.PgConnections.ToArray());
        
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines if the settings can be persisted")]
        public ePersistence Persistence
        {
            get { return _Persistence; }
            set { _Persistence = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines a mandatory persistance name")]
        public string ConfigurationName
        {
            get { return _ConfigurationName; }
            set { _ConfigurationName = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines a mandatory persistance name")]
        public string LastValue
        {
            get { return _PersistanceLastValue; }
            set { _PersistanceLastValue = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("Defines an optional prefix")]
        public string Prefix
        {
            get { return _PersistancePrefix; }
            set { _PersistancePrefix = value; }
        }

        public IPgConnection SelectedDatabase
        {
            get { return _SelectedDatabase; }
            set { _SelectedDatabase = value; }
        }

        [Category("Persistence"), RefreshProperties(RefreshProperties.All), Description("The event is raised when a database is selected")]
        public event DatabaseEventHandler OnDatabaseChanged;

        public IPgConnectionStash Stash { get { return _stash; } }
        public void AddConnection(IPgConnection connection)
        {
            _stash.AddConnection(connection);

            cbxDatabase.Items.Clear();
            cbxDatabase.Items.AddRange(_stash.PgConnections.ToArray());
            string lastDatabase = conf.GetConfigurationValue(_PersistanceLastValue);
            if(cbxDatabase.Items.Contains(lastDatabase))
                cbxDatabase.Text = lastDatabase;

        }

        private ePersistence _Persistence = ePersistence.None;
        private string _ConfigurationName = "databases";
        private string _PersistanceLastValue = "last-database";
        private string _PersistancePrefix = "";

        private IPgConnectionStash _stash = new PgConnectionStash();
        private IPgConnection _SelectedDatabase = null;

        private void cbxDatabase_TextChanged(object sender, EventArgs e)
        {
            if (OnDatabaseChanged != null)
            {
                IPgConnection connection = _stash.GetConnection(cbxDatabase.Text);
                if (connection != null && !connection.Equals(_SelectedDatabase))
                {
                    _SelectedDatabase = connection;
                    OnDatabaseChanged(this, new DatabaseEventArgs(connection));

                }
            }
        }
    }

    public class DatabaseEventArgs
    {
        public DatabaseEventArgs(IPgConnection connection)
        {
            _PgConnection = connection;
        }
        public IPgConnection PgConnection { get { return _PgConnection; } }

        private IPgConnection _PgConnection = null;

    }

    public delegate void DatabaseEventHandler(object sender, DatabaseEventArgs e);


}
