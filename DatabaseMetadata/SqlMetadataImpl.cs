using log4net;
using pgDatabase;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SourceMeta
{
    [Export(typeof(IDataSourceMetadata))]
    public class SqlMetadataImpl : IDataSourceMetadata
    {
        private Dictionary<System.Type,string> _queries = new Dictionary<System.Type, string>();
        
        private List<MetaObject> _list = new List<MetaObject>();

        private IPgDatabase _database = null;

        private static readonly ILog log = LogManager.GetLogger(typeof(SqlMetadataImpl));

        public SqlMetadataImpl(IPgDatabase database)
        {
            this._database = database;
        }

        List<MetaObject> IDataSourceMetadata.GetRoot()
        {
            // build schemas 
            System.Type NodeType = SqlTypeQueryMapping.rootNodeType();

            MetaObject root = this.CreateNode(NodeType);
            root.value = this._database.PgConnection.Database;

            List<MetaObject> result = new List<MetaObject>();
            result.Add(root);
            return result;
        }

        private MetaObject CreateNode(System.Type NodeType)
        {
            MetaObject node = (MetaObject)Assembly.GetExecutingAssembly().CreateInstance(
                    NodeType.FullName, //typeName
                    false, //ignorecase
                    new BindingFlags(), // bindingAttr
                    null, // binder
                    new[] { this }, //args
                    null, // culture
                    null  // activationAttributes
                    );

            return node;
        } 

        List<MetaObject> IDataSourceMetadata.GetChildren(MetaObject metaObject)
        {
            //prepare query
            string query = SqlTypeQueryMapping.queryMapping[metaObject.getChildType()];

            List<MetaObject> l = this.createCollectionFromQuery(
                recursiveComposeQuery(query, metaObject),
                metaObject.getChildType()
            );    

            foreach (MetaObject mo in l)
            {
                mo.parent = metaObject;
            }
            return l;
        }

        private List<MetaObject> createCollectionFromQuery(string query, System.Type NodeType)
        {
            List<MetaObject> l = new List<MetaObject>();

            DataTable dat = (this._database.GetDataTable(query) as DataTable);
            foreach (DataRow row in dat.Rows)
            {
                MetaObject mo = this.CreateNode(NodeType);
                mo.value = row.Field<string>(mo.type);
                l.Add(mo);
            }
            return l;
        }

        private string recursiveComposeQuery(string queryString, MetaObject metaObject, bool include = false)
        {
            string query = queryString;

            MetaObject parent = (include) ? metaObject : metaObject.parent;
            List<string> replace = new List<string>();
            do
            {
                if (parent != null && parent.mappable)
                {
                    replace.Add(parent.value);
                }
                parent = parent.parent;
            } while (parent != null);
            
            //@TODO
            //string table, fields, clause = "";
            //System.Collections.Generic.List<string> params = new List<string>(new string[] {fields, table, clause}});
            //string.Format("SELECT {0} FROM {1} WHERE {0}", params.ToArray());

            replace.Reverse();

            Regex regex = new Regex("%s");
            foreach (string val in replace)
            {
                //replace first occurrense of %s in query
                query = regex.Replace(query, val, 1);
            }
            return query;
        }
        
        private void addListToCollection(List<MetaObject> list, MetaObject obj)
        {
            foreach (MetaObject mo in list)
            {
                mo.parent = obj;
                if (!_list.Contains(mo))
                {
                    _list.Add(mo);
                }
            }
        }

        public DataRow getMetaInfo(MetaObject obj)
        {
            string query = SqlTypeQueryMapping.metaQueryMapping[obj.GetType()];

            DataTable dat = (this._database.GetDataTable(this.recursiveComposeQuery(query, obj, true)) as DataTable);
            return dat.Rows[0];
        }
    }
}
