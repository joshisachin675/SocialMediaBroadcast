using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IHomeService
    {
        string CreateUser(smSocialMediaProfile profile);

        List<smSocialMediaProfile> GetSocialMedia(int userId);
        string UpdateSocialMediaStatus(string accountInfo, int userId);
        List<smSocialMediaProfile> GetAllSocialMediaAccounts(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, string Photo, int SocialId, int currentUserId, out int total);
        bool DeleteSocialAccount(int id, int userId,int impersonateUserId);
        bool UpdateSocialAccount(int id, int userId, bool status,int ImpersonateUserID);
        List<smPost> GetScheduledPosts(int userId);
        List<smPost> GetTotalPosts(int userId);
        List<smPost> GetTotalPostsByUser(int userId);
        List<smPost> GetTotalPostsByDate(int userId, DateTime? startDate, DateTime? endDate);
        smScheduleEvents UpdateEventById(int EventId);
        
        List<smSocialMediaProfile> GetSocialMediaAccountsProfie(int userId);
        List<smContentLibrary> GetCategoryList(int limit, int offset, string order, string sort, int UserType, int IndustryId, out int total);
        List<smContentLibrary> GetCategoryListByPrefrence(List<smPreference> list, int limit, int offset, string order, string sort, int UserType, int IndustryId, int UserId, out int total);
        List<smContentLibrary> GetCategoryListforcalender(int IndustryId, List<smPreference> prefrencelist, int userId,string data);
        List<smPreference> GetPreference(int userId);
        smScheduleEvents AddScheduleEvents(smScheduleEvents events);
        List<smScheduleEvents> GetScheduleEvents(int UserId, DateTime startdate, DateTime endDate);

        List<smScheduleEvents> GetEventsBydate(int UserId, DateTime date);

        smScheduleEvents GetEventById(int EventId);

        smScheduleEvents UpdateScheduleEvents(smScheduleEvents events);

        smScheduleEvents GetEventByNewId(string EventId);

        #region  Manage Post

        List<smPost> GetSocialMediaPost(int userId);
        List<smPost> GetFuturePost(int userId);
        bool UpdateContent(int PostedId, string Description, string Name, List<string> imageIds, string url, int userId,int ImpersonateUserId);
        List<smPost> GetAllSocialMediaPost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total);
        List<smPost> GetAllSocialFuturePost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total);
        smPost EditContent(int id);
        //bool EditContent(smPost category);
        bool DeleteSocialPost(int id, int userId,int  ImpersonateUserId);

        #endregion


        string SaveUserFBPageDetail(smFacebookPageDetail profile);

      dynamic GetFacebookPageDetail(int userID);

        List<smIndustry> GetIndustryNameTermsAndCondition();
        smFacebookDefaultPreference SetFacebookDefaultPost(smFacebookDefaultPreference fbDefault);

        smFacebookDefaultPreference GetFacebookPereference(int userId);

        smFacebookDefaultPreference GetDefaultPrerefence(int userId);

        List<smHomeValue> GetAllLeads(int limit, int offset, string order, string sort, string Name, string Description, int currentUserId, out int total);

       dynamic GetTotalClicks (int UserId);

       dynamic EditLeads(smHomeValue data);
    }
}
