using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class frmIO : Form
    {
        private IEventAggregator eventAggregator;
        private IUnityContainer container;

        public frmIO(IEventAggregator eventAggregator, IUnityContainer container)
        {
            InitializeComponent();

            this.eventAggregator = eventAggregator;
            this.container = container;
        }

        private frmIO _instance;

        public frmIO Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new frmIO(eventAggregator, container);
                return _instance;
            }
        }

        protected override CreateParams CreateParams
        {
            //重写WinForms 控件的 CreateParams 属性，通过修改窗口的扩展样式（ExStyle）来
            //启用 WS_EX_COMPOSITED 标志（0x02000000），主要用于解决 UI 渲染时的闪烁问题
            get
            {
                CreateParams paras = base.CreateParams;
                paras.ExStyle |= 0x02000000;
                return paras;
            }
        }

        private void frmIO_Load(object sender, EventArgs e)
        {
            IO_Form ioform = new IO_Form(eventAggregator, container);
            ioform.GenerateButtons(this);
        }
    }
}
