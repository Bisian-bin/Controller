
namespace Forms
{
    partial class frmSecsGem
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
            this.qGroup1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkRunCheck = new AntdUI.Switch();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAlarmTrig = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnOPsetup = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnLotEnd = new System.Windows.Forms.Button();
            this.btnLotStart = new System.Windows.Forms.Button();
            this.btnLotIDSetup = new System.Windows.Forms.Button();
            this.btnUnloadStart = new System.Windows.Forms.Button();
            this.btnLoadStart = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.swStatus = new AntdUI.Switch();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLinkTime = new AntdUI.Input();
            this.label3 = new System.Windows.Forms.Label();
            this.chkLocal = new AntdUI.Checkbox();
            this.chkRemote = new AntdUI.Checkbox();
            this.btnClose = new AntdUI.Button();
            this.btnConnect = new AntdUI.Button();
            this.txtT8 = new AntdUI.Input();
            this.label1 = new System.Windows.Forms.Label();
            this.txtT7 = new AntdUI.Input();
            this.txtT6 = new AntdUI.Input();
            this.T7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtT5 = new AntdUI.Input();
            this.txtT3 = new AntdUI.Input();
            this.txtPort = new AntdUI.Input();
            this.txtIP = new AntdUI.Input();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.qGroup1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // qGroup1
            // 
            this.qGroup1.AllowDrop = true;
            this.qGroup1.Controls.Add(this.label8);
            this.qGroup1.Controls.Add(this.chkRunCheck);
            this.qGroup1.Controls.Add(this.groupBox1);
            this.qGroup1.Controls.Add(this.label6);
            this.qGroup1.Controls.Add(this.swStatus);
            this.qGroup1.Controls.Add(this.label4);
            this.qGroup1.Controls.Add(this.txtLinkTime);
            this.qGroup1.Controls.Add(this.label3);
            this.qGroup1.Controls.Add(this.chkLocal);
            this.qGroup1.Controls.Add(this.chkRemote);
            this.qGroup1.Controls.Add(this.btnClose);
            this.qGroup1.Controls.Add(this.btnConnect);
            this.qGroup1.Controls.Add(this.txtT8);
            this.qGroup1.Controls.Add(this.label1);
            this.qGroup1.Controls.Add(this.txtT7);
            this.qGroup1.Controls.Add(this.txtT6);
            this.qGroup1.Controls.Add(this.T7);
            this.qGroup1.Controls.Add(this.label2);
            this.qGroup1.Controls.Add(this.txtT5);
            this.qGroup1.Controls.Add(this.txtT3);
            this.qGroup1.Controls.Add(this.txtPort);
            this.qGroup1.Controls.Add(this.txtIP);
            this.qGroup1.Controls.Add(this.label11);
            this.qGroup1.Controls.Add(this.label9);
            this.qGroup1.Controls.Add(this.label7);
            this.qGroup1.Controls.Add(this.label5);
            this.qGroup1.Dock = System.Windows.Forms.DockStyle.Left;
            this.qGroup1.Location = new System.Drawing.Point(0, 0);
            this.qGroup1.Name = "qGroup1";
            this.qGroup1.Padding = new System.Windows.Forms.Padding(1);
            this.qGroup1.Size = new System.Drawing.Size(311, 931);
            this.qGroup1.TabIndex = 1;
            this.qGroup1.TabStop = false;
            this.qGroup1.Text = "连接";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(-5, 414);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 22);
            this.label8.TabIndex = 206;
            this.label8.Text = "RunCheck：";
            // 
            // chkRunCheck
            // 
            this.chkRunCheck.Checked = true;
            this.chkRunCheck.CheckedText = "开启";
            this.chkRunCheck.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkRunCheck.Location = new System.Drawing.Point(99, 407);
            this.chkRunCheck.Name = "chkRunCheck";
            this.chkRunCheck.Size = new System.Drawing.Size(90, 34);
            this.chkRunCheck.TabIndex = 205;
            this.chkRunCheck.UnCheckedText = "关闭";
            this.chkRunCheck.CheckedChanged += new AntdUI.BoolEventHandler(this.chkRunCheck_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnAlarmTrig);
            this.groupBox1.Controls.Add(this.btnContinue);
            this.groupBox1.Controls.Add(this.btnPause);
            this.groupBox1.Controls.Add(this.btnOPsetup);
            this.groupBox1.Controls.Add(this.btnAbort);
            this.groupBox1.Controls.Add(this.btnLotEnd);
            this.groupBox1.Controls.Add(this.btnLotStart);
            this.groupBox1.Controls.Add(this.btnLotIDSetup);
            this.groupBox1.Controls.Add(this.btnUnloadStart);
            this.groupBox1.Controls.Add(this.btnLoadStart);
            this.groupBox1.Location = new System.Drawing.Point(9, 538);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(298, 383);
            this.groupBox1.TabIndex = 204;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模拟事件调用";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Blue;
            this.btnCancel.Location = new System.Drawing.Point(8, 253);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 36);
            this.btnCancel.TabIndex = 215;
            this.btnCancel.Text = "消除报警";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAlarmTrig
            // 
            this.btnAlarmTrig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAlarmTrig.ForeColor = System.Drawing.Color.Blue;
            this.btnAlarmTrig.Location = new System.Drawing.Point(8, 199);
            this.btnAlarmTrig.Name = "btnAlarmTrig";
            this.btnAlarmTrig.Size = new System.Drawing.Size(105, 36);
            this.btnAlarmTrig.TabIndex = 214;
            this.btnAlarmTrig.Text = "触发报警";
            this.btnAlarmTrig.UseVisualStyleBackColor = true;
            this.btnAlarmTrig.Click += new System.EventHandler(this.btnAlarmTrig_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnContinue.ForeColor = System.Drawing.Color.Blue;
            this.btnContinue.Location = new System.Drawing.Point(218, 199);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(64, 36);
            this.btnContinue.TabIndex = 213;
            this.btnContinue.Text = "继续";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPause.ForeColor = System.Drawing.Color.Blue;
            this.btnPause.Location = new System.Drawing.Point(147, 199);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(64, 36);
            this.btnPause.TabIndex = 212;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnOPsetup
            // 
            this.btnOPsetup.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOPsetup.ForeColor = System.Drawing.Color.Blue;
            this.btnOPsetup.Location = new System.Drawing.Point(148, 144);
            this.btnOPsetup.Name = "btnOPsetup";
            this.btnOPsetup.Size = new System.Drawing.Size(134, 36);
            this.btnOPsetup.TabIndex = 210;
            this.btnOPsetup.Text = "档案下发完成";
            this.btnOPsetup.UseVisualStyleBackColor = true;
            this.btnOPsetup.Click += new System.EventHandler(this.btnOPsetup_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAbort.ForeColor = System.Drawing.Color.Blue;
            this.btnAbort.Location = new System.Drawing.Point(8, 144);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(105, 36);
            this.btnAbort.TabIndex = 209;
            this.btnAbort.Text = "终止批次";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnLotEnd
            // 
            this.btnLotEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLotEnd.ForeColor = System.Drawing.Color.Blue;
            this.btnLotEnd.Location = new System.Drawing.Point(218, 92);
            this.btnLotEnd.Name = "btnLotEnd";
            this.btnLotEnd.Size = new System.Drawing.Size(64, 36);
            this.btnLotEnd.TabIndex = 208;
            this.btnLotEnd.Text = "结批";
            this.btnLotEnd.UseVisualStyleBackColor = true;
            this.btnLotEnd.Click += new System.EventHandler(this.btnLotEnd_Click);
            // 
            // btnLotStart
            // 
            this.btnLotStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLotStart.ForeColor = System.Drawing.Color.Blue;
            this.btnLotStart.Location = new System.Drawing.Point(147, 92);
            this.btnLotStart.Name = "btnLotStart";
            this.btnLotStart.Size = new System.Drawing.Size(64, 36);
            this.btnLotStart.TabIndex = 207;
            this.btnLotStart.Text = "开批";
            this.btnLotStart.UseVisualStyleBackColor = true;
            this.btnLotStart.Click += new System.EventHandler(this.btnLotStart_Click);
            // 
            // btnLotIDSetup
            // 
            this.btnLotIDSetup.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLotIDSetup.ForeColor = System.Drawing.Color.Blue;
            this.btnLotIDSetup.Location = new System.Drawing.Point(8, 92);
            this.btnLotIDSetup.Name = "btnLotIDSetup";
            this.btnLotIDSetup.Size = new System.Drawing.Size(134, 36);
            this.btnLotIDSetup.TabIndex = 206;
            this.btnLotIDSetup.Text = "批次设置完成";
            this.btnLotIDSetup.UseVisualStyleBackColor = true;
            this.btnLotIDSetup.Click += new System.EventHandler(this.btnLotIDSetup_Click);
            // 
            // btnUnloadStart
            // 
            this.btnUnloadStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnloadStart.ForeColor = System.Drawing.Color.Blue;
            this.btnUnloadStart.Location = new System.Drawing.Point(177, 40);
            this.btnUnloadStart.Name = "btnUnloadStart";
            this.btnUnloadStart.Size = new System.Drawing.Size(105, 36);
            this.btnUnloadStart.TabIndex = 205;
            this.btnUnloadStart.Text = "开始下料";
            this.btnUnloadStart.UseVisualStyleBackColor = true;
            this.btnUnloadStart.Click += new System.EventHandler(this.btnUnloadStart_Click);
            // 
            // btnLoadStart
            // 
            this.btnLoadStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadStart.ForeColor = System.Drawing.Color.Blue;
            this.btnLoadStart.Location = new System.Drawing.Point(8, 40);
            this.btnLoadStart.Name = "btnLoadStart";
            this.btnLoadStart.Size = new System.Drawing.Size(105, 36);
            this.btnLoadStart.TabIndex = 204;
            this.btnLoadStart.Text = "开始上料";
            this.btnLoadStart.UseVisualStyleBackColor = true;
            this.btnLoadStart.Click += new System.EventHandler(this.btnLoadStart_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(10, 370);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 22);
            this.label6.TabIndex = 203;
            this.label6.Text = "连接状态：";
            // 
            // swStatus
            // 
            this.swStatus.CheckedText = "在线";
            this.swStatus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.swStatus.Location = new System.Drawing.Point(98, 363);
            this.swStatus.Name = "swStatus";
            this.swStatus.Size = new System.Drawing.Size(90, 34);
            this.swStatus.TabIndex = 202;
            this.swStatus.UnCheckedText = "离线";
            this.swStatus.CheckedChanged += new AntdUI.BoolEventHandler(this.swStatus_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(10, 325);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 201;
            this.label4.Text = "通讯状态：";
            // 
            // txtLinkTime
            // 
            this.txtLinkTime.Location = new System.Drawing.Point(90, 282);
            this.txtLinkTime.Name = "txtLinkTime";
            this.txtLinkTime.Size = new System.Drawing.Size(201, 35);
            this.txtLinkTime.TabIndex = 200;
            this.txtLinkTime.Text = "3000";
            this.txtLinkTime.WaveSize = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(1, 290);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 22);
            this.label3.TabIndex = 199;
            this.label3.Text = "LinkTime：";
            // 
            // chkLocal
            // 
            this.chkLocal.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkLocal.Location = new System.Drawing.Point(179, 323);
            this.chkLocal.Margin = new System.Windows.Forms.Padding(2);
            this.chkLocal.Name = "chkLocal";
            this.chkLocal.Size = new System.Drawing.Size(64, 29);
            this.chkLocal.TabIndex = 198;
            this.chkLocal.Text = "Local";
            this.chkLocal.CheckedChanged += new AntdUI.BoolEventHandler(this.chkLocal_CheckedChanged);
            // 
            // chkRemote
            // 
            this.chkRemote.Checked = true;
            this.chkRemote.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkRemote.Location = new System.Drawing.Point(98, 323);
            this.chkRemote.Margin = new System.Windows.Forms.Padding(2);
            this.chkRemote.Name = "chkRemote";
            this.chkRemote.Size = new System.Drawing.Size(77, 29);
            this.chkRemote.TabIndex = 197;
            this.chkRemote.Text = "Remote";
            this.chkRemote.CheckedChanged += new AntdUI.BoolEventHandler(this.chkRemote_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(179, 459);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 41);
            this.btnClose.TabIndex = 196;
            this.btnClose.Text = "关闭连接";
            this.btnClose.Type = AntdUI.TTypeMini.Error;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConnect.Location = new System.Drawing.Point(10, 459);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 41);
            this.btnConnect.TabIndex = 195;
            this.btnConnect.Text = "建立连接";
            this.btnConnect.Type = AntdUI.TTypeMini.Success;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtT8
            // 
            this.txtT8.Location = new System.Drawing.Point(90, 247);
            this.txtT8.Name = "txtT8";
            this.txtT8.Size = new System.Drawing.Size(201, 35);
            this.txtT8.TabIndex = 194;
            this.txtT8.Text = "5";
            this.txtT8.WaveSize = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(52, 254);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 22);
            this.label1.TabIndex = 193;
            this.label1.Text = "T8：";
            // 
            // txtT7
            // 
            this.txtT7.Location = new System.Drawing.Point(90, 212);
            this.txtT7.Name = "txtT7";
            this.txtT7.Size = new System.Drawing.Size(201, 35);
            this.txtT7.TabIndex = 191;
            this.txtT7.Text = "10";
            this.txtT7.WaveSize = 1;
            // 
            // txtT6
            // 
            this.txtT6.Location = new System.Drawing.Point(90, 177);
            this.txtT6.Name = "txtT6";
            this.txtT6.Size = new System.Drawing.Size(201, 35);
            this.txtT6.TabIndex = 192;
            this.txtT6.Text = "5";
            this.txtT6.WaveSize = 1;
            // 
            // T7
            // 
            this.T7.AutoSize = true;
            this.T7.BackColor = System.Drawing.Color.Transparent;
            this.T7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.T7.Location = new System.Drawing.Point(52, 219);
            this.T7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.T7.Name = "T7";
            this.T7.Size = new System.Drawing.Size(46, 22);
            this.T7.TabIndex = 190;
            this.T7.Text = "T7：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(52, 184);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 22);
            this.label2.TabIndex = 189;
            this.label2.Text = "T6：";
            // 
            // txtT5
            // 
            this.txtT5.Location = new System.Drawing.Point(90, 142);
            this.txtT5.Name = "txtT5";
            this.txtT5.Size = new System.Drawing.Size(201, 35);
            this.txtT5.TabIndex = 187;
            this.txtT5.Text = "3";
            this.txtT5.WaveSize = 1;
            // 
            // txtT3
            // 
            this.txtT3.Location = new System.Drawing.Point(90, 106);
            this.txtT3.Name = "txtT3";
            this.txtT3.Size = new System.Drawing.Size(201, 35);
            this.txtT3.TabIndex = 187;
            this.txtT3.Text = "5";
            this.txtT3.WaveSize = 1;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(90, 71);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(201, 35);
            this.txtPort.TabIndex = 187;
            this.txtPort.Text = "5555";
            this.txtPort.WaveSize = 1;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(90, 36);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(201, 35);
            this.txtIP.TabIndex = 187;
            this.txtIP.Text = "127.0.0.1";
            this.txtIP.WaveSize = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(52, 149);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 22);
            this.label11.TabIndex = 165;
            this.label11.Text = "T5：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(52, 114);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 22);
            this.label9.TabIndex = 163;
            this.label9.Text = "T3：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(0, 78);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 22);
            this.label7.TabIndex = 161;
            this.label7.Text = "LocalPort：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(16, 43);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 22);
            this.label5.TabIndex = 159;
            this.label5.Text = "LocalIP：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(311, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(1132, 931);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // frmSecsGem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1443, 931);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.qGroup1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSecsGem";
            this.Text = "frmSecsGem";
            this.Load += new System.EventHandler(this.frmSecsGem_Load);
            this.qGroup1.ResumeLayout(false);
            this.qGroup1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox qGroup1;
        private AntdUI.Input txtT5;
        private AntdUI.Input txtT3;
        private AntdUI.Input txtPort;
        private AntdUI.Input txtIP;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label5;
        private AntdUI.Input txtT7;
        private AntdUI.Input txtT6;
        internal System.Windows.Forms.Label T7;
        internal System.Windows.Forms.Label label2;
        private AntdUI.Input txtT8;
        internal System.Windows.Forms.Label label1;
        private AntdUI.Button btnConnect;
        private AntdUI.Button btnClose;
        private AntdUI.Checkbox chkRemote;
        private AntdUI.Checkbox chkLocal;
        private AntdUI.Input txtLinkTime;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label6;
        private AntdUI.Switch swStatus;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnLoadStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUnloadStart;
        private System.Windows.Forms.Button btnLotIDSetup;
        private System.Windows.Forms.Button btnLotStart;
        private System.Windows.Forms.Button btnLotEnd;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnOPsetup;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnAlarmTrig;
        private System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Label label8;
        private AntdUI.Switch chkRunCheck;
    }
}