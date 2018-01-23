using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class StreamData:DataSet
    {
        public const string STREAM_TABLE = "modules";
        public const string NAME_FIELD = "name";
        public const string NUM_FIELD = "num";
        public const string MODULES_NAME_FIELD = "modulesName";

        public StreamData()
        {
            BulidStreamData();
        }

        private void BulidStreamData()
        {
            DataTable newTable = new DataTable(STREAM_TABLE);
            DataColumnCollection columns = newTable.Columns;
            columns.Add(NAME_FIELD, typeof(string));
            columns.Add(NUM_FIELD, typeof(int));
            columns.Add(MODULES_NAME_FIELD, typeof(string));

            this.Tables.Add(newTable);
        }
    }
}
