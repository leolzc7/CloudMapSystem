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

namespace CloudMapUI
{
    public partial class configForm : Form
    {
        public int count = 1;
        public int x = 35;
        public int y = 47;
        private MainForm paf;
        public configForm(MainForm parent)
        {
            InitializeComponent();
            paf = parent;
           
        }
        public MainForm parent { get; set; }
        private void btnSave_Click(object sender, EventArgs e)
        {
            paf.comboBox_type.Items.Clear();
            paf.comboBox_type.Items.AddRange(new object[] { "所有类型" });
            if (richTextBox1.Lines.Length == 0)
            {
                paf.comboBox_type.Items.AddRange(new object[] { "aa","bb","cc" });
            }
            for (int i = 0; i < richTextBox1.Lines.Length;i++ )
            {
                for (int j = 0; j < i; j++)
                {
                    if (richTextBox1.Lines[j] == richTextBox1.Lines[i])
                    {
                        MessageBox.Show(" 类型不能重复！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                paf.comboBox_type.Items.AddRange(new object[] { richTextBox1.Lines[i] });
                globalParameters.TypeList.Add(richTextBox1.Lines[i]);
            }
            MessageBox.Show(" 保存成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        private void configForm_Load()
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            paf.comboBox_type.Items.Clear();
            paf.comboBox_type.Items.AddRange(new object[] { "所有类型","aa","bb","cc" });
            this.Hide();
        }

        private void configForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            button1_Click(sender, e);
        }

        

    }
}
