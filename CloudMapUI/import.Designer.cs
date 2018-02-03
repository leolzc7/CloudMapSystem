namespace CloudMapUI
{
    partial class importForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_SelectFinish = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.selectedAllRelation = new System.Windows.Forms.CheckBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnSelected = new System.Windows.Forms.Button();
            this.dgv_importRelation = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.selectedAllModules = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.dgv_importModule = new System.Windows.Forms.DataGridView();
            this.dgvCheckBoxModule = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnFolderBrowser = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog_import = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_importRelation)).BeginInit();
            this.panel9.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_importModule)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_SelectFinish
            // 
            this.btn_SelectFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btn_SelectFinish.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SelectFinish.Location = new System.Drawing.Point(281, 12);
            this.btn_SelectFinish.Name = "btn_SelectFinish";
            this.btn_SelectFinish.Size = new System.Drawing.Size(50, 24);
            this.btn_SelectFinish.TabIndex = 8;
            this.btn_SelectFinish.Text = "导入";
            this.btn_SelectFinish.UseVisualStyleBackColor = false;
            this.btn_SelectFinish.Click += new System.EventHandler(this.btn_SelectFinish_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(629, 361);
            this.panel1.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel10);
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(222, 46);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(407, 315);
            this.panel5.TabIndex = 2;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel10.Controls.Add(this.selectedAllRelation);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 46);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(407, 269);
            this.panel10.TabIndex = 2;
            // 
            // selectedAllRelation
            // 
            this.selectedAllRelation.AutoSize = true;
            this.selectedAllRelation.Location = new System.Drawing.Point(35, 9);
            this.selectedAllRelation.Name = "selectedAllRelation";
            this.selectedAllRelation.Size = new System.Drawing.Size(48, 16);
            this.selectedAllRelation.TabIndex = 2;
            this.selectedAllRelation.Text = "全选";
            this.selectedAllRelation.UseVisualStyleBackColor = true;
            this.selectedAllRelation.CheckedChanged += new System.EventHandler(this.selectedAllRelation_CheckedChanged);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel11.Controls.Add(this.btnSelected);
            this.panel11.Controls.Add(this.dgv_importRelation);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel11.Location = new System.Drawing.Point(0, 28);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(407, 241);
            this.panel11.TabIndex = 0;
            this.panel11.Paint += new System.Windows.Forms.PaintEventHandler(this.panel11_Paint);
            // 
            // btnSelected
            // 
            this.btnSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btnSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelected.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelected.Location = new System.Drawing.Point(0, 100);
            this.btnSelected.Name = "btnSelected";
            this.btnSelected.Size = new System.Drawing.Size(35, 22);
            this.btnSelected.TabIndex = 24;
            this.btnSelected.Text = "->";
            this.btnSelected.UseVisualStyleBackColor = false;
            this.btnSelected.Click += new System.EventHandler(this.btnSelected_Click);
            // 
            // dgv_importRelation
            // 
            this.dgv_importRelation.AllowUserToAddRows = false;
            this.dgv_importRelation.AllowUserToDeleteRows = false;
            this.dgv_importRelation.AllowUserToOrderColumns = true;
            this.dgv_importRelation.AllowUserToResizeColumns = false;
            this.dgv_importRelation.AllowUserToResizeRows = false;
            this.dgv_importRelation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dgv_importRelation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_importRelation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column2,
            this.Column3});
            this.dgv_importRelation.Location = new System.Drawing.Point(35, 0);
            this.dgv_importRelation.MultiSelect = false;
            this.dgv_importRelation.Name = "dgv_importRelation";
            this.dgv_importRelation.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_importRelation.RowHeadersVisible = false;
            this.dgv_importRelation.RowTemplate.Height = 23;
            this.dgv_importRelation.Size = new System.Drawing.Size(337, 241);
            this.dgv_importRelation.TabIndex = 0;
            this.dgv_importRelation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_importRelation_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = " ";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 30;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.DataPropertyName = "rname";
            this.Column4.HeaderText = "名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "sourceName";
            this.Column2.HeaderText = "源系统";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "targetName";
            this.Column3.HeaderText = "目标系统";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel9.Controls.Add(this.label3);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(407, 46);
            this.panel9.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(35, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "选择关系";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 46);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(222, 315);
            this.panel4.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel7.Controls.Add(this.selectedAllModules);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 46);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(222, 269);
            this.panel7.TabIndex = 1;
            // 
            // selectedAllModules
            // 
            this.selectedAllModules.AutoSize = true;
            this.selectedAllModules.Location = new System.Drawing.Point(35, 9);
            this.selectedAllModules.Name = "selectedAllModules";
            this.selectedAllModules.Size = new System.Drawing.Size(48, 16);
            this.selectedAllModules.TabIndex = 1;
            this.selectedAllModules.Text = "全选";
            this.selectedAllModules.UseVisualStyleBackColor = true;
            this.selectedAllModules.CheckedChanged += new System.EventHandler(this.selectedAllModules_CheckedChanged);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel8.Controls.Add(this.dgv_importModule);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 28);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(222, 241);
            this.panel8.TabIndex = 0;
            // 
            // dgv_importModule
            // 
            this.dgv_importModule.AllowUserToAddRows = false;
            this.dgv_importModule.AllowUserToDeleteRows = false;
            this.dgv_importModule.AllowUserToOrderColumns = true;
            this.dgv_importModule.AllowUserToResizeColumns = false;
            this.dgv_importModule.AllowUserToResizeRows = false;
            this.dgv_importModule.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dgv_importModule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_importModule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCheckBoxModule,
            this.dataGridViewTextBoxColumn1});
            this.dgv_importModule.Location = new System.Drawing.Point(35, 0);
            this.dgv_importModule.MultiSelect = false;
            this.dgv_importModule.Name = "dgv_importModule";
            this.dgv_importModule.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_importModule.RowHeadersVisible = false;
            this.dgv_importModule.RowTemplate.Height = 23;
            this.dgv_importModule.Size = new System.Drawing.Size(187, 241);
            this.dgv_importModule.TabIndex = 1;
            this.dgv_importModule.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_importModule_CellClick);
            this.dgv_importModule.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_importModule_CellContentClick);
            // 
            // dgvCheckBoxModule
            // 
            this.dgvCheckBoxModule.FillWeight = 30.45685F;
            this.dgvCheckBoxModule.HeaderText = " ";
            this.dgvCheckBoxModule.Name = "dgvCheckBoxModule";
            this.dgvCheckBoxModule.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCheckBoxModule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvCheckBoxModule.Width = 28;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn1.FillWeight = 169.5432F;
            this.dataGridViewTextBoxColumn1.HeaderText = "名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(222, 46);
            this.panel6.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(35, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "选择系统";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel3.Controls.Add(this.btnFolderBrowser);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(629, 46);
            this.panel3.TabIndex = 0;
            // 
            // btnFolderBrowser
            // 
            this.btnFolderBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btnFolderBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFolderBrowser.Location = new System.Drawing.Point(349, 16);
            this.btnFolderBrowser.Name = "btnFolderBrowser";
            this.btnFolderBrowser.Size = new System.Drawing.Size(42, 21);
            this.btnFolderBrowser.TabIndex = 5;
            this.btnFolderBrowser.Text = "浏览";
            this.btnFolderBrowser.UseVisualStyleBackColor = false;
            this.btnFolderBrowser.Click += new System.EventHandler(this.btnFolderBrowser_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(89, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(254, 21);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择项目";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btn_SelectFinish);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 361);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(629, 50);
            this.panel2.TabIndex = 15;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(355, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openFileDialog_import
            // 
            this.openFileDialog_import.FileName = "openFileDialog1";
            this.openFileDialog_import.Filter = "(*.db)|*.db";
            // 
            // importForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 411);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "importForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.importForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_importRelation)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_importModule)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_SelectFinish;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnFolderBrowser;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.CheckBox selectedAllRelation;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.CheckBox selectedAllModules;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog_import;
        private System.Windows.Forms.DataGridView dgv_importRelation;
        private System.Windows.Forms.DataGridView dgv_importModule;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button btnSelected;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvCheckBoxModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}