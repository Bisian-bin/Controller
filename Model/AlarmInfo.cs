using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AlarmInfo
    {
        public string AlarmCode { get; set; }
        public int TriggerTime { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
    }
}
