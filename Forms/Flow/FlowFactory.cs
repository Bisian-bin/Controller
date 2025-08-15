using Forms.Flow;
using QMBaseClass.控件;
using static Forms.Flow.Order;

namespace Forms
{
    public class FlowFactory
    {

        #region  流程步序

        public class FlowStep
        {
            /// <summary>
            /// 中转区流程状态 
            /// </summary>
            public static TransferStatus TransferStep = TransferStatus.中转区初始化;

            /// <summary>
            /// 左侧Tray盘流程状态
            /// </summary>
            public static LeftMoveTrayStatus LeftMoveTrayStep = LeftMoveTrayStatus.Tray盘初始化;

            /// <summary>
            /// 左侧Tray手臂流程
            /// </summary>
            public static LeftTrayArmStatus LeftTrayArmStep = LeftTrayArmStatus.Tray盘初始化;

            /// <summary>
            /// 左侧BIB手臂流程
            /// </summary>
            public static LeftBIBArmStatus LeftBIBArmStep = LeftBIBArmStatus.初始化状态;



        }

        #endregion

        #region 变量

        /// <summary>
        /// 参数界面变量
        /// </summary>
        public static ParamPage.ParamData recipe = frmParam.settings;

        /// <summary>
        /// 常用变量
        /// </summary>
        public static Variable variable { get; set; }


        #endregion

        #region 其他


        /// <summary>
        /// 设备状态
        /// </summary>
        public static MachineStatuOrder RunStatus = MachineStatuOrder.InitialState;

        /// <summary>
        /// 报警委托
        /// </summary>
        public static FrmUser.ReceiveDelegate ChildReceiveDown { get; set; }


        public static ucLog LogView = new ucLog();

        /// <summary>
        /// 轴类
        /// </summary>
        public static AxisInfo AxisContorl;

        #endregion

    }
}
