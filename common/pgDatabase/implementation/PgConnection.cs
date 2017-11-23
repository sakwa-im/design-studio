using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pgDatabase
{
    public class PgConnection : IPgConnection
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PgConnection));
        
        public PgConnection() { }
        public PgConnection(string name, string server, string database, string user, string password)
        {
            _Name = name;
            _Server = server;
            _Database = database;
            _User = user;
            _Password = password;

        }

        string IPgConnection.Name { get { return _Name; } set { _Name = value; } }
        string IPgConnection.Server { get { return _Server; } set { _Server = value; } }
        string IPgConnection.Port { get { return _Port; } set { _Port = value; } }
        string IPgConnection.Database { get { return _Database; } set { _Database = value; } }
        string IPgConnection.User { get { return _User; } set { _User = value; } }
        string IPgConnection.Password { get { return _Password; } set { _Password = value; } }
        bool IPgConnection.Persist { get { return _Persist; } set { _Persist = value; } }
        IPgConnection IPgConnection.Clone()
        {
            IPgConnection result = new PgConnection();
            result.Name = _Name;

            result.Server = _Server;
            result.Port = _Port;

            result.Database = _Database;

            result.User = _User;
            result.Password = _Password;

            result.Persist = _Persist;

            return result;
        }
        bool IPgConnection.Equals(IPgConnection input)
        {
            if (input == null)
                return false;

            return input.Name == _Name
                && input.Server == _Server
                && input.Port == _Port
                && input.Database == _Database
                && input.User == _User
                && input.Password == _Password;

        }

        string IPgConnection.ConnectionString
        {
            get
            {
                return _Password != ""
                    ? string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4}",
                            _Server, _Port, _User, _Password, _Database)
                    : string.Format("Server={0};Port={1};User Id={2};Database={3}",
                            _Server, _Port, _User, _Database);
            }
        }

        string IPgConnection.ToString()
        {
            return string.Format("Server={0};Port={1};User Id={2};Database={3}",
                            _Server, _Port, _User, _Database);
        }

        private string _Name = "";
        private string _Server = "";
        private string _Port = "5432";
        private string _Database = "";
        private string _User = "";
        private string _Password = "";
        private bool _Persist = false;

    }
}
