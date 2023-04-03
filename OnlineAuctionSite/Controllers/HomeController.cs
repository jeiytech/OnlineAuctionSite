using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnlineAuctionSite.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAuctionSite.Models
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            SqlConnection con = SQLCon.getConnection();
            SqlCommand q = con.CreateCommand();

            /* categories */
            q.CommandType = CommandType.Text;
            q.CommandText = "SELECT * FROM [Category]";
            SqlDataReader result = q.ExecuteReader();
            List<Category> categories = new List<Category>();
            while (result.Read())
            {
                categories.Add(new Category
                {
                    id = Int32.Parse(result["id"].ToString()),
                    name = result["name"].ToString(),
                    item_count = Int32.Parse(result["item_count"].ToString())
                });
            }
            ViewBag.categories = categories;
            result.Close();
            /* latest items */

            q.CommandType = CommandType.Text;
            q.CommandText = "SELECT TOP 6 [Category].name AS category_name, [Category].item_count AS category_item_count,[Item].* FROM [Item] LEFT JOIN [Category] ON [Item].category_id=[Category].id WHERE [Item].sold=0 ORDER BY [Item].id DESC";
            result = q.ExecuteReader();
            List<Item> items = new List<Item>();
            while (result.Read())
            {
                items.Add(new Item
                {
                    id = Int32.Parse(result["id"].ToString()),
                    category = new Category
                    {
                        id = Int32.Parse(result["category_id"].ToString()),
                        item_count = Int32.Parse(result["category_item_count"].ToString()),
                        name = result["category_name"].ToString()

                    },
                    userId = Int32.Parse(result["user_id"].ToString()),
                    price = Double.Parse(result["price"].ToString()),
                    name = result["name"].ToString(),
                    imageFile = result["imagefile"].ToString(),
                    description = result["description"].ToString(),
                    dates = DateTime.Parse(result["dates"].ToString())

                });
            }
            ViewBag.items = items;

            return View();
        }


        [HttpPost]
        //public ActionResult Index(int amount)
        //{
        //  ViewBag.amount = amount;
        // return View();
        //}
        public ActionResult Index(string Request)
        {
            SqlConnection con = SQLCon.getConnection();
            User luser = (User)Session["loggedUser"];
            SqlCommand q = con.CreateCommand();
            q.CommandType = CommandType.Text;
            q.CommandText = "INSERT INTO [Request](userid,date) VALUES('" + luser.email + "', '" + DateTime.Now.ToString() + "' ); ";
            if (q.ExecuteNonQuery() > 0)
                {
                return RedirectToAction("Success", "Request");
                }
            return View();
            }

    }
}