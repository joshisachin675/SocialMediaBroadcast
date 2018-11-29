using Core.Domain;
using ServiceLayer.Interfaces;
using smartData.Areas.Users.Models.User;
using smartData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace smartData.Areas.Users.ApiControllers
{
    [HandleException]
    public class PostAPIController : ApiController, IPostAPIController
    {
        IPostService _postService;

        #region constructor
        public PostAPIController(IPostService _PostService)
        {
            _postService = _PostService;
            //System.Net.Http.Headers.contContentType = new MediaTypeHeaderValue("application/json");
        }
        #endregion

        #region public methods
        public List<smSocialMediaProfile> GetSocialMediaAccounts(int userId, List<int> Ids)
        {
            return _postService.GetSocialMediaAccounts(userId, Ids);
        }

        public smPost SavePostDetails(smSocialMediaProfile data, string image, int userId, string description, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId, string tilte, string heading, string link, string PostMethod)
        {
            return _postService.SavePostDetails(data, image, userId, description, post, postType, scheduleDate, ImpersonateUserId, tilte, heading, link, PostMethod);
        }

        public void SaveFacebookPostDetails(int postId, int userId, string type, string fbId, string fbPostId)
        {
            _postService.SaveFacebookPostDetails(postId, userId, type, fbId, fbPostId);
        }

        public smPost SaveLinkedInPost(smSocialMediaProfile data, string image, int userId, string title, string url, string message, string post, int? postType, DateTime? scheduledDate, int ImpersonateUserId, string heading, string PostMethod)
        {
            return _postService.SaveLinkedInPost(data, image, userId, title, url, message, post, postType, scheduledDate, ImpersonateUserId, heading, PostMethod);
        }

        public void SaveLinkedInPostDetails(int postId, int userId, string type, string key, string url)
        {
            _postService.SaveLinkedInPostDetails(postId, userId, type, key, url);
        }

        public smPost SaveTwitterPost(smSocialMediaProfile data, string image, string link, string Title, int userId, string message, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId, string PostMethod)
        {
            return _postService.SaveTwitterPost(data, image, link,  Title, userId, message, post, postType, scheduleDate, ImpersonateUserId, PostMethod);
        }

        public void SaveTwitterPostDetails(int postId, int userId, string post, string twitterId)
        {
            _postService.SaveTwitterPostDetails(postId, userId, post, twitterId);
        }

        public List<smFacebookPostDetails> GetAllFacebookPostDetails()
        {
            return _postService.GetAllFacebookPostDetails();
        }

        public List<smTwitterPostDetails> GetAllTwitterPostDetails()
        {
            return _postService.GetAllTwitterPostDetails();
        }

       public List<smLinkedInPostDetails> GetAllLinkedInPostDetails()
        {
            return _postService.GetAllLinkedInPostDetails();
        }

        public smSocialMediaProfile GetSocilaMediaAccountByName(string Name)
        {
            return _postService.GetSocilaMediaAccountByName(Name);
        }
        public List<smFacebookPageDetail> getPageAccesstoken(List<List<long>> list )
        {
            return _postService.getPageAccesstoken(list);
        }
      public string SyncFacebookPage (int userId)
        {
            return _postService.SyncFacebookPage(userId);
        }
      public List<smFacebookPageDetail> SaveSyncFacebookPage(List<smFacebookPageDetail> _pageDetail, int userID)
      {
          return _postService.SaveSyncFacebookPage(_pageDetail, userID);
      }
        #endregion
    }
}