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
    public partial class frmAddRole : Form
    {
        private Role _editingRole; // 编辑的角色对象
        private bool _isEditing;   // 是否为编辑模式
        private List<int> _currentPermissionIds;
        public frmAddRole()
        {
            InitializeComponent();
            _isEditing = false;
            this.Text = "添加角色";
            frm_AddRole1.selectmultiple.CheckMode = true;
            frm_AddRole1.SaveButtonClick += Frm_AddRole1_SaveButtonClick;
            frm_AddRole1.CloseButtonClick += Frm_AddRole1_CloseButtonClick;
            frm_AddRole1.selectmultiple.SelectedValueChanged += Dropdown_multi_SelectedIndexChanged;
            this.Load += frmAddRole_Load;
            LoadMenuPermissions();
        }
        public frmAddRole(Role role)
        {
            InitializeComponent();
            _editingRole = role;
            _isEditing = true;
            this.Text = "编辑角色";
            frm_AddRole1.selectmultiple.CheckMode = true;
            frm_AddRole1.SaveButtonClick += Frm_AddRole1_SaveButtonClick;
            frm_AddRole1.CloseButtonClick += Frm_AddRole1_CloseButtonClick;
            frm_AddRole1.selectmultiple.SelectedValueChanged += Dropdown_multi_SelectedIndexChanged;
            this.Load += frmAddRole_Load;
            LoadMenuPermissions();

        }
        // 窗体加载时填充数据（编辑模式）
        private async void frmAddRole_Load(object sender, EventArgs e)
        {
            if (_isEditing && _editingRole != null)
            {
                frm_AddRole1.RoleName = _editingRole.RoleName;
                frm_AddRole1.RolePassWord = _editingRole.PermissionChar;
                frm_AddRole1.RoleStatus = _editingRole.IsEnabled;

                await LoadRolePermissions();
            }
        }
        // 获取当前角色的权限
        private async Task LoadRolePermissions()
        {
            try
            {
                // 获取当前角色已有的权限ID
                var roleMenuRepo = new EntityRepositorys<RoleMenuPermission>();
                var rolePermissions = await roleMenuRepo.Query($"RoleID = @RoleID",
                    new { RoleID = _editingRole.RoleID });
                _currentPermissionIds = rolePermissions.Select(rmp => rmp.PermissionID).ToList();
                roleMenuRepo.Dispose();

                // 重新加载所有权限并选中已有权限
                LoadMenuPermissions();

                // 选中已有的权限
                var selectedValues = new List<object>();
                foreach (var item in frm_AddRole1.selectmultiple.Items)
                {
                    // 强制转换为PermissionItem以获取ID
                    if (item is PermissionItem permItem && _currentPermissionIds.Contains(permItem.PermissionID))
                    {
                        selectedValues.Add(permItem);
                    }
                }
                frm_AddRole1.selectmultiple.SelectedValue = selectedValues.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载角色权限失败: {ex.Message}");
                LogService.LogService.Error("frmAddRole类的LoadRolePermissions方法", ex);
            }
        }

        // 加载菜单权限
        private async void LoadMenuPermissions()
        {
            var repository = new EntityRepositorys<MenuPermission>();
            try
            {
                var permissions = await repository.Query();
                frm_AddRole1.selectmultiple.Items.Clear();

                foreach (var permission in permissions)
                {
                    // 添加自定义PermissionItem对象（包含ID和名称）
                    frm_AddRole1.selectmultiple.Items.Add(new PermissionItem(permission.PermissionID, permission.PermissionName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载菜单权限失败: {ex.Message}");
                LogService.LogService.Error("frmAddRole类的LoadMenuPermissions方法", ex);
            }
            finally
            {
                repository.Dispose();
            }
        }
        // 保存按钮点击事件
        private async void Frm_AddRole1_SaveButtonClick(object sender, EventArgs e)
        {
            if (!frm_AddRole1.ValidateForm())
            {
                return;
            }

            // 新增：检查角色名称是否已存在
            string roleName = frm_AddRole1.RoleName.Trim();
            if (string.IsNullOrEmpty(roleName))
            {
                MessageBox.Show("角色名称不能为空");
                return;
            }

            var roleRepository = new EntityRepositorys<Role>();
            try
            {
                // 查询是否已存在相同名称的角色（不包括当前编辑的角色）
                List<Role> existingRoles;
                if (_isEditing && _editingRole != null)
                {
                    existingRoles = await roleRepository.Query($"RoleName = @RoleName AND RoleID != @RoleID",
                        new { RoleName = roleName, RoleID = _editingRole.RoleID });
                }
                else
                {
                    existingRoles = await roleRepository.Query($"RoleName = @RoleName",
                        new { RoleName = roleName });
                }

                if (existingRoles.Any())
                {
                    MessageBox.Show($"已存在名为'{roleName}'的角色，请使用不同的角色");
                    return;
                }
                var role = new Role
                {
                    RoleName = roleName,
                    PermissionChar = frm_AddRole1.RolePassWord,
                    IsEnabled = frm_AddRole1.RoleStatus
                };

                int roleId;
                if (_isEditing && _editingRole != null)
                {
                    role.RoleID = _editingRole.RoleID;
                    await roleRepository.Update(role);
                    // 先删除现有的角色权限关联
                    var roleMenuPermissionRepository = new EntityRepositorys<RoleMenuPermission>();
                    await roleMenuPermissionRepository.Delete($"RoleID = @RoleID", new { RoleID = role.RoleID });
                    roleId = role.RoleID;
                    roleMenuPermissionRepository.Dispose();
                }
                else
                {
                    roleId = (int)await roleRepository.Add(role);
                }

                // 处理权限关联
                var selectedValues = frm_AddRole1.selectmultiple.SelectedValue;
                if (selectedValues != null)
                {
                    var roleMenuPermissionRepo = new EntityRepositorys<RoleMenuPermission>();
                    foreach (var item in selectedValues)
                    {
                        if (item is PermissionItem permItem)
                        {
                            var roleMenuPermission = new RoleMenuPermission
                            {
                                RoleID = roleId,
                                PermissionID = permItem.PermissionID
                            };
                            await roleMenuPermissionRepo.Add(roleMenuPermission);
                        }
                    }
                    roleMenuPermissionRepo.Dispose();
                }

                MessageBox.Show($"角色{(_isEditing ? "更新" : "添加")}成功，ID: {roleId}");
                //frm_AddRole1.ClearInputs();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败: {ex.Message}");
                LogService.LogService.Error("frmAddRole类的Frm_AddRole1_SaveButtonClick方法", ex);
            }
            finally
            {
                roleRepository.Dispose();
            }
        }
        // 下拉框选中项改变事件
        private void Dropdown_multi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (frm_AddRole1.selectmultiple.SelectedValue != null)
            {
                var selectedTexts = new List<string>();
                foreach (var item in frm_AddRole1.selectmultiple.SelectedValue)
                {
                    if (item is PermissionItem permItem)
                    {
                        selectedTexts.Add(permItem.PermissionName);
                    }
                }
                //frm_AddRole1.selectmultiple.Text = string.Join(", ", selectedTexts);
            }
        }
        private void Frm_AddRole1_CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}