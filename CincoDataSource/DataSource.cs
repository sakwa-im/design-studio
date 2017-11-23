using System;
using sakwa;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HostDataSource
{
    [Export(typeof(IDataSourceFactory))]
    public class CincoDataSource : IDataSourceFactory
    {
        public CincoDataSource() { }

        eDataSourceType IDataSourceFactory.DataSourceType
        {
            get { return eDataSourceType.SakwaSocket; }
        }
        List<IProperty> IDataSourceFactory.ConnectionProperties
        {
            get
            {
                List<IProperty> result = new List<IProperty>();

                return result;

            }
        }
        IDataSource IDataSourceFactory.GetDataSource(List<IProperty> properties)
        {
            return new CincoDataConnection(properties);
        }
        string IDataSourceFactory.Name
        {
            get { return "Host"; }
        }
        SakwaUserControl IDataSourceFactory.GetEditor(IBaseNode node)
        {
            return new ucCincoSocketEditor(node);
        }

        private Dictionary<string, string> Parameters = new Dictionary<string, string>();

    }

    public class CincoDataConnection : IDataSource
    {
        IDataSourceFactory IDataSource.DataSourceFactory
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        List<IProperty> IDataSource.ConnectionProperties
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        string IDataSource.GetPropertyValue(string property)
        {
            return null;
        }

        string IDataSource.PropertiesAsJson(bool safeString)
        {
            throw new NotImplementedException();
        }

        IDataSourceManager IDataSource.DataSourceManager
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public CincoDataConnection(List<IProperty> properties)
        {
        }

        bool IDataSource.Open()
        {
            return false;
        }
        bool IDataSource.Close()
        {
            return false;
        }

        bool IDataSource.Persist()
        {
            return false;
        }

    }
}
