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
        private MainForm paf;
        public ModuleInfo(MainForm parent)
        {
            InitializeComponent();
            paf = parent;
        }


        private void type_TextChanged(object sender, EventArgs e)
        {

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
