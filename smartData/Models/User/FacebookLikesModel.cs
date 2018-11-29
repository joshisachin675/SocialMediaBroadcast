using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Models.User
{
    public class FacebookLikesModel

    {
        public List<Data> data { get; set; }      
    }

    public class Data
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}



