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

namespace CloudMapUI
{
    public partial class MainForm : Form
    {
        ModuleData moduledata;
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
                
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mainFormStatus();
            
            panelWidth = panel4.Size.Width;
            panelHeight = panel4.Size.Height;
            textBox2.Text = panel4.Size.Width.ToString() + " * " + panel4.Size.Height.ToString();
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
            if (DialogResult.Yes == MessageBox.Show("确定退出系统？", "企业云图", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                //Application.Exit();
                System.Environment.Exit(0);
            else
                e.Cancel = true;
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

        }

        private void ToolStripMenuItem_Level1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
