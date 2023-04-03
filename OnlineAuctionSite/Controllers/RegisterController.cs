
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
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {

            return View();
        }

        // POST : Register
        [HttpPost]
        public ActionResult Index(string registerRequest, string firstname,string lastname,string emailaddress,string password, string address,string interest)
        {

            SqlConnection con = SQLCon.getConnection();

            SqlCommand q0 = con.CreateCommand();
            q0.CommandType = CommandType.Text;
            q0.CommandText = "SELECT TOP 1 * FROM [User] WHERE email='"+emailaddress+"';";
            SqlDataReader result0 = q0.ExecuteReader();
            if (result0.HasRows) {
                result0.Close();
                return RedirectToAction("EmailExists", "Register");
            }
            else
            {
                result0.Close();
                SqlCommand q1 = con.CreateCommand();
                q1.CommandType = CommandType.Text;
                String UserType = "seller";
                q1.CommandText = "INSERT INTO [User](email,password,firstname,lastname,address,interest, UserType) VALUES('" + emailaddress + "','" + password + "','" + firstname + "','" + lastname + "','" + address + "','"+interest+ "', '"+UserType+"');";

                if (q1.ExecuteNonQuery()>0) {
                    return RedirectToAction("Success", "Register");
                }
                
            }





            return View();
        }

        // GET: Register/Success
        public ActionResult Success()
        {
            return View();
        }


        // GET: Register/EmailExists
        public ActionResult EmailExists()
        {
            return View();
        }


    }
}