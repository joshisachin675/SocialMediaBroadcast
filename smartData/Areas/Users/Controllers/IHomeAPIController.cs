using Core.Domain;
using Newtonsoft.Json.Linq;
using smartData.Models.User;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace smartData
{
    public interface IHomeAPIController
    {
        string Create(smSocialMediaProfile profile);
        List<smSocialMediaProfile> GetSocialMedia(int UsersId);
        string SocialMediaStatus(string accountInfo, int userId);
        List<smSocialMediaProfile> GetAllSocialMediaAccounts(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, string Photo, int SocialId, int currentUserId, out int total);
        bool DeleteSocialAccount(int id, int userId,int impersonateUserId);
        bool UpdateSocialAccount(int id, int userId, bool status, int ImpersonateUserID);
        List<smPost> GetScheduledPosts(int userId);
        List<smPost> GetTotalPosts(int userId);
        List<smPost> GetTotalPostsByUser(int userId);
        List<smSocialMediaProfile> GetSocialMediaAccountsProfie(int userId);

        List<smPost> GetFuturePost(int userId);
        List<smPost> GetSocialMediaPost(int userId);
        bool DeleteSocialPost(int id, int userId, int ImpersonateUserId);
        List<smPost> GetAllSocialMediaPost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total);
        List<smPost> GetAllSocialFuturePost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total);
        bool UpdateContent(int PostedId, string Description, string Name, List<string> imageIds, string Url, int userId, int ImpersonateUserId);
        smPost EditContent(int id);
        bool SaveHomeValue([FromBody]smHomeValue HomeValue);
        smHomeValue GetHomeValue(int postId);
        string SaveUserFBPageDetail(smFacebookPageDetail pageDetail);

        smFacebookDefaultPreference SetFacebookDefaultPost(smFacebookDefaultPreference fbDefault);
     //   List<smHomeValue> GetAllLeads(int limit, int offset, string order, string sort, string Name, string Description, out int total);
    }
}

