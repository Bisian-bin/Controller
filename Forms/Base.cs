using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    // -----------------------------------------------------------------------------
    // <summary>
    // <para>
    // 系统名称  ：Controller
    // </para>
    // <para>
    // 程序名称  ：基础类
    // </para>
    // <para>
    // 作 者     ： Roy
    // </para>
    // <para>
    // 创建日期  ： 2025/05/29
    // </para>
    // <para>
    // 修改日期  ：    
    // </para>
    // <para>
    // 修改记录  ：  
    // </para>
    // </summary>
    // -----------------------------------------------------------------------------
    public class Base : Form
    {
        #region 进制转换

        /// <summary>
        /// 二进制转十进制
        /// </summary>
        /// <param name="binary">二进制字符串</param>
        /// <returns>十进制数字</returns>
        public int BinaryToDecimal(string binary)
        {
            return Convert.ToInt32(binary, 2);
        }

        /// <summary>
        /// 二进制转十六进制
        /// </summary>
        /// <param name="binary">二进制字符串</param>
        /// <returns>十六进制字符串</returns>
        public string BinaryToHexadecimal(string binary)
        {
            int decimalValue = BinaryToDecimal(binary);
            return DecimalToHexadecimal(decimalValue);
        }

        /// <summary>
        /// 十进制转二进制
        /// </summary>
        /// <param name="decimalNumber">十进制数字</param>
        /// <returns>二进制字符串</returns>
        public string DecimalToBinary(int decimalNumber)
        {
            return Convert.ToString(decimalNumber, 2);
        }

        /// <summary>
        /// 十进制转十六进制
        /// </summary>
        /// <param name="decimalNumber">十进制数字</param>
        /// <returns>十六进制字符串</returns>
        public string DecimalToHexadecimal(int decimalNumber)
        {
            return Convert.ToString(decimalNumber, 16).ToUpper();
        }

        /// <summary>
        /// 十六进制转十进制
        /// </summary>
        /// <param name="hexadecimal">十六进制字符串</param>
        /// <returns>十进制数字</returns>
        public int HexadecimalToDecimal(string hexadecimal)
        {
            return Convert.ToInt32(hexadecimal, 16);
        }

        /// <summary>
        /// 十六进制转二进制
        /// </summary>
        /// <param name="hexadecimal">十六进制字符串</param>
        /// <returns>二进制字符串</returns>
        public string HexadecimalToBinary(string hexadecimal)
        {
            int decimalValue = HexadecimalToDecimal(hexadecimal);
            return DecimalToBinary(decimalValue);
        }

        #endregion

        #region Null转换
        /// <summary>
        /// Null转换字符串
        /// </summary>
        /// <param name="pParm">输入</param>
        /// <returns>输出</returns>
        public string ConvertDBNullToStrEmpty(object objParm)
        {
            if (objParm == DBNull.Value || objParm == null)
            {
                return string.Empty;
            }
            else
            {
                return objParm.ToString().Trim();
            }
        }

        /// <summary>
        /// Null转换小数
        /// </summary>
        /// <param name="pParm">输入</param>
        /// <returns>输出</returns>
        public Decimal DBNullToDecimalZero(object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换日期
        /// </summary>
        /// <param name="obj">输入</param>
        /// <returns>输出</returns>
        public DateTime DBNullToDate(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Null转换Int
        /// </summary>
        /// <param name="pParm">输入</param>
        /// <returns>输出</returns>
        public int DBNullToIntZero(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 菜单

        //菜单数组
        public MenuEntity[,] MenuData = new MenuEntity[11, 11];
        public MenuList menulist = new MenuList();

        /// <summary>
        /// 加载菜单
        /// </summary>
        public void IniMenu()
        {
            //读取并解析json文件
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "res\\Menu.json");
            menulist = JsonConvert.DeserializeObject<MenuList>(json);

            foreach (var item in menulist.MenuInfo)
            {
                //循环加载菜单
                IniMenuData(DBNullToIntZero(item.Index)
                        , DBNullToIntZero(item.No)
                        , string.Empty
                        , item.ID
                        , item.Simplified
                        , item.English
                        , item.IMG
                        , item.Visible
                        , item.Page);
            }

            foreach (var item in menulist.SubMenu)
            {
                //循环加载子菜单
                IniMenuData(DBNullToIntZero(item.SubIndex)
                        , DBNullToIntZero(item.SubNo)
                        , item.MainID
                        , item.ID
                        , item.Simplified
                        , item.English
                        , string.Empty
                        , item.Visible
                        , item.Page);

            }
        }

        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <param name="PageNo">页编号</param>
        /// <param name="Item">项编号</param>
        /// <param name="MainID">父级菜单ID</param>
        /// <param name="ID">当前菜单ID</param>
        /// <param name="Simplified">中文</param>
        /// <param name="English">英文</param>
        /// <param name="ImageFilename">图片</param>
        /// <param name="Visible">是否显示</param>
        /// <param name="Page">对应的窗体名</param>
        private void IniMenuData(int PageNo, int Item, string MainID, string ID, string Simplified, string English, string ImageFilename, bool Visible, string Page)
        {
            MenuData[PageNo, Item].Text = new string[3];
            MenuData[PageNo, Item].MainID = MainID;
            MenuData[PageNo, Item].ID = ID;
            MenuData[PageNo, Item].Text[0] = Simplified;
            MenuData[PageNo, Item].Text[1] = SimplifiedToTraditional(Simplified);
            MenuData[PageNo, Item].Text[2] = English;
            if (!string.IsNullOrEmpty(ImageFilename))
            {
                MenuData[PageNo, Item].ImageName = AppDomain.CurrentDomain.BaseDirectory + ImageFilename;
            }
            MenuData[PageNo, Item].Visible = Visible;
            MenuData[PageNo, Item].Page = Page;
        }

        /// <summary>
        /// 获取匹配的ToolStripButton
        /// </summary>
        /// <param name="name">控件名</param>
        /// <param name="tsp">父容器</param>
        /// <returns></returns>
        public ToolStripButton GetToolStripButtonText(string name, ToolStrip tsp)
        {
            ToolStripButton btn = new ToolStripButton();
            // 遍历所有控件，查找 ToolStrip

            // 在 ToolStrip 中查找ToolStripButton
            foreach (ToolStripItem item in tsp.Items)
            {
                if (item.Name == name)
                {
                    btn = (ToolStripButton)item;
                    return btn;
                }
            }
            return btn;
        }

        #endregion

        #region 其他
        /// <summary>
        /// 简繁切换
        /// </summary>
        /// <param name="Text">简体字</param>
        /// <returns>繁体字</returns>
        public string SimplifiedToTraditional(string Text)
        {
            return ChineseConverter.Convert(Text, ChineseConversionDirection.SimplifiedToTraditional);
        }
        #endregion

        /// <summary>
        /// 根据页面权限统一控制所有Button的可用性
        /// </summary>
        /// <param name="parent">父控件</param>
        /// <param name="hasPagePermission">当前用户是否有该页面权限</param>
        public void ApplyButtonPermissionsByPage(Control parent, bool hasPagePermission)
        {
            FrmRole frmRole = new FrmRole();
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.HasChildren)
                    ApplyButtonPermissionsByPage(ctrl, hasPagePermission);
                if (ctrl is AntdUI.Button btn)
                {
                    if (btn.Name == "bt_Login" || btn.Name == "bt_Quit") continue;
                    try
                    {
                        btn.Enabled = hasPagePermission;
                        // 权限控制：没有FrmRole权限则禁用表格按钮和状态开关
                        bool RolePermission = Model.Context.CurrentPermissionNames?.Contains("FrmRole") == true;
                        frmRole.frm_RoleManage1.RoleTable.Enabled = RolePermission;
                        frmRole.frm_UserManage1.UserTable.Enabled = RolePermission;
                    }
                    catch { }
                }
            }
        }
    }
}
