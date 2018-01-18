using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Data
{
    public class globalParameters
    {
        public static string dbPath;
        public static string secondDbPath;
        public static string dbName;
        public static string secondDbName;
        public static List<string> dbHistory = new List<string>();
        //public static Queue<string> dbHistory = new Queue<string> ();
        public static SQLiteConnection conn = null;
        public static string dbDirPath = @"C:\CloudMap";
        public static string dbHistoryPath = @"C:\CloudMap\history.ini";
    }
}
