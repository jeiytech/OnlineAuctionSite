using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using OnlineAuctionSite.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAuctionSite.Models
{
    public class user1
    {
        //declare connection string  

        public List<User> ListAll()
        {
            List<User> lst = new List<User>();
            using (SqlConnection con = SQLCon.getConnection())
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand com = new SqlCommand("Select * from [User]", con);
                com.CommandType = CommandType.Text;
                SqlDataReader c = com.ExecuteReader();
                
                while (c.Read())
                {
                    lst.Add(new User
                    {
                        id = Convert.ToInt32(c["id"]),
                       
                        firstName = c["firstName"].ToString(),
                        lastName = c["lastName"].ToString(),
                        email = c["email"].ToString(),
                        address = c["address"].ToString(),

                    });
                    
                }
                con.Close();
                return lst;
            }
        }
        public int Delete(int id)
        {
            int i;
            using (SqlConnection con = SQLCon.getConnection())
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

    }
}