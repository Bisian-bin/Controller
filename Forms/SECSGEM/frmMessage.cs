using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class frmMsg : Form
    {
        public frmMsg()
        {
            InitializeComponent();
        }

        public void Send(string str)
        {
            richTextBox5.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + str;
        }

        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {
            // 将光标位置设置到文本末尾
            richTextBox5.SelectionStart = richTextBox5.Text.Length;
            // 滚动到光标位置
            richTextBox5.ScrollToCaret();
        }
    }
}
