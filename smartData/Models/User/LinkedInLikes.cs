using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Models.User
{
    public class Persons
    {
        public string firstName { get; set; }
        public string headline { get; set; }
        public string id { get; set; }
        public string lastName { get; set; }
        public string pictureUrl { get; set; }
    }

    public class Values
    {
        public Persons person { get; set; }
        public long timestamp { get; set; }
    }

    public class LinkedInLikes
    {
        public int _total { get; set; }
        public List<Values> values { get; set; }
    }
}


