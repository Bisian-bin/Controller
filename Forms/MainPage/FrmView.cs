using DevComponents.DotNetBar.Controls;
using InnerEvents;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace Forms
{
    public partial class FrmView : Form
    {
        private IEventAggregator eventAggregator;
        private IUnityContainer container;
        public static string PageName= "ProdList";

        public FrmView(IEventAggregator eventAggregator, IUnityContainer container)
        {
            InitializeComponent();

            this.eventAggregator = eventAggregator;
            this.container = container;            
        }

        private FrmView _instance;

        public FrmView Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new FrmView(eventAggregator, container);
                return _instance;
            }
        }

        private async void FrmUser_Load(object sender, EventArgs e)
        {
            eventAggregator.GetEvent<UserEvent>().Subscribe((o) =>
            {
                webView21.Invoke(new Action(() =>
                {
                    if(o.ToString()== "OEE"|| o.ToString() == "OEE")
                    {
                        webView21.CoreWebView2.Navigate("file:///" + Application.StartupPath + "/dist/index.html#/OEE");
                    }
                    else if (o.ToString() == "报警信息" || o.ToString() == "Alarm Info")
                    {
                        webView21.CoreWebView2.Navigate("file:///"+ Application.StartupPath + "/dist/index.html#/AlarmList");
                    }
                    else if (o.ToString() == "生产记录" || o.ToString() == "Prod Record")
                    {
                        webView21.CoreWebView2.Navigate("file:///" + Application.StartupPath + "/dist/index.html#/ProdList");
                    }
                    else if (o.ToString() == "吸嘴监控" || o.ToString() == "Nozzle Monit")
                    {
                        webView21.CoreWebView2.Navigate("file:///" + Application.StartupPath + "/dist/index.html#/NozzleList");
                    }
                    else if (o.ToString() == "UPH" || o.ToString() == "UPH")
                    {
                        webView21.CoreWebView2.Navigate("file:///" + Application.StartupPath + "/dist/index.html#/UPH");
                    }
                    else
                    {
                        webView21.CoreWebView2.Navigate("file:///" + Application.StartupPath + "/dist/index.html#/OEE");
                    }
                }));                
            }, ThreadOption.BackgroundThread);
            ////生成json
            //PosDefine post = new PosDefine();
            //post.No = "1";
            //post.PonitLocation = "1";
            //post.Coordinates = "1,2,3";

            //PosDefine post1 = new PosDefine();
            //post.No = "2";
            //post.PonitLocation = "2";
            //post.Coordinates = "1,2,3";

            //PosDefine post3 = new PosDefine();
            //post.No = "3";
            //post.PonitLocation = "3";
            //post.Coordinates = "1,2,3";

            //PosDefine post4 = new PosDefine();
            //post.No = "4";
            //post.PonitLocation = "4";
            //post.Coordinates = "1,2,3";

            //TechData axis = new TechData();
            //axis.AxisName = "X轴";
            //axis.PosDif.Add(post);
            //axis.PosDif.Add(post1);

            //TechData axis1 = new TechData();
            //axis.AxisName = "Y轴";
            //axis.PosDif.Add(post3);
            //axis.PosDif.Add(post4);

            //Tech tech = new Tech
            //{
            //    TechDatas = new List<TechData> { axis, axis1 }
            //};

            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(tech);

            // 初始化WebView2环境（需指定运行时路径，或自动使用系统安装的版本）
            var envOptions = new CoreWebView2EnvironmentOptions();
            var environment = await CoreWebView2Environment.CreateAsync(null, null, envOptions);

            // 关联WebView2控件与环境
            await webView21.EnsureCoreWebView2Async(environment);
            webView21.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested; //禁用右键
            webView21.CoreWebView2.Settings.AreDevToolsEnabled = false;
            // 加载网页（可替换为任意合法URL）
            webView21.CoreWebView2.Navigate("file:///"+ Application.StartupPath + "/dist/index.html#/OEE");
        }

        private void CoreWebView2_ContextMenuRequested(object sender, Microsoft.Web.WebView2.Core.CoreWebView2ContextMenuRequestedEventArgs e)
        {
            e.Handled = true;
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

        private async Task LoadDataAsync()
        {
            ////数据库增删改查
            //// 用户操作
            //using (var userRepo = new EntityRepository<User>())
            //{
            //    string strsql = "select b.Permission from User a inner join Role b on a.Name=b.Name where a.ID=@ID";
            //    DataTable dt = userRepo.ExecuteQuery(strsql, new Dictionary<string, object> { { "@ID", "2" } });


            //    // 添加用户
            //    var newUser = new User { Name = "testtest", Password = "123", Score = 11 };
            //    long userId = await userRepo.Add(newUser);
            //    Console.WriteLine($"添加用户成功，ID: {userId}");

            //    // 条件查询
            //    var users = await userRepo.Query("Name = @Name", new Dictionary<string, object> { { "@Name", "testtest" } });
            //    Console.WriteLine($"找到 {users.Count} 条数据");
            //    //dataGridView1.DataSource = users;

            //    // 更新用户
            //    var userToUpdate = userRepo.GetById(2);
            //    userToUpdate.Name = "newemail";
            //    int i = await userRepo.Update(userToUpdate, "ID = @ID", new Dictionary<string, object> { { "@id", "2" } });

            //    //// 删除用户
            //    i = await userRepo.Delete("Id = @id", new Dictionary<string, object> { { "@id", "6" } });
            //}
        }

        private void FrmView_VisibleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
