using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static int HelixNumber(int x, int y)
        {
            int layer = Math.Max(Math.Abs(x), Math.Abs(y));
            int maxNumber = (2 * layer + 1) * (2 * layer + 1);
            int key;
            if (y == -layer)
                key = maxNumber + y + x;
            else if (x == -layer)
                key = maxNumber + (3 * x - y);
            else if (y == layer)
                key = maxNumber + (-x - 5 * y);
            else
                key = maxNumber + (-7 * x + y);
            return key;
        }
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

    }
}
