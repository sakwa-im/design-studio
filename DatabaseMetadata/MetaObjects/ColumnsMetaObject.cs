using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public class ColumnsMetaObject : AbstractDatabaseMetaObject
    {
        public ColumnsMetaObject(IDataSourceMetadata source) : base(source)
        {
            this.mappable = false;
            this._type = "columns";
            this._value = "columns";
        }

        protected override void getChildren()
        {
            this._children = this.source.GetChildren(this);
        }

        public override Type getChildType()
        {
            return (typeof(ColumnMetaObject));
        }

    }
}
