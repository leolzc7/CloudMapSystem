﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Data
{
    class RelationData:DataSet
    {
        public const string RELATION_TABLE = "relation";
        public const string SOURCENAME_FIELD = "sourceName";
        public const string TARGETNAME_FIELD = "targetName";
        public const string NAME_FIELD = "name";
        public const string BIDIRECTION_FIELD = "bidirection";
        public const string TYPE_FIELD = "type";
        public const string COMMENT_FIELD = "comment";

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

            this.Tables.Add(newTable);
        }
    }
}