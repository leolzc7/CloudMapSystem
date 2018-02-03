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
    public partial class StreamForm : Form
    {
        private MainForm paf;
        RecordStatus pageStatus;
        public StreamData streamdata;
        public StreamData selectData=new StreamData();
        ModuleData moduledata;
        string selectStream;

        public StreamForm(MainForm parent)
        {
            InitializeComponent();
            paf = parent;
        }

        private void StreamForm_Load(object sender, EventArgs e)
        {
            //最左边的大表
            dgv_stream.AutoGenerateColumns = false;
            dgv_stream.DataSource = StreamOperator.GetStreamName().Tables[StreamData.STREAM_TABLE].DefaultView;

            streamdata = StreamOperator.LoadStreamInfo();

            dgv_allModule.AutoGenerateColumns = false;
            moduledata = ModulesOperator.LoadModulesInfo();
            dgv_allModule.DataSource = moduledata.Tables[ModuleData.MODULES_TABLE].DefaultView;
            //dgv_selectModule.AutoGenerateColumns = false;
            //dgv_selectModule.DataSource = selectData.Tables[StreamData.STREAM_TABLE].DefaultView;                        
           
            pageStatus = RecordStatus.View ;
            SetFormControlerStatus();
            SetFormControlerData();
        }
        private void SetFormControlerStatus()
        {
            textBox_ProjectName.Text = globalParameters.dbName;
            textBox_ProjectName.ReadOnly = true;
            if (pageStatus == RecordStatus.View)
            {
                this.Size = new Size(326, 462);                    
            }
            else if (pageStatus == RecordStatus.Edit)
            {
                this.Size = new Size(701, 462);
            }
            else if (pageStatus == RecordStatus.Add)
            {
                this.Size = new Size(701, 462);              
            }
        }

        //设置显示内容
        private void SetFormControlerData()
        {
            
            if (pageStatus == RecordStatus.View) { }

            else if (pageStatus == RecordStatus.Edit)
            {
                
                selectStream = dgv_stream.CurrentCell.Value.ToString();
                streamName.Text = selectStream;
                dgv_selectModule.AutoGenerateColumns = false;
                dgv_selectModule.DataSource = selectData.Tables[StreamData.STREAM_TABLE].DefaultView;
   
            }
            else if (pageStatus == RecordStatus.Add)
            {
                selectData = new StreamData();
                streamName.Text = "";
                dgv_selectModule.AutoGenerateColumns = false;
                dgv_selectModule.DataSource = selectData.Tables[StreamData.STREAM_TABLE].DefaultView;
                //modulesNameTable = deleteDuplicateModule();
            }
        }

        //private ModuleData deleteDuplicateModule()
        //{
        //    using (ModuleData modulesNameTable = ModulesOperator.LoadModulesInfo())
        //    {
        //        for (int i = 0; i <= this.selectData.Tables[StreamData.STREAM_TABLE].Rows.Count - 1; i++)
        //        {
        //            string name = selectData.Tables[StreamData.STREAM_TABLE].Rows[i][2].ToString();
        //            DataRow[] dr = modulesNameTable.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + " = '" + name + "'");
        //            modulesNameTable.Tables[ModuleData.MODULES_TABLE].Rows.Remove(dr[0]);
        //        }
        //        dgv_allModule.AutoGenerateColumns = false;
        //        dgv_allModule.DataSource = modulesNameTable.Tables[ModuleData.MODULES_TABLE].DefaultView;
        //        dgv_allModule.Refresh();
        //        return modulesNameTable;
        //    }
        //}
        private void btnAdd_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.Add;
            SetFormControlerStatus();
            SetFormControlerData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.Edit;
            SetFormControlerStatus();
            SetFormControlerData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pageStatus = RecordStatus.View;
            SetFormControlerStatus();
            SetFormControlerData();

            if (MessageBox.Show("您确定要删除所选系统吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                selectStream = dgv_stream.CurrentCell.Value.ToString();
                if (StreamOperator.DeleteStreamInfo(selectStream))
                {
                    StreamForm_Load(sender, e);
                    //MessageBox.Show("删除成功！");                  
                }
            }
        }
        private void btnToRight_Click(object sender, EventArgs e)
        {
            if (streamName.Text == "")
            {
                MessageBox.Show("请先输入数据流名称！");
                return;
            }
            if (IsModuleChecked())
            {           
                for (int i = 0; i <= this.dgv_allModule.RowCount - 1; i++)
                    if (dgv_allModule.Rows[i].Cells[0].Selected)
                    {
                        DataRow dr = selectData.Tables[StreamData.STREAM_TABLE].NewRow();
                        dr[StreamData.NAME_FIELD]=streamName.Text;
                        dr[StreamData.NUM_FIELD]=0;
                        dr[StreamData.MODULES_NAME_FIELD]=dgv_allModule.Rows[i].Cells[0].Value.ToString();
                        selectData.Tables[StreamData.STREAM_TABLE].Rows.Add(dr.ItemArray);
                        //modulesNameTable.Tables[ModuleData.MODULES_TABLE].Rows.RemoveAt(i);
                    }
                //dgv_allModule.Refresh();
                //dgv_selectModule.Refresh();
            }
        }

        private void btnToLeft_Click(object sender, EventArgs e)
        {
            //int count = dgv_selectModule.RowCount;
            if (IsDataChecked())
            {
                for (int i = this.dgv_selectModule.RowCount - 1; i >= 0; i--)
                    if (dgv_selectModule.Rows[i].Cells[0].Selected)
                    {
                        //DataRow dr = modulesNameTable.Tables[ModuleData.MODULES_TABLE].NewRow();      
                        //string mname = selectData.Tables[StreamData.STREAM_TABLE].Rows[i][2].ToString();
                        //dr[ModuleData.NAME_FIELD] = mname;
                        //modulesNameTable.Tables[ModuleData.MODULES_TABLE].Rows.Add(dr.ItemArray);
                        selectData.Tables[StreamData.STREAM_TABLE].Rows.RemoveAt(i);
                    }
                dgv_selectModule.Refresh();
            }
        }

        private bool IsModuleChecked()
        {
            for (int i = 0; i <= this.dgv_allModule.RowCount - 1; i++)
            {
                if (dgv_allModule.Rows[i].Cells[0].Selected)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsDataChecked()
        {
            for (int i = 0; i <= this.dgv_selectModule.RowCount - 1; i++)
            {
                if (dgv_selectModule.Rows[i].Cells[0].Selected)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int count = dgv_selectModule.RowCount;
            if (IsDataChecked())
            {
                for (int i = this.dgv_selectModule.RowCount - 1; i >= 0; i--)
                    if (dgv_selectModule.Rows[i].Cells[0].Selected)
                    {
                        if (i <= 0){
                            MessageBox.Show("已经在最顶端！");
                        }else{
                            object[] array = selectData.Tables[StreamData.STREAM_TABLE].Rows[i-1].ItemArray;
                            selectData.Tables[StreamData.STREAM_TABLE].Rows[i-1].ItemArray = selectData.Tables[StreamData.STREAM_TABLE].Rows[i].ItemArray;
                            selectData.Tables[StreamData.STREAM_TABLE].Rows[i].ItemArray = array;
                        }
                    }
                dgv_selectModule.Refresh();
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (IsDataChecked())
            {
                for (int i = this.dgv_selectModule.RowCount - 1; i >= 0; i--)
                    if (dgv_selectModule.Rows[i].Cells[0].Selected)
                    {
                        if (i == this.dgv_selectModule.RowCount - 1)
                        {
                            MessageBox.Show("已经在最底端！");

                        }
                        else
                        {
                            object[] array = selectData.Tables[StreamData.STREAM_TABLE].Rows[i + 1].ItemArray;
                            selectData.Tables[StreamData.STREAM_TABLE].Rows[i + 1].ItemArray = selectData.Tables[StreamData.STREAM_TABLE].Rows[i].ItemArray;
                            selectData.Tables[StreamData.STREAM_TABLE].Rows[i].ItemArray = array;
                        }        
                    }
                dgv_selectModule.Refresh();
            }
        }

     
        private void dgv_stream_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_stream.CurrentRow == null)
                return;
            selectData = new StreamData();
            selectStream = dgv_stream.CurrentCell.Value.ToString();
            streamName.Text = selectStream;
            DataRow[] odr = streamdata.Tables[StreamData.STREAM_TABLE].Select(StreamData.NAME_FIELD + "='" + selectStream + "'");
            foreach (DataRow row in odr)
            {
                selectData.Tables[StreamData.STREAM_TABLE].Rows.Add(row.ItemArray);
            }
                
            dgv_selectModule.AutoGenerateColumns = false;
            dgv_selectModule.DataSource = selectData.Tables[StreamData.STREAM_TABLE].DefaultView;
            //modulesNameTable = deleteDuplicateModule();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (pageStatus == RecordStatus.Add)
            {
                if (!StreamOperator.InsertStreamInfo(selectData))
                {
                    MessageBox.Show("该数据流已经存在或相邻模块之间没有关系！");
                }
                else
                {
                    dgv_stream.DataSource = StreamOperator.GetStreamName().Tables[StreamData.STREAM_TABLE].DefaultView;
                    streamdata = StreamOperator.LoadStreamInfo();
                    pageStatus = RecordStatus.View;
                }
            }
            else if (pageStatus == RecordStatus.Edit)
            {
                if (!StreamOperator.UpdateStreamInfo(selectData, selectStream))
                {
                    MessageBox.Show("该数据流已经存在或相邻模块之间没有关系！");
                }
                else
                {
                    dgv_stream.DataSource = StreamOperator.GetStreamName().Tables[StreamData.STREAM_TABLE].DefaultView;
                    streamdata = StreamOperator.LoadStreamInfo();
                    pageStatus = RecordStatus.View;
                }
            }
            SetFormControlerData();
            SetFormControlerStatus();
        }

        private void streamName_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < selectData.Tables[StreamData.STREAM_TABLE].Rows.Count; i++ )
            {
                DataRow dr = selectData.Tables[StreamData.STREAM_TABLE].Rows[i];
                dr[StreamData.NAME_FIELD] = streamName.Text;
            }
        }
    }
}
