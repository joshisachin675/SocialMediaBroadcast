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
    public class PostService : IPostService
    {
        IPostRepository _postRepository = null;
        #region ctor
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        #endregion

        #region public methods
        public List<smSocialMediaProfile> GetSocialMediaAccounts(int userId, List<int> ids)
        {
            return _postRepository.GetSocialMediaAccounts(userId, ids);
        }


        public smPost SavePublishingPostDetails(List<string> SelectedPreferences, string SocialMedia, string GroupId, smSocialMediaProfile profile, string ImageArray, string TextMessage, DateTime timenew, int userId, int ImpersonateUserId , DateTime localtime)
        {
            return _postRepository.SavePublishingPostDetails(SelectedPreferences, SocialMedia, GroupId, profile, ImageArray, TextMessage, timenew, userId, ImpersonateUserId, localtime);
        }
        public smPost AutoSavePublishingPostDetails(List<string> SelectedPreferences, string SocialMedia, string GroupId, smSocialMediaProfile profile, string ImageArray, string TextMessage, DateTime timenew, int userId, int ImpersonateUserId, DateTime localtime)
        {
            return _postRepository.AutoSavePublishingPostDetails(SelectedPreferences, SocialMedia, GroupId, profile, ImageArray, TextMessage, timenew, userId, ImpersonateUserId, localtime);
        }
        public smPost SavePostDetails(smSocialMediaProfile data, string image, int userId, string description, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId, string tilte, string heading, string link, string PostMethod)
        {
            return _postRepository.SavePostDetails(data, image, userId, description, post, postType, scheduleDate, ImpersonateUserId, tilte, heading, link, PostMethod);
        }

        public void SaveFacebookPostDetails(int postId, int userId, string type, string fbId, string fbPostId)
        {
            _postRepository.SaveFacebookPostDetails(postId, userId, type, fbId, fbPostId);
        }

        public smPost SaveLinkedInPost(smSocialMediaProfile data, string image, int userId, string title, string url, string message, string post, int? postType, DateTime? scheduledDate, int ImpersonateUserId, string heading, string PostMethod)
        {
            return _postRepository.SaveLinkedInPost(data, image, userId, title, url, message, post, postType, scheduledDate, ImpersonateUserId, heading, PostMethod);
        }

        public void SaveLinkedInPostDetails(int postId, int userId, string type, string key, string url)
        {
            _postRepository.SaveLinkedInPostDetails(postId, userId, type, key, url);
        }

        public smPost SaveTwitterPost(smSocialMediaProfile data, string imagePath, string link, string Title, int userId, string message, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId, string PostMethod)
        {
            return _postRepository.SaveTwitterPost(data, imagePath, link, Title, userId, message, post, postType, scheduleDate, ImpersonateUserId, PostMethod);
        }

        public void SaveTwitterPostDetails(int PostId, int userId, string post, string twitterId)
        {
            _postRepository.SaveTwitterPostDetails(PostId, userId, post, twitterId);
        }

        public List<smFacebookPostDetails> GetAllFacebookPostDetails()
        {
            return _postRepository.GetAllFacebookPostDetails();
        }

        public List<smTwitterPostDetails> GetAllTwitterPostDetails()
        {
            return _postRepository.GetAllTwitterPostDetails();
        }

        public List<smLinkedInPostDetails> GetAllLinkedInPostDetails()
        {
            return _postRepository.GetAllLinkedInPostDetails();
        }


        public smSocialMediaProfile GetSocilaMediaAccountByName(string Name)
        {
            return _postRepository.GetSocilaMediaAccountByName(Name);
        }

        public List<smPublishingTime> GetUserPublishingTime()
        {
            return _postRepository.GetUserPublishingTime();
        }
        public smPost SaveScheduledPosts(smContentLibrary post, DateTime dt, int UserId, int ImpersonateUserId, int Fid, string socialarr, int EventId)
        {
            return _postRepository.SaveScheduledPosts(post, dt, UserId, ImpersonateUserId, Fid, socialarr, EventId);
        }

        public bool DeleteScheduledPosts(int id, int UserId, int ImpersonateUserId)
        {
            return _postRepository.DeleteScheduledPosts(id, UserId, ImpersonateUserId);
        }

        public List<smPost> GetPostsBYEventId(int eventId)
        {
            return _postRepository.GetPostsBYEventId(eventId);
        }

        public bool DeleteScheduledEvents(int id, int UserId, int ImpersonateUserId)
        {
            return _postRepository.DeleteScheduledEvents(id, UserId, ImpersonateUserId);
        }

        public smPost UpdateScheduledPosts(int PostId, DateTime time, int UserId, int ImpersonateUserId, int EventId)
        {
            return _postRepository.UpdateScheduledPosts(PostId, time, UserId, ImpersonateUserId, EventId);
        }
        public List<smFacebookPageDetail> getPageAccesstoken(List<List<long>> list)
        {
            return _postRepository.getPageAccesstoken(list);
        }
        public string SyncFacebookPage(int userId)
        {
            return _postRepository.SyncFacebookPage(userId);
        }
        public List<smFacebookPageDetail> SaveSyncFacebookPage(List<smFacebookPageDetail> _pageDetail, int UserId)
        {
            return _postRepository.SaveSyncFacebookPage(_pageDetail ,UserId);
        }
        #endregion

    }
}
