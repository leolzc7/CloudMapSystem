using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DrawLineRules
{
    public class ModuleOne
    {
        private string name;//每个模块的唯一索引值
        private int posx;
        private int posy;
        public static int modx; //两个模块X轴之间的距离,基于网格的
        public static int mody;
        public static int gapx;
        public static int gapy;
        private static int width; //定义模块的大小
        private static int height;
        private int num_north;
        private int num_south;
        private int num_east;
        private int num_west;
        public struct LineInfo
        {
            public int[] line;
            public string lineName;
        }
        //构造函数
        public ModuleOne()
        {

        }

        public ModuleOne(string n, int x, int y)
        {
            name = n;
            posx = x;
            posy = y;
        }

        public GridPoint northPin(Grid moduleGrid, int com)
        {
            if (com == 1 || com == 5)
            {
                moduleGrid.gridPointAt(posx + width - 15, posy).setRouted();
                return moduleGrid.gridPointAt(posx + width - 15, posy);
            }
            else
            {
                int x = 10;
                int step = width / (this.num_north + 1);
                for (int i = 0; i < this.num_north; i++)
                {
                    if (!moduleGrid.gridPointAt(posx + (i + 1) * step + x, posy).isRouted())
                    {
                        moduleGrid.gridPointAt(posx + (i + 1) * step + x, posy).setRouted();
                        return moduleGrid.gridPointAt(posx + (i + 1) * step + x, posy);
                    }
                }
            }
            return moduleGrid.gridPointAt(0, 0);
        }

        public GridPoint eastPin(Grid moduleGrid, int com)
        {
            if (com == 3 || com == 7)
            {
                moduleGrid.gridPointAt(posx + getWidth() - 1, posy + 25).setRouted();
                return moduleGrid.gridPointAt(posx + getWidth() - 1, posy + 25);
            }
            else
            {
                int step = height / (this.num_east + 1);
                for (int i = 0; i < this.num_east; i++)
                {
                    if (!moduleGrid.gridPointAt(posx + getWidth() - 1, posy + (i + 1) * step).isRouted())
                    {
                        moduleGrid.gridPointAt(posx + getWidth() - 1, posy + (i + 1) * step).setRouted();
                        return moduleGrid.gridPointAt(posx + getWidth() - 1, posy + (i + 1) * step);
                    }
                }
            }
            return moduleGrid.gridPointAt(0, 0);
        }

        public GridPoint southPin(Grid moduleGrid, int com)
        {
            if (com == 1 || com == 5)
            {
                moduleGrid.gridPointAt(posx + width - 15, posy + getHeight() - 1).setRouted();
                return moduleGrid.gridPointAt(posx + width - 15, posy + getHeight() - 1);
            }
            else
            {
                int x = 0;
                int step = width / (this.num_south + 1);
                for (int i = 0; i < this.num_south; i++)
                {
                    if (!moduleGrid.gridPointAt(posx + (i + 1) * step + x, posy + getHeight() - 1).isRouted())
                    {
                        moduleGrid.gridPointAt(posx + (i + 1) * step + x, posy + getHeight() - 1).setRouted();
                        return moduleGrid.gridPointAt(posx + (i + 1) * step + x, posy + getHeight() - 1);
                    }
                }
            }
            return moduleGrid.gridPointAt(0, 0);
        }

        public GridPoint westPin(Grid moduleGrid, int com)
        {
            if (com == 3 || com == 7)
            {
                moduleGrid.gridPointAt(posx, posy + 25).setRouted();
                return moduleGrid.gridPointAt(posx, posy + 25);
            }
            else
            {
                int step = height / (this.num_west + 1);
                for (int i = 0; i < this.num_west; i++)
                {
                    if (!moduleGrid.gridPointAt(posx, posy + (i + 1) * step).isRouted())
                    {
                        moduleGrid.gridPointAt(posx, posy + (i + 1) * step).setRouted();
                        return moduleGrid.gridPointAt(posx, posy + (i + 1) * step);
                    }
                }
            }
            return moduleGrid.gridPointAt(0, 0);
        }

        public int compareModuleAdd(ModuleOne gp)
        {
            if (posx == gp.getPosx())
            {
                if (posy > gp.getPosy())
                {
                    if (posy - gp.getPosy() > mody)
                        return 10;//up
                    else
                        return 1;
                }
                if (posy < gp.getPosy())
                {
                    if (gp.getPosy() - posy > mody)
                        return 14;
                    else
                        return 5;
                }
            }
            if (posx < gp.getPosx())
            {
                if (posy > gp.getPosy())
                {
                    if (posy - gp.getPosy() > mody)
                        return 11;
                    else
                        return 2;
                }
                if (posy == gp.getPosy())
                {
                    if (gp.getPosx() - posx > modx)
                        return 12;
                    else
                        return 3;
                }
                if (posy < gp.getPosy())
                {
                    if (gp.getPosy() - posy > mody)
                        return 13;
                    else
                        return 4;
                }
            }
            if (posx > gp.getPosx())
            {
                if (posy < gp.getPosy())
                {
                    if (gp.getPosy() - posy > mody)
                        return 15;
                    else
                        return 6;
                }
                if (posy == gp.getPosy())
                {
                    if (posx - gp.getPosx() > modx)
                        return 16;
                    else
                        return 7;
                }
                if (posy > gp.getPosy())
                {
                    if (posy - gp.getPosy() > mody)
                        return 9;
                    else
                        return 8;
                }
            }
            return 0;
        }

        public static void setWidth(int w) { width = w; }
        public static void setHeight(int h) { height = h; }

        public void addNumNorthOne()
        {
            this.num_north += 1;
        }
        public void addNumSouthOne()
        {
            this.num_south += 1;
        }
        public void addNumWestOne()
        {
            this.num_west += 1;
        }
        public void addNumEastOne()
        {
            this.num_east += 1;
        }

        public static int getWidth() { return width; }
        public static int getHeight() { return height; }
        public int getWidth0() { return width; }
        public int getHeight0() { return height; }
        public string getName() { return name; }
        public int getPosx() { return posx; }
        public int getPosy() { return posy; }
        public static int GetIndex(ModuleOne[] modules, string name)
        {
            int index = 0;
            foreach (ModuleOne m in modules)
            {
                if (m.name == name)
                    return index;
                index++;
            }
            return -1;
        }
        public static LineInfo[] GetLineInfo2(List<Module> modulesList, int X, int Y)
        {
            Grid myGrid = new Grid(X, Y); // Grid class instance initialization
            ModuleOne[] modules = myGrid.readModule(modulesList);
            int num_par = 0;
            double num_partition = Math.Sqrt((double)modules.Length);
            if ((int)num_partition - num_partition == 0)
            {
                num_par = (int)num_partition - 1;
            }
            else
            {
                num_par = (int)num_partition;
            }
            LineInfo[] allLine = new LineInfo[1000];
            int[] rows = new int[num_par];
            int[] columns = new int[num_par];
            // 调用DataAccess中的方法，读入关系表，包含源模块、目标模块和是否双向
            List<RelationOperator.relation> relationArray = RelationOperator.GetRelationArray();
            //int[][] relation = myGrid.readRelationArray(modules.Length, RelationFile);
            //		myGrid.setObstacleForModules(modules);
            myGrid.getGlobalInfo(modules, relationArray, rows, columns);
            myGrid.setRouteForModules(modules, relationArray, rows, columns,allLine);
            Console.Write("successful!");
            return allLine;
        }
        
        //public static void Main(string[] args)
        //{
        //    Grid myGrid = new Grid(2000, 1600); // Grid class instance initialization
        //    ModuleOne[] modules = myGrid.readModule(PositionFile);
        //    //ModuleOne mm = modules[Array.IndexOf(modules, 'm')];
        //    // 调用廖真给我的输入将其转换为ModuleOne类
        //    int num_par = 0;
        //    double num_partition = Math.Sqrt((double)modules.Length);
        //    if ((int)num_partition - num_partition == 0)
        //    {
        //        num_par = (int)num_partition - 1;
        //    }
        //    else
        //    {
        //        num_par = (int)num_partition;
        //    }

        //    int[] rows = new int[num_par];
        //    int[] columns = new int[num_par];
        //    // 调用DataAccess中的方法，读入关系表，包含源模块、目标模块和是否双向
        //    List<RelationOperator.relation> relationArray = RelationOperator.GetRelationArray();
        //    //int[][] relation = myGrid.readRelationArray(modules.Length, RelationFile);
        //    //		myGrid.setObstacleForModules(modules);
        //    myGrid.getGlobalInfo(modules, relationArray, rows, columns);
        //    myGrid.setRouteForModules(modules, relationArray, rows, columns, LineFile);
        //    Console.Write("successful!");
        //}
    }
}
