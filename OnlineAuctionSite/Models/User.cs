using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineAuctionSite.Models
{
    [Serializable]
    public class User
    {

        public int id { get; set; }
      
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string UserType { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public string interest { get; set; }
        public string bank { get; set; }
        public string password { get; set; }
    }



}