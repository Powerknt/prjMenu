using prjMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMenu.Controllers
{
    public class OrderMasterController : Controller
    {
        // GET: OrderMaster
        public ActionResult Index()
        {
            return View();
        }

        // 先抓取目前登入會員ID
        public ActionResult OrderMaster()
        {
            var mId = Session["會員ID"];
            if (mId != null)
            {
                COrderMasterDetailList x = (new COrderMasterFactory().queryById((int)mId));
                if (x != null)
                {
                    return View(x);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}