using configuration;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pgDatabase
{
    public class PgConnectionStash : IPgConnectionStash
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PgConnectionStash));
        public PgConnectionStash()
        {
        }

        List<string> IPgConnectionStash.PgConnections
        {
            get
            {
                List<string> result = new List<string>();
                foreach (string key in _DbSettings.Keys)
                    result.Add(key);

                return result;

            }
        }
        IPgConnection IPgConnectionStash.GetConnection(string name)
        {
            return name != "" ? _DbSettings[name] : null;
        }

        void IPgConnectionStash.AddConnection(IPgConnection connection)
        {
            if (_DbSettings.ContainsKey(connection.Name))
                _DbSettings[connection.Name] = connection;
            else
                _DbSettings.Add(connection.Name, connection);

        }
        void IPgConnectionStash.RemoveConnection(IPgConnection connection)
        {
            if (_DbSettings.ContainsKey(connection.Name))
                _DbSettings.Remove(connection.Name);
        }

        bool IPgConnectionStash.Contains(IPgConnection connection)
        {
            return _DbSettings.Keys.Contains<string>(connection.Name);
        }

        bool IPgConnectionStash.Contains(string connection)
        {
            return _DbSettings.Keys.Contains<string>(connection);

        }

        List<IPgConnection> IPgConnectionStash.Connections 
        {
            get 
            {
                List<IPgConnection> result = new List<IPgConnection>();

                foreach (IPgConnection con in _DbSettings.Values)
                    result.Add(con);

                return result;

            }
        }
        void IPgConnectionStash.PersistConnections(IConfigurationItem node, eConfigurationSource configurationSource)
        {
            foreach (IPgConnection dbCon in _DbSettings.Values)
            {
                if (dbCon.Persist)
                {
                    log.Debug("Persist database connection: " + dbCon.ToString());

                    IConfigurationItem item = new IConfigurationItemImpl("item" + Convert.ToString(node.ConfigurationItems.Count + 1), "", configurationSource);
                    node.AddConfigurationItem(item);

                    item.AddConfigurationItem(new IConfigurationItemImpl("name", dbCon.Name, configurationSource));
                    item.AddConfigurationItem(new IConfigurationItemImpl("server", dbCon.Server, configurationSource));
                    item.AddConfigurationItem(new IConfigurationItemImpl("port", dbCon.Port, configurationSource));
                    item.AddConfigurationItem(new IConfigurationItemImpl("database", dbCon.Database, configurationSource));
                    item.AddConfigurationItem(new IConfigurationItemImpl("user", dbCon.User, configurationSource));

                    IConfigurationItem keyIt = new IConfigurationItemImpl("password", "", configurationSource);
                    keyIt.StorageKey = "system";
                    keyIt.Configuration = node.Configuration;
                    keyIt.SetValue(dbCon.Password);
                    item.AddConfigurationItem(keyIt);

                }
            }
        }
        void IPgConnectionStash.RetreiveConnections(IConfigurationItem node)
        {
            foreach (IConfigurationItem item in node.ConfigurationItems)
            {
                IPgConnection dbCon = new PgConnection();
                foreach (IConfigurationItem it in item.ConfigurationItems)
                {
                    it.Configuration = node.Configuration;
                    switch (it.Name)
                    {
                        case "name": dbCon.Name = it.Value; break;
                        case "server": dbCon.Server = it.Value; break;
                        case "port": dbCon.Port = it.Value; break;
                        case "database": dbCon.Database = it.Value; break;
                        case "user": dbCon.User = it.Value; break;
                        case "password": dbCon.Password = it.GetValue(""); break;

                    }
                }
                dbCon.Persist = true;

                if (!_DbSettings.Keys.Contains<string>(dbCon.Name))
                {
                    log.Debug("Retrieve database connection: " + dbCon.ToString());

                    _DbSettings.Add(dbCon.Name, dbCon);
                }
            }
        }

        Dictionary<string, IPgConnection> _DbSettings = new Dictionary<string, IPgConnection>();

    }
}
