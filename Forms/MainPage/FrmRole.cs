using AntdUI;
using DataAccess;
using Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AntdUI.Table;

namespace Forms
{
    public partial class FrmRole : Form
    {
        private FrmRole _instance;

        public FrmRole Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new FrmRole();
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 权限控制：没有FrmRole权限则禁用表格按钮和状态开关
            bool hasPagePermission = Model.Context.CurrentPermissionNames?.Contains("FrmRole") == true;
            // 禁用角色表格按钮和状态开关
            frm_RoleManage1.RoleTable.Enabled = hasPagePermission;
            // 禁用用户表格按钮和状态开关
            frm_UserManage1.UserTable.Enabled = hasPagePermission;
        }

        public FrmRole()
        {
            InitializeComponent();
            // 订阅角色添加按钮点击事件
            frm_RoleManage1.AddRoleButtonClick += Frm_RoleManage1_AddRoleButtonClick;
            frm_RoleManage1.SearchButtonClick += frm_RoleManage1_SearchButtonClick;
            frm_RoleManage1.RefreshButtonClick += frm_RoleManage1_RefreshButtonClick;
            frm_RoleManage1.BatchEnableButtonClick += frm_RoleManage1_EnableButtonClick;
            frm_RoleManage1.BatchDisableButtonClick += frm_RoleManage1_DisableButtonClick;
            frm_RoleManage1.BatchDeleteButtonClick += frm_RoleManage1_DeleteButtonClick;
            // 订阅用户添加按钮点击事件
            frm_UserManage1.AddUserButtonClick += Frm_UserManage1_AddUserButtonClick;
            frm_UserManage1.SearchButtonClick += Frm_UserManage1_SearchButtonClick;
            frm_UserManage1.RefreshButtonClick += Frm_UserManage1_RefreshButtonClick;
            frm_UserManage1.BatchEnableButtonClick += frm_UserManage1_EnableButtonClick;
            frm_UserManage1.BatchDisableButtonClick += frm_UserManage1_DisableButtonClick;
            frm_UserManage1.BatchDeleteButtonClick += frm_UserManage1_DeleteButtonClick;

            // 添加角色表格初始化
            InitializeRoleTable();
            frm_RoleManage1.RoleTable.CellButtonClick += HandleButtonClick;
            // 添加用户表格初始化
            InitializeUserTable();
            frm_UserManage1.UserTable.CellButtonClick += HandleUserButtonClick;
        }
        // 用户表格初始化
        private async void InitializeUserTable()
        {
            await LoadAndDisplayUserData();
        }
        // 角色表格初始化
        private async void InitializeRoleTable()
        {
            await LoadAndDisplayRoleData();
        }
        // 加载并显示角色数据
        private async Task LoadAndDisplayRoleData(string roleName = null)
        {
            try
            {
                var roles = await LoadRoleDataAsync(roleName);

                // 配置表格列
                ConfigureRoleTableColumns();

                // 绑定数据源
                frm_RoleManage1.RoleTable.DataSource = roles;
                frm_RoleManage1.RoleTable.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载角色数据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogService.LogService.Error("frmManage类的InitializeRoleTable方法", ex);
            }
        }
        // 加载并显示用户数据
        private async Task<List<Role>> LoadRoleDataAsync(string roleName = null)
        {
            try
            {
                using (var roleRepo = new EntityRepositorys<Role>())
                {
                    List<Role> roles;
                    if (string.IsNullOrEmpty(roleName))
                    {
                        roles = await roleRepo.Query();
                    }
                    else
                    {
                        roles = await roleRepo.Query("RoleName LIKE @roleName",
                            new { roleName = $"%{roleName}%" });
                    }

                    if (roles == null || !roles.Any()) return roles;

                    // 先加载所有角色权限关联数据
                    using (var rmpRepo = new EntityRepositorys<RoleMenuPermission>())
                    {
                        var allRolePermissions = await rmpRepo.Query();
                        // 构建角色ID到权限ID列表的映射
                        var rolePermissionsDict = allRolePermissions
                            .GroupBy(rmp => rmp.RoleID)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(rmp => rmp.PermissionID).ToList()
                            );

                        // 加载所有权限数据并构建ID到名称的映射（优化查询效率）
                        using (var permRepo = new EntityRepositorys<MenuPermission>())
                        {
                            var allPermissions = await permRepo.Query();
                            // 构建权限ID到名称的字典，避免重复查询
                            var permissionDict = allPermissions.ToDictionary(p => p.PermissionID, p => p.PermissionName);

                            // 处理每个角色的菜单名称
                            foreach (var role in roles)
                            {
                                if (rolePermissionsDict.TryGetValue(role.RoleID, out var permIds) && permIds != null)
                                {
                                    // 筛选出存在的权限名称（避免因权限删除导致空值）
                                    var menuNames = permIds
                                        .Where(id => permissionDict.ContainsKey(id))
                                        .Select(id => permissionDict[id])
                                        .ToList();

                                    role.MenuNames = menuNames.Any()
                                        ? string.Join(", ", menuNames)
                                        : "无分配权限";
                                }
                                else
                                {
                                    role.MenuNames = "无分配权限";
                                }
                            }
                        }
                    }
                    return roles;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载角色数据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogService.LogService.Error("frmManage类的LoadRoleDataAsync方法", ex);
                return new List<Role>();
            }
        }
        // 配置角色表格列
        private void ConfigureRoleTableColumns()
        {
            var columns = new ColumnCollection();
            var table = frm_RoleManage1.RoleTable;
            table.Columns.Clear();

            table.Columns.Add(new ColumnCheck("Selected")
            {
                Fixed = true,      // 固定列，始终显示在左侧
                Width = "5%"
            });

            // 添加角色字段列
            table.Columns.Add(new Column("RoleID", "角色ID", ColumnAlign.Center) { Width = "5%" });
            table.Columns.Add(new Column("RoleName", "角色名称", ColumnAlign.Center) { Width = "10%" });
            table.Columns.Add(new Column("PermissionChar", "权限字符", ColumnAlign.Center) { Width = "10%" });

            // 添加菜单权限列
            table.Columns.Add(new Column("MenuNames", "菜单权限", ColumnAlign.Center)
            {
                Width = "33%",
                Ellipsis = true, // 文本过长时显示省略号
                LineBreak = true,  // 允许换行显示

            });

            // 添加状态列（显示启用/禁用）
            var statusColumn = new ColumnSwitch("IsEnabled", "状态", ColumnAlign.Center)
            {
                Width = "8%",
                Call = (value, record, i_row, i_col) =>
                {
                    if (record is Role role)
                    {
                        int roleId = role.RoleID;
                        bool newStatus = (bool)value;

                        this.BeginInvoke(new Action(async () =>
                        {
                            try
                            {
                                using (var roleRepo = new EntityRepositorys<Role>())
                                {
                                    var roleToUpdate = await roleRepo.GetById(roleId);
                                    if (roleToUpdate != null)
                                    {
                                        roleToUpdate.IsEnabled = newStatus;
                                        await roleRepo.Update(roleToUpdate);
                                    }
                                }
                                await LoadAndDisplayRoleData();
                                AntdUI.Message.success(this, $"角色状态已更新为{(newStatus ? "启用" : "禁用")}");
                            }
                            catch (Exception ex)
                            {
                                AntdUI.Message.error(this, $"更新角色状态失败：{ex.Message}");
                                LogService.LogService.Error("frmManage类的ConfigureRoleTableColumns方法", ex);
                                await LoadAndDisplayRoleData();
                            }
                        }));
                    }
                    return value;
                }
            };
            table.Columns.Add(statusColumn);

            // 添加操作列（使用Render创建按钮）
            var actionColumn = new Column("Actions", "操作", ColumnAlign.Center)
            {
                Width = "24%",
                Render = (value, row, index) =>
                {
                    if (row is Role role)
                    {
                        // 创建编辑和分配按钮（使用CellButton）
                        var editButton = new CellButton(
                            id: role.RoleID.ToString(), // 用角色ID作为标识
                            text: "编辑",
                            _type: TTypeMini.Primary
                        );

                        var assignButton = new CellButton(
                            id: role.RoleID.ToString(),
                            text: "分配",
                            _type: TTypeMini.Success
                        );
                        var deleteButton = new CellButton(
                            id: role.RoleID.ToString(),
                            text: "删除",
                            _type: TTypeMini.Error
                        );

                        // 将按钮放入CellLink数组（AntdUI.Table支持多个按钮）
                        return new CellLink[] { editButton, assignButton, deleteButton };
                    }
                    return null;
                }
            };
            table.Columns.Add(actionColumn);
        }
        // 处理角色按钮点击事件
        private async void HandleButtonClick(object sender, TableButtonEventArgs e)
        {
            if (frm_RoleManage1.RoleTable.DataSource is List<Role> roles && e.Record is Role role)
            {
                string buttonId = e.Btn.Id;         // 获取按钮ID（RoleID）
                string buttonText = e.Btn.Text;       // 获取按钮文本（编辑/分配/删除）
                int roleId = int.Parse(buttonId);     // 解析角色ID

                if (buttonText == "编辑")
                {
                    using (var editForm = new frmAddRole(role))
                    {
                        editForm.ShowDialog();
                        _ = LoadAndDisplayRoleData(); // 刷新表格
                    }
                }
                else if (buttonText == "分配")
                {
                    using (var assignForm = new frmAssign(role))
                        assignForm.ShowDialog();
                    _ = LoadAndDisplayRoleData();
                }
                else if (buttonText == "删除")
                {
                    var confirmResult = AntdUI.Modal.open(
                        this,
                        "删除警告",
                        $"确定要删除角色 '{role.RoleName}' 吗？",
                        TType.Warn
                    );
                    if (confirmResult == DialogResult.OK)
                    {
                        await DeleteRole(roleId);
                        await LoadAndDisplayRoleData();
                    }
                }
            }
        }
        // 处理用户按钮点击事件
        private async void frm_RoleManage1_SearchButtonClick(object sender, EventArgs e)
        {
            var searchText = frm_RoleManage1.RoleNameSearchInput.Text.Trim();
            try
            {
                // 调用新方法获取包含菜单权限的角色列表
                var roles = await SearchMenuPermissions(searchText);
                // 配置表格列（确保MenuNames列存在）
                ConfigureRoleTableColumns();
                // 绑定数据源
                frm_RoleManage1.RoleTable.DataSource = roles;
                frm_RoleManage1.RoleTable.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogService.LogService.Error("frmManage类的HandleButtonClick方法", ex);
            }
        }
        // 搜索角色
        private async void SearchRoles(string roleName)
        {
            try
            {
                using (var roleRepo = new EntityRepositorys<Role>())
                {
                    List<Role> roles;

                    if (string.IsNullOrEmpty(roleName))
                    {
                        roles = await roleRepo.Query();
                    }
                    else
                    {
                        // 使用参数化查询防止SQL注入
                        roles = await roleRepo.Query("RoleName LIKE @roleName",
                            new { roleName = $"%{roleName}%" });
                    }

                    // 绑定数据源并刷新表格
                    frm_RoleManage1.RoleTable.DataSource = roles;
                    frm_RoleManage1.RoleTable.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogService.LogService.Error("frmManage类的SearchRoles方法", ex);
            }
        }
        // 初始化角色表格
        private async Task<List<Role>> SearchMenuPermissions(string roleName)
        {
            try
            {
                using (var roleRepo = new EntityRepositorys<Role>())
                {
                    // 1. 先查询角色基本信息
                    List<Role> roles;
                    if (string.IsNullOrEmpty(roleName))
                    {
                        roles = await roleRepo.Query();
                    }
                    else
                    {
                        roles = await roleRepo.Query("RoleName LIKE @roleName",
                            new { roleName = $"%{roleName}%" });
                    }

                    if (roles == null || !roles.Any()) return roles;

                    // 2. 查询所有角色权限关联
                    using (var rmpRepo = new EntityRepositorys<RoleMenuPermission>())
                    {
                        var allRolePermissions = await rmpRepo.Query();
                        var rolePermissionsDict = allRolePermissions
                            .GroupBy(rmp => rmp.RoleID)
                            .ToDictionary(g => g.Key, g => g.Select(rmp => rmp.PermissionID).ToList());

                        // 3. 查询所有权限并构建ID到名称的映射
                        using (var permRepo = new EntityRepositorys<MenuPermission>())
                        {
                            var allPermissions = await permRepo.Query();
                            var permissionDict = allPermissions.ToDictionary(p => p.PermissionID, p => p.PermissionName);

                            // 4. 填充每个角色的MenuNames
                            foreach (var role in roles)
                            {
                                if (rolePermissionsDict.TryGetValue(role.RoleID, out var permIds))
                                {
                                    var menuNames = permIds
                                        .Where(id => permissionDict.ContainsKey(id))
                                        .Select(id => permissionDict[id])
                                        .ToList();
                                    role.MenuNames = menuNames.Any() ? string.Join(", ", menuNames) : "无分配权限";
                                }
                                else
                                {
                                    role.MenuNames = "无分配权限";
                                }
                            }
                        }
                    }
                    return roles;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索角色权限失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogService.LogService.Error("frmManage类的SearchMenuPermissions方法", ex);
                return new List<Role>();
            }
        }
        // 刷新角色数据
        private async void frm_RoleManage1_RefreshButtonClick(object sender, EventArgs e)
        {
            // 清空搜索框
            frm_RoleManage1.RoleNameSearchInput.Text = "";

            // 刷新角色数据
            await LoadAndDisplayRoleData();

            // 显示刷新成功消息
            //MessageBox.Show("角色数据已刷新", "刷新成功",
            //    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // 启用角色按钮
        private async void frm_RoleManage1_EnableButtonClick(object sender, EventArgs e)
        {
            // 1. 获取表格数据源
            var roles = frm_RoleManage1.RoleTable.DataSource as List<Role>;
            if (roles == null || !roles.Any(r => r.Selected))
            {
                //AntdUI.Message.warn(this, "请选择要启用的角色！");
                return;
            }
            // 2. 提取选中角色的ID
            var selectedRoleIds = roles.Where(r => r.Selected).Select(r => r.RoleID).ToList();
            try
            {
                using (var roleRepo = new EntityRepositorys<Role>())
                {
                    // 3. 批量更新数据库：设置 IsEnabled = true
                    foreach (var roleId in selectedRoleIds)
                    {
                        var role = await roleRepo.GetById(roleId); // 从数据库获取最新数据
                        if (role != null)
                        {
                            role.IsEnabled = true;
                            await roleRepo.Update(role);       // 持久化更新
                        }
                    }
                }
                // 4. 刷新表格（重新加载数据）
                await LoadAndDisplayRoleData();
                //AntdUI.Message.success(this, "批量启用成功！");
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的frm_RoleManage1_EnableButtonClick方法", ex);
                AntdUI.Message.error(this, $"批量启用失败：{ex.Message}");
            }
        }
        // 禁用角色按钮
        private async void frm_RoleManage1_DisableButtonClick(object sender, EventArgs e)
        {
            var roles = frm_RoleManage1.RoleTable.DataSource as List<Role>;
            if (roles == null || !roles.Any(r => r.Selected))
            {
                //AntdUI.Message.warn(this, "请选择要禁用的角色！");
                return;
            }

            var selectedRoleIds = roles.Where(r => r.Selected).Select(r => r.RoleID).ToList();

            try
            {
                using (var roleRepo = new EntityRepositorys<Role>())
                {
                    foreach (var roleId in selectedRoleIds)
                    {
                        var role = await roleRepo.GetById(roleId);
                        if (role != null)
                        {
                            role.IsEnabled = false;
                            await roleRepo.Update(role);
                        }
                    }
                }

                await LoadAndDisplayRoleData();
                //AntdUI.Message.success(this, "批量禁用成功！");
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的frm_RoleManage1_DisableButtonClick方法", ex);
                AntdUI.Message.error(this, $"批量禁用失败：{ex.Message}");
            }
        }
        // 删除角色按钮
        private async void frm_RoleManage1_DeleteButtonClick(object sender, EventArgs e)
        {
            var roles = frm_RoleManage1.RoleTable.DataSource as List<Role>;
            if (roles == null || !roles.Any(r => r.Selected))
            {
                //AntdUI.Message.warn(this, "请选择要删除的角色！");
                return;
            }

            // 4. 弹出确认框（防止误操作）
            var confirmResult = AntdUI.Modal.open(
                this,
                "确认删除",
                "删除后数据不可恢复，是否继续？",
                TType.Warn
            );
            if (confirmResult != DialogResult.OK) return;

            var selectedRoleIds = roles.Where(r => r.Selected).Select(r => r.RoleID).ToList();

            try
            {
                // 1. 直接创建SqlSugarClient（或从现有仓储获取）
                var db = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = "Data Source=Database.db;Version=3;Pooling=True;Max Pool Size=100;",
                    DbType = SqlSugar.DbType.Sqlite,
                    IsAutoCloseConnection = true
                });

                // 2. 构建批量删除条件（RoleID IN (@RoleIds)）
                var deleteable = db.Deleteable<Role>()
                    .Where($"RoleID IN ({string.Join(",", selectedRoleIds)})"); // 直接拼接ID列表

                // 3. 执行删除
                int affectedRows = await deleteable.ExecuteCommandAsync();

                if (affectedRows > 0)
                {
                    // 4. 刷新表格
                    await LoadAndDisplayRoleData();
                    AntdUI.Message.success(this, $"成功删除 {affectedRows} 条记录！");
                }
                else
                {
                    AntdUI.Message.warn(this, "无匹配记录，删除失败。");
                }
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的frm_RoleManage1_DeleteButtonClick方法", ex);
                AntdUI.Message.error(this, $"批量删除失败：{ex.Message}");
            }
        }
        // 删除角色
        private async Task DeleteRole(int roleId)
        {
            try
            {
                // 1. 直接创建SqlSugarClient（或从现有仓储获取）
                var db = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = "Data Source=Database.db;Version=3;Pooling=True;Max Pool Size=100;",
                    DbType = SqlSugar.DbType.Sqlite,
                    IsAutoCloseConnection = true
                });

                // 2. 构建删除条件（RoleID = @RoleId）
                var roleDeleteable = db.Deleteable<Role>()
                    .Where(r => r.RoleID == roleId);

                // 3. 执行角色删除
                int roleAffectedRows = await roleDeleteable.ExecuteCommandAsync();

                if (roleAffectedRows > 0)
                {
                    // 4. 同时删除角色关联的权限
                    var rmpDeleteable = db.Deleteable<RoleMenuPermission>()
                        .Where(rmp => rmp.RoleID == roleId);

                    int rmpAffectedRows = await rmpDeleteable.ExecuteCommandAsync();

                    AntdUI.Message.success(this, $"成功删除角色及其关联权限！");
                }
                else
                {
                    AntdUI.Message.warn(this, "未找到匹配的角色记录，删除失败。");
                }
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的DeleteRole方法", ex);
                AntdUI.Message.error(this, $"删除角色失败：{ex.Message}");
            }
        }
        // 添加角色按钮
        private void Frm_RoleManage1_AddRoleButtonClick(object sender, EventArgs e)
        {
            // 创建并显示添加角色窗体
            using (var addRoleForm = new frmAddRole())
            {
                addRoleForm.ShowDialog();
                _ = LoadAndDisplayRoleData();
            }
        }
        // 初始化用户表格
        private async Task LoadAndDisplayUserData(string userName = null)
        {
            try
            {
                var users = await LoadUserDataAsync(userName);

                // 配置表格列
                ConfigureUserTableColumns();

                // 绑定数据源
                frm_UserManage1.BindUserData(users);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的LoadAndDisplayUserData方法", ex);
                MessageBox.Show($"加载用户数据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 加载用户数据
        private async Task<List<User>> LoadUserDataAsync(string userName = null)
        {
            try
            {
                using (var userRepo = new EntityRepositorys<User>())
                {
                    List<User> users;
                    if (string.IsNullOrEmpty(userName))
                    {
                        users = await userRepo.Query();
                    }
                    else
                    {
                        users = await userRepo.Query("UserName LIKE @userName",
                            new { userName = $"%{userName}%" });
                    }

                    if (users == null || !users.Any()) return users;

                    // 加载所有角色数据并构建ID到角色的映射
                    using (var roleRepo = new EntityRepositorys<Role>())
                    {
                        var allRoles = await roleRepo.Query();
                        var roleDict = allRoles.ToDictionary(r => r.RoleID, r => r);

                        // 处理每个用户的角色信息
                        foreach (var user in users)
                        {
                            if (roleDict.TryGetValue(user.RoleID, out var role))
                            {
                                user.Role = role;
                            }
                        }
                    }

                    return users;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载用户数据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogService.LogService.Error("frmManage类的LoadUserDataAsync方法", ex);
                return new List<User>();
            }
        }
        // 配置用户表格列
        private void ConfigureUserTableColumns()
        {
            var columns = new ColumnCollection();
            var table = frm_UserManage1.UserTable;
            table.Columns.Clear();

            table.Columns.Add(new ColumnCheck("Selected")
            {
                Fixed = true,      // 固定列，始终显示在左侧
                Width = "5%"
            });

            // 添加用户字段列
            table.Columns.Add(new Column("UserID", "用户ID", ColumnAlign.Center) { Width = "5%" });
            table.Columns.Add(new Column("UserName", "用户名", ColumnAlign.Center) { Width = "10%" });
            table.Columns.Add(new Column("Password", "密码", ColumnAlign.Center) { Width = "10%" });

            // 修改角色列，使用自定义Render函数
            table.Columns.Add(new Column("RoleName", "角色", ColumnAlign.Center)
            {
                Width = "33%",
                Render = (value, row, index) =>
                {
                    if (row is User user && user.Role != null)
                    {
                        return user.Role.RoleName;
                    }
                    return "无角色";
                }
            });

            // 添加状态列（显示启用/禁用）
            var statusColumn = new ColumnSwitch("IsEnabled", "状态", ColumnAlign.Center)
            {
                Width = "8%",
                Call = (value, record, i_row, i_col) =>
                {
                    if (record is User user)
                    {
                        int userId = user.UserID;
                        bool newStatus = (bool)value;

                        this.BeginInvoke(new Action(async () =>
                        {
                            try
                            {
                                using (var userRepo = new EntityRepositorys<User>())
                                {
                                    var userToUpdate = await userRepo.GetById(userId);
                                    if (userToUpdate != null)
                                    {
                                        userToUpdate.IsEnabled = newStatus;
                                        await userRepo.Update(userToUpdate);
                                    }
                                }
                                await LoadAndDisplayUserData();
                                AntdUI.Message.success(this, $"用户状态已更新为{(newStatus ? "启用" : "禁用")}");
                            }
                            catch (Exception ex)
                            {
                                AntdUI.Message.error(this, $"更新用户状态失败：{ex.Message}");
                                LogService.LogService.Error("frmManage类的ConfigureUserTableColumns方法", ex);
                                await LoadAndDisplayUserData();
                            }
                        }));
                    }
                    return value;
                }
            };
            table.Columns.Add(statusColumn);

            // 添加操作列（使用Render创建按钮）
            var actionColumn = new Column("Actions", "操作", ColumnAlign.Center)
            {
                Width = "24%",
                Render = (value, row, index) =>
                {
                    if (row is User user)
                    {
                        // 创建编辑和删除按钮（使用CellButton）
                        var editButton = new CellButton(
                            id: user.UserID.ToString(), // 用用户ID作为标识
                            text: "编辑",
                            _type: TTypeMini.Primary
                        );

                        var deleteButton = new CellButton(
                            id: user.UserID.ToString(),
                            text: "删除",
                            _type: TTypeMini.Error
                        );

                        // 将按钮放入CellLink数组（AntdUI.Table支持多个按钮）
                        return new CellLink[] { editButton, deleteButton };
                    }
                    return null;
                }
            };
            table.Columns.Add(actionColumn);
        }
        // 处理用户表格按钮点击事件
        private async void HandleUserButtonClick(object sender, TableButtonEventArgs e)
        {
            if (frm_UserManage1.UserTable.DataSource is List<User> users && e.Record is User user)
            {
                string buttonId = e.Btn.Id;         // 获取按钮ID（UserID）
                string buttonText = e.Btn.Text;       // 获取按钮文本（编辑/删除）
                int userId = int.Parse(buttonId);     // 解析用户ID

                if (buttonText == "编辑")
                {
                    using (var editForm = new frmAddUser(user))
                    {
                        editForm.ShowDialog();
                        await LoadAndDisplayUserData(); // 刷新表格
                    }
                }
                else if (buttonText == "删除")
                {
                    var confirmResult = AntdUI.Modal.open(
                        this,
                        "删除警告",
                        $"确定要删除用户 '{user.UserName}' 吗？",
                        TType.Warn
                    );
                    if (confirmResult == DialogResult.OK)
                    {
                        await DeleteUser(userId);
                        await LoadAndDisplayUserData();
                    }
                }
            }
        }
        // 搜索用户
        private async void Frm_UserManage1_SearchButtonClick(object sender, EventArgs e)
        {
            try
            {

                var searchText = frm_UserManage1.UserNameSearchInput.Text.Trim();
                await LoadAndDisplayUserData(searchText);
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的Frm_UserManage1_SearchButtonClick", ex);
                MessageBox.Show($"搜索失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 添加用户按钮
        private void Frm_UserManage1_AddUserButtonClick(object sender, EventArgs e)
        {
            // 创建并显示添加用户窗体
            using (var addUserForm = new frmAddUser())
            {
                addUserForm.ShowDialog();
                _ = LoadAndDisplayUserData();
            }
        }
        // 启用用户按钮
        private async void frm_UserManage1_EnableButtonClick(object sender, EventArgs e)
        {
            // 1. 获取表格数据源
            var users = frm_UserManage1.UserTable.DataSource as List<User>;
            if (users == null || !users.Any(u => u.Selected))
            {
                //AntdUI.Message.warn(this, "请选择要启用的用户！");
                return;
            }
            // 2. 提取选中用户的ID
            var selectedUserIds = users.Where(u => u.Selected).Select(u => u.UserID).ToList();
            try
            {
                using (var userRepo = new EntityRepositorys<User>())
                {
                    // 3. 批量更新数据库：设置 IsEnabled = true
                    foreach (var userId in selectedUserIds)
                    {
                        var user = await userRepo.GetById(userId); // 从数据库获取最新数据
                        if (user != null)
                        {
                            user.IsEnabled = true;
                            await userRepo.Update(user);       // 持久化更新
                        }
                    }
                }
                // 4. 刷新表格（重新加载数据）
                await LoadAndDisplayUserData();
                //AntdUI.Message.success(this, "批量启用成功！");
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的frm_UserManage1_EnableButtonClick", ex);
                AntdUI.Message.error(this, $"批量启用失败：{ex.Message}");
            }
        }
        // 禁用用户按钮
        private async void frm_UserManage1_DisableButtonClick(object sender, EventArgs e)
        {
            var users = frm_UserManage1.UserTable.DataSource as List<User>;
            if (users == null || !users.Any(u => u.Selected))
            {
                //AntdUI.Message.warn(this, "请选择要禁用的用户！");
                return;
            }

            var selectedUserIds = users.Where(u => u.Selected).Select(u => u.UserID).ToList();

            try
            {
                using (var userRepo = new EntityRepositorys<User>())
                {
                    foreach (var userId in selectedUserIds)
                    {
                        var user = await userRepo.GetById(userId);
                        if (user != null)
                        {
                            user.IsEnabled = false;
                            await userRepo.Update(user);
                        }
                    }
                }

                await LoadAndDisplayUserData();
                //AntdUI.Message.success(this, "批量禁用成功！");
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的frm_UserManage1_DisableButtonClick", ex);
                AntdUI.Message.error(this, $"批量禁用失败：{ex.Message}");
            }
        }
        // 删除用户按钮
        private async void frm_UserManage1_DeleteButtonClick(object sender, EventArgs e)
        {
            var users = frm_UserManage1.UserTable.DataSource as List<User>;
            if (users == null || !users.Any(u => u.Selected))
            {
                //AntdUI.Message.warn(this, "请选择要删除的用户！");
                return;
            }

            // 4. 弹出确认框（防止误操作）
            var confirmResult = AntdUI.Modal.open(
                this,
                "确认删除",
                "删除后数据不可恢复，是否继续？",
                TType.Warn
            );
            if (confirmResult != DialogResult.OK) return;

            var selectedUserIds = users.Where(u => u.Selected).Select(u => u.UserID).ToList();

            try
            {
                // 1. 直接创建SqlSugarClient（或从现有仓储获取）
                var db = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = "Data Source=Database.db;Version=3;Pooling=True;Max Pool Size=100;",
                    DbType = SqlSugar.DbType.Sqlite,
                    IsAutoCloseConnection = true
                });

                // 2. 构建批量删除条件（UserID IN (@UserIds)）
                var deleteable = db.Deleteable<User>()
                    .Where($"UserID IN ({string.Join(",", selectedUserIds)})"); // 直接拼接ID列表

                // 3. 执行删除
                int affectedRows = await deleteable.ExecuteCommandAsync();

                if (affectedRows > 0)
                {
                    // 4. 刷新表格
                    await LoadAndDisplayUserData();
                    AntdUI.Message.success(this, $"成功删除 {affectedRows} 条记录！");
                }
                else
                {
                    AntdUI.Message.warn(this, "无匹配记录，删除失败。");
                }
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的frm_UserManage1_DeleteButtonClick", ex);
                AntdUI.Message.error(this, $"批量删除失败：{ex.Message}");
            }
        }
        // 删除用户
        private async Task DeleteUser(int userId)
        {
            try
            {
                using (var userRepo = new EntityRepositorys<User>())
                {
                    int affectedRows = await userRepo.Delete("UserID = @UserID", new { UserID = userId });

                    if (affectedRows > 0)
                    {
                        AntdUI.Message.success(this, $"成功删除用户！");
                    }
                    else
                    {
                        AntdUI.Message.warn(this, "未找到匹配的用户记录，删除失败。");
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.LogService.Error("frmManage类的DeleteUser", ex);
                AntdUI.Message.error(this, $"删除用户失败：{ex.Message}");
            }
        }
        // 刷新用户数据
        private async void Frm_UserManage1_RefreshButtonClick(object sender, EventArgs e)
        {
            // 清空搜索框
            frm_UserManage1.UserNameSearchInput.Text = "";

            // 刷新用户数据
            await LoadAndDisplayUserData();
        }

    }
}
