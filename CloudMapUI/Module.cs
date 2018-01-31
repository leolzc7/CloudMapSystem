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
        string selectModule;
        string moduleName;

        
        public ModuleEditForm(MainForm parent)
        {
            InitializeComponent();
            paf = parent;           
        }
        public MainForm parent { get; set; }

        private void ModuleEditForm_Load(object sender, EventArgs e)
        {
            dataGridView_module.AutoGenerateColumns = false;
            moduledata = ModulesOperator.LoadModulesInfo();
            dataGridView_module.DataSource = moduledata.Tables[ModuleData.MODULES_TABLE].DefaultView;
            pageStatus = RecordStatus.View;
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
                    //MessageBox.Show("删除成功！");                  
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

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pageStatus == RecordStatus.Edit)
            {
                selectModule = dataGridView_module.CurrentCell.Value.ToString();
                moduleName = name.Text.Trim();
                DataRow oRows = moduledata.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + "='" + selectModule + "'")[0];
                ModuleData saveModule = new ModuleData();
                DataRow dr = saveModule.Tables[ModuleData.MODULES_TABLE].NewRow();
                if (IsFilled())
                {
                    dr[ModuleData.NAME_FIELD] = name.Text.Trim();
                    dr[ModuleData.TYPE_FIELD] = comboBox_Type.SelectedItem.ToString().Trim();
                    dr[ModuleData.LEVEL_FIELD] = comboBox_Level.SelectedItem.ToString().Trim();
                    dr[ModuleData.COMMENT_FIELD] = comment.Text.ToString().Trim();

                    saveModule.Tables[ModuleData.MODULES_TABLE].Rows.Add(dr);
                    if (ModulesOperator.UpdateModulesInfo(saveModule, selectModule))
                    {
                        ModuleEditForm_Load(sender, e);
                        //MessageBox.Show("修改成功！");
                    }
                    else
                    {
                        MessageBox.Show("该记录已经存在！");
                    }
                }                
            }
            else if (pageStatus == RecordStatus.Add)
            {
                ModuleData saveModule = new ModuleData();
                DataRow dr = saveModule.Tables[ModuleData.MODULES_TABLE].NewRow();
                if (IsFilled())
                {
                    dr[ModuleData.NAME_FIELD] = name.Text.Trim();
                    dr[ModuleData.TYPE_FIELD] = comboBox_Type.SelectedItem.ToString().Trim();
                    dr[ModuleData.LEVEL_FIELD] = comboBox_Level.SelectedItem.ToString().Trim();
                    dr[ModuleData.COMMENT_FIELD] = comment.Text.ToString().Trim();

                    saveModule.Tables[ModuleData.MODULES_TABLE].Rows.Add(dr);
                    if (ModulesOperator.InsertModulesInfo(saveModule))
                    {
                        ModuleEditForm_Load(sender, e);
                        //MessageBox.Show("添加成功！");
                    }
                    else
                    {
                        MessageBox.Show("该记录已经存在！");
                    }
                }                
            }
        }

        //判空
        private bool IsFilled()
        {
            if (name.Text != "" && comboBox_Type.SelectedItem.ToString() != "" && comboBox_Level.SelectedItem.ToString() != "" && comment.Text != "")
                return true;
            else
            {
                MessageBox.Show("所有字段不能为空！");
                return false;
            }
        }

        //设置页面控件状态
        private void SetFormControlerStatus()
        {
            textBox_ProjectName.Text = globalParameters.dbName;
            textBox_ProjectName.ReadOnly = true;
            //textBox_ProjectName.BackColor = Color.FromArgb(235, 243, 255);
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
                btnCancel.Visible = false;
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

                name.BackColor = Color.FromArgb(235, 243, 248);
                type.BackColor = Color.FromArgb(235, 243, 248);
                level.BackColor = Color.FromArgb(235, 243, 248);
                comment.BackColor = Color.FromArgb(235, 243, 248);
            }
            else if (pageStatus == RecordStatus.Edit)
            {
                name.ReadOnly = false;
                comboBox_Type.Visible = true;
                type.Visible = false;
                comboBox_Level.Visible = true;
                level.Visible= false;
                comment.ReadOnly = false;

                btnUpdate.Visible = false;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;

                name.BackColor = Color.White;
                comboBox_Type.BackColor = Color.White;
                comboBox_Level.BackColor = Color.White;
                comment.BackColor = Color.White;
                
                //comboBox_Typ
            }
            else if (pageStatus == RecordStatus.Add)
            {               
                name.ReadOnly = false;
                comboBox_Type.Visible = true;
                type.Visible = false;
                comboBox_Level.Visible = true;
                level.Visible = false;
                comment.ReadOnly = false;

                btnUpdate.Visible = false;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;

                name.BackColor = Color.White;
                comboBox_Type.BackColor = Color.White;
                comboBox_Level.BackColor = Color.White;
                comment.BackColor = Color.White;
                comboBox_Type.BackColor = Color.White;
                comboBox_Level.BackColor = Color.White;
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
                selectModule = dataGridView_module.CurrentCell.Value.ToString();
                DataRow odr = moduledata.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + "='" + selectModule + "'")[0];
                name.Text = odr[ModuleData.NAME_FIELD].ToString();
                comboBox_Type.Text = odr[ModuleData.TYPE_FIELD].ToString();
                comboBox_Level.Text = odr[ModuleData.LEVEL_FIELD].ToString();
                comment.Text = odr[ModuleData.COMMENT_FIELD].ToString(); ;
            }
            else if (pageStatus == RecordStatus.Add)
            {
                name.Text = "";
                comboBox_Type.SelectedIndex=0;
                comboBox_Level.SelectedIndex = 0 ;
                comment.Text = "";
            }
        }
        
        //选中列表中的模块
        private void dataGridView_module_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectModule = dataGridView_module.CurrentCell.Value.ToString();
            DataRow odr = moduledata.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + "='" + selectModule + "'")[0];
            name.Text = odr[ModuleData.NAME_FIELD].ToString();
            type.Text = odr[ModuleData.TYPE_FIELD].ToString();
            level.Text = odr[ModuleData.LEVEL_FIELD].ToString();
            comboBox_Type.Text = odr[ModuleData.TYPE_FIELD].ToString();
            comboBox_Level.Text = odr[ModuleData.LEVEL_FIELD].ToString();
            comment.Text = odr[ModuleData.COMMENT_FIELD].ToString();
        }

        //取消按钮
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.View;
            SetFormControlerStatus();
            SetFormControlerData();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
      
    }
}


