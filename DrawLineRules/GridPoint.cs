using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawLineRules
{
    //设置画线搜索时每个格点的属性和方法,
    public class GridPoint
    {
        private int posx;
        private int posy;
        private int gVal = 0;

        public GridPoint(int x, int y)
        {
            posx = x;
            posy = y;
        }

        public GridPoint goWestStepVertex(ModuleOne m, int i, int step)
        {
            return myGrid.gridPointAt(m.getPosx() - (i + 1) * step, posy);
        }

        public GridPoint goEastStepVertex(ModuleOne m, int i, int step)
        {
            return myGrid.gridPointAt(m.getPosx() + m.getWidth0() + ModuleOne.gapx - (i + 1) * step, posy);
        }

        public GridPoint northStepVertex(int i, int step)
        {
            return myGrid.gridPointAt(posx, posy - (i + 1) * step);
        }

        public GridPoint southStepVertex(int i, int step)
        {
            return myGrid.gridPointAt(posx, posy + ModuleOne.gapy - (i + 1) * step);
        }

        public GridPoint eastStepVertex(int i, int step)
        {
            return myGrid.gridPointAt(posx + ModuleOne.gapx - (i + 1) * step, posy);
        }

        public GridPoint westStepVertex(int i, int step)
        {
            return myGrid.gridPointAt(posx - (i + 1) * step, posy);
        }

        public GridPoint verticalStepVertex(int i)
        {
            return myGrid.gridPointAt(posx, posy + i);
        }

        public GridPoint horizontalStepVertex(int i)
        {
            return myGrid.gridPointAt(posx + i, posy);
        }

        public int getPosx() { return posx; }
        public int getPosy() { return posy; }

        public void setRouted()
        {
            gVal = 1;
        }
        public bool isRouted() { return (gVal == 1); }

        public static Grid myGrid;
    }
}
