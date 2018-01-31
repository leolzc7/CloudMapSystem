namespace CloudMapUI
{
    partial class RelationEditForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_ProjectName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.type = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton_bidirection = new System.Windows.Forms.RadioButton();
            this.radioButton_single = new System.Windows.Forms.RadioButton();
            this.text_type = new System.Windows.Forms.Label();
            this.comboBox_Type = new System.Windows.Forms.ComboBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.comment = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_addRelation = new System.Windows.Forms.Panel();
            this.dgv_target = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_source = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.target = new System.Windows.Forms.TextBox();
            this.source = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView_relation = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel_addRelation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_target)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_source)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_relation)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.textBox_ProjectName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 46);
            this.panel1.TabIndex = 45;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // textBox_ProjectName
            // 
            this.textBox_ProjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.textBox_ProjectName.Location = new System.Drawing.Point(94, 12);
            this.textBox_ProjectName.Name = "textBox_ProjectName";
            this.textBox_ProjectName.ReadOnly = true;
            this.textBox_ProjectName.Size = new System.Drawing.Size(307, 21);
            this.textBox_ProjectName.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "项目名称";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(877, 539);
            this.panel2.TabIndex = 46;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(401, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(476, 539);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Controls.Add(this.panel_addRelation);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(478, 545);
            this.panel5.TabIndex = 77;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel8.Controls.Add(this.btnCancel);
            this.panel8.Controls.Add(this.type);
            this.panel8.Controls.Add(this.btnSave);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.radioButton_bidirection);
            this.panel8.Controls.Add(this.radioButton_single);
            this.panel8.Controls.Add(this.text_type);
            this.panel8.Controls.Add(this.comboBox_Type);
            this.panel8.Controls.Add(this.btnUpdate);
            this.panel8.Controls.Add(this.comment);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 270);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(478, 253);
            this.panel8.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(167, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 24);
            this.btnCancel.TabIndex = 89;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // type
            // 
            this.type.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.type.Location = new System.Drawing.Point(69, 33);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(121, 21);
            this.type.TabIndex = 88;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(93, 152);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 24);
            this.btnSave.TabIndex = 87;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 86;
            this.label8.Text = "方向";
            // 
            // radioButton_bidirection
            // 
            this.radioButton_bidirection.AutoSize = true;
            this.radioButton_bidirection.Location = new System.Drawing.Point(122, 6);
            this.radioButton_bidirection.Name = "radioButton_bidirection";
            this.radioButton_bidirection.Size = new System.Drawing.Size(47, 16);
            this.radioButton_bidirection.TabIndex = 85;
            this.radioButton_bidirection.TabStop = true;
            this.radioButton_bidirection.Text = "双向";
            this.radioButton_bidirection.UseVisualStyleBackColor = true;
            // 
            // radioButton_single
            // 
            this.radioButton_single.AutoSize = true;
            this.radioButton_single.Location = new System.Drawing.Point(69, 6);
            this.radioButton_single.Name = "radioButton_single";
            this.radioButton_single.Size = new System.Drawing.Size(47, 16);
            this.radioButton_single.TabIndex = 84;
            this.radioButton_single.TabStop = true;
            this.radioButton_single.Text = "单向";
            this.radioButton_single.UseVisualStyleBackColor = true;
            // 
            // text_type
            // 
            this.text_type.AutoSize = true;
            this.text_type.Location = new System.Drawing.Point(34, 37);
            this.text_type.Name = "text_type";
            this.text_type.Size = new System.Drawing.Size(29, 12);
            this.text_type.TabIndex = 83;
            this.text_type.Text = "类型";
            // 
            // comboBox_Type
            // 
            this.comboBox_Type.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.comboBox_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Type.FormattingEnabled = true;
            this.comboBox_Type.Items.AddRange(new object[] {
            "type1",
            "type2",
            "type3"});
            this.comboBox_Type.Location = new System.Drawing.Point(69, 34);
            this.comboBox_Type.Name = "comboBox_Type";
            this.comboBox_Type.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Type.TabIndex = 82;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(167, 152);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(50, 24);
            this.btnUpdate.TabIndex = 77;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // comment
            // 
            this.comment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.comment.Location = new System.Drawing.Point(69, 67);
            this.comment.Multiline = true;
            this.comment.Name = "comment";
            this.comment.Size = new System.Drawing.Size(374, 73);
            this.comment.TabIndex = 81;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 79;
            this.label3.Text = "描述";
            // 
            // panel_addRelation
            // 
            this.panel_addRelation.Controls.Add(this.dgv_target);
            this.panel_addRelation.Controls.Add(this.dgv_source);
            this.panel_addRelation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_addRelation.Location = new System.Drawing.Point(0, 63);
            this.panel_addRelation.Name = "panel_addRelation";
            this.panel_addRelation.Size = new System.Drawing.Size(478, 207);
            this.panel_addRelation.TabIndex = 1;
            this.panel_addRelation.Visible = false;
            // 
            // dgv_target
            // 
            this.dgv_target.AllowUserToAddRows = false;
            this.dgv_target.AllowUserToDeleteRows = false;
            this.dgv_target.AllowUserToResizeColumns = false;
            this.dgv_target.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dgv_target.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_target.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dgv_target.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_target.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.dgv_target.Location = new System.Drawing.Point(268, 0);
            this.dgv_target.MultiSelect = false;
            this.dgv_target.Name = "dgv_target";
            this.dgv_target.ReadOnly = true;
            this.dgv_target.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dgv_target.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_target.RowTemplate.Height = 23;
            this.dgv_target.Size = new System.Drawing.Size(175, 210);
            this.dgv_target.TabIndex = 91;
            this.dgv_target.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_target_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn1.HeaderText = "目标系统";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dgv_source
            // 
            this.dgv_source.AllowUserToAddRows = false;
            this.dgv_source.AllowUserToDeleteRows = false;
            this.dgv_source.AllowUserToResizeColumns = false;
            this.dgv_source.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dgv_source.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_source.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dgv_source.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_source.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4});
            this.dgv_source.Location = new System.Drawing.Point(69, 0);
            this.dgv_source.MultiSelect = false;
            this.dgv_source.Name = "dgv_source";
            this.dgv_source.ReadOnly = true;
            this.dgv_source.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dgv_source.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_source.RowTemplate.Height = 23;
            this.dgv_source.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_source.Size = new System.Drawing.Size(175, 210);
            this.dgv_source.TabIndex = 90;
            this.dgv_source.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_source_CellClick);
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.DataPropertyName = "name";
            this.Column4.HeaderText = "源系统";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.target);
            this.panel6.Controls.Add(this.source);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.name);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(478, 63);
            this.panel6.TabIndex = 0;
            // 
            // target
            // 
            this.target.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.target.Location = new System.Drawing.Point(303, 33);
            this.target.Name = "target";
            this.target.ReadOnly = true;
            this.target.Size = new System.Drawing.Size(140, 21);
            this.target.TabIndex = 96;
            // 
            // source
            // 
            this.source.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.source.Location = new System.Drawing.Point(69, 33);
            this.source.Name = "source";
            this.source.ReadOnly = true;
            this.source.Size = new System.Drawing.Size(152, 21);
            this.source.TabIndex = 95;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(244, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 94;
            this.label7.Text = "目标系统";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 93;
            this.label6.Text = "源系统";
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.name.Location = new System.Drawing.Point(69, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(152, 21);
            this.name.TabIndex = 82;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 81;
            this.label2.Text = "名称";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel3.Controls.Add(this.dataGridView_relation);
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(401, 539);
            this.panel3.TabIndex = 0;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // dataGridView_relation
            // 
            this.dataGridView_relation.AllowUserToAddRows = false;
            this.dataGridView_relation.AllowUserToDeleteRows = false;
            this.dataGridView_relation.AllowUserToResizeColumns = false;
            this.dataGridView_relation.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dataGridView_relation.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_relation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_relation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dataGridView_relation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_relation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView_relation.Location = new System.Drawing.Point(35, 0);
            this.dataGridView_relation.MultiSelect = false;
            this.dataGridView_relation.Name = "dataGridView_relation";
            this.dataGridView_relation.ReadOnly = true;
            this.dataGridView_relation.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dataGridView_relation.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_relation.RowTemplate.Height = 23;
            this.dataGridView_relation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_relation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_relation.Size = new System.Drawing.Size(366, 490);
            this.dataGridView_relation.TabIndex = 72;
            this.dataGridView_relation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_relation_CellClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "rname";
            this.Column1.HeaderText = "名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
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
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(133, 502);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(50, 24);
            this.btnDelete.TabIndex = 71;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(59, 502);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 24);
            this.btnAdd.TabIndex = 70;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // RelationEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 585);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RelationEditForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑关系";
            this.Load += new System.EventHandler(this.RelationEditForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel_addRelation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_target)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_source)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_relation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView_relation;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButton_bidirection;
        private System.Windows.Forms.RadioButton radioButton_single;
        private System.Windows.Forms.Label text_type;
        private System.Windows.Forms.ComboBox comboBox_Type;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox comment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox target;
        private System.Windows.Forms.TextBox source;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBox_ProjectName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel_addRelation;
        private System.Windows.Forms.DataGridView dgv_target;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridView dgv_source;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}