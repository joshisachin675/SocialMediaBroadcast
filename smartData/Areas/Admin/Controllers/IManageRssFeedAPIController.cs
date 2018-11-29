using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartData.Areas.Admin.Controllers
{
   public interface IManageRssFeedAPIController
    {
       int AddRssData(string message, string imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, DateTime CreatedDate, string textdescription, string Subindustryname, string Heading, string OriginalTitle, int feedID);
       int IgnoreRssData(string message, string url, int userId, DateTime CreatedDate);
       bool AddRssFeed(string FeedName, string FeedUrl, int UserId, Core.Domain.Users user);
       bool DeleteRssFeed( int FeedId);
       bool ApproveRssFeed(int FeedId);
       smRssFeeds GetFeedById(int id);
       smRssFeeds GetFeedByUrl(string Url);
       smRssFeeds GetFeedByName(string Name);
       List<smRssArtical> getIgnoreList(int userId);
    }
}
