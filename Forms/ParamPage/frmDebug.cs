using Model;
using Newtonsoft.Json;
using QMBaseClass;
using QMBaseClass.板卡.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class frmDebug : Form
    {
        public frmDebug()
        {
            InitializeComponent();
        }

        private frmDebug _instance;

        public frmDebug Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new frmDebug();
                return _instance;
            }
        }
        /// <summary>
        /// 当前调试轴
        /// </summary>
        public static QMAxisModel Current { get; set; }

        /// <summary>
        /// 显示网格轴数量
        /// </summary>
        public static int AxisRowCount = 20;


        DataGridClass Grid = new DataGridClass();  //读取DataGrid类

        /// <summary>
        /// 记录当前轴数值
        /// </summary>
        public static string AxisPosite { get; set; }

        private void frmDebug_Load(object sender, EventArgs e)
        {
            InitAxis();//初始化轴数据
            Grid.NotChangeListRow(dgvAxis);//禁止更改宽高
            Grid.DataGridViewInit(dgvAxis);//初始化表格
            Grid.AddControl_Parameter(dgvAxis, 0, AxisRowCount);//生成按钮
            listBoxAxis.SelectedIndex = 0; listBoxAxis_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// 初始化轴数据
        /// </summary>
        /// <param name="axiss"></param>
        public async void InitAxis()
        {
            Current = FlowFactory.AxisContorl.Axis.AxisList[0];
            FlowFactory.AxisContorl.Axis.AxisList.ForEach(ca1 => { listBoxAxis.Items.Add(ca1.AxisNo + "-" + ca1.AxisName); });
            if (await FlowFactory.AxisContorl.InitCardAsync()) new Thread(Timer1) { IsBackground = true }.Start();
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        private void Timer1()
        {
            while (true)
            {
                Thread.Sleep(100);
                this.Invoke(new Action(() => { UpdateAxis(); }));
            }
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        private void UpdateAxis()
        {
            if (Current != null)
            {
                AxisPosite = txtCurrentPos.Text = Current.AxisStatus.CurrentPos.ToString();
                if (Current.AxisStatus.IsAlarm.Val == false) { btnErr.BackColor = Color.Green; } else { btnErr.BackColor = Color.Red; }
                if (Current.AxisStatus.IsOrg.Val) { btnOrg.BackColor = Color.Green; } else { btnOrg.BackColor = Color.Red; }
                if (Current.AxisStatus.IsPel.Val) { btnPlimit.BackColor = Color.Green; } else { btnPlimit.BackColor = Color.Red; }
                if (Current.AxisStatus.IsNel.Val) { btnNlimit.BackColor = Color.Green; } else { btnNlimit.BackColor = Color.Red; }
                if (Current.AxisStatus.IsServo.Val){ EnableBtn.BackColor = Color.Green; } else { EnableBtn.BackColor = Color.Gray; }
            }
        }

        /// <summary>
        /// 点击轴号事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            Current = FlowFactory.AxisContorl.Axis.AxisList[listBoxAxis.SelectedIndex];
            Axislabel.Text = Current.AxisName + "(" + Current.HomeType + ")";
            QMPosExcelModel excelModel = FlowFactory.AxisContorl.Axis.posExcelList.Find(da => da.AxisNo == Current.AxisNo);
            Grid.ChangeLabname(excelModel, dgvAxis); Grid.PicImage(picAxis, (listBoxAxis.SelectedIndex + 1));//显示图片
        }

        /// <summary>
        /// 使能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void EnableBtn_Click(object sender, EventArgs e)
        {
             FlowFactory.AxisContorl.MotorEnable(Current.AxisNo, EnableBtn.BackColor == Color.Green);
        }

        /// <summary>
        /// 停止运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopBtn_Click(object sender, EventArgs e)
        {
            FlowFactory.AxisContorl.MotionStop();//停止运动
        }

        /// <summary>
        ///单轴归零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HomeBtn_Click(object sender, EventArgs e)
        {
           await FlowFactory.AxisContorl.HomeSingleAxis(Current.AxisNo);
        }

        /// <summary>
        /// 正向运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JogBtnAdd_Click(object sender, EventArgs e)
        {
            FlowFactory.AxisContorl.MoveAxis(Current.AxisNo,(Convert.ToDouble(txtCurrentPos.Text) + Convert.ToDouble(combVel.Text)), 1);
        }

        /// <summary>
        /// 负向运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JogBtnSub_Click(object sender, EventArgs e)
        {
            FlowFactory.AxisContorl.MoveAxis(Current.AxisNo, (Convert.ToDouble(txtCurrentPos.Text) - Convert.ToDouble(combVel.Text)), 1);
        }

        /// <summary>
        /// 清除状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            FlowFactory.AxisContorl.ClearAxisStatus(Current.AxisNo);
        }
    }
}
