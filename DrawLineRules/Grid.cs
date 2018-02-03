using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DrawLineRules
{
    public class Grid
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

        public ModuleOne[] readModule(List<Module> modulesList )
        {
            int len = modulesList.Count;
            ModuleOne[] modules = new ModuleOne[len-2];
            ///////////////////the first line and second line///////////////////      
            //level = Convert.ToInt32(modulesList[0].moduleName);
            ModuleOne.setWidth(Convert.ToInt32(modulesList[0].x));
            ModuleOne.setHeight(Convert.ToInt32(modulesList[0].y));
            ModuleOne.modx = Convert.ToInt32(modulesList[1].x);
            ModuleOne.mody = Convert.ToInt32(modulesList[1].y);
            for (int i = 2; i < len; i++)
            {
                modules[i - 2] = new ModuleOne(modulesList[i].moduleName,modulesList[i].x, modulesList[i].y);
            }
            return modules;
        }

        public void setRouteForModules(ModuleOne[] modules, List<RelationOperator.relation> relation, int[] rows, int[] columns, List<ModuleOne.LineInfo> allLine)
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
            step_column[0] = ModuleOne.gapy / (2*rows[0] + 1);
            step_row[columns.Length-1] = ModuleOne.gapx / (2*columns[columns.Length-1] + 1);
            GridPoint start = null;
            GridPoint target = null;
            foreach (RelationOperator.relation relationOne in relation)
            {
                ModuleOne moduleSource = modules[ModuleOne.GetIndex(modules, relationOne.sourceName)];
                ModuleOne moduleTarget = modules[ModuleOne.GetIndex(modules, relationOne.targetName)];
                string bidirection = relationOne.bidirection;
                string relationName = relationOne.relationName;
                string comment = relationOne.comment;
                int show = relationOne.show;
                int com = moduleSource.compareModuleAdd(moduleTarget);
                switch (com)
                {
                    case 1:
                    case 2:
                    case 8:
                    case 9:
                    case 11:
                        start = moduleSource.northPin(GridPoint.myGrid, com);
                        target = moduleTarget.southPin(GridPoint.myGrid, com);
                        break;
                    case 3:
                        start = moduleSource.eastPin(GridPoint.myGrid, com);
                        target = moduleTarget.westPin(GridPoint.myGrid, com);
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 13:
                    case 15:
                        start = moduleSource.southPin(GridPoint.myGrid, com);
                        target = moduleTarget.northPin(GridPoint.myGrid, com);
                        break;
                    case 7:
                        start = moduleSource.westPin(GridPoint.myGrid, com);
                        target = moduleTarget.eastPin(GridPoint.myGrid, com);
                        break;
                    case 10:
                    case 14:
                        start = moduleSource.eastPin(GridPoint.myGrid, com);
                        target = moduleTarget.eastPin(GridPoint.myGrid, com);
                        break;
                    case 12:
                    case 16:
                        start = moduleSource.northPin(GridPoint.myGrid, com);
                        target = moduleTarget.northPin(GridPoint.myGrid, com);
                        break;
                    default:
                        Console.WriteLine("no left pin");
                        break;
                }
                List<int[]> lineOnePath = Route(start, target, com, rows, columns, r, c, step_row, step_column, moduleSource, moduleTarget, bidirection);
                saveLine(lineOnePath, allLine, relationName,comment,show);
            }     
        }

        public void getGlobalInfo(ModuleOne[] modules, List<RelationOperator.relation> relation, int[] rows, int[] columns)
        {
            foreach (RelationOperator.relation relationOne in relation)
            {
                ModuleOne moduleSource = modules[ModuleOne.GetIndex(modules, relationOne.sourceName)];
                ModuleOne moduleTarget = modules[ModuleOne.GetIndex(modules, relationOne.targetName)];
                int com = moduleSource.compareModuleAdd(moduleTarget);
                computeRowColumn(moduleSource, moduleTarget, com, rows, columns);
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
                    rows[mi.getPosy() / ModuleOne.mody] += 1;
                    break;
                case 3:
                    mi.addNumEastOne();
                    mj.addNumWestOne();
                    break;
                case 4:
                case 6:
                    mi.addNumSouthOne();
                    mj.addNumNorthOne();
                    rows[mi.getPosy() / ModuleOne.mody+1] += 1;
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
                    rows[mi.getPosy() / ModuleOne.mody] += 1;
                    rows[mj.getPosy() / ModuleOne.mody+1] += 1;
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
                    rows[mi.getPosy() / ModuleOne.mody] += 1;
                    rows[mj.getPosy() / ModuleOne.mody+1] += 1;
                    columns[mi.getPosx() / ModuleOne.modx] += 1;
                    break;
                case 12:
                case 16:
                    mi.addNumNorthOne();
                    mj.addNumNorthOne();
                    rows[mi.getPosy() / ModuleOne.mody] += 1;
                    break;
                case 13:
                    mi.addNumSouthOne();
                    mj.addNumNorthOne();
                    rows[mi.getPosy() / ModuleOne.mody+1] += 1;
                    rows[mj.getPosy() / ModuleOne.mody ] += 1;
                    columns[mi.getPosx() / ModuleOne.modx] += 1;
                    break;
                case 15:
                    mi.addNumSouthOne();
                    mj.addNumNorthOne();
                    rows[mi.getPosy() / ModuleOne.mody+1] += 1;
                    rows[mj.getPosy() / ModuleOne.mody] += 1;
                    columns[mi.getPosx() / ModuleOne.modx - 1] += 1;
                    break;
                default:
                    break;
            }
        }

        public List<int[]> Route(GridPoint start, GridPoint target , int com, int[] rows, int[] columns, int[] r, int[] c, int[] step_row, int[] step_column, ModuleOne mi, ModuleOne mj, string bidirection)
        {//返回一条路径所对应的线段
            //if (start == null || target == null)
            //    return -1;
            GridPoint[] trace = new GridPoint[10];
            //记录一条路径的拐点，根据已有的布线算法，拐点最多3个
            int x = 0;
            int y = 0;
            int delta_x = 0;
            int delta_y = 0;
            List<int[]> line = new List<int[]>();
            if (start == target)
            {
                Console.WriteLine("start and target are the same!");
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
                        y = start.getPosy() / ModuleOne.mody;
                        trace[1] = start.northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_x = target.getPosx() - start.getPosx();
                        trace[2] = trace[1].horizontalStepVertex(delta_x);
                        trace[3] = target;
                        break;
                    case 4:
                    case 6:
                        y = start.getPosy() / ModuleOne.mody+1;
                        trace[1] = start.southStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_x = target.getPosx() - start.getPosx();
                        trace[2] = trace[1].horizontalStepVertex(delta_x);
                        trace[3] = target;
                        break;
                    case 9:
                        y = start.getPosy() / ModuleOne.mody;
                        trace[1] = start.northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        x = start.getPosx() / ModuleOne.modx - 1;
                        trace[2] = trace[1].goWestStepVertex(mi, c[x], step_row[x]);
                        c[x] += 1;
                        trace[5] = target;
                        y = target.getPosy() / ModuleOne.mody+1;
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
                        y = start.getPosy() / ModuleOne.mody;
                        trace[1] = start.northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        x = start.getPosx() / ModuleOne.modx;
                        trace[2] = trace[1].goEastStepVertex(mi, c[x], step_row[x]);
                        c[x] += 1;
                        trace[5] = target;
                        y = target.getPosy() / ModuleOne.mody+1;
                        trace[4] = trace[5].southStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_y = trace[2].getPosy() - trace[4].getPosy();
                        trace[3] = trace[2].verticalStepVertex(delta_y);
                        break;
                    case 12:
                    case 16:
                        y = start.getPosy() / ModuleOne.mody;
                        trace[1] = start.northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_x = target.getPosx() - start.getPosx();
                        trace[2] = trace[1].horizontalStepVertex(delta_x);
                        trace[3] = target;
                        break;
                    case 13:
                        y = start.getPosy() / ModuleOne.mody+1;
                        trace[1] = start.southStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        x = start.getPosx() / ModuleOne.modx;
                        trace[2] = trace[1].goEastStepVertex(mi, c[x], step_row[x]);
                        c[x] += 1;
                        trace[5] = target;
                        y = target.getPosy() / ModuleOne.mody;
                        trace[4] = trace[5].northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_y = trace[2].getPosy() - trace[4].getPosy();
                        trace[3] = trace[2].verticalStepVertex(delta_y);
                        break;
                    case 15:
                        y = start.getPosy() / ModuleOne.mody+1;
                        trace[1] = start.southStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        x = start.getPosx() / ModuleOne.modx - 1;
                        trace[2] = trace[1].goWestStepVertex(mi, c[x], step_row[x]);
                        c[x] += 1;
                        trace[5] = target;
                        y = target.getPosy() / ModuleOne.mody;
                        trace[4] = trace[5].northStepVertex(r[y], step_column[y]);
                        r[y] += 1;
                        delta_y = trace[2].getPosy() - trace[4].getPosy();
                        trace[3] = trace[2].verticalStepVertex(delta_y);
                        break;
                    default:
                        break;
                }
                    //需要传入是否双向
                line = plotOneTrace(trace, bidirection);
            }
            return line;
        }
        public int saveLine(List<int[]> lineOne, List<ModuleOne.LineInfo> lineAll, string relationName,string comment, int show)
        {
            //for (int i = 0; i < lineAll.Count; i++)
            //{
                //if (lineAll[i][0] == 0 && lineAll[i][1] == 0 && lineAll[i][2] == 0 && lineAll[i][3] == 0)
                //if(lineAll[i].line==null)
                //{
                    for (int j = 0; j < lineOne.Count; j++)
                    {
                        if (lineOne[j] == null || (lineOne[j][0] == 0 && lineOne[j][1] == 0 && lineOne[j][2] == 0 && lineOne[j][3] == 0))
                        {
                            return 0;
                        }
                        else
                        {
                            ModuleOne.LineInfo lineRoute = new ModuleOne.LineInfo();
                            lineRoute.line = lineOne[j];
                            lineRoute.lineName = relationName;
                            lineRoute.lineComment = comment;
                            lineRoute.show = show;
                            lineAll.Add(lineRoute);
                            
                        }
                    //}
                //}
            }
            return -1;
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

        public List<int[]> plotOneTrace(GridPoint[] trace0, String bidirection)
        {
            //因为拐点最多5个，变成线段也最多10个
            GridPoint[] trace = new GridPoint[20];
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
            GridPoint[] line = new GridPoint[20];
            int n = 0;
            //int[][] lineTxt = new int[line.Length / 2][];
            List<int[]> lineLong = new List<int[]>();
            //for (int i = 0; i < line.Length / 2; i++)
            //{
            //    lineTxt[i] = new int[5];
            //}
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
            //////////////////////////////对于第一条线段和最后一条线段需要标明有没有箭头，
            if (line[2] == null)
            {
                if (int.Parse(bidirection) == 1)
                {
                    int[] lineOne = new int[5];
                    lineOne[0] = line[0].getPosx();
                    lineOne[1] = line[0].getPosy();
                    lineOne[2] = line[0 + 1].getPosx();
                    lineOne[3] = line[0 + 1].getPosy();
                    lineOne[4] = 3;
                    lineLong.Add(lineOne);
                }
                else
                {
                    int[] lineOne = new int[5];
                    lineOne[0] = line[0].getPosx();
                    lineOne[1] = line[0].getPosy();
                    lineOne[2] = line[0 + 1].getPosx();
                    lineOne[3] = line[0 + 1].getPosy();
                    lineOne[4] = GetDirectionInfo(lineOne[0], lineOne[1], lineOne[2], lineOne[3], 0);//终止点有箭头
                    lineLong.Add(lineOne);
                }
            }
            else
            {
                if (int.Parse(bidirection) == 1)
                {
                    int[] lineOne = new int[5];
                    lineOne[0] = line[0].getPosx();
                    lineOne[1] = line[0].getPosy();
                    lineOne[2] = line[0 + 1].getPosx();
                    lineOne[3] = line[0 + 1].getPosy();
                    lineOne[4] = GetDirectionInfo(lineOne[0], lineOne[1], lineOne[2], lineOne[3], 1);//起始点有箭头
                    lineLong.Add(lineOne);
                }
                else
                {
                    int[] lineOne = new int[5];
                    lineOne[0] = line[0].getPosx();
                    lineOne[1] = line[0].getPosy();
                    lineOne[2] = line[0 + 1].getPosx();
                    lineOne[3] = line[0 + 1].getPosy();
                    lineOne[4] = 0;
                    lineLong.Add(lineOne);
                }
                int index = 0;
                
                for (int i = 1; i < line.Length - 1; i++)
                {
                    if (!(line[i + 2] == null))
                    {
                        int[] lineOne = new int[5];
                        lineOne[0] = line[i].getPosx();
                        lineOne[1] = line[i].getPosy();
                        lineOne[2] = line[i + 1].getPosx();
                        lineOne[3] = line[i + 1].getPosy();
                        lineOne[4] = 0;
                        lineLong.Add(lineOne);
                       // Console.WriteLine(lineTxt[i][0] + " " + lineTxt[i][1] + " " + lineTxt[i][2] + " " + lineTxt[i][3] + bidirection);
                    }
                    else
                    {
                        index = i;
                        break;
                    }
                }
                int[] lineOneIndex = new int[5];
                lineOneIndex[0] = line[index].getPosx();
                lineOneIndex[1] = line[index].getPosy();
                lineOneIndex[2] = line[index + 1].getPosx();
                lineOneIndex[3] = line[index + 1].getPosy();
                lineOneIndex[4] = GetDirectionInfo(lineOneIndex[0], lineOneIndex[1], lineOneIndex[2], lineOneIndex[3], 0);//起始点有箭头
                lineLong.Add(lineOneIndex);
            }
            return lineLong;
        }
        private int GetDirectionInfo(int x1, int y1, int x2, int y2 , int first)
        {
            if (first == 1)
            {
                if (x1 - x2 < 0 || y1 - y2 < 0)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (x1 - x2 < 0 || y1 - y2 < 0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }       
        }


    }
}
