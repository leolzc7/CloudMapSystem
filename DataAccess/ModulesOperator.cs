using System.Data.SQLite;
using System.Data;
using System;
using Data;
using System.Collections.Generic;

namespace DataAccess
{

    public class ModulesOperator
    {
        static SQLiteDataAdapter command;
        public static ModuleData LoadModulesInfo()
        {
            ModuleData data = new ModuleData();
            string sql0 = "select * from modules";
            command = new SQLiteDataAdapter(sql0, globalParameters.conn);
            command.Fill(data.Tables[ModuleData.MODULES_TABLE]);
            return data;
        }

        public static bool InsertModulesInfo(ModuleData module)
        {
            string insertCommand = GetInsertCommand(module);
            SystemOperator.ExecuteSql(insertCommand);
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
            SystemOperator.ExecuteSql(updateCommand);
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
        private static SQLiteDataReader ExecuteReaderSql(string sql)
        {
            SQLiteCommand cmdReader = new SQLiteCommand(sql, globalParameters.conn);
            SQLiteDataReader reader = cmdReader.ExecuteReader();
            return reader;
        }
        public static modulesName read_modules()
        {
            modulesName modulesList = new modulesName();
            modulesList.modulesL1 = new List<string>();
            modulesList.modulesL2 = new List<string>();
            modulesList.modulesL3 = new List<string>();
            string sql = "select level,name from modules";
            SQLiteDataReader reader = ExecuteReaderSql(sql);
            while (reader.Read())
            {
                if (reader.GetInt32(0) == 1)
                {
                    modulesList.modulesL1.Add(reader.GetString(1));
                }
                else
                {
                    if (reader.GetInt32(0) == 2)
                    {
                        modulesList.modulesL2.Add(reader.GetString(1));
                    }
                    else
                    {
                        modulesList.modulesL3.Add(reader.GetString(1));
                    }
                }
            }
            return modulesList;
        }
        public static List<ModulesList> GetModuleCount()
        {
            modulesName modulesName = read_modules();         
            List<ModulesList> modules = new List<ModulesList>();
            RelationData relationdata = RelationOperator.LoadRelationInfo();
            RelationData relaitonL1 = RelationOperator.GetRelationInfoForDiffLevel(modulesName, 1);
            RelationData relaitonL2 = RelationOperator.GetRelationInfoForDiffLevel(modulesName, 2);
            //if (GetString(modulesName.modulesL1) != null)
            //{
            //    DataRow[] rowL1 = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " in " + GetString(modulesName.modulesL1) + " and " + RelationData.TARGETNAME_FIELD + " in " + GetString(modulesName.modulesL1));
            //    foreach (DataRow row in rowL1)
            //    {
            //        relaitonL1.Tables[RelationData.RELATION_TABLE].Rows.Add(row.ItemArray);
            //    }
            //}
            //if (GetString(modulesName.modulesL2) != null)
            //{
            //    string con = RelationData.SOURCENAME_FIELD + " in " + GetString(modulesName.modulesL1, modulesName.modulesL2) + " or " + RelationData.TARGETNAME_FIELD + " in " + GetString(modulesName.modulesL1,modulesName.modulesL2);
            //    DataRow[] rowL2 = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " in " + GetString(modulesName.modulesL2) + " and " + RelationData.TARGETNAME_FIELD + " in " + GetString(modulesName.modulesL2));
            //    foreach (DataRow row in rowL2)
            //    {
            //        relaitonL2.Tables[RelationData.RELATION_TABLE].Rows.Add(row.ItemArray);
            //    }
            //}

            foreach (string mod in modulesName.modulesL1)
            {
                ModulesList module = new ModulesList();
                DataRow[] rl1 = relaitonL1.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " = '"+mod+"' or " + RelationData.TARGETNAME_FIELD +"='"+mod+"'");
                module.name = mod;
                module.level = 1;
                module.count = rl1.Length;
                modules.Add(module);
            }
            foreach (string mod in modulesName.modulesL2)
            {
                ModulesList module = new ModulesList();
                DataRow[] rl2 = relaitonL2.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " = '" + mod + "' or " + RelationData.TARGETNAME_FIELD + "='" + mod + "'");
                module.name = mod;
                module.level = 2;
                module.count = rl2.Length;
                modules.Add(module);
            } 
            foreach (string mod in modulesName.modulesL3)
            {
                ModulesList module = new ModulesList();
                DataRow[] rl3 = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " = '" + mod + "' or " + RelationData.TARGETNAME_FIELD + "='" + mod + "'");
                module.name = mod;
                module.level = 3;
                module.count = rl3.Length;
                modules.Add(module);
            }
            return modules;
        }
        public struct modulesName
        {
            public List<string> modulesL1;
            public List<string> modulesL2;
            public List<string> modulesL3;
        }
        public struct ModulesList
        {
            public string name;
            public int level;
            public int count;
        }
        //private static string GetString(List<string> modulesName)
        //{
        //    string nameString = @"(";
        //    if (modulesName.Count > 0)
        //    {
        //        nameString = nameString + "'" + modulesName[0] + "'";
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //    for (int i = 1; i < modulesName.Count; i++)
        //    {
        //        nameString = nameString + ",'" + modulesName[i] + "'";
        //    }
        //    nameString = nameString + ")";
        //    return nameString;
        //}
        //private static string GetString(List<string> modulesL1, List<string> modulesL2)
        //{
        //    string nameString = @"(";
        //    if (modulesL1.Count + modulesL2.Count> 0)
        //    {
        //        if (modulesL1.Count == 0)
        //        {
        //            return GetString( modulesL2);
        //        }
        //        else
        //        {
        //            nameString = nameString + "'" + modulesL1[0] + "'";
        //        } 
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //    for (int i = 1; i < modulesL1.Count; i++)
        //    {
        //        nameString = nameString + ",'" + modulesL1[i] + "'";
        //    }
        //    for (int i = 0; i < modulesL2.Count; i++)
        //    {
        //        nameString = nameString + ",'" + modulesL2[i] + "'";
        //    }
        //    nameString = nameString + ")";
        //    return nameString;
        //}

    }
}
