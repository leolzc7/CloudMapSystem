using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Data;

namespace DataAccess
{
    public class SystemOperator
    {
        public static void Connect_open_db()
        {
            globalParameters.conn = new SQLiteConnection(globalParameters.dbPath);//创建数据库实例，指定文件位置
            globalParameters.conn.Open();//打开数据库，若文件不存在会自动创建
        }
        public static void NewProjectConnectDb(string dbSelfName, string dbSelfPath)
        {
            globalParameters.dbName = dbSelfName + ".db";
            globalParameters.dbPath = "Data Source = " + dbSelfPath + globalParameters.dbName;
            Connect_open_db();
        }
        public static void ExecuteSql(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, globalParameters.conn);
            cmd.ExecuteNonQuery();
        }
        public static void NewProject(string dbSelfName, string dbSelfPath)
        {
            //新建项目时调用，建立数据库和数据表
            NewProjectConnectDb(dbSelfName, dbSelfPath);
            string sql = "CREATE TABLE IF NOT EXISTS modules(name varchar(50) PRIMARY KEY, " +
                "type varchar(50), level INTEGER, comment varchar(100) ); ";//建表语句
            ExecuteSql(sql);

            string sq2 = "CREATE TABLE IF NOT EXISTS relation(sourceName STRING NOT NULL, " +
                "targetName STRING NOT NULL, rname varchar(50), bidirection varchar(50), type varchar(50), " +
                "comment varchar(100), PRIMARY KEY(sourceName, targetName),FOREIGN KEY (sourceName) " +
                "REFERENCES modules(name) on delete cascade on update cascade, " +
                "FOREIGN KEY(targetName) REFERENCES modules(name) on delete cascade on update cascade); ";//建表语句
            ExecuteSql(sq2);

            string sq3 = "PRAGMA foreign_keys = 'on';";
            ExecuteSql(sq3);
        }

        public static void OpenProject(string filePath)
        {
            string[] text = filePath.Split('\\');
            globalParameters.dbName = text[text.Length - 1];
            globalParameters.dbPath = "Data Source = " + filePath;
            Connect_open_db();
            string sq3 = "PRAGMA foreign_keys = 'on';";
            ExecuteSql(sq3);
        }

    }
}
