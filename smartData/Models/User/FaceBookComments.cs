using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Models.User
{
    public class FaceBookComments
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }

    public class From
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Datum
    {
        public string created_time { get; set; }
        public From from { get; set; }
        public string message { get; set; }
        public string id { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
    }



}


