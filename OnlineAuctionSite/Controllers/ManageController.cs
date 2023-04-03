using OnlineAuctionSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAuctionSite.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        user1 dc = new user1();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(dc.ListAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int id)
        {
            var Employee = dc.ListAll().Find(x => x.id.Equals(id));
            return Json(Employee, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            return Json(dc.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}