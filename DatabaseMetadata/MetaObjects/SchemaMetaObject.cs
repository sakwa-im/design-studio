using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public class SchemaMetaObject : AbstractDatabaseMetaObject
    {
        public SchemaMetaObject(IDataSourceMetadata source) : base(source)
        {
            this._type = "schema";
        }

        protected override void getChildren()
        {
            MetaObject moTable = new TablesMetaObject(this.source);
            moTable.parent = this;
            this._children.Add(moTable);

            MetaObject moView = new ViewsMetaObject(this.source);
            moView.parent = this;
            this._children.Add(moView);
        }

    }
}
