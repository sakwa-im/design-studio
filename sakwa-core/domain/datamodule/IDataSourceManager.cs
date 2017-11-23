using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public interface IDataSourceManager
    {
        List<IDataSourceFactory> DataSourceFactories { get; }
        IDataSourceFactory GetDataSourceFactory(string name);

    }
}
