using log4net;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pgDatabase
{
    public sealed class PgDatabase : IPgDatabase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PgDatabase));

        private PgDatabase() { }
        public static IPgDatabase Interface
        {
            get
            {
                if (_Interface == null)
                    _Interface = new PgDatabase();

                return _Interface;
            }
        }

        IPgConnection IPgDatabase.PgConnection
        {
            get
            {

                return _PgConnection;
            }
            set
            {
                if (_PgConnection.Name != value.Name)
                {
                    _Interface.Close();
                    _PgConnection = value;

                }
            }
        }


        private NpgsqlConnection _Connection = null;
        NpgsqlConnection IPgDatabase.Connection
        {
            get
            {
                if (_Connection == null)
                {
                    try
                    {
                        log.Debug("Trying to open: " + _PgConnection.ToString());
                        _Connection = new NpgsqlConnection(_PgConnection.ConnectionString);

                        _Connection.Open();

                        log.Debug("Database connection OK");

                    }
                    catch (Exception exc)
                    {
                        log.Debug(exc.ToString());
                        _Connection = null;

                    }
                }

                return _Connection;
            }
        }

        void IPgDatabase.Close()
        {
            if (_Connection != null)
            {
                log.Debug("Closing: " + _Connection.ToString());

                _Connection.Close();
                _Connection = null;
            }
        }

        bool IPgDatabase.IsConnectionLife
        {
            get
            {
                try
                {
                    DataTable dt = (this as IPgDatabase).GetDataTable("select true");
                    if (dt != null)
                        return dt.Rows.Count == 1;

                }
                catch (Exception exc)
                {
                    log.Debug(exc.ToString());
                }

                return false;
            }
        }

        DataTable IPgDatabase.GetDataTable(string sql)
        {
            DataTable result = null;
            if (_Interface.Connection != null)
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, _Connection);

                DataSet ds = new DataSet();
                da.Fill(ds);
                result = ds.Tables.Count > 0 ? ds.Tables[0] : null;

            }

            return result;

        }
        DataTable IPgDatabase.CacheTable(string sql, bool reload)
        {
            DataTable result = _CachedTables.Keys.Contains(sql)
                ? _CachedTables[sql]
                : null;

            if (reload || result == null)
            {
                if (result != null)
                    _CachedTables.Remove(sql);

                result = Interface.GetDataTable(sql);

                if (result != null)
                    _CachedTables.Add(sql, result);

            }

            return result;

        }
        int IPgDatabase.Value(Object fieldValue, int defaultValue)
        {
            return fieldValue == null || fieldValue.ToString() == ""
                ? defaultValue
                : (int)fieldValue;

        }
        string IPgDatabase.Value(Object fieldValue, string defaultValue)
        {
            return fieldValue == null
                ? defaultValue
                : fieldValue.ToString();

        }
        List<string> IPgDatabase.Value(Object fieldValue, List<string> defaultValue)
        {
            if (fieldValue == null)
                return defaultValue;

            List<string> result = new List<string>();
            try
            {
                result.AddRange((string[])fieldValue);
            }
            catch (Exception){}

            return result;

        }
        List<int> IPgDatabase.Value(Object fieldValue, List<int> defaultValue)
        {
            if (fieldValue == null)
                return defaultValue;

            List<int> result = new List<int>();
            try
            {
                result.AddRange((int[])fieldValue);
            }
            catch(Exception){}

            return result;

        }
        bool IPgDatabase.Value(Object fieldValue, bool defaultValue)
        {
            return fieldValue == null || fieldValue.ToString() == ""
                ? defaultValue
                : (bool)fieldValue;

        }

        private static IPgDatabase _Interface = null;
        private IPgConnection _PgConnection = new PgConnection();
        private Dictionary<string, DataTable> _CachedTables = new Dictionary<string, DataTable>();

    }
}
