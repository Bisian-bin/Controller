using InnerEvents;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using QMBaseClass.板卡.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Forms
{
    public class IO_Form
    {
        private IEventAggregator eventAggregator;
        private IUnityContainer container;
        public static string PageName = "输入1";

        public IO_Form(IEventAggregator eventAggregator, IUnityContainer container)
        {
            this.eventAggregator = eventAggregator;
            this.container = container;

            eventAggregator.GetEvent<UserEvent>().Subscribe((o) =>
            {
                ChangePage(o);
            }, ThreadOption.BackgroundThread);
        }

        private List<QMIOModel> InputList = FlowFactory.AxisContorl.Axis.InputList;//所有输入  
        private List<QMIOModel> OutputList = FlowFactory.AxisContorl.Axis.OutputList;//所有输出
        Rectangle rect = new Rectangle(2, 2, 18, 18), fillRect = new Rectangle(0, 0, 20, 20);
        int nCurrPage = 0, tCurrPage = 0;
        int BitStY = 5, DrawPitch = 26;
        PictureBox[] m_Pbx = new PictureBox[8];
        public static int IOButtonCount = 0, OutButtonCount = 0;

        #region 生成按钮
        public void GenerateButtons(Form form1)
        {
            IOButtonCount = (InputList.Count) / (16 * 8) + ((InputList.Count) % (16 * 8) == 0 ? 0 : 1);//定义输入按钮的数量
            OutButtonCount = (OutputList.Count) / (16 * 8) + ((OutputList.Count) % (16 * 8) == 0 ? 0 : 1);//定义输入按钮的数量
            // 定义按钮之间的水平和垂直间距
            int verticalSpacing = 100;

            // 起始位置
            int startX = 40;
            int startY = 10;

            //for (int i = 0; i < IOButtonCount + OutButtonCount; i++)
            //{
            //    // 创建新的按钮实例
            //    Button button = new Button();
            //    if (i < IOButtonCount) { button.Text = $"输入{i + 1}"; button.Name = $"rdoDI{i + 1}"; } else { button.Text = $"输出 {i - IOButtonCount + 1}"; button.Name = $"rdoDO{i + 1}"; }
            //    button.Location = new System.Drawing.Point(startX + (i * (button.Height + verticalSpacing)), startY);
            //    button.Click += Button_Click;
            //    // 将按钮添加到窗体的Controls集合中
            //    form1.Controls.Add(button);
            //}
            //=============================================
            //初始化IO界面
            int mWt = (form1.Width - 50) / 4;
            int mHt = (form1.Height - 80) / 2 + 10;
            int mPitchX = mWt + 10;
            for (int i = 0; i < 8; i++)
            {
                m_Pbx[i] = (PictureBox)form1.Controls.Find("pbxIO" + (i + 1).ToString(), true)[0];
                m_Pbx[i].Location = new Point(i < 4 ? 40 + i * mPitchX : 40 + (i - 4) * mPitchX, i < 4 ? 35 : mHt + 45);
                m_Pbx[i].Size = new Size(mWt, mHt);
                m_Pbx[i].Paint += PaintIOStatus;
                m_Pbx[i].MouseDown += pbxIO_MouseDown;
            }
            Timer1();//刷新界面
        }

        private void ChangePage(object o)
        {
            nCurrPage = 0;
            if (o.ToString() == "输入1"|| o.ToString() == "Input 1")
            {
                nCurrPage = 0;
            }
            if (o.ToString() == "输入2" || o.ToString() == "Input 2")
            {
                nCurrPage = 1;
            }
            if (o.ToString() == "输出1" || o.ToString() == "Output 1")
            {
                nCurrPage = 2;
            }
            if (o.ToString() == "输出2" || o.ToString() == "Output 2")
            {
                nCurrPage = 3;
            }           
            if (tCurrPage != nCurrPage)
            {
                tCurrPage = nCurrPage;
                for (int i = 0; i < 8; i++)
                    m_Pbx[i].Invalidate();
            }
        }

        ///// <summary>
        ///// 按钮事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Button_Click(object sender, EventArgs e)
        //{
        //    Button mRdoBtn = (Button)sender;
        //    nCurrPage = int.Parse(System.Text.RegularExpressions.Regex.Replace(mRdoBtn.Name, @"[^0-9]+", "")) - 1;
        //    if (tCurrPage != nCurrPage)
        //    {
        //        tCurrPage = nCurrPage;
        //        for (int i = 0; i < 8; i++)
        //            m_Pbx[i].Invalidate();
        //    }
        //}
        #endregion

        #region 初始化界面

        private void PaintIOStatus(object sender, PaintEventArgs e)
        {
            PictureBox Pbx = (PictureBox)sender;
            int nPbxNo = int.Parse(System.Text.RegularExpressions.Regex.Replace(Pbx.Name, @"[^0-9]+", ""));
            Font mfont = new Font("Arial", 9, FontStyle.Bold);
            Point PtDraw = new Point();
            string szData;
            if (nCurrPage < IOButtonCount)
                szData = string.Format("Card{0:D2}_DI", (nPbxNo - 1) + (nCurrPage * 8));
            else if (nCurrPage > IOButtonCount + 1)
                szData = string.Format("Card{0:D2}_DO", (nPbxNo - 1) + ((nCurrPage - 5) * 8));
            else
                szData = string.Format("Card{0:D2}_DI", (nPbxNo - 1) + ((nCurrPage - 4) * 8));
            e.Graphics.DrawString(szData, mfont, Brushes.Blue, 400, 5);
            int CardNo = ((nCurrPage % 2) * 8) + (nPbxNo - 1);
            int BitNo;
            int AxisNo = 0 + (nCurrPage - IOButtonCount) * 16;
            GraphicsPath path;
            for (int i = 0; i < 16; i++)
            {
                PtDraw.X = 30;
                PtDraw.Y = BitStY + (i * DrawPitch);
                if (nCurrPage < IOButtonCount)
                {
                    BitNo = nCurrPage * 8 * 16 + (nPbxNo - 1) * 16 + i;
                    if (BitNo >= InputList.Count)
                    { break; }
                    szData = string.Format("DI_{0:D3}_", BitNo) + "\t   " + InputList[BitNo].IOName;
                    e.Graphics.DrawString(szData, mfont, i % 2 == 0 ? Brushes.Black : Brushes.BlueViolet, PtDraw.X, PtDraw.Y);
                    rect.X = 5;
                    rect.Y = PtDraw.Y;
                    if (InputList[BitNo].LabelAddress.Substring(0, 1) == "W")//(BitNo < 32)
                        e.Graphics.FillEllipse(InputList[BitNo].State.Val == true ? Brushes.Gray : Brushes.GreenYellow, rect);
                    else
                        e.Graphics.FillEllipse(InputList[BitNo].State.Val == true ? Brushes.GreenYellow : Brushes.Gray, rect);

                    e.Graphics.DrawEllipse(Pens.Blue, rect);
                }
                else
                {
                    BitNo = (nCurrPage - IOButtonCount) * 8 * 16 + (nPbxNo - 1) * 16 + i;
                    if (BitNo >= OutputList.Count)
                        break;
                    szData = string.Format("DO_{0:D3}_", BitNo) + "   " + OutputList[BitNo].IOName;
                    e.Graphics.DrawString(szData, mfont, i % 2 == 0 ? Brushes.Black : Brushes.BlueViolet, PtDraw.X, PtDraw.Y);
                    rect.X = 5;
                    rect.Y = PtDraw.Y;
                    fillRect.X = rect.X - 1;
                    fillRect.Y = rect.Y - 1;
                    fillRect.Width = rect.Width + 1;
                    fillRect.Height = rect.Height + 1;
                    e.Graphics.FillRectangle(OutputList[BitNo].State.Val == true ? Brushes.GreenYellow : Brushes.Gray, rect);
                    path = DrawControl.CreateRoundedRectanglePath(fillRect, 5);
                    e.Graphics.DrawPath(OutputList[BitNo].State.Val == true ? Pens.Red : Pens.Black, path);
                }
            }
        }


        private void pbxIO_MouseDown(object sender, MouseEventArgs e)
        {
            if (nCurrPage < IOButtonCount) return;
            PictureBox Pbx = (PictureBox)sender;
            if (e.Y < BitStY) return;
            int SelBitNo = (e.Y - 30) / DrawPitch;
            int nCurrCard = (int.Parse(System.Text.RegularExpressions.Regex.Replace(Pbx.Name, @"[^0-9]+", "")) - 1) + ((nCurrPage % IOButtonCount) * 8);
            try
            {
                int DO_No = nCurrCard * 16 + SelBitNo;
                FlowFactory.AxisContorl.OutAuto(DO_No, OutputList[DO_No].State.Val == true ? false : true);
                Pbx.Invalidate();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        private void Timer1()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        for (int nPbxNo = 0; nPbxNo < m_Pbx.Length; nPbxNo++) { m_Pbx[nPbxNo].Invalidate(); }
                        await Task.Delay(100);
                    }
                    catch
                    {

                    }
                }
            });
        }
        #endregion
    }
}
