using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace stuScore
{
    public class SqlServerHelper
    {
        private readonly string conStr;

        //替换成自己的数据库
        public static string server = "192.168.1.105,9443";
        public static string database = "tsglxt";
        public static string uid = "sa";
        public static string pwd = "JL@881103l";

        public SqlServerHelper()
        {
            conStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};",server,database,uid,pwd);
        }

        public static string GetMasterConStr()
        {
            return string.Format("Server={0};Database={1};Uid={2};Pwd={3};", server, "master", uid, pwd);
        }

        public SqlServerHelper(string conStr)
        {
            this.conStr = conStr;
        }

        public System.Data.SqlClient.SqlConnection CreateCon()
        {
            return new System.Data.SqlClient.SqlConnection(conStr);
        }

        public int ExecuteSql(string sql)
        {
            using (var con = CreateCon())
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = sql;
                return cmd.ExecuteNonQuery();
            }
        }
        
        public DataTable QuerySqlDataTable(string sql)
        {
            using (var con = CreateCon())
            {
                DataTable table = new DataTable();
                System.Data.SqlClient.SqlDataAdapter dapt = new System.Data.SqlClient.SqlDataAdapter(sql, con);
                dapt.Fill(table);
                return table;
            }
        }

        public DataSet QuerySqlDataSet(string sql)
        {
            using (var con = CreateCon())
            {
                DataSet ds = new DataSet();
                System.Data.SqlClient.SqlDataAdapter dapt = new System.Data.SqlClient.SqlDataAdapter(sql, con);
                dapt.Fill(ds);
                return ds;
            }
        }
    }

    public static class SqlServerHelperExt
    {
        public static int ExecuteSql(this System.Data.SqlClient.SqlConnection con, string sql, System.Data.SqlClient.SqlTransaction trans)
        {
            var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Transaction = trans;
            return cmd.ExecuteNonQuery();
        }
    }
}
