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
        static SQLiteDataAdapter command;
        public static ModuleData LoadModulesInfo()
        {
            ModuleData data = new ModuleData();
            string sql0 = "select * from modules";
            command = new SQLiteDataAdapter(sql0, globalParameters.conn);
            command.Fill(data.Tables[ModuleData.MODULES_TABLE]);
            return data;
        }
        private static bool CheckDuplication(ModuleData module)
        {
            DataRow data = module.Tables[ModuleData.MODULES_TABLE].Rows[0];
            string name = "'" + data[ModuleData.NAME_FIELD] + "'";
            string cmdCheck = "SELECT count(*) FROM modules WHERE name = " + name;
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
        public static bool InsertModulesInfo(ModuleData module)
        {
            bool check = CheckDuplication(module);
            if (!check)
                return false;
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
            bool check = CheckDuplication(module);
            if (!check)
                return false;
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
        public static modulesName read_modules()//读取出不同等级的模块名，存到一个结构体中返回
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
        public static List<ModulesList> read_all_modules()//读取所有等级的模块名
        {
            List<ModulesList> modulesAll = new List<ModulesList>();
            string sql = "select name,level from modules";
            SQLiteDataReader reader = ExecuteReaderSql(sql);
            while (reader.Read())
            {
                ModulesList mm = new ModulesList();
                mm.name = reader.GetString(0);
                mm.level = reader.GetInt32(1);
                modulesAll.Add(mm);
            }
            return modulesAll;
        }
        public static List<ModulesList> GetModuleCount(int level)//根据不同等级的模块对应的关系表读取出每个模块的关系数目
        {
            modulesName modulesName = read_modules();
            List<ModulesList> modulesAll = read_all_modules();
            List<ModulesList> modules = new List<ModulesList>();
            RelationData relationdata = RelationOperator.LoadRelationInfo();
            RelationData relaitonL1 = RelationOperator.GetRelationInfoForDiffLevel(modulesName, 1); //得到等级1的模块对应的关系表
            RelationData relaitonL2 = RelationOperator.GetRelationInfoForDiffLevel(modulesName, 2);//得到等级1和2的模块对应的关系表
            switch (level)
            {
                case 1:
                    for (int i = 0; i < modulesAll.Count; i++)
                    {
                        DataRow[] rl1 = relaitonL1.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " = '" + modulesAll[i].name + "' or " + RelationData.TARGETNAME_FIELD + "='" + modulesAll[i].name + "'");
                        ModulesList module = new ModulesList();
                        module.name = modulesAll[i].name;
                        module.level = modulesAll[i].level;
                        module.count = rl1.Length;
                        modules.Add(module);
                    }
                    break;
                case 2:
                    for (int i = 0; i < modulesAll.Count; i++)
                    {
                        ModulesList module = new ModulesList();
                        DataRow[] rl2 = relaitonL2.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " = '" + modulesAll[i].name + "' or " + RelationData.TARGETNAME_FIELD + "='" + modulesAll[i].name + "'");
                        module.name = modulesAll[i].name;
                        module.level = modulesAll[i].level;
                        module.count = rl2.Length;
                        modules.Add(module);
                    }
                    break;
                case 3:
                    for (int i = 0; i < modulesAll.Count; i++)
                    {
                        ModulesList module = new ModulesList();
                        DataRow[] rl3 = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + " = '" + modulesAll[i].name + "' or " + RelationData.TARGETNAME_FIELD + "='" + modulesAll[i].name + "'");
                        module.name = modulesAll[i].name;
                        module.level = modulesAll[i].level;
                        module.count = rl3.Length;
                        modules.Add(module);
                    }
                    break;
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
    }
}
