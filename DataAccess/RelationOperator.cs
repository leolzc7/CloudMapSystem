using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using Data;

namespace DataAccess
{
    public class RelationOperator
    {
        static SQLiteDataAdapter command;
        public static RelationData LoadRelationInfo()
        {
            RelationData data = new RelationData();
            string sql0 = "select * from relation";
            command = new SQLiteDataAdapter(sql0, globalParameters.conn);
            command.Fill(data.Tables[RelationData.RELATION_TABLE]);
            return data;
        }
        public static bool InsertRelationInfo(RelationData relation)
        {
            string insertCommand = GetInsertCommand(relation);
            SystemOperator.ExecuteSql(insertCommand);
            if (relation.HasErrors)
            {
                relation.Tables[RelationData.RELATION_TABLE].GetErrors()[0].ClearErrors();
                return false;
            }
            else
            {
                relation.AcceptChanges();
                return true;
            }
        }
        private static string GetInsertCommand(RelationData relation)
        {
            DataRow data = relation.Tables[RelationData.RELATION_TABLE].Rows[0];
            string cmdInsert;
            string name = "'" + data[RelationData.NAME_FIELD] + "',";
            string source = "'" + data[RelationData.SOURCENAME_FIELD] + "',";
            string target = "'" + data[RelationData.TARGETNAME_FIELD] + "',";
            string bidirection = "'" + data[RelationData.BIDIRECTION_FIELD] + "',";
            string type = "'" + data[RelationData.TYPE_FIELD] + "',";
            string comment = "'" + data[RelationData.COMMENT_FIELD] + "'";
            cmdInsert = "INSERT INTO relation VALUES(" + source + target + name + bidirection + type + comment + ")";
            return cmdInsert;
        }
        public static bool UpdateRelationInfo(RelationData relation, string selectSource, string selectTarget)
        {
            string updateCommand = GetUpdateCommand(relation, selectSource, selectTarget);
            SystemOperator.ExecuteSql(updateCommand);
            if (relation.HasErrors)
            {
                relation.Tables[RelationData.RELATION_TABLE].GetErrors()[0].ClearErrors();
                return false;
            }
            else
            {
                relation.AcceptChanges();
                return true;
            }
        }
        private static string GetUpdateCommand(RelationData relation, string selectSource, string selectTarget)
        {
            DataRow data = relation.Tables[RelationData.RELATION_TABLE].Rows[0];
            string cmdUpdate;
            string change = @"sourceName = '" + data[RelationData.SOURCENAME_FIELD] + "'," + "targetName = '" + data[RelationData.TARGETNAME_FIELD] + "'," + "rname = '" + data[RelationData.NAME_FIELD] + "'," + "type = '" + data[RelationData.TYPE_FIELD] + "'," + "bidirection = '" + data[RelationData.BIDIRECTION_FIELD] + "'," + "comment = '" + data[RelationData.COMMENT_FIELD] + "'";
            string condition1 = @"sourceName = '" + selectSource + "'";
            string condition2 = @"targetName = '" + selectTarget + "'";
            cmdUpdate = "UPDATE relation SET " + change + " WHERE " + condition1 + " and " + condition2;
            return cmdUpdate;
        }
        public static bool DeleteRelationInfo(string selectSource, string selectTarget)
        {
            string cmdDelete;
            string condition1 = @"sourceName = '" + selectSource + "'";
            string condition2 = @"targetName = '" + selectTarget + "'";
            cmdDelete = "DELETE FROM relation WHERE " + condition1 + " and " + condition2;
            SQLiteCommand cmd = new SQLiteCommand(cmdDelete, globalParameters.conn);
            if (cmd.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}

