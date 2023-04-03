using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAuctionSite.Models
{
    public class SQLCon
    {
        
        public static SqlConnection getConnection() {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\Desktop\\OnlineAuctionSite\\OnlineAuctionSite\\OnlineAuctionSite\\Database\\OnlineAuctionSite.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            return con; 
        }     

    }
}