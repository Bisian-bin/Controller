using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Forms
{
    class DrawControl
    {
        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        //一般按钮
        public static Color mGradientColorT = Color.Blue;
        public static Color mGradientColorB = Color.Wheat;

        public static void OnBtn_MouseDown(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Button m_Btncmd = (System.Windows.Forms.Button)sender;
            DrawControl.PaintGradientBackground(m_Btncmd, Color.Blue, Color.White);
        }

        public static void PaintGradientBackground(System.Windows.Forms.Button btn, Color GradientColorT, Color GradientColorB)
        {
            if (btn.Image != null) btn.Image.Dispose();
            Bitmap newGradientBackImg = new Bitmap(btn.Width, btn.Height);
            LinearGradientBrush brush = new LinearGradientBrush(new PointF(1, 1), new PointF(1, btn.Height - 2), GradientColorT, GradientColorB);
            Graphics gr = Graphics.FromImage(newGradientBackImg);
            gr.FillRectangle(brush, new RectangleF(0, 0, btn.Width, btn.Height));
            Pen pen = new Pen(Color.DarkMagenta, 1);
            Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);
            gr.DrawRectangle(pen, rect);
            //btn.BackColor = Color.Transparent;
            btn.Image = newGradientBackImg;
            gr.Dispose();
            newGradientBackImg = null;
        }

        public static void SetGradientBtnMouseDownColor(Color GradientColorT, Color GradientColorB)
        {
            mGradientColorT = GradientColorT;
            mGradientColorB = GradientColorB;
        }

        public static void PaintGradientBtnMouseDown(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            if (btn.Image != null) btn.Image.Dispose();
            Bitmap newGradientBackImg = new Bitmap(btn.Width - 2, btn.Height - 2);
            LinearGradientBrush brush = new LinearGradientBrush(new PointF(1, 1), new PointF(1, btn.Height - 2), mGradientColorT, mGradientColorB);
            Graphics gr = Graphics.FromImage(newGradientBackImg);
            gr.FillRectangle(brush, new RectangleF(0, 0, btn.Width, btn.Height));
            Pen pen = new Pen(Color.DarkMagenta, 1);
            Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);
            gr.DrawRectangle(pen, rect);
            //btn.BackColor = Color.Transparent;
            btn.Image = newGradientBackImg;
            gr.Dispose();
            newGradientBackImg = null;
        }
        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        public static void PaintRadioBtnBackground(System.Windows.Forms.RadioButton btn, Color GradientColorT, Color GradientColorB)
        {
            if (btn.Image != null) btn.Image.Dispose();
            Bitmap newGradientBackImg = new Bitmap(btn.Width - 2, btn.Height - 2);
            LinearGradientBrush brush;
            if (btn.Checked == true)
            {
                btn.ForeColor = Color.White;
                brush = new LinearGradientBrush(new PointF(1, 1), new PointF(1, btn.Height - 2), GradientColorT, GradientColorB);
            }
            else
            {
                btn.ForeColor = Color.Cornsilk;
                brush = new LinearGradientBrush(new PointF(1, 1), new PointF(1, btn.Height - 2), GradientColorT, Color.DarkGray);
            }
            Graphics gr = Graphics.FromImage(newGradientBackImg);
            gr.FillRectangle(brush, new RectangleF(0, 0, btn.Width, btn.Height));
            Pen pen = new Pen(Color.DarkMagenta, 1);
            Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);
            gr.DrawRectangle(pen, rect);
            btn.Image = newGradientBackImg;
            gr.Dispose();
            newGradientBackImg = null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Check butonn"></param>
        /// <param name="GradientColorT"></param>
        /// <param name="GradientColorB"></param>
        /// 
        /// <summary>
        /// 选取颜色
        /// </summary>
        private static Color GradientColorC = Color.Blue;
        /// <summary>
        /// 未选取颜色
        /// </summary>
        private static Color GradientColorU = Color.Gray;
        public static void SetCheckBoxBackground(Color GradientColorT, Color GradientColorB)
        {
            GradientColorC = GradientColorT;
            GradientColorU = GradientColorB;
        }

        public static void OnCheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox btn = (System.Windows.Forms.CheckBox)sender;
            if (btn.Image != null) btn.Image.Dispose();
            Bitmap newGradientBackImg = new Bitmap(btn.Width - 2, btn.Height - 2);
            LinearGradientBrush brush;
            if (btn.Checked == true)
            {
                btn.ForeColor = Color.Red;
                brush = new LinearGradientBrush(new PointF(1, 1), new PointF(1, btn.Height - 2), GradientColorC, GradientColorU);
            }
            else
            {
                btn.ForeColor = Color.Cornsilk;
                brush = new LinearGradientBrush(new PointF(1, 1), new PointF(1, btn.Height - 2), GradientColorU, Color.DarkGray);
            }
            Graphics gr = Graphics.FromImage(newGradientBackImg);
            gr.FillRectangle(brush, new RectangleF(0, 0, btn.Width, btn.Height));
            Pen pen = new Pen(Color.DarkMagenta, 1);
            Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);
            gr.DrawRectangle(pen, rect);
            btn.Image = newGradientBackImg;
            gr.Dispose();
            newGradientBackImg = null;
        }

        public static void PaintCheckBtnBackground(System.Windows.Forms.CheckBox btn, Color GradientColorT, Color GradientColorB)
        {
            if (btn.Image != null) btn.Image.Dispose();
            Bitmap newGradientBackImg = new Bitmap(btn.Width - 2, btn.Height - 2);
            LinearGradientBrush brush;
            if (btn.Checked == true)
            {
                btn.ForeColor = Color.White;
                brush = new LinearGradientBrush(new PointF(1, 1), new PointF(1, btn.Height - 2), GradientColorT, GradientColorB);
            }
            else
            {
                btn.ForeColor = Color.Cornsilk;
                brush = new LinearGradientBrush(new PointF(1, 1), new PointF(1, btn.Height - 2), GradientColorT, Color.DarkGray);
            }
            Graphics gr = Graphics.FromImage(newGradientBackImg);
            gr.FillRectangle(brush, new RectangleF(0, 0, btn.Width, btn.Height));
            Pen pen = new Pen(Color.DarkMagenta, 1);
            Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);
            gr.DrawRectangle(pen, rect);
            btn.Image = newGradientBackImg;
            gr.Dispose();
            newGradientBackImg = null;
        }

        public static void PaintGroupBox(System.Windows.Forms.GroupBox grp, Color BorderColor, int PenSize)
        {
            Bitmap newBackImg = new Bitmap(grp.Width, grp.Height);
            Graphics gr = Graphics.FromImage(newBackImg);
            Pen pen = new Pen(BorderColor, PenSize);
            Rectangle rect = new Rectangle(1, PenSize / 2, grp.Width - PenSize, grp.Height - PenSize / 2);
            gr.DrawRectangle(pen, rect);
            //grp.BackColor = Color.Transparent;
            grp.BackgroundImage = newBackImg;
            gr.Dispose();
            newBackImg = null;
        }

        public static void DrawLabelOutLine(System.Windows.Forms.Label lbl, Color BorderColor, int PenSize, int Mode)
        {
            Bitmap newBackImg = new Bitmap(lbl.Width, lbl.Height);
            Graphics gr = Graphics.FromImage(newBackImg);
            Pen pen = new Pen(BorderColor, PenSize);
            Rectangle rect = new Rectangle(1, PenSize / 2, lbl.Width - PenSize, lbl.Height - PenSize);
            if (Mode == 0) gr.DrawRectangle(pen, rect);
            if (Mode == 1) gr.DrawEllipse(pen, rect);
            lbl.BackColor = Color.Transparent;
            lbl.BackgroundImage = newBackImg;
            gr.Dispose();
            newBackImg = null;
        }

        public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        public static void tabPage_DrawItem(object sender, DrawItemEventArgs e)
        {
            System.Windows.Forms.TabControl tab = (System.Windows.Forms.TabControl)sender;
            Brush[] BackBrush = new Brush[2] { Brushes.LightGray, Brushes.DarkGreen };
            //标签文字填充颜色
            Brush[] ForeBrush = new Brush[2] { Brushes.DarkGray, Brushes.White };
            StringFormat StringF = new StringFormat();
            //设置文字对齐方式
            StringF.Alignment = StringAlignment.Center;
            StringF.LineAlignment = StringAlignment.Center;

            for (int i = 0; i < tab.TabPages.Count; i++)
            {
                //获取标签头工作区域
                Rectangle Rec = tab.GetTabRect(i);
                //绘制标签头背景颜色
                e.Graphics.FillRectangle(BackBrush[tab.SelectedIndex == i ? 1 : 0], Rec);
                //绘制标签头文字
                e.Graphics.DrawString(tab.TabPages[i].Text, new Font("Arial", 12, FontStyle.Bold), ForeBrush[tab.SelectedIndex == i ? 1 : 0], Rec, StringF);
            }
        }
    }
}
