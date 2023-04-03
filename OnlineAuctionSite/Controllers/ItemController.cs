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
    public class ItemController : Controller
    {
        [HttpPost]
        public ActionResult PlaceBid(int item_id, double bid_amount)
        {

            // Get the current highest bid
            double current_highest_bid = GetCurrentHighestBid(item_id);

            // Check if the bid amount is greater than the current highest bid
            if (bid_amount > current_highest_bid)
            {
                SqlConnection con = SQLCon.getConnection();
                SqlCommand q = con.CreateCommand();
                User user = (User)Session["loggedUser"];
                int user_id = user.id;
                // Insert the bid into the database
                q.CommandType = CommandType.Text;
                q.CommandText = "INSERT INTO Bid (item_id, user_id, bid_amount, bid_time) VALUES (@item_id, @user_id, @bid_amount, @bid_time)";
                q.Parameters.AddWithValue("@item_id", item_id);
                q.Parameters.AddWithValue("@user_id", user_id);
                q.Parameters.AddWithValue("@bid_amount", bid_amount);
                q.Parameters.AddWithValue("@bid_time", DateTime.Now);
                q.ExecuteNonQuery();
                // Pass a success message to the TempData
                TempData["message"] = "Your bid has been placed successfully!";
            }
            else if (bid_amount == current_highest_bid)
            {
                // Pass an error message to the view
                TempData["error"] = "Sorry, your bid amount must be greater than the current highest bid.";
            }
            else
            {
                // Pass an error message to the TempData
                TempData["error"] = "Sorry, your bid amount must be greater than the current highest bid.";
            }

            return RedirectToAction("Index", new { item_id });
        }
        private double GetCurrentHighestBid(int item_id)
        {
            double current_highest_bid = 0;
            using (SqlConnection con = SQLCon.getConnection())
            {
                SqlCommand q = con.CreateCommand();
                q.CommandType = CommandType.Text;
                q.CommandText = "SELECT MAX(bid_amount) FROM Bid WHERE item_id = @item_id";
                q.Parameters.AddWithValue("@item_id", item_id);
                object result = q.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    current_highest_bid = Convert.ToDouble(result);
                }
            }
            return current_highest_bid;
        }


        // GET: Item
        public ActionResult Index(int item_id, double? bid_amount)
        {


            SqlConnection con = SQLCon.getConnection();
            SqlCommand q = con.CreateCommand();

            q.CommandType = CommandType.Text;
            q.CommandText = "SELECT price FROM Item WHERE id = " + item_id;
            bid_amount = q.ExecuteScalar() != DBNull.Value ? Convert.ToDouble(q.ExecuteScalar()) : 0;
            if (Session["loggedUser"] != null)
            {
                User user = (User)Session["loggedUser"];
                int user_id = user.id;
                // Retrieve the current highest bid

                // Insert the bid into the database
                q.CommandType = CommandType.Text;
                q.CommandText = "INSERT INTO Bid (item_id, user_id, bid_amount, bid_time) VALUES (@item_id, @user_id, @bid_amount, @bid_time)";
                q.Parameters.AddWithValue("@item_id", item_id);
                q.Parameters.AddWithValue("@user_id", user_id);
                q.Parameters.AddWithValue("@bid_amount", bid_amount);
                q.Parameters.AddWithValue("@bid_time", DateTime.Now);
                q.ExecuteNonQuery();


                // Retrieve the current highest bid
                q.CommandText = "SELECT TOP 1 bid_amount FROM Bid WHERE item_id = @item_id ORDER BY bid_amount DESC";
                q.Parameters.Clear();
                q.Parameters.AddWithValue("@item_id", item_id);
                double current_highest_bid = Convert.ToDouble(q.ExecuteScalar());

                // Pass the current highest bid as a ViewBag variable to the CSHTML file
                ViewBag.current_highest_bid = current_highest_bid;


                q.CommandText = "SELECT TOP 1 [User].id AS seller_id, [User].firstname,[User].lastname, [User].email,[User].address,[User].bank,[Category].name AS category_name, [Category].item_count AS category_item_count,[Item].* FROM [Item] LEFT JOIN [Category] ON [Item].category_id=[Category].id LEFT JOIN [User] ON [Item].user_id=[User].id WHERE [Item].id='" + item_id + "'";
                SqlDataReader result = q.ExecuteReader();
                result.Read();
                Item item = new Item
                {
                    id = Int32.Parse(result["id"].ToString()),
                    category = new Category
                    {
                        id = Int32.Parse(result["category_id"].ToString()),
                        item_count = Int32.Parse(result["category_item_count"].ToString()),
                        name = result["category_name"].ToString()

                    },
                    userId = Int32.Parse(result["user_id"].ToString()),
                    isSold = Int32.Parse(result["sold"].ToString()) == 1,
                    price = Double.Parse(result["price"].ToString()),
                    name = result["name"].ToString(),
                    imageFile = result["imagefile"].ToString(),
                    description = result["description"].ToString(),
                    user = new User
                    {
                        id = Int32.Parse(result["seller_id"].ToString()),
                        email = result["email"].ToString(),
                        firstName = result["firstname"].ToString(),
                        lastName = result["lastname"].ToString(),
                        address = result["address"].ToString(),
                        bank = result["bank"].ToString()
                    }
                };
                ViewBag.item = item;
                result.Close();

                /* get if item is sold */
                if (item.isSold)
                {
                    q.CommandText = "SELECT TOP 1 [User].*,[Order].id AS oid,[Order].ordered_time AS ordered_time, [Order].payment_code AS payment_code FROM [Order] LEFT JOIN [User] ON [Order].user_id=[User].id WHERE [Order].item_id='" + item.id + "'";
                    result = q.ExecuteReader();
                    if (result.HasRows)
                    {
                        result.Read();
                        Order order = new Order
                        {
                            id = Int32.Parse(result["oid"].ToString()),
                            orderedTime = DateTime.Parse(result["ordered_time"].ToString()),
                            paymentCode = result["payment_code"].ToString(),
                            item = item,
                            user = new User
                            {
                                id = Int32.Parse(result["id"].ToString()),
                                email = result["email"].ToString(),
                                firstName = result["firstname"].ToString(),
                                lastName = result["lastname"].ToString(),
                                address = result["address"].ToString(),
                                bank = result["bank"].ToString()
                            }


                        };

                        ViewBag.order = order;

                    }

                }
                ViewBag.message = null;


                return View("Index", new { item_id, bid_amount, user_id });
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}