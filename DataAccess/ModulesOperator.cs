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
        public static string[] read_modules()
        {
            int num = 0;
            string sql0 = "select count(*) from modules";
            SQLiteDataReader reader0 = ExecuteReaderSql(sql0);
            while (reader0.Read())
            {
                num = reader0.GetInt32(0);
            }
            string[] modulesList = new string[num];
            int i = 0;
            string sql = "select name from modules";

            SQLiteDataReader reader = ExecuteReaderSql(sql);
            while (reader.Read())
            {
                modulesList[i] = reader.GetString(0);
                i = i + 1;
            }
            return modulesList;
        }


        public static List<ModulesList> GetModuleCount()
        {

            string[] modulesList = read_modules();
            int num = modulesList.Length;
            ModulesList[] mod = new ModulesList[num];
            List<ModulesList> modules = new List<ModulesList>();
            int i = 0;
            foreach (string moduleName in modulesList)
            {
                string sql = "select count(*) from relation where sourceName = '" + moduleName + "' or targetName = '" + moduleName + "'"; ;
                SQLiteDataReader reader = ExecuteReaderSql(sql);
                while (reader.Read())
                {
                    mod[i].name = moduleName;
                    mod[i].count = reader.GetInt16(0);
                    modules.Add(mod[i]);
                    i++;
                }
            }
            return modules;
        }

        public struct ModulesList
        {
            public string name;
            public int count;
        };  

    }
}
