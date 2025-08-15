using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using SECSGEM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Forms
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            UnityBoot boot = new UnityBoot();
            boot.Run();
        }
    }

    public class UnityBoot : UnityBootstrapper
    {
        private MainModule myForm;
        protected override DependencyObject CreateShell()
        {
            myForm = this.Container.TryResolve<MainModule>();
            return myForm;
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            //moduleCatalog.AddModule(typeof(MainModule));
        }
        
    }

    public class MainModule : Window
    {
        public MainModule(IEventAggregator eventAggregator, IUnityContainer container)
        {
            //初始化
            HSMS proxy = container.Resolve<HSMS>();

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MainForm(eventAggregator, container));
        }
    }
}
