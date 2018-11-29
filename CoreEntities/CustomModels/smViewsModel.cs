using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Domain
{
    public partial class smViewsModel
    {
        public int ViewId { get; set; }
        public int UserId { get; set; }
        public string UniquePostId { get; set; }
        public int Postid { get; set; }
        public DateTime DateEntered { get; set; }     
        public string SocialMedia { get; set; }
    }
}