using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        //public static void fresh()
        //{
        //    mainform = new MainForm();
        //    mainform.dataGridView_module.AutoGenerateColumns = false;
        //    moduledata = ModulesOperator.LoadModulesInfo();
        //    mainform.dataGridView_module.DataSource = moduledata.Tables[ModuleData.MODULES_TABLE].DefaultView;
        //    mainform.dataGridView_relation.AutoGenerateColumns = false;
        //    relationdata = RelationOperator.LoadRelationInfo();
        //    mainform.dataGridView_relation.DataSource = relationdata.Tables[RelationData.RELATION_TABLE].DefaultView;
        //}
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
                //ToolStripMenuItem_SysLevel.Enabled = false;       
                //ToolStripMenuItem_DisplayScale.Enabled = false;
                //ToolStripMenuItem_Border.Enabled = false;
                //ToolStripMenuItem_Line.Enabled = false;
                //ToolStripMenuItem_comment.Enabled = false;


                toolStripButton_saveProject.Enabled = false;
                toolStripButton_saveImage.Enabled = false;
                toolStripButton_prePrint.Enabled = false;
                toolStripButton_import.Enabled = false;
                toolStripButton_addModule.Enabled = false;
                toolStripButton_addRelation.Enabled = false;
                //toolStripDropDownButton_colorFilling.Enabled = false;
                //toolStripDropDownButton_borderLIne.Enabled = false;
                //toolStripDropDownButton_lineWidth.Enabled = false;
                //toolStripDropDownButton_lineColor.Enabled = false;
                //toolStripDropDownButton_comment.Enabled = false;

                dataGridView_module.Visible = false;
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
            
            panelWidth = panel4.Size.Width;
            panelHeight = panel4.Size.Height;
            AddHistoryItem();
            //textBox2.Text = panel4.Size.Width.ToString() + " * " + panel4.Size.Height.ToString();
            //panel1.Left = 0;
            //panel1.Top = 25;
        }
        private void AddHistoryItem()
        {
            SystemOperator.ReadHistory();
            foreach (string history in globalParameters.dbHistory)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = history;
                item.Text = history;
                item.Click += new EventHandler(historyItemClik);
                ToolStripMenuItem_history.DropDownItems.Add(item);
            }
        }
        private void historyItemClik(object sender, EventArgs e)
        {
            string path = ((ToolStripMenuItem)sender).Text;
            SystemOperator.OpenProject(path);
            mainFormStatus();
        }
        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        


        private void NToolStripMenuItem_newProject_Click(object sender, EventArgs e)
        {
            NewProjectForm newProjrctFrom = new NewProjectForm(this);
            newProjrctFrom.ShowDialog();

            //ModuleEditForm newModuleEditForm = new ModuleEditForm(this);
            //newModuleEditForm.ShowDialog();

            //RelationEditForm newRelationEditForm = new RelationEditForm(this);
            //newRelationEditForm.ShowDialog();       
        }

        private void ToolStripMenuItem_import_Click(object sender, EventArgs e)
        {
            importForm import = new importForm(this);

            import.ShowDialog();
        }

        private void ToolStripMenuItem_OpenProject_Click(object sender, EventArgs e)
        {   
            openFileDialog_OpenProject.ShowDialog();
            SystemOperator.OpenProject(openFileDialog_OpenProject.FileName);
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
            File.Copy(oldFilePath,filePath);
        }

        private void 保存云图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog_saveImage.ShowDialog();
        }

        private void ToolStripMenuItem_Print_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();   
        }
        private void ToolStripMenuItem_PrePrint_Click(object sender, EventArgs e)
        {
             printPreviewDialog1.ShowDialog();  
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
        }

        private void ToolStripMenuItem_AddRelation_Click(object sender, EventArgs e)
        {
            RelationEditForm newRelationEditForm = new RelationEditForm(this);

            newRelationEditForm.ShowDialog();
        }

        private void ToolStripMenuItem_BorderColor_Click(object sender, EventArgs e)
        {
            BorderColor.ShowDialog();
            btn_generateMap_Click(sender, e);
        }

        private void ToolStripMenuItem_LineColor_Click(object sender, EventArgs e)
        {
            LineColor.ShowDialog();
            btn_generateMap_Click(sender,e);
        }

        private void ToolStripMenuItem_DisplayScale_Click(object sender, EventArgs e)
        {
            DisplayScaleForm displayScaleForm = new DisplayScaleForm(this);
            displayScaleForm.ShowDialog();

        }

        private void 注释ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            btn_generateMap_Click(sender, e);
        }

        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("可视化系统之间的关系", "关于云图", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }
         
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" 使用方式同记事本 ", "使用帮助", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }

        

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemOperator.WriteHistory();
            //if (DialogResult.Yes == MessageBox.Show("确定退出系统？", "企业云图", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //    //Application.Exit();
            //    System.Environment.Exit(0);
            //else
            //    e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {


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
            ToolStripMenuItem_PrePrint_Click(sender, e);
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
            Graphics g1 = panel4.CreateGraphics();
            Pen boderpen = new Pen(BorderColor.Color, 1);//模块边框画笔
            List<Module> modPosition = new List<Module>();
            modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height);
            int NumCount = modPosition.Count;
            RichTextBox[] textBox = new RichTextBox[NumCount];

            for (int i = 2; i < NumCount; i++)
            {
                g1.DrawRectangle(boderpen, modPosition[i].x - 1, modPosition[i].y - 1, modPosition[0].x + 1, modPosition[0].y + 1);
                textBox[i] = new RichTextBox();
                textBox[i].BackColor = ModuleColor.Color;
                //textBox[i].BackColor = Color.Purple;
                textBox[i].Font = fontDialog1.Font;
                textBox[i].Size = new System.Drawing.Size(modPosition[0].x, modPosition[0].y);
                textBox[i].Location = new Point(modPosition[i].x, modPosition[i].y);
                textBox[i].Text = modPosition[i].moduleName;//显示文字
                textBox[i].SelectionAlignment = HorizontalAlignment.Center;//居中显示，目前只能水平居中不能垂直居中。
                textBox[i].ReadOnly = true;//只读
                textBox[i].BorderStyle = BorderStyle.None;
                //textBox[i].Multiline = true;
                panel4.Controls.Add(textBox[i]);
            }
            return modPosition;
        }

        public void DrawModuleAndLines()
        {
            List<Module> modPosition = DrawModules();
            ModuleOne.LineInfo[] line =  ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height);
            //Pen linePen = new Pen(Color.Black, 1);
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
        private void btn_generateMap_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();//控件的清空
            this.panel4.Refresh();//Graphics的清空
            DrawModuleAndLines();//调用画控件函数
        }

        private void ToolStripMenuItem_Level1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ToolStripMenuItem_colorFilling_Click(object sender, EventArgs e)
        {
            ModuleColor.ShowDialog();
            btn_generateMap_Click(sender, e);
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

        private void 磅ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            penWidth = 1;
            btn_generateMap_Click(sender, e);
        }

        private void 磅ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            penWidth = 2;
            btn_generateMap_Click(sender, e);
        }

        private void 磅ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            penWidth = 4;
            btn_generateMap_Click(sender, e);
        }
    }
}
