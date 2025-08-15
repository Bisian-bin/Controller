using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Forms.ParamPage
{
    public class ParamData
    {
        public string Language { get; set; }//语言
        public string ActivityMode { get; set; }//运行模式
        public string ProductModel { get; set; }//产品型号
        public bool DoorLock { get; set; }//门禁
        public bool Buzzer { get; set; }//蜂鸣
        public bool Fan { get; set; }//离子风扇
        public bool ICphoto { get; set; }//IC拍照
        public bool TrayModel { get; set; }//Tray横排选择
        public bool SplitBin { get; set; }//分Bin自动抓取
        public bool SecsGem { get; set; }//SecsGem选择
        public bool RandomGeneration { get; set; }//随机生成

        public string BinNum { get; set; }//bin数量

        //轴参数
        public AxisSet AxisSet { get; set; } = new AxisSet();

        // Tray尺寸设置
        public TrayDimensions TrayDimensions { get; set; } = new TrayDimensions();

        // BIBBcard尺寸设置
        public BIBBcardDimensions BIBBcardDimensions { get; set; } = new BIBBcardDimensions();

        // 吸嘴设置
        public Suction Suction { get; set; } = new Suction();

        // 吸真空延时
        public AbsorbSet AbsorbSet { get; set; } = new AbsorbSet();

        //破真空延时

        public BrokenSet BrokenSet { get; set; } = new BrokenSet();

        // 中转站尺寸设置
        public TransferStationDimensions TransferStationDimensions { get; set; } = new TransferStationDimensions();

        // 其他设置
        public OtherSettings OtherSettings { get; set; } = new OtherSettings();

        //分BIN
        public List<BinData> BinDates { get; set; } = new List<BinData>();


        public void GenJson()
        {
            var datas = new ParamData
            {
                Language = ParamInfo.language,
                ActivityMode = ParamInfo.runModel,
                ProductModel = ParamInfo.productModel,
                DoorLock = ParamInfo.door,
                Buzzer = ParamInfo.buzzer,
                Fan = ParamInfo.fan,
                ICphoto = ParamInfo.icPhoto,
                TrayModel = ParamInfo.trayModel,
                SplitBin = ParamInfo.binSplit,
                SecsGem = ParamInfo.secsgem,
                RandomGeneration = ParamInfo.random,
                BinNum = ParamInfo.binNum,

                //轴参数
                AxisSet = new AxisSet
                {
                    Speed = ParamInfo.speed
                },

                // Tray尺寸设置
                TrayDimensions = new TrayDimensions
                {
                    TrayXNum = ParamInfo.trayXnum,
                    TrayYNum = ParamInfo.trayYnum,
                    TrayXSpacing = ParamInfo.trayXspa,
                    TrayYSpacing = ParamInfo.trayYspa,
                    TrayHight = ParamInfo.hight,
                    TrayColInterval = ParamInfo.trayCol,
                    TrayRowInterval = ParamInfo.trayRow,
                    BinColInterval = ParamInfo.binCol,
                    BinRowInterval = ParamInfo.binRow
                },

                // BIBBcard尺寸设置
                BIBBcardDimensions = new BIBBcardDimensions
                {
                    BIBXNum = ParamInfo.bibXnum,
                    BIBYNum = ParamInfo.bibYnum,
                    BIBXSpacing = ParamInfo.bibXspa,
                    BIBYSpacing = ParamInfo.bibYspa,
                    BIBColInterval = ParamInfo.bibCol,
                    BIBRowInterval = ParamInfo.bibRow,
                },

                // 吸嘴设置
                Suction = new Suction
                {
                    LeftSuction = new List<bool> { ParamInfo.Lbin1, ParamInfo.Lbin2, ParamInfo.Lbin3, ParamInfo.Lbin4, ParamInfo.Lbin5 },
                    RightSuction = new List<bool> { ParamInfo.Rbin1, ParamInfo.Rbin2, ParamInfo.Rbin3, ParamInfo.Rbin4, ParamInfo.Rbin5 },
                    TraySuction = new List<bool> { ParamInfo.tray1, ParamInfo.tray2, ParamInfo.tray3, ParamInfo.tray4, ParamInfo.tray5, ParamInfo.tray6, ParamInfo.tray7, ParamInfo.tray8, ParamInfo.tray9, ParamInfo.tray10, ParamInfo.tray11, ParamInfo.tray12 },
                    BIBSuction = new List<bool> { ParamInfo.bib1, ParamInfo.bib2, ParamInfo.bib3, ParamInfo.bib4, ParamInfo.bib5, ParamInfo.bib6, ParamInfo.bib7, ParamInfo.bib8, ParamInfo.bib9, ParamInfo.bib10, ParamInfo.bib11, ParamInfo.bib12 }
                },

                // 吸真空延时设置
                AbsorbSet = new AbsorbSet
                {
                    TrayAbsorb = ParamInfo.trayA,
                    BIBAbsorb = ParamInfo.bibA,
                    LeftBinAbsorb = ParamInfo.leftA,
                    RightBinAbsorb = ParamInfo.rightA,
                },
                //破真空延时设置
                BrokenSet = new BrokenSet
                {
                    TrayBroken = ParamInfo.trayB,
                    BIBBroken = ParamInfo.bibB,
                    LeftBinBroken = ParamInfo.leftB,
                    RightBinBroken = ParamInfo.rightB,
                },

                // 中转站尺寸设置
                TransferStationDimensions = new TransferStationDimensions
                {
                    TransferNum = Convert.ToInt16(ParamInfo.transferNum),
                    TransferSpacing = Convert.ToDouble(ParamInfo.transferSpa)
                },

                // 其他设置
                OtherSettings = new OtherSettings
                {
                    KitName = ParamInfo.kitName,
                    MachineNo = ParamInfo.machineNo
                },

                //分Bin
                BinDates = new List<BinData>
                {
                    new BinData { Name = ParamInfo.bin1Name, Category = ParamInfo.class1, Code = ParamInfo.code1 },
                    new BinData { Name = ParamInfo.bin2Name, Category = ParamInfo.class2, Code = ParamInfo.code2 },
                    new BinData { Name = ParamInfo.bin3Name, Category = ParamInfo.class3, Code = ParamInfo.code3 },
                    new BinData { Name = ParamInfo.bin4Name, Category = ParamInfo.class4, Code = ParamInfo.code4 },
                    new BinData { Name = ParamInfo.bin5Name, Category = ParamInfo.class5, Code = ParamInfo.code5 },
                    new BinData { Name = ParamInfo.bin6Name, Category = ParamInfo.class6, Code = ParamInfo.code6 },
                    new BinData { Name = ParamInfo.bin7Name, Category = ParamInfo.class7, Code = ParamInfo.code7 },
                    new BinData { Name = ParamInfo.bin8Name, Category = ParamInfo.class8, Code = ParamInfo.code8 }
                }

            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(datas, Formatting.Indented);

            File.WriteAllText(ParamInfo.path, json);

        }
    }

    public class AxisSet
    {
        public string Speed { get; set; }
    }
    public class TrayDimensions
    {
        public int TrayXNum { get; set; }
        public int TrayYNum { get; set; }
        public double TrayXSpacing { get; set; }
        public double TrayYSpacing { get; set; }
        public string TrayHight { get; set; }
        public string TrayColInterval { get; set; }
        public string TrayRowInterval { get; set; }
        public string BinColInterval { get; set; }
        public string BinRowInterval { get; set; }
    }

    public class BIBBcardDimensions
    {
        public int BIBXNum { get; set; }
        public int BIBYNum { get; set; }
        public double BIBXSpacing { get; set; }
        public double BIBYSpacing { get; set; }
        public string BIBColInterval { get; set; }
        public string BIBRowInterval { get; set; }

    }

    public class Suction
    {
        public List<bool> LeftSuction { get; set; } = new List<bool>();
        public List<bool> RightSuction { get; set; } = new List<bool>();
        public List<bool> TraySuction { get; set; } = new List<bool>();
        public List<bool> BIBSuction { get; set; } = new List<bool>();
    }

    public class AbsorbSet
    {
        public string TrayAbsorb { get; set; }
        public string BIBAbsorb { get; set; }
        public string LeftBinAbsorb { get; set; }
        public string RightBinAbsorb { get; set; }
    }

    public class BrokenSet
    {
        public string TrayBroken { get; set; }
        public string BIBBroken { get; set; }
        public string LeftBinBroken { get; set; }
        public string RightBinBroken { get; set; }
    }

    public class TransferStationDimensions
    {
        public int TransferNum { get; set; }
        public double TransferSpacing { get; set; }

    }

    public class OtherSettings
    {
        public string KitName { get; set; }
        public string MachineNo { get; set; }
    }
    public class BinData
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
    }


    public class JsonLoader
    {
        public static ParamData LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ParamData>(json);
        }
    }
}
