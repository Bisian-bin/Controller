using QMBaseClass.工厂类;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Flow
{
    #region 流程步序

    /// <summary>
    /// 步序
    /// </summary>
    public enum LeftBIBArmStatus
    {
       初始化状态,

        #region 上料模式

        根据飞梭状态上料,
        去飞梭取料,
        飞梭Z轴下降,
        飞梭Z轴上升安全位,
        检查真空值,
        去BIB板放料,
        BIB板Z轴下降,
        打开破真空,



        #endregion
    }
    #endregion

    /// <summary>
    /// 左侧BIB状态
    /// </summary>
    public class LeftBIBArm : QMFlow
    {
        /// <summary>
        /// 执行动作流程
        /// </summary>
        public void LeftBIBArmRun()
        {
            if (FlowFactory.RunStatus == Order.MachineStatuOrder.Running)//设备生产状态
            {
                switch (FlowFactory.FlowStep.LeftBIBArmStep)
                {

                    #region 上料模式
                    case LeftBIBArmStatus.初始化状态:
                        if (FlowFactory.variable.Mode.OnlyLoadEnable)
                        {
                            if (FlowFactory.variable.TransferVar.AllowActions)
                            {
                                FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.初始化状态;
                            }
                        }
                        else if (FlowFactory.variable.Mode.OnlyUnloadEnable)
                        {

                        }
                        break;

                    case LeftBIBArmStatus.根据飞梭状态上料:
                        if (FlowFactory.variable.LeftTrayArmVar.BIBAllowActions)
                        {
                            for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                            {
                                if (FlowFactory.variable.LeftBIBArmVar.TrayNozzleStatus[a] == 6)
                                {
                                    FlowFactory.variable.LeftBIBArmVar.TrayNozzleStatus[a] = 7;//有料  
                                    FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.去飞梭取料;
                                }
                                else
                                {
                                    FlowFactory.variable.LeftBIBArmVar.TrayNozzleStatus[a] = 0;//无料
                                }
                            }
                        }
                        break;

                    case LeftBIBArmStatus.去飞梭取料:
                        if (FlowFactory.AxisContorl.GetAxisActualPos((int)Order.AxisOrder.左1上下料机头X轴) <= FlowFactory.AxisContorl.GetAxisTargetPos((int)Order.AxisOrder.左1上下料机头X轴, "待机位"))
                        {
                            if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左2上下料机头X轴, "飞梭取料位"))
                            {
                                for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                                {
                                    if (FlowFactory.variable.LeftBIBArmVar.TrayNozzleStatus[a] == 7)
                                    {
                                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y070_左2上下料机头1吸嘴气缸出 + a, true);
                                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y046_左2上下料机头1吸真空 + a, true);
                                    }
                                    else
                                    {
                                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y070_左2上下料机头1吸嘴气缸出 + a, false);
                                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y046_左2上下料机头1吸真空 + a, false);
                                    }
                                }
                                FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.飞梭Z轴下降;
                            }
                        }
                        break;

                    case LeftBIBArmStatus.飞梭Z轴下降:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左2上下料机头Z轴, "飞梭取料位"))
                        {
                            System.Threading.Thread.Sleep(FlowFactory.variable.LeftBIBArmVar.VacuumWaitingTime);
                            for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                            {
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y070_左2上下料机头1吸嘴气缸出 + a, false);
                            }
                            FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.飞梭Z轴上升安全位;
                        }
                            break;

                    case LeftBIBArmStatus.飞梭Z轴上升安全位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左2上下料机头Z轴, "安全位"))
                        {
                            FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.检查真空值;
                        }
                            break;

                    case LeftBIBArmStatus.检查真空值:
                        for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                        {
                            if (FlowFactory.variable.LeftBIBArmVar.TrayNozzleStatus[a] == 7)//检查是否报警
                            {
                                if (FlowFactory.AxisContorl.GetXStatus((int)Order.InOrder.X016_左1上下料机头1吸真空检测 + a)
                                    || FlowFactory.variable.Mode.RunMode.status == Variable.RunModeStatus.离线空跑)
                                {
                                    FlowFactory.variable.LeftBIBArmVar.TrayNozzleStatus[a] = 8;//=8 说明吸嘴真空满足要求
                                }
                                else
                                {
                                    FlowFactory.variable.LeftBIBArmVar.AlarmWaitingTime[a] = FlowFactory.variable.LeftBIBArmVar.AlarmWaitingTime[a] + 1;
                                    if (FlowFactory.variable.LeftBIBArmVar.AlarmWaitingTime[a] >= 200)
                                    {
                                        FlowFactory.ChildReceiveDown(8000 + a);//吸真空检测异常
                                        FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.去飞梭取料;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                FlowFactory.variable.LeftBIBArmVar.AlarmWaitingTime[a] = 0;
                            }
                        }
                        if (FlowFactory.FlowStep.LeftBIBArmStep == LeftBIBArmStatus.检查真空值)
                        {
                            for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                            {
                                if (FlowFactory.variable.LeftBIBArmVar.AlarmWaitingTime[a] != 0)
                                {
                                    FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.检查真空值; break;
                                }
                            }
                            FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.去BIB板放料;
                        }
                        break;

                    case LeftBIBArmStatus.去BIB板放料:
                        if (FlowFactory.AxisContorl.BIBMoveAuto((int)Order.AxisOrder.左2上下料机头X轴, "左侧第一排放料", FlowFactory.variable.LeftBIBArmVar.TrayMappedBIB))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y084_左2机头开盖气缸出, (int)Order.OutOrder.Y085_左2机头开盖气缸回,
                                (int)Order.InOrder.X070_左2机头开盖气缸出感应, (int)Order.InOrder.X069_左2机头开盖气缸回感应))
                            {
                                for (int a = FlowFactory.variable.LeftTrayArmVar.NzlSrtPos; a < FlowFactory.variable.LeftTrayArmVar.NzlSrtPos + FlowFactory.variable.LeftTrayArmVar.NozzleNumber; a++)//吸嘴数量
                                {
                                    if (FlowFactory.variable.LeftBIBArmVar.TrayNozzleStatus[a] == 8)
                                    {
                                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y070_左2上下料机头1吸嘴气缸出 + a, true);
                                    }
                                    else
                                    {
                                        FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y070_左2上下料机头1吸嘴气缸出 + a, false);
                                    }
                                }
                                FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.BIB板Z轴下降;
                            }
                        }
                        break;

                    case LeftBIBArmStatus.BIB板Z轴下降:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左2上下料机头Z轴, "BIB板放料"))
                        {
                            for (int a =0; a < 12; a++)//吸嘴数量
                            {
                                FlowFactory.AxisContorl.OutAuto((int)Order.OutOrder.Y046_左2上下料机头1吸真空 + a, false);
                            }
                        }
                        FlowFactory.FlowStep.LeftBIBArmStep = LeftBIBArmStatus.BIB板Z轴下降;
                        break;


                    case LeftBIBArmStatus.打开破真空:

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
                    LeftBIBArmRun();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
