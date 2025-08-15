using QMBaseClass;
using QMBaseClass.板卡.InterfaceCard;
using QMBaseClass.板卡.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public class AxisInfo
    {
        #region 调用板卡

        /// <summary>
        /// 轴卡对象
        /// </summary>
        public  IAutomationDevice.IAxisControl Axis = null;


        /// <summary>
        /// 调用板卡
        /// </summary>
        /// <returns></returns>
        public  bool InitialAll()
        {
            return mGlobal.CreatAxisObject(mGlobal.AxisCardTpye.GSN, Application.StartupPath, ref Axis) == mGlobal.CreatObjectError.NoError ? true : false;
        }

        #endregion

        #region 初始化板卡
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitCardAsync()
        {
            return await Axis.InitCardAsync() == 0 ? true : false;
        }

        #endregion 

        #region 轴运动

        /// <summary>
        /// 自动运动轴方法
        /// </summary>
        /// <param name="AxisId">轴号</param>
        /// <param name="pos">轴点位名称</param>
        /// <param name="SpeedPercent">百分比</param>
        /// <returns></returns>
        public bool MoveRelAuto(int AxisNo, string posName)
        {
            double Doublepos = GetAxisTargetPos(AxisNo, posName);
            // 单轴运动
            var singleAxis = new List<(int, double)> { (AxisNo, Doublepos) };
            return MultiAxisMove(singleAxis);
        }

        /// <summary>
        /// 双轴运动
        /// </summary>
        /// <param name="AxisNo1">轴号1#</param>
        /// <param name="posName1">轴点位名称</param>
        /// <param name="AxisNo2">轴号2#</param>
        /// <param name="posName2">轴点位名称</param>
        /// <returns></returns>
        public bool TwoMoveRelAuto(int AxisNo1, string posName1, int AxisNo2, string posName2)
        {
            double Doublepos1 = GetAxisTargetPos(AxisNo1, posName1);
            double Doublepos2 = GetAxisTargetPos(AxisNo2, posName2);
            // 双轴运动
            var twoAxes = new List<(int, double)>
            {
               (AxisNo1, Doublepos1),
               (AxisNo2, Doublepos2)
            };
            return MultiAxisMove(twoAxes);
        }

        /// <summary>
        /// Transfer自动运动轴方法
        /// </summary>
        /// <param name="AxisNo">轴号</param>
        /// <param name="pos">轴点位名称</param>
        /// <param name="SpeedPercent">百分比</param>
        /// <returns></returns>
        public bool TransferMoveAuto(int AxisNo, string posName, int Num)
        {
            double Doublepos = GetAxisTargetPos(AxisNo, posName)- (Num) * FlowFactory.recipe.TransferStationDimensions.TransferSpacing;
            // 单轴运动
            var singleAxis = new List<(int, double)> { (AxisNo, Doublepos) };
            return MultiAxisMove(singleAxis);
        }

        /// <summary>
        /// BIB自动运动轴方法
        /// </summary>
        /// <param name="AxisNo">轴号</param>
        /// <param name="pos">轴点位名称</param>
        /// <param name="SpeedPercent">百分比</param>
        /// <returns></returns>
        public bool BIBMoveAuto(int AxisNo, string posName, int Num)
        {
            double Doublepos = GetAxisTargetPos(AxisNo, posName) + (Num) * FlowFactory.recipe.BIBBcardDimensions.BIBXSpacing;
            // 单轴运动
            var singleAxis = new List<(int, double)> { (AxisNo, Doublepos) };
            return MultiAxisMove(singleAxis);
        }


        /// <summary>
        /// 左侧TrayArm
        /// </summary>
        /// <param name="TrayRows"></param>
        /// <param name="TrayCells"></param>
        /// <param name="Nozzle"></param>
        /// <returns></returns>
        public bool LeftTrayMove(int TrayRows, int TrayCells, int Nozzle)
        {
            double Doublepos1 = Axis.PosObjData.TryGetValue(0, out var pos1) ? pos1.PosDif.FirstOrDefault(p => p.PonitLocation == "Tray第一排起始位")?.Pos ?? 0 : 0
                - (TrayRows) * FlowFactory.recipe.TrayDimensions.TrayYSpacing;
            double Doublepos6 = Axis.PosObjData.TryGetValue(5, out var pos6) ? pos6.PosDif.FirstOrDefault(p => p.PonitLocation == "Tray第一排起始位")?.Pos ?? 0 : 0
                + (TrayCells- Nozzle) * FlowFactory.recipe.TrayDimensions.TrayXSpacing; ;
            double Doublepos3 = Axis.PosObjData.TryGetValue(2, out var pos3) ? pos3.PosDif.FirstOrDefault(p => p.PonitLocation == "Tray变距")?.Pos ?? 0 : 0;

            // 三轴运动
            var twoAxes = new List<(int, double)>
            {
               (0, Doublepos1),
               (5, Doublepos6),
               (2, Doublepos3)
            };
            return MultiAxisMove(twoAxes);
        }


        /// <summary>
        /// 多轴运动（支持任意数量轴同步运动）
        /// </summary>
        /// <param name="axisMovements">轴运动信息列表，包含轴号和点位名称</param>
        /// <returns>所有轴是否都运动到位</returns>
        public bool MultiAxisMove(List<(int AxisNo, double targetPos)> axisMovements)
        {
            // 验证输入参数
            if (axisMovements == null || axisMovements.Count == 0)
            {
                throw new ArgumentException("至少需要指定一个轴的运动信息", nameof(axisMovements));
            }

            bool allAxesInPosition = true;

            // 遍历所有轴，执行运动并检查到位状态
            foreach (var movement in axisMovements)
            {
                int axisIndex = movement.AxisNo - 1;

                // 验证轴号有效性
                if (axisIndex < 0 || axisIndex >= Axis.AxisList.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(axisMovements),
                        $"轴号 {movement.AxisNo} 无效，超出范围");
                }

                // 执行轴运动
                var axis = Axis.AxisList[axisIndex];
                Axis.MoveAbs(axis.AxisNo, movement.targetPos, axis.SpeedPercent);

                // 检查是否到位
                bool isInPosition = (movement.targetPos + axis.PosOffset >= axis.AxisStatus.CurrentPos &&
                                     movement.targetPos - axis.PosOffset <= axis.AxisStatus.CurrentPos);

                // 只要有一个轴不到位，整体结果就为false
                allAxesInPosition &= isInPosition;
            }
            return allAxesInPosition;
        }


        /// <summary>
        /// 使能开启/关闭
        /// </summary>
        /// <param name="AxisNo">轴号</param>
        /// <param name="IsMotorEnabled">使能状态</param>
        public void MotorEnable(int AxisNo, bool IsMotorEnabled)
        {
            Axis.Motor_Enable(AxisNo, IsMotorEnabled);
        }

        /// <summary>
        /// 设备停止运动
        /// </summary>
        public void MotionStop()
        {
            FlowFactory.AxisContorl.Axis.AxisList.ForEach(async ca1 =>
            {
                await Axis.DecStop(ca1.AxisNo);
            });
        }

        /// <summary>
        /// 单轴归零
        /// </summary>
        /// <param name="AxisNo">轴号</param>
        /// <returns></returns>
        public async Task<bool> HomeSingleAxis(int AxisNo)
        {
            return await Axis.Axis_GoHome(AxisNo);
        }

        /// <summary>
        /// 轴运动
        /// </summary>
        /// <param name="AxisNo">轴号</param>
        /// <param name="currentPos">位置</param>
        /// <param name="SpeedPercentage">速度百分比</param>
        public void MoveAxis(int AxisNo, double currentPos, double SpeedPercentage)
        {
            Axis.MoveAbs(AxisNo, currentPos, SpeedPercentage);
        }

        /// <summary>
        /// 清除状态
        /// </summary>
        /// <param name="AxisNo"></param>
        public void ClearAxisStatus(int AxisNo)
        {
            Axis.Clear_AxisStatus(AxisNo);
        }

        /// <summary>
        /// 获取实际值
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <returns></returns>
        public double GetAxisActualPos(int AxisNo)
        {
            return Axis.AxisList[AxisNo].AxisStatus.CurrentPos;
        }

        /// <summary>
        /// 获取目标值
        /// </summary>
        /// <param name="AxisNo"></param>
        /// <param name="posName"></param>
        /// <returns></returns>
        public double GetAxisTargetPos(int AxisNo, string posName)
        {
            return Axis.PosObjData.TryGetValue(AxisNo - 1, out var pos) ? pos.PosDif.FirstOrDefault(p => p.PonitLocation == posName)?.Pos ?? 0 : 0;
        }

        #endregion

        #region 输入输出

        /// <summary>
        /// 气缸报警事件
        /// </summary>
        /// <param name="OutNo">输出编号</param>
        /// <param name="X1">输入信号1</param>
        /// <param name="X2">输入信号2</param>
        /// <param name="flog">标志位</param>
        /// <returns>1=成功，0=报警</returns>
        public bool CylinderAuto(int OutNo, int X1, int X2, bool flog)
        {
            OutAuto(OutNo, flog);
            DateTime StartWorkTime = DateTime.Now;
            try
            {
                while (true)
                {
                    Application.DoEvents();
                    if (DateTime.Now.Subtract(StartWorkTime).Seconds > 10) return false;
                    return X1 == X2 ? GetXStatus(X1) : GetXStatus(X1) & !GetXStatus(X2);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 5位3通气缸报警事件
        /// </summary>
        /// <param name="OutNo">输出编号</param>
        /// <param name="X1">输入信号1</param>
        /// <param name="X2">输入信号2</param>
        /// <param name="flog">标志位</param>
        /// <returns>1=成功，0=报警</returns>
        public bool CylinderAuto(int OutNo1, int OutNo2, int X1, int X2)
        {
            OutAuto(OutNo1, true); OutAuto(OutNo2, false);
            DateTime StartWorkTime = DateTime.Now;
            try
            {
                while (true)
                {
                    Application.DoEvents();
                    if (DateTime.Now.Subtract(StartWorkTime).Seconds > 10) return false;
                    return X1 == X2 ? GetXStatus(X1) : GetXStatus(X1) & !GetXStatus(X2);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///  循环时间判断感应器状态
        /// </summary>
        /// <param name="X">起始输入X</param>
        /// <returns></returns>
        public bool XStatusWait(int X)
        {
            DateTime StartWorkTime = DateTime.Now;
            try
            {
                while (true)
                {
                    Application.DoEvents();
                    if (DateTime.Now.Subtract(StartWorkTime).Seconds > 10) return false;
                    if (GetXStatus(X)){ return true; }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 输出信号
        /// </summary>
        /// <param name="num">输出点位</param>
        /// <param name="flog">标志位</param>
        public void OutAuto(int num, bool flog)
        {
            Axis.OutAuto(num, flog);
        }

        /// <summary>
        /// 获取输入IO状态
        /// </summary>
        /// <param name="X">输入序列号</param>
        /// <returns></returns>
        public bool GetXStatus(int X)
        {
            return Axis.InputList[X].State.Val ? true : false;
        }

        /// <summary>
        ///  获取输出IO状态
        /// </summary>
        /// <param name="Y">输出序列号</param>
        /// <returns></returns>
        public bool GetYStatus(int Y)
        {
            return Axis.OutputList[Y].State.Val ? true : false;
        }

        /// <summary>
        /// 获取正极限状态
        /// </summary>
        /// <param name="AxisNo">轴号</param>
        /// <returns></returns>
        public bool GetPelStatus(int AxisNo)
        {
            DateTime StartWorkTime = DateTime.Now;
            try
            {
                while (true)
                {
                    Application.DoEvents();
                    if (DateTime.Now.Subtract(StartWorkTime).Seconds > 2) return false;
                    if (Axis.AxisList[AxisNo].AxisStatus.IsPel.Val) { return true; }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 获取负极限状态
        /// </summary>
        /// <param name="AxisNo">轴号</param>
        /// <returns></returns>
        public bool GetNelStatus(int AxisNo)
        {
            DateTime StartWorkTime = DateTime.Now;
            try
            {
                while (true)
                {
                    Application.DoEvents();
                    if (DateTime.Now.Subtract(StartWorkTime).Seconds > 2) return false;
                    if (Axis.AxisList[AxisNo].AxisStatus.IsNel.Val) { return true; }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

    }
}
