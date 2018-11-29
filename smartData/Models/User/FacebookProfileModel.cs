using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Areas.Users.Models.User
{
    public class FacebookProfileModel
    {
        public string link { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public Picture picture { get; set; }
        public AgeRange age_range { get; set; }
        public long Id { get; set; }
          public Accounts accounts { get; set; } ///SRohit
    }
    public class Accounts
    {
        public List<data> data { get; set; }
       
    }

    public class data
    {
        public string access_token { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public long id { get; set; }
    }
    

    public class Data
    {
        public bool is_silhouette { get; set; }
        public string url { get; set; }
    }

    public class Picture
    {
        public Data data { get; set; }
    }

    public class AgeRange
    {
        public int min { get; set; }
    }

}