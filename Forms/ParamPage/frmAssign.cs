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
    public partial class frmAssign : Form
    {
        private Role _assignedRole;
        public frmAssign()
        {
            InitializeComponent();

        }
        public frmAssign(Role role)
        {
            InitializeComponent();
            this.Text = "分配用户";
            _assignedRole = role;
            frm_Assign1.SaveButtonClick += Frm_Assign1_SaveButtonClickAsync;
            frm_Assign1.CloseButtonClick += Frm_Assign1_CloseButtonClick;
            frm_Assign1.RoleName = role.RoleName;
            LoadUsersToDropdown().ConfigureAwait(false);
            frm_Assign1.selectmultiple.SelectedValueChanged += Dropdown_multi_SelectedIndexChanged;
        }
        //加载用户
        public class UserItem
        {
            public int UserID { get; set; }
            public string UserName { get; set; }

            public UserItem(int id, string name)
            {
                UserID = id;
                UserName = name;
            }

            public override string ToString()
            {
                return UserName;
            }
        }
        private async Task LoadUsersToDropdown()
        {
            using (var userRepo = new EntityRepositorys<User>())
            {
                // 查询所有用户
                var users = await userRepo.Query();

                // 清空下拉框
                frm_Assign1.selectmultiple.Items.Clear();

                // 将用户添加到下拉框（使用UserItem存储ID和名称）
                foreach (var user in users)
                {
                    // 排除已经是当前角色的用户
                    if (user.RoleID != _assignedRole.RoleID)
                    {
                        frm_Assign1.selectmultiple.Items.Add(
                            new UserItem(user.UserID, user.UserName)
                        );
                    }
                }
            }
        }
        private void Dropdown_multi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (frm_Assign1.selectmultiple.SelectedValue != null)
            {
                var selectedTexts = new List<string>();
                foreach (var item in frm_Assign1.selectmultiple.SelectedValue)
                {
                    if (item is UserItem userItem)
                    {
                        selectedTexts.Add(userItem.UserName);
                    }
                }
                // 可以在此处更新UI显示，例如显示选中的用户名列表
                // frm_Assign1.selectmultiple.Text = string.Join(", ", selectedTexts);
            }
        }
        //保存
        private async void Frm_Assign1_SaveButtonClickAsync(object sender, EventArgs e)
        {
            try
            {
                // 验证：至少选择了一个用户
                if (frm_Assign1.selectmultiple.SelectedValue == null ||
                    frm_Assign1.selectmultiple.SelectedValue.Length == 0)
                {
                    MessageBox.Show("请至少选择一个用户", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var userRepo = new EntityRepositorys<User>())
                {
                    int count = 0;
                    foreach (var item in frm_Assign1.selectmultiple.SelectedValue)
                    {
                        if (item is UserItem userItem)
                        {
                            // 根据ID查询用户
                            var user = await userRepo.GetById(userItem.UserID);
                            if (user == null) continue;

                            // 更新用户的角色ID
                            user.RoleID = _assignedRole.RoleID;
                            int result = await userRepo.Update(user, "UserID = @UserID",
                                new { user.UserID });

                            if (result > 0) count++;
                        }
                    }

                    if (count > 0)
                    {
                        MessageBox.Show($"已成功将 {count} 个用户分配到角色 '{_assignedRole.RoleName}'",
                            "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("没有用户被更新", "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogService.LogService.Error("frmAssign类的Frm_Assign1_SaveButtonClickAsync", ex);
            }
        }
        private void Frm_Assign1_CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
