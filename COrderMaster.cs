using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMenu.Models
{
    public class COrderMaster
    {
        public string f訂單編號 { get; set; }
        public string f訂購日期 { get; set; }
        public int f會員編號 { get; set; }
        public int f訂單總價 { get; set; }
        public bool f訂單狀態 { get; set; }
    }
}