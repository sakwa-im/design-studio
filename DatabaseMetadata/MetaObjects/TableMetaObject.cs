using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public class TableMetaObject : AbstractDatabaseMetaObject
    {
        public TableMetaObject(IDataSourceMetadata source) : base(source)
        {
            this._type = "table";
        }

        protected override void getChildren()
        {
            MetaObject mo = new ColumnsMetaObject(this.source);
            mo.parent = this;
            this._children.Add(mo);
        }
    }
}
