
namespace Forms
{
    partial class frmMap
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
            this.frm_Map1 = new UserDemo.Frm_Map.Frm_Map();
            this.SuspendLayout();
            // 
            // frm_Map1
            // 
            this.frm_Map1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(230)))), ((int)(((byte)(246)))));
            this.frm_Map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_Map1.Location = new System.Drawing.Point(0, 0);
            this.frm_Map1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.frm_Map1.Name = "frm_Map1";
            this.frm_Map1.Size = new System.Drawing.Size(1880, 976);
            this.frm_Map1.TabIndex = 0;
            // 
            // frmMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1880, 976);
            this.Controls.Add(this.frm_Map1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmMap";
            this.Text = "frmMap";
            this.ResumeLayout(false);

        }

        #endregion

        private UserDemo.Frm_Map.Frm_Map frm_Map1;
    }
}