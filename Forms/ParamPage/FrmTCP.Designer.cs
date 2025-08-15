
namespace Forms
{
    partial class FrmTCP
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
            this.frm_Tcp1 = new UserDemo.Frm_Tcp.Frm_Tcp();
            this.SuspendLayout();
            // 
            // frm_Tcp1
            // 
            this.frm_Tcp1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(243)))));
            this.frm_Tcp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_Tcp1.Location = new System.Drawing.Point(0, 0);
            this.frm_Tcp1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.frm_Tcp1.Name = "frm_Tcp1";
            this.frm_Tcp1.Size = new System.Drawing.Size(1864, 880);
            this.frm_Tcp1.TabIndex = 0;
            // 
            // FrmTCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1864, 880);
            this.Controls.Add(this.frm_Tcp1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmTCP";
            this.Text = "FrmTCP";
            this.ResumeLayout(false);

        }

        #endregion

        private UserDemo.Frm_Tcp.Frm_Tcp frm_Tcp1;
    }
}