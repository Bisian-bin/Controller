
namespace Forms
{
    partial class frmAddUser
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
            this.frm_AddUser1 = new UserDemo.Frm_Manage.Frm_AddUser();
            this.SuspendLayout();
            // 
            // frm_AddUser1
            // 
            this.frm_AddUser1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(243)))));
            this.frm_AddUser1.Location = new System.Drawing.Point(-6, -20);
            this.frm_AddUser1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.frm_AddUser1.Name = "frm_AddUser1";
            this.frm_AddUser1.Size = new System.Drawing.Size(1289, 721);
            this.frm_AddUser1.TabIndex = 4;
            this.frm_AddUser1.UserDropdownSelectedValue = null;
            this.frm_AddUser1.UserName = "";
            this.frm_AddUser1.UserPassWord = "";
            this.frm_AddUser1.UserStatus = true;
            // 
            // frmAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1276, 680);
            this.Controls.Add(this.frm_AddUser1);
            this.Name = "frmAddUser";
            this.Text = "frmAddUser";
            this.ResumeLayout(false);

        }

        #endregion

        private UserDemo.Frm_Manage.Frm_AddUser frm_AddUser1;
    }
}