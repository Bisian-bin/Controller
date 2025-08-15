
namespace Forms
{
    partial class frmDebug
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDebug));
            this.listBoxAxis = new System.Windows.Forms.ListBox();
            this.picAxis = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvAxis = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnNlimit = new AntdUI.Button();
            this.btnPlimit = new AntdUI.Button();
            this.btnOrg = new AntdUI.Button();
            this.btnErr = new AntdUI.Button();
            this.button1 = new AntdUI.Button();
            this.EnableBtn = new AntdUI.Button();
            this.txtCurrentPos = new AntdUI.Input();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.input1 = new AntdUI.Input();
            this.StopBtn = new AntdUI.Button();
            this.HomeBtn = new AntdUI.Button();
            this.JogBtnSub = new AntdUI.Button();
            this.JogBtnAdd = new AntdUI.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.combVel = new System.Windows.Forms.ComboBox();
            this.Axislabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picAxis)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAxis)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxAxis
            // 
            this.listBoxAxis.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBoxAxis.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxAxis.FormattingEnabled = true;
            this.listBoxAxis.ItemHeight = 24;
            this.listBoxAxis.Location = new System.Drawing.Point(0, 0);
            this.listBoxAxis.Name = "listBoxAxis";
            this.listBoxAxis.Size = new System.Drawing.Size(431, 868);
            this.listBoxAxis.TabIndex = 14;
            this.listBoxAxis.SelectedIndexChanged += new System.EventHandler(this.listBoxAxis_SelectedIndexChanged);
            // 
            // picAxis
            // 
            this.picAxis.BackColor = System.Drawing.SystemColors.ControlLight;
            this.picAxis.Location = new System.Drawing.Point(965, 2);
            this.picAxis.Name = "picAxis";
            this.picAxis.Size = new System.Drawing.Size(955, 866);
            this.picAxis.TabIndex = 13;
            this.picAxis.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.groupBox1.Controls.Add(this.dgvAxis);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.Axislabel);
            this.groupBox1.Location = new System.Drawing.Point(431, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 868);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // dgvAxis
            // 
            this.dgvAxis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAxis.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAxis.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAxis.ColumnHeadersHeight = 30;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAxis.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAxis.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(231)))), ((int)(((byte)(221)))));
            this.dgvAxis.Location = new System.Drawing.Point(6, 251);
            this.dgvAxis.Name = "dgvAxis";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAxis.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAxis.RowHeadersWidth = 51;
            this.dgvAxis.RowTemplate.Height = 23;
            this.dgvAxis.Size = new System.Drawing.Size(516, 616);
            this.dgvAxis.TabIndex = 13;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnNlimit);
            this.groupBox3.Controls.Add(this.btnPlimit);
            this.groupBox3.Controls.Add(this.btnOrg);
            this.groupBox3.Controls.Add(this.btnErr);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.EnableBtn);
            this.groupBox3.Controls.Add(this.txtCurrentPos);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(6, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(516, 91);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "状态";
            // 
            // btnNlimit
            // 
            this.btnNlimit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnNlimit.DefaultBack = System.Drawing.SystemColors.ScrollBar;
            this.btnNlimit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNlimit.Location = new System.Drawing.Point(450, 56);
            this.btnNlimit.Name = "btnNlimit";
            this.btnNlimit.Shape = AntdUI.TShape.Circle;
            this.btnNlimit.Size = new System.Drawing.Size(45, 27);
            this.btnNlimit.TabIndex = 16;
            // 
            // btnPlimit
            // 
            this.btnPlimit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnPlimit.DefaultBack = System.Drawing.SystemColors.ScrollBar;
            this.btnPlimit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPlimit.Location = new System.Drawing.Point(323, 57);
            this.btnPlimit.Name = "btnPlimit";
            this.btnPlimit.Shape = AntdUI.TShape.Circle;
            this.btnPlimit.Size = new System.Drawing.Size(45, 27);
            this.btnPlimit.TabIndex = 17;
            // 
            // btnOrg
            // 
            this.btnOrg.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOrg.DefaultBack = System.Drawing.SystemColors.ScrollBar;
            this.btnOrg.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOrg.Location = new System.Drawing.Point(196, 57);
            this.btnOrg.Name = "btnOrg";
            this.btnOrg.Shape = AntdUI.TShape.Circle;
            this.btnOrg.Size = new System.Drawing.Size(45, 27);
            this.btnOrg.TabIndex = 15;
            // 
            // btnErr
            // 
            this.btnErr.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnErr.DefaultBack = System.Drawing.SystemColors.ScrollBar;
            this.btnErr.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnErr.Location = new System.Drawing.Point(69, 57);
            this.btnErr.Name = "btnErr";
            this.btnErr.Shape = AntdUI.TShape.Circle;
            this.btnErr.Size = new System.Drawing.Size(45, 27);
            this.btnErr.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(154)))), ((int)(((byte)(245)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(211)))), ((int)(((byte)(239)))));
            this.button1.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Icon = ((System.Drawing.Image)(resources.GetObject("button1.Icon")));
            this.button1.IconGap = 0.15F;
            this.button1.IconPosition = AntdUI.TAlignMini.Right;
            this.button1.IconSize = new System.Drawing.Size(25, 25);
            this.button1.Location = new System.Drawing.Point(399, 16);
            this.button1.Name = "button1";
            this.button1.OriginalBackColor = System.Drawing.SystemColors.HighlightText;
            this.button1.Size = new System.Drawing.Size(107, 36);
            this.button1.TabIndex = 24;
            this.button1.Text = "清除";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EnableBtn
            // 
            this.EnableBtn.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(154)))), ((int)(((byte)(245)))));
            this.EnableBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.EnableBtn.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(211)))), ((int)(((byte)(239)))));
            this.EnableBtn.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.EnableBtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EnableBtn.Icon = ((System.Drawing.Image)(resources.GetObject("EnableBtn.Icon")));
            this.EnableBtn.IconGap = 0.15F;
            this.EnableBtn.IconPosition = AntdUI.TAlignMini.Right;
            this.EnableBtn.IconSize = new System.Drawing.Size(25, 25);
            this.EnableBtn.Location = new System.Drawing.Point(286, 16);
            this.EnableBtn.Name = "EnableBtn";
            this.EnableBtn.OriginalBackColor = System.Drawing.SystemColors.HighlightText;
            this.EnableBtn.Size = new System.Drawing.Size(107, 37);
            this.EnableBtn.TabIndex = 14;
            this.EnableBtn.Text = "使能";
            this.EnableBtn.Click += new System.EventHandler(this.EnableBtn_Click);
            // 
            // txtCurrentPos
            // 
            this.txtCurrentPos.BackColor = System.Drawing.Color.White;
            this.txtCurrentPos.BorderWidth = 2F;
            this.txtCurrentPos.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrentPos.Location = new System.Drawing.Point(121, 18);
            this.txtCurrentPos.Name = "txtCurrentPos";
            this.txtCurrentPos.PlaceholderText = "";
            this.txtCurrentPos.Size = new System.Drawing.Size(124, 31);
            this.txtCurrentPos.TabIndex = 23;
            this.txtCurrentPos.WaveSize = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(30, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 20;
            this.label1.Text = "Err:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(403, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 14);
            this.label9.TabIndex = 18;
            this.label9.Text = "Lmt-:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(274, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 16;
            this.label7.Text = "Lmt+:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(152, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 14);
            this.label6.TabIndex = 14;
            this.label6.Text = "Org:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(17, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "当前坐标(mm):";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.input1);
            this.groupBox2.Controls.Add(this.StopBtn);
            this.groupBox2.Controls.Add(this.HomeBtn);
            this.groupBox2.Controls.Add(this.JogBtnSub);
            this.groupBox2.Controls.Add(this.JogBtnAdd);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.combVel);
            this.groupBox2.Location = new System.Drawing.Point(6, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(516, 95);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MOVE";
            // 
            // input1
            // 
            this.input1.BackColor = System.Drawing.Color.White;
            this.input1.BorderWidth = 2F;
            this.input1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.input1.Location = new System.Drawing.Point(121, 55);
            this.input1.Name = "input1";
            this.input1.PlaceholderText = "";
            this.input1.Size = new System.Drawing.Size(124, 31);
            this.input1.TabIndex = 24;
            this.input1.WaveSize = 0;
            // 
            // StopBtn
            // 
            this.StopBtn.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(154)))), ((int)(((byte)(245)))));
            this.StopBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.StopBtn.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(211)))), ((int)(((byte)(239)))));
            this.StopBtn.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.StopBtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StopBtn.Icon = ((System.Drawing.Image)(resources.GetObject("StopBtn.Icon")));
            this.StopBtn.IconSize = new System.Drawing.Size(25, 25);
            this.StopBtn.Location = new System.Drawing.Point(400, 53);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.OriginalBackColor = System.Drawing.SystemColors.HighlightText;
            this.StopBtn.Shape = AntdUI.TShape.Round;
            this.StopBtn.Size = new System.Drawing.Size(107, 38);
            this.StopBtn.TabIndex = 25;
            this.StopBtn.Text = "Stop";
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // HomeBtn
            // 
            this.HomeBtn.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(154)))), ((int)(((byte)(245)))));
            this.HomeBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.HomeBtn.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(211)))), ((int)(((byte)(239)))));
            this.HomeBtn.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.HomeBtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HomeBtn.Icon = ((System.Drawing.Image)(resources.GetObject("HomeBtn.Icon")));
            this.HomeBtn.IconSize = new System.Drawing.Size(25, 25);
            this.HomeBtn.Location = new System.Drawing.Point(286, 53);
            this.HomeBtn.Name = "HomeBtn";
            this.HomeBtn.OriginalBackColor = System.Drawing.SystemColors.HighlightText;
            this.HomeBtn.Shape = AntdUI.TShape.Round;
            this.HomeBtn.Size = new System.Drawing.Size(107, 38);
            this.HomeBtn.TabIndex = 16;
            this.HomeBtn.Text = "Home";
            this.HomeBtn.Click += new System.EventHandler(this.HomeBtn_Click);
            // 
            // JogBtnSub
            // 
            this.JogBtnSub.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(154)))), ((int)(((byte)(245)))));
            this.JogBtnSub.Cursor = System.Windows.Forms.Cursors.Default;
            this.JogBtnSub.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(211)))), ((int)(((byte)(239)))));
            this.JogBtnSub.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.JogBtnSub.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.JogBtnSub.Icon = ((System.Drawing.Image)(resources.GetObject("JogBtnSub.Icon")));
            this.JogBtnSub.IconSize = new System.Drawing.Size(25, 25);
            this.JogBtnSub.Location = new System.Drawing.Point(399, 12);
            this.JogBtnSub.Name = "JogBtnSub";
            this.JogBtnSub.OriginalBackColor = System.Drawing.SystemColors.HighlightText;
            this.JogBtnSub.Shape = AntdUI.TShape.Round;
            this.JogBtnSub.Size = new System.Drawing.Size(108, 38);
            this.JogBtnSub.TabIndex = 16;
            this.JogBtnSub.Text = "JOG-";
            this.JogBtnSub.Click += new System.EventHandler(this.JogBtnSub_Click);
            // 
            // JogBtnAdd
            // 
            this.JogBtnAdd.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(154)))), ((int)(((byte)(245)))));
            this.JogBtnAdd.Cursor = System.Windows.Forms.Cursors.Default;
            this.JogBtnAdd.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(211)))), ((int)(((byte)(239)))));
            this.JogBtnAdd.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.JogBtnAdd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.JogBtnAdd.Icon = ((System.Drawing.Image)(resources.GetObject("JogBtnAdd.Icon")));
            this.JogBtnAdd.IconSize = new System.Drawing.Size(25, 25);
            this.JogBtnAdd.Location = new System.Drawing.Point(286, 12);
            this.JogBtnAdd.Name = "JogBtnAdd";
            this.JogBtnAdd.OriginalBackColor = System.Drawing.SystemColors.HighlightText;
            this.JogBtnAdd.Shape = AntdUI.TShape.Round;
            this.JogBtnAdd.Size = new System.Drawing.Size(107, 38);
            this.JogBtnAdd.TabIndex = 15;
            this.JogBtnAdd.Text = "JOG+";
            this.JogBtnAdd.Click += new System.EventHandler(this.JogBtnAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(44, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 12;
            this.label4.Text = "位置(mm):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(30, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "速度(mm/s):";
            // 
            // combVel
            // 
            this.combVel.BackColor = System.Drawing.Color.White;
            this.combVel.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combVel.FormattingEnabled = true;
            this.combVel.Items.AddRange(new object[] {
            "0.1",
            "0.2",
            "1",
            "5",
            "10",
            "20",
            "30",
            "40",
            "50"});
            this.combVel.Location = new System.Drawing.Point(121, 18);
            this.combVel.Name = "combVel";
            this.combVel.Size = new System.Drawing.Size(124, 27);
            this.combVel.TabIndex = 8;
            this.combVel.Text = "0.1";
            // 
            // Axislabel
            // 
            this.Axislabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(211)))), ((int)(((byte)(217)))));
            this.Axislabel.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Axislabel.Location = new System.Drawing.Point(6, 17);
            this.Axislabel.Name = "Axislabel";
            this.Axislabel.Size = new System.Drawing.Size(516, 28);
            this.Axislabel.TabIndex = 0;
            this.Axislabel.Text = "轴名称";
            this.Axislabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(230)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1914, 870);
            this.Controls.Add(this.listBoxAxis);
            this.Controls.Add(this.picAxis);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDebug";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmDebug_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picAxis)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAxis)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox listBoxAxis;
        private System.Windows.Forms.PictureBox picAxis;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView dgvAxis;
        private System.Windows.Forms.GroupBox groupBox3;
        private AntdUI.Button btnErr;
        private AntdUI.Button button1;
        private AntdUI.Button EnableBtn;
        private AntdUI.Input txtCurrentPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private AntdUI.Button StopBtn;
        private AntdUI.Button HomeBtn;
        private AntdUI.Button JogBtnSub;
        private AntdUI.Button JogBtnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combVel;
        private System.Windows.Forms.Label Axislabel;
        private AntdUI.Button btnOrg;
        private AntdUI.Input input1;
        private AntdUI.Button btnNlimit;
        private AntdUI.Button btnPlimit;
    }
}