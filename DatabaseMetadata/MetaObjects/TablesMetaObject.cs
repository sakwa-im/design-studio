using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public class TablesMetaObject : AbstractDatabaseMetaObject
    {
        public TablesMetaObject(IDataSourceMetadata source) : base(source)
        {
            this.mappable = false;
            this._type = "tables";
            this._value = "tables";
        }

        protected override void getChildren()
        {
            this._children = this.source.GetChildren(this);
        }

        public override Type getChildType()
        {
            return typeof(TableMetaObject);
        }

    }
}
