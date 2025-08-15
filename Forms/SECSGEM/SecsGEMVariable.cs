using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms
{
    [Serializable]
    public class RunCheckEntity
    {
        public string Name;
        public string MCName;
        public string ShuttleStepX;
        public string ShuttleStepY;
        public string ShuttleXCount;
        public string ShuttleYCount;
        public string TrayStepX;
        public string TrayStepY;
        public string TrayXCount;
        public string TrayYCount;
        public string BIBStepX;
        public string BIBStepX2;
        public string BIBStepY;
        public string BIBStepXPick;
        public string BIBStepX2Pick;
        public string BIBStepYPick;
        public string BIBXCount;
        public string BIBYCount;
        public string SthR;
        public string ScloseR;
        public string BibPickCount;
        public string Bin1Name;
        public string Bin1Flag;
        public string Bin1Code;
        public string Bin2Name;
        public string Bin2Flag;
        public string Bin2Code;
        public string Bin3Name;
        public string Bin3Flag;
        public string Bin3Code;
        public string TesterIp;
        public string ShareRootPath;
        public string TBadMax;
        public string TChangeNum;
        public string TPassBin;
        public string TFailBin;
        public string TErrorBin;
        public string BmsBindRootPath;
        public string BmsCompareRootPath;
        public string BmsMapRootPath;
        public string BmsPtmRootPath;
        public string RadarDelay;
        public string PullGoldDelay;
        public string SendTestDelay;
        public string LArcSpeed;
        public string RArcSpeed;
        public string TArcSpeed;
        public string GoldSpeed;
        public string TrayTransDistOffset;
        public string TrayTransDistOffsetPut;
        public string TrayTransDistOffsetPutEmpty;
        public string TestTimeOut;
        public string LeftArmCheckMaxValue;
        public string RightArmCheckMaxValue;
        public string TestArmCheckMaxValue;
    }

    public class SecsGEMVariable
    {
        //TODO:需要在程序中的某个位置给RunCheckList赋值，EAP在进行RunCheck时需要查询这些变量的值
        /// <summary>
        /// RunCheck
        /// </summary>
        public RunCheckEntity RunCheckList = new RunCheckEntity();

        /// <summary>
        /// 设备ID
        /// </summary>
        public string MachineID = "";

        /// <summary>
        /// 设备状态
        /// </summary>
        public string MachineStatus = "";

        /// <summary>
        /// 机台控制模式 OFFLINE:0; ONLINE：1;Local:2; Remote:3;
        /// </summary>
        public string MachineControlMode = "";

        public string WorkMode = "";


        /// <summary>
        /// 工作模式(上料或者下料)LOADING/UNLOADING
        /// </summary>
        public string RunMode = "";

        /// <summary>
        /// 设备工作时间
        /// </summary>
        public int WorkTime = 0;

        /// <summary>
        /// 产出总数
        /// </summary>
        public string OutTotalCount = "";

        /// <summary>
        /// 良品数
        /// </summary>
        public string PassCount = "";

        /// <summary>
        /// 次品数
        /// </summary>
        public string FailCount = "";

        /// <summary>
        /// 总良率
        /// </summary>
        public string Yield = "";

        /// <summary>
        /// UPH
        /// </summary>
        public string UPH = "";

        /// <summary>
        /// 报警率
        /// </summary>
        public string AlarmRate = "";

        /// <summary>
        /// 工作时长
        /// </summary>
        public string WorkHours = "";

        /// <summary>
        /// 工号GH
        /// </summary>
        public string OpID = "";

        /// <summary>
        /// Machine Model Name
        /// </summary>
        public string MDLN = "";

        /// <summary>
        /// 批号
        /// </summary>
        public string LotNumber = "";

        /// <summary>
        /// 料号
        /// </summary>
        public string PN = "";

        /// <summary>
        /// 载入数量
        /// </summary>
        public string ZRSL = "";

        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkSheetNumber = "";

        /// <summary>
        /// 开批时间
        /// </summary>
        public string BatchStartTime = "";

        /// <summary>
        /// 结批时间
        /// </summary>
        public string BatchEndTime = "";

        /// <summary>
        /// 当前配方(program)
        /// </summary>
        public string Recipe = "";

        /// <summary>
        /// 软件版本
        /// </summary>
        public string Version = "";

        /// <summary>
        /// 板子类型
        /// </summary>
        public string BoardType = "";

        /// <summary>
        /// 
        /// </summary>
        public string BoardID = "";

        /// <summary>
        /// EAP返回的MapIndex
        /// </summary>
        public string MapIndex = "";

        /// <summary>
        /// EAP返回的Map列数
        /// </summary>
        public string MapCol = "";

        /// <summary>
        /// EAP返回的Map行数
        /// </summary>
        public string MapRow = "";

        /// <summary>
        /// 上传的Map信息
        /// </summary>
        public string UpLoadMapInfo = "";

        /// <summary>
        /// 上传的MapIndex
        /// </summary>
        public string UpLoadMapIndex = "";


        /// <summary>
        /// 上传的Map数量
        /// </summary>
        public string MapNum = "";

        /// <summary>
        /// 上传的错误信息
        /// </summary>
        public string ErrorMessage = "";

        /// <summary>
        /// SCAN/AUTO
        /// </summary>
        public string ScanMode = "";




        public string CCResult_DutNum = "";


        /// <summary>
        /// Load区投入总量
        /// </summary>
        public string LoadAreaInputCount = "";

        /// <summary>
        /// BIB区投入数量
        /// </summary>
        public string BIBAreaInputCount = "";

        /// <summary>
        /// Out1对应的Bin定义信息
        /// </summary>
        public string Out1BinInfo = "";

        /// <summary>
        /// Out1的IC Error Code数量
        /// </summary>
        public string Out1IC_ErrorCodeCount = "";

        /// <summary>
        /// Out1的IC占总数的比例
        /// </summary>
        public string Out1IC_NGRate = "";

        /// <summary>
        /// Out2对应的Bin定义信息
        /// </summary>
        public string Out2BinInfo = "";

        /// <summary>
        /// Out2的IC Error Code数量
        /// </summary>
        public string Out2IC_ErrorCodeCount = "";

        /// <summary>
        /// Out2的IC占总数的比例
        /// </summary>
        public string Out2IC_NGRate = "";

        /// <summary>
        /// Out3对应的Bin定义信息
        /// </summary>
        public string Out3BinInfo = "";

        /// <summary>
        /// Out3的IC Error Code数量
        /// </summary>
        public string Out3IC_ErrorCodeCount = "";

        /// <summary>
        /// Out3的IC占总数的比例
        /// </summary>
        public string Out3IC_NGRate = "";

        /// <summary>
        /// Out4对应的Bin定义信息
        /// </summary>
        public string Out4BinInfo = "";

        /// <summary>
        /// Out4的IC Error Code数量
        /// </summary>
        public string Out4IC_ErrorCodeCount = "";

        /// <summary>
        /// Out4的IC占总数的比例
        /// </summary>
        public string Out4IC_NGRate = "";
    }
}
