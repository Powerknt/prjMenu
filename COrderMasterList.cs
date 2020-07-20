using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMenu.Models
{
    // 先根據會員ID，找出已下單的所有訂單編號
    public class COrderMasterId
    {
        public string f訂單編號 { get; set; }
    }

    // 根據每筆訂單編號，找出對應的訂單明細內容
    public class COrderMasterDetail
    {
        public List<COrderDetail> list訂單明細 { get; set; }

        public string f訂購日期 { get; set; }
        public string f訂單編號 { get; set; }
        public int f訂單總價 { get; set; }
        public bool f訂單狀態 { get; set; }
    }

    // 將所有上面查到的訂單彙總到List裡面
    public class COrderMasterDetailList
    {
        public List<COrderMasterDetail> list用戶訂單 { get; set; }
    }
}