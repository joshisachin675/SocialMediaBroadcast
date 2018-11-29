using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Areas.Users.Models.User
{
    public class ImageModel
    {
        public string TextMessage { get; set; }
        public string Title { get; set; }
        public string Heading { get; set; }
        public string Link { get; set; }
        public List<int> Ids { get; set; }
        public List<string> ImageArray { get; set; }
        public int? PostType { get; set; }
        public Nullable<DateTime> ScheduledTime { get; set; }
        public int timeoffset { get; set; }
        public string PostMethod { get; set; }
        public List<List<long>> selectedPageList { get; set; }
        public int? MultipostType { get; set; }
    }

    public class PublishingModel
    {
        public string TextMessage { get; set; }
        public string ImageArray { get; set; }
        public List<string> Time { get; set; }
        public int PostType { get; set; }
        public string Day { get; set; }
        public string SocialMedia { get; set; }
        public int timeoffset { get; set; }
        public string GroupId { get; set; }
        public List<string> SelectedPreferences { get; set; }
        public List<string> SelectedPreferencesName { get; set; }
        public List<string> PageID { get; set; }
        public bool Status { get; set; }
        public int DayID { get; set; }
    }
}