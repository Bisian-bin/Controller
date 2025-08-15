using Model;
using SqlSugar;
using System;

namespace Model
{
    [SugarTable("User")] // 指定表名
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键且自增
        public int UserID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        [SugarColumn(ColumnDataType = "BOOLEAN", DefaultValue = "1")] // 明确指定类型和默认值
        public bool IsEnabled { get; set; } = true;
        public bool Selected { get; set; }
        public int RoleID { get; set; }

        [SugarColumn(IsIgnore = true)] // 忽略数据库映射
        public Role Role { get; set; }
    }
}
