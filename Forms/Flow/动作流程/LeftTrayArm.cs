using QMBaseClass.工厂类;
using System;
using System.Threading;

namespace Forms.Flow
{
    #region 流程步序

    /// <summary>
    /// 左侧Tray手臂步序
    /// </summary>
    public enum LeftTrayArmStatus
    {
        Tray盘初始化,

        #region 上料模式
        根据BIB板情况判断Tray吸嘴,
        判断Tray吸嘴优先级,
        去Tray盘对应位置,
        Tray盘换盘等待,
        判断Tray区吸嘴是否需要下降,
        吸嘴气缸动作,
        Tray吸嘴下降,
        Tray吸嘴上升安全位,
        检查真空值,
        去飞梭放料,
        飞梭吸嘴下降,
        打开破真空,
        破真空等待时间,
        飞梭吸嘴上升安全位,
        运动待机位,
        #endregion
    }
    #endregion

    /// <summary>
    /// 左侧Tray手臂
    /// </summary>
    public class LeftTrayArm : QMFlow
    {
        /// <summary>
        /// 执行动作流程
        /// </summary>
        public void LeftTrayArmRun()
        {
            if (FlowFactory.RunStatus == Order.MachineStatuOrder.Running)
            {
                switch (FlowFactory.FlowStep.LeftTrayArmStep)
                {
                    #region 判断上下料
                    case LeftTrayArmStatus.Tray盘初始化:
                        if (FlowFactory.variable.Mode.OnlyLoadEnable)
                        {
                            if (FlowFactory.variable.TransferVar.AllowActions)
                            {
                                FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus = new int[FlowFactory.variable.LeftTrayArmVar.NozzleNumber];
                                FlowFactory.variable.LeftTrayArmVar.BIBMappedTray = 0;
                                FlowFactory.variable.LeftTrayArmVar.BIBCountTray = FlowFactory.recipe.BIBBcardDimensions.BIBXNum / 2;
                                FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.根据BIB板情况判断Tray吸嘴;
                            }
                        }
                        else if (FlowFactory.variable.Mode.OnlyUnloadEnable)
                        {
                           
                        }
                        break;

                    #endregion

                    #region 上料模式

                    case LeftTrayArmStatus.根据BIB板情况判断Tray吸嘴:
                        for (int b = 0; b < FlowFactory.variable.LeftTrayArmVar.NozzleNumber; b++)
                        {
                            if (FlowFactory.variable.TransferVar.BIBGrid[FlowFactory.variable.LeftTrayArmVar.BIBMappedTray, b].status ==
                               Variable.BIBGridStatus.允许上料)
                            {
                                FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[b] = 1;//吸嘴上料
                                FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.判断Tray吸嘴优先级;
                            }
                            else
                            {
                                FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[b] = 0;//吸嘴不上料  
                            }
                        }
                        if (FlowFactory.FlowStep.LeftTrayArmStep == LeftTrayArmStatus.根据BIB板情况判断Tray吸嘴)
                        {
                            FlowFactory.variable.LeftTrayArmVar.BIBMappedTray = FlowFactory.variable.LeftTrayArmVar.BIBMappedTray + 1;
                            if (FlowFactory.variable.LeftTrayArmVar.BIBMappedTray == FlowFactory.variable.LeftTrayArmVar.BIBCountTray)
                            {
                                FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.运动待机位;
                            }
                        }
                        break;

                    case LeftTrayArmStatus.判断Tray吸嘴优先级:
                        for (int a = 0; a < FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//1~6上料吸嘴
                        {
                            if (FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] == 1)//NozzlePriority
                            {
                                FlowFactory.variable.LeftTrayArmVar.NozzlePriority = a;//记录吸嘴优先级
                                break;//退出循环
                            }
                        }
                        //记录吸嘴去LOAD区位置
                        for (int a = 0; a < FlowFactory.recipe.TrayDimensions.TrayXNum; a++)
                        {
                            for (int b = 0; b < FlowFactory.recipe.TrayDimensions.TrayYNum; b++)
                            {
                                if (FlowFactory.variable.LeftTrayArmVar.TrayGrid[a,b].status == Variable.TrayGridStatus.有料)
                                {
                                    FlowFactory.variable.LeftTrayArmVar.TrayAreaRows= a;//记录load区对应行
                                    FlowFactory.variable.LeftTrayArmVar.TrayAreaCells = b;//记录load区对应列 
                                    FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.去Tray盘对应位置;
                                    return;
                                }
                            }
                        }
                        if (FlowFactory.FlowStep.LeftTrayArmStep == LeftTrayArmStatus.判断Tray吸嘴优先级)
                        {
                            if (FlowFactory.FlowStep.LeftMoveTrayStep == LeftMoveTrayStatus.Tray进行上下料)
                            {
                                if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左1上下料机头Z轴, "安全位"))
                                {
                                    if (FlowFactory.variable.ClaeanOut)
                                    {
                                        if (FlowFactory.variable.Mode.RunMode.status == Variable.RunModeStatus.离线空跑)
                                        {
                                            for (int a = 0; a < FlowFactory.recipe.TrayDimensions.TrayXNum; a++)
                                            {
                                                for (int b = 0; b < FlowFactory.recipe.TrayDimensions.TrayYNum; b++)
                                                {
                                                    FlowFactory.variable.LeftTrayArmVar.TrayGrid[a, b].status = Variable.TrayGridStatus.有料;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.Tray盘换盘等待;//Tray上顶轴后运动等待位
                                            FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray上顶轴后运动等待位;
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                        break;

                    case LeftTrayArmStatus.去Tray盘对应位置:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左1上下料机头Z轴, "安全位"))
                        {
                            if (FlowFactory.AxisContorl.LeftTrayMove(FlowFactory.variable.LeftTrayArmVar.TrayAreaRows, FlowFactory.variable.LeftTrayArmVar.TrayAreaCells, FlowFactory.variable.LeftTrayArmVar.NozzlePriority))
                            {
                                for (int a = 0; a < 12; a++)//关闭所有破真空
                                {
                                    FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y022_左1上下料机头1破真空 + a, false);
                                }
                                    FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.判断Tray区吸嘴是否需要下降;
                            }
                        }
                        break;

                    case LeftTrayArmStatus.判断Tray区吸嘴是否需要下降:
                        for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)
                        {
                            if (FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] <= 3 & FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] >= 1)//判断吸嘴上是否有料
                            {
                                if (FlowFactory.variable.LeftTrayArmVar.TrayAreaCells + (a - FlowFactory.variable.LeftTrayArmVar.NozzlePriority) < FlowFactory.recipe.TrayDimensions.TrayXNum)//行数不能大于设定值
                                {
                                    if (FlowFactory.variable.LeftTrayArmVar.TrayGrid[FlowFactory.variable.LeftTrayArmVar.TrayAreaRows, FlowFactory.variable.LeftTrayArmVar.TrayAreaCells + (a - FlowFactory.variable.LeftTrayArmVar.NozzlePriority)].status == Variable.TrayGridStatus.有料)
                                    {
                                        FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] = 2;//=2，说明吸嘴气缸需要下降吸料
                                        FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.判断Tray区吸嘴是否需要下降;
                                    }
                                }
                            }
                        }
                        //判断
                        if (FlowFactory.FlowStep.LeftTrayArmStep == LeftTrayArmStatus.判断Tray区吸嘴是否需要下降)
                        {
                            FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.判断Tray吸嘴优先级;
                        }
                        break;

                    case LeftTrayArmStatus.吸嘴气缸动作:
                        for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                        {
                            if (FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] <= 3 & FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] >= 2)
                            {
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y034_左1上下料机头1吸嘴气缸出 +a, true);//吸嘴杠下降
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y010_左1上下料机头1吸真空 + a, true);
                                FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] = 3;//=3，说明吸嘴气缸需要吸真空
                            }
                            else
                            {
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y034_左1上下料机头1吸嘴气缸出 + a, false);//吸嘴杠下降
                            }
                        }
                        FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.Tray吸嘴下降;
                        break;

                    case LeftTrayArmStatus.Tray吸嘴下降:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左1上下料机头Z轴, "Tray取料位"))
                        {
                           Thread.Sleep(FlowFactory.variable.LeftTrayArmVar.VacuumWaitingTime);
                           FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.Tray吸嘴上升安全位;
                        }
                            break;

                    case LeftTrayArmStatus.Tray吸嘴上升安全位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左1上下料机头Z轴, "安全位"))
                        {
                            FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.检查真空值;
                        }
                        break;

                    case LeftTrayArmStatus.检查真空值:

                        for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                        {
                            if (FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] == 3)//检查是否报警
                            {
                                if (FlowFactory.AxisContorl.GetXStatus((int)Order.InOrder.X016_左1上下料机头1吸真空检测 +a)
                                    || FlowFactory.variable.Mode.RunMode.status == Variable.RunModeStatus.离线空跑)
                                {
                                    FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] = 4;//=4，说明吸嘴真空满足要求
                                    FlowFactory.variable.LeftTrayArmVar.LoadingQuantity = FlowFactory.variable.LeftTrayArmVar.LoadingQuantity + 1;//上料数量
                                }
                                else
                                {
                                    FlowFactory.variable.LeftTrayArmVar.AlarmWaitingTime[a] = FlowFactory.variable.LeftTrayArmVar.AlarmWaitingTime[a] + 1;
                                    if (FlowFactory.variable.LeftTrayArmVar.AlarmWaitingTime[a] >= 200)
                                    {
                                        FlowFactory.ChildReceiveDown(6000 + a);//吸真空检测异常
                                        FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.Tray吸嘴下降;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                FlowFactory.variable.LeftTrayArmVar.AlarmWaitingTime[a] = 0;
                            }
                        }
                        if (FlowFactory.FlowStep.LeftTrayArmStep == LeftTrayArmStatus.检查真空值)
                        {
                            for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                            {
                                if (FlowFactory.variable.LeftTrayArmVar.AlarmWaitingTime[a] != 0)
                                {
                                    FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.检查真空值;break;
                                }
                            }
                            FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.去飞梭放料;
                        }
                        break;

                    case LeftTrayArmStatus.去飞梭放料:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左1上下料机头Z轴, "安全位"))
                        {
                            if (FlowFactory.AxisContorl.TwoMoveRelAuto((int)Order.AxisOrder.左1上下料机头变距轴,"上料位",(int)Order.AxisOrder.左1上下料机头变距轴, "飞梭变距"))
                            {
                                for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                                {
                                    if (FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] == 4)
                                    {
                                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y034_左1上下料机头1吸嘴气缸出 + a, true);//吸嘴杠下降
                                        FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] = 5;
                                    }
                                    else
                                    {
                                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y034_左1上下料机头1吸嘴气缸出 + a, false);//吸嘴杠下降
                                    }
                                }
                                FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.飞梭吸嘴下降;
                            }
                        }
                            break;

                    case LeftTrayArmStatus.飞梭吸嘴下降:                      
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左1上下料机头Z轴, "飞梭放料位"))
                        {
                            for (int a = 0; a < 12; a++)//吸嘴数量 
                            {
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y010_左1上下料机头1吸真空 + a, false);
                            }
                        }
                        FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.打开破真空;
                        break;

                    case LeftTrayArmStatus.打开破真空:
                        for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                        {
                            if (FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] == 5)
                            {
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y022_左1上下料机头1破真空 + a, true);//吸嘴杠下降
                                FlowFactory.variable.LeftBIBArmVar.TrayNozzleStatus[a] = FlowFactory.variable.LeftTrayArmVar.TrayNozzleStatus[a] = 6;
                            }
                            else
                            {
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y022_左1上下料机头1破真空 + a, false);//吸嘴杠下降
                            }
                        }
                        FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.破真空等待时间;
                        break;

                    case LeftTrayArmStatus.破真空等待时间:
                        System.Threading.Thread.Sleep(FlowFactory.variable.LeftTrayArmVar.VacuumBreakingWaitingTime);
                        for (int a = 0; a < 12; a++)//吸嘴数量 
                        {
                            FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y010_左1上下料机头1吸真空 + a, false);
                        }
                        FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.飞梭吸嘴上升安全位;
                        break;

                    case LeftTrayArmStatus.飞梭吸嘴上升安全位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左1上下料机头Z轴, "安全位"))
                        {
                            FlowFactory.variable.LeftBIBArmVar.TrayMappedBIB = FlowFactory.variable.LeftTrayArmVar.BIBMappedTray;
                            FlowFactory.variable.LeftTrayArmVar.BIBMappedTray = FlowFactory.variable.LeftTrayArmVar.BIBMappedTray + 1;
                            if (FlowFactory.variable.LeftTrayArmVar.BIBMappedTray == FlowFactory.variable.LeftTrayArmVar.BIBCountTray)
                            {
                                FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.运动待机位;
                            }
                            else
                            {
                                FlowFactory.FlowStep.LeftTrayArmStep = LeftTrayArmStatus.根据BIB板情况判断Tray吸嘴;
                            }
                        }
                            break;

                    case LeftTrayArmStatus.运动待机位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左1上下料机头Z轴, "安全位"))
                        {
                            if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左1上下料机头X轴, "待机位"))
                            {
                                FlowFactory.variable.LeftTrayArmVar.BIBAllowActions = true;
                            }
                        }
                            break;

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
                    LeftTrayArmRun();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
