using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Data;
using System.IO;

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

            string filePath = dbSelfPath + dbSelfName + ".db";
            EnqueueChecked(filePath);
        }
        public static void OpenProject(string filePath)
        {
            string[] text = filePath.Split('\\');
            globalParameters.dbName = text[text.Length - 1];
            globalParameters.dbPath = "Data Source = " + filePath;
            Connect_open_db();
            EnqueueChecked(filePath);
        }
        public static void WriteHistory()
        {
            //string path = @"C:\CloudMap\history.ini";
            StringBuilder sb = new StringBuilder();
            foreach (string oneHistory in globalParameters.dbHistory)
            {
                sb.AppendLine(oneHistory);
            }
            File.WriteAllText(globalParameters.dbHistoryPath, sb.ToString());
        }

        public static void ReadHistory()
        {
            string path = globalParameters.dbHistoryPath;
            if (File.Exists(path))
            {
                //StreamReader sr = new StreamReader(path);
                string[] strs1 = File.ReadAllLines(path);
                int length = strs1.Length;
                for (int i = 0; i < length; i++)
                {
                    globalParameters.dbHistory.Enqueue(strs1[i]);
                    Console.WriteLine(strs1[i]);
                }
            }
        }
        public static void EnqueueChecked(string filePath)
        {
            if (globalParameters.dbHistory.Count == 0)
            {
                globalParameters.dbHistory.Enqueue(filePath);
            }
            else
            {
                bool have = false;
                foreach (string path in globalParameters.dbHistory)
                {
                    have = (path == filePath) || have;
                }
                if (have == false)
                    globalParameters.dbHistory.Enqueue(filePath);
            }
        }
    }
}
