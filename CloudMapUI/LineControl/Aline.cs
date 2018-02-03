using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudMapUI
{
    public partial class ALine : Control
    {
        public ALine()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor
                | ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.Opaque, true);
            BackColor = Color.Transparent;
        }

        //默认线从左到右，从上到下画
        int[] points = { 20, 20, 20, 80, 2 };
        private int penwidth = 1;
        private Color pencolor = Color.Black;
        private int penid = 0;
        private int controlwidth;
        private int controlheight;
        //private int penid = 0;
        public void SetControlSize()
        {
            if (points[0] == points[2])//竖线
            {
                controlwidth = 8 * Penwidth + 1;
                controlheight = Math.Abs(points[1] - points[3]) + 1;
            }
            else//横线
            {
                controlwidth = Math.Abs(points[0] - points[2]) + 1;
                controlheight = 8 * Penwidth + 1;
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            this.SetControlSize();
            base.SetBoundsCore(x, y, this.controlwidth, this.controlheight, specified);
        }

        [Browsable(true), Category("自定义"), Description("设置控件起始和终止点坐标")]
        public int[] Points
        {
            get { return points; }
            set
            {
                points = value;
                this.Invalidate();
            }
        }

        [Browsable(true), Category("自定义"), Description("设置控件画笔宽度")]
        public int Penwidth
        {
            get { return penwidth; }
            set
            {
                penwidth = value;
                this.Invalidate();
            }
        }

        [Browsable(true), Category("自定义"), Description("设置控件画笔颜色")]
        public Color Pencolor
        {
            get { return pencolor; }
            set
            {
                pencolor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), Category("自定义"), Description("设置控件所属id号")]
        public int Penid
        {
            get { return penid; }
            set
            {
                penid = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen pen = new Pen(SystemColors.ControlText);//定义一个画笔            
            pen.Width = Penwidth;//使用控件的属性，这样使用者可以通过修改属性来控制这个控件的行为，而不是修改源代码后再编译
            pen.Color = Pencolor;
            SetBoundsCore(this.Location.X, this.Location.Y, this.controlwidth, this.controlheight, BoundsSpecified.None);


            //横线
            if (points[1] == points[3])
            {
                e.Graphics.DrawLine(pen, 0, 4 * Penwidth, points[2] - points[0], points[3] - points[1] + 4 * Penwidth);
                //箭头显示位置，从数组中获取的一个标志位，代表该线段的某一端或两端有箭头
                switch (points[4])
                {
                    case 1://右边有箭头
                        if (points[0] < points[2])
                        {
                            Point[] pointRightarrow = this.DrawRightArrow(points[2] - points[0], points[3] - points[1] + 4 * Penwidth);
                            e.Graphics.DrawLine(pen, pointRightarrow[0], pointRightarrow[1]);
                            e.Graphics.DrawLine(pen, pointRightarrow[0], pointRightarrow[2]);
                        }
                        else
                        {
                            Point[] pointRightarrow = this.DrawRightArrow(0, 4 * Penwidth);
                            e.Graphics.DrawLine(pen, pointRightarrow[0], pointRightarrow[1]);
                            e.Graphics.DrawLine(pen, pointRightarrow[0], pointRightarrow[2]);
                        }
                        break;

                    case 2://左边有箭头
                        if (points[0] < points[2])
                        {
                            Point[] pointLeftarrow = this.DrawLeftArrow(0, 4 * Penwidth);
                            e.Graphics.DrawLine(pen, pointLeftarrow[0], pointLeftarrow[1]);
                            e.Graphics.DrawLine(pen, pointLeftarrow[0], pointLeftarrow[2]);
                        }
                        else
                        {
                            Point[] pointLeftarrow = this.DrawLeftArrow(points[2] - points[0], points[3] - points[1] + 4 * Penwidth);
                            e.Graphics.DrawLine(pen, pointLeftarrow[0], pointLeftarrow[1]);
                            e.Graphics.DrawLine(pen, pointLeftarrow[0], pointLeftarrow[2]);
                        }
                        break;

                    case 3://两边都有
                        Point[] pointAllarrowL = this.DrawLeftArrow(0, 4 * Penwidth);
                        e.Graphics.DrawLine(pen, pointAllarrowL[0], pointAllarrowL[1]);
                        e.Graphics.DrawLine(pen, pointAllarrowL[0], pointAllarrowL[2]);
                        Point[] pointAllarrowR = this.DrawRightArrow(points[2] - points[0], points[3] - points[1] + 4 * Penwidth);
                        e.Graphics.DrawLine(pen, pointAllarrowR[0], pointAllarrowR[1]);
                        e.Graphics.DrawLine(pen, pointAllarrowR[0], pointAllarrowR[2]);
                        break;
                    case 0:
                        break;
                }
            }
            //竖线
            else
            {
                e.Graphics.DrawLine(pen, 4 * Penwidth, 0, points[2] - points[0] + 4 * Penwidth, points[3] - points[1]);
                switch (points[4])
                {
                    case 1:
                        if (points[1] < points[3])
                        {
                            Point[] pointDownarrow = this.DrawDownArrow(points[2] - points[0] + 4 * Penwidth, points[3] - points[1]);
                            e.Graphics.DrawLine(pen, pointDownarrow[0], pointDownarrow[1]);
                            e.Graphics.DrawLine(pen, pointDownarrow[0], pointDownarrow[2]);
                        }
                        else
                        {
                            Point[] pointDowntarrow = this.DrawDownArrow(4 * Penwidth, 0);
                            e.Graphics.DrawLine(pen, pointDowntarrow[0], pointDowntarrow[1]);
                            e.Graphics.DrawLine(pen, pointDowntarrow[0], pointDowntarrow[2]);
                        }
                        break;

                    case 2:
                        if (points[1] < points[3])
                        {
                            Point[] pointUparrow = this.DrawUpArrow(4 * Penwidth, 0);
                            e.Graphics.DrawLine(pen, pointUparrow[0], pointUparrow[1]);
                            e.Graphics.DrawLine(pen, pointUparrow[0], pointUparrow[2]);
                        }
                        else
                        {
                            Point[] pointLeftarrow = this.DrawUpArrow(points[2] - points[0] + 4 * Penwidth, points[3] - points[1]);
                            e.Graphics.DrawLine(pen, pointLeftarrow[0], pointLeftarrow[1]);
                            e.Graphics.DrawLine(pen, pointLeftarrow[0], pointLeftarrow[2]);
                        }
                        break;

                    case 3:
                        Point[] pointAllarrowU = this.DrawUpArrow(4 * Penwidth, 0);
                        e.Graphics.DrawLine(pen, pointAllarrowU[0], pointAllarrowU[1]);
                        e.Graphics.DrawLine(pen, pointAllarrowU[0], pointAllarrowU[2]);
                        Point[] pointAllarrowD = this.DrawDownArrow(points[2] - points[0] + 4 * Penwidth, points[3] - points[1]);
                        e.Graphics.DrawLine(pen, pointAllarrowD[0], pointAllarrowD[1]);
                        e.Graphics.DrawLine(pen, pointAllarrowD[0], pointAllarrowD[2]);
                        break;
                    case 0:
                        break;
                }
            }
            base.OnPaint(e);//这句不能少，这个是调用基础类（Control)的OnPaint代码
        }
        public Point[] DrawLeftArrow(int a, int b)
        {
            int arglenght = 8 + Penwidth;
            Point[] points = new Point[3];
            Point p1 = new Point(a, b);
            Point p2 = new Point(a + arglenght, b + arglenght / 2);
            Point p3 = new Point(a + arglenght, b - arglenght / 2);
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
            return points;
        }
        public Point[] DrawRightArrow(int a, int b)
        {
            int arglenght = 8 + Penwidth;
            Point[] points = new Point[3];
            Point p1 = new Point(a, b);
            Point p2 = new Point(a - arglenght, b + arglenght / 2);
            Point p3 = new Point(a - arglenght, b - arglenght / 2);
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
            return points;
        }
        public Point[] DrawUpArrow(int a, int b)
        {
            int arglenght = 8 + Penwidth;
            Point[] points = new Point[3];
            Point p1 = new Point(a, b);
            Point p2 = new Point(a + arglenght / 2, b + arglenght);
            Point p3 = new Point(a - arglenght / 2, b + arglenght);
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
            return points;
        }
        public Point[] DrawDownArrow(int a, int b)
        {
            int arglenght = 8 + Penwidth;
            Point[] points = new Point[3];
            Point p1 = new Point(a, b);
            Point p2 = new Point(a + arglenght / 2, b - arglenght);
            Point p3 = new Point(a - arglenght / 2, b - arglenght);
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
            return points;
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            Visible = false;
            Visible = true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }
    }
}
