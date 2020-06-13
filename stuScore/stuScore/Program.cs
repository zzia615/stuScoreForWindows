using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace stuScore
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitTables();
            Application.Run(new Form1());
        }

        static void InitTables()
        {
            SqlServerHelper Sql_Master = new SqlServerHelper(SqlServerHelper.GetMasterConStr());

            var table = Sql_Master.QuerySqlDataTable("select * From master.dbo.sysdatabases where name='"+SqlServerHelper.database+"'");
            if (table.Rows.Count <= 0)
            {
                Sql_Master.ExecuteSql("Create Database " + SqlServerHelper.database);

                SqlServerHelper Sql = new SqlServerHelper();
                string[] sqlTmp = new string[]
                {
                    "create table yhxx" +
                    "(yhzh varchar(20) not null," +//用户账号
                    " yhxm varchar(20) not null," +//用户姓名
                    " yhmm varchar(60) not null," +//用户密码
                    " yhlb varchar(20) not null," +//用户类别 管理员 学生 教师
                    " yhxb varchar(5) null," +   //用户性别
                    " xszy varchar(100) null," + //学生专业
                    " xsnj varchar(100) null," + //学生年级
                    " constraint pk_yhxx primary key(yhzh)" +
                    ")",
                    "create table kcxx" +
                    "(kcbh varchar(20) not null," +//课程编号
                    " kcmc varchar(100) not null," +//课程名称
                    " constraint pk_kcxx primary key(kcbh)" +
                    ")",
                    "create table xscj" +
                    "(yhzh varchar(20) not null," +//用户账号
                    " kcbh varchar(20) not null," +//课程编号
                    " kccj numeric(14,1) null," +//课程成绩
                    " constraint pk_xscj primary key(yhzh,kcbh)" +
                    ")"
                };

                foreach (var sql in sqlTmp)
                {
                    Sql.ExecuteSql(sql);
                }
            }
        }
    }
}
