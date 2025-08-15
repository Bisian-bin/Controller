using DataAccess;
using Model;
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
    public partial class frmAddUser : Form
    {
        private User _editingUser; // 编辑的用户对象
        private bool _isEditing;   // 是否为编辑模式
        private List<Role> _allRoles;
        public frmAddUser()
        {
            InitializeComponent();
            _isEditing = false;
            this.Text = "添加用户";
            frm_AddUser1.SaveButtonClick += Frm_AddUser1_SaveButtonClick;
            frm_AddUser1.CloseButtonClick += Frm_AddUser1_CloseButtonClick;
            frm_AddUser1.UserDropdown.SelectedValueChanged += Dropdown1_SelectedValueChanged;
            this.Load += frmAddUser_Load;
            LoadRoles();
        }

        public frmAddUser(User user)
        {
            InitializeComponent();
            _editingUser = user;
            _isEditing = true;
            this.Text = "编辑用户";
            frm_AddUser1.SaveButtonClick += Frm_AddUser1_SaveButtonClick;
            frm_AddUser1.CloseButtonClick += Frm_AddUser1_CloseButtonClick;
            frm_AddUser1.UserDropdown.SelectedValueChanged += Dropdown1_SelectedValueChanged;
            this.Load += frmAddUser_Load;
            LoadRoles();
        }

        // 窗体加载时填充数据（编辑模式）
        private void frmAddUser_Load(object sender, EventArgs e)
        {
            if (_isEditing && _editingUser != null)
            {
                frm_AddUser1.UserName = _editingUser.UserName;
                frm_AddUser1.UserPassWord = _editingUser.Password;
                frm_AddUser1.UserStatus = _editingUser.IsEnabled;

                // 使用已加载的角色列表查找角色
                var role = _allRoles?.FirstOrDefault(r => r.RoleID == _editingUser.RoleID);

                if (role != null)
                {
                    // 使用 SelectedValue 设置选中项
                    frm_AddUser1.UserDropdown.Text = role.RoleName;
                }
            }
        }
        // 加载角色列表
        private async void LoadRoles()
        {
            var roleRepo = new EntityRepositorys<Role>();
            try
            {
                _allRoles = await roleRepo.Query();
                frm_AddUser1.UserDropdown.Items.Clear();

                foreach (var role in _allRoles)
                {
                    frm_AddUser1.UserDropdown.Items.Add(role.RoleName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载角色列表失败: {ex.Message}");
                LogService.LogService.Error("frmAddUser类的LoadRoles方法", ex);
            }
            finally
            {
                roleRepo.Dispose();
            }
        }
        // 角色下拉框值改变时
        private void Dropdown1_SelectedValueChanged(object sender, EventArgs e)
        {
            // 获取选中的值
            var selectedValue = frm_AddUser1.UserDropdown.SelectedValue;
            if (selectedValue != null)
            {
                // 查找选中值对应的角色名称
                for (int i = 0; i < frm_AddUser1.UserDropdown.Items.Count; i++)
                {
                    if (frm_AddUser1.UserDropdown.Items[i].Equals(selectedValue))
                    {
                        frm_AddUser1.UserDropdown.Text = frm_AddUser1.UserDropdown.Items[i].ToString();
                        break;
                    }
                }
            }
        }
        // 保存按钮点击事件
        private async void Frm_AddUser1_SaveButtonClick(object sender, EventArgs e)
        {
            if (!frm_AddUser1.ValidateForm())
            {
                return;
            }

            string userName = frm_AddUser1.UserName.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }

            var userRepo = new EntityRepositorys<User>();
            try
            {
                // 检查用户名是否已存在（不包括当前编辑的用户）
                List<User> existingUsers;
                if (_isEditing && _editingUser != null)
                {
                    existingUsers = await userRepo.Query($"UserName = @UserName AND UserID != @UserID",
                        new { UserName = userName, UserID = _editingUser.UserID });
                }
                else
                {
                    existingUsers = await userRepo.Query($"UserName = @UserName",
                        new { UserName = userName });
                }
                if (existingUsers.Any())
                {
                    MessageBox.Show($"已存在名为'{userName}'的用户，请使用不同的用户名");
                    return;
                }

                var user = new User
                {
                    UserName = userName,
                    Password = frm_AddUser1.UserPassWord,
                    IsEnabled = frm_AddUser1.UserStatus
                };

                // 通过角色名称查询角色ID
                string selectedRoleName = frm_AddUser1.UserDropdown.Text.Trim();
                if (string.IsNullOrEmpty(selectedRoleName))
                {
                    MessageBox.Show("请选择角色");
                    return;
                }

                var roleRepo = new EntityRepositorys<Role>();
                try
                {
                    // 根据角色名称查询角色对象
                    var roles = await roleRepo.Query($"RoleName = @RoleName",
                        new { RoleName = selectedRoleName });
                    if (roles.Count == 0)
                    {
                        MessageBox.Show($"未找到角色名为'{selectedRoleName}'的角色");
                        return;
                    }
                    user.RoleID = roles[0].RoleID;
                }
                catch (Exception roleEx)
                {
                    MessageBox.Show($"查询角色ID失败: {roleEx.Message}");
                    LogService.LogService.Error("frmAddUser类的LoadRoles方法", roleEx);
                    return;
                }
                finally
                {
                    roleRepo.Dispose();
                }

                int userId;
                if (_isEditing && _editingUser != null)
                {
                    user.UserID = _editingUser.UserID;
                    await userRepo.Update(user);
                    userId = user.UserID;
                }
                else
                {
                    userId = (int)await userRepo.Add(user);
                }

                frm_AddUser1.ClearInputs();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败: {ex.Message}");
                LogService.LogService.Error("frmAddUser类的Frm_AddUser1_SaveButtonClick方法", ex);
            }
            finally
            {
                userRepo.Dispose();
            }
        }
        private void Frm_AddUser1_CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}