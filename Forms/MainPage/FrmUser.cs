using Forms.Flow;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class FrmUser : Form
    {
        private Form _window;
        public delegate void ReceiveDelegate(int Code);

        public FrmUser(Form window)
        {
            InitializeComponent();


            this._window = window;

            this.infoPanel.AddSingleRecipe("a");
            this.infoPanel.AddSingleRecipe("b");
            this.infoPanel.AddSingleRecipe("c");
            this.infoPanel.AddSingleRecipe("d");
        }

        protected override CreateParams CreateParams
        {
            //重写WinForms 控件的 CreateParams 属性，通过修改窗口的扩展样式（ExStyle）来
            //启用 WS_EX_COMPOSITED 标志（0x02000000），主要用于解决 UI 渲染时的闪烁问题
            get
            {
                CreateParams paras = base.CreateParams;
                paras.ExStyle |= 0x02000000;
                return paras;
            }
        }

        private FrmUser _instance;

        public FrmUser Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new FrmUser(_window);
                return _instance;
            }
        }
        private async void statePanel_OnLoginButtonClicked(object sender, EventArgs e)
        {
            var loginPanel = new LoginPanel();
            var drawerForm = AntdUI.Drawer.open(new AntdUI.Drawer.Config(_window, loginPanel)
            {
                Align = AntdUI.TAlignMini.Left,
                Mask = true,
                MaskClosable = true,
                Padding = 0,
            });
            loginPanel.DrawerForm = drawerForm;
        }

        private void infoPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.infoPanel.Op = this.infoPanel.Items[this.infoPanel.SelectedIndex].ToString();
        }

        /// <summary>
        /// 载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmUser_Load(object sender, EventArgs e)
        {
            if (FlowFactory.AxisContorl.InitialAll())//初始化板卡
            { FlowFactory.LogView.Invoke(new Action(() => { FlowFactory.LogView.Append("调用板卡成功!"); })); }
            else { FlowFactory.LogView.Invoke(new Action(() => { FlowFactory.LogView.Append("调用板卡失败!"); })); Application.Exit(); }
            FlowFactory.ChildReceiveDown = Down;
        }


        public Frm_POP pop = null;
        public void Down(int Code)
        {
            this.Invoke(new Action(() =>
            {
                Thread.Sleep(300);//延迟弹窗
                FlowFactory.AxisContorl.Axis.AlarmObjData.TryGetValue(Code.ToString(), out var Alarm);//根据Code读取对应资料
                pop = new Frm_POP();//界面显示
                pop.StartPosition = FormStartPosition.CenterScreen;
                pop.Show();
                pop.pictureBox.Image = Image.FromFile(Application.StartupPath + "\\ico\\" + Alarm.AlarmPictures + ".png");
                pop.LabelX1.Text = Alarm.AlarmNote;
            }));
        }

        /// <summary>
        /// 设备状态显示
        /// </summary>
        public void SetMachineStatus()
        {
            switch (FlowFactory.RunStatus)
            {
                case Order.MachineStatuOrder.Homing:
                    {
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y000_启动灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y001_停止灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y002_复位灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y003_OneCycle灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y004_CleanOut灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y005_归零灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y006_红灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y007_黄灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y008_绿灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y009_蜂鸣器, false);
                    }
                    break;

                case Order.MachineStatuOrder.Pause:
                    {
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y000_启动灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y001_停止灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y002_复位灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y003_OneCycle灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y004_CleanOut灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y005_归零灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y006_红灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y007_黄灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y008_绿灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y009_蜂鸣器, false);
                    }
                    break;

                case Order.MachineStatuOrder.Running:
                    {
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y000_启动灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y001_停止灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y002_复位灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y003_OneCycle灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y004_CleanOut灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y005_归零灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y006_红灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y007_黄灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y008_绿灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y009_蜂鸣器, false);
                    }
                    break;

                case Order.MachineStatuOrder.Alarm:
                    {
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y000_启动灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y001_停止灯, true);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y002_复位灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y003_OneCycle灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y004_CleanOut灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y005_归零灯, false);
                        if (FlowFactory.AxisContorl.GetYStatus((int)Order.OutOrder.Y006_红灯)){ FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y006_红灯, true); }
                        else { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y006_红灯, false); }
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y007_黄灯, false);
                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y008_绿灯, false);
                        if (FlowFactory.AxisContorl.GetYStatus((int)Order.OutOrder.Y009_蜂鸣器)) { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y009_蜂鸣器, true);}
                        else { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y009_蜂鸣器, false); }
                    }
                    break;

                case Order.MachineStatuOrder.EStop:
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y000_启动灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y001_停止灯, true);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y002_复位灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y003_OneCycle灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y004_CleanOut灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y005_归零灯, false);
                    if (FlowFactory.AxisContorl.GetYStatus((int)Order.OutOrder.Y006_红灯)) { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y006_红灯, true); }
                    else { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y006_红灯, false); }
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y007_黄灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y008_绿灯, false);
                    if (FlowFactory.AxisContorl.GetYStatus((int)Order.OutOrder.Y009_蜂鸣器)) { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y009_蜂鸣器, true); }
                    else { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y009_蜂鸣器, false); }
                    break;

                case Order.MachineStatuOrder.Reset:
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y000_启动灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y001_停止灯, true);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y002_复位灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y003_OneCycle灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y004_CleanOut灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y005_归零灯, false);
                    if (FlowFactory.AxisContorl.GetYStatus((int)Order.OutOrder.Y006_红灯)) { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y006_红灯, true); }
                    else { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y006_红灯, false); }
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y007_黄灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y008_绿灯, false);
                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y009_蜂鸣器, false);
                    break;

                case Order.MachineStatuOrder.HomeCompleted:


                    break;
            }
        }
    }
}
