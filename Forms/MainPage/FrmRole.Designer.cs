
namespace Forms
{
    partial class FrmRole
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
            this.frm_RoleManage1 = new UserDemo.Frm_Manage.Frm_RoleManage();
            this.frm_UserManage1 = new UserDemo.Frm_Manage.Frm_UserManage();
            this.SuspendLayout();
            // 
            // frm_RoleManage1
            // 
            this.frm_RoleManage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(243)))));
            this.frm_RoleManage1.Location = new System.Drawing.Point(-1, -2);
            this.frm_RoleManage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.frm_RoleManage1.Name = "frm_RoleManage1";
            this.frm_RoleManage1.Size = new System.Drawing.Size(1920, 425);
            this.frm_RoleManage1.TabIndex = 2;
            // 
            // frm_UserManage1
            // 
            this.frm_UserManage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(243)))));
            this.frm_UserManage1.Location = new System.Drawing.Point(-1, 426);
            this.frm_UserManage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.frm_UserManage1.Name = "frm_UserManage1";
            this.frm_UserManage1.Size = new System.Drawing.Size(1920, 425);
            this.frm_UserManage1.TabIndex = 3;
            // 
            // FrmRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1443, 849);
            this.Controls.Add(this.frm_UserManage1);
            this.Controls.Add(this.frm_RoleManage1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmRole";
            this.ResumeLayout(false);

        }

        #endregion

        public UserDemo.Frm_Manage.Frm_RoleManage frm_RoleManage1;
        public UserDemo.Frm_Manage.Frm_UserManage frm_UserManage1;
    }
}