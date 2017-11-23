using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public class CatalogMetaObject : AbstractDatabaseMetaObject
    {
        public CatalogMetaObject(IDataSourceMetadata source) : base(source)
        {
            this._type = "catalog";
        }

        protected override void getChildren()
        {
            
        }

    }
}
