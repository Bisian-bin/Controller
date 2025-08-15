using HsmsLibrary;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
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
    public partial class frmSecsGem : Form
    {
        private IEventAggregator eventAggregator;
        private IUnityContainer container;

        public frmSecsGem(IEventAggregator eventAggregator, IUnityContainer container)
        {
            InitializeComponent();

            this.eventAggregator = eventAggregator;
            this.container = container;
        }

        private frmSecsGem _instance;

        public frmSecsGem Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new frmSecsGem(eventAggregator, container);
                return _instance;
            }
        }

        private void chkRemote_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            MainForm.EventReport(3001, false);
            if (chkRemote.Checked)
            {
                HSMS.m_bCommEnabled = true;
                chkLocal.Checked = false;
            }
        }

        private void chkLocal_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            MainForm.EventReport(3001, false);
            if (chkLocal.Checked)
            {
                HSMS.m_bCommEnabled = false;
                chkRemote.Checked = false;
            }
        }

        private void swStatus_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            MainForm.EventReport(3001, false);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //提供log记录功能
                LogService.LogService.Info(this.GetType().FullName + "btnConnect_Click");

                //订阅状态变更
                Publisher.Subscriber_Status += new Publisher.StatusEntrust(StatusChange);
                Publisher.Subscriber_RevMsg += new Publisher.StatusEntrust(StatusChange);
                Publisher.Subscriber_SendMsg += new Publisher.StatusEntrust(StatusChange);

                MainForm.Hsms.T3 = Convert.ToInt32(txtT3.Text);
                MainForm.Hsms.T5 = Convert.ToInt32(txtT5.Text);
                MainForm.Hsms.T6 = Convert.ToInt32(txtT6.Text);
                MainForm.Hsms.T7 = Convert.ToInt32(txtT7.Text);
                MainForm.Hsms.T8 = Convert.ToInt32(txtT8.Text);
                MainForm.Hsms.TL = Convert.ToInt32(txtLinkTime.Text);
                MainForm.Hsms.IP = txtIP.Text;
                MainForm.Hsms.Port = txtPort.Text;
                if (MainForm.Hsms.Connect())
                {
                    btnConnect.Enabled = false;
                    btnClose.Enabled = true;
                }
                else
                {
                    btnConnect.Enabled = true;
                    btnClose.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //将异常信息记录到log文件
                LogService.LogService.Error(this.GetType().FullName + "btnConnect_Click", ex);
            }
        }

        public void StatusChange(string strStatus)
        {
            try
            {
                if (strStatus == "SELECTED")
                {
                    HSMS.m_bCommEnabled = true;
                    swStatus.Checked = true;
                }
                if (strStatus == "DISCONNECTED")
                {
                    swStatus.Checked = false;
                    HSMS.m_bCommEnabled = false;
                }
                richTextBox1.Invoke(new Action(() =>
                {
                    richTextBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + strStatus + "\r\n\r\n";
                }));
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "StatusChange", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Publisher.Subscriber_Status -= new Publisher.StatusEntrust(StatusChange);
            Publisher.Subscriber_RevMsg -= new Publisher.StatusEntrust(StatusChange);
            Publisher.Subscriber_SendMsg -= new Publisher.StatusEntrust(StatusChange);
            MainForm.Hsms.DisConnect();
            swStatus.Checked = false;
            richTextBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " :关闭连接\r\n\r\n";
            btnConnect.Enabled = true;
            btnClose.Enabled = false;
        }

        private void frmSecsGem_Load(object sender, EventArgs e)
        {
            HSMS.UpdateRdo -= UpdateRdoBox;
            HSMS.UpdateRdo += UpdateRdoBox;
            btnConnect_Click(null, null);
        }

        private void UpdateRdoBox(bool value)
        {
            if (value)
            {
                chkRemote.Checked = true;
                chkLocal.Checked = false;
            }
            else
            {
                chkRemote.Checked = false;
                chkLocal.Checked = true;
            }
        }

        /// <summary>
        /// 开始上料模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadStart_Click(object sender, EventArgs e)
        {
            MainForm.Hsms.secsGEMVariable.LotNumber = "A0001111";
            MainForm.Hsms.secsGEMVariable.OpID = "Z000111";
            //3002 批次号，工号
            MainForm.EventReport(3002, false);
        }

        /// <summary>
        /// 开始下料模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnloadStart_Click(object sender, EventArgs e)
        {
            //3003 批次号，工号
            MainForm.Hsms.secsGEMVariable.LotNumber = "A0001111";
            MainForm.Hsms.secsGEMVariable.OpID = "Z000111";
            MainForm.EventReport(3003, false);
        }

        /// <summary>
        /// 批次号设置完成模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLotIDSetup_Click(object sender, EventArgs e)
        {
            //3004 批次号
            MainForm.Hsms.secsGEMVariable.LotNumber = "A0001111";
            MainForm.EventReport(3004, false);
        }

        /// <summary>
        /// 模拟开批
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLotStart_Click(object sender, EventArgs e)
        {
            //3005 机台工作时间，批次号，工号
            MainForm.Hsms.secsGEMVariable.LotNumber = "A0001111";
            MainForm.Hsms.secsGEMVariable.OpID = "Z000111";
            MainForm.EventReport(3005, false);
        }

        /// <summary>
        /// 模拟结批
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLotEnd_Click(object sender, EventArgs e)
        {
            //3006 机台工作时间，批次号，工号
            MainForm.Hsms.secsGEMVariable.LotNumber = "A0001111";
            MainForm.Hsms.secsGEMVariable.OpID = "Z000111";

            //1011,1012,1013,1014
            MainForm.Hsms.secsGEMVariable.OutTotalCount = "200";
            MainForm.Hsms.secsGEMVariable.PassCount = "192";
            MainForm.Hsms.secsGEMVariable.FailCount = "8";
            MainForm.Hsms.secsGEMVariable.Yield = "4.0";
            MainForm.EventReport(3006, false);
        }

        /// <summary>
        /// 模拟终止批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbort_Click(object sender, EventArgs e)
        {
            //3007 机台工作时间，批次号，工号
            MainForm.Hsms.secsGEMVariable.LotNumber = "A0001111";
            MainForm.Hsms.secsGEMVariable.OpID = "Z000111";
            MainForm.EventReport(3007, false);
        }

        /// <summary>
        /// Recipe下发完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOPsetup_Click(object sender, EventArgs e)
        {
            //3008 Recipe名称
            MainForm.Hsms.secsGEMVariable.Recipe = "ESSD12_1";
            MainForm.EventReport(3008, false);
        }

        ///// <summary>
        ///// 设备固定信息变更模拟
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnContansChange_Click(object sender, EventArgs e)
        //{
        //    //3009  MachineID
        //}

        /// <summary>
        /// 暂停模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            //3009 机台工作时间
            MainForm.Hsms.secsGEMVariable.MachineID = "QM919";
            MainForm.EventReport(3009, false);
        }

        /// <summary>
        /// 继续模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinue_Click(object sender, EventArgs e)
        {
            //3010 机台工作时间
            MainForm.Hsms.secsGEMVariable.MachineID = "QM919";
            MainForm.EventReport(3010, false);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 50000)
            {
                richTextBox1.Text = richTextBox1.Text.Substring(30000);
            }
            // 将光标位置设置到文本末尾
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // 滚动到光标位置
            richTextBox1.ScrollToCaret();
        }

        private void btnAlarmTrig_Click(object sender, EventArgs e)
        {
            //MainForm.AlarmReport("2001", 1, "吸嘴吸真空异常");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainForm.AlarmReport("2001", 0, "吸嘴吸真空异常");
        }

        private void chkRunCheck_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            if (chkRunCheck.Checked)
            {
                HSMS.isRunCheck = true;
            }
            else
            {
                HSMS.isRunCheck = false;
            }
        }
    }
}
