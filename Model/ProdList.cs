using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProdList
    {
        public string LotNumber { get; set; }
        public int PartNumber { get; set; }
        public string WorkOrder { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int RunTime { get; set; }
        public int StopTime { get; set; }
        public int AlarmTime { get; set; }
        public float JamRate { get; set; }
        public int UPH { get; set; }
        public int InQty { get; set; }
        public int OutQty { get; set; }
        public float Yield { get; set; }
        public string FailQty { get; set; }
        public string User { get; set; }
    }
}
