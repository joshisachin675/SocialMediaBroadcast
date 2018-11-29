using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Models
{
    public class FeedModel
    {
        public int FeedType { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
        public List<FeedModelItem> Item { get; set; }
    }


    public class FeedModelItem
    {

        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string ContentEncoded { get; set; }
        public string Image { get; set; }
        public string PubDate { get; set; }
        public string Author { get; set; }
        public string Creator { get; set; }
        public bool isAdded { get; set; }
    }

    public class FeedChannelModel
    {
        public int FeedType { get; set; }
        public string Link { get; set; }
        public string ChannelTitle { get; set; }
        public string ChannelDescription { get; set; }
        public string LogoLink { get; set; }
        public string ChannelUrl { get; set; }
        public int Followers { get; set; }
        public int Articles { get; set; }
        public bool IsFollow { get; set; }
        public string RecentArticleTitle { get; set; }
        public string PubDate { get; set; }
        public FeedModelItem RecentArticle { get; set; }
        public List<FeedModelItem> Item { get; set; }
    }

    public class FeedVideoModel
    {
        public int VideoType { get; set; }
        public string ConcernedLink { get; set; }
        public string VideoUrl { get; set; }
    }
}