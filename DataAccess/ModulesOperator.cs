using System.Data.SQLite;
using System.Data;
using System;
using Data;
using System.Collections.Generic;

namespace DataAccess
{
    //针对模块表的操作，包括Load进整个表，和对表的增、删、改，以及读取出模块和不同等级的模块对应的关系数量
    public class ModulesOperator
    {
        //static SQLiteDataAdapter command;
        public static ModuleData LoadModulesInfo()
        {
            ModuleData data = new ModuleData();
            string sql0 = "select * from modules";
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteDataAdapter command = new SQLiteDataAdapter(sql0, conn))
                {
                    command.Fill(data.Tables[ModuleData.MODULES_TABLE]);
                    conn.Close();
                    return data;
                }
            } 
        }
        public static ModuleData LoadModulesInfoForSecondDb()
        {
            ModuleData data = new ModuleData();
            string sql1 = "select * from secondDb.modules";
            string sql00 = "ATTACH DATABASE '" + globalParameters.secondDbPath + "' as 'secondDb'";
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql00, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteDataAdapter command = new SQLiteDataAdapter(sql1, conn))
                {
                    command.Fill(data.Tables[ModuleData.MODULES_TABLE]);
                }
                conn.Close();
            }
            return data;
        }
        private static bool CheckDuplication(ModuleData module)
        {
            DataRow data = module.Tables[ModuleData.MODULES_TABLE].Rows[0];
            string name = "'" + data[ModuleData.NAME_FIELD] + "'";
            string cmdCheck = "SELECT count(*) FROM modules WHERE name = " + name;
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmdReader = new SQLiteCommand(cmdCheck, conn))
                {
                    using (SQLiteDataReader reader = cmdReader.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 0)
                            {
                                conn.Close();
                                return true;
                            }
                        }
                        conn.Close();
                        return false;
                    }
                }
            }
        }
        private static bool CheckAllModule(List<string> modulesName)
        {
            string cmdCheck = "SELECT count(*) FROM modules WHERE name in " + RelationOperator.GetString(modulesName);
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmdReader = new SQLiteCommand(cmdCheck, conn))
                {
                    using (SQLiteDataReader reader = cmdReader.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 0)
                            {
                                conn.Close();
                                return true;
                            }
                        }
                        conn.Close();
                        return false;
                    }
                }
            }
        }
        public static bool importModules(List<string> modulesName)
        {
            if (modulesName.Count == 0)
                return false;
            bool check = CheckAllModule(modulesName);
            if (!check)
                return false;
            string sql1 = "insert into modules select * from secondDb.modules where name in " + RelationOperator.GetString(modulesName);
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
            return true;
        }

        public static bool InsertModulesInfo(ModuleData module)
        {
            bool check = CheckDuplication(module);
            if (!check)
                return false;
            string insertCommand = GetInsertCommand(module);
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(insertCommand, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            if (module.HasErrors)
            {
                module.Tables[ModuleData.MODULES_TABLE].GetErrors()[0].ClearErrors();
                return false;
            }
            else
            {
                module.AcceptChanges();
                return true;
            }
        }
        private static string GetInsertCommand(ModuleData module)
        {
            DataRow data = module.Tables[ModuleData.MODULES_TABLE].Rows[0];
            string cmdInsert;
            string name = "'" + data[ModuleData.NAME_FIELD] + "',";
            string type = "'" + data[ModuleData.TYPE_FIELD] + "',";
            string level = data[ModuleData.LEVEL_FIELD] + ",";
            string comment = "'" + data[ModuleData.COMMENT_FIELD] + "'";
            cmdInsert = "INSERT INTO modules VALUES(" + name + type + level + comment + ")";
            return cmdInsert;
        }
        public static bool UpdateModulesInfo(ModuleData module, string selectModule)
        {
            string updateCommand = GetUpdateCommand(module, selectModule);
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
            if (module.HasErrors)
            {
                module.Tables[ModuleData.MODULES_TABLE].GetErrors()[0].ClearErrors();
                return false;
            }
            else
            {
                module.AcceptChanges();
                return true;
            }
        }
        private static string GetUpdateCommand(ModuleData module, string selectModule)
        {
            DataRow data = module.Tables[ModuleData.MODULES_TABLE].Rows[0];
            string cmdUpdate;
            string change = @"name = '" + data[ModuleData.NAME_FIELD] + "'," + "type = '" + data[ModuleData.TYPE_FIELD] + "'," + "level = " + data[ModuleData.LEVEL_FIELD] + "," + "comment = '" + data[ModuleData.COMMENT_FIELD] + "'";
            string condition = @"name = '" + selectModule + "'";
            cmdUpdate = "UPDATE modules SET " + change + " WHERE " + condition;
            return cmdUpdate;
        }
        public static bool DeleteModulesInfo(string selectModule)
        {
            string cmdDelete;
            string condition = @"name = '" + selectModule + "'";
            cmdDelete = "DELETE FROM modules WHERE " + condition;
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
        public static List<string> ReadModulesForDiffLevel(int level)
        {
            List<string> modules = new List<string>();
            string sql = "select name,level from modules";
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmdReader = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = cmdReader.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(1) <= level)
                            {
                                modules.Add(reader.GetString(0));
                            }
                        }
                    }
                    conn.Close();
                    return modules;
                }
            }
        }
        public static List<ModulesList> CountModuleLevel(int level)
        {
            List<string> modulesName = ReadModulesForDiffLevel(level);
            RelationData relation = RelationOperator.GetRelationInfoForDiffModList(modulesName);//Type为3
            List<ModulesList> modules = new List<ModulesList>();
            for (int i = 0; i < modulesName.Count; i++)
            {
                ModulesList module = new ModulesList();
                DataRow[] rl = relation.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " = '" + modulesName[i] + "' or " + RelationData.TARGETNAME_FIELD + "='" + modulesName[i] + "'");
                module.name = modulesName[i];
                module.count = rl.Length;
                modules.Add(module);
            }
            return modules;
        }
         public static List<string> ReadModulesForDiffLevelAndType(int level,string type)
        {
            List<string> modules = new List<string>();
            string sql = "select name,level from modules where type ='"+type+"'";
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmdReader = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = cmdReader.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(1)<=level)
                            modules.Add(reader.GetString(0));
                        }
                    }
                    conn.Close();
                    return modules;
                }
            }
        }
         public static List<ModulesList> CountModuleLevelAndType(int level,string type)
         {
             List<string> modulesName = ReadModulesForDiffLevelAndType(level,type);
             RelationData relation = RelationOperator.GetRelationInfoForDiffModList(modulesName);//Type为3
             List<ModulesList> modules = new List<ModulesList>();
             for (int i = 0; i < modulesName.Count; i++)
             {
                 ModulesList module = new ModulesList();
                 DataRow[] rl = relation.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " = '" + modulesName[i] + "' or " + RelationData.TARGETNAME_FIELD + "='" + modulesName[i] + "'");
                 module.name = modulesName[i];
                 module.count = rl.Length;
                 modules.Add(module);
             }
             return modules;
         }
        public struct ModulesList
        {
            public string name;
            public int count;
        }
    }
}
