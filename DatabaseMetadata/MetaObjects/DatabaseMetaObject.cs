using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public class DatabaseMetaObject : AbstractDatabaseMetaObject
    {
        public DatabaseMetaObject(IDataSourceMetadata source) : base(source)
        {
            this._type = "database";
        }

        protected override void getChildren()
        {
            MetaObject mo = new SchemasMetaObject(this.source);
            mo.parent = this;
            this._children.Add(mo);
        }

    }
}
