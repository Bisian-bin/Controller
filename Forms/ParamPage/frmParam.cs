using Forms.ParamPage;
using MiniExcelLibs;
using QMBaseClass.板卡.InterfaceCard;
using QMBaseClass.板卡.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class frmParam : Form
    {


        public frmParam()
        {

            InitializeComponent();
            LoadJsonFilesToComboBoxes();          
        }

        private frmParam _instance;

        public frmParam Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new frmParam();
                return _instance;
            }
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
        #region 载入配方文件到下拉框
        private void LoadJsonFilesToComboBoxes()
        {
            string directoryPath = Application.StartupPath + @"\配方文件";

            // 清空现有项

            cmbReadFile.Items.Clear();

            foreach (var filePath in Directory.GetFiles(directoryPath, "*.json"))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                cmbReadFile.Items.Add(fileName);
            }
        }
        #endregion
    
        public static ParamData settings;


        #region 保存配方文件
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (inputSaveFile.Text == "")
            {
                MessageBox.Show("请输入文件名");
                return;
            }
            #region 控件赋值给静态字段
            ParamInfo.language = cmbLanguage.Text;
            ParamInfo.runModel = cmbRunModel.Text;
            ParamInfo.productModel = cmbProductModel.Text;
            ParamInfo.door = chkDoor.Checked;
            ParamInfo.buzzer = chkBuzzer.Checked;
            ParamInfo.fan = chkIonFan.Checked;
            ParamInfo.icPhoto = chkICPhoto.Checked;
            ParamInfo.trayModel = chkTrayModel.Checked;
            ParamInfo.binSplit = chkSplitBin.Checked;
            ParamInfo.secsgem = chkSecsGem.Checked;
            ParamInfo.random = chkRandom.Checked;
            ParamInfo.binNum = cmbBinNum.Text;
            ParamInfo.speed = cmbSpeed.Text;

            ParamInfo.trayXnum = Convert.ToInt16(inputTrayXNum.Text);
            ParamInfo.trayYnum = Convert.ToInt16(inputTrayYNum.Text);
            ParamInfo.trayXspa = Convert.ToDouble(inputTrayXSpacing.Text);
            ParamInfo.trayYspa = Convert.ToDouble(inputTrayYSpacing.Text);
            ParamInfo.hight = inputHight.Text;
            ParamInfo.trayCol = inputTrayColInterval.Text;
            ParamInfo.trayRow = inputTrayRowInterval.Text;
            ParamInfo.binCol = inputBIBColInterval.Text;
            ParamInfo.binRow = inputBIBRowInterval.Text;

            ParamInfo.bibXnum = Convert.ToInt16(inputBIBXNum.Text);
            ParamInfo.bibYnum = Convert.ToInt16(inputBIBYNum.Text);
            ParamInfo.bibXspa = Convert.ToDouble(inputBIBXSpacing.Text);
            ParamInfo.bibYspa = Convert.ToDouble(inputBIBYSpacing.Text);
            ParamInfo.bibCol = inputBIBColInterval.Text;
            ParamInfo.bibRow = inputBIBRowInterval.Text;

            ParamInfo.Lbin1 = chkLeftBinSuction1.Checked;
            ParamInfo.Lbin2 = chkLeftBinSuction2.Checked;
            ParamInfo.Lbin3 = chkLeftBinSuction3.Checked;
            ParamInfo.Lbin4 = chkLeftBinSuction4.Checked;
            ParamInfo.Lbin5 = chkLeftBinSuction5.Checked;

            ParamInfo.Rbin1 = chkRightBinSuction1.Checked;
            ParamInfo.Rbin2 = chkRightBinSuction2.Checked;
            ParamInfo.Rbin3 = chkRightBinSuction3.Checked;
            ParamInfo.Rbin4 = chkRightBinSuction4.Checked;
            ParamInfo.Rbin5 = chkRightBinSuction5.Checked;

            ParamInfo.tray1 = chkTraySuction1.Checked;
            ParamInfo.tray2 = chkTraySuction2.Checked;
            ParamInfo.tray3 = chkTraySuction3.Checked;
            ParamInfo.tray4 = chkTraySuction4.Checked;
            ParamInfo.tray5 = chkTraySuction5.Checked;
            ParamInfo.tray6 = chkTraySuction6.Checked;
            ParamInfo.tray7 = chkTraySuction7.Checked;
            ParamInfo.tray8 = chkTraySuction8.Checked;
            ParamInfo.tray9 = chkTraySuction9.Checked;
            ParamInfo.tray10 = chkTraySuction10.Checked;
            ParamInfo.tray11 = chkTraySuction11.Checked;
            ParamInfo.tray12 = chkTraySuction12.Checked;

            ParamInfo.bib1 = chkBIBSuction1.Checked;
            ParamInfo.bib2 = chkBIBSuction2.Checked;
            ParamInfo.bib3 = chkBIBSuction3.Checked;
            ParamInfo.bib4 = chkBIBSuction4.Checked;
            ParamInfo.bib5 = chkBIBSuction5.Checked;
            ParamInfo.bib6 = chkBIBSuction6.Checked;
            ParamInfo.bib7 = chkBIBSuction7.Checked;
            ParamInfo.bib8 = chkBIBSuction8.Checked;
            ParamInfo.bib9 = chkBIBSuction9.Checked;
            ParamInfo.bib10 = chkBIBSuction10.Checked;
            ParamInfo.bib11 = chkBIBSuction11.Checked;
            ParamInfo.bib12 = chkBIBSuction12.Checked;

            ParamInfo.trayA = inputTrayAbsorbDelay.Text;
            ParamInfo.bibA = inputBIBAbsorbDelay.Text;
            ParamInfo.leftA = inputLeftBinAbsorbDelay.Text;
            ParamInfo.rightA = inputRightBinAbsorbDelay.Text;

            ParamInfo.trayB = inputTrayBrokenDelay.Text;
            ParamInfo.bibB = inputBIBBrokenDelay.Text;
            ParamInfo.leftB = inputLeftBinBrokenDelay.Text;
            ParamInfo.rightB = inputRightBinBrokenDelay.Text;

            ParamInfo.transferNum = inputTransferNum.Text;
            ParamInfo.transferSpa = inputTransferSpacing.Text;



            ParamInfo.bin1Name = inputBinName1.Text;
            ParamInfo.class1 = cmbSortingClass1.Text;
            ParamInfo.code1 = inputCode1.Text;

            ParamInfo.bin2Name = inputBinName2.Text;
            ParamInfo.class2 = cmbSortingClass2.Text;
            ParamInfo.code2 = inputCode2.Text;

            ParamInfo.bin3Name = inputBinName3.Text;
            ParamInfo.class3 = cmbSortingClass3.Text;
            ParamInfo.code3 = inputCode3.Text;

            ParamInfo.bin4Name = inputBinName4.Text;
            ParamInfo.class4 = cmbSortingClass4.Text;
            ParamInfo.code4 = inputCode4.Text;

            ParamInfo.bin5Name = inputBinName5.Text;
            ParamInfo.class5 = cmbSortingClass5.Text;
            ParamInfo.code5 = inputCode5.Text;

            ParamInfo.bin6Name = inputBinName6.Text;
            ParamInfo.class6 = cmbSortingClass6.Text;
            ParamInfo.code6 = inputCode6.Text;

            ParamInfo.bin7Name = inputBinName7.Text;
            ParamInfo.class7 = cmbSortingClass7.Text;
            ParamInfo.code7 = inputCode7.Text;

            ParamInfo.bin8Name = inputBinName8.Text;
            ParamInfo.class8 = cmbSortingClass8.Text;
            ParamInfo.code8 = inputCode8.Text;
            #endregion

            try
            {

                string selectedFile = inputSaveFile.Text + ".json";

                ParamInfo.path = Path.Combine(Application.StartupPath + @"\配方文件", selectedFile);
                settings.GenJson();
                MessageBox.Show("导出成功");
                if (!cmbReadFile.Items.Contains(inputSaveFile.Text))
                {
                    cmbReadFile.Items.Add(inputSaveFile.Text);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("导出失败");
            }

        }
        #endregion

        #region 加载配方文件
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (cmbReadFile.SelectedIndex == -1)
            {
                MessageBox.Show("请选择文件");
                return;
            }
            try
            {
                // 加载JSON数据
                string readFile = cmbReadFile.SelectedItem.ToString() + ".json";
                ParamInfo.path = Path.Combine(Application.StartupPath + @"\配方文件", readFile);
                settings = JsonLoader.LoadFromFile(ParamInfo.path);
                #region 赋值给控件
                // 绑定基本设置
                cmbLanguage.Text = settings.Language;
                cmbRunModel.Text = settings.ActivityMode;
                cmbProductModel.Text = settings.ProductModel;

                // 绑定复选框
                chkDoor.Checked = settings.DoorLock;
                chkBuzzer.Checked = settings.Buzzer;
                chkIonFan.Checked = settings.Fan;
                chkICPhoto.Checked = settings.ICphoto;
                chkTrayModel.Checked = settings.TrayModel;
                chkSplitBin.Checked = settings.SplitBin;
                chkSecsGem.Checked = settings.SecsGem;
                chkRandom.Checked = settings.RandomGeneration;
                cmbBinNum.Text = settings.BinNum;

                // 绑定轴设置
                cmbSpeed.Text = settings.AxisSet.Speed;

                // 绑定Tray尺寸
                inputTrayXNum.Text = settings.TrayDimensions.TrayXNum.ToString();
                inputTrayYNum.Text = settings.TrayDimensions.TrayYNum.ToString();
                inputTrayXSpacing.Text = settings.TrayDimensions.TrayXSpacing.ToString();
                inputTrayYSpacing.Text = settings.TrayDimensions.TrayYSpacing.ToString();
                inputHight.Text = settings.TrayDimensions.TrayHight;
                inputTrayColInterval.Text = settings.TrayDimensions.TrayColInterval;
                inputTrayRowInterval.Text = settings.TrayDimensions.TrayRowInterval;
                inputBinColInterval.Text = settings.TrayDimensions.BinColInterval;
                inputBinRowInterval.Text = settings.TrayDimensions.BinRowInterval;

                // 控件BIB尺寸
                inputBIBXNum.Text = settings.BIBBcardDimensions.BIBXNum.ToString();
                inputBIBYNum.Text = settings.BIBBcardDimensions.BIBYNum.ToString();
                inputBIBXSpacing.Text = settings.BIBBcardDimensions.BIBXSpacing.ToString();
                inputBIBYSpacing.Text = settings.BIBBcardDimensions.BIBYSpacing.ToString();
                inputBIBColInterval.Text = settings.BIBBcardDimensions.BIBColInterval;
                inputBIBRowInterval.Text = settings.BIBBcardDimensions.BIBRowInterval;

                //绑定吸嘴
                chkLeftBinSuction1.Checked = settings.Suction.LeftSuction[0];
                chkLeftBinSuction2.Checked = settings.Suction.LeftSuction[1];
                chkLeftBinSuction3.Checked = settings.Suction.LeftSuction[2];
                chkLeftBinSuction4.Checked = settings.Suction.LeftSuction[3];
                chkLeftBinSuction5.Checked = settings.Suction.LeftSuction[4];

                chkRightBinSuction1.Checked = settings.Suction.RightSuction[0];
                chkRightBinSuction2.Checked = settings.Suction.RightSuction[1];
                chkRightBinSuction3.Checked = settings.Suction.RightSuction[2];
                chkRightBinSuction4.Checked = settings.Suction.RightSuction[3];
                chkRightBinSuction5.Checked = settings.Suction.RightSuction[4];

                chkTraySuction1.Checked = settings.Suction.TraySuction[0];
                chkTraySuction2.Checked = settings.Suction.TraySuction[1];
                chkTraySuction3.Checked = settings.Suction.TraySuction[2];
                chkTraySuction4.Checked = settings.Suction.TraySuction[3];
                chkTraySuction5.Checked = settings.Suction.TraySuction[4];
                chkTraySuction6.Checked = settings.Suction.TraySuction[5];
                chkTraySuction7.Checked = settings.Suction.TraySuction[6];
                chkTraySuction8.Checked = settings.Suction.TraySuction[7];
                chkTraySuction9.Checked = settings.Suction.TraySuction[8];
                chkTraySuction10.Checked = settings.Suction.TraySuction[9];
                chkTraySuction11.Checked = settings.Suction.TraySuction[10];
                chkTraySuction12.Checked = settings.Suction.TraySuction[11];

                chkBIBSuction1.Checked = settings.Suction.BIBSuction[0];
                chkBIBSuction2.Checked = settings.Suction.BIBSuction[1];
                chkBIBSuction3.Checked = settings.Suction.BIBSuction[2];
                chkBIBSuction4.Checked = settings.Suction.BIBSuction[3];
                chkBIBSuction5.Checked = settings.Suction.BIBSuction[4];
                chkBIBSuction6.Checked = settings.Suction.BIBSuction[5];
                chkBIBSuction7.Checked = settings.Suction.BIBSuction[6];
                chkBIBSuction8.Checked = settings.Suction.BIBSuction[7];
                chkBIBSuction9.Checked = settings.Suction.BIBSuction[8];
                chkBIBSuction10.Checked = settings.Suction.BIBSuction[9];
                chkBIBSuction11.Checked = settings.Suction.BIBSuction[10];
                chkBIBSuction12.Checked = settings.Suction.BIBSuction[11];

                //绑定延时
                inputTrayAbsorbDelay.Text = settings.AbsorbSet.TrayAbsorb;
                inputBIBAbsorbDelay.Text = settings.AbsorbSet.BIBAbsorb;
                inputLeftBinAbsorbDelay.Text = settings.AbsorbSet.LeftBinAbsorb;
                inputRightBinAbsorbDelay.Text = settings.AbsorbSet.RightBinAbsorb;

                inputTrayBrokenDelay.Text = settings.BrokenSet.TrayBroken;
                inputBIBBrokenDelay.Text = settings.BrokenSet.BIBBroken;
                inputLeftBinBrokenDelay.Text = settings.BrokenSet.LeftBinBroken;
                inputRightBinBrokenDelay.Text = settings.BrokenSet.RightBinBroken;

                //绑定中转站
                inputTransferNum.Text = settings.TransferStationDimensions.TransferNum.ToString();
                inputTransferSpacing.Text = settings.TransferStationDimensions.TransferSpacing.ToString();


                //绑定Bin
                inputBinName1.Text = settings.BinDates[0].Name;
                inputCode1.Text = settings.BinDates[0].Code;
                cmbSortingClass1.Text = settings.BinDates[0].Category;

                inputBinName2.Text = settings.BinDates[1].Name;
                inputCode2.Text = settings.BinDates[1].Code;
                cmbSortingClass2.Text = settings.BinDates[1].Category;

                inputBinName3.Text = settings.BinDates[2].Name;
                inputCode3.Text = settings.BinDates[2].Code;
                cmbSortingClass3.Text = settings.BinDates[2].Category;

                inputBinName4.Text = settings.BinDates[3].Name;
                inputCode4.Text = settings.BinDates[3].Code;
                cmbSortingClass4.Text = settings.BinDates[3].Category;

                inputBinName5.Text = settings.BinDates[4].Name;
                inputCode5.Text = settings.BinDates[4].Code;
                cmbSortingClass5.Text = settings.BinDates[4].Category;

                inputBinName6.Text = settings.BinDates[5].Name;
                inputCode6.Text = settings.BinDates[5].Code;
                cmbSortingClass6.Text = settings.BinDates[5].Category; 

                inputBinName7.Text = settings.BinDates[6].Name;
                inputCode7.Text = settings.BinDates[6].Code;
                cmbSortingClass7.Text = settings.BinDates[6].Category;

                inputBinName8.Text = settings.BinDates[7].Name;
                inputCode8.Text = settings.BinDates[7].Code;
                cmbSortingClass8.Text = settings.BinDates[7].Category;
                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 

        #endregion


    }
}
