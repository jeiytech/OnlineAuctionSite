using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineAuctionSite.Models;

namespace OnlineAuctionSite.Controllers
{
    public class AdminController : Controller
    {
        request db = new request();
       
        // GET: home
        public ActionResult Index()
        {
            //return RedirectToAction("List");
            return View();
        }
        public JsonResult List => Json(db.ListAll(), JsonRequestBehavior.AllowGet);

        public JsonResult GetbyID(int id)
        {
            var li = db.ListAll().Find(x => x.id.Equals(id));
            return Json(li, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult Delete(int id)
        {
            return Json(db.Delete(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Manage()
        {
            return View();

        }
       
    }
}