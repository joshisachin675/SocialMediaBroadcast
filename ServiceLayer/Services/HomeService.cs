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
    public class HomeService : IHomeService
    {
        IHomeRepository _homeRepository = null;
        #region ctor
        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }
        #endregion

        #region public methods
        public string CreateUser(smSocialMediaProfile profile)
        {
            string response = _homeRepository.CreateUser(profile);
            return response;
        }

        public List<smSocialMediaProfile> GetSocialMedia(int UserId)
        {
            return _homeRepository.GetSocialMedia(UserId);
        }

        public string UpdateSocialMediaStatus(string accountInfo, int userId)
        {
            string response = _homeRepository.UpdateSocialMediaStatus(accountInfo, userId);
            return response;
        }

        public List<smSocialMediaProfile> GetAllSocialMediaAccounts(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, string Photo, int SocialId, int currentUserId, out int total)
        {
            return _homeRepository.GetAllSocialMediaAccounts(limit, offset, order, sort, FirstName, LastName, EmailId, Photo, SocialId, currentUserId, out  total);
        }

        public bool DeleteSocialAccount(int id, int userId, int impersonateUserId)
        {
            return _homeRepository.DeleteSocialAccount(id, userId, impersonateUserId);
        }

        public bool UpdateSocialAccount(int id, int userId, bool status, int ImpersonateUserID)
        {
            return _homeRepository.UpdateSocialAccount(id, userId, status, ImpersonateUserID);
        }

        public List<smPost> GetScheduledPosts(int userId)
        {
            return _homeRepository.GetScheduledPosts(userId);
        }

        public List<smPost> GetTotalPosts(int userId)
        {
            return _homeRepository.GetTotalPosts(userId);
        }

        public List<smPost> GetTotalPostsByUser(int userId)
        {
            return _homeRepository.GetTotalPostsByUser(userId);
        }

        public List<smPost> GetTotalPostsByDate(int userId, DateTime? startDate, DateTime? endDate)
        {
            return _homeRepository.GetTotalPostsByDate(userId, startDate, endDate);
        }


        public smScheduleEvents UpdateEventById(int EventId)
        {
            return _homeRepository.UpdateEventById(EventId);
        }


        public List<smSocialMediaProfile> GetSocialMediaAccountsProfie(int userId)
        {
            return _homeRepository.GetSocialMediaAccountsProfie(userId);
        }

        public List<Users> GetUsersList(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, out int total)
        {
            return _homeRepository.GetUsersList(limit, offset, order, sort, FirstName, LastName, EmailId, out total);
        }

        public List<smContentLibrary> GetCategoryList(int limit, int offset, string order, string sort, int UserType, int IndustryId, out int total)
        {
            return _homeRepository.GetCategoryList(limit, offset, order, sort, UserType, IndustryId, out total);
        }

        public List<smContentLibrary> GetCategoryListByPrefrence(List<smPreference> list, int limit, int offset, string order, string sort, int UserType, int IndustryId, int UserId, out int total)
        {
            return _homeRepository.GetCategoryListByPrefrence(list, limit, offset, order, sort, UserType, IndustryId, UserId, out total);
        }

        public List<smContentLibrary> GetCategoryListforcalender(int IndustryId, List<smPreference> prefrencelist, int userId ,string data) 
        {
            return _homeRepository.GetCategoryListforcalender(IndustryId, prefrencelist, userId ,data);
        }


        public smScheduleEvents AddScheduleEvents(smScheduleEvents events)
        {
            return _homeRepository.AddScheduleEvents(events);
        }


        public List<smScheduleEvents> GetScheduleEvents(int UserId, DateTime startdate, DateTime endDate)
        {
            return _homeRepository.GetScheduleEvents(UserId, startdate, endDate);
        }

        public List<smScheduleEvents> GetEventsBydate(int UserId, DateTime date)
        {
            return _homeRepository.GetEventsBydate(UserId, date);
        }
        #endregion

        public smScheduleEvents GetEventById(int EventId)
        {
            return _homeRepository.GetEventById(EventId);
        }

        public smScheduleEvents UpdateScheduleEvents(smScheduleEvents events)
        {
            return _homeRepository.UpdateScheduleEvents(events);
        }

        #region Manage Post
        public List<smPost> GetAllSocialMediaPost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total)
        {
            return _homeRepository.GetAllSocialMediaPost(limit, offset, order, sort, Name, Description, Url, ImageUrl, currentUserId, out total);

        }
        public List<smPost> GetAllSocialFuturePost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total)
        {
            return _homeRepository.GetAllSocialFuturePost(limit, offset, order, sort, Name, Description, Url, ImageUrl, currentUserId, out total);

        }

        public smPost EditContent(int id)
        {
            return _homeRepository.EditContent(id);
        }


        //public bool EditContent(smPost category)
        //{
        //    return _homeRepository.EditContent(category);
        //}


        public bool DeleteSocialPost(int id, int userId, int ImpersonateUserId)
        {
            return _homeRepository.DeleteSocialPost(id, userId, ImpersonateUserId);
        }

        public bool UpdateContent(int PostId, string Description, string Name, List<string> imageIds, string url, int userId, int ImpersonateUserId)
        {
            return _homeRepository.UpdateContent(PostId, Description, Name, imageIds, url, userId, ImpersonateUserId);
        }

        public List<smPost> GetSocialMediaPost(int userId)
        {
            return _homeRepository.GetSocialMediaPost(userId);
        }
        public List<smPost> GetFuturePost(int userId)
        {
            return _homeRepository.GetFuturePost(userId);

        }

        #endregion

        public List<smPreference> GetPreference(int userId)
        {
            return _homeRepository.GetPreference(userId);

        }

        public smScheduleEvents GetEventByNewId(string EventId)
        {
            return _homeRepository.GetEventByNewId(EventId);
        }
        public string SaveUserFBPageDetail(smFacebookPageDetail pageDetail)
        {
            return _homeRepository.SaveUserFBPageDetail(pageDetail);
        }

        public dynamic GetFacebookPageDetail(int userID)
        {
            return _homeRepository.GetFacebookPageDetail(userID);
        }
        public List<smIndustry> GetIndustryNameTermsAndCondition()
        {
            return _homeRepository.GetIndustryNameTermsAndCondition();
        }
        public smFacebookDefaultPreference SetFacebookDefaultPost(smFacebookDefaultPreference fbDefault)
        {
            return _homeRepository.SetFacebookDefaultPost(fbDefault);
        }
        public smFacebookDefaultPreference GetFacebookPereference(int userId)
        {
            return _homeRepository.GetFacebookPereference(userId);
        }
        public smFacebookDefaultPreference GetDefaultPrerefence(int userID)
        {
            return _homeRepository.GetDefaultPrerefence(userID);
        }

        public List<smHomeValue> GetAllLeads(int limit, int offset, string order, string sort, string Name, string Description, int currentUserId, out int total)
        {
            return _homeRepository.GetAllLeads(limit, offset, order, sort, Name, Description, currentUserId, out total);
        }

        public dynamic GetTotalClicks(int UserId)
        {
       return  _homeRepository.GetTotalClicks(UserId);
        }
        public dynamic EditLeads (smHomeValue data)
        {
            return _homeRepository.EditLeads(data);
        }
    }
}
