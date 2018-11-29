using Core.Domain;
using smartData.Areas.Users.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Areas.Users.ApiControllers
{
    public interface IPostAPIController
    {
        List<smSocialMediaProfile> GetSocialMediaAccounts(int userId, List<int> Ids);

        smPost SavePostDetails(smSocialMediaProfile data, string image, int userId, string description, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId, string tilte, string heading, string link, string PostMethod);

        void SaveFacebookPostDetails(int postId, int userId, string type, string fbId, string fbPostId);

        smPost SaveLinkedInPost(smSocialMediaProfile data, string image, int userId, string title, string url, string message, string post, int? postType, DateTime? scheduledPost, int ImpersonateUserId, string heading, string PostMethod);

        void SaveLinkedInPostDetails(int postId, int userId, string type, string key, string url);

        smPost SaveTwitterPost(smSocialMediaProfile data, string imagePath, string link, string Title, int userId, string message, string post, int? postType, DateTime? scheduledate, int ImpersonateUserId, string PostMethod);

        void SaveTwitterPostDetails(int PostId, int userId, string post, string twitterId);

        List<smFacebookPostDetails> GetAllFacebookPostDetails();

        List<smTwitterPostDetails> GetAllTwitterPostDetails();

        List<smLinkedInPostDetails> GetAllLinkedInPostDetails();

        smSocialMediaProfile GetSocilaMediaAccountByName( string Name);






        List<smFacebookPageDetail> getPageAccesstoken(List<List<long>> list);

        string SyncFacebookPage(int userId);

        List<smFacebookPageDetail> SaveSyncFacebookPage(List<smFacebookPageDetail> _pageDetail, int userID);
    }
}