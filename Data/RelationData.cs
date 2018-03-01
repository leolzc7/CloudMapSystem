using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Data
{
    //新建一个对应关系表的内存中的Dataset表
    public class RelationData:DataSet
    {
        public const string RELATION_TABLE = "relation";
        public const string SOURCENAME_FIELD = "sourceName";
        public const string TARGETNAME_FIELD = "targetName";
        public const string NAME_FIELD = "rname";
        public const string BIDIRECTION_FIELD = "bidirection";
        public const string TYPE_FIELD = "type";
        public const string COMMENT_FIELD = "comment";
        public const string SHOW_FIELD = "show";
        public const string SIZE_X_FIELD = "sizeX";
        public const string SIZE_Y_FIELD = "sizeY";
        public const string LOC_X_FIELD = "locX";
        public const string LOC_Y_FIELD = "locY";
        public const string LOC_DELTA_X_FIELD = "locDeltaX";
        public const string LOC_DELTA_Y_FIELD = "locDeltaY";

        public RelationData()
        {
            BulidRelationData();
        }

        private void BulidRelationData()
        {
            DataTable newTable = new DataTable(RELATION_TABLE);
            DataColumnCollection columns = newTable.Columns;
            columns.Add(SOURCENAME_FIELD, typeof(string));
            columns.Add(TARGETNAME_FIELD, typeof(string));
            columns.Add(NAME_FIELD, typeof(string));
            columns.Add(BIDIRECTION_FIELD, typeof(string));
            columns.Add(TYPE_FIELD, typeof(string));
            columns.Add(COMMENT_FIELD, typeof(string));
            columns.Add(SHOW_FIELD, typeof(int));

            columns.Add(SIZE_X_FIELD, typeof(int));
            columns.Add(SIZE_Y_FIELD, typeof(int));
            columns.Add(LOC_X_FIELD, typeof(int));
            columns.Add(LOC_Y_FIELD, typeof(int));
            columns.Add(LOC_DELTA_X_FIELD, typeof(int));
            columns.Add(LOC_DELTA_Y_FIELD, typeof(int));

            this.Tables.Add(newTable);
        }
    }
}
