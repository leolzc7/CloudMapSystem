using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Data
{
    class ModuleData:DataSet
    {
        public const string MODULES_TABLE = "modules";
        public const string NAME_FIELD = "name";
        public const string TYPE_FIELD = "type";
        public const string LEVEL_FIELD = "level";
        public const string COMMENT_FIELD = "comment";

        public ModuleData()
        {
            BulidModulesData();
        }

        private void BulidModulesData()
        {
            DataTable newTable = new DataTable(MODULES_TABLE);
            DataColumnCollection columns = newTable.Columns;
            columns.Add(NAME_FIELD, typeof(string));
            columns.Add(TYPE_FIELD, typeof(string));
            columns.Add(LEVEL_FIELD, typeof(string));
            columns.Add(COMMENT_FIELD, typeof(string));

            this.Tables.Add(newTable);
        }
    }
}
