using HsmsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Forms
{


    [Serializable]
    public class LotMessageEntity
    {
        public string LotID;
        public string Mode;
    }

    public class PPSelectEntity
    {
        public string Formula;
    }

    [Serializable]
    public class SVIDEntity
    {
        public string ObjName;//对象名称
        public HsmsClass.SECSDataType ObjType;//对象数值类型
        public object objValue;
    }

    public class HSMS
    {
        public static event Action<LotMessageEntity> NewLot;
        public static event Action<LotMessageEntity> EndLot;
        public static event Action<PPSelectEntity> UpdatePGM;
        public static event Action<bool> UpdateRdo;
        public static event Action<object> Start;
        public static event Action<object> Stop;
        public static event Action<string> ShowMsg;
        public static event Action<string[]> RunCheck;

        public static bool m_bCommEnabled = false;//允许通讯
        public static bool isRunCheck = false;//RunCheck
        public static bool SecsGemOnLine = true;//Online模式

        private Dictionary<string, SVIDEntity> dic = new Dictionary<string, SVIDEntity>();

        MovData Redata = new MovData();

        public ExternalClass external = new ExternalClass();

        public string IP = "127.0.0.1";

        //TODO:临时Recipe路径配置
        public string RecipePath = Application.StartupPath + "\\res\\Recipe\\";

        public string Port = "5555";

        public string[] strMachine;

        //FrmSecsGem frm = new FrmSecsGem();

        public int T3 = 5;
        public int T5 = 5;
        public int T6 = 5;
        public int T7 = 10;
        public int T8 = 5;
        public int TL = 5;

        /// <summary>
        /// 连接状态
        /// </summary>
        public bool ConnectStatus = false;

        public HSMS()
        {
            dic.Add("1000", new SVIDEntity()
            {
                ObjName = "机台运行状态",
                ObjType = HsmsClass.SECSDataType.UINT_2,
                objValue = GetSVID(1000)
            });

            dic.Add("1001", new SVIDEntity()
            {
                ObjName = "机台控制模式",
                ObjType = HsmsClass.SECSDataType.UINT_2,
                objValue = GetSVID(1001)
            });

            dic.Add("1002", new SVIDEntity()
            {
                ObjName = "机台工作模式",
                ObjType = HsmsClass.SECSDataType.UINT_2,
                objValue = GetSVID(1002)
            });
            dic.Add("1003", new SVIDEntity()
            {
                ObjName = "机台工作时间",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1003)
            });
            dic.Add("1004", new SVIDEntity()
            {
                ObjName = "批次号",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1004)
            });
            dic.Add("1005", new SVIDEntity()
            {
                ObjName = "操作员",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1005)
            });
            dic.Add("1006", new SVIDEntity()
            {
                ObjName = "Recipe名称",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1006)
            });
            dic.Add("1007", new SVIDEntity()
            {
                ObjName = "机台软件版本",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1007)
            });
            dic.Add("1008", new SVIDEntity()
            {
                ObjName = "Machine Model name",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1008)
            });
            dic.Add("1009", new SVIDEntity()
            {
                ObjName = "MachineID",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1009)
            });
            dic.Add("1010", new SVIDEntity()
            {
                ObjName = "Total Count",
                ObjType = HsmsClass.SECSDataType.UINT_2,
                objValue = GetSVID(1010)
            });
            dic.Add("1011", new SVIDEntity()
            {
                ObjName = "已测试数量",
                ObjType = HsmsClass.SECSDataType.UINT_2,
                objValue = GetSVID(1011)
            });
            dic.Add("1012", new SVIDEntity()
            {
                ObjName = "好品数量",
                ObjType = HsmsClass.SECSDataType.UINT_2,
                objValue = GetSVID(1012)
            });
            dic.Add("1013", new SVIDEntity()
            {
                ObjName = "次品数量",
                ObjType = HsmsClass.SECSDataType.UINT_2,
                objValue = GetSVID(1013)
            });
            dic.Add("1014", new SVIDEntity()
            {
                ObjName = "良率",
                ObjType = HsmsClass.SECSDataType.FT_4,
                objValue = GetSVID(1014)
            });
            dic.Add("1015", new SVIDEntity()
            {
                ObjName = "料号",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1015)
            });
            dic.Add("1016", new SVIDEntity()
            {
                ObjName = "工单号",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1016)
            });
            dic.Add("1017", new SVIDEntity()
            {
                ObjName = "UPH",
                ObjType = HsmsClass.SECSDataType.UINT_2,
                objValue = GetSVID(1017)
            });
            dic.Add("1018", new SVIDEntity()
            {
                ObjName = "工作时长",
                ObjType = HsmsClass.SECSDataType.UINT_2,
                objValue = GetSVID(1018)
            });
            dic.Add("1019", new SVIDEntity()
            {
                ObjName = "开始时间",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1019)
            });
            dic.Add("1020", new SVIDEntity()
            {
                ObjName = "结束时间",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1020)
            });

            //RunCheck SVID
            dic.Add("1101", new SVIDEntity()
            {
                ObjName = "Name",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1101)
            });
            dic.Add("1102", new SVIDEntity()
            {
                ObjName = "MCName",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1102)
            });
            dic.Add("1103", new SVIDEntity()
            {
                ObjName = "ShuttleStepX",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1103)
            });
            dic.Add("1104", new SVIDEntity()
            {
                ObjName = "ShuttleStepY",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1104)
            });
            dic.Add("1105", new SVIDEntity()
            {
                ObjName = "ShuttleXCount",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1105)
            });
            dic.Add("1106", new SVIDEntity()
            {
                ObjName = "ShuttleYCount",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1106)
            });
            dic.Add("1107", new SVIDEntity()
            {
                ObjName = "TrayStepX",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1107)
            });
            dic.Add("1108", new SVIDEntity()
            {
                ObjName = "TrayStepY",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1108)
            });
            dic.Add("1109", new SVIDEntity()
            {
                ObjName = "TrayXCount",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1109)
            });
            dic.Add("1110", new SVIDEntity()
            {
                ObjName = "TrayYCount",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1110)
            });
            dic.Add("1111", new SVIDEntity()
            {
                ObjName = "BIBStepX",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1111)
            });
            dic.Add("1112", new SVIDEntity()
            {
                ObjName = "BIBStepX2",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1112)
            });
            dic.Add("1113", new SVIDEntity()
            {
                ObjName = "BIBStepY",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1113)
            });
            dic.Add("1114", new SVIDEntity()
            {
                ObjName = "BIBStepXPick",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1114)
            });
            dic.Add("1115", new SVIDEntity()
            {
                ObjName = "BIBStepX2Pick",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1115)
            });
            dic.Add("1116", new SVIDEntity()
            {
                ObjName = "BIBStepYPick",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1116)
            });
            dic.Add("1117", new SVIDEntity()
            {
                ObjName = "BIBXCount",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1117)
            });
            dic.Add("1118", new SVIDEntity()
            {
                ObjName = "BIBYCount",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1118)
            });
            dic.Add("1119", new SVIDEntity()
            {
                ObjName = "SthR",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1119)
            });
            dic.Add("1120", new SVIDEntity()
            {
                ObjName = "ScloseR",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1120)
            });
            dic.Add("1121", new SVIDEntity()
            {
                ObjName = "BibPickCount",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1121)
            });
            dic.Add("1122", new SVIDEntity()
            {
                ObjName = "Bin1Name",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1122)
            });
            dic.Add("1123", new SVIDEntity()
            {
                ObjName = "Bin1Flag",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1123)
            });
            dic.Add("1124", new SVIDEntity()
            {
                ObjName = "Bin1Code",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1124)
            });
            dic.Add("1125", new SVIDEntity()
            {
                ObjName = "Bin2Name",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1125)
            });
            dic.Add("1126", new SVIDEntity()
            {
                ObjName = "Bin2Flag",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1126)
            });
            dic.Add("1127", new SVIDEntity()
            {
                ObjName = "Bin2Code",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1127)
            });
            dic.Add("1128", new SVIDEntity()
            {
                ObjName = "Bin3Name",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1128)
            });
            dic.Add("1129", new SVIDEntity()
            {
                ObjName = "Bin3Flag",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1129)
            });
            dic.Add("1130", new SVIDEntity()
            {
                ObjName = "Bin3Code",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1130)
            });
            dic.Add("1131", new SVIDEntity()
            {
                ObjName = "TesterIp",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1131)
            });
            dic.Add("1132", new SVIDEntity()
            {
                ObjName = "ShareRootPath",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1132)
            });
            dic.Add("1133", new SVIDEntity()
            {
                ObjName = "TBadMax",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1133)
            });
            dic.Add("1134", new SVIDEntity()
            {
                ObjName = "TChangeNum",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1134)
            });
            dic.Add("1135", new SVIDEntity()
            {
                ObjName = "TPassBin",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1135)
            });
            dic.Add("1136", new SVIDEntity()
            {
                ObjName = "TFailBin",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1136)
            });
            dic.Add("1137", new SVIDEntity()
            {
                ObjName = "TErrorBin",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1137)
            });
            dic.Add("1138", new SVIDEntity()
            {
                ObjName = "BmsBindRootPath",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1138)
            });
            dic.Add("1139", new SVIDEntity()
            {
                ObjName = "BmsCompareRootPath",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1139)
            });
            dic.Add("1140", new SVIDEntity()
            {
                ObjName = "BmsMapRootPath",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1140)
            });
            dic.Add("1141", new SVIDEntity()
            {
                ObjName = "BmsPtmRootPath",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1141)
            });
            dic.Add("1142", new SVIDEntity()
            {
                ObjName = "RadarDelay",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1142)
            });
            dic.Add("1143", new SVIDEntity()
            {
                ObjName = "PullGoldDelay",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1143)
            });
            dic.Add("1144", new SVIDEntity()
            {
                ObjName = "SendTestDelay",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1144)
            });
            dic.Add("1145", new SVIDEntity()
            {
                ObjName = "LArcSpeed",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1145)
            });
            dic.Add("1146", new SVIDEntity()
            {
                ObjName = "RArcSpeed",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1146)
            });
            dic.Add("1147", new SVIDEntity()
            {
                ObjName = "TArcSpeed",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1147)
            });
            dic.Add("1148", new SVIDEntity()
            {
                ObjName = "GoldSpeed",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1148)
            });
            dic.Add("1149", new SVIDEntity()
            {
                ObjName = "TrayTransDistOffset",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1149)
            });
            dic.Add("1150", new SVIDEntity()
            {
                ObjName = "TrayTransDistOffsetPut",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1150)
            });
            dic.Add("1151", new SVIDEntity()
            {
                ObjName = "TrayTransDistOffsetPutEmpty",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1151)
            });
            dic.Add("1152", new SVIDEntity()
            {
                ObjName = "TestTimeOut",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1152)
            });
            dic.Add("1153", new SVIDEntity()
            {
                ObjName = "TestTimeOut",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1153)
            });
            dic.Add("1154", new SVIDEntity()
            {
                ObjName = "TestTimeOut",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1154)
            });
            dic.Add("1155", new SVIDEntity()
            {
                ObjName = "TestTimeOut",
                ObjType = HsmsClass.SECSDataType.ASCII,
                objValue = GetSVID(1155)
            });
        }

        private string GetSVID(int iSVID)
        {
            Parameter_Secs(iSVID - 1000);
            return Data_Secs[iSVID - 1000];
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            Publisher.Subscriber_S2F41 += new Publisher.MyEntrust(S2F41);
            Publisher.Subscriber_S1F1 += new Publisher.MyEntrust(S1F1);
            Publisher.Subscriber_S1F3 += new Publisher.MyEntrust(S1F3);
            Publisher.Subscriber_S1F13 += new Publisher.MyEntrust(S1F13);
            Publisher.Subscriber_S1F15 += new Publisher.MyEntrust(S1F15);
            Publisher.Subscriber_S1F17 += new Publisher.MyEntrust(S1F17);
            Publisher.Subscriber_S10F3 += new Publisher.MyEntrust(S10F3);
            Publisher.Subscriber_S5F3 += new Publisher.MyEntrust(S5F3);
            Publisher.Subscriber_S2F31 += new Publisher.MyEntrust(S2F31);
            Publisher.Subscriber_S2F33 += new Publisher.MyEntrust(S2F33);
            Publisher.Subscriber_S2F35 += new Publisher.MyEntrust(S2F35);
            Publisher.Subscriber_S2F37 += new Publisher.MyEntrust(S2F37);
            Publisher.Subscriber_S7F1 += new Publisher.MyEntrust(S7F1);
            Publisher.Subscriber_S7F3 += new Publisher.MyEntrust(S7F3);
            Publisher.Subscriber_S7F5 += new Publisher.MyEntrust(S7F5);
            Publisher.Subscriber_S7F19 += new Publisher.MyEntrust(S7F19);
            Publisher.Subscriber_S7F17 += new Publisher.MyEntrust(S7F17);
            external.Close();
            ConnectStatus = false;

            if (external.Connect(IP, Port, "0", TL, true, T3, T5, T6, T7, T8))
            {
                ConnectStatus = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisConnect()
        {
            Publisher.Subscriber_S2F41 -= new Publisher.MyEntrust(S2F41);
            Publisher.Subscriber_S1F1 -= new Publisher.MyEntrust(S1F1);
            Publisher.Subscriber_S1F3 -= new Publisher.MyEntrust(S1F3);
            Publisher.Subscriber_S1F13 -= new Publisher.MyEntrust(S1F13);
            Publisher.Subscriber_S10F3 -= new Publisher.MyEntrust(S10F3);
            Publisher.Subscriber_S1F15 -= new Publisher.MyEntrust(S1F15);
            Publisher.Subscriber_S1F17 -= new Publisher.MyEntrust(S1F17);
            Publisher.Subscriber_S5F3 -= new Publisher.MyEntrust(S5F3);
            Publisher.Subscriber_S2F31 -= new Publisher.MyEntrust(S2F31);
            Publisher.Subscriber_S2F33 -= new Publisher.MyEntrust(S2F33);
            Publisher.Subscriber_S2F35 -= new Publisher.MyEntrust(S2F35);
            Publisher.Subscriber_S2F37 -= new Publisher.MyEntrust(S2F37);

            Publisher.Subscriber_S7F1 -= new Publisher.MyEntrust(S7F1);
            Publisher.Subscriber_S7F3 -= new Publisher.MyEntrust(S7F3);
            Publisher.Subscriber_S7F5 -= new Publisher.MyEntrust(S7F5);
            Publisher.Subscriber_S7F19 -= new Publisher.MyEntrust(S7F19);
            Publisher.Subscriber_S7F17 -= new Publisher.MyEntrust(S7F17);

            ConnectStatus = false;

            external.Close();
        }


        public void S2F41(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            string strMsg = "";
            try
            {
                XmlNode xln = xml.ChildNodes[0];

                string strRCMD = xln.ChildNodes[1].InnerText;

                XmlNodeList xnl = xln.ChildNodes[2].ChildNodes;

                string strResult = "";
                string strReason = "";
                switch (strRCMD.ToUpper())
                {
                    case "RECIPEVERIFYACK":
                        {
                            foreach (XmlNode xl in xnl)
                            {
                                if (xl.ChildNodes.Count == 3)
                                {
                                    switch (xl.ChildNodes[1].InnerText.ToUpper())
                                    {
                                        case "RESULT":
                                            strResult = xl.ChildNodes[2].InnerText.Trim();
                                            break;
                                        case "REASON":
                                            strReason = xl.ChildNodes[2].InnerText.Trim();
                                            break;
                                    }
                                }
                            }
                            if (RunCheck != null)
                            {
                                RunCheck(new string[] { strResult, strReason });
                            }
                            break;
                        }
                    case "RUNCHECKEND":
                        {
                            foreach (XmlNode xl in xnl)
                            {
                                if (xl.ChildNodes.Count == 3)
                                {
                                    switch (xl.ChildNodes[1].InnerText.ToUpper())
                                    {
                                        case "RESULT":
                                            strResult = xl.ChildNodes[2].InnerText.Trim();
                                            break;
                                        case "REASON":
                                            strReason = xl.ChildNodes[2].InnerText.Trim();
                                            break;
                                    }
                                }
                            }
                            if (RunCheck != null)
                            {
                                RunCheck(new string[] { strResult, strReason });
                            }
                            break;
                        }
                    case "PPSELECT":
                        {
                            PPSelectEntity sel = new PPSelectEntity();
                            foreach (XmlNode xl in xnl)
                            {
                                if (xl.ChildNodes.Count == 3)
                                {
                                    switch (xl.ChildNodes[1].InnerText.ToUpper())
                                    {
                                        case "PP_NAME":
                                            sel.Formula = xl.ChildNodes[2].InnerText.Trim();
                                            break;
                                    }
                                }
                            }

                            //TODO:Recipe临时目录(需变更)
                            bool isMatch = false;
                            string[] files = Directory.GetFiles(RecipePath, "*.json");
                            foreach (string file in files)
                            {
                                if (!sel.Formula.Contains(".json"))
                                {
                                    sel.Formula += ".json";
                                }
                                if (file.Split('\\')[file.Split('\\').Length - 1] == sel.Formula)
                                {
                                    if (UpdatePGM != null)
                                    {
                                        UpdatePGM(sel);
                                    }

                                    //返回true
                                    strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F42", strSQN, "0", "00");
                                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 1, null);
                                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 2, "0");
                                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                                    external.TcpServer.SendSECSIIMessage(strMsg);
                                    isMatch = true;
                                    break;
                                }
                            }
                            if (!isMatch)
                            {
                                //返回false
                                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F42", strSQN, "0", "00");
                                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 1, null);
                                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, "No match file names were found".Length, "No match file names were found");
                                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                                external.TcpServer.SendSECSIIMessage(strMsg);
                            }
                            break;
                        }
                    case "START":
                        {
                            if (Start != null)
                            {
                                Start(1);
                            }

                            //返回true
                            strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F42", strSQN, "0", "00");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 1, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 2, "0");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.TcpServer.SendSECSIIMessage(strMsg);
                            break;
                        }
                    case "STOP":
                        {
                            if (Stop != null)
                            {
                                Stop(1);
                            }

                            //返回true
                            strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F42", strSQN, "0", "00");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 1, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 2, "0");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.TcpServer.SendSECSIIMessage(strMsg);
                            break;
                        }
                    case "NEWLOT":
                        {
                            LotMessageEntity ppety = new LotMessageEntity();
                            foreach (XmlNode xl in xnl)
                            {
                                if (xl.ChildNodes.Count == 3)
                                {
                                    switch (xl.ChildNodes[1].InnerText.ToUpper())
                                    {
                                        case "LOTID":
                                            ppety.LotID = xl.ChildNodes[2].InnerText.Trim();
                                            break;

                                        case "MODE":
                                            ppety.Mode = xl.ChildNodes[2].InnerText.Trim();
                                            break;
                                    }
                                }
                            }

                            if (NewLot != null)
                            {
                                NewLot(ppety);
                            }
                            strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F42", strSQN, "0", "00");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 1, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 1, "0");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.TcpServer.SendSECSIIMessage(strMsg);
                            break;
                        }
                    case "ENDLOT":
                        {
                            LotMessageEntity ppety = new LotMessageEntity();
                            foreach (XmlNode xl in xnl)
                            {
                                if (xl.ChildNodes.Count == 3)
                                {
                                    switch (xl.ChildNodes[1].InnerText.ToUpper())
                                    {
                                        case "LOTID":
                                            ppety.LotID = xl.ChildNodes[2].InnerText.Trim();
                                            break;
                                    }
                                }
                            }

                            if (EndLot != null)
                            {
                                EndLot(ppety);
                            }
                            strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F42", strSQN, "0", "00");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 1, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 1, "0");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.TcpServer.SendSECSIIMessage(strMsg);
                            break;
                        }
                    default:
                        {
                            strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F42", strSQN, "0", "00");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 1, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 1, "0");
                            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                            external.TcpServer.SendSECSIIMessage(strMsg);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S2F42", ex);
                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F42", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 1, null);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 5, "Alarm");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, ex.Message.Length, ex.Message);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }
        public void S1F1(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            //定义回复
            string strMsg = "";
            strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S1F2", strSQN, "0", "00");
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 6, "QM0919");
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 4, "V1.0");
            external.TcpServer.SendSECSIIMessage(strMsg);
        }
        public void S1F3(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;

            List<SVIDEntity> svid = new List<SVIDEntity>();
            XmlNode xln = xml.ChildNodes[0];
            for (int i = 0; i < xln.ChildNodes.Count; i++)
            {
                if (i > 0)
                {
                    if (dic.Keys.Contains(xln.ChildNodes[i].InnerText))
                    {
                        dic[xln.ChildNodes[i].InnerText].objValue = GetSVID(DBNullToIntZero(xln.ChildNodes[i].InnerText));
                        svid.Add(dic[xln.ChildNodes[i].InnerText]);
                    }
                    else
                    {
                        svid.Add(new SVIDEntity());
                    }
                }
            }

            string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S1F4", strSQN, "0", "00");
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, svid.Count, null);
            foreach (SVIDEntity sv in svid)
            {
                if (sv.objValue == null)
                {
                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.UINT_2, 1, 0);
                }
                else
                {
                    if (sv.ObjType == HsmsClass.SECSDataType.ASCII)
                    {
                        external.hsmsLibCls.DataItemOut(ref strMsg, sv.ObjType, sv.objValue.ToString().Length, sv.objValue);
                    }
                    else
                    {
                        external.hsmsLibCls.DataItemOut(ref strMsg, sv.ObjType, 1, DBNullToLong(sv.objValue));
                    }
                }

            }
            external.TcpServer.SendSECSIIMessage(strMsg);
        }

        public void S1F13(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            //定义回复
            string strMsg = "";
            strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S1F14", strSQN, "0", "00");
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 6, "QM919");
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 4, "V1.0");
            external.TcpServer.SendSECSIIMessage(strMsg);
        }

        public void S10F3(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            XmlNode xln = xml.ChildNodes[0];
            string strMessage = ((System.Xml.XmlElement)((System.Xml.XmlElement)xln).ChildNodes[2]).InnerXml;

            if (ShowMsg != null)
            {
                ShowMsg(strMessage);
            }

            //定义回复
            string strMsg = "";
            strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S10F4", strSQN, "0", "00");
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
            external.TcpServer.SendSECSIIMessage(strMsg);
        }

        public void S1F17(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            SecsGemOnLine = true;
            if (UpdateRdo != null)
            {
                UpdateRdo(true);
            }
            string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S1F18", strSQN, "0", "00");
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
            external.TcpServer.SendSECSIIMessage(strMsg);
        }

        public void S1F15(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            SecsGemOnLine = false;
            if (UpdateRdo != null)
            {
                UpdateRdo(false);
            }
            string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S1F16", strSQN, "0", "00");
            external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
            external.TcpServer.SendSECSIIMessage(strMsg);
        }

        public void S5F3(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            try
            {
                string path14 = System.Windows.Forms.Application.StartupPath + "\\Alarm.ini";//路径4#
                XmlNode xln = xml.ChildNodes[0];

                string ALED = "0";
                if (xln.ChildNodes[1].InnerText.StartsWith("0x"))
                {
                    ALED = xln.ChildNodes[1].InnerText.Substring(3);
                }
                string ALID = xln.ChildNodes[2].InnerText;

                Redata.writeIni("AlarmReportEnable", ALID, ALED, path14);

                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S5F4", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S5F4", ex);
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S5F4", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void S7F1(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            try
            {
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F2", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S7F2", ex);
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F2", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void S7F3(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            string strMsg = "";
            try
            {
                XmlNode xln = xml.ChildNodes[0];
                string PPID = xln.ChildNodes[1].InnerText;
                string PPText = xln.ChildNodes[2].InnerText;

                string[] strDatas = PPText.Split(';');

                string filePath = Path.Combine(RecipePath, PPID + ".json");

                // 判断文件是否存在
                if (File.Exists(filePath))
                {
                    strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F4", strSQN, "0", "00");
                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                    external.TcpServer.SendSECSIIMessage(strMsg);
                    return;
                }
                else
                {
                    // 创建文件并写入数据
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            foreach (string str in strDatas)
                            {
                                streamWriter.WriteLine(str);
                            }
                        }
                    }
                }

                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F4", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S7F4", ex);
                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F4", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void S7F5(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            string strMsg = "";
            try
            {
                string PPID = xml.ChildNodes[0].InnerText;
                string PPBody = "";

                string[] files = Directory.GetFiles(RecipePath, "*.json");
                foreach (string file in files)
                {
                    string[] split = file.Split(new Char[] { '\\' });
                    if (PPID + ".json" == split[split.Length - 1])
                    {
                        string filePath = RecipePath + PPID + ".json";

                        if (File.Exists(filePath))
                        {
                            using (StreamReader reader = new StreamReader(filePath))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (line.Trim() != "")
                                    {
                                        PPBody += line + ";";
                                    }
                                }
                            }
                        }
                    }
                }

                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F6", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, PPID.Length, PPID);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, PPBody.Length, PPBody);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S7F6", ex);
                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F6", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 1, "0");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, 1, " ");
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void S7F17(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            string strMsg = "";

            try
            {
                XmlNode xln = xml.ChildNodes[0];
                for (int i = 1; i < xln.ChildNodes.Count; i++)
                {
                    string filePath = Path.Combine(RecipePath, xln.ChildNodes[i].InnerText + ".json");
                    string newFilePath = filePath.Replace(".json", ".rmv" + DateTime.Now.ToString("HHmmssffff"));
                    if (File.Exists(filePath))
                    {
                        File.Move(filePath, newFilePath);
                    }
                }

                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F18", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S7F18", ex);
                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F18", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void S7F19(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            List<string> PPIDS = new List<string>();
            string strMsg = "";
            try
            {
                string PPBody = "";

                string[] files = Directory.GetFiles(RecipePath, "*.json");
                foreach (string file in files)
                {
                    string[] split = file.Split(new Char[] { '\\' });
                    PPIDS.Add(split[split.Length - 1]);
                }

                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F20", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, PPIDS.Count, null);
                foreach (string str in PPIDS.ToArray())
                {
                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, str.Length, str.Replace(".json", ""));
                }
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S7F20", ex);
                strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S7F20", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 0, null);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void S2F33(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            try
            {
                XmlNode xln = xml.ChildNodes[0];

                for (int j = 1; j < xln.ChildNodes[2].ChildNodes.Count; j++)
                {
                    string strRPTID = xln.ChildNodes[2].ChildNodes[j].ChildNodes[1].InnerText;

                    XmlNodeList xnl = xln.ChildNodes[2].ChildNodes[j].ChildNodes[2].ChildNodes;

                    string strSVIDS = "";
                    for (int i = 1; i < xnl.Count; i++)
                    {
                        strSVIDS += "," + xnl[i].InnerText;
                    }
                    if (strSVIDS.Length > 0)
                    {
                        strSVIDS = strSVIDS.Substring(1);
                    }

                    string path = System.Windows.Forms.Application.StartupPath + "\\Config\\SecsGem\\linkRPTID.txt";
                    inIHelper.writeIni("PRTID_DATA", strRPTID, strSVIDS, path);
                }
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F34", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S2F33", ex);
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F34", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void S2F35(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            try
            {
                XmlNode xln = xml.ChildNodes[0];

                for (int j = 1; j < xln.ChildNodes[2].ChildNodes.Count; j++)
                {
                    string CEID = xln.ChildNodes[2].ChildNodes[j].ChildNodes[1].InnerText;

                    XmlNodeList xnl = xln.ChildNodes[2].ChildNodes[j].ChildNodes[2].ChildNodes;

                    string strRPTIDS = "";
                    for (int i = 1; i < xnl.Count; i++)
                    {
                        strRPTIDS += "," + xnl[i].InnerText;
                    }
                    if (strRPTIDS.Length > 0)
                    {
                        strRPTIDS = strRPTIDS.Substring(1);
                    }

                    string path = System.Windows.Forms.Application.StartupPath + "\\Config\\SecsGem\\linkRPTID.txt";
                    inIHelper.writeIni("CEID_DATA", CEID, strRPTIDS, path);
                }
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F36", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S2F36", ex);
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F36", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void S2F37(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\Config\\SecsGem\\linkRPTID.txt";
                XmlNode xln = xml.ChildNodes[0];

                string EventEnable = "0";
                if (xln.ChildNodes[1].InnerText.StartsWith("0x"))
                {
                    EventEnable = xln.ChildNodes[1].InnerText.Substring(3);
                }

                XmlNodeList xnl = xln.ChildNodes[2].ChildNodes;

                for (int i = 1; i < xnl.Count; i++)
                {
                    string CEID = xnl[i].InnerText;
                    inIHelper.writeIni("EventEnabled", CEID, EventEnable, path);
                }

                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F38", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error(this.GetType().FullName + "S2F38", ex);
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F38", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void S2F31(XmlDocument xml, string strRawData, string strSxFy, string strSQN, out bool isAutoReply)
        {
            isAutoReply = false;
            XmlNode xln = xml.ChildNodes[0];
            DateTime after;
            System.Globalization.DateTimeStyles dt = new System.Globalization.DateTimeStyles();
            if (DateTime.TryParseExact(xln.InnerText, "yyyyMMddHHmmss", null, dt, out after))
            {
                DateTime dateTime = DateTime.ParseExact(xln.InnerText, "yyyyMMddHHmmss", null);
                TimeSpan ts = dateTime - DateTime.Now;
                secsGEMVariable.WorkTime = Convert.ToInt32(ts.TotalSeconds);

                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F32", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 0);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
            else
            {
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S2F32", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, 1);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }

        public void AlarmReport(string Id, int Status, string Content)
        {
            Thread.Sleep(100);
            if (m_bCommEnabled && SecsGemOnLine)
            {
                if (external == null)
                {
                    return;
                }
                string strSQN = external.CharSetCls.StringAddSplit(external.GetSQN("0"));

                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S5F1", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 3, null);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.BINARY, 1, Status);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.UINT_2, 1, Id);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.ASCII, Content.Length, Content);
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }


        EventEntity[] eventEntity;
        INIHelper inIHelper = new INIHelper();
        public void EventReport(int ceid, bool b)
        {
            if ((m_bCommEnabled && SecsGemOnLine) || b)
            {
                string CEID = ceid.ToString();
                string path = System.Windows.Forms.Application.StartupPath + "\\Config\\SecsGem\\linkRPTID.txt";//路径4#
                string EventEnable = inIHelper.getIni("EventEnabled", CEID, "", path);//事件使能
                if (EventEnable == "")
                {
                    EventEnable = "1";
                    inIHelper.writeIni("EventEnabled", CEID, EventEnable, path);
                }
                if (EventEnable == "0")
                {
                    return;
                }

                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                string RPTID = inIHelper.getIni("CEID_DATA", CEID, "", path);
                string[] RPTID_Datas = RPTID.Split(',');

                int eventNum = RPTID_Datas.Length;//设置事件数量

                eventEntity = new EventEntity[eventNum];

                if (eventEntity.Length < 1 || eventEntity == null)
                {
                    return;
                }

                string strSQN = external.CharSetCls.StringAddSplit(external.GetSQN("0"));
                string strMsg = external.hsmsLibCls.ConfigDeviceSxFyString("S6F11", strSQN, "0", "00");
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 3, null);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.UINT_2, 1, 0);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.UINT_2, 1, ceid);
                external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, eventEntity.Length, null);

                //设置事件参数，以下为参考
                string res = "";
                // Event_Cnt = Event_Cnt + 1;
                for (int i = 0; i < RPTID_Datas.Length; i++)
                {
                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, 2, null);
                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.UINT_2, 1, DBNullToIntZero(RPTID_Datas[i]));
                    res = "";
                    if (RPTID_Datas[i] == "") continue;
                    string curr_RPTID = RPTID_Datas[i];//当前报告编号
                    string SVIDS = inIHelper.getIni("PRTID_DATA", curr_RPTID, "", path);//当前报告里的参数
                    string[] SVID_Datas = SVIDS.Split(',');
                    external.hsmsLibCls.DataItemOut(ref strMsg, HsmsClass.SECSDataType.LIST, SVID_Datas.Length, null);
                    for (int h = 0; h < SVID_Datas.Length; h++)
                    {
                        if (SVID_Datas[h] == "") continue;
                        Parameter_Secs(Convert.ToInt32(SVID_Datas[h]) - 1000);
                        res = Data_Secs[Convert.ToInt32(SVID_Datas[h]) - 1000];
                        //根据SVID取得类型
                        SVIDEntity enty = dic[SVID_Datas[h]];
                        if (enty != null)
                        {
                            if (enty.ObjType == HsmsClass.SECSDataType.ASCII)
                            {
                                if (res == null)
                                {
                                    res = "";
                                }
                                external.hsmsLibCls.DataItemOut(ref strMsg, enty.ObjType, res.Length, res);
                            }
                            else
                            {
                                if (res == null || res == "")
                                {
                                    res = "0";
                                }
                                external.hsmsLibCls.DataItemOut(ref strMsg, enty.ObjType, 1, res);
                            }
                        }
                    }
                }
                external.TcpServer.SendSECSIIMessage(strMsg);
            }
        }



        private string[] Data_Secs = new string[200];//secs-gem的数量
        public SecsGEMVariable secsGEMVariable = new SecsGEMVariable();

        public void Parameter_Secs(int a, bool jp_flag = false)
        {
            //SVID取值
            if (a == 0)
            {
                if (secsGEMVariable.MachineStatus == "" || secsGEMVariable.MachineStatus == "-1")
                {
                    secsGEMVariable.MachineStatus = "0";
                    Data_Secs[0] = secsGEMVariable.MachineStatus;

                }
                else if (secsGEMVariable.MachineStatus != "-1")
                {
                    Data_Secs[0] = secsGEMVariable.MachineStatus;
                    secsGEMVariable.MachineStatus = "-1";
                }
                else
                {
                    Data_Secs[0] = secsGEMVariable.MachineStatus;
                }
            }
            else if (a == 1)
            {
                //1:Online Remote,2:Online Local,0:Offline
                if (!SecsGemOnLine)
                {
                    secsGEMVariable.MachineControlMode = "0";

                    Data_Secs[1] = secsGEMVariable.MachineControlMode;
                }
                else
                {
                    if (m_bCommEnabled)
                    {
                        secsGEMVariable.MachineControlMode = "1";
                    }
                    else
                    {
                        secsGEMVariable.MachineControlMode = "2";
                    }
                    Data_Secs[1] = secsGEMVariable.MachineControlMode;
                }

            }
            else if (a == 2)
            {
                Data_Secs[2] = secsGEMVariable.WorkMode;
            }
            else if (a == 3)
            {
                Data_Secs[3] = DateTime.Now.AddSeconds(secsGEMVariable.WorkTime).ToString("yyyyMMddHHmmss");
            }
            else if (a == 4)
            {
                Data_Secs[4] = secsGEMVariable.LotNumber;
            }
            else if (a == 5)
            {
                Data_Secs[5] = secsGEMVariable.OpID;
            }
            else if (a == 6)
            {
                Data_Secs[6] = secsGEMVariable.Recipe;
            }
            else if (a == 7)
            {
                Data_Secs[7] = secsGEMVariable.Version;
            }
            else if (a == 8)
            {
                Data_Secs[8] = secsGEMVariable.MDLN;
            }
            else if (a == 9)
            {
                Data_Secs[9] = secsGEMVariable.MachineID;
            }
            else if (a == 10)
            {
                Data_Secs[10] = secsGEMVariable.ZRSL;
            }
            else if (a == 11)
            {
                Data_Secs[11] = secsGEMVariable.OutTotalCount;
            }
            else if (a == 12)
            {
                Data_Secs[12] = secsGEMVariable.PassCount;
            }
            else if (a == 13)
            {
                Data_Secs[13] = secsGEMVariable.FailCount;
            }
            else if (a == 14)
            {
                Data_Secs[14] = secsGEMVariable.Yield;
            }
            else if (a == 15)
            {
                Data_Secs[15] = secsGEMVariable.PN;
            }
            else if (a == 16)
            {
                Data_Secs[16] = secsGEMVariable.WorkSheetNumber;
            }
            else if (a == 17)
            {
                Data_Secs[17] = secsGEMVariable.UPH;
            }
            else if (a == 18)
            {
                Data_Secs[18] = secsGEMVariable.WorkHours;
            }
            else if (a == 19)
            {
                Data_Secs[19] = secsGEMVariable.BatchStartTime;
            }
            else if (a == 20)
            {
                Data_Secs[20] = secsGEMVariable.BatchEndTime;
            }
            else if (a == 101)
            {
                Data_Secs[101] = secsGEMVariable.RunCheckList.Name;
            }
            else if (a == 102)
            {
                Data_Secs[102] = secsGEMVariable.RunCheckList.MCName;
            }
            else if (a == 103)
            {
                Data_Secs[103] = secsGEMVariable.RunCheckList.ShuttleStepX;
            }
            else if (a == 104)
            {
                Data_Secs[104] = secsGEMVariable.RunCheckList.ShuttleStepY;
            }
            else if (a == 105)
            {
                Data_Secs[105] = secsGEMVariable.RunCheckList.ShuttleXCount;
            }
            else if (a == 106)
            {
                Data_Secs[106] = secsGEMVariable.RunCheckList.ShuttleYCount;
            }
            else if (a == 107)
            {
                Data_Secs[107] = secsGEMVariable.RunCheckList.TrayStepX;
            }
            else if (a == 108)
            {
                Data_Secs[108] = secsGEMVariable.RunCheckList.TrayStepY;
            }
            else if (a == 109)
            {
                Data_Secs[109] = secsGEMVariable.RunCheckList.TrayXCount;
            }
            else if (a == 110)
            {
                Data_Secs[110] = secsGEMVariable.RunCheckList.TrayYCount;
            }
            else if (a == 111)
            {
                Data_Secs[111] = secsGEMVariable.RunCheckList.BIBStepX;
            }
            else if (a == 112)
            {
                Data_Secs[112] = secsGEMVariable.RunCheckList.BIBStepX2;
            }
            else if (a == 113)
            {
                Data_Secs[113] = secsGEMVariable.RunCheckList.BIBStepY;
            }
            else if (a == 114)
            {
                Data_Secs[114] = secsGEMVariable.RunCheckList.BIBStepXPick;
            }
            else if (a == 115)
            {
                Data_Secs[115] = secsGEMVariable.RunCheckList.BIBStepX2Pick;
            }
            else if (a == 116)
            {
                Data_Secs[116] = secsGEMVariable.RunCheckList.BIBStepYPick;
            }
            else if (a == 117)
            {
                Data_Secs[117] = secsGEMVariable.RunCheckList.BIBXCount;
            }
            else if (a == 118)
            {
                Data_Secs[118] = secsGEMVariable.RunCheckList.BIBYCount;
            }
            else if (a == 119)
            {
                Data_Secs[119] = secsGEMVariable.RunCheckList.SthR;
            }
            else if (a == 120)
            {
                Data_Secs[120] = secsGEMVariable.RunCheckList.ScloseR;
            }
            else if (a == 121)
            {
                Data_Secs[121] = secsGEMVariable.RunCheckList.BibPickCount;
            }
            else if (a == 122)
            {
                Data_Secs[122] = secsGEMVariable.RunCheckList.Bin1Name;
            }
            else if (a == 123)
            {
                Data_Secs[123] = secsGEMVariable.RunCheckList.Bin1Flag;
            }
            else if (a == 124)
            {
                Data_Secs[124] = secsGEMVariable.RunCheckList.Bin1Code;
            }
            else if (a == 125)
            {
                Data_Secs[125] = secsGEMVariable.RunCheckList.Bin2Name;
            }
            else if (a == 126)
            {
                Data_Secs[126] = secsGEMVariable.RunCheckList.Bin2Flag;
            }
            else if (a == 127)
            {
                Data_Secs[127] = secsGEMVariable.RunCheckList.Bin2Code;
            }
            else if (a == 128)
            {
                Data_Secs[128] = secsGEMVariable.RunCheckList.Bin3Name;
            }
            else if (a == 129)
            {
                Data_Secs[129] = secsGEMVariable.RunCheckList.Bin3Flag;
            }
            else if (a == 130)
            {
                Data_Secs[130] = secsGEMVariable.RunCheckList.Bin3Code;
            }
            else if (a == 131)
            {
                Data_Secs[131] = secsGEMVariable.RunCheckList.TesterIp;
            }
            else if (a == 132)
            {
                Data_Secs[132] = secsGEMVariable.RunCheckList.ShareRootPath;
            }
            else if (a == 133)
            {
                Data_Secs[133] = secsGEMVariable.RunCheckList.TBadMax;
            }
            else if (a == 134)
            {
                Data_Secs[134] = secsGEMVariable.RunCheckList.TChangeNum;
            }
            else if (a == 135)
            {
                Data_Secs[135] = secsGEMVariable.RunCheckList.TPassBin;
            }
            else if (a == 136)
            {
                Data_Secs[136] = secsGEMVariable.RunCheckList.TFailBin;
            }
            else if (a == 137)
            {
                Data_Secs[137] = secsGEMVariable.RunCheckList.TErrorBin;
            }
            else if (a == 138)
            {
                Data_Secs[138] = secsGEMVariable.RunCheckList.BmsBindRootPath;
            }
            else if (a == 139)
            {
                Data_Secs[139] = secsGEMVariable.RunCheckList.BmsCompareRootPath;
            }
            else if (a == 140)
            {
                Data_Secs[140] = secsGEMVariable.RunCheckList.BmsMapRootPath;
            }
            else if (a == 141)
            {
                Data_Secs[141] = secsGEMVariable.RunCheckList.BmsPtmRootPath;
            }
            else if (a == 142)
            {
                Data_Secs[142] = secsGEMVariable.RunCheckList.RadarDelay;
            }
            else if (a == 143)
            {
                Data_Secs[143] = secsGEMVariable.RunCheckList.PullGoldDelay;
            }
            else if (a == 144)
            {
                Data_Secs[144] = secsGEMVariable.RunCheckList.SendTestDelay;
            }
            else if (a == 145)
            {
                Data_Secs[145] = secsGEMVariable.RunCheckList.LArcSpeed;
            }
            else if (a == 146)
            {
                Data_Secs[146] = secsGEMVariable.RunCheckList.RArcSpeed;
            }
            else if (a == 147)
            {
                Data_Secs[147] = secsGEMVariable.RunCheckList.TArcSpeed;
            }
            else if (a == 148)
            {
                Data_Secs[148] = secsGEMVariable.RunCheckList.GoldSpeed;
            }
            else if (a == 149)
            {
                Data_Secs[149] = secsGEMVariable.RunCheckList.TrayTransDistOffset;
            }
            else if (a == 150)
            {
                Data_Secs[150] = secsGEMVariable.RunCheckList.TrayTransDistOffsetPut;
            }
            else if (a == 151)
            {
                Data_Secs[151] = secsGEMVariable.RunCheckList.TrayTransDistOffsetPutEmpty;
            }
            else if (a == 152)
            {
                Data_Secs[152] = secsGEMVariable.RunCheckList.TestTimeOut;
            }
            else if (a == 153)
            {
                Data_Secs[153] = secsGEMVariable.RunCheckList.LeftArmCheckMaxValue;
            }
            else if (a == 154)
            {
                Data_Secs[154] = secsGEMVariable.RunCheckList.RightArmCheckMaxValue;
            }
            else if (a == 155)
            {
                Data_Secs[155] = secsGEMVariable.RunCheckList.TestArmCheckMaxValue;
            }
        }

        /// <summary>
        /// DBNull转换Empty
        /// </summary>
        /// <param name="pParm"></param>
        /// <returns></returns>
        public string ConvertDBNullToStrEmpty(object objParm)
        {
            if (objParm == DBNull.Value || objParm == null)
            {
                return string.Empty;
            }
            else
            {
                return objParm.ToString().Trim();
            }
        }

        /// <summary>
        /// DBNull转换0
        /// </summary>
        /// <param name="pParm"></param>
        /// <returns></returns>
        public Decimal DBNullToDecimalZero(object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return 0;
            }
        }

        public DateTime DBNullToDate(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// DBNull转换0
        /// </summary>
        /// <param name="pParm"></param>
        /// <returns></returns>
        public int DBNullToIntZero(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// DBNull转换0
        /// </summary>
        /// <param name="pParm"></param>
        /// <returns></returns>
        public long DBNullToLong(object obj)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch
            {
                return 0;
            }
        }
    }
}

public class EventEntity
{
    public int EventID = 0;
    public string EventDesp = "";
    public int EventNo = 0;
}