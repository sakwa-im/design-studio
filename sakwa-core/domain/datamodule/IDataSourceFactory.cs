using System.Collections.Generic;

namespace sakwa
{
    public enum eDataSourceType { Database, SakwaSocket, XmlFile, DataFile, Webservice, User}
    public interface IDataSourceFactory
    {
        string Name { get; }
        eDataSourceType DataSourceType { get; }
        List<IProperty> ConnectionProperties { get; }
        IDataSource GetDataSource(List<IProperty> properties);
        SakwaUserControl GetEditor(IBaseNode node);

    }
}
