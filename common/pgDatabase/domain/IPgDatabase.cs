using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace pgDatabase
{
    public interface IPgDatabase
    {
        IPgConnection PgConnection { get; set; }

        NpgsqlConnection Connection { get; }
        void Close();

        bool IsConnectionLife { get; }

        DataTable GetDataTable(string sql);
        DataTable CacheTable(string sql, bool reload = false);

        int Value(Object fieldValue, int defaultValue);
        string Value(Object fieldValue, string defaultValue);
        bool Value(Object fieldValue, bool defaultValue);
        List<string> Value(Object fieldValue, List<string> defaultValue);
        List<int> Value(Object fieldValue, List<int> defaultValue);

    }
}
