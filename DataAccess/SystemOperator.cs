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
        public static bool OpenProject(string filePath)
        {
            if (!File.Exists(filePath))
            {
                globalParameters.dbHistory.Remove(filePath);
                return false;
            }
            else
            {
                string[] text = filePath.Split('\\');
                globalParameters.dbName = text[text.Length - 1];
                globalParameters.dbPath = "Data Source = " + filePath;
                Connect_open_db();
                EnqueueChecked(filePath);
                return true;
            }       
        }

        public static void ReadHistory()
        {
            string path = globalParameters.dbHistoryPath;
            if (File.Exists(path))
            {
                string[] strs1 = File.ReadAllLines(path);
                int length = strs1.Length;
                for (int i = 0; i < length; i++)
                {
                    globalParameters.dbHistory.Add(strs1[i]);
                    //Console.WriteLine(strs1[i]);
                }
            }
        }
        public static void EnqueueChecked(string filePath)
        {
            if (globalParameters.dbHistory.Count == 0)
            {
                globalParameters.dbHistory.Add(filePath);
            }
            else
            {
                bool have = false;
                foreach (string path in globalParameters.dbHistory)
                {
                    have = (path == filePath) || have;
                }
                if (have == false)
                    globalParameters.dbHistory.Add(filePath);
            }
        }

        public static void WriteHistory()
        {
            string dirPath = globalParameters.dbDirPath;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                // File.SetAttributes(dirPath, FileAttributes.Hidden);
            }
            string filePath = globalParameters.dbHistoryPath;
            if (!File.Exists(filePath))
            {
                FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs1);
                foreach (string oneHistory in globalParameters.dbHistory)
                {
                    sw.WriteLine(oneHistory);
                }
                sw.Flush();
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs2 = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                StreamWriter sw2 = new StreamWriter(fs2);

                foreach (string oneHistory in globalParameters.dbHistory)
                {
                    sw2.WriteLine(oneHistory);
                }
                for (int i = globalParameters.dbHistory.Count ; i < 10; i++)
                {
                    sw2.WriteLine("");
                }
                sw2.Flush();
                sw2.Close();
                fs2.Close();
            }
        }
    }

    
}
