using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SourceMeta
{ 
    abstract public class AbstractDatabaseMetaObject : MetaObject
    {
        public AbstractDatabaseMetaObject(IDataSourceMetadata source) : base(source)
        {
            
        }

    }
}
