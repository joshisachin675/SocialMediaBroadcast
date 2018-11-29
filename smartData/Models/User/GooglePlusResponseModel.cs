using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Models.User
{
    public class GooglePlusResponseModel
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string objectType { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
        public Name name { get; set; }
        public Images image { get; set; }
        public bool isPlusUser { get; set; }
        public string language { get; set; }
        public AgeRange ageRange { get; set; }
        public int circledByCount { get; set; }
        public bool verified { get; set; }
    }

    public class Images
    {
        public string url { get; set; }
        public bool isDefault { get; set; }
    }

    public class AgeRange
    {
        public int min { get; set; }
    }

    public class Name
    {
        public string familyName { get; set; }
        public string givenName { get; set; }
    }
}