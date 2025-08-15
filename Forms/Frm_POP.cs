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
    public partial class Frm_POP : Form
    {
        //Function function = new Function();
        public Frm_POP()
        {
            InitializeComponent();
        }
        private static Frm_POP _instance;

        public static Frm_POP Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new Frm_POP();
                return _instance;
            }
        }

        private void Frm_POP_Load(object sender, EventArgs e)
        {
            //string filePath = "Languages\\" + DefTask.variable.Languages + "\\Frm_POP.json";
            //if (DefTask.variable.Languages == "Chinese")
            //{
            //    filePath = "Languages\\Chinese\\Frm_POP.json";
            //}
            //else if (DefTask.variable.Languages == "English")
            //{
            //    filePath = "Languages\\English\\Frm_POP.json";
            //}
            //if (DefTask.variable.LanguagesWrite)
            //{
            //    MultiLanguage.BtnSaveToJson(this, filePath);
            //}
            //else
            //{
            //    MultiLanguage.BtnLoadFromJson(this, filePath);
            //}

            //label1.Visible = false;
            //textBox1.Visible = false;
        }


        #region 确定
        private void btnSure_Click(object sender, EventArgs e)
        {
            //if (DefTask.variable.id != 0 && DefTask.variable.cancelStep != 0 && DefTask.variable.sureStep != 0)
            //{
            //    DefTask.CurrStep[DefTask.variable.id] = DefTask.variable.sureStep;
            //}
            //if (DefTask.variable.CleanOutFlag)
            //{
            //    DefTask.variable.CleanOutFlag = false;
            //    DefTask.variable.MachineState = MachineStatus.Stop;
            //}

            //DefTask.variable.POPFlag = false;
            //DefTask.variable.btnReset = true;
            //DefTask.variable.id = 0;
            //DefTask.variable.cancelStep = 0;
            //DefTask.variable.sureStep = 0;
            timerScan.Stop();
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
            }
            this.Close();

        }

        #endregion

        #region 取消/跳过
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (DefTask.variable.id != 0 && DefTask.variable.cancelStep != 0 && DefTask.variable.sureStep != 0)
            //{
            //    DefTask.CurrStep[DefTask.variable.id] = DefTask.variable.cancelStep;             
            //}

            //if (LabelY1.Text == "BIB板扫码NG次数达到设定值，点确定重新扫码，点跳过手动输入!" && textBox1.Text.Length > 0)
            //{
            //    DefTask.variable.strQR = textBox1.Text;
            //}

            //DefTask.variable.POPFlag = false;
            //DefTask.variable.btnReset = true;
            //DefTask.variable.id = 0;
            //DefTask.variable.cancelStep = 0;
            //DefTask.variable.sureStep = 0;
            //timerScan.Stop();
            //if (pictureBox.Image != null)
            //{
            //    pictureBox.Image.Dispose();
            //}
            //this.Close();
        }
        #endregion

        private void timerScan_Tick(object sender, EventArgs e)
        {
            //if (LabelY1.Text == "BIB板扫码NG次数达到设定值，点确定重新扫码，点跳过手动输入!")
            //{
            //    label1.Visible = true;
            //    textBox1.Visible = true;
            //}
            //else
            //{
            //    label1.Visible = false;
            //    textBox1.Visible = false;
            //}

            //if (IOStatus.XStatus[(int)enDI.DI001_Stop] || DefTask.variable.btnStop == true)
            //{
            //    DefTask.variable.POPFlag = false;
            //    DefTask.variable.AlarmFlag = false;
            //    DefTask.variable.AxisAlarm = false;
            //    timerScan.Stop();
            //    if (pictureBox.Image != null)
            //    {
            //        pictureBox.Image.Dispose();
            //    }
            //    this.Close();
            //}
        }
    }
}
