using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class IDataSourceImpl : IBaseNodeImpl, IDataSource
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(IBaseNodeImpl));

        protected IDataSource IDataSourceInterface { get { return this as IDataSource; } }

        bool IDataSource.Close()
        {
            throw new NotImplementedException();
        }

        bool IDataSource.Open()
        {
            throw new NotImplementedException();
        }

        bool IDataSource.Persist()
        {
            throw new NotImplementedException();
        }

        List<IProperty> IDataSource.ConnectionProperties
        {
            get
            {
                return _ConnectionProperties;
            }
        }
        string IDataSource.GetPropertyValue(string property)
        {
            IProperty prop = getProperty(property);
            return prop != null ? prop.Value : "";
        }


        string IDataSource.PropertiesAsJson(bool safeString)
        {
            return coreSupport.PropertiesAsJson(_ConnectionProperties, safeString);
        }

        IDataSourceManager IDataSource.DataSourceManager
        {
            get
            {
                return _DataSourceManager;
            }

            set
            {
                _DataSourceManager = value;
            }
        }

        IDataSourceFactory IDataSource.DataSourceFactory
        {
            get
            {
                return _DataSourceFactory;
            }

            set
            {
                if (value != _DataSourceFactory)
                {
                    _DataSourceFactory = value;

                    if (_DataSourceFactory != null)
                        _ConnectionProperties = _DataSourceFactory.ConnectionProperties;

                    OnUpdated();
                }
            }
        }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    string dataSource = _DataSourceFactory != null
                        ? _DataSourceFactory.Name
                        : "";

                    persistence.UpsertField(Constants.DataNode_DataSourceFactory, dataSource);

                    persistence.UpsertField(Constants.DataNode_DataSourceProperties, 
                        coreSupport.PropertiesAsJson(_ConnectionProperties));

                    break;

            }

            return true;

        }
        protected override bool Retrieve(IPersistence persistence, ref ePersistence phase)
        {
            base.Retrieve(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    string dataSourceFactory = persistence.GetFieldValue(Constants.DataNode_DataSourceFactory, "");
                    if (dataSourceFactory != "" && _DataSourceManager != null)
                    {
                        _DataSourceFactory = _DataSourceManager.GetDataSourceFactory(dataSourceFactory);
                        if (_DataSourceFactory != null)
                            _ConnectionProperties = _DataSourceFactory.ConnectionProperties;

                    }

                    string json = persistence.GetFieldValue(Constants.DataNode_DataSourceProperties, "");
                    if (json != "")
                    {
                        try
                        {
                            Dictionary<string, string> props = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                            foreach (string name in props.Keys)
                            {
                                IProperty prop = getProperty(name);
                                if (prop != null)
                                    prop.Value = props[name];

                            }
                        }
                        catch (Exception ex)
                        {
                            log.Debug(ex.ToString());
                        }
                    }

                    break;
            }

            return true;

        }

        protected IProperty getProperty(string name)
        {
            Predicate<IProperty> PredForName = (x => x.Name == name);
            return this._ConnectionProperties.Find(PredForName);
        }

        protected IDataSourceFactory _DataSourceFactory = null;
        protected IDataSourceManager _DataSourceManager = null;
        protected List<IProperty> _ConnectionProperties = new List<IProperty>();

    }
}
