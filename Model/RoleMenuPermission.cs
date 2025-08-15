using Model;
using SqlSugar;

namespace Model
{
    [SugarTable("RoleMenuPermission")] // 指定表名
    public class RoleMenuPermission
    {
        [SugarColumn(IsPrimaryKey = true)] // 复合主键
        public int RoleID { get; set; }

        [SugarColumn(IsPrimaryKey = true)] // 复合主键
        public int PermissionID { get; set; }

        // 导航属性（多对一：角色）
        [Navigate(NavigateType.ManyToOne, nameof(RoleID))]
        public Role Role { get; set; }

        // 导航属性（多对一：权限）
        [Navigate(NavigateType.ManyToOne, nameof(PermissionID))]
        public MenuPermission MenuPermission { get; set; }
    }
}
