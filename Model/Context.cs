using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class Context
    {
        public static User CurrentUser { get; set; }
        public static Role CurrentRole { get; set; }
        public static List<string> CurrentPermissionNames { get; set; }
    }
}
