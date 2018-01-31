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
    public class StreamOperator
    {
        public static StreamData LoadStreamInfo()
        {
            StreamData data = new StreamData();
            string sql0 = "select * from stream";
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteDataAdapter command = new SQLiteDataAdapter(sql0, conn))
                {
                    command.Fill(data.Tables[StreamData.STREAM_TABLE]);
                    conn.Close();
                    return data;
                }
            }
        }
        public static StreamData GetStreamName()
        {
            StreamData data = new StreamData();
            string sql0 = "select DISTINCT sname from stream";
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteDataAdapter command = new SQLiteDataAdapter(sql0, conn))
                {
                    command.Fill(data.Tables[StreamData.STREAM_TABLE]);
                    conn.Close();
                    return data;
                }
            }
        }
        private static bool CheckDuplication(DataRow data)
        {
            string sname = "'" + data[StreamData.NAME_FIELD] + "'";
            string num = data[StreamData.NUM_FIELD].ToString();
            string cmdCheck = "SELECT count(*) FROM stream WHERE sname = " + sname + " and num =  " + num;
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
        private static bool CheckRelation(DataRow dataFirst, DataRow dataSecond)
        {
            string sourceName = "'" + dataFirst[StreamData.MODULES_NAME_FIELD].ToString() + "'";
            string targetName = "'" + dataSecond[StreamData.MODULES_NAME_FIELD].ToString() + "'";
            string cmdCheck1 = "SELECT count(*) FROM relation WHERE sourceName = " + sourceName + " and targetName =  " + targetName;
            string cmdCheck2 = "SELECT count(*) FROM relation WHERE sourceName = " + targetName + " and targetName =  " + sourceName + " and bidirection = '1'";
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteCommand cmdReader1 = new SQLiteCommand(cmdCheck1, conn))
                {
                    using (SQLiteDataReader reader1 = cmdReader1.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            if (reader1.GetInt32(0) != 0)
                            {
                                conn.Close();
                                return false;
                            }
                            else //针对源到目的没有关系，判断是否有目的到源的双向关系
                            {
                                using (SQLiteCommand cmdReader2 = new SQLiteCommand(cmdCheck2, conn))
                                {
                                    using (SQLiteDataReader reader2 = cmdReader2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            if (reader2.GetInt32(0) == 0)
                                            {
                                                conn.Close();
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        conn.Close();
                        return false;
                    }
                }
            }
        }
        public static bool InsertStreamInfo(StreamData stream) //检查每一行是否有重复然后插入
        {
            for (int i = 0; i < stream.Tables[StreamData.STREAM_TABLE].Rows.Count; i++)
            {
                DataRow data = stream.Tables[StreamData.STREAM_TABLE].Rows[i];
                data[StreamData.NUM_FIELD] = i + 1;
                bool check = CheckDuplication(data);
                if (!check)
                    return false;
                if (i < stream.Tables[StreamData.STREAM_TABLE].Rows.Count - 1) //判断相邻的模块是否有关系
                {
                    DataRow dataSecond = stream.Tables[StreamData.STREAM_TABLE].Rows[i + 1];
                    bool checkRelation = CheckRelation(data, dataSecond);
                    if (!checkRelation)
                        return false;
                }
                string insertCommand = GetInsertCommand(data);
                using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(insertCommand, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                if (stream.HasErrors)
                {
                    stream.Tables[StreamData.STREAM_TABLE].GetErrors()[0].ClearErrors();
                    return false;
                }
                else
                {
                    stream.AcceptChanges();
                    return true;
                }
            }
            return false;
        }
        private static string GetInsertCommand(DataRow stream)
        {
            DataRow data = stream;
            string cmdInsert;
            string sname = "'" + data[StreamData.NAME_FIELD] + "',";
            string num = data[StreamData.NUM_FIELD].ToString()+",";
            string modulesName = "'" + data[StreamData.MODULES_NAME_FIELD] + "'";
            cmdInsert = "INSERT INTO stream VALUES(" + sname + num + modulesName + ")";
            return cmdInsert;
        }
        public static bool UpdateStreamInfo(StreamData stream, string selectsname)
        {
            DeleteStreamInfo(selectsname);
            InsertStreamInfo(stream);
            if (stream.HasErrors)
            {
                stream.Tables[StreamData.STREAM_TABLE].GetErrors()[0].ClearErrors();
                return false;
            }
            else
            {
                stream.AcceptChanges();
                return true;
            }
        }
        public static bool DeleteStreamInfo(string selectsname) //根据指定的业务名，删除该业务相关记录
        {
            string cmdDelete = GetDeleteCommand(selectsname);
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
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
        private static string GetDeleteCommand(string selectsname)
        {
            string cmdDelete;
            string condition1 = @"sname = '" + selectsname + "'";
            cmdDelete = "DELETE FROM stream WHERE " + condition1;
            return cmdDelete;
        }
        public static List<streamList> GetStreamForModule(string moduleName) //给定模块名，返回模块名对应的所有业务流List
        {
            List<streamList> stream = new List<streamList>();
            StreamData streamData = LoadStreamInfo();
            DataRow[] streamNameList = streamData.Tables[StreamData.STREAM_TABLE].Select(StreamData.MODULES_NAME_FIELD + " = '" + moduleName+"'");
            foreach (DataRow row in streamNameList)
            {
                streamList oneStream = new streamList();
                oneStream.modulesList = new List<string>();
                oneStream.streamName = row[StreamData.NAME_FIELD].ToString();
                DataRow[] oneStreamTable = streamData.Tables[StreamData.STREAM_TABLE].Select(StreamData.NAME_FIELD + "= '" + oneStream.streamName+"'");
                foreach (DataRow streamRow in oneStreamTable)
                {
                    string mod = streamRow[StreamData.MODULES_NAME_FIELD].ToString();
                    oneStream.modulesList.Add(mod);
                }
                stream.Add(oneStream);
            }
            return stream;
        }
        public struct streamList
        {
            public string streamName;
            public List<string> modulesList;
        }
    }
}

