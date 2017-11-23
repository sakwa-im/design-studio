using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceMeta
{
    public sealed class SqlTypeQueryMapping
    {
        private static SqlTypeQueryMapping _instance = null;

        public static SqlTypeQueryMapping instance {
            get {
                if (_instance == null)
                {
                    _instance = new SqlTypeQueryMapping();
                }
                return _instance;
            }
        }

        private Dictionary<System.Type, string> queries = new Dictionary<System.Type, string>();

        private Dictionary<System.Type, string> metaQueries = new Dictionary<System.Type, string>();

        public static Dictionary<System.Type, string> queryMapping
        {
            get
            {
                Dictionary<Type, string> mappingCopy = new Dictionary<Type, string>();
                foreach (Type n in SqlTypeQueryMapping.instance.queries.Keys)
                {
                    mappingCopy.Add(n, instance.queries[n]);
                }

                return mappingCopy;
            }
        }

        public static Dictionary<System.Type, string> metaQueryMapping
        {
            get
            {
                Dictionary<Type, string> mappingCopy = new Dictionary<Type, string>();
                foreach (Type n in SqlTypeQueryMapping.instance.metaQueries.Keys)
                {
                    mappingCopy.Add(n, instance.metaQueries[n]);
                }

                return mappingCopy;
            }
        }

        private SqlTypeQueryMapping()
        {
            this.queries.Add(typeof(DatabaseMetaObject), "SELECT catalog_name AS database FROM information_schema.information_schema_catalog_name ORDER BY catalog_name");
            this.queries.Add(typeof(SchemaMetaObject), "SELECT schema_name AS schema FROM information_schema.schemata WHERE catalog_name = '%s' ORDER BY schema_name");
            this.queries.Add(typeof(TableMetaObject), "SELECT table_name AS table FROM information_schema.tables WHERE table_catalog = '%s' AND table_schema = '%s' AND table_type != 'VIEW' ORDER BY table_name");
            this.queries.Add(typeof(ViewMetaObject), "SELECT table_name AS view FROM information_schema.views WHERE table_catalog = '%s' AND table_schema = '%s' ORDER BY table_name");
            this.queries.Add(typeof(ColumnMetaObject), "SELECT column_name AS column FROM information_schema.columns WHERE table_catalog = '%s' AND table_schema = '%s' AND table_name = '%s' ORDER BY table_name");

            this.metaQueries.Add(typeof(ColumnMetaObject), "SELECT * FROM information_schema.columns WHERE table_catalog = '%s' AND table_schema = '%s' AND table_name = '%s' AND column_name = '%s'");
            //this.metaQueries.Add(typeof(TableMetaObject), "SELECT * FROM information_schema.constraint_column_usage JOIN information_schema.table_constraints ON(table_constraints.constraint_name = constraint_column_usage.constraint_name) WHERE constraint_type = 'FOREIGN KEY'");
        }

        public static Type rootNodeType()
        {
            return SqlTypeQueryMapping.instance.queries.Keys.First<Type>();
        }
        
    }
        
}
