using AppInterfaces.Repository;
using Core.Domain;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
   public class ManageRssFeedService: IManageRssFeedService
    {
       
        IManageRssFeedRepository _manageRssFeedRepository = null;
        #region ctor
        public ManageRssFeedService(IManageRssFeedRepository manageRssFeedRepository)
        {
            _manageRssFeedRepository = manageRssFeedRepository;
        }
        #endregion

        #region public methods
        public bool AddRss(string FeedName, string Feed, int UserId, Users user)
        {
            return _manageRssFeedRepository.AddRss(FeedName, Feed, UserId, user);
        }

        public bool DeleteRss(int Id)
        {
            return _manageRssFeedRepository.DeleteRss(Id);
        }

        public bool ApproveRssFeed(int FeedId)
        {
            return _manageRssFeedRepository.ApproveRssFeed(FeedId);
        }

        public smRssFeeds GetFeedById(int id)
        {
            return _manageRssFeedRepository.GetFeedById(id);
        }
        public List<smRssArtical> getIgnoreList(int userID)
        {
            return _manageRssFeedRepository.getIgnoreList(userID);
       }

        public int AddRssData(string message, string imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, DateTime CreatedDate, string textdescription, string Subindustryname, string Heading, string OriginalTitle, int feedID)
       {
           return _manageRssFeedRepository.AddRssData(message, imageIds, url, userId, categoryId, socialMedia, categoryName, CreatedDate, textdescription, Subindustryname, Heading, OriginalTitle, feedID);
       }
       public int IgnoreRssData (string message ,string url ,int userId,DateTime CreatedDate)
       {
           return _manageRssFeedRepository.IgnoreRssData(message, url, userId, CreatedDate);

       }
       public List<smContentLibrary> getAddedData (int userId){
           return _manageRssFeedRepository.getAddedData(userId);
       }

        public smRssFeeds GetFeedByUrl(string Url)
        {
            return _manageRssFeedRepository.GetFeedByUrl(Url);
        }

        public smRssFeeds GetFeedByName(string Name)
        {
            return _manageRssFeedRepository.GetFeedByName(Name);
        }
        #endregion
    }
}
