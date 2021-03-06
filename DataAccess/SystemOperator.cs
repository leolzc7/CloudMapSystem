﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Data;
using System.IO;
using System.Data;
using System.Xml;

namespace DataAccess
{
    //针对整个数据库的操作，例如链接并打开数据库、新建数据库、保存和读取历史记录等
    public class SystemOperator
    {
        public static void NewProjectConnectDb(string dbSelfName0, string dbSelfPath)
        {
            globalParameters.dbName = dbSelfName0 + ".db";
            globalParameters.dbPath = "Data Source = " + dbSelfPath +"\\"+ globalParameters.dbName;
            globalParameters.xmlFilePath = dbSelfPath + "\\" + dbSelfName0 + ".xml";
            WriteTypeXML(globalParameters.TypeList);
        }
        public static void NewProject(string dbSelfName, string dbSelfPath)
        {
            //新建项目时调用，建立数据库和数据表
            NewProjectConnectDb(dbSelfName, dbSelfPath);
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();

                string sql = "CREATE TABLE IF NOT EXISTS modules(name varchar(50) PRIMARY KEY, " +
                "type varchar(50), level INTEGER, comment varchar(100) ); ";//建表语句
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string sq2 = "CREATE TABLE IF NOT EXISTS relation(sourceName STRING NOT NULL, " +
                    "targetName STRING NOT NULL, rname varchar(50), bidirection varchar(20), type varchar(50), " +
                    "comment varchar(100), show varchar(20), sizeX INTERGER  default(0), sizeY INTERGER default(0), locX INTERGER default(0), locY INTERGER default(0), locDeltaX INTERGER default(0), locDeltaY INTERGER default(0)," +
                    "PRIMARY KEY(sourceName, targetName),FOREIGN KEY (sourceName) " +
                    "REFERENCES modules(name) on delete cascade on update cascade, " +
                    "FOREIGN KEY(targetName) REFERENCES modules(name) on delete cascade on update cascade); ";//建表语句
                using (SQLiteCommand cmd = new SQLiteCommand(sq2, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string sq3 = "PRAGMA foreign_keys = 'on';";
                using (SQLiteCommand cmd = new SQLiteCommand(sq3, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                string sq4 = "CREATE TABLE IF NOT EXISTS stream(sname varchar(50), num INTERGER," +
                    "modulesName STRING NOT NULL,PRIMARY KEY(sname,num),FOREIGN KEY (modulesName) " +
                    "REFERENCES modules(name) on delete cascade on update cascade)";//建表语句
                using (SQLiteCommand cmd = new SQLiteCommand(sq4, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }//创建数据库实例，指定文件位置
            string filePath = dbSelfPath + dbSelfName + ".db";
            EnqueueChecked(filePath);
            globalParameters.backupDbPath = @"Data Source =" + @"C:\CloudMap\tempppppppppppppp.db";
            CreateBackUpDb();
        }
        public static bool OpenProject(string filePath,bool first) 
        {
            //first表示是否是正常打开数据库，对于导入数据库时，相当于打开第二个数据库时，first为false
            if (!File.Exists(filePath))
            {
                globalParameters.dbHistory.Remove(filePath);
                return false;
            }
            else
            {
                if (first)
                {
                    globalParameters.backupDbPath = @"Data Source =" + @"C:\CloudMap\tempppppppppppppp.db";
                    string[] text = filePath.Split('\\');
                    globalParameters.dbName = text[text.Length - 1];
                    globalParameters.dbPath = "Data Source = " + filePath;
                    EnqueueChecked(filePath);
                    CreateBackUpDb();
                    string[] text2 = filePath.Split('.');
                    globalParameters.xmlFilePath = text2[0] + ".xml";
                    return true;
                }
                else
                {
                    string[] text = filePath.Split('\\');
                    globalParameters.secondDbName = text[text.Length - 1];
                    globalParameters.secondDbPath = filePath;
                    return true;
                }   
            }       
        }
        public static void CreateBackUpDb()
        {
            if (!Directory.Exists(globalParameters.dbDirPath))
            {
                Directory.CreateDirectory(globalParameters.dbDirPath);
                // File.SetAttributes(dirPath, FileAttributes.Hidden);
            }
            if (File.Exists(globalParameters.tempDb))
            {
                File.Delete(globalParameters.tempDb);
            }
            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath))
            {
                conn.Open();
                using (SQLiteConnection backupConn = new SQLiteConnection(globalParameters.backupDbPath))
                {
                    backupConn.Open();
                    conn.BackupDatabase(backupConn, "main", "main", -1, null, 0);
                    conn.Close();
                    backupConn.Close();
                }
            }
            string change = globalParameters.dbPath;
            globalParameters.dbPath = globalParameters.backupDbPath;
            globalParameters.backupDbPath = change;
        }
        public static void SaveBackupDb()
        {
            //File.Delete(globalParameters.tempDb);

            using (SQLiteConnection conn = new SQLiteConnection(globalParameters.dbPath)) //对缓存数据库建立链接
            {
                conn.Open();
                using (SQLiteConnection backupConn = new SQLiteConnection(globalParameters.backupDbPath))//对源数据库建立链接
                {
                    backupConn.Open();
                    conn.BackupDatabase(backupConn, "main", "main", -1, null, 0);//把缓存数据库的内容拷贝到源数据库
                    conn.Close();
                    backupConn.Close();
                }
            }
        }
        public static void ReadHistory()
        {
            string path = globalParameters.dbHistoryPath;
            if (File.Exists(path))
            {
                string[] strs1 = File.ReadAllLines(path);
                int length = strs1.Length;
                for (int i = 0; i < length; i++)
                {
                    globalParameters.dbHistory.Add(strs1[i]);
                    //Console.WriteLine(strs1[i]);
                }
            }
        }
        public static void EnqueueChecked(string filePath)
        {
            if (globalParameters.dbHistory.Count == 0)
            {
                globalParameters.dbHistory.Add(filePath);
            }
            else
            {
                bool have = false;
                foreach (string path in globalParameters.dbHistory)
                {
                    have = (path == filePath) || have;
                }
                if (have == false)
                    globalParameters.dbHistory.Add(filePath);
            }
        }
        public static void WriteHistory()
        {
            string dirPath = globalParameters.dbDirPath;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                // File.SetAttributes(dirPath, FileAttributes.Hidden);
            }
            string filePath = globalParameters.dbHistoryPath;
            if (!File.Exists(filePath))
            {
                FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs1);
                foreach (string oneHistory in globalParameters.dbHistory)
                {
                    sw.WriteLine(oneHistory);
                }
                sw.Flush();
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs2 = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                StreamWriter sw2 = new StreamWriter(fs2);

                foreach (string oneHistory in globalParameters.dbHistory)
                {
                    sw2.WriteLine(oneHistory);
                }
                //for (int i = globalParameters.dbHistory.Count ; i < 10; i++)
                //{
                //    sw2.WriteLine("");
                //}
                sw2.Flush();
                sw2.Close();
                fs2.Close();
            }
        }
        private static void CreateXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("Config");
            xmlDoc.AppendChild(root);

            XmlNode fontNode = xmlDoc.CreateNode(XmlNodeType.Element, "Font", null);
            CreateNode(xmlDoc, fontNode, fontList[0], "");
            CreateNode(xmlDoc, fontNode, fontList[1], "");
            CreateNode(xmlDoc, fontNode, fontList[2], "");
            CreateNode(xmlDoc, fontNode, fontList[3], "");
            CreateNode(xmlDoc, fontNode, fontList[4], "");
            CreateNode(xmlDoc, fontNode, fontList[5], "");
            CreateNode(xmlDoc, fontNode, fontList[6], "");
            root.AppendChild(fontNode);

            try
            {
                xmlDoc.Save(globalParameters.xmlFilePath);
            }
            catch (Exception e)
            {
                //显示错误信息  
                Console.WriteLine(e.Message);
            }  
        }
        public static void WriteTypeXML(List<string> typeList)
        {
            CreateXML();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(globalParameters.xmlFilePath);    //加载Xml文件  
            XmlElement rootElem = xmlDoc.DocumentElement;   //获取根节点  
            XmlNode typeNode = xmlDoc.CreateNode(XmlNodeType.Element, "TypeList", null);
            for (int i = 0; i < typeList.Count; i++ )
            {
                CreateNode(xmlDoc, typeNode, "type", typeList[i]);
            }
            rootElem.AppendChild(typeNode);
            try
            {
                xmlDoc.Save(globalParameters.xmlFilePath);
            }
            catch (Exception e)
            {
                //显示错误信息  
                Console.WriteLine(e.Message);
            }  
        }
        public static void ChangeFondConfig(string nodeName, string nodeValue)
        {
            if (!File.Exists(globalParameters.xmlFilePath))
            {
                CreateXML();
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(globalParameters.xmlFilePath);    //加载Xml文件  
            XmlElement rootElem = doc.DocumentElement;   //获取根节点  
            XmlNodeList fontNodeList = rootElem.GetElementsByTagName("Font"); //获取fond子节点集合 ,默认情况下应该只有一个节点 
            XmlNode fontNode = fontNodeList[0];
            XmlNodeList fontName = ((XmlElement)fontNode).GetElementsByTagName(nodeName);   //获取name属性值  
            fontName[0].InnerText = nodeValue;
            try
            {
                doc.Save(globalParameters.xmlFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }  
        }
        private static void CreateNode(XmlDocument xmlDoc,XmlNode parentNode,string name,string value)  
        {  
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);  
            node.InnerText = value;  
            parentNode.AppendChild(node);  
        }
        public static void getXmlValue()
        {
            globalParameters.TypeList = new List<string>();
            if (!File.Exists(globalParameters.xmlFilePath))
            {
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(globalParameters.xmlFilePath);    //加载Xml文件  
            XmlElement rootElem = doc.DocumentElement;   //获取根节点  
            XmlNodeList userNodes = rootElem.GetElementsByTagName("TypeList"); //获取person子节点集合  
            foreach (XmlNode node in userNodes)
            {
                XmlNodeList strType = ((XmlElement)node).GetElementsByTagName("type");   //获取name属性值  
                for (int i = 0; i < strType.Count; i++ )
                {
                    string str = strType[i].InnerText;
                    globalParameters.TypeList.Add(str);
                }
            }
        }
        public static List<string> fontList = new List<string>(){
            "moduleFontColor", "moduleColor","borderColor","lineColor","lineWidth","fontName","fontSize" };
        public static List<string> GetColor()
        {
            List<string> colorList = new List<string>();
            if (!File.Exists(globalParameters.xmlFilePath))
            {
                return colorList;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(globalParameters.xmlFilePath);    //加载Xml文件  
            XmlElement rootElem = doc.DocumentElement;   //获取根节点  
            XmlNodeList fontNodes = rootElem.GetElementsByTagName("Font"); //获取person子节点集合  
            XmlNode node = fontNodes[0];
            foreach (string font in fontList)
            {
                XmlNodeList strType = ((XmlElement)node).GetElementsByTagName(font);   //获取name属性值  
                if(strType.Count == 1)
                    colorList.Add(strType[0].InnerText);
                
            }
            return colorList;
        }
    
    }   
}
