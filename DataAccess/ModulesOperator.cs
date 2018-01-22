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
        public static List<string> ReadModulesForDiffType(string type)
        {
            List<string> modules = new List<string>();
            string sql = "select name,type from modules";
            SQLiteDataReader reader = ExecuteReaderSql(sql);
            while (reader.Read())
            {
                if (reader.GetString(1) == type)
                {
                    modules.Add(reader.GetString(0));
                }
            }
            return modules;
        }
        public static List<ModulesList> CountModuleType(string type)
        {
            List<string> modulesName = ReadModulesForDiffType(type);
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
        public static List<string> ReadModulesForDiffLevel(int level)
        {
            List<string> modules = new List<string>();
            string sql = "select name,level from modules";
            SQLiteDataReader reader = ExecuteReaderSql(sql);
            while (reader.Read())
            {
                if (reader.GetInt32(1) <= level)
                {
                    modules.Add(reader.GetString(0));
                }
            }
            return modules;
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
        public struct ModulesList
        {
            public string name;
            public int count;
        }
    }
}
