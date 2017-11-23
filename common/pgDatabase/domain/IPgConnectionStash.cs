using configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pgDatabase
{
    public interface IPgConnectionStash
    {
        List<string> PgConnections { get; }
        IPgConnection GetConnection(string name);
        void AddConnection(IPgConnection connection);
        void RemoveConnection(IPgConnection connection);
        bool Contains(IPgConnection connection);
        bool Contains(string connection);

        List<IPgConnection> Connections { get; }

        void PersistConnections(IConfigurationItem node, eConfigurationSource configurationSource = eConfigurationSource.AllUsersAppData);
        void RetreiveConnections(IConfigurationItem node);

    }
}
