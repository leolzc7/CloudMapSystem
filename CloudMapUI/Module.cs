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
    public partial class ModuleEditForm : Form
    {
        private MainForm paf;
        RecordStatus pageStatus;
        ModuleData moduledata;

        string moduleName;
        string moduleType;
        string moduleLevel;
        string moduleComment;
        string selectModule;

        
        public ModuleEditForm(MainForm parent)
        {
            InitializeComponent();
            paf = parent;           
        }
        public MainForm parent { get; set; }

        private void ModuleEditForm_Load(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.View;
            textBox_ProjectName.Text = globalParameters.dbName;
            textBox_ProjectName.ReadOnly = true;
            textBox_ProjectName.BackColor = Color.Gainsboro;
            dataGridView_module.AutoGenerateColumns = false;
            moduledata = ModulesOperator.LoadModulesInfo();
            dataGridView_module.DataSource = moduledata.Tables[ModuleData.MODULES_TABLE].DefaultView;
            SetFormControlerStatus();
            SetFormControlerData();
        }

        //新建模块
        private void btnAdd_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.Add;
            SetFormControlerStatus();
            SetFormControlerData();
        }

        //编辑模块
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.Edit;
            SetFormControlerStatus();
            SetFormControlerData();
        }

        //删除模块
        private void btnDelete_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.View;
            SetFormControlerStatus();
            SetFormControlerData();

            if (MessageBox.Show("您确定要删除所选系统吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                selectModule = dataGridView_module.CurrentCell.Value.ToString();
                if (ModulesOperator.DeleteModulesInfo(selectModule))
                {
                    DataRow rows = moduledata.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + "='" + selectModule + "'")[0];
                    ModuleEditForm_Load(sender,e);
                    MessageBox.Show("删除成功！");                  
                }
            }
            else
            {
                DataRow odr = moduledata.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + "='" + selectModule + "'")[0];
                name.Text = odr[ModuleData.NAME_FIELD].ToString();
                type.Text = odr[ModuleData.TYPE_FIELD].ToString();
                level.Text = odr[ModuleData.LEVEL_FIELD].ToString();
                comment.Text = odr[ModuleData.COMMENT_FIELD].ToString();
            }
        }


        private void comboBox_Level_SelectedIndexChanged(object sender, EventArgs e)
        {
            moduleLevel = comboBox_Level.Text;
        }

        private void text_comment_TextChanged(object sender, EventArgs e)
        {
            moduleComment = comment.Text;
        }

        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            moduleName = name.Text;
        }

        public static bool isPress = false;
        private void btn_EditModuleFinish_Click(object sender, EventArgs e)
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
            comment.Text = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pageStatus == RecordStatus.Edit)
            {
                selectModule = dataGridView_module.CurrentCell.Value.ToString();
                moduleName = name.Text.Trim();
                DataRow oRows = moduledata.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + "='" + selectModule + "'")[0];
                ModuleData saveModule = new ModuleData();
                DataRow dr = saveModule.Tables[ModuleData.MODULES_TABLE].NewRow();

                dr[ModuleData.NAME_FIELD] = name.Text.Trim();
                dr[ModuleData.TYPE_FIELD] = comboBox_Type.SelectedItem.ToString().Trim();
                dr[ModuleData.LEVEL_FIELD] = comboBox_Level.SelectedItem.ToString().Trim();
                dr[ModuleData.COMMENT_FIELD] = comment.Text.ToString().Trim();

                saveModule.Tables[ModuleData.MODULES_TABLE].Rows.Add(dr);
                if (ModulesOperator.UpdateModulesInfo(saveModule, selectModule))
                {
                    ModuleEditForm_Load(sender, e);
                    MessageBox.Show("修改成功！");
                }
            }
            else if (pageStatus == RecordStatus.Add)
            {
                ModuleData saveModule = new ModuleData();
                DataRow dr = saveModule.Tables[ModuleData.MODULES_TABLE].NewRow();

                dr[ModuleData.NAME_FIELD] = name.Text.Trim();
                dr[ModuleData.TYPE_FIELD] = comboBox_Type.SelectedItem.ToString().Trim();
                dr[ModuleData.LEVEL_FIELD] = comboBox_Level.SelectedItem.ToString().Trim();
                dr[ModuleData.COMMENT_FIELD] = comment.Text.ToString().Trim();

                saveModule.Tables[ModuleData.MODULES_TABLE].Rows.Add(dr);
                if (ModulesOperator.InsertModulesInfo(saveModule))
                {
                    ModuleEditForm_Load(sender, e);
                    MessageBox.Show("添加成功！");
                }
            }
           
        }

        //设置页面控件状态
        private void SetFormControlerStatus()
        {
            if (pageStatus == RecordStatus.View)
            {
                name.ReadOnly = true;
                comboBox_Type.Visible = false;
                type.Visible = true;
                type.ReadOnly = true;
                comboBox_Level.Visible = false;
                level.Visible = true;
                level.ReadOnly = true;
                comment.ReadOnly = true;

                btnAdd.Visible = true;
                btnSave.Visible = true;
                if (moduledata.Tables[ModuleData.MODULES_TABLE].Rows.Count > 0)
                {
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                }

                name.BackColor = Color.Gainsboro;
                type.BackColor = Color.Gainsboro;
                level.BackColor = Color.Gainsboro;
                comment.BackColor = Color.Gainsboro;
            }
            else if (pageStatus == RecordStatus.Edit)
            {
                btnUpdate.Visible = false;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnSave.Visible = true;

                name.ReadOnly = false;
                comboBox_Type.Visible = true;
                type.Visible = false;
                comboBox_Level.Visible = true;
                level.Visible= false;
                comment.ReadOnly = false;
                comment.Text = "";

                name.BackColor = Color.Gainsboro;
                comboBox_Type.BackColor = Color.White;
                comboBox_Level.BackColor = Color.White;
                comment.BackColor = Color.White;
            }
            else if (pageStatus == RecordStatus.Add)
            {
                btnUpdate.Visible = false;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnSave.Visible = true;

                name.ReadOnly = false;
                comboBox_Type.Visible = true;
                type.Visible = false;
                comboBox_Level.Visible = true;
                level.Visible = false;
                comment.ReadOnly = false;

                name.BackColor = Color.Gainsboro;
                comboBox_Type.BackColor = Color.Pink;
                comboBox_Level.BackColor = Color.PaleTurquoise;
                comment.BackColor = Color.PaleTurquoise;
            }
        }

        //设置显示内容
        private void SetFormControlerData()
        {
            if (pageStatus == RecordStatus.View)
            {
                if (dataGridView_module.SelectedRows.Count > 0)
                {
                    selectModule = dataGridView_module.CurrentCell.Value.ToString();
                    DataRow odr = moduledata.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + "='" + selectModule + "'")[0];
                    name.Text = odr[ModuleData.NAME_FIELD].ToString();
                    type.Text = odr[ModuleData.TYPE_FIELD].ToString();
                    level.Text = odr[ModuleData.LEVEL_FIELD].ToString();
                    comment.Text = odr[ModuleData.COMMENT_FIELD].ToString();
                }
                else
                {
                    name.Text = "";
                    type.Text = "";
                    level.Text = "";
                    comment.Text = "";
                }
            }
            else if (pageStatus == RecordStatus.Edit)
            {
            }
            else if (pageStatus == RecordStatus.Add)
            {
                name.Text = "";
                type.Text = "";
                level.Text = "";
                comment.Text = "";
            }
        }



        private void dataGridView_module_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectModule = dataGridView_module.CurrentCell.Value.ToString();
            DataRow odr = moduledata.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + "='" + selectModule + "'")[0];
            name.Text = odr[ModuleData.NAME_FIELD].ToString();
            type.Text = odr[ModuleData.TYPE_FIELD].ToString();
            level.Text = odr[ModuleData.LEVEL_FIELD].ToString();
            comment.Text = odr[ModuleData.COMMENT_FIELD].ToString();
        }
        
    }
}


