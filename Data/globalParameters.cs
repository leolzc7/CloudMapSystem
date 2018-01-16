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
        public static string dbName;
        public static Queue<string> dbHistory = new Queue<string> ();
        public static SQLiteConnection conn = null;
        public static string dbHistoryPath = @"C:\CloudMap\history.ini";
    }
}
