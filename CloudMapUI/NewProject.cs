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
    public partial class NewProjectForm : Form
    {
        private MainForm paf;
        public NewProjectForm(MainForm parent)
        {
            InitializeComponent();
            paf = parent;
        }

        public MainForm parent { get; set; }

        private void btnFolderBrowser_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            dbSelfPath = folderBrowserDialog1.SelectedPath;
            textBox2.Text = dbSelfPath;
        }

        string dbName = null;
        string dbSelfPath = null;
        
        private void btnNewProjectSure_Click(object sender, EventArgs e)
        {
            if (dbName == null || dbSelfPath == null)
            {
                MessageBox.Show("请输入项目名称和地址！", "关于云图", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                SystemOperator.NewProject(dbName, dbSelfPath);
                paf.panel4.Controls.Clear();
                paf.mainFormStatus();
                this.Hide();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dbName = textBox1.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnNewProjectCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
