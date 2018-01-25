namespace CloudMapUI
{
    partial class GenerateMapForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateMapForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_selectedCancel = new System.Windows.Forms.Button();
            this.button_order_down = new System.Windows.Forms.Button();
            this.button_order_up = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_selected = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_SelectFinish = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.button_selectedCancel);
            this.panel1.Controls.Add(this.button_order_down);
            this.panel1.Controls.Add(this.button_order_up);
            this.panel1.Controls.Add(this.button_cancel);
            this.panel1.Controls.Add(this.button_selected);
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btn_SelectFinish);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 363);
            this.panel1.TabIndex = 0;
            // 
            // button_selectedCancel
            // 
            this.button_selectedCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.button_selectedCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_selectedCancel.Location = new System.Drawing.Point(218, 325);
            this.button_selectedCancel.Name = "button_selectedCancel";
            this.button_selectedCancel.Size = new System.Drawing.Size(50, 24);
            this.button_selectedCancel.TabIndex = 27;
            this.button_selectedCancel.Text = "取消";
            this.button_selectedCancel.UseVisualStyleBackColor = false;
            // 
            // button_order_down
            // 
            this.button_order_down.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.button_order_down.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_order_down.Image = ((System.Drawing.Image)(resources.GetObject("button_order_down.Image")));
            this.button_order_down.Location = new System.Drawing.Point(378, 199);
            this.button_order_down.Name = "button_order_down";
            this.button_order_down.Size = new System.Drawing.Size(24, 50);
            this.button_order_down.TabIndex = 26;
            this.button_order_down.UseVisualStyleBackColor = false;
            // 
            // button_order_up
            // 
            this.button_order_up.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.button_order_up.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_order_up.Image = ((System.Drawing.Image)(resources.GetObject("button_order_up.Image")));
            this.button_order_up.Location = new System.Drawing.Point(378, 115);
            this.button_order_up.Name = "button_order_up";
            this.button_order_up.Size = new System.Drawing.Size(24, 50);
            this.button_order_up.TabIndex = 25;
            this.button_order_up.UseVisualStyleBackColor = false;
            this.button_order_up.Click += new System.EventHandler(this.button_order_up_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_cancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_cancel.Location = new System.Drawing.Point(178, 194);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(52, 23);
            this.button_cancel.TabIndex = 24;
            this.button_cancel.Text = "<—";
            this.button_cancel.UseVisualStyleBackColor = false;
            // 
            // button_selected
            // 
            this.button_selected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.button_selected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_selected.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_selected.Location = new System.Drawing.Point(178, 129);
            this.button_selected.Name = "button_selected";
            this.button_selected.Size = new System.Drawing.Size(52, 23);
            this.button_selected.TabIndex = 23;
            this.button_selected.Text = "—>";
            this.button_selected.UseVisualStyleBackColor = false;
            // 
            // dataGridView2
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(237, 45);
            this.dataGridView2.Name = "dataGridView2";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(135, 268);
            this.dataGridView2.TabIndex = 22;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(35, 45);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(182)))), ((int)(((byte)(221)))));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(135, 268);
            this.dataGridView1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "业务流名称";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(253, 21);
            this.textBox1.TabIndex = 19;
            // 
            // btn_SelectFinish
            // 
            this.btn_SelectFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(211)))), ((int)(((byte)(233)))));
            this.btn_SelectFinish.FlatAppearance.BorderSize = 5;
            this.btn_SelectFinish.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SelectFinish.Location = new System.Drawing.Point(144, 325);
            this.btn_SelectFinish.Name = "btn_SelectFinish";
            this.btn_SelectFinish.Size = new System.Drawing.Size(50, 24);
            this.btn_SelectFinish.TabIndex = 18;
            this.btn_SelectFinish.Text = "完成";
            this.btn_SelectFinish.UseVisualStyleBackColor = false;
            // 
            // GenerateMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 363);
            this.Controls.Add(this.panel1);
            this.Name = "GenerateMapForm";
            this.Text = "业务流";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_selectedCancel;
        private System.Windows.Forms.Button button_order_up;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_selected;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_SelectFinish;
        protected System.Windows.Forms.Button button_order_down;

    }
}