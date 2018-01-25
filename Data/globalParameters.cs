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
        public static string dbPath;
        public static string secondDbPath;
        public static string dbName;
        public static string secondDbName;
        public static List<string> dbHistory = new List<string>();
        public static List<string> moduleType = new List<string>();
        //public static SQLiteConnection conn = null;
        //public static SQLiteConnection secondConn = null;
        public static string dbDirPath = @"C:\CloudMap\";
        public static string dbHistoryPath = dbDirPath+"history.ini";
        public static string tempDb = @"C:\CloudMap\tempppppppppppppp.db";
        public static string attachDb;

    }
}
