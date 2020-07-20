using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMenu.Models
{
    public class COrderDetail
    {
        public string f訂單編號 { get; set; }
        public int f訂單明細項次 { get; set; }
        public int f食材編號 { get; set; }
        public int f訂購數量 { get; set; }
        public string f食材單位 { get; set; }
        public bool f是否購買 { get; set; }
        public int f食譜編號 { get; set; }
        public string f食材名稱 { get; set; }
    }
}