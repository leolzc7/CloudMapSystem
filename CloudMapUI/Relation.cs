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
    public partial class RelationEditForm : Form
    {
        private MainForm paf;
        public RelationEditForm(MainForm parent)
        {
            InitializeComponent();
            //paf = parent;
            //string[] text = NewProjectForm.dbName.Split('.');
            //textBox_ProjectName.Text = text[0];
            //clearAllWidget();
            //connect_open_db();
            //flushList(); //对源和目的list进行初始化
            //flushDataGrid();
        }

        public MainForm parent { get; set; }

        string relationSource;
        string relationTarget;
        string relationName;
        string relationBidirection = null;
        string relationType;
        string relationComment;
        public static string[,] list;
        public static string[] relationList;
        string selectSource;
        string selectTarget;
        
        private void RelationEditForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_EditRelationFinish_Click(object sender, EventArgs e)
        {
            flushList();
        }

        private void btnRelationCommit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {
            relationName = textBox_Name.Text;
        }

        private void comboBox_Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            //relationSource = comboBox_Source.Text;
        }

        private void comboBox_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            //relationTarget = comboBox_Target.Text;
        }

        private void comboBox_Bidirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //relationBidirection = comboBox_Bidirection.Text;
            //if (comboBox_Bidirection.Text.Equals("no"))
            //{
            //    relationBidirection = 0; //代表单向
            //}
            //else
            //{
            //    relationBidirection = 1;
            //}
        }

        private void textBox_comment_TextChanged(object sender, EventArgs e)
        {
            relationComment = textBox_comment.Text;
        }

        public void insert_relations()
        {
            if (relationName == null || relationType == null || relationSource == null || relationTarget == null || relationComment == null)
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

        public static void read_relation_source_target()
        {
           
        }

        public void read_record_and_show()
        {
           
        }

        public void flushList()
        {
           
        }

        public void flushDataGrid()
        {
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            insert_relations();
            clearAllWidget();
            flushDataGrid();
        }

        public void clearAllWidget()
        {
           
        }

        private void comboBox_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            relationType = comboBox_type.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //update_modules();
            //clearAllWidget();
            //flushDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //delete_modules();
            //clearAllWidget();
            //flushDataGrid();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;
            //selectTarget = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            //selectSource = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            //read_record_and_show();
        }

        private void connect_open_db()
        {
            //conn = new SQLiteConnection(NewProjectForm.dbPath);//创建数据库实例，指定文件位置
            //conn.Open();
        }

        private void close_db()
        {
          
        }

        private void textBox_Name_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            panel7.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            this.Hide();
        }
    }
}
