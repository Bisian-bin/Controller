using SqlSugar;
using System.Collections.Generic;

namespace Model
{
    [SugarTable("Role")]
    [SugarIndex(
        indexName: "IX_UniqueRoleName",
        fieldName: "RoleName",
        sortType: OrderByType.Asc,
        isUnique: true
    )]
    public class Role
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleID { get; set; }
        [SugarColumn(Length = 50, IsNullable = false)]

        public string RoleName { get; set; }
        [SugarColumn(Length = 10, IsNullable = false)]
        public string PermissionChar { get; set; }
        [SugarColumn(ColumnDataType = "BOOLEAN", DefaultValue = "1")]
        public bool IsEnabled { get; set; } = true;
        public bool Selected { get; set; }

        // 导航属性（一对多：用户关联）
        [Navigate(NavigateType.OneToMany, nameof(User.RoleID))]
        public List<User> Users { get; set; }

        // 导航属性（多对多：权限关联）
        [Navigate(typeof(RoleMenuPermission), nameof(RoleMenuPermission.RoleID),
                nameof(RoleMenuPermission.PermissionID))]
        public List<MenuPermission> Permissions { get; set; }
        [SugarColumn(IsIgnore = true)] // 忽略数据库映射
        public string MenuNames { get; set; } = "";

    }

    public class PermissionItem
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }

        public PermissionItem(int id, string name)
        {
            PermissionID = id;
            PermissionName = name;
        }

        public override string ToString()
        {
            return PermissionName; // 显示时仅返回名称
        }
    }
}
