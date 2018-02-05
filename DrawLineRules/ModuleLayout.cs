using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DrawLineRules
{
    public struct Module
    {
        public string moduleName;
        public int x;
        public int y;
    }
    public struct MapLocation
    {
        public int id;
        public int positionX;
        public int positionY;
    }
    public class ModuleLayout
    {
        //螺旋排列相对坐标点与其上值的对应（1开始），从坐标到值
        //逆时针旋转
        //public static int HelixNumber(int x, int y)
        //{
        //    int layer = Math.Max(Math.Abs(x), Math.Abs(y));
        //    int maxNumber = (2 * layer + 1) * (2 * layer + 1);
        //    int key;
        //    if (y == layer)
        //        key = maxNumber - y + x;
        //    else if (x == -layer)
        //        key = maxNumber + (3 * x + y);
        //    else if (y == -layer)
        //        key = maxNumber + (-x + 5 * y);
        //    else
        //        key = maxNumber + (-7 * x - y);
        //    return key;
        //}
        //顺时针旋转，先左后上
        public static int HelixNumber(int x, int y)
        {
            int layer = Math.Max(Math.Abs(x), Math.Abs(y));
            int maxNumber = (2 * layer + 1) * (2 * layer + 1);
            int key;
            if (y == layer)
                key = maxNumber - y - x;
            else if (x == layer)
                key = maxNumber + (-3 * x + y);
            else if (y == -layer)
                key = maxNumber + (x + 5 * y);
            else
                key = maxNumber + (7 * x - y);
            return key;
        }
        //给定层数，确定该层内所有值的对应，layer=1，有9个map；layer=2，有25个map，
        public static MapLocation[] GetMap(int layer)
        {
            MapLocation[] map = new MapLocation[(2 * layer + 1) * (2 * layer + 1)];
            for (int y = -layer; y <= layer; y++)
            {
                for (int x = -layer; x <= layer; x++)
                {
                    int index = HelixNumber(x, y) - 1;
                    map[index].id = index - 1;
                    map[index].positionX = x;
                    map[index].positionY = y;
                }
            }
            return map;
        }
        //重载sortmodulebycount方法，一种level，一种type
        public static List<ModulesOperator.ModulesList> SortModuleByCount(int level)
        {
            List<ModulesOperator.ModulesList> modules = ModulesOperator.CountModuleLevel(level);
            modules = modules.OrderByDescending(module => module.count).ToList();
            return modules;
        }
        public static List<ModulesOperator.ModulesList> SortModuleByCount(int level,  String type)
        {
            List<ModulesOperator.ModulesList> modules = ModulesOperator.CountModuleLevelAndType(level, type);
            modules = modules.OrderByDescending(module => module.count).ToList();
            return modules;
        }
        public static List<Module> ModulePosition(int panelWidth, int panelHeight, int level)
        {
            List<ModulesOperator.ModulesList> modules = SortModuleByCount(level);
      
            List<Module> modPosition = new List<Module>();
   
            int CountNum = modules.Count;
            int layer = (int)(Math.Ceiling(Math.Sqrt(CountNum)) / 2);
            int derta = 2 * layer + 1;
            int dertaX = panelWidth / derta;
            int dertaY = panelHeight / derta;
            MapLocation[] map = GetMap(layer);
            Module[] mods = new Module[CountNum];
            for (int i = 0; i < CountNum; i++)
            {
                mods[i].moduleName = modules[i].name;
                mods[i].x = (map[i].positionX + layer) * dertaX + (int)(0.3 * dertaX);
                mods[i].y = (map[i].positionY + layer) * dertaY + (int)(0.3 * dertaY);
            }
            Module[] moduleSize = new Module[2];//第一行模块大小，第二行模块间距
            moduleSize[0].moduleName = level.ToString();
            moduleSize[0].x = (int)(dertaX * 0.4);
            moduleSize[0].y = (int)(dertaY * 0.4);
            moduleSize[1].moduleName = " ";
            moduleSize[1].x = dertaX;
            moduleSize[1].y = dertaY;
            for (int i = 0; i < 2; i++)
            {
                modPosition.Add(moduleSize[i]);
            }
            for (int i = 0; i < CountNum; i++)
            {
                modPosition.Add(mods[i]);
            }
            return modPosition;
        }
        //重载方法moduleposition
        public static List<Module> ModulePosition(int panelWidth, int panelHeight, int level, string type)
        {
            List<ModulesOperator.ModulesList> modules = SortModuleByCount(level, type);
            
            List<Module> modPosition = new List<Module>();
            
            int CountNum = modules.Count;
            int layer = (int)(Math.Ceiling((Math.Sqrt(CountNum))) / 2);
            int derta = 2 * layer + 1;
            int dertaX = panelWidth / derta;
            int dertaY = panelHeight / derta;
            MapLocation[] map = GetMap(layer);
            Module[] mods = new Module[CountNum];
            for (int i = 0; i < CountNum; i++)
            {
                mods[i].moduleName = modules[i].name;
                mods[i].x = (map[i].positionX + layer) * dertaX + (int)(0.3 * dertaX);
                mods[i].y = (map[i].positionY + layer) * dertaY + (int)(0.3 * dertaY);
            }
            Module[] moduleSize = new Module[2];//第一行模块大小，第二行模块间距
            moduleSize[0].moduleName = " ";
            moduleSize[0].x = (int)(dertaX * 0.4);
            moduleSize[0].y = (int)(dertaY * 0.4);
            moduleSize[1].moduleName = " ";
            moduleSize[1].x = dertaX;
            moduleSize[1].y = dertaY;
            for (int i = 0; i < 2; i++)
            {
                modPosition.Add(moduleSize[i]);
            }
            for (int i = 0; i < CountNum; i++)
            {
                modPosition.Add(mods[i]);
            }
            return modPosition;
        }
    }
}
