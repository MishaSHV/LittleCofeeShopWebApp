using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleCofeeShopWebApp.Controllers
{
    public class MakeOrderController : Controller
    {
        // GET: MakeOrder
       /* public ActionResult MakeOrder(int cofeeId)
        {
            return View();
        }
        */
        public PartialViewResult MakeOrder(int cofeeId)
        {
            return PartialView(cofeeId);
        }
    }
}