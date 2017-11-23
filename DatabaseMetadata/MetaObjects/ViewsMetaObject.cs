using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public class ViewsMetaObject : AbstractDatabaseMetaObject
    {
        public ViewsMetaObject(IDataSourceMetadata source) : base(source)
        {
            this.mappable = false;
            this._type = "views";
            this._value = "views";
        }

        protected override void getChildren()
        {
            this._children = this.source.GetChildren(this);
        }

        public override Type getChildType()
        {
            return (typeof(ViewMetaObject));
        }

    }
}
