using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CloudMapUI
{
    public partial class ModuleEditForm : Form
    {
        private MainForm paf;
        public ModuleEditForm(MainForm parent)
        {
            InitializeComponent();
            paf = parent;
            //string[] text = NewProjectForm.dbName.Split('.');
            //textBox_ProjectName.Text = text[0];
            //connect_open_db();
            //flushDataGrid();
        }

        public MainForm parent { get; set; }

        string moduleName;
        string moduleType;
        string moduleLevel;
        string moduleComment;
        string selectModule;
        //public static Item[] modulesName;
        public static string[] modulesList;
       


        private void comboBox_Level_SelectedIndexChanged(object sender, EventArgs e)
        {
            moduleLevel = comboBox_Level.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            moduleComment = text_comment.Text;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete_modules();
            clearAllWidget();
            flushDataGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //update_modules();
            //clearAllWidget();
            //flushDataGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //insert_modules(sender);
            //clearAllWidget();
            //flushDataGrid();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            moduleName = name.Text;
        }

        public static bool isPress = false;
        private void btn_EditModuleFinish_Click(object sender, EventArgs e)
        {
            
        }

        private void ModuleEditForm_Load(object sender, EventArgs e)
        {

        }

        private void btnModuleCommit_Click(object sender, EventArgs e)
        {
            //read_modules(); //读取moduleList传到relation中
            //close_db();
            //this.Hide();
        }

        private void comboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            moduleType = comboBox_Type.Text;
        }

        public void insert_modules(object sender)
        {
            if(moduleName == null || moduleType == null || moduleLevel == null || moduleComment == null)
            {
                MessageBox.Show(" 所有的属性都不能为空！！ ", "使用帮助", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
               
            }
        }

        public void delete_modules()
        {
            
        }

        public void update_modules()
        {
           
        }

        public static void read_modules()
        {
           
        }

        public void read_record()
        {
           
        }

        public class Item
        {
            private string text;
            public Item(string text)
            {
                this.text = text;
            }
            public string Text
            {
                get
                {
                    return text;
                }
            }
        }

        public void flushDataGrid()
        {
          
        }

        private void clearAllWidget()
        {
            comboBox_Level.Text = null;
            name.Text = null;
            comboBox_Type.Text = null;
            text_comment.Text = null;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selectModule = dataGridView_module.CurrentCell.Value.ToString();
            //dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;
            read_record();
        }

        private void connect_open_db()
        {
            
        }

        private void close_db()
        {
           
        }

        private void textBox_ProjectName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }
    }
}
