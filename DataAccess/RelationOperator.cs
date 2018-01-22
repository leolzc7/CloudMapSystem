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
    //针对关系表的操作，包括根据不同的等级Load整个表，和对表的增、删、改，以及得到不同等级的模块对应的关系数组
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
        private static bool CheckDuplication(RelationData relation)
        {
            DataRow data = relation.Tables[RelationData.RELATION_TABLE].Rows[0];
            string nameSource = "'" + data[RelationData.SOURCENAME_FIELD ] + "'";
            string nameTarget = "'" + data[RelationData.TARGETNAME_FIELD] + "'";
            string cmdCheck = "SELECT count(*) FROM relation WHERE sourceName = " + nameSource + " and targetName =  " + nameTarget;
            SQLiteDataReader reader = ExecuteReaderSql(cmdCheck);
            while (reader.Read())
            {
                if (reader.GetInt32(0) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static RelationData GetRelationInfoForDiffModList(List<string> modulesName)
        {
            RelationData relationdata = LoadRelationInfo();
            RelationData relationFilter = new RelationData();
            if (modulesName.Count > 0)
            {
                DataRow[] rowT3 = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " in " + GetString(modulesName) + " and " + RelationData.TARGETNAME_FIELD + " in " + GetString(modulesName));
                foreach (DataRow row in rowT3)
                {
                    relationFilter.Tables[RelationData.RELATION_TABLE].Rows.Add(row.ItemArray);
                }
            }
            return relationFilter;
        }//不同的等级的关系虚拟表Dataset
        public static bool InsertRelationInfo(RelationData relation)
        {
            bool check = CheckDuplication(relation);
            if (!check)
                return false;
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
            bool check = CheckDuplication(relation);
            if (!check)
                return false;
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
        public static SQLiteDataReader ExecuteReaderSql(string sql)
        {
            SQLiteCommand cmdReader = new SQLiteCommand(sql, globalParameters.conn);
            SQLiteDataReader reader = cmdReader.ExecuteReader();
            return reader;
        }
        public static List<relation> GetRelationArray(int level) //得到不同等级的模块对应的关系数组
        {
            List<string> modulesName = ModulesOperator.ReadModulesForDiffLevel(level);
            RelationData relation = RelationOperator.GetRelationInfoForDiffModList(modulesName);//Type为3
            List<relation> relationArray = new List<relation>();
            for (int i = 0; i < relation.Tables[RelationData.RELATION_TABLE].Rows.Count; i++ )
            {
                relation relationOne = new relation();
                relationOne.sourceName = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.SOURCENAME_FIELD].ToString();
                relationOne.targetName = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.TARGETNAME_FIELD].ToString();
                relationOne.bidirection = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.BIDIRECTION_FIELD].ToString();
                relationOne.relationName = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.NAME_FIELD].ToString();
                relationArray.Add(relationOne);
            }
            return relationArray;
        }

        public struct relation
        {
            public string sourceName;
            public string targetName;
            public string bidirection;
            public string relationName;
        }
        private static string GetString(List<string> modulesName)
        {
            string nameString = @"(";
            if (modulesName.Count > 0)
            {
                nameString = nameString + "'" + modulesName[0] + "'";
            }
            else
            {
                return null;
            }
            for (int i = 1; i < modulesName.Count; i++)
            {
                nameString = nameString + ",'" + modulesName[i] + "'";
            }
            nameString = nameString + ")";
            return nameString;
        }//将模块的名字列表变成一个字符串
        private static string GetString(List<string> modulesL1, List<string> modulesL2)
        {
            string nameString = @"(";
            if (modulesL1.Count + modulesL2.Count > 0)
            {
                if (modulesL1.Count == 0)
                {
                    return GetString(modulesL2);
                }
                else
                {
                    nameString = nameString + "'" + modulesL1[0] + "'";
                }
            }
            else
            {
                return null;
            }
            for (int i = 1; i < modulesL1.Count; i++)
            {
                nameString = nameString + ",'" + modulesL1[i] + "'";
            }
            for (int i = 0; i < modulesL2.Count; i++)
            {
                nameString = nameString + ",'" + modulesL2[i] + "'";
            }
            nameString = nameString + ")";
            return nameString;
        }
    }
}

