using sakwa;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pgDatabase;

namespace pgDataSource
{
    [Export(typeof(IDataSourceFactory))]
    public class pgDataSource : IDataSourceFactory
    {
        public pgDataSource() { }

        eDataSourceType IDataSourceFactory.DataSourceType
        {
            get { return eDataSourceType.Database; }
        }

        List<IProperty> IDataSourceFactory.ConnectionProperties
        {
            get
            {
                List<IProperty> result = new List<IProperty>();

                IProperty pSettingsMode = new IPropertyStringImpl(
                    Constants.PropertySettingCaption, Constants.PropertySettingName, "pgDataSource",
                    new Constraint<string>(new string[] 
                        { Constants.PropertySettingUseGlobal,
                          Constants.PropertySettingUseThese}),
                    eAttributeTarget.DataConnection,
                    eAttributeRequirement.User | eAttributeRequirement.Optional,
                    Constants.PropertySettingUseGlobal, "Settings");

                IProperty pHost = new IPropertyStringImpl(
                    "Host", "dbHost", "pgDataSource",
                    new Constraint<string>(ConstraintTest.Any),
                    eAttributeTarget.DataConnection,
                    eAttributeRequirement.User | eAttributeRequirement.Mandatory,
                    "", "Database Connection");

                IProperty pPort = new IPropertyIntImpl(
                    "Port", "dbPort", "pgDataSource",
                    new Constraint<int>(ConstraintTest.Any),
                    eAttributeTarget.DataConnection,
                    eAttributeRequirement.User | eAttributeRequirement.Mandatory,
                    5432, "Database Connection");

                IProperty pDatabase = new IPropertyStringImpl(
                    "Database", "dbDatabase", "pgDataSource",
                    new Constraint<string>(ConstraintTest.Any),
                    eAttributeTarget.DataConnection,
                    eAttributeRequirement.User | eAttributeRequirement.Mandatory,
                    "", "Database Connection");

                IProperty pUser = new IPropertyStringImpl(
                    "User", "dbUser", "pgDataSource",
                    new Constraint<string>(ConstraintTest.Any),
                    eAttributeTarget.DataConnection,
                    eAttributeRequirement.User | eAttributeRequirement.Delayed,
                    "", "Database Connection");

                IProperty pPassword = new IPropertyStringImpl(
                    "Password", "dbPassword", "pgDataSource",
                    new Constraint<string>(ConstraintTest.Any),
                    eAttributeTarget.DataConnection,
                    eAttributeRequirement.User | eAttributeRequirement.Delayed | eAttributeRequirement.Password,
                    "", "Database Connection");

                result.Add(pPassword);
                result.Add(pUser);
                result.Add(pDatabase);
                result.Add(pPort);
                result.Add(pHost);

                result.Add(pSettingsMode);

                return result;

            }
        }

        IDataSource IDataSourceFactory.GetDataSource(List<IProperty> properties)
        {
            return new pgDataDefinition(properties);
        }
        string IDataSourceFactory.Name
        {
            get { return "Postgresql"; }
        }
        SakwaUserControl IDataSourceFactory.GetEditor(IBaseNode node)
        {
            return new ucPostgreslEditor(node);
        }

        private Dictionary<string, string> Parameters = new Dictionary<string, string>();

    }

    public class pgDataDefinition : IDataSource
    {
        private List<IProperty> _properties;

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
                // for now
                return this._properties;
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

        public pgDataDefinition(List<IProperty> properties)
        {
            this._properties = properties;


        }
        
        bool IDataSource.Open()
        {
            

            return true;
        }
        bool IDataSource.Close()
        {
            return false;
        }

        bool IDataSource.Persist()
        {
            return false;
        }

        private string getName()
        {
            return this.getProperty("Name").Value;
        }

        private string getServer()
        {
            return this.getProperty("Server").Value;
        }

        /*
         *  result.Name = _Name;

            result.Server = _Server;
            result.Port = _Port;

            result.Database = _Database;

            result.User = _User;
            result.Password = _Password;

            result.Persist = _Persist;
            */
        private IProperty getProperty(string name)
        {
            Predicate<IProperty> PredForName = (x => x.Name == name);
            return this._properties.Find(PredForName);
        }
    }
    
}
