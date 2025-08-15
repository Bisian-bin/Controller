
namespace Forms
{
    partial class LoginPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_Login = new AntdUI.Button();
            this.lbl_Password = new AntdUI.Label();
            this.lbl_User = new AntdUI.Label();
            this.txt_Password = new AntdUI.Input();
            this.txt_User = new AntdUI.Input();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.SuspendLayout();
            // 
            // bt_Login
            // 
            this.bt_Login.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(156)))), ((int)(((byte)(184)))));
            this.bt_Login.BackExtend = "135, #83a4d4,#b6fbff";
            this.bt_Login.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(196)))), ((int)(((byte)(219)))));
            this.bt_Login.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_Login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(84)))), ((int)(((byte)(118)))));
            this.bt_Login.Location = new System.Drawing.Point(236, 744);
            this.bt_Login.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_Login.Name = "bt_Login";
            this.bt_Login.Size = new System.Drawing.Size(191, 80);
            this.bt_Login.TabIndex = 5;
            this.bt_Login.Text = "登录";
            this.bt_Login.Type = AntdUI.TTypeMini.Primary;
            // 
            // lbl_Password
            // 
            this.lbl_Password.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Password.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(43)))), ((int)(((byte)(101)))));
            this.lbl_Password.Location = new System.Drawing.Point(48, 525);
            this.lbl_Password.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(143, 71);
            this.lbl_Password.TabIndex = 8;
            this.lbl_Password.Text = "密码";
            this.lbl_Password.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lbl_User
            // 
            this.lbl_User.BackColor = System.Drawing.Color.Transparent;
            this.lbl_User.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_User.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(43)))), ((int)(((byte)(101)))));
            this.lbl_User.Location = new System.Drawing.Point(48, 298);
            this.lbl_User.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(143, 71);
            this.lbl_User.TabIndex = 9;
            this.lbl_User.Text = "用户名";
            this.lbl_User.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txt_Password
            // 
            this.txt_Password.BackExtend = "66,#BAD5F2,#D4E8FA";
            this.txt_Password.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(127)))), ((int)(((byte)(182)))));
            this.txt_Password.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(229)))));
            this.txt_Password.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(164)))), ((int)(((byte)(212)))));
            this.txt_Password.BorderWidth = 3F;
            this.txt_Password.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Password.Location = new System.Drawing.Point(48, 604);
            this.txt_Password.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(580, 65);
            this.txt_Password.TabIndex = 7;
            // 
            // txt_User
            // 
            this.txt_User.BackExtend = "66,#BAD5F2,#D4E8FA";
            this.txt_User.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(127)))), ((int)(((byte)(182)))));
            this.txt_User.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(229)))));
            this.txt_User.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(164)))), ((int)(((byte)(212)))));
            this.txt_User.BorderWidth = 3F;
            this.txt_User.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_User.Location = new System.Drawing.Point(48, 376);
            this.txt_User.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_User.Name = "txt_User";
            this.txt_User.Size = new System.Drawing.Size(580, 71);
            this.txt_User.TabIndex = 6;
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.Color.Transparent;
            this.pageHeader1.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(192)))), ((int)(((byte)(229)))));
            this.pageHeader1.DividerShow = true;
            this.pageHeader1.DividerThickness = 7F;
            this.pageHeader1.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageHeader1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pageHeader1.FullBox = true;
            this.pageHeader1.Location = new System.Drawing.Point(4, 18);
            this.pageHeader1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.Size = new System.Drawing.Size(661, 120);
            this.pageHeader1.SubText = "";
            this.pageHeader1.TabIndex = 10;
            this.pageHeader1.Text = "用户登录";
            this.pageHeader1.UseSubCenter = true;
            // 
            // LoginPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Forms.Properties.Resources.Venice;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.lbl_User);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.txt_User);
            this.Controls.Add(this.bt_Login);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LoginPanel";
            this.Size = new System.Drawing.Size(669, 972);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Button bt_Login;
        private AntdUI.Label lbl_Password;
        private AntdUI.Label lbl_User;
        private AntdUI.Input txt_Password;
        private AntdUI.Input txt_User;
        private AntdUI.PageHeader pageHeader1;
    }
}
