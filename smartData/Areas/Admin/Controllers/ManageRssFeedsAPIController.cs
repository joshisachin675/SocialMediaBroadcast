using Core.Domain;
using ServiceLayer.Interfaces;
using smartData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace smartData.Areas.Admin.Controllers
{
    [HandleException]
    public class ManageRssFeedsAPIController : ApiController, IManageRssFeedAPIController
    {
        IManageRssFeedService _manageRssFeedservice;

        public ManageRssFeedsAPIController(IManageRssFeedService _ManageRssFeedService)
        {
            _manageRssFeedservice = _ManageRssFeedService;
        }



        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddRssFeed(string FeedName, string FeedUrl, int UserId, Core.Domain.Users user)
        {
            return _manageRssFeedservice.AddRss(FeedName, FeedUrl, UserId, user);
        }

        [HttpPost]
        public bool DeleteRssFeed(int FeedId)
        {
            return _manageRssFeedservice.DeleteRss(FeedId);
        }

        public bool ApproveRssFeed(int FeedId)
        {
            return _manageRssFeedservice.ApproveRssFeed(FeedId);
        }

        public smRssFeeds GetFeedById(int id)
        {
            return _manageRssFeedservice.GetFeedById(id);
        }

        public smRssFeeds GetFeedByUrl(string Url)
        {
            return _manageRssFeedservice.GetFeedByUrl(Url);
        }
        public  smRssFeeds GetFeedByName(string Name)
        {
            return _manageRssFeedservice.GetFeedByName(Name);
        }

        public List<smRssArtical> getIgnoreList(int userId)
        {
            return _manageRssFeedservice.getIgnoreList(userId);
        }


        public int AddRssData(string message, string imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, DateTime CreatedDate, string textdescription, string Subindustryname, string Heading, string OriginalTitle,int feedID)
    {
        return _manageRssFeedservice.AddRssData(message, imageIds, url, userId, categoryId, socialMedia, categoryName, CreatedDate, textdescription, Subindustryname, Heading, OriginalTitle, feedID);
    }
        public int IgnoreRssData (string message ,string url,int userId,DateTime CreatedDate)
        {
            return _manageRssFeedservice.IgnoreRssData(message, url, userId, CreatedDate);
        }


    }
}
