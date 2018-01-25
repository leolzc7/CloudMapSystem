using Data;
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
    public partial class RelationInfo : Form
    {
        String relationName;
        RelationData relationdata;
        public RelationInfo(ALine myAline)
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            relationName = myAline.Text;
            relationdata = RelationOperator.LoadRelationInfo();
            ShowInfo();
        }

        private void ShowInfo()
        {
            DataRow odr = relationdata.Tables[RelationData.RELATION_TABLE].Select(RelationData.NAME_FIELD + "='" + relationName + "'")[0];
            name.Text = odr[RelationData.NAME_FIELD].ToString();
            type.Text = odr[RelationData.TYPE_FIELD].ToString();
            source_name.Text = odr[RelationData.SOURCENAME_FIELD].ToString();
            target_name.Text = odr[RelationData.TARGETNAME_FIELD].ToString();
            if (odr[RelationData.BIDIRECTION_FIELD].ToString().Equals("0"))
            {
                bidirection.Text = "否";
            }
            else
            {
                bidirection.Text = "是";
            }
            
            //stream.Text = odr[ModuleData.TYPE_FIELD].ToString();
            comment.Text = odr[RelationData.COMMENT_FIELD].ToString();
        }
      
        
        public RelationInfo()
        {
            InitializeComponent();
        }

        private void RelationInfo_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
