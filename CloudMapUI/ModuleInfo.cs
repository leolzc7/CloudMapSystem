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
    public partial class ModuleInfo : Form
    {
        String moduleName;
        ModuleData moduledata;
        public ModuleInfo(MyButton mybtn)
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            moduleName = mybtn.Text;
            moduledata = ModulesOperator.LoadModulesInfo();
            ShowInfo();
        }
        public void ShowInfo()
        {
            DataRow odr = moduledata.Tables[ModuleData.MODULES_TABLE].Select(ModuleData.NAME_FIELD + "='" + moduleName + "'")[0];
            name.Text = odr[ModuleData.NAME_FIELD].ToString();
            type.Text = odr[ModuleData.TYPE_FIELD].ToString();
            level.Text = odr[ModuleData.LEVEL_FIELD].ToString();
            //stream.Text = odr[ModuleData.TYPE_FIELD].ToString();
            comment.Text = odr[ModuleData.COMMENT_FIELD].ToString();
        }

        private void level_TextChanged(object sender, EventArgs e)
        {

        }

        private void comment_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
