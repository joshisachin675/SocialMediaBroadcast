using Core.Domain;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using smartData.Common;
using smartData.Filter;
using smartData.Messages;
using smartData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;

namespace smartData.Areas.Admin.Controllers
{
    [CheckSession]
    //[AuthorizedRoles(Roles = "SuperAdmin")]
    public class ManageAdminController : Controller
    {
        #region Global Variables
        IManageAdminService _manageAdminService;
        IManageAdminAPIController _manageAdminAPIController;
        IManageContentService _manageContentService;
        IUserService _userService;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        #endregion

        #region constructor
        public ManageAdminController(ManageAdminService manageAdminService, IManageAdminAPIController manageAdminAPIController, IUserService userService, IManageContentService manageContentService)
        {
            _manageAdminService = manageAdminService;
            _manageAdminAPIController = manageAdminAPIController;
            _userService = userService;
            _manageContentService = manageContentService;
        }
        #endregion

        //
        // GET: /Admin/ManageAdmin/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Delete the User admin by superadmin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        public JsonResult DeleteAdminAccount(int id)
        {
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageAdminAPIController.DeleteAdminAccount(id, userId);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create the User admin by superadmin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateAdmin(AdminRegisterViewModel model)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    List<Core.Domain.Users> list = _manageAdminAPIController.GetUsersByEmail(model.UserName.ToString());
                    Core.Domain.Users userobj = new Core.Domain.Users();
                    userobj.FirstName = model.FirstName;
                    userobj.LastName = model.LastName;
                    userobj.IndustryId = model.IndustryName;
                    userobj.IsDeleted = false;
                    userobj.Password = smartData.Common.CommonFunction.Encrypt(model.Password);
                    userobj.ConfirmPassword = smartData.Common.CommonFunction.Encrypt(model.ConfirmPassword);
                    userobj.CreatedBy = smartData.Common.SessionManager.LoggedInUser.UserID;
                    userobj.Email = model.UserName;
                    var addAdmin = _userService.AddUser(userobj);

                    //int _userID = WebSecurity.GetUserId(model.UserName);
                    //if (list.Count == 0 && _userID > 0)
                    //{
                    //    ((SimpleMembershipProvider)Membership.Provider).DeleteUser(model.UserName.ToString(), true); // deletes record from webpages_Membership table
                    //}
                    //WebSecurity.CreateUserAndAccount(model.UserName, model.Password, propertyValues: new { FirstName = model.FirstName, LastName = model.LastName, UserType = 2 });
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                }
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        public JsonResult UpdateAdminStatusActive(int id)
        {
            bool stat = true;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageAdminAPIController.UpdateAdminStatus(id, userId, stat);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        public JsonResult UpdateAdminStatusDeactive(int id)
        {
            bool stat = false;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageAdminAPIController.UpdateAdminStatus(id, userId, stat);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string GetUserByEmailandIndustry(string email, string industry)
        {
            var user = _manageAdminAPIController.GetUserByEmailandIndustry(email, industry);
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
        public string GetUserByEmailandIndustryId(string email, int  industry)
        {
            var user = _manageAdminService.GetUserByEmailandIndustryId(email, industry);
            if (user != null)
            {
                return "exists";
            }
            else
            {
                return "not exists";
            }
        }


        [AuditLog(Event = "SuperAdminLogin", Message = "SuperAdmin Login to User Account")]
        public ActionResult LoginAdmin(string email, string password, int industryId)
        {
            var currentUser = smartData.Common.SessionManager.LoggedInUser.UserID;
            //List<Core.Domain.Users> list = _userService.GetUsersByEmail(email);
            Core.Domain.Users list = _manageAdminService.GetUserByEmailandIndustryId(email, industryId);
            if (list != null)//list.Count > 0
            {
                Core.Domain.Users _objuser = list;
                var user = _userService.AuthenticateUser(email, industryId, _objuser.Password);

                if (user != null)
                {
                    int cID = WebSecurity.GetUserId(email);
                    LoginViewModel model = new LoginViewModel();
                    model.UserName = email;
                    model.Password = password;
                    CreateAuthenticateFormsTicket(model);
                    var HasProfileImage = _objuser.Photo == null ? false : true;
                    SessionManager.FillSession(_objuser.UserId, _objuser.FirstName, _objuser.LastName, _objuser.Email, Convert.ToInt32(_objuser.UserTypeId), HasProfileImage, user.IndustryId, currentUser);
                    return RedirectToAction("Index", "Home", new { @Area = "Users" });
                }
            }
            return View();
        }


        [NonAction]
        private void CreateAuthenticateFormsTicket(LoginViewModel eTracLogin)
        {
            try
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                             1,
                             eTracLogin.UserName.ToString(),                                 //user Name
                             DateTime.Now,
                             DateTime.Now.AddMinutes(30),                          // expiry in 30 min
                             eTracLogin.RememberMe, "");

                if (eTracLogin.RememberMe)
                {
                    string formsCookieStr = string.Empty;

                    formsCookieStr = FormsAuthentication.Encrypt(authTicket);

                    HttpCookie FormsCookie = new HttpCookie("LoginCookie", formsCookieStr);
                    FormsCookie.Expires = DateTime.Now.AddDays(15);

                    FormsCookie["UserName"] = eTracLogin.UserName;
                    FormsCookie["pwd"] = eTracLogin.Password;

                    HttpContext.Response.Cookies.Add(FormsCookie);

                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    HttpCookie myCookie = new HttpCookie("LoginCookie");
                    myCookie.Expires = DateTime.Now;
                    Response.Cookies.Add(myCookie);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);
                }
                Session["eTrac"] = eTracLogin;
            }
            catch (Exception ex)
            { throw ex; }
        }



    }
}