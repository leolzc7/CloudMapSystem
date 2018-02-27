using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using Data;
using DataAccess;
using DrawLineRules;
using System.IO;
using System.Threading;

namespace CloudMapUI
{
    public partial class MainForm : Form
    {
        public static int penWidth=1;
        public static ModuleData moduledata;
        public static RelationData relationdata;
        public static MainForm mainform;
        public static int panelWidth;
        public static int panelHeight;
        public static Color currentColor;
        public static bool isSaved = false;
        public MainForm()
        {
            InitializeComponent();
        }

        //在没有打开项目时，和项目相关的控件不可用
        public void mainFormStatus()
        {
            if (globalParameters.dbName == null)
            {
                ToolStripMenuItem_saveImage.Enabled = false;
                ToolStripMenuItem_SaveAs.Enabled = false;
                ToolStripMenuItem_PrePrint.Enabled = false;
                ToolStripMenuItem_Print.Enabled = false;
                ToolStripMenuItem_AddModule.Enabled = false;
                ToolStripMenuItem_AddRelation.Enabled = false;
                ToolStripMenuItem_import.Enabled = false;
                ToolStripMenuItem_SaveProject.Enabled = false;
                添加业务流ToolStripMenuItem.Enabled = false;
                类型配置ToolStripMenuItem.Enabled = true;
                toolStripButton_stream.Enabled = false;
                ToolStripMenuItem_DisplayScale.Enabled = false;
                ToolStripMenuItem_Refresh.Enabled = false;

                toolStripButton_saveProject.Enabled = false;
                toolStripButton_saveImage.Enabled = false;
                toolStripButton_prePrint.Enabled = false;
                toolStripButton_import.Enabled = false;
                toolStripButton_addModule.Enabled = false;
                toolStripButton_addRelation.Enabled = false;
                toolStripButton1.Enabled = false;

                dataGridView_module.Visible = false;
                dataGridView_relation.Visible = false;
                panel2.Visible = false;
            }
            else
            {
                ToolStripMenuItem_saveImage.Enabled = true;
                ToolStripMenuItem_SaveAs.Enabled = true;
                ToolStripMenuItem_PrePrint.Enabled = true;
                ToolStripMenuItem_Print.Enabled = true;
                ToolStripMenuItem_AddModule.Enabled = true;
                ToolStripMenuItem_AddRelation.Enabled = true;
                ToolStripMenuItem_import.Enabled = true;
                ToolStripMenuItem_SaveProject.Enabled = true;
                添加业务流ToolStripMenuItem.Enabled = true;
                类型配置ToolStripMenuItem.Enabled = false;
                toolStripButton_stream.Enabled = true;
                ToolStripMenuItem_DisplayScale.Enabled = true;
                ToolStripMenuItem_Refresh.Enabled = true;

                toolStripButton_saveProject.Enabled = true;
                toolStripButton_saveImage.Enabled = true;
                toolStripButton_prePrint.Enabled = true;
                toolStripButton_import.Enabled = true;
                toolStripButton_addModule.Enabled = true;
                toolStripButton_addRelation.Enabled = true;
                toolStripButton1.Enabled = true;

                panel2.Visible = true;

                dataGridView_module.Visible = true;
                dataGridView_module.AutoGenerateColumns = false;
                moduledata = ModulesOperator.LoadModulesInfo();
                dataGridView_module.DataSource = moduledata.Tables[ModuleData.MODULES_TABLE].DefaultView;

                dataGridView_relation.Visible = true;
                dataGridView_relation.AutoGenerateColumns = false;
                relationdata = RelationOperator.LoadRelationInfo();
                dataGridView_relation.DataSource = relationdata.Tables[RelationData.RELATION_TABLE].DefaultView;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mainFormStatus();
            AddHistoryItem();
            SetCanvas();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        
        private void SetCanvas()
        {
            //设置画布，使其可以放大缩小
            int width = Screen.PrimaryScreen.Bounds.Width - 200;//得到与屏幕一样大小的panel1，用于存放panel4.画图
            int height = Screen.PrimaryScreen.Bounds.Height - 196;
            panel1.Size = new Size(width, height);
            panel1.HorizontalScroll.Visible = true;
            panel1.VerticalScroll.Visible = true;
            panel4.Size = panel1.Size;
            panelWidth = panel4.Size.Width;
            panelHeight = panel4.Size.Height;
            this.comboBox_level.SelectedIndex = 2;
            this.comboBox_type.SelectedIndex = 0;
        }
        private void AddHistoryItem()
        {
            SystemOperator.ReadHistory();
            foreach (string history in globalParameters.dbHistory)
            {
                if (history == "")
                {
                    return;
                }
                else
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Name = history;
                    item.Text = history;
                    item.Click += new EventHandler(historyItemClik);
                    ToolStripMenuItem_history.DropDownItems.Add(item);
                }
            }
        }
        private void historyItemClik(object sender, EventArgs e)
        {
            string path = ((ToolStripMenuItem)sender).Text;
            bool fileExist = SystemOperator.OpenProject(path,true);
            if (!fileExist)
            {
                MessageBox.Show("该项目不存在！", "关于云图", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                ((ToolStripMenuItem)sender).Visible = false;
            }
            mainFormStatus();
        }
        //生成云图按钮
        public void btn_generateMap_Click(object sender, EventArgs e)
        {
            panel4.BackColor = Color.White;
            panel4.Controls.Clear();//控件的清空
            this.panel4.Refresh();//Graphics的清空
            DrawModuleAndLines();//调用画控件函数
        }

        private void NToolStripMenuItem_newProject_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("设置系统类型（否则使用默认值）", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                类型配置ToolStripMenuItem_Click(sender, e);
                this.comboBox_type.SelectedIndex = 0;
            }
            else
            {
                this.comboBox_type.Items.Clear();
                this.comboBox_type.Items.AddRange(new object[] { "所有类型", "aa", "bb", "cc" });//将控件中的内容设置为默认值
                this.comboBox_type.SelectedIndex = 0;
            }
            NewProjectForm newProjrctFrom = new NewProjectForm(this);
            newProjrctFrom.ShowDialog();
            isSaved = false;
            ResetColor();
            if (globalParameters.dbName != null)
            {
                this.Text = globalParameters.dbName + "-企业云图";
            }
        }

        //导入项目
        private void ToolStripMenuItem_import_Click(object sender, EventArgs e)
        {
            importForm import = new importForm(this);
            import.ShowDialog();
            mainFormStatus();
        }

        private void ToolStripMenuItem_OpenProject_Click(object sender, EventArgs e)
        {
            if (globalParameters.dbPath != null && globalParameters.dbPath != "" && !isSaved)
            {
                DialogResult result = MessageBox.Show("是否保存当前项目并打开新的项目?", "关于云图", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    ToolStripMenuItem_SaveProject_Click(sender, e);
                }
            }
            if (openFileDialog_OpenProject.ShowDialog() != DialogResult.OK)
                return;
            SystemOperator.OpenProject(openFileDialog_OpenProject.FileName, true);
            isSaved = true;

            SystemOperator.getXmlValue(); //读取xml中的类型文件
            if (globalParameters.TypeList.Count > 0)
            {
                this.comboBox_type.Items.Clear();
                this.comboBox_type.Items.Add("所有类型");
                foreach (string type in globalParameters.TypeList)
                {
                    this.comboBox_type.Items.AddRange(new object[] { type });
                }
            }
            else
            {
                this.comboBox_type.Items.Clear();
                this.comboBox_type.Items.AddRange(new object[] { "所有类型","aa","bb","cc" });//将控件中的内容设置为默认值
                this.comboBox_type.SelectedIndex = 0;
            }
            List<string> color = SystemOperator.GetColor();//读取xml中界面设置文件
            if (color.Count == 7 && color[4] != "")
            {
                moduleFontColor.Color = ColorTranslator.FromHtml(color[0]);
                ModuleColor.Color = ColorTranslator.FromHtml(color[1]);
                BorderColor.Color = ColorTranslator.FromHtml(color[2]);
                LineColor.Color = ColorTranslator.FromHtml(color[3]);
                penWidth = Convert.ToInt32(color[4]);
                float size = Convert.ToSingle(color[6]);
                Font newFont = new Font(color[5],size);
                fontDialog1.Font = newFont;
            }
            else
            {
                ResetColor();
            }
            btn_generateMap_Click(sender, e);
            if (globalParameters.dbName != null)
            {
                this.Text = globalParameters.dbName + "-企业云图";
            }
            mainFormStatus();
        }
        private void ResetColor()
        {
            moduleFontColor.Color = Color.Black;
            ModuleColor.Color = Color.FromArgb( 213, 238, 255);
            BorderColor.Color = Color.Black;
            LineColor.Color = Color.Black;
            penWidth = 1;
            float size = 13;
            Font newFont = new Font("微软雅黑", size);
            fontDialog1.Font = newFont;
        }

        private void ToolStripMenuItem_SaveProject_Click(object sender, EventArgs e)
        {
            SystemOperator.SaveBackupDb();
            isSaved = true;
            //将画图上的颜色配置保存下来
            string mfColor = ColorTranslator.ToHtml(moduleFontColor.Color);
            SystemOperator.ChangeFondConfig("moduleFontColor", mfColor);
            string mColor = ColorTranslator.ToHtml(ModuleColor.Color);
            SystemOperator.ChangeFondConfig("moduleColor", mColor);
            string bColor = ColorTranslator.ToHtml(BorderColor.Color);
            SystemOperator.ChangeFondConfig("borderColor", bColor);
            string lColor = ColorTranslator.ToHtml(LineColor.Color);
            SystemOperator.ChangeFondConfig("lineColor", lColor);
            SystemOperator.ChangeFondConfig("lineWidth", penWidth.ToString());
            string fontName = fontDialog1.Font.Name;
            SystemOperator.ChangeFondConfig("fontName", fontName);
            string fontSize = fontDialog1.Font.Size.ToString();
            SystemOperator.ChangeFondConfig("fontSize", fontSize);

            MessageBox.Show("保存成功！", "关于云图", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }
        private void ToolStripMenuItem_SaveAs_Click(object sender, EventArgs e)
        {
            saveFileDialog_SaveProject.ShowDialog();
            string filePath = saveFileDialog_SaveProject.FileName;
            string[] text = globalParameters.dbPath.Split('=');
            string oldFilePath = globalParameters.tempDb;
            if (filePath == null || filePath == "")
            {
                return;
            }
            else
            {
                File.Copy(oldFilePath, filePath);
            } 
        }

        private void 保存云图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog_saveImage.ShowDialog();
            if (saveFileDialog_saveImage.FileName == null || saveFileDialog_saveImage.FileName == "")
                return;
            SaveFileToImage(saveFileDialog_saveImage.FileName);
        }

        private void ToolStripMenuItem_Print_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custum", panel4.Width, panel4.Height);
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage);
            printPreviewDialog1.Document = printDoc;
            DialogResult result = printDialog1.ShowDialog();
        }
        private void ToolStripMenuItem_PrePrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custum", panel4.Width, panel4.Height);
            printDoc.DefaultPageSettings.Landscape = false;
            printDoc.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage);
            printPreviewDialog1.Document = printDoc; 
            DialogResult result= printPreviewDialog1.ShowDialog();
        }
        //设置打印内容
        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, panel4.Width, panel4.Height);
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {
                this.panel4.DrawToBitmap(bmp, rect);
                e.Graphics.DrawImageUnscaledAndClipped(bmp, rect);
            }
        }

        private void SaveFileToImage(string filename)
        {
            Rectangle rect = new Rectangle(0, 0, panel4.Width, panel4.Height);
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {
                this.panel4.DrawToBitmap(bmp, rect);
                bmp.Save(filename);
            }
        }

        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            if (globalParameters.dbPath != null && globalParameters.dbPath != "" && !isSaved)
            {
                DialogResult result = MessageBox.Show("是否保存当前项目?", "关于云图", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    ToolStripMenuItem_SaveProject_Click(sender, e);
                }
            }
            SystemOperator.WriteHistory();
            System.Environment.Exit(0);
        }
   
        private void ToolStripMenuItem_AddModule_Click(object sender, EventArgs e)
        {
            ModuleEditForm newModuleEditForm = new ModuleEditForm(this);
            newModuleEditForm.ShowDialog();
            mainFormStatus();
            isSaved = false;
        }

        private void ToolStripMenuItem_AddRelation_Click(object sender, EventArgs e)
        {
            RelationEditForm newRelationEditForm = new RelationEditForm(this);
            newRelationEditForm.ShowDialog();
            mainFormStatus();
            isSaved = false;
        }

        private void ToolStripMenuItem_BorderColor_Click(object sender, EventArgs e)
        {
            BorderColor.ShowDialog();
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is Button)
                {
                    ((Button)con).FlatAppearance.BorderColor = BorderColor.Color;
                }
            }
            isSaved = false;
        }
        private void ToolStripMenuItem_colorFilling_Click(object sender, EventArgs e)
        {
            ModuleColor.ShowDialog();
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is Button)
                {
                    ((Button)con).BackColor = ModuleColor.Color;
                }
            }
            isSaved = false;
        }

        private void 模块字体颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moduleFontColor.ShowDialog();
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is Button)
                {
                    ((Button)con).ForeColor = moduleFontColor.Color;
                }
            }
            isSaved = false;
        }

        private void ToolStripMenuItem_LineColor_Click(object sender, EventArgs e)
        {
            LineColor.ShowDialog();
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    ((ALine)con).Pencolor = LineColor.Color;
                }
            }
            isSaved = false;
        }

        private void ToolStripMenuItem_DisplayScale_Click(object sender, EventArgs e)
        {
            DisplayScaleForm displayScaleForm = new DisplayScaleForm(this);
            displayScaleForm.ShowDialog();
            isSaved = false;
        }

        private void 注释ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is Button)
                {
                    ((Button)con).Font = fontDialog1.Font;
                }
            }
            isSaved = false;
        }
        public void ChangeLineWidth(int penwidth)
        {
            Control.ControlCollection Cons = this.panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    ((ALine)con).Penwidth = penwidth;
                    if (((ALine)con).Points[0] == ((ALine)con).Points[2])
                    {
                        ((ALine)con).Location = new Point(((ALine)con).Points[0] - 4 * ((ALine)con).Penwidth, ((ALine)con).Points[1]);
                    }
                    else
                        ((ALine)con).Location = new Point(((ALine)con).Points[0], ((ALine)con).Points[1] - 4 * ((ALine)con).Penwidth);
                }
            }
            isSaved = false;
        }
        private void 磅ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ChangeLineWidth(1);
            penWidth = 1;
        }
        private void 磅ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ChangeLineWidth(2);
            penWidth = 2;
        }

        private void 磅ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ChangeLineWidth(4);
            penWidth = 4;
        }
        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("可视化系统之间的自动布线工具！", "关于云图", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }
         
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. 单击系统：系统和与之相连的关系线高亮显示(系统背景边框颜色加深，关系线颜色变为红色）。\r\n2. 双击系统：弹出系统详细信息窗口，并恢复界面上所有系统关系线的默认属性。\r\n3. 单击关系线：红色显示该关系，并在合适位置显示关系名称。\r\n4. 双击关系线：弹出关系线详细信息窗口，并恢复界面上所有关系线的默认属性。\r\n5. 双击关系线上显示的关系名称：隐藏该名称显示。",
                "界面操作指南", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (globalParameters.dbPath != null && globalParameters.dbPath != "" && !isSaved)
            {
                DialogResult result = MessageBox.Show("是否保存当前项目?", "关于云图", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    ToolStripMenuItem_SaveProject_Click(sender, e);
                }
            }
            SystemOperator.WriteHistory();
        }
       
        // Icon click events
        private void toolStripButton_newProject_Click(object sender, EventArgs e)
        {
            NToolStripMenuItem_newProject_Click(sender, e);
        }

        private void toolStripButton_openProject_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_OpenProject_Click(sender, e);
        }

        private void toolStripButton_moduleFontColor_Click(object sender, EventArgs e)
        {
            模块字体颜色ToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton_saveProject_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_SaveProject_Click(sender, e);
        }

        private void toolStripButton_prePrint_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_Print_Click(sender,e);
        }

        private void toolStripButton_import_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_import_Click(sender, e);
        }

        private void toolStripButton_addModule_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_AddModule_Click(sender, e);
        }

        private void toolStripButton_addRelation_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_AddRelation_Click(sender, e);
        }

        private void toolStripDropDownButton_colorFilling_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_colorFilling_Click(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_DisplayScale_Click(sender, e);
        }

        private void toolStripButton_information_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_About_Click(sender, e);
        }

        private void toolStripButton_stream_Click(object sender, EventArgs e)
        {
            添加业务流ToolStripMenuItem_Click(sender, e);
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            帮助ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton_saveImage_Click(object sender, EventArgs e)
        {
            保存云图ToolStripMenuItem_Click(sender,e);
        }

        private void toolStripDropDownButton_comment_Click(object sender, EventArgs e)
        {
            注释ToolStripMenuItem_Click(sender,e);
        }

        private void toolStripDropDownButton_borderLIne_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_BorderColor_Click(sender,e);
        }

        private void toolStripDropDownButton_lineColor_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_LineColor_Click(sender,e);
        }
        #region StreamUIOperations
        //全局的Streamlist对象，方便不同函数使用
        public List<StreamOperator.streamList> streamsInfo = new List<StreamOperator.streamList>();
        private void MyButton_RightMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SetContextMenuStrip(sender);
            }
        }
        //动态生成鼠标右键菜单
        private void SetContextMenuStrip(object sender)
        {
            this.contextMenuStrip_RightKey.Items.Clear();
            string moduleName = ((MyButton)sender).Text;
            streamsInfo = StreamOperator.GetStreamForModule(moduleName);
            foreach (StreamOperator.streamList stream in streamsInfo)
            {
                ToolStripItem item = new ToolStripMenuItem();
                item.Name = stream.streamName;
                item.Text = stream.streamName;
                item.Click += new EventHandler(contextMenuStrip1_ItemClick);
                this.contextMenuStrip_RightKey.Items.Add(item);
            }
        }
        //点击右键菜单中某一行的触发函数
        private delegate void myDelegate(ALine aline);//声明委托
        private Thread thread1;
        
        private void contextMenuStrip1_ItemClick(object sender, EventArgs e)
        {
            //调用带参数的线程更新界面
            thread1 = new Thread(new ParameterizedThreadStart(UpdateStream));
            thread1.Start(sender);
        }

        private void UpdateStream(object sender)
        {
            //panel4.Controls.CheckForIllegalCrossThreadCalls = false;
            List<string> modules = new List<string>();//业务流包含的模块的名字列表
            Control.ControlCollection Cons = this.panel4.Controls;
            List<MyButton> streamNamelist = new List<MyButton>();
            List<ALine> streamAlinelist = new List<ALine>();
            foreach (StreamOperator.streamList stream in streamsInfo)
            {
                if (stream.streamName == ((ToolStripItem)sender).Name)
                {
                    modules = stream.modulesList;
                }
            }
            for (int i = 0; i < modules.Count; i++)
            {
                #region 显示完的模块变灰
                if (i > 0)
                {
                    foreach (Control con in Cons)
                    {
                        if (con is MyButton && ((MyButton)con).Text == modules[i-1])
                        {
                            ((MyButton)con).FlatAppearance.BorderSize = 1;
                            ((MyButton)con).BackColor = Color.LightGray;
                        }
                    }
                }
                #endregion

                #region 模块闪烁
                foreach (Control con in Cons)
                {
                    if (con is MyButton && ((MyButton)con).Text == modules[i] && ((MyButton)con).InvokeRequired)
                    {
                        ((MyButton)con).FlatAppearance.BorderSize = 3;
                        ((MyButton)con).BackColor = Color.Red;
                        System.Threading.Thread.Sleep(300);
                        ((MyButton)con).FlatAppearance.BorderSize = 1;
                        ((MyButton)con).BackColor = Color.LightGray;
                        System.Threading.Thread.Sleep(300);
                        ((MyButton)con).FlatAppearance.BorderSize = 3;
                        ((MyButton)con).BackColor = Color.Red;
                        System.Threading.Thread.Sleep(300);
                        ((MyButton)con).FlatAppearance.BorderSize = 1;
                        ((MyButton)con).BackColor = Color.LightGray;
                        System.Threading.Thread.Sleep(300);
                        ((MyButton)con).FlatAppearance.BorderSize = 3;
                        ((MyButton)con).BackColor = Color.Red;
                        System.Threading.Thread.Sleep(300);
                        ((MyButton)con).FlatAppearance.BorderSize = 1;
                        ((MyButton)con).BackColor = Color.LightGray;
                    }
                }
                System.Threading.Thread.Sleep(700);//高亮显示完模块后，暂停时间长度
                #endregion

                #region 线条闪烁
                if (i < modules.Count - 1)
                {
                    string relationName = RelationOperator.GetRelationName(modules[i], modules[i + 1]);
                    int sum = 0;
                    List<ALine> relationNamelist = new List<ALine>();
                   foreach(Control con in Cons)
                   {
                       if (con is ALine && ((ALine)con).Text == relationName)
                       {
                           relationNamelist.Add(((ALine)con));
                           streamAlinelist.Add(((ALine)con));
                       }
                       
                   }
                    foreach (ALine aline in relationNamelist)
                    {
                        aline.Pencolor = Color.Red;
                    }
                    System.Threading.Thread.Sleep(300);
                    foreach (ALine aline in relationNamelist)
                    {
                        aline.Pencolor = Color.LightGray;
                    }
                    System.Threading.Thread.Sleep(300);
                    foreach (ALine aline in relationNamelist)
                    {
                        aline.Pencolor = Color.Red;
                    }
                    System.Threading.Thread.Sleep(300);
                    foreach (ALine aline in relationNamelist)
                    {
                        aline.Pencolor = Color.LightGray;
                    }
                    System.Threading.Thread.Sleep(300);
                    foreach (ALine aline in relationNamelist)
                    {
                        aline.Pencolor = Color.Red;
                    }
                    System.Threading.Thread.Sleep(300);
                    foreach (ALine aline in relationNamelist)
                    {
                        aline.Pencolor = Color.LightGray;
                        if (sum == 0)
                        {
                            WriteLabel(aline);
                            sum++;
                        }
                    }
                }
                System.Threading.Thread.Sleep(700);//高亮显示关系线后，暂停同样时间，等待下一个循环显示下一个模块
            }
            #endregion

            #region 全部闪烁
            foreach (Control con in Cons)
            {
                if (con is MyButton)
                {
                    if (modules.Contains(((MyButton)con).Text))
                    {
                        streamNamelist.Add(((MyButton)con));
                    }
                }
            }

            for (int i = 0; i < streamNamelist.Count; i++)
            {
                streamNamelist[i].FlatAppearance.BorderSize = 3;
                streamNamelist[i].BackColor = Color.Red;
            }
            foreach (ALine aline in streamAlinelist)
            {
                aline.Pencolor = Color.Red;
            }
            System.Threading.Thread.Sleep(300);
            for (int i = 0; i < streamNamelist.Count; i++)
            {
                streamNamelist[i].FlatAppearance.BorderSize = 1;
                streamNamelist[i].BackColor = Color.LightGray;
            }
            foreach (ALine aline in streamAlinelist)
            {
                aline.Pencolor = Color.LightGray;
            }
            System.Threading.Thread.Sleep(300);
            for (int i = 0; i < streamNamelist.Count; i++)
            {
                streamNamelist[i].FlatAppearance.BorderSize = 3;
                streamNamelist[i].BackColor = Color.Red;
            }
            foreach (ALine aline in streamAlinelist)
            {
                aline.Pencolor = Color.Red;
            }
            System.Threading.Thread.Sleep(300);
            for (int i = 0; i < streamNamelist.Count; i++)
            {
                streamNamelist[i].FlatAppearance.BorderSize = 1;
                streamNamelist[i].BackColor = Color.LightGray;
            }
            foreach (ALine aline in streamAlinelist)
            {
                aline.Pencolor = Color.LightGray;
            }
            System.Threading.Thread.Sleep(300);
            for (int i = 0; i < streamNamelist.Count; i++)
            {
                streamNamelist[i].FlatAppearance.BorderSize = 3;
                streamNamelist[i].BackColor = Color.Red;
            }
            foreach (ALine aline in streamAlinelist)
            {
                aline.Pencolor = Color.Red;
            }
            System.Threading.Thread.Sleep(300);
            for (int i = 0; i < streamNamelist.Count; i++)
            {
                streamNamelist[i].FlatAppearance.BorderSize = 1;
                streamNamelist[i].BackColor = Color.LightGray;
            }
            foreach (ALine aline in streamAlinelist)
            {
                aline.Pencolor = Color.LightGray;
            }
            System.Threading.Thread.Sleep(300);
            for (int i = 0; i < streamNamelist.Count; i++)
            {
                streamNamelist[i].FlatAppearance.BorderSize = 3;
                streamNamelist[i].BackColor = Color.Red;
            }
            foreach (ALine aline in streamAlinelist)
            {
                aline.Pencolor = Color.Red;
            }
            System.Threading.Thread.Sleep(300);
            for (int i = 0; i < streamNamelist.Count; i++)
            {
                streamNamelist[i].FlatAppearance.BorderSize = 1;
                streamNamelist[i].BackColor = Color.LightGray;
            }
            foreach (ALine aline in streamAlinelist)
            {
                aline.Pencolor = Color.LightGray;
            }
            System.Threading.Thread.Sleep(300);
            #endregion
        }
        #endregion

        #region DrawLineAndModules
        public List<Module> DrawModules()
        {
            List<Module> modPosition = new List<Module>();
            if (comboBox_type.Text == "所有类型")
            {
                switch (comboBox_level.Text)
                {
                    case "1":
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 1);
                        break;
                    case "2":
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 2);
                        break;
                    case "3":
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 3);
                        break;
                    default:
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 3);
                        break;
                }
            }
            else
            {
                switch (comboBox_level.Text)
                {
                    case "1":
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 1, comboBox_type.Text);
                        break;
                    case "2":
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 2, comboBox_type.Text);
                        break;
                    case "3":
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 3, comboBox_type.Text);
                        break;
                    default:
                        modPosition = ModuleLayout.ModulePosition(this.panel4.Width, this.panel4.Height, 3,comboBox_type.Text);
                        break;
                }
            }
            int NumCount = modPosition.Count;
            MyButton[] btn = new MyButton[NumCount];

            for (int i = 2; i < NumCount; i++)
            {
                btn[i] = new MyButton();
                btn[i].BackColor = ModuleColor.Color;
                btn[i].Font = fontDialog1.Font;
                btn[i].Size = new System.Drawing.Size(modPosition[0].x, modPosition[0].y);
                btn[i].Location = new Point(modPosition[i].x, modPosition[i].y);
                btn[i].Text = modPosition[i].moduleName;//显示文字
                btn[i].FlatStyle = FlatStyle.Flat;
                btn[i].FlatAppearance.BorderColor = BorderColor.Color;
                panel4.Controls.Add(btn[i]);
                btn[i].MouseDown += new MouseEventHandler(this.MyButton_RightMouseDown);//右击事件
                btn[i].Click += new EventHandler(this.MyButton_Click);//单击事件
                btn[i].ContextMenuStrip = this.contextMenuStrip_RightKey;//添加右击菜单列表
                btn[i].DoubleClick += new EventHandler(this.MyButton_DoubleClick);//双击事件
            }
            return modPosition;
        }
         
        
        public void DrawModuleAndLines()
        {
            List<Module> modPosition = DrawModules();
            if (modPosition.Count == 2)
            {
                return;
            }
            List<ModuleOne.LineInfo> line = new List<ModuleOne.LineInfo>();
            if (comboBox_type.Text == "所有类型")
            {
                switch (comboBox_level.Text)
                {
                    case "1":
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 1);
                        break;
                    case "2":
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 2);
                        break;
                    case "3":
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 3);
                        break;
                    default:
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 3);
                        break;
                }
            }
            else
            {
                switch (comboBox_level.Text)
                {
                    case "1":
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 1, comboBox_type.Text);
                        break;
                    case "2":
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 2, comboBox_type.Text);
                        break;
                    case "3":
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 3, comboBox_type.Text);
                        break;
                    default:
                        line = ModuleOne.GetLineInfo(modPosition, this.panel4.Width, this.panel4.Height, 3, comboBox_type.Text);
                        break;
                }
            }
           
            int LineCount = line.Count;
            if (LineCount == 0)
                return;
            //对所有线段坐标重新排列，从上到下或者从左到右
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i].line[0] > line[i].line[2] || line[i].line[1] > line[i].line[3])
                {
                    int x = line[i].line[0];
                    int y = line[i].line[1];
                    line[i].line[0] = line[i].line[2];
                    line[i].line[1] = line[i].line[3];
                    line[i].line[2] = x;
                    line[i].line[3] = y;
                }
            }
            ALine[] aline = new ALine[LineCount];
            for (int i = 0; i < LineCount; i++)
            {
                aline[i] = new ALine();
                aline[i].Points = line[i].line;
                aline[i].Penwidth = penWidth;
                aline[i].Pencolor = LineColor.Color;
                aline[i].Name = line[i].lineComment;//name 记录线段描述
                if (line[i].line[0] == line[i].line[2])
                {
                    aline[i].Location = new Point(line[i].line[0] - 4 * aline[i].Penwidth, line[i].line[1]);
                }
                else
                    aline[i].Location = new Point(line[i].line[0], line[i].line[1] - 4 * aline[i].Penwidth);
                aline[i].Text = line[i].lineName;//text 记录线段名字
                aline[i].MouseHover += new EventHandler(this.AlineHover);//悬浮事件
                aline[i].Click += new EventHandler(this.AlineClick);//单击事件
                aline[i].DoubleClick += new EventHandler(this.AlineDoubleClick);//双击事件
                panel4.Controls.Add(aline[i]);
            }
            //如果默认要显示关系名称则显示。
            if (line[0].show == 1)
            {
                WriteLabel(aline[0]);
            }
            for (int i = 1; i < LineCount; i++)
            {
                if (line[i].show==1 && line[i].lineName != line[i - 1].lineName)
                {
                    WriteLabel(aline[i]);
                }
            }
        }
        #endregion
        
        #region ModuleUIOperations
        //双击模块，恢复默认样式，并显示系统详细信息
        private void MyButton_DoubleClick(object sender, EventArgs e)
        {
            //恢复模块原有样式
            Control.ControlCollection Cons = this.panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is MyButton)
                {
                    ((MyButton)con).FlatAppearance.BorderSize = 1;
                    ((MyButton)con).BackColor = this.ModuleColor.Color;
                }
            }
            //恢复关系线原有样式
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    ((ALine)con).Pencolor = this.LineColor.Color;
                }
            }
            ModuleInfo moduleinfo = new ModuleInfo((MyButton)sender);
            moduleinfo.ShowDialog();
        }
        //单击按钮
        private void MyButton_Click(object sender, EventArgs e)
        {
            //使左侧模块的datagridview选中
            string seleteName = ((MyButton)sender).Text;
            int index = 0;
            for (int i = 0; i < dataGridView_module.RowCount; i++)//遍历所有选中的行
            {
                if (dataGridView_module.Rows[i].Cells[0].Value.Equals(seleteName))
                {
                    index = i;
                    break;
                }
            }
            dataGridView_module.CurrentCell = dataGridView_module.Rows[index].Cells[0];

            //并且使它和与它相连的关系线高亮显示
            List<MyButton> btnList = new List<MyButton>();
            List<ALine> alineList = new List<ALine>();
            Control.ControlCollection Cons = panel4.Controls;
            MyButton select = (MyButton)sender;
            //每个模块恢复原样
            foreach (Control con in Cons)
            {
                if (con is MyButton)
                {
                    btnList.Add((MyButton)con);
                    ((MyButton)con).FlatAppearance.BorderSize = 0;
                    ((MyButton)con).BackColor = Color.WhiteSmoke;
                }
            }
            //每个关系线恢复原样
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    alineList.Add((ALine)con);
                    ((ALine)con).Pencolor = Color.WhiteSmoke;
                }
            }
            //关系线与模块相连则变红色
            foreach (ALine aline in alineList)
            {
                if (
                        ((aline.Location.X + aline.Size.Width == select.Location.X + 1) && (aline.Location.Y + aline.Size.Height > select.Location.Y) && (aline.Location.Y < select.Location.Y + select.Size.Height)) ||
                        ((aline.Location.Y + aline.Size.Height == select.Location.Y + 1) && (aline.Location.X + aline.Size.Width > select.Location.X) && (aline.Location.X < select.Location.X + select.Size.Width)) ||
                        ((aline.Location.X + 1 == select.Location.X + select.Size.Width) && (aline.Location.Y + aline.Size.Height > select.Location.Y) && (aline.Location.Y < select.Location.Y + select.Size.Height)) ||
                        ((aline.Location.Y + 1 == select.Location.Y + select.Size.Height) && (aline.Location.X + aline.Size.Width > select.Location.X) && (aline.Location.X < select.Location.X + select.Size.Width))
                        )
                {
                    aline.Pencolor = Color.Red;
                    foreach (ALine alineSame in alineList)
                    {
                        if (alineSame.Text.Equals(aline.Text))
                        {
                            alineSame.Pencolor = Color.Red;
                        }
                    }
                }
            }
            //选中的这模块高亮显示
            select.FlatAppearance.BorderSize = 3;
            select.BackColor = Color.LightSkyBlue;
        }
        #endregion

        #region LineUIOperations

        private void WriteLabel(ALine aline)
        {
            if (aline.InvokeRequired)
            {
                myDelegate myde = new myDelegate(WriteLabel);
                this.BeginInvoke(myde, aline);
            }
            else
            {
                List<ALine> singleline = new List<ALine>();
                Control.ControlCollection Cons = panel4.Controls;
                ALine longline = new ALine();
                longline.Points = new int[] { 0, 0, 0, 0, 0 };
                foreach (Control con in Cons)
                {
                    if (con is ALine)
                    {
                        if ((con.Text).Equals(aline.Text))
                        {
                            singleline.Add((ALine)con);
                        }
                    }
                }
                //找到这条关系中最长的线
                foreach (ALine line in singleline)
                {
                    int dertaX = Math.Abs(line.Points[0] - line.Points[2]);
                    int dertaY = Math.Abs(line.Points[1] - line.Points[3]);
                    int longderta = Math.Max(Math.Abs(longline.Points[0] - longline.Points[2]), Math.Abs(longline.Points[1] - longline.Points[3]));
                    if (Math.Max(dertaX, dertaY) > longderta)
                        longline = line;
                }
                int labelX;
                int labelY;
                Label label_longline = new Label();

                label_longline.BackColor = Color.Transparent;
                label_longline.AutoSize = false;
                label_longline.Text = aline.Name;//记录关系描述信息
                int fontsize = (int)label_longline.Font.Size;
                int width, height;
                //判断最长的线是横着还是竖着的，横竖size和location不一样
                if (longline.Points[0] == longline.Points[2])//竖线
                {
                    width = aline.Name.Length * (fontsize + 5);
                    height = fontsize + 5;
                    label_longline.Size = new Size(height, width);
                    labelX = longline.Location.X + 10;
                    labelY = longline.Location.Y + (int)(longline.Size.Height / 2) - width / 2;
                }
                else//横线
                {
                    width = aline.Name.Length * (fontsize + 5);
                    height = fontsize + 5;
                    labelX = longline.Location.X + (int)(longline.Size.Width / 2) - width / 2;
                    labelY = longline.Location.Y - 10;
                    label_longline.Size = new Size(width, height);
                }
                label_longline.Location = new Point(labelX, labelY);
                LabelTransparent labelTransparent = new LabelTransparent(label_longline);
                //labelTransparent.DoubleClick += new EventHandler(this.LabelTransp_RightMouseDown);
                labelTransparent.MouseDown += new MouseEventHandler(this.LabelTransp_MouseDown);
                labelTransparent.MouseUp += new MouseEventHandler(this.LabelTransp_MouseUp);
                foreach (Control con in Cons)
                {
                    if (con is LabelTransparent)
                    {
                        if ((((LabelTransparent)con).Location) == labelTransparent.Location)
                        {
                            return;
                        }
                    }
                }
                panel4.Controls.Add(labelTransparent);
                labelTransparent.BringToFront();
            }
        }
        private Point mouse_offset;
        private void LabelTransp_RightClick(object sender, EventArgs e)
        {
            panel4.Controls.Remove((LabelTransparent)sender);
        }
        private void LabelTransp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                panel4.Controls.Remove((LabelTransparent)sender);
            }
            else
            {
                mouse_offset = new Point(-e.X, -e.Y);
            }
        }
        private void LabelTransp_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                ((LabelTransparent)sender).Location = ((LabelTransparent)sender).Parent.PointToClient(mousePos);
            }
        }

        //单击关系线
        private void AlineClick(object sender, EventArgs e)
        {
            string selectRelation = ((ALine)sender).Text;
            WriteLabel((ALine)sender);
            Control.ControlCollection Cons = panel4.Controls;
            //点击关系线高亮显示
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    if ((con.Text).Equals(selectRelation))
                    {
                        ((ALine)con).Pencolor = Color.Red;
                    }
                }
            }
            //在左侧datagridview_relation中选中
            int index=0;
            for (int i = 0; i < dataGridView_relation.RowCount ; i++)//遍历所有选中的行
            {
                if (dataGridView_relation.Rows[i].Cells[0].Value.Equals(selectRelation))
                {
                    index=i;
                    break;
                }
            }
            dataGridView_relation.CurrentCell=dataGridView_relation.Rows[index].Cells[0];
        }

        private void AlineDoubleClick(object sender, EventArgs e)
        {
            Control.ControlCollection Cons = panel4.Controls;
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    ((ALine)con).Pencolor = LineColor.Color;
                }
            }
            RelationInfo relationinfo = new RelationInfo((ALine)sender);
            relationinfo.ShowDialog();
        }
       
        public void AlineHover(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip((ALine)sender, ((ALine)sender).Text);
        }
        #endregion


        private void ToolStripMenuItem_Level1_Click(object sender, EventArgs e)
        {
            comboBox_level.SelectedIndex = 0;
        }
        //实现放大缩小窗口时自动调整图像大小
        private void Form_Changed(object sender, EventArgs e)
        {
            Control.ControlCollection Cons = panel4.Controls;
            if (globalParameters.dbPath == null || Cons.Count == 0)
            {
                return;
            }
            else
            {
                btn_generateMap_Click(sender, e);
            }
        }

        private void toolStripDropDownButton_lineWidth_Click(object sender, EventArgs e)
        {
            isSaved = false;
        }

        private void ToolStripMenuItem_Refresh_Click(object sender, EventArgs e)
        {
            btn_generateMap_Click(sender, e);
        }

        private void ToolStripMenuItem_level2_Click(object sender, EventArgs e)
        {
            comboBox_level.SelectedIndex = 1;
        }

        private void ToolStripMenuItem_Level3_Click(object sender, EventArgs e)
        {
            comboBox_level.SelectedIndex = 2;
        }

        private void trackBar_displaySacle_Scroll(object sender, EventArgs e)
        {
            int newPanelWidth = panelWidth;
            int newPanelHeight = panelHeight;
            switch (this.trackBar_displaySacle.Value)
            {
                case 0:
                    newPanelWidth = (int)(panelWidth * 0.7);
                    newPanelHeight = (int)(panelHeight * 0.7);
                    break;
                case 1:
                    newPanelWidth = panelWidth;
                    newPanelHeight = panelHeight;
                    break;
                case 2:
                    newPanelWidth = (int)(panelWidth * 1.2);
                    newPanelHeight = (int)(panelHeight * 1.2);
                    break;
                case 3:
                    newPanelWidth = (int)(panelWidth * 1.5);
                    newPanelHeight = (int)(panelHeight * 1.5);
                    break;
                case 4:
                    newPanelWidth = (int)(panelWidth * 2);
                    newPanelHeight = (int)(panelHeight * 2);
                    break;
                case 5:
                    newPanelWidth = (int)(panelWidth * 3);
                    newPanelHeight = (int)(panelHeight * 3);
                    break;
            }
            
            panel4.Size = new Size(newPanelWidth, newPanelHeight);
            btn_generateMap_Click(sender, e);
        }

        private void 倍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            磅ToolStripMenuItem3_Click(sender, e);
        }

        private void 倍ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            磅ToolStripMenuItem5_Click(sender, e);
        }

        private void 倍ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            磅ToolStripMenuItem6_Click(sender, e);
        }

        //点击左侧datagrid中的模块，使面板中对应模块高亮显示
        private void dataGridView_module_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (dataGridView_module.CurrentRow == null)
            {
                return;
            }
            string selectModule = dataGridView_module.CurrentCell.Value.ToString();
            Control.ControlCollection Cons = panel4.Controls;
            //恢复面板上所有之前选中模块
            foreach (Control con in Cons)
            {
                if (con is MyButton)
                {
                    //((MyButton)con).FlatAppearance.BorderColor = BorderColor.Color;
                    ((MyButton)con).FlatAppearance.BorderSize = 1;
                    ((MyButton)con).BackColor = ModuleColor.Color;
                }
            }
            //使选中cell的模块高亮显示
            foreach (Control con in Cons)
            {
                if (con is MyButton)
                {
                    if (((MyButton)con).Text == selectModule)
                    {
                        //((MyButton)con).FlatAppearance.BorderColor = Color.LightBlue;
                        ((MyButton)con).FlatAppearance.BorderSize = 3;
                        ((MyButton)con).BackColor = Color.LightYellow;
                    }
                }
            }
        }
        //点击左侧datagrid中的关系，使面板中对应关系线高亮显示
        private void dataGridView_relation_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (dataGridView_relation.CurrentRow==null)
            {
                return;
            }
            //string selectModule = dataGridView_module.CurrentCell.Value.ToString();
            string selectRelation = dataGridView_relation.CurrentCell.Value.ToString();
            Control.ControlCollection Cons = panel4.Controls;
            //恢复面板上所有之前选中关系线
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    ((ALine)con).Pencolor = LineColor.Color;
                }
            }
            //使选中cell的关系线高亮显示
            foreach (Control con in Cons)
            {
                if (con is ALine)
                {
                    if (((ALine)con).Text == selectRelation)
                    {
                        ((ALine)con).Pencolor = Color.Red;
                    }
                }
            }
        }
        //双击关系线名称，使其消失
        
        private void 添加业务流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamForm stream = new StreamForm(this);
            stream.ShowDialog();
            mainFormStatus();          
        }

        private void 类型配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configForm config = new configForm(this);
            config.ShowDialog();
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            bool isInCons = false;
            Control.ControlCollection Con = panel4.Controls;
            foreach (Control con in Con)
            {
                if(con.ClientRectangle.Contains(e.Location))
                {
                    isInCons = true;
                }
            }
            if (!isInCons)
            {
                foreach (Control con in Con)
                {
                    if (con is ALine)
                    {
                        ((ALine)con).Pencolor = LineColor.Color;
                    }
                    if (con is MyButton)
                    {
                        //((MyButton)con).FlatAppearance.BorderColor = BorderColor.Color;
                        ((MyButton)con).FlatAppearance.BorderSize = 1;
                        ((MyButton)con).BackColor = ModuleColor.Color;
                    }
                }
            }
        }
    }
}
