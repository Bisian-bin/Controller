using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public struct MenuEntity
    {
        public string MainID;
        public string ID;
        public string[] Text;
        public int Access;
        public string ImageName;
        public Bitmap[] backImage;
        public bool Visible;
        public string Page;
    };
    public class MenuList
    {
        public List<MenuItem> MenuInfo { get; set; }
        public List<SubMenuItem> SubMenu { get; set; }
    }

    public class MenuItem
    {
        public string Index { get; set; }
        public string No { get; set; }
        public string ID { get; set; }
        public string Simplified { get; set; }
        public string English { get; set; }
        public string IMG { get; set; }
        public bool Visible { get; set; }
        public string Page { get; set; }
    }

    public class SubMenuItem
    {
        public string MainID { get; set; }
        public string SubIndex { get; set; }
        public string SubNo { get; set; }
        public string ID { get; set; }
        public string Simplified { get; set; }
        public string English { get; set; }
        public bool Visible { get; set; }
        public string Page { get; set; }
    }
}
