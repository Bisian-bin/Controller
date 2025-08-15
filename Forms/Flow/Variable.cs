using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Flow
{
    /// <summary>
    /// 变量
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// 结批
        /// </summary>
        public bool ClaeanOut { get; set; }

        #region 扫码变量

        public class ScanStatus
        {
            /// <summary>
            /// 判断是否连线
            /// </summary>
            public bool IsScannerConnected { get; set; }

            /// <summary>
            /// 存储扫码返回
            /// </summary>
            public string ScannedReturnString { get; set; }

            /// <summary>
            /// 扫码发送指令
            /// </summary>
            public string ScannerSendCommand { get; set; }

            /// <summary>
            /// 扫码次数
            /// </summary>
            public int ScanningCount { get; set; }

            /// <summary>
            /// 扫码结果等待时间
            /// </summary>
            public int ScanWaitingTime { get; set; }
        }

        /// <summary>
        /// 扫码变量
        /// </summary>
        public ScanStatus Scanner;

        #endregion


        #region 模式

        /// <summary>
        /// 自动模式
        /// </summary>
        public enum RunModeStatus
        {
            自动模式,
            离线空跑,
            离线带料
        }

        /// <summary>
        /// BIB状态
        /// </summary>
        public struct RunStatus
        {
            public RunModeStatus status;
        }

        public class Pattern
        {
            /// <summary>
            /// 只上料
            /// </summary>
            public bool OnlyLoadEnable { get; set; }

            /// <summary>
            /// 只下料
            /// </summary>
            public bool OnlyUnloadEnable { get; set; }

            /// <summary>
            /// 离线/自动模式
            /// </summary>
            public RunStatus RunMode { get; set; }
        }

        /// <summary>
        /// 模式
        /// </summary>
        public Pattern Mode;

        #endregion

        #region 流程变量

        #region TransferVariable

        /// <summary>
        /// BIB板中转站状态
        /// </summary>
        public enum TransferGridStatus
        {
            /// <summary>
            /// 上/下料完成
            /// </summary>
            绿色,
            /// <summary>
            /// 待上/下料
            /// </summary>
            蓝色,
            /// <summary>
            /// 正在进行上/下料
            /// </summary>
            粉色,
            /// <summary>
            /// 空闲状态
            /// </summary>
            白色
        }

        /// <summary>
        /// BIB状态
        /// </summary>
        public enum BIBGridStatus
        {
            /// <summary>
            /// 屏蔽端口
            /// </summary>
            屏蔽,
            /// <summary>
            /// 允许上料
            /// </summary>
            允许上料,
            /// <summary>
            /// 已经上料
            /// </summary>
            已上料
        }

        /// <summary>
        /// BIB板中转站状态
        /// </summary>
        public struct CtrStatus
        {
            public TransferGridStatus status;
        }

        /// <summary>
        /// BIB状态
        /// </summary>
        public struct BIBStatus
        {
            public BIBGridStatus status;
        }

        public class TransferStatus
        {
            /// <summary>
            /// BIB板中转站GridView
            /// </summary>
            public  CtrStatus[] TransferGrid { get; set; }//new CtrStatus[FlowFactory.recipe.TransferStationDimensions.TransferNum]

            /// <summary>
            /// 中转站锁定某一行
            /// </summary>
            public int TransferCurrentRow { get; set; }

            /// <summary>
            /// BIB上料状态
            /// </summary>
            public BIBStatus[,] BIBGrid { get; set; }

            /// <summary>
            /// BIB下料状态
            /// </summary>
            public string[,] MAPBIBGrid { get; set; }

            /// <summary>
            /// 允许对BIB板进行上下料
            /// </summary>
            public bool AllowActions { get; set; }
        }

        /// <summary>
        /// 中转站+移载变量
        /// </summary>
        public TransferStatus TransferVar;

        #endregion

        #region LeftTrayArm


        /// <summary>
        /// Tray盘状态
        /// </summary>
        public enum TrayGridStatus
        {
            有料,

            无料
        }

        public struct TrayStatus
        {
            public TrayGridStatus status;
        }


        public class LeftTrayArmStatus
        {
            /// <summary>
            /// 吸嘴启用数量
            /// </summary>
            public int NozzleNumber { get; set; }

            /// <summary>
            /// 吸嘴起始位
            /// </summary>
            public int NzlSrtPos { get; set; }

            /// <summary>
            /// BIB移动次数
            /// </summary>
            public int BIBMvtCount { get; set; }

            /// <summary>
            /// 左侧BIB板映射Tray
            /// </summary>
            public int BIBMappedTray { get; set; }

            /// <summary>
            /// 左侧Tray锁定BIB列数
            /// </summary>
            public int BIBCountTray { get; set; }

            /// <summary>
            /// 左侧Tray吸嘴状态
            /// </summary>
            public int[] TrayNozzleStatus { get; set; }

            /// <summary>
            /// 吸嘴优先级
            /// </summary>
            public int NozzlePriority { get; set; }

            /// <summary>
            /// Tray物料状态
            /// </summary>
            public TrayStatus[,] TrayGrid { get; set; }

            /// <summary>
            /// 记录Tray行
            /// </summary>
            public int TrayAreaRows { get; set; }

            /// <summary>
            /// 记录Tray列
            /// </summary>
            public int TrayAreaCells { get; set; }

            /// <summary>
            /// 吸真空等待时间
            /// </summary>
            public int VacuumWaitingTime { get; set; }

            /// <summary>
            /// 上料数量
            /// </summary>
            public int LoadingQuantity { get; set; }

            /// <summary>
            /// 报警等待时间
            /// </summary>
           public int [] AlarmWaitingTime { get; set; }

            /// <summary>
            /// 破真空等待时间
            /// </summary>
           public int VacuumBreakingWaitingTime { get; set; }

            /// <summary>
            /// 允许左侧BIB手臂
            /// </summary>
           public bool BIBAllowActions { get; set; }

        }

        /// <summary>
        /// 左侧手臂
        /// </summary>
        public LeftTrayArmStatus LeftTrayArmVar;

        #endregion



        #region LeftBIBArm
        public class LeftBIBArmStatus
        {
            /// <summary>
            /// 左侧BIB吸嘴状态
            /// </summary>
            public int[] TrayNozzleStatus { get; set; }

            /// <summary>
            ///  吸真空等待时间
            /// </summary>
            public int VacuumWaitingTime { get; set; }


            /// <summary>
            /// 报警等待时间
            /// </summary>
            public int[] AlarmWaitingTime { get; set; }

            /// <summary>
            /// 左侧Tray映射BIB板
            /// </summary>
            public int TrayMappedBIB { get; set; }
        }

        /// <summary>
        /// 左侧BIB手臂
        /// </summary>
        public LeftBIBArmStatus LeftBIBArmVar;

        #endregion

        #endregion

    }

    /// <summary>
    /// 枚举
    /// </summary>
    public class Order
    {
        #region  轴序列号
        public enum AxisOrder
        {
            左1上下料机头X轴 = 1,
            左1上下料机头Z轴 = 2,
            左1上下料机头变距轴 = 3,
            左2上下料机头X轴 = 4,
            左2上下料机头Z轴 = 5,
            左移TrayY轴 = 6,
            左移Tray上顶步进轴前 = 7,
            左移Tray上顶步进轴后 = 8,
            BIB板移栽Y轴 = 9,
            BIB板移栽钩子轴 = 10,
            备用1 = 11,
            备用2 = 12,
            右1下料机头X轴 = 13,
            右1下料机头Z轴 = 14,
            右1下料机头变距轴 = 15,
            右2下料机头X轴 = 16,
            右2下料机头Z轴 = 17,
            右移TrayY轴 = 18,
            右移Tray上顶步进轴前 = 19,
            右移Tray上顶步进轴后 = 20,
            中转站Z轴 = 21,
            备用3 = 22,
        }
        #endregion

        #region IO名称
        /// <summary>
        /// 输入名称
        /// </summary>
        public enum InOrder
        {
            X000_启动,
            X001_停止,
            X002_复位,
            X003_OneCycle,
            X004_CleanOut,
            X005_归零,
            X006_前门禁,
            X007_后门禁,
            X008_左门禁,
            X009_右门禁,
            X010_急停,
            X011_正压,
            X012_负压,
            X013_备用,
            X014_备用,
            X015_备用,
            X016_左1上下料机头1吸真空检测,
            X017_左1上下料机头2吸真空检测,
            X018_左1上下料机头3吸真空检测,
            X019_左1上下料机头4吸真空检测,
            X020_左1上下料机头5吸真空检测,
            X021_左1上下料机头6吸真空检测,
            X022_左1上下料机头7吸真空检测,
            X023_左1上下料机头8吸真空检测,
            X024_左1上下料机头9吸真空检测,
            X025_左1上下料机头10吸真空检测,
            X026_左1上下料机头11吸真空检测,
            X027_左1上下料机头12吸真空检测,
            X028_左2上下料机头1吸真空检测,
            X029_左2上下料机头2吸真空检测,
            X030_左2上下料机头3吸真空检测,
            X031_左2上下料机头4吸真空检测,
            X032_左2上下料机头5吸真空检测,
            X033_左2上下料机头6吸真空检测,
            X034_左2上下料机头7吸真空检测,
            X035_左2上下料机头8吸真空检测,
            X036_左2上下料机头9吸真空检测,
            X037_左2上下料机头10吸真空检测,
            X038_左2上下料机头11吸真空检测,
            X039_左2上下料机头12吸真空检测,
            X040_左飞梭检测产品感应1,
            X041_左飞梭检测产品感应2,
            X042_左飞梭检测产品感应3,
            X043_左飞梭检测产品感应4,
            X044_左飞梭检测产品感应5,
            X045_左飞梭检测产品感应6,
            X046_左飞梭检测产品感应7,
            X047_左飞梭检测产品感应8,
            X048_左飞梭检测产品感应9,
            X049_左飞梭检测产品感应10,
            X050_左飞梭检测产品感应11,
            X051_左飞梭检测产品感应12,
            X052_左移Tray定位气缸回1,
            X053_左移Tray定位气缸出,
            X054_左移Tray定位气缸回2,
            X055_左移Tray盘子有无检测,
            X056_左分Tray定位气缸回1,
            X057_左分Tray定位气缸出1,
            X058_左分Tray定位气缸回2,
            X059_左分Tray定位气缸出2,
            X060_左放Tray无盘信号,
            X061_左放Tray满盘信号,
            X062_左收Tray无盘信号,
            X063_左收Tray满盘信号,
            X064_左前检测有无,
            X065_左后检测有无,
            X066_左收Tray左右防呆,
            X067_左收Tray前后防呆,
            X068_左2机头防掉料感应,
            X069_左2机头开盖气缸回感应,
            X070_左2机头开盖气缸出感应,
            X071_左1机头防撞感应1,
            X072_左1机头防撞感应2,
            X073_左2机头防撞感应1,
            X074_左2机头防撞感应2,
            X075_备用,
            X076_备用,
            X077_备用,
            X078_备用,
            X079_备用,
            X080_右1上下料机头1吸真空检测,
            X081_右1上下料机头2吸真空检测,
            X082_右1上下料机头3吸真空检测,
            X083_右1上下料机头4吸真空检测,
            X084_右1上下料机头5吸真空检测,
            X085_右1上下料机头6吸真空检测,
            X086_右1上下料机头7吸真空检测,
            X087_右1上下料机头8吸真空检测,
            X088_右1上下料机头9吸真空检测,
            X089_右1上下料机头10吸真空检测,
            X090_右1上下料机头11吸真空检测,
            X091_右1上下料机头12吸真空检测,
            X092_右2上下料机头1吸真空检测,
            X093_右2上下料机头2吸真空检测,
            X094_右2上下料机头3吸真空检测,
            X095_右2上下料机头4吸真空检测,
            X096_右2上下料机头5吸真空检测,
            X097_右2上下料机头6吸真空检测,
            X098_右2上下料机头7吸真空检测,
            X099_右2上下料机头8吸真空检测,
            X100_右2上下料机头9吸真空检测,
            X101_右2上下料机头10吸真空检测,
            X102_右2上下料机头11吸真空检测,
            X103_右2上下料机头12吸真空检测,
            X104_右飞梭检测产品感应1,
            X105_右飞梭检测产品感应2,
            X106_右飞梭检测产品感应3,
            X107_右飞梭检测产品感应4,
            X108_右飞梭检测产品感应5,
            X109_右飞梭检测产品感应6,
            X110_右飞梭检测产品感应7,
            X111_右飞梭检测产品感应8,
            X112_右飞梭检测产品感应9,
            X113_右飞梭检测产品感应10,
            X114_右飞梭检测产品感应11,
            X115_右飞梭检测产品感应12,
            X116_右移Tray定位气缸回1,
            X117_右移Tray定位气缸出,
            X118_右移Tray定位气缸回2,
            X119_右移Tray盘子有无检测,
            X120_右分Tray定位气缸回1,
            X121_右分Tray定位气缸出1,
            X122_右分Tray定位气缸回2,
            X123_右分Tray定位气缸出2,
            X124_右放Tray无盘信号,
            X125_右放Tray满盘信号,
            X126_右收Tray无盘信号,
            X127_右收Tray满盘信号,
            X128_右前检测有无,
            X129_右后检测有无,
            X130_右收Tray左右防呆,
            X131_右收Tray前后防呆,
            X132_右2机头防掉料感应,
            X133_右2机头开盖气缸回感应,
            X134_右2机头开盖气缸出感应,
            X135_右1机头防撞感应1,
            X136_右1机头防撞感应2,
            X137_右2机头防撞感应1,
            X138_右2机头防撞感应2,
            X139_备用,
            X140_备用,
            X141_备用,
            X142_移栽滚轮气缸回位,
            X143_移栽滚轮气缸出位,
            X144_移载上顶气缸感应器下,
            X145_移载上顶气缸感应器上,
            X146_移载侧定位气缸感应器回,
            X147_移载侧定位气缸感应器出,
            X148_移载前定位气缸感应器回,
            X149_移载前定位气缸感应器出,
            X150_移载后定位气缸感应器回,
            X151_移载后定位气缸感应器出,
            X152_移载钩子上顶气缸感应器回,
            X153_移载钩子上顶气缸感应器出,
            X154_升降机BIB板阻挡气缸下,
            X155_升降机BIB板阻挡气缸上,
            X156_升降机BIB板平推气缸回,
            X157_升降机BIB板平推气缸出,
            X158_升降机两边打BIB板气缸回,
            X159_升降机两边打BIB板气缸出,
            X160_移载BIB到位检测前感应器,
            X161_移载BIB到位检测后感应器,
            X162_升降机BIB板进料左防呆感应,
            X163_升降机BIB板进料右防呆感应,
            X164_升降机BIB板挡条防卡板感应,
            X165_升降机上层按钮,
            X166_升降机下层按钮,
            X167_升降机移栽平台防卡板感应,
            X168_备用,
            X169_分Bin机头1吸真空检测,
            X170_分Bin机头2吸真空检测,
            X171_BinA有Tray检测,
            X172_BinB有Tray检测,
            X173_BinC有Tray检测,
            X174_BinD有Tray检测,
            X175_备用,
            X176_1号离子风扇通电信号,
            X177_1号离子风扇报警信号,
            X178_2号离子风扇通电信号,
            X179_2号离子风扇报警信号,
            X180_3号离子风扇通电信号,
            X181_3号离子风扇报警信号,
            X182_4号离子风扇通电信号,
            X183_4号离子风扇报警信号,
            X184_5号离子风扇通电信号,
            X185_5号离子风扇报警信号,
            X186_6号离子风扇通电信号,
            X187_6号离子风扇报警信号,
            X188_备用,
            X189_备用,
            X190_备用,
            X191_备用,
            X192_备用,
            X193_备用,
            X194_备用,
            X195_备用,
            X196_备用,
            X197_备用,
            X198_备用,
            X199_备用,
            X200_备用,
            X201_备用,
            X202_备用,
            X203_备用,
            X204_备用,
            X205_备用,
            X206_备用,
            X207_备用,
        }

        /// <summary>
        /// 输出名称
        /// </summary>
        public enum OutOrder
        {
            Y000_启动灯,
            Y001_停止灯,
            Y002_复位灯,
            Y003_OneCycle灯,
            Y004_CleanOut灯,
            Y005_归零灯,
            Y006_红灯,
            Y007_黄灯,
            Y008_绿灯,
            Y009_蜂鸣器,
            Y010_左1上下料机头1吸真空,
            Y011_左1上下料机头2吸真空,
            Y012_左1上下料机头3吸真空,
            Y013_左1上下料机头4吸真空,
            Y014_左1上下料机头5吸真空,
            Y015_左1上下料机头6吸真空,
            Y016_左1上下料机头7吸真空,
            Y017_左1上下料机头8吸真空,
            Y018_左1上下料机头9吸真空,
            Y019_左1上下料机头10吸真空,
            Y020_左1上下料机头11吸真空,
            Y021_左1上下料机头12吸真空,
            Y022_左1上下料机头1破真空,
            Y023_左1上下料机头2破真空,
            Y024_左1上下料机头3破真空,
            Y025_左1上下料机头4破真空,
            Y026_左1上下料机头5破真空,
            Y027_左1上下料机头6破真空,
            Y028_左1上下料机头7破真空,
            Y029_左1上下料机头8破真空,
            Y030_左1上下料机头9破真空,
            Y031_左1上下料机头10破真空,
            Y032_左1上下料机头11破真空,
            Y033_左1上下料机头12破真空,
            Y034_左1上下料机头1吸嘴气缸出,
            Y035_左1上下料机头2吸嘴气缸出,
            Y036_左1上下料机头3吸嘴气缸出,
            Y037_左1上下料机头4吸嘴气缸出,
            Y038_左1上下料机头5吸嘴气缸出,
            Y039_左1上下料机头6吸嘴气缸出,
            Y040_左1上下料机头7吸嘴气缸出,
            Y041_左1上下料机头8吸嘴气缸出,
            Y042_左1上下料机头9吸嘴气缸出,
            Y043_左1上下料机头10吸嘴气缸出,
            Y044_左1上下料机头11吸嘴气缸出,
            Y045_左1上下料机头12吸嘴气缸出,
            Y046_左2上下料机头1吸真空,
            Y047_左2上下料机头2吸真空,
            Y048_左2上下料机头3吸真空,
            Y049_左2上下料机头4吸真空,
            Y050_左2上下料机头5吸真空,
            Y051_左2上下料机头6吸真空,
            Y052_左2上下料机头7吸真空,
            Y053_左2上下料机头8吸真空,
            Y054_左2上下料机头9吸真空,
            Y055_左2上下料机头10吸真空,
            Y056_左2上下料机头11吸真空,
            Y057_左2上下料机头12吸真空,
            Y058_左2上下料机头1破真空,
            Y059_左2上下料机头2破真空,
            Y060_左2上下料机头3破真空,
            Y061_左2上下料机头4破真空,
            Y062_左2上下料机头5破真空,
            Y063_左2上下料机头6破真空,
            Y064_左2上下料机头7破真空,
            Y065_左2上下料机头8破真空,
            Y066_左2上下料机头9破真空,
            Y067_左2上下料机头10破真空,
            Y068_左2上下料机头11破真空,
            Y069_左2上下料机头12破真空,
            Y070_左2上下料机头1吸嘴气缸出,
            Y071_左2上下料机头2吸嘴气缸出,
            Y072_左2上下料机头3吸嘴气缸出,
            Y073_左2上下料机头4吸嘴气缸出,
            Y074_左2上下料机头5吸嘴气缸出,
            Y075_左2上下料机头6吸嘴气缸出,
            Y076_左2上下料机头7吸嘴气缸出,
            Y077_左2上下料机头8吸嘴气缸出,
            Y078_左2上下料机头9吸嘴气缸出,
            Y079_左2上下料机头10吸嘴气缸出,
            Y080_左2上下料机头11吸嘴气缸出,
            Y081_左2上下料机头12吸嘴气缸出,
            Y082_左移Tray定位气缸出,
            Y083_左分Tray定位气缸出,
            Y084_左2机头开盖气缸出,
            Y085_左2机头开盖气缸回,
            Y086_备用,
            Y087_备用,
            Y088_备用,
            Y089_备用,
            Y090_右1上下料机头1吸真空,
            Y091_右1上下料机头2吸真空,
            Y092_右1上下料机头3吸真空,
            Y093_右1上下料机头4吸真空,
            Y094_右1上下料机头5吸真空,
            Y095_右1上下料机头6吸真空,
            Y096_右1上下料机头7吸真空,
            Y097_右1上下料机头8吸真空,
            Y098_右1上下料机头9吸真空,
            Y099_右1上下料机头10吸真空,
            Y100_右1上下料机头11吸真空,
            Y101_右1上下料机头12吸真空,
            Y102_右1上下料机头1破真空,
            Y103_右1上下料机头2破真空,
            Y104_右1上下料机头3破真空,
            Y105_右1上下料机头4破真空,
            Y106_右1上下料机头5破真空,
            Y107_右1上下料机头6破真空,
            Y108_右1上下料机头7破真空,
            Y109_右1上下料机头8破真空,
            Y110_右1上下料机头9破真空,
            Y111_右1上下料机头10破真空,
            Y112_右1上下料机头11破真空,
            Y113_右1上下料机头12破真空,
            Y114_右1上下料机头1吸嘴气缸出,
            Y115_右1上下料机头2吸嘴气缸出,
            Y116_右1上下料机头3吸嘴气缸出,
            Y117_右1上下料机头4吸嘴气缸出,
            Y118_右1上下料机头5吸嘴气缸出,
            Y119_右1上下料机头6吸嘴气缸出,
            Y120_右1上下料机头7吸嘴气缸出,
            Y121_右1上下料机头8吸嘴气缸出,
            Y122_右1上下料机头9吸嘴气缸出,
            Y123_右1上下料机头10吸嘴气缸出,
            Y124_右1上下料机头11吸嘴气缸出,
            Y125_右1上下料机头12吸嘴气缸出,
            Y126_右2上下料机头1吸真空,
            Y127_右2上下料机头2吸真空,
            Y128_右2上下料机头3吸真空,
            Y129_右2上下料机头4吸真空,
            Y130_右2上下料机头5吸真空,
            Y131_右2上下料机头6吸真空,
            Y132_右2上下料机头7吸真空,
            Y133_右2上下料机头8吸真空,
            Y134_右2上下料机头9吸真空,
            Y135_右2上下料机头10吸真空,
            Y136_右2上下料机头11吸真空,
            Y137_右2上下料机头12吸真空,
            Y138_右2上下料机头1破真空,
            Y139_右2上下料机头2破真空,
            Y140_右2上下料机头3破真空,
            Y141_右2上下料机头4破真空,
            Y142_右2上下料机头5破真空,
            Y143_右2上下料机头6破真空,
            Y144_右2上下料机头7破真空,
            Y145_右2上下料机头8破真空,
            Y146_右2上下料机头9破真空,
            Y147_右2上下料机头10破真空,
            Y148_右2上下料机头11破真空,
            Y149_右2上下料机头12破真空,
            Y150_右2上下料机头1吸嘴气缸出,
            Y151_右2上下料机头2吸嘴气缸出,
            Y152_右2上下料机头3吸嘴气缸出,
            Y153_右2上下料机头4吸嘴气缸出,
            Y154_右2上下料机头5吸嘴气缸出,
            Y155_右2上下料机头6吸嘴气缸出,
            Y156_右2上下料机头7吸嘴气缸出,
            Y157_右2上下料机头8吸嘴气缸出,
            Y158_右2上下料机头9吸嘴气缸出,
            Y159_右2上下料机头10吸嘴气缸出,
            Y160_右2上下料机头11吸嘴气缸出,
            Y161_右2上下料机头12吸嘴气缸出,
            Y162_右移Tray定位气缸出,
            Y163_右分Tray定位气缸出,
            Y164_右2机头开盖气缸出,
            Y165_右2机头开盖气缸回,
            Y166_备用,
            Y167_备用,
            Y168_备用,
            Y169_备用,
            Y170_分BIN上下料机头1吸真空,
            Y171_分BIN上下料机头2吸真空,
            Y172_分BIN上下料机头1破真空,
            Y173_分BIN上下料机头2破真空,
            Y174_分BIN下料机头1吸嘴气缸下,
            Y175_分BIN下料机头2吸嘴气缸下,
            Y176_中转站BIB板推送电机正转,
            Y177_中转站BIB板推送电机反转,
            Y178_移载上顶气缸出,
            Y179_移载侧定位气缸出,
            Y180_移载前定位气缸出,
            Y181_移载后定位气缸出,
            Y182_移载钩子上顶气缸出,
            Y183_移载滚轮气缸回,
            Y184_升降机BIB板阻挡气缸上升,
            Y185_升降机BIB板平推气缸出,
            Y186_升降机两边打BIB板气缸出,
            Y187_备用,
            Y188_备用,
            Y189_备用,
            Y190_备用,
            Y191_报警音乐1,
            Y192_报警音乐2,
            Y193_报警音乐3,
            Y194_报警音乐4,
            Y195_报警音乐5,
            Y196_光源输出信号1,
            Y197_光源输出信号2,
            Y198_光源输出信号3,
            Y199_光源输出信号4,
            Y200_光源输出信号5,
            Y201_光源输出信号6,
            Y202_光源输出信号7,
            Y203_备用,
            Y204_备用,
            Y205_备用,

        }
        #endregion

        #region 设备状态
        public enum MachineStatuOrder
        {
            /// <summary>
            /// 报警复位
            /// </summary>
            Reset,
            /// <summary>
            /// 初始状态
            /// </summary>
            InitialState,
            /// <summary>
            /// 归零中
            /// </summary>
            Homing,
            /// <summary>
            /// 运行中
            /// </summary>
            Running,
            /// <summary>
            /// 报警中 
            /// </summary>
            Alarm,
            /// <summary>
            /// 暂停中
            /// </summary>
            Pause,
            /// <summary>
            /// 急停中
            /// </summary>
            EStop,
            /// <summary>
            /// 归零完成
            /// </summary>
            HomeCompleted
        }

        #endregion
    }

}
