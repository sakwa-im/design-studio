using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public interface IDataSource
    {
        IDataSourceFactory DataSourceFactory { get; set; }
        List<IProperty> ConnectionProperties { get; }
        string GetPropertyValue(string property);
        string PropertiesAsJson(bool safeString = false);
        IDataSourceManager DataSourceManager { get; set; }

        bool Open();
        bool Close();
        bool Persist();

    }
}
