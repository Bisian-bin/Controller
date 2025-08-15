using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Model;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using SECSGEM;
using InnerEvents;
using System.Collections;

namespace Forms
{
    // -----------------------------------------------------------------------------
    // <summary>
    // <para>
    // 作 者     ： TangJiaMing
    // <para>
    // 修改日期  ：2025/07/30    
    // <para>
    // 修改记录  ：  
    // 1.tray机构误感应Load区对射感应器导致气缸提前伸出。修改逻辑:tray盘上升到位，tray机构移走脱离load区再把气缸伸出来。
    // </summary>
    // -----------------------------------------------------------------------------
    public partial class MainForm : Base
    {
        #region 对象
        //菜单对象
        int Language = 0;
        ToolStripButton[] MainBtn = new ToolStripButton[10];
        ToolStripButton[] SubBtn = new ToolStripButton[10];

        //窗体对象
        public Dictionary<string, Form> formInstances = new Dictionary<string, Form>();
        FrmUser frmUser;
        frmDebug frmDebug;
        frmParam frmParam;
        FrmTCP FrmTCP;
        frmMap frmMap;
        FrmRole FrmRole;
        FrmView frmView;
        frmIO frmIO;
        frmSecsGem frmSecsGem;

        private IEventAggregator eventAggregator;
        private IUnityContainer container;
        #endregion

        #region 事件处理
        public MainForm(IEventAggregator eventAggregator, IUnityContainer container)
        {
            InitializeComponent();

            this.eventAggregator = eventAggregator;
            this.container = container;

            //窗体最大化
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //创建Event订阅
            eventAggregator.GetEvent<MainEvent>().Subscribe((obj) =>
            {
                string aa = obj.ToString();
            }, ThreadOption.BackgroundThread);

            #region 初始化菜单
            IniMenu();

            //初始化绑定主菜单
            for (int i = 0; i < 10; i++)
            {
                MainBtn[i] = GetToolStripButtonText("ts" + (i + 1).ToString(), ToolStrip1);
                MainBtn[i].BackColor = Color.Transparent;
                if (MenuData[0, i + 1].ID == null)
                {
                    MainBtn[i].Visible = false;
                    continue;
                }
                MainBtn[i].Visible = MenuData[0, i + 1].Visible;
                MainBtn[i].AccessibleName = MenuData[0, i + 1].ID;
                Bitmap b = new Bitmap(MenuData[0, i + 1].ImageName);
                MainBtn[i].Image = b;
                MainBtn[i].Text = MenuData[0, i + 1].Text[Language];
                MainBtn[i].TextAlign = ContentAlignment.BottomCenter;
            }
            MainBtn[0].BackColor = ColorTranslator.FromHtml("#E5F3FF");

            //初始化绑定主菜单对应的子菜单
            for (int i = 0; i < 10; i++)
            {
                SubBtn[i] = GetToolStripButtonText("tsb" + (i + 1).ToString(), tolSpmid);
                SubBtn[i].BackColor = Color.Transparent;
                if (MenuData[1, i].ID == null)
                {
                    SubBtn[i].Visible = false;
                    continue;
                }
                SubBtn[i].Visible = MenuData[1, i].Visible;
                SubBtn[i].AccessibleName = MenuData[1, i].ID;
                SubBtn[i].Text = MenuData[1, i].Text[Language];
                SubBtn[i].TextAlign = ContentAlignment.MiddleCenter;
            }
            SubBtn[0].BackColor = ColorTranslator.FromHtml("#E5F3FF");
            #endregion

            #region 初始化子窗体
            dock.Controls.Clear();

            frmUser = new FrmUser(this);
            frmUser.Instance.TopLevel = false;
            frmUser.Instance.Dock = DockStyle.Fill;
            dock.Controls.Add(frmUser.Instance);
            formInstances.Add("FrmUser", frmUser.Instance);

            frmParam = new frmParam();
            frmParam.Instance.TopLevel = false;
            frmParam.Instance.Dock = DockStyle.Fill;
            dock.Controls.Add(frmParam.Instance);
            formInstances.Add("frmParam", frmParam.Instance);
            FlowFactory.LogView = frmParam.Instance.ucLog2;

            frmDebug = new frmDebug();
            frmDebug.Instance.TopLevel = false;
            frmDebug.Instance.Dock = DockStyle.Fill;
            dock.Controls.Add(frmDebug.Instance);
            formInstances.Add("frmDebug", frmDebug.Instance);

            FrmTCP = new FrmTCP();
            FrmTCP.Instance.TopLevel = false;
            FrmTCP.Instance.Dock = DockStyle.Fill;
            dock.Controls.Add(FrmTCP.Instance);
            formInstances.Add("FrmTCP", FrmTCP.Instance);

            frmMap = new frmMap();
            frmMap.Instance.TopLevel = false;
            frmMap.Instance.Dock = DockStyle.Fill;
            dock.Controls.Add(frmMap.Instance);
            formInstances.Add("FrmMap", frmMap.Instance);

            FrmRole = new FrmRole();
            FrmRole.Instance.TopLevel = false;
            FrmRole.Instance.Dock = DockStyle.Fill;
            dock.Controls.Add(FrmRole.Instance);
            formInstances.Add("FrmRole", FrmRole.Instance);

            frmView = new FrmView(eventAggregator,container);
            frmView.Instance.TopLevel = false;
            frmView.Instance.Dock = DockStyle.Fill;
            dock.Controls.Add(frmView.Instance);
            formInstances.Add("FrmView", frmView.Instance);
            frmView.Instance.Visible = true;
            frmView.Instance.Visible = false;

            frmIO = new frmIO(eventAggregator, container);
            frmIO.Instance.TopLevel = false;
            frmIO.Instance.Dock = DockStyle.Fill;
            dock.Controls.Add(frmIO.Instance);
            formInstances.Add("frmIO", frmIO.Instance);

            frmSecsGem = new frmSecsGem(eventAggregator, container);
            frmSecsGem.Instance.TopLevel = false;
            frmSecsGem.Instance.Dock = DockStyle.Fill;
            dock.Controls.Add(frmSecsGem.Instance);
            formInstances.Add("frmSecsGem", frmSecsGem.Instance);

            frmUser.Instance.Visible = true;//显示主界面
            #endregion     
        }

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Click(object sender, EventArgs e)
        {
            ShowFrm(sender);
        }

        /// <summary>
        /// 主菜单按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            //获取对应的button
            ToolStripButton thsbtn = (ToolStripButton)sender;

            //绑定新的子菜单
            int iflag = 0;

            //隐藏
            for (int k = 0; k < SubBtn.Length; k++)
            {
                SubBtn[k].Visible = false;
            }

            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (MenuData[i, j].MainID == thsbtn.AccessibleName)
                    {
                        SubBtn[iflag] = GetToolStripButtonText("tsb" + (iflag + 1).ToString(), tolSpmid);
                        SubBtn[iflag].BackColor = Color.Transparent;
                        if (MenuData[i, j].ID == null)
                        {
                            SubBtn[iflag].Visible = false;
                            continue;
                        }
                        SubBtn[iflag].Visible = MenuData[i, j].Visible;
                        SubBtn[iflag].AccessibleName = MenuData[i, j].ID;
                        SubBtn[iflag].Text = MenuData[i, j].Text[Language];
                        SubBtn[iflag].TextAlign = ContentAlignment.MiddleCenter;
                        iflag++;
                    }
                }
            }
            SubBtn[0].BackColor = ColorTranslator.FromHtml("#E5F3FF");

            //更新子菜单显示状态
            foreach (ToolStripButton btn in MainBtn)
            {
                btn.BackColor = Color.Transparent;
            }
            thsbtn.BackColor = ColorTranslator.FromHtml("#E5F3FF");
        }

        /// <summary>
        /// 子菜单按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubMenu_MouseDown(object sender, MouseEventArgs e)
        {
            //获取对应的button
            ToolStripButton thsbtn = (ToolStripButton)sender;

            //更新子菜单显示状态
            foreach (ToolStripButton btn in SubBtn)
            {
                btn.BackColor = Color.Transparent;
            }
            thsbtn.BackColor = ColorTranslator.FromHtml("#E5F3FF");
        }
        #endregion

        #region 方法处理

        /// <summary>
        /// 切换页面
        /// </summary>
        /// <param name="sender">按钮对象</param>
        private void ShowFrm(object sender)
        {
            string thisPage = string.Empty;
            //获取对应的button
            ToolStripButton thsbtn = (ToolStripButton)sender;

            //创建Event推送
            eventAggregator.GetEvent<UserEvent>().Publish(thsbtn.Text);

            //抓取组件Page信息
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (MenuData[i, j].ID == thsbtn.AccessibleName)
                    {
                        thisPage = MenuData[i, j].Page;
                        break;
                    }
                }
            }

            //显示对应的Page界面
            foreach (var page in formInstances)
            {
                if (page.Key == thisPage)
                {
                    page.Value.Visible = true;
                }
                else
                {
                    page.Value.Visible = false;
                }
            }
        }
        #endregion

        #region SECSGEM
        //SECSGEM组件
        public static HSMS Hsms = new HSMS();
        //ini配置文件读取类
        public static MovData Redata = new MovData();
        //报警消息队列
        private static Queue qAlarm = new Queue();

        /// <summary>
        /// 报警消息推送
        /// </summary>
        /// <param name="Id">AlarmID</param>
        /// <param name="Status">Alarm状态，1:触发报警，0:消除报警</param>
        /// <param name="Content">Alarm内</param>
        public static void AlarmReport(string Id, int Status, string Content)
        {
            string path = Application.StartupPath + "\\Config\\SecsGem\\ALARM.ini";
            string AlarmEnable = Redata.getIni("AlarmReportEnable", Id, "", path);

            if (Status == 1)
            {
                //记录报警信息
                qAlarm.Enqueue(new string[] { Id, Content });
                Status = 128;
            }

            if (AlarmEnable == "")
            {
                AlarmEnable = "1";
                Redata.writeIni("AlarmReportEnable", Id, AlarmEnable, path);
            }
            if (AlarmEnable == "1")
            {
                Hsms.AlarmReport(Id, Status, Content);
            }
        }

        /// <summary>
        /// 事件报告推送
        /// </summary>
        /// <param name="ceid">事件ID</param>
        /// <param name="b">默认false</param>
        public static void EventReport(int ceid, bool b)
        {
            Hsms.EventReport(ceid, b);
        }
        #endregion
    }
}
