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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TextBox type = new TextBox();
　　　　    type.Name="Label"+count;
　　　　    type.Location=new Point(x,y);
            type.Multiline = false;
            type.Size = new System.Drawing.Size(231, 21);
            this.Controls.Add(type);
            count++;
            y += 33;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            paf.comboBox_type.Items.Clear();

            Control.ControlCollection Cons = this.Controls;
            foreach (Control con in Cons)
            {
                if (con is TextBox)
                {
                    paf.comboBox_type.Items.AddRange(new object[] {con.Text.ToString()});                
                }
            }
            MessageBox.Show("保存成功！");
            this.Hide();
        }

        private void configForm_Load()
        {

        }

        

    }
}
