using OnlineAuctionSite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAuctionSite.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Success()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string userRequest, string message)
        {

            SqlConnection con = SQLCon.getConnection();
            SqlCommand q = con.CreateCommand();
            q.CommandType = CommandType.Text;
            int user = 1;
            q.CommandText = "INSERT INTO [Message](user_id,message_type,added_time) VALUES('" + user+ "','" + message + "', '" + DateTime.Now.ToString() +"' ); ";

            if (q.ExecuteNonQuery() > 0)
            {
                return RedirectToAction("Success", "Request");
            }
            return View();
        }
    }
}