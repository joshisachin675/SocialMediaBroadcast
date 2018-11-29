using Newtonsoft.Json.Linq;
using ServiceLayer.Interfaces;
using smartData.Filter;
using smartData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace smartData.Areas.Users.Controllers
{
    class AdminUserListFilters : smartData.Common.GridFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public int Roles { get; set; }
        public string Photo { get; set; }
        public int socialId { get; set; }
        public int currentUserId { get; set; }
        public int userType { get; set; }
        public int industryId { get; set; }
    }

    class ContentListFilters : smartData.Common.GridFilter
    {
        public string SocialMedia { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserType { get; set; }
        public int IndustryId { get; set; }
        public string PostedBy { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string IsArchive { get; set; }

    }

    class AdminPostListFilters : smartData.Common.GridFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Photo { get; set; }
        public string SocialMedia { get; set; }
        public string Description { get; set; }
        public int UserType { get; set; }
        public int IndustryId { get; set; }
    }

    class AdminCategoryListFilters : smartData.Common.GridFilter
    {
        public string Category { get; set; }
    }

    class RssFeedListFilters : smartData.Common.GridFilter
    {
        public int UserType { get; set; }
    }

    class SubIndustryListFilters : smartData.Common.GridFilter
    {
        public string subIndustry { get; set; }
        public string Preference { get; set; }
        public int IndustryId { get; set; }
        public int UserType { get; set; }
        public int currentUserId { get; set; }
    }

    class ActivityListFilters : smartData.Common.GridFilter
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public int userType { get; set; }
    }

    class AdminContentVal
    {
        public IEnumerable<Core.Domain.smContentLibrary> AdminContentData;
    }

    class AdminUserVal
    {
        public IEnumerable<Core.Domain.Users> AdminSocialAccountData;
    }

    class AdminPostVal
    {
        public IEnumerable<Core.Domain.smPost> AdminPostData;
    }

    class AdminCategoryVal
    {
        public IEnumerable<Core.Domain.smIndustry> AdminCategoryData;
    }

    class RssFeedVal
    {
        public IEnumerable<Core.Domain.smRssFeeds> RssFeedData;
    }

    class SubIndustryVal
    {
        public IEnumerable<Core.Domain.smSubIndustry> SubIndustryData;
    }

    class ActivityLogVal
    {
        public IEnumerable<Core.Domain.smActivityLog> ActivityLogData;
    }
    class TermsandCondition
    {
        public IEnumerable<Core.Domain.CustomTermsAndCondition> TermsandConditions;
    }

    [HandleException]
    public class AdminAPIController : ApiController
    {
        IAdminService _adminService;
        IManageAdminService _manageAdminService;
        IPreferenceService _prefenceService;
        IUserService _userService;



        public AdminAPIController(IAdminService _HomeService, IManageAdminService manageAdminService, IPreferenceService prefenceService, IUserService userService)
        {
            _adminService = _HomeService;
            _manageAdminService = manageAdminService;
            _prefenceService = prefenceService;
            _userService = userService;
        }

        [HttpPost]
        [ActionName("GetUsersList")]
        public dynamic GetUsersList(JObject Obj)
        {
            AdminUserListFilters filter = Obj.ToObject<AdminUserListFilters>();
            int total = 0;
            AdminUserVal re = new AdminUserVal();
            re.AdminSocialAccountData = _adminService.GetUsersList(filter.limit, filter.offset, filter.order, filter.sort, filter.FirstName, filter.LastName, filter.EmailId, filter.Photo, out total);
            var result = new
            {
                total = total,
                rows = re.AdminSocialAccountData,
            };
            return result;
        }

        [HttpPost]
        [ActionName("GetEndUsersList")]
        public dynamic GetEndUsersList(JObject Obj)
        {
            AdminUserListFilters filter = Obj.ToObject<AdminUserListFilters>();
            int total = 0;
            AdminUserVal re = new AdminUserVal();
            re.AdminSocialAccountData = _adminService.GetEndUsersList(filter.limit, filter.offset, filter.order, filter.sort, filter.FirstName, filter.LastName, filter.EmailId, filter.Photo, filter.userType, filter.Roles, filter.industryId, out total);
            //foreach (var password in re.AdminSocialAccountData.ToList())
            //{
            //    var decryptedPassword = smartData.Common.CommonFunction.Decrypt(re.AdminSocialAccountData.FirstOrDefault().Password);
            //    password.Password = decryptedPassword;
            //}
            var result = new
            {
                total = total,
                rows = re.AdminSocialAccountData,

            };
            return result;
        }

        [HttpPost]
        [ActionName("GetContentLists")]
        public dynamic GetContentLists(JObject Obj)
        {
            ContentListFilters filter = Obj.ToObject<ContentListFilters>();
            if (filter.SocialMedia == "Select Social Media---")
            {
                filter.SocialMedia = "";
            }            
            int total = 0;
            AdminContentVal re = new AdminContentVal();
            re.AdminContentData = _adminService.GetContentLists(filter.limit, filter.offset, filter.order, filter.sort, filter.SocialMedia, filter.Description, filter.FirstName, filter.LastName, filter.CategoryName, filter.CreatedDate, filter.UserType, filter.IndustryId, filter.PostedBy, filter.DateFrom, filter.DateTo ,filter.IsArchive, out total);
            var groupBy = re.AdminContentData.GroupBy(x => x.GroupId.Trim() ).Select(x => x.ToList());
            var d = groupBy.Skip(filter.offset).Take(filter.limit).ToList();
           // total = groupBy.Count();
            var result = new
            {
                total = total,
                rows = d
            };
            return result;
        }

        [HttpPost]
        [ActionName("GetPostList")]
        public dynamic GetPostList(JObject Obj)
        {
            AdminPostListFilters filter = Obj.ToObject<AdminPostListFilters>();
            int total = 0;
            AdminPostVal re = new AdminPostVal();
            re.AdminPostData = _adminService.GetPostList(filter.limit, filter.offset, filter.order, filter.sort, filter.SocialMedia, filter.FirstName, filter.LastName, filter.Description, out total, filter.UserType, filter.IndustryId);
            var result = new
            {
                total = total,
                rows = re.AdminPostData,
            };
            return result;
        }

        [HttpPost]
        [ActionName("GetCategories")]
        public dynamic GetCategories(JObject Obj)
        {
            AdminCategoryListFilters filter = Obj.ToObject<AdminCategoryListFilters>();
            int total = 0;
            AdminCategoryVal re = new AdminCategoryVal();
            re.AdminCategoryData = _adminService.GetCategories(filter.limit, filter.offset, filter.order, filter.sort, filter.Category, out total);
            var result = new
            {
                total = total,
                rows = re.AdminCategoryData,
            };
            return result;
        }

        [HttpPost]
        [ActionName("GetRssFeed")]
        public dynamic GetRssFeed(JObject Obj)
        {
            RssFeedListFilters filter = Obj.ToObject<RssFeedListFilters>();
            int total = 0;
            RssFeedVal re = new RssFeedVal();
            re.RssFeedData = _adminService.GetFeeds(filter.limit, filter.offset, filter.order, filter.sort, filter.UserType , filter.search, out total);
            var result = new
            {
                total = total,
                rows = re.RssFeedData,
            };
            return result;
        }


        [HttpPost]
        [ActionName("GetAllSubCategoryList")]
        public dynamic GetAllSubCategoryList(JObject Obj)
        {
            SubIndustryListFilters filter = Obj.ToObject<SubIndustryListFilters>();
            int total = 0;
            SubIndustryVal re = new SubIndustryVal();
            re.SubIndustryData = _adminService.GetAllSubCategoryList(filter.limit, filter.offset, filter.order, filter.sort, filter.subIndustry, filter.IndustryId, filter.UserType, out total);
            var result = new
            {
                total = total,
                rows = re.SubIndustryData,
            };
            return result;
        }


        [HttpPost]
        [ActionName("GetAllSubCategoryListWithPrefrence")]
        public dynamic GetAllSubCategoryListWithPrefrence(JObject Obj)
        {
            SubIndustryListFilters filter = Obj.ToObject<SubIndustryListFilters>();
            int total = 0;
            SubIndustryVal re = new SubIndustryVal();

            var prefrence = _prefenceService.GetPreference(filter.currentUserId);
            re.SubIndustryData = _adminService.GetAllSubCategoryList(filter.limit, filter.offset, filter.order, filter.sort, filter.Preference, filter.IndustryId, filter.UserType, out total);
            foreach (var item in re.SubIndustryData)
            {
                foreach (var pref in prefrence)
                {
                    if (item.SubIndustryName == pref.Preference)
                    {
                        item.Preference = true;
                    }
                }
            }
            var result = new
            {
                total = total,
                rows = re.SubIndustryData,
            };
            return result;
        }


        [HttpGet]
        [ActionName("GetUserByEmailInd")]
        public string GetUserByEmailInd(string email, string industry)
        {
            var user = _manageAdminService.GetUserByEmailandIndustry(email, industry);
            if (user != null)
            {
                return "exists";
            }
            else
            {
                return "not exists";
            }
        }


        [HttpGet]
        [ActionName("CheckExistingShortName")]
        public string CheckExistingShortName(string shortname, int UserId)
        {
            var user = _userService.CheckExistingShortName(shortname , UserId);
            if (user != null)
            {
                return "exists";
            }
            else
            {
                return "not exists";
            }
        }



        [HttpPost]
        [ActionName("GetActivityList")]
        public dynamic GetActivityList(JObject Obj)
        {
            ActivityListFilters filter = Obj.ToObject<ActivityListFilters>();
            int total = 0;
            ActivityLogVal re = new ActivityLogVal();
            re.ActivityLogData = _adminService.GetActivityList(filter.limit, filter.offset, filter.order, filter.sort, filter.Name, filter.Email, filter.userType, filter.Role, out total);
            var result = new
            {
                total = total,
                rows = re.ActivityLogData,
            };
            return result;
        }



        #region 
        [HttpPost]
        [ActionName("GetTermsandCondition")]
        public dynamic GetTermsandCondition(JObject Obj)
        {


            ActivityListFilters filter = Obj.ToObject<ActivityListFilters>();
            int total = 0;           
            TermsandCondition re = new TermsandCondition();
            re.TermsandConditions = _adminService.GetTermsandCondition(filter.limit, filter.offset, filter.order, filter.sort, out total);
            var result = new
            {
                total = total,
                rows = re.TermsandConditions,
            };
            return result;
        }
        #endregion
        #region
        [HttpPost]
        [ActionName("GetTermsandConditionUser")]
        public dynamic GetTermsandConditionUser(JObject Obj)
        {

            int total = 0;

            int userID = Obj["useriD"].Value<int>();
            int IndustryID = Obj["IndustryId"].Value<int>();
            TermsandCondition re = new TermsandCondition();
            re.TermsandConditions = _adminService.GetTermsandConditionUser(userID, IndustryID);
            var result = new
            {
                total = total,
                rows = re.TermsandConditions,
            };
            return result;
        }
        #endregion

        #region get state list 
        [HttpGet]
        [ActionName("getstate")]
        public dynamic getstate(int id)
        {
         
            var statelist =  _userService.getstate(id);
            return statelist;
        }  
#endregion 
    }
}