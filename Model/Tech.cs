using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TechData
    {
        public string AxisName { get; set; }
        public List<PosDefine> PosDif { get; set; } = new List<PosDefine>();
    }

    public class PosDefine
    {
        public string PonitLocation { get; set; }
        public string Coordinates { get; set; }
    }
}
