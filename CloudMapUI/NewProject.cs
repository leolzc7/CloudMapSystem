﻿using System;
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

        public static string dbPath;
        public static string dbName;
        string dbSelfPath;
        
        private void btnNewProjectSure_Click(object sender, EventArgs e)
        {
           
        }

        public void connect_open_db()
        {   
           
        }

        public void close_db()
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dbName = textBox1.Text;
        }
    }
}