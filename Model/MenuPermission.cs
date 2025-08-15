using Model;
using SqlSugar;
using System.Collections.Generic;

namespace Model
{
    [SugarTable("MenuPermission")]
    public class MenuPermission
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int PermissionID { get; set; }
        [SugarColumn(Length = 100, IsNullable = false)]
        public string PermissionName { get; set; }
        [SugarColumn(Length = 100)]
        public string UIName { get; set; }

        [Navigate(typeof(RoleMenuPermission), nameof(RoleMenuPermission.PermissionID),
                nameof(RoleMenuPermission.RoleID))]
        public List<Role> Roles { get; set; }
        public override string ToString()
        {
            return UIName; // 确保控件显示名称
        }
    }
}
