using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using smartData.Areas.Users.Models.User;
using System.Web.Script.Serialization;
using Core.Domain;
using Microsoft.AspNet.Identity;
using ServiceLayer.Services;
using smartData.Models.User;
using Sparkle.LinkedInNET;
using Sparkle.LinkedInNET.OAuth2;
using System.Threading.Tasks;
using Sparkle.LinkedInNET.Profiles;
using TweetSharp;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using ASPSnippets.GoogleAPI;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Oauth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util;
using System.Threading;
using smartData.Common;
using Newtonsoft.Json.Linq;
using smartData.Filter;
using smartData.Messages;
using smartData.Areas.Users.ApiControllers;
using ServiceLayer.Interfaces;

namespace smartData.Areas.Users.Controllers
{


    [CheckSession]
    public class PreferenceController : Controller
    {
        #region Global Variables
        IManageCategoryService _manageCategoryService;
        ServiceLayer.Interfaces.IPreferenceService _preferenceService;
        IPreferenceAPIController _preferenceAPIController;
        public List<ScreenPermissionList> obj;
        public List<ScreenPermissionList> objScreenPermissionList = null;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        #endregion


        #region constructor
        public PreferenceController(PreferenceService preferenceService, IPreferenceAPIController preferenceAPIController, IManageCategoryService manageCategoryService)
        {
            _preferenceService = preferenceService;
            _preferenceAPIController = preferenceAPIController;
            _manageCategoryService = manageCategoryService;
        }
        #endregion
        // GET: Users/Preference
        public ActionResult Index()
        {
            return View();
        }

        [AuditLog(Event = "Manage Preference", Message = "Manage Preference")] 
        public ActionResult ManagePreference()
        {
            int userId = SessionManager.LoggedInUser.UserID;
            UserDashboardModel SmPreference = new UserDashboardModel();
            var preferences = _preferenceAPIController.GetPreference(userId);
            SmPreference.socialPreference = preferences;
            return View();
        }

        [HttpGet]
        [AntiforgeryValidate]
        [AuditLog(Event = "Delete preference", Message = "Delete preference")] 
        public JsonResult DeletePreference(string name)
        {
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = SessionManager.LoggedInUser.UserID;
            int IndustryId = SessionManager.LoggedInUser.IndustryId;
            int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
              bool status =true;
            if (name != "All")
            {
                status = _preferenceAPIController.DeletePreference(name, userId, ImpersonateUserId);
            }
            else
            {
                List<smSubIndustry> listSubIndustry = _manageCategoryService.GetSubCategoryList(IndustryId).Where(x => x.IsDeleted == false).ToList();
                status = _preferenceService.DeleteAllPreference(listSubIndustry, userId, ImpersonateUserId);
            }           
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Edit preference", Message = "Edit preference")] 
        public JsonResult EditPreference(int id)
        {
            smPreference content = _preferenceAPIController.EditPreference(id);
            return Json(new { result = content });
        }

        [ValidateInput(false)]
        [AuditLog(Event = "Update Preference", Message = "Update Preference")] 
        public ActionResult UpdatePreference(string PostInformation)
        {
            PostClass postedContent = new PostClass();
            postedContent = JsonConvert.DeserializeObject<PostClass>(PostInformation);
            bool status = false;

            try
            {

                int userId = SessionManager.LoggedInUser.UserID;
                int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
                status = _preferenceAPIController.UpdatePreference(postedContent.PreferenceId, postedContent.Preference, userId, ImpersonateUserId);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

         [HttpGet]
        [AuditLog(Event = "Add Preference", Message = "Add Preference")]        
        public ActionResult AddPreference(string name)
        {
           int userId = SessionManager.LoggedInUser.UserID;
           int IndustryId= SessionManager.LoggedInUser.IndustryId;
           int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
              bool status=true;
           if (name != "All")
           {
               status = _preferenceAPIController.AddPreference(name, userId, ImpersonateUserId);
           }
           else
           {
               List<smSubIndustry> listSubIndustry = _manageCategoryService.GetSubCategoryList(IndustryId).Where(x=>x.IsDeleted==false).ToList();
               status = _preferenceService.AddAllPreference(listSubIndustry, userId, ImpersonateUserId);
           }
           return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
    }
}