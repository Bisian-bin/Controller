
namespace Forms
{
    partial class frmAssign
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
            this.frm_Assign1 = new UserDemo.Frm_Manage.Frm_Assign();
            this.SuspendLayout();
            // 
            // frm_Assign1
            // 
            this.frm_Assign1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(243)))));
            this.frm_Assign1.Location = new System.Drawing.Point(-9, -23);
            this.frm_Assign1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.frm_Assign1.Name = "frm_Assign1";
            this.frm_Assign1.RoleName = "";
            this.frm_Assign1.Size = new System.Drawing.Size(1289, 721);
            this.frm_Assign1.TabIndex = 4;
            this.frm_Assign1.UserStatus = true;
            // 
            // frmAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1271, 674);
            this.Controls.Add(this.frm_Assign1);
            this.Name = "frmAssign";
            this.Text = "frmAssign";
            this.ResumeLayout(false);

        }

        #endregion

        private UserDemo.Frm_Manage.Frm_Assign frm_Assign1;
    }
}