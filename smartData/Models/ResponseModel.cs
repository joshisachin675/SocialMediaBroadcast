using smartData.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Models
{
    public class ResponseModels
    {
        public int LikesCount { get; set; }
        public int commentCount { get; set; }
        public List<string> LikesName { get; set; }
        public List<string> Comments { get; set; }
        public List<string> CommentsName { get; set; }
        public FaceBookComments fbcomment { get; set; }
        public LinkedInComments linkedincomment { get; set; }
        public LinkedInLikes linkedInlikes { get; set; }
    }
}