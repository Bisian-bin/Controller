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
    /// 左侧Tray盘步序
    /// </summary>
    public enum LeftMoveTrayStatus
    {
        Tray盘初始化,
        Tray机构运动取盘位,
        Tray上顶轴前运动上升位,
        Tray上顶轴前运动下降位,
        Tray上顶轴前运动等待位,
        判断Tray机构是否有盘,
        Tray进行上下料,
        Tray上顶轴后运动等待位,
        Tray上顶轴后运动上升位,
        Tray机构运动收盘位,
        Tray收空盘完成运动等待位,
        结批Tray机构运动取盘位,
        结批Tray上顶轴前运动上升接盘位,
        结批Tray上顶轴前运动上升位,
        结批Tray上顶轴前运动等待位,
        结批完成
    }
    #endregion

    /// <summary>
    /// 左侧tray机构
    /// </summary>
    public class LeftMoveTray : QMFlow
    {
        /// <summary>
        /// 执行动作流程
        /// </summary>
        public void LeftTrayArmRun()
        {
            if (FlowFactory.RunStatus == Order.MachineStatuOrder.Running)//设备生产状态
            {
                switch (FlowFactory.FlowStep.LeftMoveTrayStep)
                {
                    #region Tray盘上下料

                    case LeftMoveTrayStatus.Tray盘初始化:
                        if (FlowFactory.AxisContorl.GetXStatus((int)Order.InOrder.X060_左放Tray无盘信号))
                        {
                            if (FlowFactory.AxisContorl.GetXStatus((int)Order.InOrder.X061_左放Tray满盘信号))
                            {
                                if (FlowFactory.AxisContorl.GetXStatus((int)Order.InOrder.X055_左移Tray盘子有无检测))
                                {
                                    FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray机构运动取盘位;
                                }
                                else
                                {
                                    FlowFactory.ChildReceiveDown(7002);//触发报警-X055_左移Tray盘机构有盘，请确认!
                                }
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(7001);//触发报警-X061_左放Tray盘满盘，请确认!
                            }
                        }
                        else
                        {
                            FlowFactory.ChildReceiveDown(7000);//触发报警-X060_左放Tray盘无料，请确认!
                        }
                        break;

                    case LeftMoveTrayStatus.Tray机构运动取盘位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左移TrayY轴, "取盘位"))
                        {
                            FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray上顶轴前运动上升位;
                        }
                        break;

                    case LeftMoveTrayStatus.Tray上顶轴前运动上升位:
                        if (FlowFactory.AxisContorl.TwoMoveRelAuto((int)Order.AxisOrder.右移Tray上顶步进轴前, "上升位", 
                            (int)Order.AxisOrder.左移Tray上顶步进轴后, "等待位"))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y083_左分Tray定位气缸出,
                               (int)Order.InOrder.X056_左分Tray定位气缸回1, (int)Order.InOrder.X057_左分Tray定位气缸出1, true))
                            {
                                FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray上顶轴前运动下降位;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(7003);//X056_左分Tray定位气缸回1感应器异常，请确认!
                            }
                        }
                        break;

                    case LeftMoveTrayStatus.Tray上顶轴前运动下降位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.右移Tray上顶步进轴前, "下降位"))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y083_左分Tray定位气缸出,
                             (int)Order.InOrder.X057_左分Tray定位气缸出1, (int)Order.InOrder.X056_左分Tray定位气缸回1, false))
                            {
                                FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray上顶轴前运动等待位;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(7004); //X057_左分Tray定位气缸出1感应器异常，请确认!
                            }
                        }
                        break;

                    case LeftMoveTrayStatus.Tray上顶轴前运动等待位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.右移Tray上顶步进轴前, "等待位"))
                        {
                            FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.判断Tray机构是否有盘;
                        }
                        break;

                    case LeftMoveTrayStatus.判断Tray机构是否有盘:
                        if (!FlowFactory.AxisContorl.GetXStatus((int)Order.InOrder.X055_左移Tray盘子有无检测))
                        {
                            FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray进行上下料;
                        }
                        else
                        {
                            FlowFactory.ChildReceiveDown(7005); //X055_左移Tray盘机构无盘，请确认!
                        }
                        break;

                    #endregion

                    #region Tray盘收空盘

                    case LeftMoveTrayStatus.Tray上顶轴后运动等待位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.右移Tray上顶步进轴后, "等待位"))
                        {
                            FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray机构运动收盘位;
                        }
                        break;

                    case LeftMoveTrayStatus.Tray机构运动收盘位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左移TrayY轴, "收盘位"))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y082_左移Tray定位气缸出,
                            (int)Order.InOrder.X052_左移Tray定位气缸回1, (int)Order.InOrder.X053_左移Tray定位气缸出, false))
                            {
                                FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray上顶轴后运动上升位;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(7004); //X057_左分Tray定位气缸出1感应器异常，请确认!
                            }
                        }
                        break;

                    case LeftMoveTrayStatus.Tray上顶轴后运动上升位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.右移Tray上顶步进轴后, "上升位"))
                        {
                            if (FlowFactory.AxisContorl.GetPelStatus((int)Order.AxisOrder.左移Tray上顶步进轴后))
                            {
                                FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray机构运动取盘位;
                            }
                        }
                        break;

                    case LeftMoveTrayStatus.Tray收空盘完成运动等待位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左移TrayY轴, "取盘位"))
                        {

                            FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.Tray盘初始化;
                        }
                        break;

                    #endregion

                    #region 结批完成收有料盘
                    case LeftMoveTrayStatus.结批Tray机构运动取盘位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.左移TrayY轴, "取盘位"))
                        {
                            FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.结批Tray上顶轴前运动上升接盘位;
                        }
                        break;

                    case LeftMoveTrayStatus.结批Tray上顶轴前运动上升接盘位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.右移Tray上顶步进轴前, "上升接盘位"))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y083_左分Tray定位气缸出,
                              (int)Order.InOrder.X056_左分Tray定位气缸回1, (int)Order.InOrder.X057_左分Tray定位气缸出1, true))
                            {
                                FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.结批Tray上顶轴前运动上升位;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(7003);//X056_左分Tray定位气缸回1感应器异常，请确认!
                            }
                        }
                        break;

                    case LeftMoveTrayStatus.结批Tray上顶轴前运动上升位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.右移Tray上顶步进轴前, "上升位"))
                        {
                            if (FlowFactory.AxisContorl.CylinderAuto((int)Order.OutOrder.Y083_左分Tray定位气缸出,
                            (int)Order.InOrder.X057_左分Tray定位气缸出1, (int)Order.InOrder.X056_左分Tray定位气缸回1, false))
                            {
                                FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.结批Tray上顶轴前运动等待位;
                            }
                            else
                            {
                                FlowFactory.ChildReceiveDown(7004); //X057_左分Tray定位气缸出1感应器异常，请确认!
                            }
                        }
                        break;

                    case LeftMoveTrayStatus.结批Tray上顶轴前运动等待位:
                        if (FlowFactory.AxisContorl.MoveRelAuto((int)Order.AxisOrder.右移Tray上顶步进轴前, "等待位"))
                        {
                            FlowFactory.FlowStep.LeftMoveTrayStep = LeftMoveTrayStatus.结批完成;
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
