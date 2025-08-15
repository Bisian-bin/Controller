namespace Forms
{
    partial class FrmUser
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

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.userPanel = new PageMain.UserPanel();
            this.runningTimePanel = new PageMain.RunningTimePanel();
            this.infoPanel = new PageMain.InfoPanel();
            this.statePanel = new PageMain.StatePanel();
            this.frm_Bin1 = new PageMain.Frm_Bin();
            this.operatePanel = new PageMain.OperatePanel();
            this.runningTimeLine = new PageMain.RunningTimeLine();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.SuspendLayout();
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Location = new System.Drawing.Point(0, 0);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(0, 0);
            this.webView21.TabIndex = 0;
            this.webView21.ZoomFactor = 1D;
            // 
            // userPanel
            // 
            this.userPanel.BackColor = System.Drawing.Color.Transparent;
            this.userPanel.GTN = false;
            this.userPanel.Location = new System.Drawing.Point(1048, 11);
            this.userPanel.Margin = new System.Windows.Forms.Padding(4);
            this.userPanel.Name = "userPanel";
            this.userPanel.Size = new System.Drawing.Size(264, 183);
            this.userPanel.TabIndex = 7;
            this.userPanel.TCP = false;
            this.userPanel.User = "未登录";
            // 
            // runningTimePanel
            // 
            this.runningTimePanel.AlarmRate = 0F;
            this.runningTimePanel.BackColor = System.Drawing.Color.Transparent;
            this.runningTimePanel.EndTime = new System.DateTime(((long)(0)));
            this.runningTimePanel.Good = 0F;
            this.runningTimePanel.Location = new System.Drawing.Point(579, 477);
            this.runningTimePanel.Name = "runningTimePanel";
            this.runningTimePanel.Size = new System.Drawing.Size(463, 380);
            this.runningTimePanel.Sum = 0F;
            this.runningTimePanel.TabIndex = 3;
            this.runningTimePanel.UPH = 0F;
            this.runningTimePanel.WorkTime = System.TimeSpan.Parse("00:00:00");
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.Transparent;
            this.infoPanel.LoadCount = "1";
            this.infoPanel.Location = new System.Drawing.Point(618, -14);
            this.infoPanel.MtrId = "1";
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Number = "1";
            this.infoPanel.Op = "1";
            this.infoPanel.Order = "1";
            this.infoPanel.ProName = "1";
            this.infoPanel.SelectedIndex = -1;
            this.infoPanel.SelectedValue = null;
            this.infoPanel.Size = new System.Drawing.Size(463, 485);
            this.infoPanel.TabIndex = 2;
            this.infoPanel.Type = "1";
            this.infoPanel.SelectedIndexChanged += new System.EventHandler(this.infoPanel_SelectedIndexChanged);
            // 
            // statePanel
            // 
            this.statePanel.BackColor = System.Drawing.Color.Transparent;
            this.statePanel.Info = "系统不支持";
            this.statePanel.InfoState = PageMain.MainState.InfoLevel.信息;
            this.statePanel.Location = new System.Drawing.Point(10, 11);
            this.statePanel.Name = "statePanel";
            this.statePanel.Size = new System.Drawing.Size(476, 829);
            this.statePanel.TabIndex = 1;
            this.statePanel.OnLoginButtonClicked += new System.EventHandler(this.statePanel_OnLoginButtonClicked);
            // 
            // frm_Bin1
            // 
            this.frm_Bin1.BackColor = System.Drawing.Color.Transparent;
            this.frm_Bin1.BinName = "Tray";
            this.frm_Bin1.Count = "100";
            this.frm_Bin1.DataSource = null;
            this.frm_Bin1.Location = new System.Drawing.Point(1048, 200);
            this.frm_Bin1.Margin = new System.Windows.Forms.Padding(2);
            this.frm_Bin1.Name = "frm_Bin1";
            this.frm_Bin1.Ratio = "99.5%";
            this.frm_Bin1.Size = new System.Drawing.Size(586, 648);
            this.frm_Bin1.StatusColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(159)))), ((int)(((byte)(43)))));
            this.frm_Bin1.TabIndex = 10;
            // 
            // operatePanel
            // 
            this.operatePanel.BackColor = System.Drawing.Color.Transparent;
            this.operatePanel.Location = new System.Drawing.Point(1319, 2);
            this.operatePanel.Margin = new System.Windows.Forms.Padding(4);
            this.operatePanel.Name = "operatePanel";
            this.operatePanel.Operation = PageMain.OperatePanel.OperationEnum.单下料;
            this.operatePanel.Size = new System.Drawing.Size(315, 192);
            this.operatePanel.TabIndex = 8;
            // 
            // runningTimeLine
            // 
            this.runningTimeLine.BackColor = System.Drawing.Color.Transparent;
            this.runningTimeLine.Location = new System.Drawing.Point(1655, 2);
            this.runningTimeLine.Name = "runningTimeLine";
            this.runningTimeLine.Size = new System.Drawing.Size(253, 855);
            this.runningTimeLine.TabIndex = 9;
            // 
            // FrmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(230)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1920, 900);
            this.Controls.Add(this.frm_Bin1);
            this.Controls.Add(this.runningTimeLine);
            this.Controls.Add(this.operatePanel);
            this.Controls.Add(this.userPanel);
            this.Controls.Add(this.runningTimePanel);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.statePanel);
            this.Controls.Add(this.webView21);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUser";
            this.Load += new System.EventHandler(this.FrmUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PageMain.InfoPanel infoPanel;
        private PageMain.RunningTimePanel runningTimePanel;
        private PageMain.UserPanel userPanel;
        private PageMain.StatePanel statePanel;
        private PageMain.Frm_Bin frm_Bin1;
        private PageMain.OperatePanel operatePanel;
        private PageMain.RunningTimeLine runningTimeLine;
    }
}
