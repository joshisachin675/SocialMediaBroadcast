using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInterfaces.Repository
{
    public interface IPostRepository
    {
        List<smSocialMediaProfile> GetSocialMediaAccounts(int userId, List<int> Ids);
        smPost SavePublishingPostDetails(List<string> SelectedPreferences, string SocialMedia, string GroupId, smSocialMediaProfile profile, string ImageArray, string TextMessage, DateTime timenew, int userId, int ImpersonateUserId, DateTime localtime);
        smPost AutoSavePublishingPostDetails(List<string> SelectedPreferences, string SocialMedia, string GroupId, smSocialMediaProfile profile, string ImageArray, string TextMessage, DateTime timenew, int userId, int ImpersonateUserId, DateTime localtime);

        
        smPost SavePostDetails(smSocialMediaProfile data, string image, int userId, string description, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId, string tilte, string heading, string link, string PostMethod);

        void SaveFacebookPostDetails(int postId, int userId, string type, string fbId, string fbPostId);

        smPost SaveLinkedInPost(smSocialMediaProfile data, string image, int userId, string title, string url, string param, string post, int? postType, DateTime? scheduledPost, int ImpersonateUserId,string heading,string PostMethod);

        void SaveLinkedInPostDetails(int postId, int userId, string type, string key, string url);

        smPost SaveTwitterPost(smSocialMediaProfile data, string image,string link, string Title, int userId, string message, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId, string PostMethod);

        void SaveTwitterPostDetails(int PostId, int userId, string post, string twitterId);

        List<smFacebookPostDetails> GetAllFacebookPostDetails();

        List<smTwitterPostDetails> GetAllTwitterPostDetails();

        List<smLinkedInPostDetails> GetAllLinkedInPostDetails();

        smSocialMediaProfile GetSocilaMediaAccountByName(string Name);

        smPost SaveScheduledPosts(smContentLibrary post, DateTime dt, int UserId, int ImpersonateUserId, int Fi, string socialarrd, int EventId);

        bool DeleteScheduledPosts(int id, int UserId, int ImpersonateUserId);

        List<smPost> GetPostsBYEventId(int eventId);

        bool DeleteScheduledEvents(int id, int UserId, int ImpersonateUserId);

        smPost UpdateScheduledPosts(int PostId, DateTime time, int UserId, int ImpersonateUserId, int eventID);


        List<smFacebookPageDetail> getPageAccesstoken(List<List<long>> list);

        string SyncFacebookPage(int userId);
        List<smFacebookPageDetail> SaveSyncFacebookPage(List<smFacebookPageDetail> _pageDetail, int UserId);

        List<smPublishingTime> GetUserPublishingTime();
    }
}
