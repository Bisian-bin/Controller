using QMBaseClass.工厂类;
using System;
using System.Threading;
using static Forms.Flow.Variable;

namespace Forms.Flow
{
    #region 流程步序

    /// <summary>
    /// tray机构动作流程
    /// </summary>
    public enum TransferStatus
    {
        中转区初始化,
        中转区BIB状态,
        中转Z轴运动对应层数,
        中转区等待结批完成,
        中转区升降机两边打BIB板气缸出,
        移栽运动等待中转区BIB板,
        移栽BIB到位前感应,
        移栽钩子运动钩板开始位,
        移栽钩子运动钩板结束位,
        移载前定位气缸出,
        移载上顶气缸出,
        移载侧定位气缸出,
        移载运动BIB扫码位置,
        分析扫码返回结果,
        根据二维码获取上料信息,
        移载运动BIB上下料位置,
        等待上下料完成
    }
    #endregion

    /// <summary>
    /// 中转+移载
    /// </summary>
    public class Transfer:QMFlow
    {
        /// <summary>
        /// 执行动作流程
        /// </summary>
        public void TransferRun()
        {
            if (FlowFactory.RunStatus == Order.MachineStatuOrder.Running)//设备生产状态
            {
                switch (FlowFactory.FlowStep.TransferStep)
                {
                    #region 中转区推送BIB板

                    case TransferStatus.中转区BIB状态://
                        FlowFactory.variable.TransferVar.TransferCurrentRow = 100;//每次判断之前进行参数化
                        for (int a = 0; a < FlowFactory.recipe.TransferStationDimensions.TransferNum; a++)
                        {
                            if (FlowFactory.variable.TransferVar.TransferGrid[a].status == TransferGridStatus.蓝色)//
                            {
                                FlowFactory.variable.TransferVar.TransferCurrentRow = (a + 1);//锁定某一行
                                break;
                            }
                        }
                        //判断结果
                        if (FlowFactory.variable.TransferVar.TransferCurrentRow == 100)//报警更换BIB板
                        {
                            if (!FlowFactory.variable.ClaeanOut)
                            {
                                FlowFactory.ChildReceiveDown(5000);//触发报警-请确认小推车上是否有放BIB板! 
                            }
                            else
                            {
                                FlowFactory.FlowStep.TransferStep = TransferStatus.中转区等待结批完成;
                            }
                        }
                        else
                        {
                            FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y184_升降机BIB板阻挡气缸上升, true);
                            FlowFactory.FlowStep.TransferStep = TransferStatus.中转Z轴运动对应层数;
                        }
                        break;

                    case TransferStatus.中转Z轴运动对应层数:
                        if (FlowFactory.AxisContorl.TransferMoveAuto((int)Order.AxisOrder.中转站Z轴, "第一层", FlowFactory.variable.TransferVar.TransferCurrentRow))
                        {
                            if (FlowFactory.AxisContorl.TransferMoveAuto((int)Order.AxisOrder.BIB板移栽Y轴, "等待中转区BIB板", FlowFactory.variable.TransferVar.TransferCurrentRow))
                            {
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y176_中转站BIB板推送电机正转, true); 
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y177_中转站BIB板推送电机反转, false);
                                for (int a = 0; a < 6; a++) { FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y178_移载上顶气缸出 + a, false); }
                                FlowFactory.FlowStep.TransferStep = TransferStatus.移栽运动等待中转区BIB板;
                            }
                        }
                        break;

                    case TransferStatus.中转区升降机两边打BIB板气缸出:
                        if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y184_升降机BIB板阻挡气缸上升,
                           (int)Order.InOrder.X155_升降机BIB板阻挡气缸上,(int)Order.InOrder.X154_升降机BIB板阻挡气缸下,true))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y178_移载上顶气缸出,
                             (int)Order.InOrder.X145_移载上顶气缸感应器上, (int)Order.InOrder.X144_移载上顶气缸感应器下, false))
                            {
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y186_升降机两边打BIB板气缸出, true);
                                FlowFactory.FlowStep.TransferStep = TransferStatus.移栽BIB到位前感应;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(5004);//X145_移载上顶气缸感应器上异常，请确认!
                            }
                        }
                        else
                        {
                            FlowFactory.ChildReceiveDown(5001);//X155_升降机BIB板阻挡气缸上感应异常，请确认!
                        }
                        break;

                    #endregion

                    #region 移栽接受BIB板

                    case TransferStatus.移栽BIB到位前感应:

                        if (FlowFactory.AxisContorl.XStatusWait((int)(Order.InOrder.X160_移载BIB到位检测前感应器)))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y183_移载滚轮气缸回,
                              (int)Order.InOrder.X142_移栽滚轮气缸回位, (int)Order.InOrder.X143_移栽滚轮气缸出位, true))
                            {
                                FlowFactory.FlowStep.TransferStep = TransferStatus.移栽钩子运动钩板开始位;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(5002);// X142_移栽滚轮气缸回位感应异常，请确认!
                            }
                        }
                        else
                        {
                            FlowFactory.ChildReceiveDown(5003);//X160_移载BIB到位未检测到BIB,请确认!
                        }
                        break;

                    case TransferStatus.移栽钩子运动钩板开始位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.BIB板移栽钩子轴, "钩板开始位"))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y182_移载钩子上顶气缸出,
                              (int)Order.InOrder.X153_移载钩子上顶气缸感应器出, (int)Order.InOrder.X152_移载钩子上顶气缸感应器回, true))
                            {
                                FlowFactory.FlowStep.TransferStep = TransferStatus.移栽钩子运动钩板结束位;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(5005);//X153_移载钩子上顶气缸感应器出异常，请确认!
                            }
                        }
                        break;

                    case TransferStatus.移栽钩子运动钩板结束位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.BIB板移栽钩子轴, "钩板结束位"))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y182_移载钩子上顶气缸出,
                                 (int)Order.InOrder.X152_移载钩子上顶气缸感应器回, (int)Order.InOrder.X153_移载钩子上顶气缸感应器出, false))
                            {
                                FlowFactory.FlowStep.TransferStep = TransferStatus.移载前定位气缸出;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(5006);//X152_移载钩子上顶气缸感应器回异常，请确认!
                            }
                        }
                        break;

                    case TransferStatus.移载前定位气缸出:
                        if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y179_移载侧定位气缸出,
                             (int)Order.InOrder.X147_移载侧定位气缸感应器出, (int)Order.InOrder.X148_移载前定位气缸感应器回, true))
                        {
                            FlowFactory.FlowStep.TransferStep = TransferStatus.移载侧定位气缸出;
                        }
                        else
                        {
                            FlowFactory.ChildReceiveDown(5007);//X147_移载侧定位气缸感应器出异常，请确认!
                        }
                        break;

                    case TransferStatus.移载侧定位气缸出:
                        if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y178_移载上顶气缸出,
                          (int)Order.InOrder.X144_移载上顶气缸感应器下, (int)Order.InOrder.X145_移载上顶气缸感应器上, true))
                        {
                            FlowFactory.FlowStep.TransferStep = TransferStatus.移载上顶气缸出;
                        }
                        else
                        {
                            FlowFactory.ChildReceiveDown(5008);//X144_移载上顶气缸感应器下异常，请确认!
                        }
                        break;

                    case TransferStatus.移载上顶气缸出:
                        if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y178_移载上顶气缸出,
                             (int)Order.InOrder.X144_移载上顶气缸感应器下, (int)Order.InOrder.X145_移载上顶气缸感应器上, true))
                        {
                            if (FlowFactory.variable.Mode.RunMode.status == RunModeStatus.自动模式)
                            {
                                FlowFactory.FlowStep.TransferStep = TransferStatus.移载运动BIB扫码位置;
                            }
                            else 
                            {
                                for (int a = 0; a < Convert.ToInt16(FlowFactory.recipe.BIBBcardDimensions.BIBXNum); a++)
                                {
                                    for (int b = 0; b < Convert.ToInt16(FlowFactory.recipe.BIBBcardDimensions.BIBXNum); b++)
                                    {
                                        FlowFactory.variable.TransferVar.BIBGrid[a, b].status = BIBGridStatus.允许上料;
                                    }
                                }
                                FlowFactory.variable.TransferVar.AllowActions = true;
                                FlowFactory.FlowStep.TransferStep = TransferStatus.移载运动BIB上下料位置;
                            }
                        }
                        else
                        {
                            FlowFactory.ChildReceiveDown(5008);//X144_移载上顶气缸感应器下异常，请确认!
                        }
                        break;
                    #endregion

                    #region 移载-BIB板运动

                    case TransferStatus.移载运动BIB扫码位置:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.BIB板移栽Y轴, "BIB扫码位"))
                        {
                            if (FlowFactory.variable.Scanner.IsScannerConnected)//连线成功
                            {
                                //发送指令
                                Thread.Sleep(FlowFactory.variable.Scanner.ScanWaitingTime);
                                FlowFactory.FlowStep.TransferStep = TransferStatus.分析扫码返回结果;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(5010);//BIB扫码枪通讯异常，请确认!
                            }
                        }
                        break;

                    case TransferStatus.分析扫码返回结果:
                        if (FlowFactory.variable.Scanner.ScannedReturnString != "")//说明扫码成功
                        {
                            //ScannedReturnString=返回内容
                            //根据二维码获取上料讯息
                        }
                        else
                        {
                            FlowFactory.variable.Scanner.ScanningCount = FlowFactory.variable.Scanner.ScanningCount + 1;
                            if (FlowFactory.variable.Scanner.ScanningCount >= 3)
                            {
                                FlowFactory.variable.Scanner.ScanningCount = 0;
                                FlowFactory.ChildReceiveDown(5011);//BIB扫码枪返回内容异常，请确认！
                            }
                        }
                        break;

                    case TransferStatus.根据二维码获取上料信息:


                        break;

                    case TransferStatus.移载运动BIB上下料位置:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.BIB板移栽Y轴, "BIB上下料位"))
                        {
                            FlowFactory.FlowStep.TransferStep = TransferStatus.等待上下料完成;
                        }
                        break;

                        #endregion

                    #region 移栽送BIB板

                        #endregion
                }
            }
        }

        /// <summary>
        /// 执行动作流程
        /// </summary>
        public override void Process()
        {
            while (true)
            {
                Sleep(10);
                PauseResetEvent.WaitOne();
                try
                {
                    TransferRun();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
