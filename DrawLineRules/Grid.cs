using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawLineRules
{
    class Grid
    {
        GridPoint[][] gridArray;
        GridPoint src;
        GridPoint tgt;
        public Grid(int w, int h)
        {
            gridArray = new GridPoint[w][];
            for (int i = 0; i < w; i++)
            {
                gridArray[i] = new GridPoint[h];
                for (int j = 0; j < h; j++)
                {
                    gridArray[i][j] = new GridPoint(i, j);
                }
            }
            GridPoint.myGrid = this;
        }

        public int width()
        {
            return gridArray.Length;
        }

        public int height()
        {
            return gridArray[0].Length;
        }


        public GridPoint gridPointAt(int x, int y)
        {
            if (x < 0 || x >= width())
                return null;
            if (y < 0 || y >= height())
                return null;
            return gridArray[x][y];
        }

        public ModuleOne[] readModule(String fileName)
        {
            //string path = @"C:\CloudMap2\history.ini";
            int length = 0;
            ModuleOne[] modules = null;
            if (File.Exists(fileName))
            {
                //StreamReader sr = new StreamReader(path);
                string[] strs1 = File.ReadAllLines(fileName);
                int len = strs1.Length;
                ///////////////////the first line///////////////////
                String[] size = strs1[0].Split(' ');
                ModuleOne.modx = Convert.ToInt32(size[0]);
                ModuleOne.mody = Convert.ToInt32(size[1]);
                //////////////////the second line//////////////////
                String[] label = strs1[1].Split(' ');
                length = Convert.ToInt32(label[0]);
                modules = new ModuleOne[length];
                ModuleOne.setWidth(Convert.ToInt32(label[1]));
                ModuleOne.setHeight(Convert.ToInt32(label[2]));
                for (int i = 2; i < len; i++)
                {
                    String[] temp = strs1[i].Split(' ');
                    modules[Convert.ToInt32(temp[0]) - 1] = new ModuleOne(Convert.ToInt32(temp[0]) - 1,
                            Convert.ToInt32(temp[1]), Convert.ToInt32(temp[2]));
                    Console.WriteLine(strs1[i]);
                }
            }
            return modules;
        }

        public int[][] readRelationArray(int len, String RelationFile)
        {
            String line; // 一行数据
            int[][] relation = new int[len][];
            for (int i = 0; i < len; i++)
                relation[i] = new int[len];
            if (File.Exists(RelationFile))
            {
                string[] strs1 = File.ReadAllLines(RelationFile);
                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < len; j++)
                    {
                        String[] temp = strs1[i].Split(' ');
                        relation[i][j] = Convert.ToInt32(temp[j]);
                    }
                }
            }
            return relation;
        }

        //	public void setObstacleForModules(ModuleOne[] modules) throws InterruptedException, IOException {
        //		for (int i = 0; i < modules.length; i++) {
        //			modules[i].setObstacle(GridPoint.myGrid);
        //		}
        //	}

        public void setRouteForModules(ModuleOne[] modules, int[][] relation, int[] rows, int[] columns, String LineFile)
        {
            ModuleOne.gapy = ModuleOne.mody - ModuleOne.getHeight();
            ModuleOne.gapx = ModuleOne.modx - ModuleOne.getWidth();

            int[] step_column = new int[columns.Length];//记录每个区域的划分后的间隔
            int[] step_row = new int[rows.Length];
            int[] r = new int[rows.Length];
            int[] c = new int[columns.Length];
            for (int m = 0; m < rows.Length; m++)
            {
                step_column[m] = ModuleOne.gapy / (rows[m] + 1);
                step_row[m] = ModuleOne.gapx / (columns[m] + 1);
            }
            GridPoint start = null;
            GridPoint target = null;
            for (int i = 0; i < relation.Length; i++)
            {
                for (int j = 0; j < relation[0].Length; j++)
                {
                    if (relation[i][j] == 1)
                    {
                        int com = modules[i].compareModuleAdd(modules[j]);
                        switch (com)
                        {
                            case 1:
                            case 2:
                            case 8:
                            case 9:
                            case 11:
                                start = modules[i].northPin(GridPoint.myGrid, com);
                                target = modules[j].southPin(GridPoint.myGrid, com);
                                break;
                            case 3:
                                start = modules[i].eastPin(GridPoint.myGrid, com);
                                target = modules[j].westPin(GridPoint.myGrid, com);
                                break;
                            case 4:
                            case 5:
                            case 6:
                            case 13:
                            case 15:
                                start = modules[i].southPin(GridPoint.myGrid, com);
                                target = modules[j].northPin(GridPoint.myGrid, com);
                                break;
                            case 7:
                                start = modules[i].westPin(GridPoint.myGrid, com);
                                target = modules[j].eastPin(GridPoint.myGrid, com);
                                break;
                            case 10:
                            case 14:
                                start = modules[i].eastPin(GridPoint.myGrid, com);
                                target = modules[j].eastPin(GridPoint.myGrid, com);
                                break;
                            case 12:
                            case 16:
                                start = modules[i].northPin(GridPoint.myGrid, com);
                                target = modules[j].northPin(GridPoint.myGrid, com);
                                break;
                            default:
                                Console.WriteLine("no left pin");
                                break;
                        }
                        Route(start, target, LineFile, com, rows, columns, r, c, step_row, step_column, modules[i], modules[j]);
                    }
                }
            }
        }

        public void getGlobalInfo(ModuleOne[] modules, int[][] relation, int[] rows, int[] columns)
        {
            for (int i = 0; i < relation.Length; i++)
            {
                for (int j = 0; j < relation[0].Length; j++)
                {
                    if (relation[i][j] == 1)
                    {
                        int com = modules[i].compareModuleAdd(modules[j]);
                        computeRowColumn(modules[i], modules[j], com, rows, columns);
                    }
                }
            }
        }

        public void computeRowColumn(ModuleOne mi, ModuleOne mj, int com, int[] rows, int[] columns)
        {
            switch (com)
            {
                case 1:
                    mi.addNumNorthOne();
                    mj.addNumSouthOne();
                    break;
                case 2:
                case 8:
                    mi.addNumNorthOne();
                    mj.addNumSouthOne();
                    rows[mi.getPosy() / ModuleOne.mody - 1] += 1;
                    break;
                case 3:
                    mi.addNumEastOne();
                    mj.addNumWestOne();
                    break;
                case 4:
                case 6:
                    mi.addNumSouthOne();
                    mj.addNumNorthOne();
                    rows[mi.getPosy() / ModuleOne.mody] += 1;
                    break;
                case 5:
                    mi.addNumSouthOne();
                    mj.addNumNorthOne();
                    break;
                case 7:
                    mi.addNumWestOne();
                    mj.addNumEastOne();
                    break;
                case 9:
                    mi.addNumNorthOne();
                    mj.addNumSouthOne();
                    rows[mi.getPosy() / ModuleOne.mody - 1] += 1;
                    rows[mj.getPosy() / ModuleOne.mody] += 1;
                    columns[mi.getPosx() / ModuleOne.modx - 1] += 1;
                    break;
                case 10:
                case 14:
                    mi.addNumEastOne();
                    mj.addNumEastOne();
                    columns[mi.getPosx() / ModuleOne.modx] += 1;
                    break;
                case 11:
                    mi.addNumNorthOne();
                    mj.addNumSouthOne();
                    rows[mi.getPosy() / ModuleOne.mody - 1] += 1;
                    rows[mj.getPosy() / ModuleOne.mody] += 1;
                    columns[mi.getPosx() / ModuleOne.modx] += 1;
                    break;
                case 12:
                case 16:
                    mi.addNumNorthOne();
                    mj.addNumNorthOne();
                    rows[mi.getPosy() / ModuleOne.mody - 1] += 1;
                    break;
                case 13:
                    mi.addNumSouthOne();
                    mj.addNumNorthOne();
                    rows[mi.getPosy() / ModuleOne.mody] += 1;
                    rows[mj.getPosy() / ModuleOne.mody - 1] += 1;
                    columns[mi.getPosx() / ModuleOne.modx] += 1;
                    break;
                case 15:
                    mi.addNumSouthOne();
                    mj.addNumNorthOne();
                    rows[mi.getPosy() / ModuleOne.mody] += 1;
                    rows[mj.getPosy() / ModuleOne.mody - 1] += 1;
                    columns[mi.getPosx() / ModuleOne.modx - 1] += 1;
                    break;
                default:
                    break;
            }
        }

        public int Route(GridPoint start, GridPoint target, String LineFile, int com, int[] rows, int[] columns, int[] r, int[] c, int[] step_row, int[] step_column, ModuleOne mi, ModuleOne mj)
        {
            if (start == null || target == null)
                return -1;
            GridPoint[] trace = new GridPoint[2 * width()];
            int x = 0;
            int y = 0;
            int delta_x = 0;
            int delta_y = 0;
            if (start == target)
            {
                Console.WriteLine("start and target are the same!");
                return 0;
            }
            else
            {
                trace[0] = start;
                switch (com)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                        trace[1] = target;
                        break;
                    case 2:
                    case 8:
                        y = start.getPosy() / ModuleOne.mody - 1;
                        trace[1] = start.northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_x = target.getPosx() - start.getPosx();
                        trace[2] = trace[1].horizontalStepVertex(delta_x);
                        trace[3] = target;
                        break;
                    case 4:
                    case 6:
                        y = start.getPosy() / ModuleOne.mody;
                        trace[1] = start.southStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_x = target.getPosx() - start.getPosx();
                        trace[2] = trace[1].horizontalStepVertex(delta_x);
                        trace[3] = target;
                        break;
                    case 9:
                        y = start.getPosy() / ModuleOne.mody - 1;
                        trace[1] = start.northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        x = start.getPosx() / ModuleOne.modx - 1;
                        trace[2] = trace[1].goWestStepVertex(mi, c[x], step_row[x]);
                        c[x] += 1;
                        trace[5] = target;
                        y = target.getPosy() / ModuleOne.mody;
                        trace[4] = trace[5].southStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_y = trace[2].getPosy() - trace[4].getPosy();
                        trace[3] = trace[2].verticalStepVertex(delta_y);
                        break;
                    case 10:
                    case 14:
                        x = start.getPosx() / ModuleOne.modx;
                        trace[1] = start.eastStepVertex(c[x], step_row[x]);
                        c[x] += 1;
                        delta_y = start.getPosy() - target.getPosy();
                        trace[2] = trace[1].verticalStepVertex(delta_y);
                        trace[3] = target;
                        break;
                    case 11:
                        y = start.getPosy() / ModuleOne.mody - 1;
                        trace[1] = start.northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        x = start.getPosx() / ModuleOne.modx;
                        trace[2] = trace[1].goEastStepVertex(mi, c[x], step_row[x]);
                        c[x] += 1;
                        trace[5] = target;
                        y = target.getPosy() / ModuleOne.mody;
                        trace[4] = trace[5].southStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_y = trace[2].getPosy() - trace[4].getPosy();
                        trace[3] = trace[2].verticalStepVertex(delta_y);
                        break;
                    case 12:
                    case 16:
                        y = start.getPosy() / ModuleOne.mody - 1;
                        trace[1] = start.northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_x = target.getPosx() - start.getPosx();
                        trace[2] = trace[1].horizontalStepVertex(delta_x);
                        trace[3] = target;
                        break;
                    case 13:
                        y = start.getPosy() / ModuleOne.mody;
                        trace[1] = start.southStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        x = start.getPosx() / ModuleOne.modx;
                        trace[2] = trace[1].goEastStepVertex(mi, c[x], step_row[x]);
                        c[x] += 1;
                        trace[5] = target;
                        y = target.getPosy() / ModuleOne.mody - 1;
                        trace[4] = trace[5].northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_y = trace[2].getPosy() - trace[4].getPosy();
                        trace[3] = trace[2].verticalStepVertex(delta_y);
                        break;
                    case 15:
                        y = start.getPosy() / ModuleOne.mody;
                        trace[1] = start.southStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        x = start.getPosx() / ModuleOne.modx - 1;
                        trace[2] = trace[1].goWestStepVertex(mi, c[x], step_row[x]);
                        c[x] += 1;
                        trace[5] = target;
                        y = target.getPosy() / ModuleOne.mody - 1;
                        trace[4] = trace[5].northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_y = trace[2].getPosy() - trace[4].getPosy();
                        trace[3] = trace[2].verticalStepVertex(delta_y);
                        break;
                    default:
                        break;
                }
                if (!(trace == null))
                {
                    plotOneTrace(trace, LineFile);
                }
            }
            return 0;
        }

        public void writeLine(int[][] line, String LineFile)
        {
            if (!File.Exists(LineFile))
            {
                FileStream fs1 = new FileStream(LineFile, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs1);
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i][0] == 0 && line[i][1] == 0 && line[i][2] == 0 && line[i][3] == 0)
                    {
                        break;
                    }
                    else
                    {
                        sw.WriteLine(line[i][0] + " " + line[i][1] + " " + line[i][2] + " " + line[i][3]);
                    }
                }
                sw.Flush();
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs2 = new FileStream(LineFile, FileMode.Append, FileAccess.Write);
                StreamWriter sw2 = new StreamWriter(fs2);
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j][0] == 0 && line[j][1] == 0 && line[j][2] == 0 && line[j][3] == 0)
                    {
                        break;
                    }
                    else
                    {
                        sw2.WriteLine(line[j][0] + " " + line[j][1] + " " + line[j][2] + " " + line[j][3]);
                    }
                }
                sw2.Flush();
                sw2.Close();
                fs2.Close();
            }
        }

        public void plotOneTrace(GridPoint[] trace0, String LineFile)
        {
            GridPoint[] trace = new GridPoint[2 * width()];
            int nn = 0;
            for (int k = 0; k < trace0.Length; k++)
            {
                if (!(trace0[k + 1] == null))
                {
                    if ((trace0[k].getPosx() == trace0[k + 1].getPosx())
                            && (trace0[k].getPosy() == trace0[k + 1].getPosy()))
                    {
                        trace[nn] = trace0[k];
                        nn++;
                        k++;
                    }
                    else
                    {
                        trace[nn] = trace0[k];
                        nn++;
                    }
                }
                else
                {
                    trace[nn] = trace0[k];
                    break;
                }
            }
            GridPoint[] line = new GridPoint[2 * width()];
            int n = 0;
            int[][] lineTxt = new int[line.Length / 2][];
            for (int i = 0; i < line.Length / 2; i++)
            {
                lineTxt[i] = new int[4];
            }
            line[0] = trace[0];
            n++;
            for (int m = 1; m < trace.Length; m++)
            {
                if (!(trace[m] == null))
                {
                    if (trace[m + 1] == null)
                    {
                        line[n] = trace[m];
                        n++;
                        break;
                    }
                    bool k0 = (trace[m - 1].getPosx() == trace[m].getPosx())
                            && (trace[m + 1].getPosx() == trace[m].getPosx());
                    bool k1 = (trace[m - 1].getPosy() == trace[m].getPosy())
                            && (trace[m + 1].getPosy() == trace[m].getPosy());
                    if (k0 || k1)
                    {
                        continue;
                    }
                    else
                    {
                        line[n] = trace[m];
                        n++;
                    }
                }
            }
            for (int i = 0; i < line.Length; i++)
            {
                if (!(line[i + 1] == null))
                {
                    lineTxt[i][0] = line[i].getPosx();
                    lineTxt[i][1] = line[i].getPosy();
                    lineTxt[i][2] = line[i + 1].getPosx();
                    lineTxt[i][3] = line[i + 1].getPosy();
                    Console.WriteLine(lineTxt[i][0] + " " + lineTxt[i][1] + " " + lineTxt[i][2] + " " + lineTxt[i][3]);
                }
                else
                    break;
            }
            writeLine(lineTxt, LineFile);
            //return lineTxt;
        }
    }
}
