using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInterfaces.Repository
{
   public interface IManageRssFeedRepository
    {
       bool AddRss(string FeedName, string Feed, int UserId, Core.Domain.Users user);
       int AddRssData(string message, string imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, DateTime CreatedDate, string textdescription, string Subindustryname, string Heading, string OriginalTitle, int feedID);
       int IgnoreRssData(string message, string url, int userId, DateTime CreatedDate);
       bool DeleteRss(int FeedId);
       bool ApproveRssFeed(int FeedId);
       smRssFeeds GetFeedById(int id);
       List<smRssArtical> getIgnoreList(int userId);
       List<smContentLibrary> getAddedData(int userId);

        smRssFeeds GetFeedByUrl(string Url);
        smRssFeeds GetFeedByName(string Name);


    }
}
