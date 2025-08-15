using QMBaseClass.工厂类;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Flow
{
    public class Machine : List<QMFlow>
    {
        //单例锁
        private static readonly object _instanceLock = new object();
        //单例
        private static Machine _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Machine Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Machine();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 中转区流程
        /// </summary>
        public Transfer TransferThread { get; set; }

        /// <summary>
        /// 左侧BIB手臂
        /// </summary>
        public LeftBIBArm LeftBIBArmThread { get; set; }

        /// <summary>
        /// 左侧Tray盘机构
        /// </summary>
        public LeftMoveTray LeftMoveTrayThread { get; set; }

        /// <summary>
        /// 左侧tray手臂
        /// </summary>
        public LeftTrayArm LeftTrayArmThread { get; set; }


        private Machine()
        {
            TransferThread = new Transfer();
            this.Add(TransferThread);
            LeftBIBArmThread = new LeftBIBArm();
            this.Add(LeftBIBArmThread);
            LeftMoveTrayThread = new LeftMoveTray();
            this.Add(LeftMoveTrayThread);
            LeftTrayArmThread = new LeftTrayArm();
            this.Add(LeftTrayArmThread);
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            foreach (QMFlow work in this)
            {
                try
                {
                    work.Start();
                }
                catch { }
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            foreach (QMFlow work in this)
            {
                try
                {
                    work.Stop();
                }
                catch { }
            }
        }
        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            foreach (QMFlow work in this)
            {
                try
                {
                    work.Pause();
                }
                catch { }
            }
        }
        /// <summary>
        /// 继续
        /// </summary>
        public void Continue()
        {
            foreach (QMFlow work in this)
            {
                try
                {
                    work.Continue();
                }
                catch (Exception ex)
                { }
            }
        }
    }
}
