using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceMeta
{
    public interface IDataSourceMetadata
    {
        List<MetaObject> GetRoot();
        List<MetaObject> GetChildren(MetaObject metaObject);
    }
}
