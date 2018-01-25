using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Data;
using System.IO;
using System.Data;

namespace DataAccess
{
    //针对整个数据库的操作，例如链接并打开数据库、新建数据库、保存和读取历史记录等
    public class SystemOperator
    {
        public static void NewProjectConnectDb(string dbSelfName, string dbSelfPath)
        {
            globalParameters.dbName = dbSelfName + ".db";
            globalParameters.dbPath = "Data Source = " + dbSelfPath + globalParameters.dbName;
            globalParameters.attachDb = dbSelfPath + globalParameters.dbName;
        }
        public static void NewProject(string dbSelfName, string dbSelfPath)
        {
            //新建项目时调用，建立数据库和数据表
            NewProjectConnectDb(dbSelfName, dbSelfPath);
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                string sq0 = "PRAGMA foreign_keys = 'on';";
                using (SQLiteCommand cmd = new SQLiteCommand(sq0, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                string sql = "CREATE TABLE IF NOT EXISTS modules(name varchar(50) PRIMARY KEY, " +
                "type varchar(50), level INTEGER, comment varchar(100) ); ";//建表语句
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string sq2 = "CREATE TABLE IF NOT EXISTS relation(sourceName STRING NOT NULL, " +
                    "targetName STRING NOT NULL, rname varchar(50), bidirection varchar(50), type varchar(50), " +
                    "comment varchar(100), PRIMARY KEY(sourceName, targetName),FOREIGN KEY (sourceName) " +
                    "REFERENCES modules(name) on delete cascade on update cascade, " +
                    "FOREIGN KEY(targetName) REFERENCES modules(name) on delete cascade on update cascade); ";//建表语句
                using (SQLiteCommand cmd = new SQLiteCommand(sq2, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string sq3 = "PRAGMA foreign_keys = 'on';";
                using (SQLiteCommand cmd = new SQLiteCommand(sq3, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }//创建数据库实例，指定文件位置
            string filePath = dbSelfPath + dbSelfName + ".db";
            EnqueueChecked(filePath);
            //CopyDb();
        }
        public static bool OpenProject(string filePath,bool first) 
        {
            //first表示是否是正常打开数据库，对于导入数据库时，相当于打开第二个数据库时，first为false
            if (!File.Exists(filePath))
            {
                globalParameters.dbHistory.Remove(filePath);
                return false;
            }
            else
            {
                if (first)
                {
                    string[] text = filePath.Split('\\');
                    globalParameters.dbName = text[text.Length - 1];
                    globalParameters.attachDb = filePath;
                    globalParameters.dbPath = "Data Source = " + filePath;
                    EnqueueChecked(filePath);
                    //CopyDb();
                    return true;
                }
                else
                {
                    string[] text = filePath.Split('\\');
                    globalParameters.secondDbName = text[text.Length - 1];
                    globalParameters.secondDbPath = filePath;
                    return true;
                }   
            }       
        }
        //public static void CopyDb()//创建副本数据库
        //{
        //    if (!File.Exists(globalParameters.tempDb))
        //    {
        //        string sql0 = "ATTACH DATABASE '" + globalParameters.tempDb + "' as 'db0'";
        //        ExecuteSql(sql0);
        //        string sql1 = "CREATE TABLE IF NOT EXISTS db0.modules(name varchar(50) PRIMARY KEY, " +
        //       "type varchar(50), level INTEGER, comment varchar(100) ); ";//建表语句
        //        ExecuteSql(sql1);
        //        string sq2 = "CREATE TABLE IF NOT EXISTS db0.relation(sourceName STRING NOT NULL, " +
        //            "targetName STRING NOT NULL, rname varchar(50), bidirection varchar(50), type varchar(50), " +
        //            "comment varchar(100), PRIMARY KEY(sourceName, targetName),FOREIGN KEY (sourceName) " +
        //            "REFERENCES modules(name) on delete cascade on update cascade, " +
        //            "FOREIGN KEY(targetName) REFERENCES modules(name) on delete cascade on update cascade); ";//建表语句
        //        ExecuteSql(sq2);
        //        //string sql3 = "create table db0.modules as select * from modules";
        //        string sql3 = "insert into db0.modules select * from modules";
        //        ExecuteSql(sql3);
        //        //string sql4 = "create table db0.relation as select * from relation";
        //        string sql4 = "insert into db0.relation select * from relation";
        //        ExecuteSql(sql4);
        //    }
        //    else
        //    {
        //        string sql0 = "ATTACH DATABASE '" + globalParameters.tempDb + "' as 'db0'";
        //        ExecuteSql(sql0);
        //        //string sql3 = "delete from db0.modules";
        //        string sql01 = "DROP TABLE db0.modules";
        //        ExecuteSql(sql01);
        //        string sql02 = "DROP TABLE db0.relation";
        //        ExecuteSql(sql02);
        //        string sql1 = "CREATE TABLE IF NOT EXISTS db0.modules(name varchar(50) PRIMARY KEY, " +
        //       "type varchar(50), level INTEGER, comment varchar(100) ); ";//建表语句
        //        ExecuteSql(sql1);
        //        string sq2 = "CREATE TABLE IF NOT EXISTS db0.relation(sourceName STRING NOT NULL, " +
        //            "targetName STRING NOT NULL, rname varchar(50), bidirection varchar(50), type varchar(50), " +
        //            "comment varchar(100), PRIMARY KEY(sourceName, targetName),FOREIGN KEY (sourceName) " +
        //            "REFERENCES modules(name) on delete cascade on update cascade, " +
        //            "FOREIGN KEY(targetName) REFERENCES modules(name) on delete cascade on update cascade); ";//建表语句
        //        ExecuteSql(sq2);
        //        string sql3 = "insert into db0.modules select * from modules";
        //        ExecuteSql(sql3);
        //        string sql4 = "insert into db0.relation select * from relation";
        //        ExecuteSql(sql4);
        //        //string sql1 = "insert into db0.modules select * from modules";
        //        //ExecuteSql(sql1);
        //        //string sql4 = "delete from db0.relation";
        //        //ExecuteSql(sql4);
        //        //string sql2 = "insert into db0.relation select * from relation";
        //        //ExecuteSql(sql2);
        //    }
        //    //////////复制数据库后，在复制的数据库内进行操作////////////////////////////////
        //    //CloseDb();
        //    globalParameters.dbPath = "Data Source = " + globalParameters.tempDb;
        //    //Connect_open_db();
        //    string sql = "ATTACH DATABASE '" + globalParameters.attachDb + "'" + " as 'realdb'";
        //    ExecuteSql(sql);
        //}
        //public static void SaveDb()
        //{
        //    ExecuteSql("delete from realdb.modules");
        //    string sql1 = "insert into realdb.modules select * from modules";
        //    ExecuteSql(sql1);
        //    ExecuteSql("delete from realdb.relation");
        //    string sql2 = "insert into realdb.relation select * from relation";
        //    ExecuteSql(sql2);
        //}
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
