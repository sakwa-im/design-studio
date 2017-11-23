using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public class SchemasMetaObject : AbstractDatabaseMetaObject
    {
        public SchemasMetaObject(IDataSourceMetadata source) : base(source)
        {
            this.mappable = false;
            this._type = "schemas";
            this._value = "schemas";
        }

        protected override void getChildren()
        {
            this._children = this.source.GetChildren(this);
        }

        public override Type getChildType()
        {
            return typeof(SchemaMetaObject);
        }
    }
}
