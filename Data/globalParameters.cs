using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Data
{
    //记录关于数据库的全局变量
    public class globalParameters
    {
        public static string dbPath; //要操作的数据库的链接地址
        public static string secondDbPath; //导入的数据库的链接地址
        public static string dbName; // 操作的数据库的名字
        public static string secondDbName; //导入的数据库的名字
        public static string backupDbPath; //副本数据库的地址
        public static List<string> dbHistory = new List<string>();
        public static string dbDirPath = @"C:\CloudMap\";
        public static string dbHistoryPath = dbDirPath+"history.ini";
        public static string tempDb = @"C:\CloudMap\tempppppppppppppp.db";
    }
}
