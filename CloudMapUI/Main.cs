using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using Data;
using DataAccess;
using DrawLineRules;
using System.IO;

namespace CloudMapUI
{
    public partial class MainForm : Form
    {
        public static int penWidth=1;
        public static ModuleData moduledata;
        public static RelationData relationdata;
        public static MainForm mainform;
        public static int panelWidth;
        public static int panelHeight;
        public MainForm()
        {
            InitializeComponent();
        }

        //在没有打开项目时，和项目相关的控件不可用
        public void mainFormStatus()
        {
            if (globalParameters.dbName == null)
            {
                ToolStripMenuItem_saveImage.Enabled = false;
                ToolStripMenuItem_SaveAs.Enabled = false;
                ToolStripMenuItem_PrePrint.Enabled = false;
                ToolStripMenuItem_Print.Enabled = false;
                ToolStripMenuItem_AddModule.Enabled = false;
                ToolStripMenuItem_AddRelation.Enabled = false;
                ToolStripMenuItem_import.Enabled = false;

                toolStripButton_saveProject.Enabled = false;
                toolStripButton_saveImage.Enabled = false;
                toolStripButton_prePrint.Enabled = false;
                toolStripButton_import.Enabled = false;
                toolStripButton_addModule.Enabled = false;
                toolStripButton_addRelation.Enabled = false;

                dataGridView_module.Visible = false;
                dataGridView_relation.Visible = false;
                panel2.Visible = false;
            }
            else
            {
                ToolStripMenuItem_saveImage.Enabled = true;
                ToolStripMenuItem_SaveAs.Enabled = true;
                ToolStripMenuItem_PrePrint.Enabled = true;
                ToolStripMenuItem_Print.Enabled = true;
                ToolStripMenuItem_AddModule.Enabled = true;
                ToolStripMenuItem_AddRelation.Enabled = true;
                ToolStripMenuItem_import.Enabled = true;

                toolStripButton_saveProject.Enabled = true;
                toolStripButton_saveImage.Enabled = true;
                toolStripButton_prePrint.Enabled = true;
                toolStripButton_import.Enabled = true;
                toolStripButton_addModule.Enabled = true;
                toolStripButton_addRelation.Enabled = true;
                panel2.Visible = true;

                dataGridView_module.Visible = true;
                dataGridView_module.AutoGenerateColumns = false;
                moduledata = ModulesOperator.LoadModulesInfo();
                dataGridView_module.DataSource = moduledata.Tables[ModuleData.MODULES_TABLE].DefaultView;

                dataGridView_relation.Visible = true;
                dataGridView_relation.AutoGenerateColumns = false;
                relationdata = RelationOperator.LoadRelationInfo();
                dataGridView_relation.DataSource = relationdata.Tables[RelationData.RELATION_TABLE].DefaultView;
                
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mainFormStatus();
            AddHistoryItem();
            int width = Screen.PrimaryScreen.Bounds.Width-200;//得到与屏幕一样大小的panel1，用于存放panel4.画图
            int height = Screen.PrimaryScreen.Bounds.Height-151;
            panel1.Size = new Size(width, height);
            panel1.HorizontalScroll.Visible = true;
            panel1.VerticalScroll.Visible = true;
            panel4.Size = panel1.Size;
            panelWidth = panel4.Size.Width;
            panelHeight = panel4.Size.Height;
        }
        private void AddHistoryItem()
        {
            SystemOperator.ReadHistory();
            foreach (string history in globalParameters.dbHistory)
            {
                if (history == "")
                {
                    return;
                }
                else
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Name = history;
                    item.Text = history;
                    item.Click += new EventHandler(historyItemClik);
                    ToolStripMenuItem_history.DropDownItems.Add(item);
                }
            }
        }
        private void historyItemClik(object sender, EventArgs e)
        {
            string path = ((ToolStripMenuItem)sender).Text;
            bool fileExist = SystemOperator.OpenProject(path,true);
            if (!fileExist)
            {
                MessageBox.Show("该数据库已被删除", "关于云图", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                ((ToolStripMenuItem)sender).Visible = false;
                //this.Controls.Remove(((ToolStripMenuItem)sender).Name);
            }
            mainFormStatus();
        }
        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void NToolStripMenuItem_newProject_Click(object sender, EventArgs e)
        {
            NewProjectForm newProjrctFrom = new NewProjectForm(this);
            newProjrctFrom.ShowDialog();  
        }

        //导入项目
        private void ToolStripMenuItem_import_Click(object sender, EventArgs e)
        {
            importForm import = new importForm(this);
            import.ShowDialog();
        }

        private void ToolStripMenuItem_OpenProject_Click(object sender, EventArgs e)
        {   
            openFileDialog_OpenProject.ShowDialog();
            SystemOperator.OpenProject(openFileDialog_OpenProject.FileName, true);
            mainFormStatus();
        }

        private void ToolStripMenuItem_SaveProject_Click(object sender, EventArgs e)
        {
            //System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog_SaveProject.OpenFile();
            saveFileDialog_SaveProject.ShowDialog();
        }

        private void ToolStripMenuItem_SaveAs_Click(object sender, EventArgs e)
        {
            saveFileDialog_SaveProject.ShowDialog();
            string filePath = saveFileDialog_SaveProject.FileName;
            string[] text = globalParameters.dbPath.Split('=');
            string oldFilePath = text[1];
            if (filePath == null || filePath =="")
            {
                MessageBox.Show("路径为空！", "关于云图", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                File.Copy(oldFilePath, filePath);
            } 
        }

        private void 保存云图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog_saveImage.ShowDialog();
            SaveFileToImage(saveFileDialog_saveImage.FileName);
        }

        private void ToolStripMenuItem_Print_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custum", panel4.Width, panel4.Height);
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage);
            printPreviewDialog1.Document = printDoc;
            DialogResult result = printDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //    printDoc.Print();
        }
        private void ToolStripMenuItem_PrePrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custum", panel4.Width, panel4.Height);
            printDoc.DefaultPageSettings.Landscape = false;
            printDoc.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage);
            printPreviewDialog1.Document = printDoc; 
            DialogResult result= printPreviewDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //    printDoc.Print();
        }
        //设置打印内容
        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, panel4.Width, panel4.Height);
            //Rectangle rect = new Rectangle(0, 0, 210, 297);
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {
                this.panel4.DrawToBitmap(bmp, rect);
                e.Graphics.DrawImageUnscaledAndClipped(bmp, rect);
            }
        }

        private void SaveFileToImage(string filename)
        {
            Rectangle rect = new Rectangle(0, 0, panel4.Width, panel4.Height);
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {
                this.panel4.DrawToBitmap(bmp, rect);
                bmp.Save(filename);
            }
        }

        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确定退出系统？", "企业云图", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                System.Environment.Exit(0);
        }
   
        private void ToolStripMenuItem_AddModule_Click(object sender, EventArgs e)
        {
            ModuleEditForm newModuleEditForm = new ModuleEditForm(this);
            newModuleEditForm.ShowDialog();
            mainFormStatus();
        }

        private void ToolStripMenuItem_AddRelation_Click(object sender, EventArgs e)
        {
            RelationEditForm newRelationEditForm = new RelationEditForm(this);
            newRelationEditForm.ShowDialog();
            mainFormStatus();
        }

        private void ToolStripMenuItem_BorderColor_Click(object sender, EventArgs e)
        {
            BorderColor.ShowDialog();
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is Button)
                {
                    ((Button)con).FlatAppearance.BorderColor = BorderColor.Color;
                }
            }
        }
        private void ToolStripMenuItem_colorFilling_Click(object sender, EventArgs e)
        {
            ModuleColor.ShowDialog();
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is Button)
                {
                    ((Button)con).BackColor = ModuleColor.Color;
                }
            }
        }

        private void ToolStripMenuItem_LineColor_Click(object sender, EventArgs e)
        {
            LineColor.ShowDialog();
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    ((ALine)con).Pencolor = LineColor.Color;
                }
            }
        }

        private void ToolStripMenuItem_DisplayScale_Click(object sender, EventArgs e)
        {
            DisplayScaleForm displayScaleForm = new DisplayScaleForm(this);
            displayScaleForm.ShowDialog();

        }

        private void 注释ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is Button)
                {
                    ((Button)con).Font = fontDialog1.Font;
                }
            }
        }
        private void 磅ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    ((ALine)con).Penwidth = 1;
                    if (((ALine)con).Points[0] == ((ALine)con).Points[2])
                    {
                        ((ALine)con).Location = new Point(((ALine)con).Points[0] - 4 * ((ALine)con).Penwidth, ((ALine)con).Points[1]);
                    }
                    else
                        ((ALine)con).Location = new Point(((ALine)con).Points[0], ((ALine)con).Points[1] - 4 * ((ALine)con).Penwidth);
                }
            }
        }
        private void 磅ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    ((ALine)con).Penwidth = 2;
                    if (((ALine)con).Points[0] == ((ALine)con).Points[2])
                    {
                        ((ALine)con).Location = new Point(((ALine)con).Points[0] - 4 * ((ALine)con).Penwidth, ((ALine)con).Points[1]);
                    }
                    else
                        ((ALine)con).Location = new Point(((ALine)con).Points[0], ((ALine)con).Points[1] - 4 * ((ALine)con).Penwidth);
                }
            }
        }

        private void 磅ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    ((ALine)con).Penwidth = 4;
                    if (((ALine)con).Points[0] == ((ALine)con).Points[2])
                    {
                        ((ALine)con).Location = new Point(((ALine)con).Points[0] - 4 * ((ALine)con).Penwidth, ((ALine)con).Points[1]);
                    }
                    else
                        ((ALine)con).Location = new Point(((ALine)con).Points[0], ((ALine)con).Points[1] - 4 * ((ALine)con).Penwidth);
                }
            }
        }
        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("可视化系统之间的自动布线工具！", "关于云图", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }
         
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SOS NOT FOUND 404！", "HELP", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }

        

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemOperator.WriteHistory();
            SystemOperator.CloseDb();
            //if (DialogResult.Yes == MessageBox.Show("确定退出系统？", "企业云图", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //    //Application.Exit();
            //    System.Environment.Exit(0);
            //else
            //    e.Cancel = true;
        }


        private void ToolStripMenuItem_File_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog_OpenProject_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        
        // Icon click events
        private void toolStripButton_newProject_Click(object sender, EventArgs e)
        {
            NToolStripMenuItem_newProject_Click(sender, e);
        }

        private void toolStripButton_openProject_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_OpenProject_Click(sender, e);
        }

        private void toolStripButton_saveProject_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_SaveProject_Click(sender, e);
        }

        private void toolStripButton_prePrint_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_Print_Click(sender,e);
        }

        private void toolStripButton_import_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_import_Click(sender, e);
        }

        private void toolStripButton_addModule_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_AddModule_Click(sender, e);
        }

        private void toolStripButton_addRelation_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_AddRelation_Click(sender, e);
        }

        private void toolStripDropDownButton_colorFilling_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_colorFilling_Click(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_DisplayScale_Click(sender, e);
        }

        private void toolStripButton_information_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_About_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            帮助ToolStripMenuItem_Click(sender, e);
        }

        private void saveFileDialog_SaveProject_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void toolStripButton_saveImage_Click(object sender, EventArgs e)
        {
            保存云图ToolStripMenuItem_Click(sender,e);
        }

        private void toolStripDropDownButton_comment_Click(object sender, EventArgs e)
        {
            注释ToolStripMenuItem_Click(sender,e);
        }

        private void toolStripDropDownButton_borderLIne_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_BorderColor_Click(sender,e);
        }

        private void toolStripDropDownButton_lineColor_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_LineColor_Click(sender,e);
        }
        public List<Module> DrawModules()
        {
            //Graphics g1 = panel4.CreateGraphics();
            //Pen boderpen = new Pen(BorderColor.Color, 1);//模块边框画笔
            List<Module> modPosition = new List<Module>();
            if (comboBox_level.Text != null && comboBox_level.Text != "")
            {
                switch (comboBox_level.Text)
                {
                    case "一级":
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 1);
                        break;
                    case "二级":
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 2);
                        break;
                    case "三级":
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 3);
                        break;
                }
            }
            else
            {
                modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height);
            }
            int NumCount = modPosition.Count;
            Button[] btn = new Button[NumCount];

            for (int i = 2; i < NumCount; i++)
            {
                //g1.DrawRectangle(boderpen, modPosition[i].x - 1, modPosition[i].y - 1, modPosition[0].x + 1, modPosition[0].y + 1);
                btn[i] = new Button();
                btn[i].BackColor = ModuleColor.Color;
                //textBox[i].BackColor = Color.Purple;
                btn[i].Font = fontDialog1.Font;
                btn[i].Size = new System.Drawing.Size(modPosition[0].x, modPosition[0].y);
                btn[i].Location = new Point(modPosition[i].x, modPosition[i].y);
                btn[i].Text = modPosition[i].moduleName;//显示文字
                btn[i].FlatStyle = FlatStyle.Flat;
                btn[i].FlatAppearance.BorderColor = BorderColor.Color;
                panel4.Controls.Add(btn[i]);
                btn[i].Click += new EventHandler(this.TextBox_Click);
            }
            return modPosition;
        }
        public void TextBox_Click(object sender, EventArgs e)
        {
            List<Button> btnList = new List<Button>();
            List<ALine> alineList = new List<ALine>();
            Control.ControlCollection Cons = panel4.Controls;
            Button select = (Button)sender;
            foreach (Control con in Cons)
            {
                if (con is Button)
                {
                    btnList.Add((Button)con);
                    ((Button)con).FlatAppearance.BorderColor = BorderColor.Color;
                    ((Button)con).FlatAppearance.BorderSize = 1;
                    ((Button)con).BackColor = ModuleColor.Color;
                }
            }
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    alineList.Add((ALine)con);
                    ((ALine)con).Pencolor = LineColor.Color;
                }
            }
            foreach (ALine aline in alineList)
            {
                if (
                        ((aline.Location.X + aline.Size.Width == select.Location.X + 1) && (aline.Location.Y + aline.Size.Height > select.Location.Y) && (aline.Location.Y < select.Location.Y + select.Size.Height)) ||
                        ((aline.Location.Y + aline.Size.Height == select.Location.Y + 1) && (aline.Location.X + aline.Size.Width > select.Location.X) && (aline.Location.X < select.Location.X + select.Size.Width)) ||
                        ((aline.Location.X + 1 == select.Location.X + select.Size.Width) && (aline.Location.Y + aline.Size.Height > select.Location.Y) && (aline.Location.Y < select.Location.Y + select.Size.Height)) ||
                        ((aline.Location.Y + 1 == select.Location.Y + select.Size.Height) && (aline.Location.X + aline.Size.Width > select.Location.X) && (aline.Location.X < select.Location.X + select.Size.Width))
                        )
                {
                    aline.Pencolor = Color.Red;
                    foreach (ALine alineSame in alineList)
                    {
                        if (alineSame.Text.Equals(aline.Text))
                        {
                            alineSame.Pencolor = Color.Red;
                        }
                    }
                }
            }
            select.FlatAppearance.BorderColor = Color.LightBlue;
            select.FlatAppearance.BorderSize = 3;
            select.BackColor = Color.LightYellow;
        }
        public void DrawModuleAndLines()
        {
            List<Module> modPosition = DrawModules();
            ModuleOne.LineInfo[] line = new ModuleOne.LineInfo[1000];
            if (comboBox_level.Text != null && comboBox_level.Text != "")
            {
                switch (comboBox_level.Text)
                {
                    case "一级":
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 1);
                        break;
                    case "二级":
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 2);
                        break;
                    case "三级":
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 3);
                        break;
                }
            }
            else
            {
                line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 3);
                comboBox_level.SelectedIndex = 2;
            }
            int LineCount = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].line != null)
                {
                    LineCount++;
                }

            }
            for (int i = 0; i < LineCount; i++)
            {
                if (line[i].line[0] > line[i].line[2] || line[i].line[1] > line[i].line[3])
                {
                    int x = line[i].line[0];
                    int y = line[i].line[1];
                    line[i].line[0] = line[i].line[2];
                    line[i].line[1] = line[i].line[3];
                    line[i].line[2] = x;
                    line[i].line[3] = y;
                }
            }
            ALine[] aline = new ALine[LineCount];
            for (int i = 0; i < LineCount; i++)
            {
                //g1.DrawLine(linePen, line[i][0], line[i][1], line[i][2], line[i][3]);
                aline[i] = new ALine();
                aline[i].Points = line[i].line;
                aline[i].Penwidth = penWidth;
                aline[i].Pencolor = LineColor.Color;
                if (line[i].line[0] == line[i].line[2])
                {
                    aline[i].Location = new Point(line[i].line[0] - 4 * aline[i].Penwidth, line[i].line[1]);
                }
                else
                    aline[i].Location = new Point(line[i].line[0], line[i].line[1] - 4 * aline[i].Penwidth);
                aline[i].Text = line[i].lineName;
                aline[i].MouseHover += new EventHandler(this.AlineHover);
                //aline[i].Click += new EventHandler(this.AlineClick);
                aline[i].MouseDown += new MouseEventHandler(this.AlineDown);
                aline[i].MouseUp += new MouseEventHandler(this.AlineUp);
                //aline[i].MouseHover += (e, a) => AlineHover(line[i].lineName);
                panel4.Controls.Add(aline[i]);
            }
        }
        public void AlineDown(object sender, EventArgs e)
        {
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    if ((con.Text).Equals(((ALine)sender).Text))
                    {
                        ((ALine)con).Pencolor = Color.Red;
                        ((ALine)con).Penwidth = ((ALine)con).Penwidth + 1;
                        if (((ALine)con).Points[0] == ((ALine)con).Points[2])
                        {
                            ((ALine)con).Location = new Point(((ALine)con).Points[0] - 4*((ALine)con).Penwidth, ((ALine)con).Points[1]);
                        }
                        else
                            ((ALine)con).Location = new Point(((ALine)con).Points[0], ((ALine)con).Points[1] - 4 * ((ALine)con).Penwidth);
                    }
                }
            }
            ((ALine)sender).Pencolor = Color.Red;
            ((ALine)sender).Penwidth = ((ALine)sender).Penwidth + 1;
            if (((ALine)sender).Points[0] == ((ALine)sender).Points[2])
            {
                ((ALine)sender).Location = new Point(((ALine)sender).Points[0] - 4 * ((ALine)sender).Penwidth, ((ALine)sender).Points[1]);
            }
            else
                ((ALine)sender).Location = new Point(((ALine)sender).Points[0], ((ALine)sender).Points[1] - 4 * ((ALine)sender).Penwidth);
        }
        public void AlineUp(object sender, EventArgs e)
        {
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    if ((con.Text).Equals(((ALine)sender).Text))
                    {
                        ((ALine)con).Pencolor = LineColor.Color;
                        ((ALine)con).Penwidth = ((ALine)con).Penwidth - 1;
                        if (((ALine)con).Points[0] == ((ALine)con).Points[2])
                        {
                            ((ALine)con).Location = new Point(((ALine)con).Points[0] - 4*((ALine)con).Penwidth, ((ALine)con).Points[1]);
                        }
                        else
                            ((ALine)con).Location = new Point(((ALine)con).Points[0], ((ALine)con).Points[1] - 4 * ((ALine)con).Penwidth);
                    }
                }
            }
            ((ALine)sender).Pencolor = LineColor.Color;
            ((ALine)sender).Penwidth = ((ALine)sender).Penwidth - 1;
            if (((ALine)sender).Points[0] == ((ALine)sender).Points[2])
            {
                ((ALine)sender).Location = new Point(((ALine)sender).Points[0] - 4*((ALine)sender).Penwidth, ((ALine)sender).Points[1]);
            }
            else
                ((ALine)sender).Location = new Point(((ALine)sender).Points[0], ((ALine)sender).Points[1] -  4*((ALine)sender).Penwidth);
        }

        public void AlineHover(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip((ALine)sender, ((ALine)sender).Text);
        }
        public void btn_generateMap_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();//控件的清空
            this.panel4.Refresh();//Graphics的清空
            DrawModuleAndLines();//调用画控件函数
        }

        private void ToolStripMenuItem_Level1_Click(object sender, EventArgs e)
        {
            comboBox_level.SelectedIndex = 0;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }



        private void ToolStripMenuItem_BorderWidth_Click(object sender, EventArgs e)
        {

        }

        private void Form_Changed(object sender, EventArgs e)
        {
            if (globalParameters.dbPath == null)
            {
                return;
            }
            else
            {
                btn_generateMap_Click(sender, e);
            }
            //btn_generateMap_Click(sender, e);
        }

        private void toolStripDropDownButton_lineWidth_Click(object sender, EventArgs e)
        {

        }





        private void comboBox_level_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btn_generateMap_Click(sender, e);
        }

        private void ToolStripMenuItem_Refresh_Click(object sender, EventArgs e)
        {
            btn_generateMap_Click(sender, e);
        }

        private void ToolStripMenuItem_level2_Click(object sender, EventArgs e)
        {
            comboBox_level.SelectedIndex = 1;
        }

        private void ToolStripMenuItem_Level3_Click(object sender, EventArgs e)
        {
            comboBox_level.SelectedIndex = 2;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void trackBar_displaySacle_Scroll(object sender, EventArgs e)
        {
            int newPanelWidth = (int)(panelWidth * this.trackBar_displaySacle.Value / 100);
            int newPanelHeight = (int)(panelHeight * this.trackBar_displaySacle.Value / 100);
            panel4.Size = new Size(newPanelWidth, newPanelHeight);
            btn_generateMap_Click(sender, e);
        }

        private void 倍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            磅ToolStripMenuItem3_Click(sender, e);
        }

        private void 倍ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            磅ToolStripMenuItem5_Click(sender, e);
        }

        private void 倍ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            磅ToolStripMenuItem6_Click(sender, e);
        }
    }
}
