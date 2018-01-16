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

namespace CloudMapUI
{
    public partial class MainForm : Form
    {
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
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ToolStripMenuItem_saveImage.Enabled = false;
            mainFormStatus();
            panelWidth = panel4.Size.Width;
            panelHeight = panel4.Size.Height;
            //textBox2.Text = panel4.Size.Width.ToString() + " * " + panel4.Size.Height.ToString();
            //panel1.Left = 0;
            //panel1.Top = 25;
        }
        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void NToolStripMenuItem_newProject_Click(object sender, EventArgs e)
        {
            NewProjectForm newProjrctFrom = new NewProjectForm(this);
            newProjrctFrom.ShowDialog();

            ModuleEditForm newModuleEditForm = new ModuleEditForm(this);
            newModuleEditForm.ShowDialog();

            RelationEditForm newRelationEditForm = new RelationEditForm(this);
            newRelationEditForm.ShowDialog();

                
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
        }

        private void ToolStripMenuItem_SaveProject_Click(object sender, EventArgs e)
        {
            //System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog_SaveProject.OpenFile();
            saveFileDialog_SaveProject.ShowDialog();
        }

        private void ToolStripMenuItem_SaveAs_Click(object sender, EventArgs e)
        {
            saveFileDialog_SaveProject.ShowDialog();
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
        }

        private void ToolStripMenuItem_DisplayScale_Click(object sender, EventArgs e)
        {
            DisplayScaleForm displayScaleForm = new DisplayScaleForm(this);
            displayScaleForm.ShowDialog();

        }

        private void 注释ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
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
            //if (DialogResult.Yes == MessageBox.Show("确定退出系统？", "企业云图", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //    //Application.Exit();
            //    //System.Environment.Exit(0);
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
            ToolStripMenuItem_BorderColor_Click(sender, e);
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
        private void btn_generateMap_Click(object sender, EventArgs e)
        {
            
            Graphics g1 = panel4.CreateGraphics();
            Pen boderpen = new Pen(BorderColor.Color, 1);
            List<Module> modPosition = new List<Module>();
            modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height);
            int NumCount=modPosition.Count;
            RichTextBox[] textBox = new RichTextBox[NumCount];
            
            for (int i = 2; i < NumCount; i++)
            {
                g1.DrawRectangle(boderpen, modPosition[i].x - 1, modPosition[i].y - 1, modPosition[0].x + 1, modPosition[0].y + 1);
                textBox[i] = new RichTextBox();
                textBox[i].BackColor = ModuleColor.Color;
                //textBox[i].BackColor = Color.Red;

                textBox[i].Size = new System.Drawing.Size(modPosition[0].x, modPosition[0].y);
                textBox[i].Location = new Point(modPosition[i].x, modPosition[i].y);
                textBox[i].Text = modPosition[i].moduleName;//显示文字
                textBox[i].SelectionAlignment = HorizontalAlignment.Center;//居中显示，目前只能水平居中不能垂直居中。
                textBox[i].ReadOnly = true;//只读
                textBox[i].BorderStyle = BorderStyle.Fixed3D;
                //textBox[i].Multiline = true;
                panel4.Controls.Add(textBox[i]);
            }
            int[][] line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height);
            //Pen linePen = new Pen(Color.Black, 1);
            int LineCount = 0; 
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != null)
                {
                    LineCount++;
                }

            }
            for (int i = 0; i < LineCount; i++)
            {
                if (line[i][0] > line[i][2] || line[i][1] > line[i][3])
                {
                    int x = line[i][0];
                    int y = line[i][1];
                    line[i][0] = line[i][2];
                    line[i][1] = line[i][3];
                    line[i][2] = x;
                    line[i][3] = y;
                }
            }
            ALine[] aline = new ALine[LineCount];
            for (int i = 0; i < LineCount; i++)
            {
                //g1.DrawLine(linePen, line[i][0], line[i][1], line[i][2], line[i][3]);
                aline[i] = new ALine();
                aline[i].Points = line[i];
                aline[i].Pencolor = Color.Black;
                if (line[i][0] == line[i][2])
                {
                    aline[i].Location = new Point(line[i][0] - 4 * aline[i].Penwidth, line[i][1]);
                }else
                    aline[i].Location = new Point(line[i][0], line[i][1] - 4 * aline[i].Penwidth);
                panel4.Controls.Add(aline[i]);
            }
        }
        public void DrawLine(int[] points)
        {
            Graphics g1 = panel4.CreateGraphics();
            Pen linePen = new Pen(BorderColor.Color, 1);
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
    }
}
