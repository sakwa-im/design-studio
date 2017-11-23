using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public class ColumnMetaObject : AbstractDatabaseMetaObject
    {
        public ColumnMetaObject(IDataSourceMetadata source) : base(source)
        {
            this._type = "column";
        }
        
        public override List<MetaInfoViewRow> MetaInfo
        {
            get
            {
                DataRow dat = (this.source as SqlMetadataImpl).getMetaInfo(this);

                List<MetaInfoViewRow> list = new List<MetaInfoViewRow>();
                MetaInfoViewRow metaInfoViewRow = new MetaInfoViewRow();

                metaInfoViewRow.Name = dat.Field<string>("column_name");
                metaInfoViewRow.Type = dat.Field<string>("data_type");

                metaInfoViewRow.Description = "<placeholder>";
                metaInfoViewRow.Relation = "<placeholder>";
                metaInfoViewRow.Value = "<placeholder>";

                list.Add(metaInfoViewRow);
                return list;
            }
        }



    }
}
