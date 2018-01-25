using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using Data;


namespace CloudMapUI
{
    public partial class RelationEditForm : Form
    {
        private MainForm paf;
        RecordStatus pageStatus;
        RelationData relationdata;
        ModuleData moduledata;
        string sourcemodule;
        string targetmodule;
        string selectSource;
        string selectTarget;
        public RelationEditForm(MainForm parent)
        {
            InitializeComponent();
            paf = parent;
        }

        public MainForm parent { get; set; }
        
        private void RelationEditForm_Load(object sender, EventArgs e)
        {          
            dataGridView_relation.AutoGenerateColumns = false;
            relationdata = RelationOperator.LoadRelationInfo();
            dataGridView_relation.DataSource = relationdata.Tables[RelationData.RELATION_TABLE].DefaultView;

            dgv_source.AutoGenerateColumns = false;
            moduledata = ModulesOperator.LoadModulesInfo();
            dgv_source.DataSource = moduledata.Tables[ModuleData.MODULES_TABLE].DefaultView;

            dgv_target.AutoGenerateColumns = false;
            moduledata = ModulesOperator.LoadModulesInfo();
            dgv_target.DataSource = moduledata.Tables[ModuleData.MODULES_TABLE].DefaultView;

            pageStatus = RecordStatus.View;
            SetFormControlerStatus();
            SetFormControlerData();

            //dataGridView_relation.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            //dataGridView_relation.RowsDefaultCellStyle.BackColor = Color.Bisque;
        }

        //设置控件状态
        private void SetFormControlerStatus()
        {
            textBox_ProjectName.Text = globalParameters.dbName;
            textBox_ProjectName.ReadOnly = true;
            //textBox_ProjectName.BackColor = Color.Gainsboro;
            if (pageStatus == RecordStatus.View)
            {
                name.ReadOnly = true;
                source.ReadOnly = true;
                target.ReadOnly = true;
                panel_addRelation.Visible = false;
                comboBox_Type.Visible = false;
                type.Visible = true;
                type.ReadOnly = true;
                comment.ReadOnly = true;

                btnAdd.Visible = true;
                btnSave.Visible = true;
                btnCancel.Visible = false;
                if (relationdata.Tables[RelationData.RELATION_TABLE].Rows.Count > 0)
                {
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                }

                //name.BackColor = Color.Gainsboro;
                //source.BackColor = Color.Gainsboro;
                //target.BackColor = Color.Gainsboro;
                //type.BackColor = Color.Gainsboro;
                //comment.BackColor = Color.Gainsboro;
            }
            else if (pageStatus == RecordStatus.Edit)
            {
                name.ReadOnly = false;
                source.ReadOnly = true;
                target.ReadOnly = true;
                panel_addRelation.Visible = true;
                comboBox_Type.Visible = true;
                type.Visible = false;
                comment.ReadOnly = false;

                btnUpdate.Visible = false;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;

                name.BackColor = Color.White;
                source.BackColor = Color.White;
                target.BackColor = Color.White;
                comboBox_Type.BackColor = Color.White;
                comment.BackColor = Color.White;
            }
            else if (pageStatus == RecordStatus.Add)
            {
                name.ReadOnly = false;
                source.ReadOnly = true;
                target.ReadOnly = true;
                panel_addRelation.Visible = true;
                comboBox_Type.Visible = true;
                type.Visible = false;
                comment.ReadOnly = false;

                btnUpdate.Visible = false;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;

                name.BackColor = Color.White;
                source.BackColor = Color.White;
                target.BackColor = Color.White;
                comboBox_Type.BackColor = Color.White;
                comment.BackColor = Color.White;
            }
        }
        //设置显示内容
        private void SetFormControlerData()
        {
            if (pageStatus == RecordStatus.View)
            {
                if (dataGridView_relation.SelectedRows.Count > 0)
                {
                    selectSource = dataGridView_relation.CurrentRow.Cells[1].Value.ToString();
                    selectTarget = dataGridView_relation.CurrentRow.Cells[2].Value.ToString();
                    DataRow dr = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD +"='"+ selectSource+"' and  "+RelationData.TARGETNAME_FIELD +"='"+ selectTarget+"'")[0];
                    name.Text = dr[RelationData.NAME_FIELD].ToString();
                    source.Text = dr[RelationData.SOURCENAME_FIELD].ToString();
                    target.Text = dr[RelationData.TARGETNAME_FIELD].ToString();
                    type.Text = dr[RelationData.TYPE_FIELD].ToString();
                    comment.Text = dr[RelationData.COMMENT_FIELD].ToString();
                    if (dr[RelationData.BIDIRECTION_FIELD].ToString() == "0")
                    {
                        radioButton_single.Checked = true;
                        radioButton_bidirection.Checked = false;
                    }
                    else
                    {
                        radioButton_single.Checked = false;
                        radioButton_bidirection.Checked = true;
                    }
                }
                else
                {
                    name.Text = "";
                    source.Text = "";
                    target.Text = "";
                    type.Text = "";
                    comment.Text = "";                   
                    radioButton_single.Checked = false;
                    radioButton_bidirection.Checked = false;
                }
            }
            else if (pageStatus == RecordStatus.Edit)
            {
                selectSource = dataGridView_relation.CurrentRow.Cells[1].Value.ToString();
                selectTarget = dataGridView_relation.CurrentRow.Cells[2].Value.ToString();
                DataRow dr = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + "='" + selectSource + "' and  " + RelationData.TARGETNAME_FIELD + "='" + selectTarget + "'")[0];
                name.Text = dr[RelationData.NAME_FIELD].ToString();
                source.Text = dr[RelationData.SOURCENAME_FIELD].ToString();
                target.Text = dr[RelationData.TARGETNAME_FIELD].ToString();
                comboBox_Type.Text = dr[RelationData.TYPE_FIELD].ToString();
                comment.Text = dr[RelationData.COMMENT_FIELD].ToString();
                if (dr[RelationData.BIDIRECTION_FIELD].ToString() == "0")
                {
                    radioButton_single.Checked = true;
                    radioButton_bidirection.Checked = false;
                }
                else
                {
                    radioButton_single.Checked = false;
                    radioButton_bidirection.Checked = true;
                }
            }
            else if (pageStatus == RecordStatus.Add)
            {
                name.Text = "";
                source.Text = "";
                target.Text = "";
                comboBox_Type.SelectedIndex=0;
                comment.Text = "";
                radioButton_single.Checked = true;
                radioButton_bidirection.Checked = false;
            }
        }

        //添加关系
        private void btnAdd_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.Add;
            SetFormControlerStatus();
            SetFormControlerData();
        }

        //修改关系
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.Edit;
            SetFormControlerStatus();
            SetFormControlerData();
        }

        //删除关系
        private void btnDelete_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.View;
            SetFormControlerStatus();
            SetFormControlerData();

            if (MessageBox.Show("您确定要删除所选系统吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                selectSource = dataGridView_relation.CurrentRow.Cells[1].Value.ToString();
                selectTarget = dataGridView_relation.CurrentRow.Cells[2].Value.ToString();
                if (RelationOperator.DeleteRelationInfo(selectSource,selectTarget))
                {
                    RelationEditForm_Load(sender, e);
                    MessageBox.Show("删除成功！");
                }
            }
            else
            {
                selectSource = dataGridView_relation.CurrentRow.Cells[1].Value.ToString();
                selectTarget = dataGridView_relation.CurrentRow.Cells[2].Value.ToString();
                DataRow dr = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + "='" + selectSource + "' and  " + RelationData.TARGETNAME_FIELD + "='" + selectTarget + "'")[0];
                name.Text = dr[RelationData.NAME_FIELD].ToString();
                source.Text = dr[RelationData.SOURCENAME_FIELD].ToString();
                target.Text = dr[RelationData.TARGETNAME_FIELD].ToString();
                type.Text = dr[RelationData.TYPE_FIELD].ToString();
                comboBox_Type.Text = dr[RelationData.TYPE_FIELD].ToString();
                comment.Text = dr[RelationData.COMMENT_FIELD].ToString();
                if (dr[RelationData.BIDIRECTION_FIELD].ToString() == "0")
                {
                    radioButton_single.Checked = true;
                    radioButton_bidirection.Checked = false;
                }
                else
                {
                    radioButton_single.Checked = false;
                    radioButton_bidirection.Checked = true;
                }
            }
        }

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pageStatus == RecordStatus.Edit)
            {
                selectSource = dataGridView_relation.CurrentRow.Cells[1].Value.ToString();
                selectTarget = dataGridView_relation.CurrentRow.Cells[2].Value.ToString();
                DataRow odr = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + "='" + selectSource + "' and  " + RelationData.TARGETNAME_FIELD + "='" + selectTarget + "'")[0];
                RelationData saveRelation = new RelationData();
                DataRow dr = saveRelation.Tables[RelationData.RELATION_TABLE].NewRow();
                if (IsFilled())
                {
                    dr[RelationData.NAME_FIELD] = name.Text.ToString().Trim();
                    dr[RelationData.SOURCENAME_FIELD] = source.Text.ToString().Trim();
                    dr[RelationData.TARGETNAME_FIELD] = target.Text.ToString().Trim();
                    dr[RelationData.TYPE_FIELD] = comboBox_Type.SelectedItem.ToString().Trim();
                    dr[ModuleData.COMMENT_FIELD] = comment.Text.ToString().Trim();
                    if (radioButton_bidirection.Checked)
                        dr[RelationData.BIDIRECTION_FIELD] = "1";
                    else
                        dr[RelationData.BIDIRECTION_FIELD] = "0";

                    saveRelation.Tables[RelationData.RELATION_TABLE].Rows.Add(dr);
                    if (RelationOperator.UpdateRelationInfo(saveRelation, selectSource, selectTarget))
                    {
                        RelationEditForm_Load(sender, e);
                        MessageBox.Show("修改成功！");
                    }
                    else
                    {
                        MessageBox.Show("该记录已存在！");
                    }
                }                
            }
            else if (pageStatus == RecordStatus.Add)
            {
                RelationData saveRelation = new RelationData();
                DataRow dr = saveRelation.Tables[RelationData.RELATION_TABLE].NewRow();
                if (IsFilled())
                {
                    dr[RelationData.NAME_FIELD] = name.Text.ToString().Trim();
                    dr[RelationData.SOURCENAME_FIELD] = source.Text.ToString().Trim();
                    dr[RelationData.TARGETNAME_FIELD] = target.Text.ToString().Trim();
                    dr[RelationData.TYPE_FIELD] = comboBox_Type.SelectedItem.ToString().Trim();
                    dr[ModuleData.COMMENT_FIELD] = comment.Text.ToString().Trim();
                    if (radioButton_bidirection.Checked)
                        dr[RelationData.BIDIRECTION_FIELD] = "1";
                    else
                        dr[RelationData.BIDIRECTION_FIELD] = "0";

                    saveRelation.Tables[RelationData.RELATION_TABLE].Rows.Add(dr);
                    if (RelationOperator.InsertRelationInfo(saveRelation))
                    {
                        RelationEditForm_Load(sender, e);
                        MessageBox.Show("添加成功！");
                    }
                    else
                    {
                        MessageBox.Show("该记录已存在！");
                    }
                }               
            }

        }

        //判空
        public bool IsFilled()
        {
            if(name.Text!=""&&source.Text!=""&&target.Text!=""&&comboBox_Type.SelectedItem.ToString()!=""&&comment.Text!=""&&(radioButton_bidirection.Checked||radioButton_single.Checked))
            {
                if (source.Text == target.Text)
                {
                    MessageBox.Show("源和目标不能相同！");
                    return false;
                }
                else
                    return true;
            }
            else
            {
                MessageBox.Show("所有字段不能为空！");
                return false;
            }
        }
 
        //查看关系
        private void dataGridView_relation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectSource = dataGridView_relation.CurrentRow.Cells[1].Value.ToString();
            selectTarget = dataGridView_relation.CurrentRow.Cells[2].Value.ToString();
            DataRow dr = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + "='" + selectSource + "' and  " + RelationData.TARGETNAME_FIELD + "='" + selectTarget + "'")[0];
            name.Text = dr[RelationData.NAME_FIELD].ToString();
            source.Text = dr[RelationData.SOURCENAME_FIELD].ToString();
            target.Text = dr[RelationData.TARGETNAME_FIELD].ToString();
            type.Text = dr[RelationData.TYPE_FIELD].ToString();
            comboBox_Type.Text = dr[RelationData.TYPE_FIELD].ToString();
            comment.Text = dr[RelationData.COMMENT_FIELD].ToString();
            if (dr[RelationData.BIDIRECTION_FIELD].ToString() == "0")
            {
                radioButton_single.Checked = true;
                radioButton_bidirection.Checked = false;
            }
            else
            {
                radioButton_single.Checked = false;
                radioButton_bidirection.Checked = true;
            }
        }

        //选择源
        private void dgv_source_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sourcemodule = dgv_source.CurrentCell.Value.ToString();
            source.Text = sourcemodule;
        }

        //选择目标
        private void dgv_target_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            targetmodule = dgv_target.CurrentCell.Value.ToString();
            target.Text = targetmodule;
        }

        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.View;
            SetFormControlerStatus();
            SetFormControlerData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
