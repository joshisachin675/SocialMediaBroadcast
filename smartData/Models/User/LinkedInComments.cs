using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Models.User
{
    public class Value2
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Headers
    {
        public int _total { get; set; }
        public List<Value2> values { get; set; }
    }

    public class ApiStandardProfileRequest
    {
        public Headers headers { get; set; }
        public string url { get; set; }
    }

    public class SiteStandardProfileRequest
    {
        public string url { get; set; }
    }

    public class Person
    {
        public ApiStandardProfileRequest apiStandardProfileRequest { get; set; }
        public string firstName { get; set; }
        public string headline { get; set; }
        public string id { get; set; }
        public string lastName { get; set; }
        public string pictureUrl { get; set; }
        public SiteStandardProfileRequest siteStandardProfileRequest { get; set; }
    }

    public class Value
    {
        public string comment { get; set; }
        public long id { get; set; }
        public Person person { get; set; }
        public int sequenceNumber { get; set; }
        public long timestamp { get; set; }
    }

    public class LinkedInComments
    {
        public int _total { get; set; }
        public List<Value> values { get; set; }
    }
}

