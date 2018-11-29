using Core.Domain;
using Newtonsoft.Json.Linq;
using ServiceLayer.Interfaces;
using smartData.Common;
using smartData.Filter;
using smartData.Infrastructure;
using smartData.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace smartData.Areas.Users.Controllers
{

    class SocialAccountListFilters : smartData.Common.GridFilter
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public string photo { get; set; }
        public int SocialId { get; set; }
        public int currentUserId { get; set; }
    }

    class SocialAccountListFiltersPost : smartData.Common.GridFilter
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public int currentUserId { get; set; }

    }
    class SocialPost
    {
        public IEnumerable<smPost> SocialAccountData;
    }

    class ContentFilters : smartData.Common.GridFilter
    {
        public string categoryname { get; set; }
        public int IndustryId { get; set; }
        public int UserType { get; set; }
        public int UserId { get; set; }
    }

    class SocialAccountVal
    {
        public IEnumerable<smSocialMediaProfile> SocialAccountData;
    }

    [HandleException]
    public class HomeAPIController : ApiController, IHomeAPIController
    {
        IHomeService _homeService;
        IHomeValueService _homeValueService;


        public HomeAPIController(IHomeService _UserService, IHomeValueService homeValueService)
        {
            _homeService = _UserService;
            _homeValueService = homeValueService;
            //System.Net.Http.Headers.contContentType = new MediaTypeHeaderValue("application/json");
        }

        [HttpPost]
        [HandleAPIExceptionAttribute]
        public string Create(smSocialMediaProfile profile)
        {
            string response = _homeService.CreateUser(profile);
            return response;
        }
        [HttpPost]
        [HandleAPIExceptionAttribute]
        public string SaveUserFBPageDetail(smFacebookPageDetail pageDetail)
        {
            string response = _homeService.SaveUserFBPageDetail(pageDetail);
            return response;
        }
        [HttpPost]
        [HandleAPIExceptionAttribute]
        public smFacebookDefaultPreference SetFacebookDefaultPost(smFacebookDefaultPreference fbDefault)
        {
            smFacebookDefaultPreference response = _homeService.SetFacebookDefaultPost(fbDefault);
            return response;
        }
        [HttpPost]
        [ActionName("GetFacebookPereference")]
        public smFacebookDefaultPreference GetFacebookPereference(int userId)
        {
            smFacebookDefaultPreference response = _homeService.GetFacebookPereference(userId);
            return response;
        }
        public List<smSocialMediaProfile> GetSocialMedia(int UsersId)
        {
            return _homeService.GetSocialMedia(UsersId);
        }

        [HttpPost]
        [HandleAPIExceptionAttribute]
        public string SocialMediaStatus(string accountInfo, int userId)
        {
            return _homeService.UpdateSocialMediaStatus(accountInfo, userId);
        }

        [HttpPost]
        [HandleAPIExceptionAttribute]
        public List<smSocialMediaProfile> GetAllSocialMediaAccounts(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, string Photo, int SocialId, int currentUserId, out int total)
        {
            return _homeService.GetAllSocialMediaAccounts(limit, offset, order, sort, FirstName, LastName, EmailId, Photo, SocialId, currentUserId, out total);
        }

        [HttpPost]
        [ActionName("GetAllSocialMedia")]
        public dynamic GetAllSocialMedia(JObject Obj)
        {
            SocialAccountListFilters filter = Obj.ToObject<SocialAccountListFilters>();
            int total;
            SocialAccountVal re = new SocialAccountVal();
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = filter.currentUserId;
            re.SocialAccountData = _homeService.GetAllSocialMediaAccounts(filter.limit, filter.offset, filter.order, filter.sort, filter.firstName, filter.lastName, filter.emailId, filter.photo, filter.SocialId, userId, out total);
            var result = new
            {
                total = total,
                rows = re.SocialAccountData,
            };
            return result;
        }

        /// <summary>
        /// Delete the Socal account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        public bool DeleteSocialAccount(int id, int userId, int impersonateUserId)
        {
            return _homeService.DeleteSocialAccount(id, userId, impersonateUserId);
        }

        /// <summary>
        /// Delete the Socal account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        public bool UpdateSocialAccount(int id, int userId, bool status, int ImpersonateUserID)
        {
            return _homeService.UpdateSocialAccount(id, userId, status, ImpersonateUserID);
        }

        public List<smPost> GetScheduledPosts(int userId)
        {
            return _homeService.GetScheduledPosts(userId);
        }

        public List<smPost> GetTotalPosts(int userId)
        {
            return _homeService.GetTotalPosts(userId);
        }

        public List<smPost> GetTotalPostsByUser(int userId)
        {
            return _homeService.GetTotalPostsByUser(userId);
        }

        public List<smSocialMediaProfile> GetSocialMediaAccountsProfie(int userId)
        {
            return _homeService.GetSocialMediaAccountsProfie(userId);
        }
        [HttpPost]
        [ActionName("GetContentListByKeyword")]
        public dynamic GetContentListByKeyword(JObject obj)
        {
            int total = 0;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            //  userId = SessionManager.LoggedInUser.UserID;

            ContentFilters filter = obj.ToObject<ContentFilters>();

            //string[] strArray = null;
            //if (string.IsNullOrEmpty(filter.categoryname))
            //    strArray = string.Join(",", GetPreference(userId).Select(v => v.Preference)).Split(',');
            //else
            //    strArray = filter.categoryname.Split(',');

            //List<string> myList = strArray.ToList();

            //List<smContentLibrary> contentlist = null;
            List<smContentLibrary> contentlist = new List<smContentLibrary>(0);
            //if (myList[0] != "")
            //{
            contentlist = _homeService.GetCategoryList(filter.limit, filter.offset, filter.order, filter.sort, filter.UserType, filter.IndustryId, out total);
            //}

            var result = new
            {
                total = total,
                rows = contentlist,
            };
            return result;

        }



        [HttpPost]
        [ActionName("GetContentListByPreference")]
        public dynamic GetContentListByPreference(JObject obj)
        {
            int total = 0;
            int userId = 0;
            ContentFilters filter = obj.ToObject<ContentFilters>();
            //string[] strArray = null;

            var strArray = GetPreference(filter.UserId);
            List<smContentLibrary> contentlist = new List<smContentLibrary>(0);
            contentlist = _homeService.GetCategoryListByPrefrence(strArray, filter.limit, filter.offset, filter.order, filter.sort, filter.UserType, filter.IndustryId, filter.UserId, out total);
            var result = new
            {
                total = total,
                rows = contentlist,
            };
            return result;

        }



        public List<smPreference> GetPreference(int userId)
        {
            return _homeService.GetPreference(userId);
        }

        #region  Manage Post
        public List<smPost> GetAllSocialMediaPost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total)
        {
            return _homeService.GetAllSocialMediaPost(limit, offset, order, sort, Name, Description, Url, ImageUrl, currentUserId, out total);
        }
        public List<smPost> GetAllSocialFuturePost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total)
        {
            return _homeService.GetAllSocialFuturePost(limit, offset, order, sort, Name, Description, Url, ImageUrl, currentUserId, out total);
        }

        [HttpPost]
        public bool SaveHomeValue([FromBody]smHomeValue HomeValue)
        {
            return _homeValueService.SaveHomeValue(HomeValue);
        }


        public smHomeValue GetHomeValue([FromUri]int postId)
        {
            return _homeValueService.GetHomeValue(postId);
        }

        //[HttpPost]
        //[ActionName("GetAllSocialMedia")]
        //public dynamic GetAllSocialMedia(JObject Obj)
        //{
        //    SocialAccountListFilters filter = Obj.ToObject<SocialAccountListFilters>();
        //    int total;
        //    SocialAccountVal re = new SocialAccountVal();
        //    MembershipUser mu = Membership.GetUser();
        //    int userId = 0;
        //    userId = Convert.ToInt32(mu.ProviderUserKey);
        //    re.SocialAccountData = _homeService.GetAllSocialMediaAccounts(filter.limit, filter.offset, filter.order, filter.sort, filter.firstName, filter.lastName, filter.emailId, filter.photo, filter.SocialId, userId, out total);
        //    var result = new
        //    {
        //        total = total,
        //        rows = re.SocialAccountData,
        //    };
        //    return result;
        //}




        [HttpPost]
        [ActionName("GetAllSocialPost")]
        public dynamic GetAllSocialPost(JObject Obj)
        {
            SocialAccountListFiltersPost filter = Obj.ToObject<SocialAccountListFiltersPost>();
            int total;

            SocialPost re = new SocialPost();
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = filter.currentUserId;
            re.SocialAccountData = _homeService.GetAllSocialMediaPost(filter.limit, filter.offset, filter.order, filter.sort, filter.Name, filter.Description, filter.Url, filter.ImageUrl, filter.currentUserId, out total);

            foreach (var item in re.SocialAccountData)
            {

                item.PostDate = item.PostDate.ToLocalTime();

                if (item.Description == null)
                {
                    item.Description = item.Name;
                }
            }
            var result = new
            {
                total = total,
                rows = re.SocialAccountData
            };

            return result;
        }





        [HttpPost]
        public bool UpdateContent(int PostedId, string Description, string Name, List<string> imageIds, string url, int userId, int ImpersonateUserId)
        {
            return _homeService.UpdateContent(PostedId, Description, Name, imageIds, url, userId, ImpersonateUserId);
        }


        [HttpPost]
        [ActionName("GetAllSocialFuturePost")]
        public dynamic GetAllSocialFuturePost(JObject Obj)
        {
            SocialAccountListFiltersPost filter = Obj.ToObject<SocialAccountListFiltersPost>();
            int total;

            SocialPost re = new SocialPost();
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = filter.currentUserId;
            re.SocialAccountData = _homeService.GetAllSocialFuturePost(filter.limit, filter.offset, filter.order, filter.sort, filter.Name, filter.Description, filter.Url, filter.ImageUrl, filter.currentUserId, out total);

            foreach (var item in re.SocialAccountData)
            {
                item.PostDate = item.PostDate.ToLocalTime();
                if (item.Description == null)
                {
                    item.Description = item.Name;
                }
            }
            var result = new
            {
                total = total,
                rows = re.SocialAccountData
            };

            return result;
        }
        //[HttpPost]
        //[ActionName("EditContent")]

        //public smPost EditContent(int id)
        //{
        //    return _homeService.EditContent(id);
        //}
        [HttpPost]
        public smPost EditContent(int id)
        {
            return _homeService.EditContent(id);
        }

        /// <summary>
        /// Delete the Socal account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        /// 
        public bool DeleteSocialPost(int id, int userId, int ImpersonateUserId)
        {
            return _homeService.DeleteSocialPost(id, userId, ImpersonateUserId);
        }

        public List<smPost> GetSocialMediaPost(int userId)
        {
            return _homeService.GetSocialMediaPost(userId);
        }


        public List<smPost> GetFuturePost(int userId)
        {
            return _homeService.GetFuturePost(userId);
        }
        #endregion

        [HttpPost]
        [ActionName("GetFacebookPageDetail")]
        public dynamic GetFacebookPageDetail(JObject userId)
        {
            int userID = userId["userID"].Value<int>();

            List<CustomModelForPage> fbpage = new List<CustomModelForPage>();
            fbpage = _homeService.GetFacebookPageDetail(userID);
            return fbpage;
        }
        #region Get Default preference to bind radio button
        [HttpPost]
        [ActionName("GetDefaultPrerefence")]
        public dynamic GetDefaultPrerefence (JObject userId)
        {
            int UserID = userId["userID"].Value<int>();
            smFacebookDefaultPreference preference = new smFacebookDefaultPreference();
            return _homeService.GetDefaultPrerefence(UserID);
        }
        #endregion
        [HttpPost]
        [ActionName("SetFacebookDefaultPreference")]
        public dynamic SetFacebookDefaultPreference(JObject obj)
        {

            long PageId = obj["PageId"].Value<long>();
            int UserID = obj["userId"].Value<int>();
            smFacebookDefaultPreference fbdefault = new smFacebookDefaultPreference();
            fbdefault.userID = UserID;
            fbdefault.PageId = PageId;
            fbdefault.UpdatedDate = DateTime.UtcNow;
            fbdefault.isActive = true;
            fbdefault.isDeleted = false;
            if (PageId>0)
            {
                fbdefault.Type = 1;   
            }
            else
            {
                fbdefault.Type = 0;
            }
            smFacebookDefaultPreference result = _homeService.SetFacebookDefaultPost(fbdefault);
          
            return true;

        }
        [HttpPost]
        [ActionName("GetIndustryNameTermsAndCondition")]
        public dynamic GetIndustryNameTermsAndCondition()
        {


            List<smIndustry> industryDetail = new List<smIndustry>();
            industryDetail = _homeService.GetIndustryNameTermsAndCondition();
            return industryDetail;
        }

        #region  manage leads  User 
        [HttpPost]
        [ActionName("GetLeads")]
        public dynamic GetLeads(JObject Obj)
        {
            SocialAccountListFiltersPost filter = Obj.ToObject<SocialAccountListFiltersPost>();
            int total;

            List<smHomeValue> re = new List<smHomeValue>();

            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = filter.currentUserId;
            re = _homeService.GetAllLeads(filter.limit, filter.offset, filter.order, filter.sort, filter.Name, filter.Description, filter.currentUserId ,out total);

           
            var result = new
            {
                total = total,
                rows = re
            };

            return result;
        }
        #endregion
    }
}