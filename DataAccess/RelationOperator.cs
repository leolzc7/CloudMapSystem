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
        public static RelationData LoadRelationInfo()
        {
            RelationData data = new RelationData();
            string sql0 = "select * from relation";
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteDataAdapter command = new SQLiteDataAdapter(sql0, conn))
                {
                    command.Fill(data.Tables[RelationData.RELATION_TABLE]);
                    conn.Close();
                    return data;
                }
            } 
        }
        public static RelationData LoadRelationInfoForSecondDb()
        {
            RelationData data = new RelationData();
            string sql0 = "select * from secondDb.relation";
            string sql00 = "ATTACH DATABASE '" + globalParameters.secondDbPath + "' as 'secondDb'";
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql00, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteDataAdapter command = new SQLiteDataAdapter(sql0, conn))
                {
                    command.Fill(data.Tables[RelationData.RELATION_TABLE]);
                    conn.Close();
                    return data;
                }
            }
        }
        private static bool CheckDuplication(DataRow data)
        {
            //DataRow data = relation.Tables[RelationData.RELATION_TABLE].Rows[0];
            string nameSource = "'" + data[RelationData.SOURCENAME_FIELD ] + "'";
            string nameTarget = "'" + data[RelationData.TARGETNAME_FIELD] + "'";
            string nameRelation = "'" + data[RelationData.NAME_FIELD] + "'";
            string cmdCheck = "SELECT count(*) FROM relation WHERE sourceName = " + nameSource + " and targetName =  " + nameTarget;
            string cmdCheckR = "SELECT count(*) FROM relation WHERE rname = " + nameRelation;
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmdReader = new SQLiteCommand(cmdCheck, conn))
                {
                    using (SQLiteCommand cmdReader2 = new SQLiteCommand(cmdCheckR, conn))
                    {
                        using (SQLiteDataReader reader2 = cmdReader2.ExecuteReader())
                        {
                            using (SQLiteDataReader reader = cmdReader.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0)
                                    {
                                        conn.Close();
                                        return false;
                                    }
                                }
                                while (reader2.Read())
                                {
                                    if (reader2.GetInt32(0) != 0)
                                    {
                                        conn.Close();
                                        return false;
                                    }
                                }
                                conn.Close();
                                return true;
                            }
                        }
                    }
                }
            }
        }
        public static RelationData GetRelationInfoForImport(List<string> modulesName)
        {
            RelationData relationdata = LoadRelationInfoForSecondDb();
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
        }
        public static bool ImportRelation(RelationData relation) //检查每一行是否有重复然后插入
        {
            for (int i = 0; i < relation.Tables[RelationData.RELATION_TABLE].Rows.Count; i++)
            {
                DataRow data = relation.Tables[RelationData.RELATION_TABLE].Rows[i];
                bool check = CheckDuplication(data);
                if (!check)
                    return false;
                string sourceName = "'" + relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.SOURCENAME_FIELD].ToString() + "'";
                string targetName = "'" + relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.TARGETNAME_FIELD].ToString() + "'";
                string sql1 = "insert into relation select * from secondDb.relation where sourceName = " + sourceName + " and targetName = " + targetName;
                string sql00 = "ATTACH DATABASE '" + globalParameters.secondDbPath + "' as 'secondDb'";
                using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql00, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(sql1, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            return true;
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
            DataRow data = relation.Tables[RelationData.RELATION_TABLE].Rows[0];
            bool check = CheckDuplication(data);
            if (!check)
                return false;
            string insertCommand = GetInsertCommand(relation);
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(insertCommand, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
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
            string comment = "'" + data[RelationData.COMMENT_FIELD] + "',";
            string show = "'" + data[RelationData.SHOW_FIELD] + "'";
            cmdInsert = "INSERT INTO relation VALUES(" + source + target + name + bidirection + type + comment + show + ")";
            return cmdInsert;
        }
        public static bool UpdateRelationInfo(RelationData relation, string selectSource, string selectTarget)
        {
            //bool check = CheckDuplication(relation);
            //if (!check)
            //    return false;
            string updateCommand = GetUpdateCommand(relation, selectSource, selectTarget);
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                string sq0 = "PRAGMA foreign_keys = 'on';";
                using (SQLiteCommand cmd = new SQLiteCommand(sq0, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(updateCommand, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
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
            string change = @"sourceName = '" + data[RelationData.SOURCENAME_FIELD] + "'," + "targetName = '" + data[RelationData.TARGETNAME_FIELD] + "'," + "rname = '" + data[RelationData.NAME_FIELD] + "'," + "type = '" + data[RelationData.TYPE_FIELD] + "'," + "bidirection = '" + data[RelationData.BIDIRECTION_FIELD] + "'," + "comment = '" + data[RelationData.COMMENT_FIELD] + "'," + "show ='" + data[RelationData.SHOW_FIELD] + "'";
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
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                string sq0 = "PRAGMA foreign_keys = 'on';";
                using (SQLiteCommand cmd = new SQLiteCommand(sq0, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(cmdDelete, conn))
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        conn.Close();
                        return false;
                    }
                }
            }
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
                relationOne.comment = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.COMMENT_FIELD].ToString();
                if(relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.SHOW_FIELD].ToString() == "0"){
                    relationOne.show = 0;
                }
                else
                {
                    relationOne.show = 1;
                }
                relationArray.Add(relationOne);
            }
            return relationArray;
        }
        public static List<relation> GetRelationArray(string type) //得到不同type的模块对应的关系数组
        {
            List<string> modulesName = ModulesOperator.ReadModulesForDiffType(type);
            RelationData relation = RelationOperator.GetRelationInfoForDiffModList(modulesName);
            List<relation> relationArray = new List<relation>();
            for (int i = 0; i < relation.Tables[RelationData.RELATION_TABLE].Rows.Count; i++)
            {
                relation relationOne = new relation();
                relationOne.sourceName = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.SOURCENAME_FIELD].ToString();
                relationOne.targetName = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.TARGETNAME_FIELD].ToString();
                relationOne.bidirection = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.BIDIRECTION_FIELD].ToString();
                relationOne.relationName = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.NAME_FIELD].ToString();
                relationOne.comment = relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.COMMENT_FIELD].ToString();
                if (relation.Tables[RelationData.RELATION_TABLE].Rows[i][RelationData.SHOW_FIELD].ToString() == "0")
                {
                    relationOne.show = 0;
                }
                else
                {
                    relationOne.show = 1;
                } 
                relationArray.Add(relationOne);
            }
            return relationArray;
        }
        public static string GetRelationName(string sourceName, string targetName)
        {
            string relationName = null;
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                string sql = "SELECT rname FROM relation WHERE sourceName = '"+sourceName +"' and targetName = '"+targetName+"'";
                using (SQLiteCommand cmdReader = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = cmdReader.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           relationName = reader.GetString(0);
                        }
                        conn.Close();
                        return relationName;
                    }
                }
            }
        }
        public struct relation
        {
            public string sourceName;
            public string targetName;
            public string bidirection;
            public string relationName;
            public string comment;
            public int show;
        }
        public static string GetString(List<string> modulesName)
        {
            if (modulesName.Count == 0)
                return null;
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
        public static string GetString(List<string> modulesL1, List<string> modulesL2)
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

