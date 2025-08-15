using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using Model;

namespace Forms
{
    public partial class LoginPanel : UserControl
    {
        public Form DrawerForm { get; set; }

        public LoginPanel()
        {
            InitializeComponent();
            bt_Login.Click += Bt_Login_Click;
            txt_Password.PasswordChar = '●';
        }

        private async void Bt_Login_Click(object sender, EventArgs e)
        {
            string username = txt_User.Text.Trim();
            string password = txt_Password.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入用户名和密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var repo = new EntityRepositorys<User>())
            {
                var users = await repo.Query("UserName=@UserName AND Password=@Password AND IsEnabled=1", new { UserName = username, Password = password });
                if (users.Count > 0)
                {
                    var user = users[0];
                    // 查询角色
                    using (var roleRepo = new EntityRepositorys<Role>())
                    {
                        var roles = await roleRepo.Query("RoleID=@RoleID", new { RoleID = user.RoleID });
                        user.Role = roles.Count > 0 ? roles[0] : null;
                        Context.CurrentUser = user;
                        Context.CurrentRole = user.Role;
                    }
                    MessageBox.Show($"欢迎，{user.UserName}！", "登录成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 查询当前角色所有菜单权限名
                    using (var roleMenuRepo = new EntityRepositorys<Model.RoleMenuPermission>())
                    {
                        var roleId = user.RoleID;
                        var roleMenuPermissions = await roleMenuRepo.Query("RoleID=@RoleID", new { RoleID = roleId });
                        var permissionIds = roleMenuPermissions.Select(rmp => rmp.PermissionID).ToList();
                        using (var permRepo = new EntityRepositorys<Model.MenuPermission>())
                        {
                            var allPerms = await permRepo.Query();
                            var userPermissionNames = allPerms
                                .Where(mp => permissionIds.Contains(mp.PermissionID))
                                .Select(mp => mp.UIName)
                                .ToList();
                            Model.Context.CurrentPermissionNames = userPermissionNames;
                        }
                    }
                    // 登录成功后刷新所有页面按钮权限
                    var mainForm = Application.OpenForms.OfType<Forms.MainForm>().FirstOrDefault();
                    if (mainForm != null)
                    {
                        var role = Model.Context.CurrentRole;
                        foreach (var kv in mainForm.formInstances)
                        {
                            string pageKey = kv.Key;
                            Form pageForm = kv.Value;
                            bool hasPagePermission = Model.Context.CurrentPermissionNames?.Contains(pageKey) == true;
                            new Base().ApplyButtonPermissionsByPage(pageForm, hasPagePermission);
                        }
                    }
                    DrawerForm?.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误，或账号被禁用！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
