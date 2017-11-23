using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;


namespace sakwa
{
    [Export(typeof(IDataSourceManager))]
    public class IDataSourceManagerImpl : IDataSourceManager, IPartImportsSatisfiedNotification
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(IDataSourceManagerImpl));

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<IDataSourceFactory> DataSourceFactories { get; set; }

        public IDataSourceManagerImpl() { }

        List<IDataSourceFactory> IDataSourceManager.DataSourceFactories
        {
            get
            {
                List<IDataSourceFactory> result = new List<IDataSourceFactory>();
                result.AddRange(DataSourceFactories.ToArray<IDataSourceFactory>());
                return result;
            }
        }
        IDataSourceFactory IDataSourceManager.GetDataSourceFactory(string name)
        {
            foreach (IDataSourceFactory ds in DataSourceFactories)
                if (ds.Name == name)
                    return ds;

            return null;
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
            log.Debug("Parts loaded");
        }
    }
}
