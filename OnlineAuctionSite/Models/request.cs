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
    public class request
    {
        //declare connection string  

        public List<request2> ListAll()
        {
            List<request2> lst = new List<request2>();
            using (SqlConnection con = SQLCon.getConnection())
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand com = new SqlCommand("Select * from [Request]", con);
                com.CommandType = CommandType.Text;
                SqlDataReader c = com.ExecuteReader();

                while (c.Read())
                {
                    lst.Add(new request2
                    {
                        id = Convert.ToInt32(c["id"]),
                        userid = c["userid"].ToString(),
                        date = Convert.ToDateTime(c["date"]),

                    });

                }
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