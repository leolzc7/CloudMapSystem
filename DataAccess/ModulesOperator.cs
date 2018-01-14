using System.Data.SQLite;
using System.Data;
using Data;

namespace DataAccess
{

    class ModulesOperator
    {
        static SQLiteDataAdapter command;

        public static ModuleData LoadModulesInfo()
        {
            ModuleData data = new ModuleData();
            string sql0 = "select * from modules";
            command = new SQLiteDataAdapter(sql0, globalParameters.conn);
            command.Fill(data);
            return data;
        }

        public bool InsertModulesInfo(ModuleData module)
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

        private string GetInsertCommand(ModuleData module)
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
        public bool UpdateModulesInfo(ModuleData module, string selectModule)
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
        private string GetUpdateCommand(ModuleData module, string selectModule)
        {
            DataRow data = module.Tables[ModuleData.MODULES_TABLE].Rows[0];
            string cmdUpdate;
            string change = @"name = '" + data[ModuleData.NAME_FIELD] + "'," + "type = '" + data[ModuleData.TYPE_FIELD] + "'," + "level = " + data[ModuleData.LEVEL_FIELD] + "," + "comment = '" + data[ModuleData.COMMENT_FIELD] + "'";
            string condition = @"name = '" + selectModule + "'";
            cmdUpdate = "UPDATE modules SET " + change + " WHERE " + condition;
            return cmdUpdate;
        }
        public bool DeleteModulesInfo(string selectModule)
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

    }
}
