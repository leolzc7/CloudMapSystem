﻿using Data;
using DataAccess;
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
    public partial class importForm : Form
    {
        private MainForm paf;
        List<string> selectModule;
        RelationData relation;
        RelationData selectRelation;
        public importForm(MainForm parent)
        {
            InitializeComponent();
            paf = parent;
        }
        public MainForm parent { get; set; }

        //导入
        private void btn_SelectFinish_Click(object sender, EventArgs e)
        {
            selectRelation = new RelationData();
            string source;
            string target;
            for (int i = 0; i <= this.dgv_importRelation.RowCount - 1; i++)
                if (dgv_importRelation.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    source = dgv_importRelation.Rows[i].Cells[2].Value.ToString();
                    target = dgv_importRelation.Rows[i].Cells[3].Value.ToString();
                    DataRow odr = relation.Tables[RelationData.RELATION_TABLE].Select(RelationData.SOURCENAME_FIELD + "='" + source + "' and  " + RelationData.TARGETNAME_FIELD + "='" + target + "'")[0];
                    DataRow dr = selectRelation.Tables[RelationData.RELATION_TABLE].NewRow();
                    dr[2] = odr[RelationData.NAME_FIELD];
                    dr[0] = odr[RelationData.SOURCENAME_FIELD];
                    dr[1] = odr[RelationData.TARGETNAME_FIELD];
                    dr[3] = odr[RelationData.BIDIRECTION_FIELD].ToString();
                    dr[4] = odr[RelationData.TYPE_FIELD].ToString();
                    dr[5] = odr[RelationData.COMMENT_FIELD].ToString();
                    dr[6] = odr[RelationData.SHOW_FIELD];
                    selectRelation.Tables[RelationData.RELATION_TABLE].Rows.Add(dr);
                }
            if (IsModuleChecked())
            {
                GetModulesList();
                if (ModulesOperator.importModules(selectModule))
                    MessageBox.Show(" 信息系统导入成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (IsRelationChecked())
            {
                if(RelationOperator.ImportRelation(selectRelation))
                    MessageBox.Show(" 关系导入成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainForm.isSaved = false;
            }
        }

        //取消导入
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
  
        private void btnFolderBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog_import.ShowDialog();
            if (openFileDialog_import.FileName == null || openFileDialog_import.FileName == "")
                return;
            SystemOperator.OpenProject(openFileDialog_import.FileName, false);
            textBox1.Text = globalParameters.secondDbName;
            dgv_importModule.AutoGenerateColumns = false;
            ModuleData moduledata = ModulesOperator.LoadModulesInfoForSecondDb();
            dgv_importModule.DataSource = moduledata.Tables[ModuleData.MODULES_TABLE].DefaultView;
        }

        private void importForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        //判断模块是否有选中的行
        private bool IsModuleChecked()
        {
            for (int i = 0; i <= this.dgv_importModule.RowCount - 1; i++)
            {
                if (dgv_importModule.Rows[i].Cells[0].Value != null)
                {
                    if ((bool)dgv_importModule.Rows[i].Cells[0].Value == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //判断关系是否有选中的行
        private bool IsRelationChecked()
        {
            for (int i = 0; i <= this.dgv_importRelation.RowCount - 1; i++)
            {
                if (dgv_importRelation.Rows[i].Cells[0].Value != null)
                {
                    if ((bool)dgv_importRelation.Rows[i].Cells[0].Value == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //选中模块
        private void dgv_importModule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_importModule.CurrentRow == null)
                return;
            //如果选中点击则取消，反而逆之
            if (dgv_importModule.Rows[e.RowIndex].Cells[0].Value != null)
            {
                if ((bool)dgv_importModule.Rows[e.RowIndex].Cells[0].Value == true)
                    dgv_importModule.Rows[e.RowIndex].Cells[0].Value = false;
                else
                    dgv_importModule.Rows[e.RowIndex].Cells[0].Value = true;
            }
            else
            {
                dgv_importModule.Rows[e.RowIndex].Cells[0].Value = true;
            }
      
            //全选
            if (e.RowIndex >= 0)
            {
             int state2 = 0;
                for (int i = 0; i <= this.dgv_importModule.RowCount - 1; i++)
                {
                    if (dgv_importModule.Rows[i].Cells[0].Value != null)
                    {
                        if ((bool)dgv_importModule.Rows[i].Cells[0].Value == true)
                            state2++;
                        else
                            state2--;
                    }
                    else
                        state2--;
                }
                if (state2 == dgv_importModule.Rows.Count)
                    selectedAllModules.CheckState = CheckState.Checked;
                else if (state2 == -dgv_importModule.Rows.Count)
                    selectedAllModules.CheckState = CheckState.Unchecked;
                else
                    selectedAllModules.CheckState = CheckState.Indeterminate;           
            }
        }

        //选中关系
        private void dgv_importRelation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_importRelation.CurrentRow == null)
                return;
            //如果选中点击则取消，反而逆之
            if(dgv_importRelation.Rows[e.RowIndex].Cells[0].Value!=null)
            {
                if ((bool)dgv_importRelation.Rows[e.RowIndex].Cells[0].Value == true)
                    dgv_importRelation.Rows[e.RowIndex].Cells[0].Value = false;
                else
                    dgv_importRelation.Rows[e.RowIndex].Cells[0].Value = true;
            }
            else
            {
                dgv_importRelation.Rows[e.RowIndex].Cells[0].Value = true;
            }
           
            //全选
            if (e.RowIndex>=0)
            {
                int state1 = 0;
                for (int i = 0; i <= this.dgv_importRelation.RowCount - 1; i++)
                {
                    if (dgv_importRelation.Rows[i].Cells[0].Value != null)
                    {
                        if ((bool)dgv_importRelation.Rows[i].Cells[0].Value == true)
                            state1++;
                        else
                            state1--;
                    }
                    else
                        state1--;
                }
                if (state1 == dgv_importRelation.Rows.Count)
                    selectedAllRelation.CheckState = CheckState.Checked;
                else if (state1 == -dgv_importModule.Rows.Count)
                    selectedAllRelation.CheckState = CheckState.Unchecked;
                else
                    selectedAllRelation.CheckState = CheckState.Indeterminate;
            }
        }

        //模块全选复选框
        private void selectedAllModules_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedAllModules.CheckState == CheckState.Checked)
            {
                for (int i = 0; i <= this.dgv_importModule.RowCount - 1; i++)
                {
                    this.dgv_importModule.Rows[i].Cells[0].Value = true;
                }
            }
            else if (selectedAllModules.CheckState == CheckState.Unchecked)
            {
                for (int i = 0; i <= this.dgv_importModule.RowCount - 1; i++)
                {
                    this.dgv_importModule.Rows[i].Cells[0].Value = false;
                }
            }
        }

        //关系全选复选框
        private void selectedAllRelation_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedAllRelation.CheckState == CheckState.Checked)
            {
                for (int i = 0; i <= this.dgv_importRelation.RowCount - 1; i++)
                {
                    this.dgv_importRelation.Rows[i].Cells[0].Value = true;
                }
            }
            else if (selectedAllRelation.CheckState == CheckState.Unchecked)
            {
                for (int i = 0; i <= this.dgv_importRelation.RowCount - 1; i++)
                {
                    this.dgv_importRelation.Rows[i].Cells[0].Value = false;
                }
            }
        }

        //显示选中模块的关系
        private void btnSelected_Click(object sender, EventArgs e)
        {            
            if (IsModuleChecked())
            {
                GetModulesList();
                relation = new RelationData();
                relation=RelationOperator.GetRelationInfoForImport(selectModule);
                dgv_importRelation.AutoGenerateColumns = false;
                dgv_importRelation.DataSource = relation.Tables[RelationData.RELATION_TABLE].DefaultView;
            }
        }
        private void GetModulesList()
        {
            selectModule = new List<string>();
            for (int i = 0; i <= this.dgv_importModule.RowCount - 1; i++)
                if (dgv_importModule.Rows[i].Cells[0].Value != null)
                {
                    if ((bool)dgv_importModule.Rows[i].Cells[0].Value == true)
                        selectModule.Add(dgv_importModule.Rows[i].Cells[1].Value.ToString());
                }
        }
        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void selectedAllModules_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                selectedAllModules.Checked = false;
                selectedAllRelation.Checked = false;
                MessageBox.Show(" 请先选择要导入的数据库！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
