using Core.Domain;
using CoreEntities.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceLayer.Email;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using smartData.Common;
using smartData.Filter;
using smartData.Messages;
using smartData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace smartData.Areas.Admin.Controllers
{
    [Authorize]
    //[AuthorizedRoles(Roles = "SuperAdmin,Admin,User")]
    public class AccountController : Controller
    {
        IManageAdminService _manageAdminService;
        IEmailService _emailService;
        IUserService _userService;
        IScreenPermissionService _screenPermissionService;
        IManageContentService _manageContentService;
        public AccountController(IScreenPermissionService screenPermissionService, IEmailService emailService, IUserService userService, IManageContentService manageContentService, IManageAdminService manageAdminService)
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())),
            new EmailService(new Config(), new HtmlMessageFormatter(new Config())
                , new EmailMessageSender(new Config())), screenPermissionService, userService, manageContentService, manageAdminService)
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager, IEmailService emailService, IScreenPermissionService screenPermissionService, IUserService userService, IManageContentService manageContentService, IManageAdminService manageAdminService)
        {
            _emailService = emailService;
            _userService = userService;
            UserManager = userManager;
            _screenPermissionService = screenPermissionService;
            _manageContentService = manageContentService;
            _manageAdminService = manageAdminService;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            // string url = Request.Url.AbsoluteUri;
            // string [] urlarray=url.Split('/');
            // string suburl=urlarray[3];

            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }

            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel eTracLogin = new LoginViewModel();
            if (Request.Cookies["LoginCookie"] != null)
            {
                try
                {
                    eTracLogin.UserName = Request.Cookies["LoginCookie"]["UserName"];
                    eTracLogin.Password = Request.Cookies["LoginCookie"]["pwd"]; //Cryptography.GetDecryptedData(Request.Cookies["LoginCookie"]["pwd"], true); ;
                    eTracLogin.IndustryId = Convert.ToInt32(Request.Cookies["LoginCookie"]["IndustryId"]); 
                    eTracLogin.RememberMe = true;
                    // eTracLogin.user = suburl;
                    return View(eTracLogin);
                }
                catch
                {
                    return View();
                }
            }
            else
                return View();
        }

        [AllowAnonymous]
        public ActionResult SuperAdminLogin(string returnUrl)
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            // string url = Request.Url.AbsoluteUri;
            // string [] urlarray=url.Split('/');
            // string suburl=urlarray[3];

            //ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }

            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel eTracLogin = new LoginViewModel();
            if (Request.Cookies["LoginCookie"] != null)
            {
                try
                {
                    eTracLogin.UserName = Request.Cookies["LoginCookie"]["UserName"];
                    eTracLogin.Password = Request.Cookies["LoginCookie"]["pwd"]; //Cryptography.GetDecryptedData(Request.Cookies["LoginCookie"]["pwd"], true); ;
                    eTracLogin.RememberMe = true;
                    return View(eTracLogin);
                }
                catch
                {
                    return View();
                }
            }
            else
                return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            // ServiceLayer.Services.ScreenPermissionService _ActionAccessPermissionService = new ServiceLayer.Services.ScreenPermissionService();

            //// If we got this far, something failed, redisplay form
            //return View(model);

            if (ModelState.IsValid)
            {
                // ServiceLayer.Services.ResetPasswordService _ResetPasswordService = new ServiceLayer.Services.ResetPasswordService();
                List<Core.Domain.Users> list = _userService.GetUsersByEmail(model.UserName.ToString());
                if (list.Count > 0)
                {
                    Core.Domain.Users _objuser = list.FirstOrDefault();
                    var pass = smartData.Common.CommonFunction.Encrypt(model.Password);
                    Core.Domain.Users user = null;
                    if (_objuser.IsSuperAdmin == true && model.IndustryId == 0)
                    {
                        user = _userService.AuthenticateSuperAdmin(model.UserName, pass);
                    }

                    else
                    {
                        user = _userService.AuthenticateUser(model.UserName, model.IndustryId, pass);
                    }

                    if (user != null)
                    {
                        int cID = WebSecurity.GetUserId(model.UserName);
                        string TokenID = _screenPermissionService.GetAuthorizeToken(Convert.ToInt32(cID));
                        Session["TokenID"] = TokenID;
                        if (_objuser.UserTypeId == 1)
                        {
                            ViewBag.ErrMessage = true;
                            return View();
                        }
                        else
                        {

                            if (Session["TokenID"].ToString() == "")
                            {
                                TokenID = _screenPermissionService.GetAuthorizeToken(Convert.ToInt32(cID));
                                Session["TokenID"] = TokenID;
                            }
                            if (model.RememberMe)
                            {
                                CreateAuthenticateFormsTicket(model);
                            }
                            CreateAuthenticateFormsTicket(model);
                            if (returnUrl != null && returnUrl != "/")
                            {
                                return Redirect(returnUrl);
                            }
                            var ProfileImage = _objuser.Photo == null ? false : true;
                            //Fill session accordin to the user type.
                            SessionManager.FillAdminSession(_objuser.UserId, _objuser.FirstName, _objuser.LastName, _objuser.Email, Convert.ToInt32(_objuser.UserTypeId), ProfileImage, _objuser.IsSuperAdmin, model.IndustryId);
                            GlobalVar.Insudtry = smartData.Common.SessionManager.LoggedInUser.IndustryId;
                            GlobalVar.userType = smartData.Common.SessionManager.LoggedInUser.UserType;
                            ViewData.Add("FullName", list[0].FirstName + " " + list[0].LastName);
                            return RedirectToRoute("AdminDashboardRoute");
                        }
                    }
                    //if (WebSecurity.Login(model.UserName, model.Password))
                    //{

                    //}
                    else
                    {
                        TempData["Message"] = "errorInvalidUser";
                        //ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
                        return View(model);
                    }
                }
                else
                {
                    TempData["Message"] = "errorInvalidUser";
                    //ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
                    return View(model);
                }

            }
            TempData["Message"] = "errorInvalidUser";
            //ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult SuperAdminLogin(LoginViewModel model, string returnUrl)
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            if (ModelState.IsValid)
            {
                // ServiceLayer.Services.ResetPasswordService _ResetPasswordService = new ServiceLayer.Services.ResetPasswordService();
                List<Core.Domain.Users> list = _userService.GetUsersByEmail(model.UserName.ToString());
                if (list.Count > 0)
                {
                    Core.Domain.Users _objuser = list.FirstOrDefault();
                    var pass = smartData.Common.CommonFunction.Encrypt(model.Password);
                    Core.Domain.Users user = null;
                    if (_objuser.IsSuperAdmin == true && model.IndustryId == 0)
                    {
                        user = _userService.AuthenticateSuperAdmin(model.UserName, pass);
                    }

                    else
                    {
                        user = _userService.AuthenticateUser(model.UserName, model.IndustryId, pass);
                    }

                    if (user != null)
                    {
                        int cID = WebSecurity.GetUserId(model.UserName);
                        string TokenID = _screenPermissionService.GetAuthorizeToken(Convert.ToInt32(cID));
                        Session["TokenID"] = TokenID;
                        if (_objuser.UserTypeId == 1)
                        {
                            ViewBag.ErrMessage = true;
                            return View();
                        }
                        else
                        {

                            if (Session["TokenID"].ToString() == "")
                            {
                                TokenID = _screenPermissionService.GetAuthorizeToken(Convert.ToInt32(cID));
                                Session["TokenID"] = TokenID;
                            }
                            if (model.RememberMe)
                            {
                                CreateAuthenticateFormsTicket(model);
                            }
                            CreateAuthenticateFormsTicket(model);
                            if (returnUrl != null && returnUrl != "/")
                            {
                                return Redirect(returnUrl);
                            }
                            var ProfileImage = _objuser.Photo == null ? false : true;
                            //Fill session accordin to the user type.
                            SessionManager.FillAdminSession(_objuser.UserId, _objuser.FirstName, _objuser.LastName, _objuser.Email, Convert.ToInt32(_objuser.UserTypeId), ProfileImage, _objuser.IsSuperAdmin, model.IndustryId);
                            GlobalVar.Insudtry = smartData.Common.SessionManager.LoggedInUser.IndustryId;
                            GlobalVar.userType = smartData.Common.SessionManager.LoggedInUser.UserType;
                            ViewData.Add("FullName", list[0].FirstName + " " + list[0].LastName);

                            // Send email to superadmin
                            string hostName = Dns.GetHostName();
                            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                            #region strbody for email
                            string strBody = string.Empty;
                            strBody = "<body marginwidth='0' marginheight='0' offset='0' topmargin='0' leftmargin='0'>" +
                                "<center>" +
                                "<table id='bodyTable' width='100%' cellspacing='0' cellpadding='0' border='0' align='center' height='100%' style='background-color: #dee0e2;'>" +
                                 "<tbody>" +
                        "<tr>" +
                        "<td id='bodyCell' valign='top' align='center' style='border-top: 4px solid #bbbbbb; padding: 20px;'>" +
                        "<table cellspacing='0' cellpadding='0' border='0' style='border: 1px solid #bbbbbb; width: 600px;'>" +
                         "<tbody>" +
                                // GetHeaderString() +
                                "<tr>" +
                                            "<td valign='top' align='center'>" +
                                                "<table id='templateBody' width='100%' cellspacing='0' cellpadding='0' border='0' style='background-color: #f4f4f4; border-bottom: 1px solid #cccccc;  border-top: 1px solid #ffffff;'>" +
                                                    "<tbody>" +
                                                        "<tr>" +
                                                            "<td class='bodyContent' valign='top' mc:edit='body_content' style='color: #505050;font-family: Helvetica;font-size: 16px;line-height: 150%; padding: 20px; text-align: left;'>" +
                                                                "<h1 style='color: #202020 !important; font-size: 26px; line-height: 100%; margin: 0 0 10px; '>Hi " + list[0].FirstName + ",</h1>" +
                                                                "<br />" +
                            "Someone attempts to login in a superadmin section. " + "<br />" +
                            "With following Ip address: " + myIP +
                                                                "<br>" +
                                                                "<br />" +
                                                                "Thanks," +
                                                                "<br /> <br />" +
                                                                "The " + @System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + " Team" +
                                                           " </td>" +
                                                        "</tr>" +
                                                    "</tbody>" +
                                                "</table>" +
                                           " </td>" +
                                        "</tr>" +
                                // GetFooterString() +
                            "</tbody>" +
                 "</table>" +
                 "</td>" +
                 "</tr>" +
                 "</tbody>" +
                         "</table>" +
                         "</center>" +
                         "</body>";
                            #endregion
                            CommonFunctions.SendEmail("rohitgrover@smartdatainc.net", "Superadmin Login Notification", strBody);
                            return RedirectToRoute("AdminDashboardRoute");

                        }
                    }
                    else
                    {
                        TempData["Message"] = "errorInvalidUser";
                        //ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
                        return View(model);
                    }
                }
                else
                {
                    TempData["Message"] = "errorInvalidUser";
                    //ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
                    return View(model);
                }

            }
            TempData["Message"] = "errorInvalidUser";
            //ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
            return View(model);
        }


        public ActionResult SwitchSuperAdminLogin(string UserName, string Password, string IndustryId)
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            LoginViewModel model = new LoginViewModel();
            model.IndustryId = Convert.ToInt32(IndustryId);
            model.UserName = UserName;
            model.Password = Password;
            model.RememberMe = true;
            if (ModelState.IsValid)
            {
                // ServiceLayer.Services.ResetPasswordService _ResetPasswordService = new ServiceLayer.Services.ResetPasswordService();
                Core.Domain.Users list = _manageAdminService.GetUserByEmailandIndustryId(UserName, model.IndustryId);
                if (list!=null) //list.Count > 0
                {
                    Core.Domain.Users _objuser = list;
                    var pass = list.Password;
                    Core.Domain.Users user = null;
                    if (_objuser.IsSuperAdmin == true && model.IndustryId == 0)
                    {
                        user = _userService.AuthenticateSuperAdmin(UserName, pass);
                    }

                    else
                    {
                        user = _userService.AuthenticateUser(UserName, model.IndustryId, pass);
                    }

                    if (user != null)
                    {
                        int cID = WebSecurity.GetUserId(UserName);
                        string TokenID = _screenPermissionService.GetAuthorizeToken(Convert.ToInt32(cID));
                        Session["TokenID"] = TokenID;
                        if (_objuser.UserTypeId == 1)
                        {
                            ViewBag.ErrMessage = true;
                            return View();
                        }
                        else
                        {

                            if (Session["TokenID"].ToString() == "")
                            {
                                TokenID = _screenPermissionService.GetAuthorizeToken(Convert.ToInt32(cID));
                                Session["TokenID"] = TokenID;
                            }                            
                            //CreateAuthenticateFormsTicket(model);
                            //if (returnUrl != null && returnUrl != "/")
                            //{
                            //    return Redirect(returnUrl);
                            //}
                            var ProfileImage = _objuser.Photo == null ? false : true;
                            //Fill session accordin to the user type.
                            SessionManager.FillAdminSession(_objuser.UserId, _objuser.FirstName, _objuser.LastName, _objuser.Email, Convert.ToInt32(_objuser.UserTypeId), ProfileImage, _objuser.IsSuperAdmin, model.IndustryId);

                            //ViewData.Add("FullName", list[0].FirstName + " " + list[0].LastName);
                            ViewData.Add("FullName", list.FirstName + " " + list.LastName);

                            // Send email to superadmin
                            //string hostName = Dns.GetHostName();
                            //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                            //CommonFunctions.SendEmail("rohitgrover@smartdatainc.net", "Superadmin Login Notification", strBody);
                            //return RedirectToRoute("AdminDashboardRoute");
                            return RedirectToAction("Index", "ManageUser", new { @area = "Admin" });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
                    return View(model);
                }

            }
            ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
            return View(model);
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
                    FormsCookie["IndustryId"] = eTracLogin.IndustryId.ToString();

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

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            //ServiceLayer.Services.ResetPasswordService _ResetPasswordService = new ServiceLayer.Services.ResetPasswordService();
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            if (ModelState.IsValid)
            {
                try
                {
                    //string name = model.Name;
                    string firstname = model.FirstName;
                    string lastname = model.LastName;
                    string[] names = new string[] { };
                    //if (name.Contains(" "))
                    //{
                    //    names = name.Split(' ');
                    //    firstname = names[0];
                    //    lastname = names[1];
                    //}
                    List<Core.Domain.Users> list = _userService.GetUsersByEmail(model.UserName.ToString());
                    int _userID = WebSecurity.GetUserId(model.UserName);
                    if (list.Count == 0 && _userID > 0)
                    {
                        ((SimpleMembershipProvider)Membership.Provider).DeleteUser(model.UserName.ToString(), true); // deletes record from webpages_Membership table
                    }
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, propertyValues: new { FirstName = firstname, LastName = lastname, UserType = 2 });
                    //TODO This Code Use For Mainain Password History
                    string RetPassword = HashData(model.Password);
                    SecUserPasswordHistory _secUserPasswordHistory = new SecUserPasswordHistory();
                    byte[] array = Encoding.ASCII.GetBytes(RetPassword);

                    _secUserPasswordHistory.PasswordHash256 = array;
                    _secUserPasswordHistory.DeleteFlag = false;
                    _secUserPasswordHistory.RowVersion = null;
                    _secUserPasswordHistory.SecUserID = WebSecurity.GetUserId(model.UserName);
                    _userService.AddPasswordHistory(_secUserPasswordHistory);
                    //End

                    ModelState.AddModelError("", CustomMessages.UserRegSuccess);
                    //// Create mail body
                    //StringBuilder mailBody = new StringBuilder();
                    //// mailBody.AppendFormat("Registration Email");
                    //mailBody.AppendFormat("<br />");
                    //mailBody.AppendFormat("Dear {0}", model.UserName);
                    //mailBody.AppendFormat("<br />");
                    //mailBody.AppendFormat("<p>Welcome to Social Media Broadcast</p>");
                    //mailBody.AppendFormat("<p>Now you can post to multiple social media at once.</p>");
                    //mailBody.AppendFormat("<br />");
                    //mailBody.AppendFormat("<br />");
                    //mailBody.AppendFormat("Thanks,");
                    //mailBody.AppendFormat("Social Media Broadcast team");
                    //mailBody.AppendFormat("<br />");
                    //CommonFunctions.SendEmail(model.UserName, "Registration Email", Convert.ToString(mailBody));
                    //TempData["Message"] = "Your account has been registered please login to continue.";
                    TempData["Message"] = true;
                    return RedirectToAction("Login");

                }
                catch (Exception ex)
                {
                    TempData["Message"] = false;
                    ModelState.AddModelError("", "User already exist..");
                }
            }
            TempData["Message"] = false;
            ModelState.AddModelError("", "User already exist..");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        static string HashData(string data)
        {

            SHA256 hasher = SHA256Managed.Create();

            byte[] hashedData = hasher.ComputeHash(

                Encoding.Unicode.GetBytes(data));



            // Now we'll make it into a hexadecimal string for saving

            StringBuilder sb = new StringBuilder(hashedData.Length * 2);

            foreach (byte b in hashedData)
            {

                sb.AppendFormat("{0:x2}", b);

            }

            return sb.ToString();

        }

        public ActionResult LogOut()
        {
            WebSecurity.Logout();
            //return RedirectToAction("Login", "Account");
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            return Redirect("" + baseUrl + "admin/?logoff");
        }

        public ActionResult LogOutSuperAdmin()
        {
            WebSecurity.Logout();
            //return RedirectToAction("SuperAdminLogin", "Account");
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            return Redirect("" + baseUrl + "superadmin/?logoff");
        }

        #region Reset Password
        [AllowAnonymous]
        public ActionResult ResetPasswordAdmin()
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
             var path="";
            if (Request.UrlReferrer != null)
            {
                path = Request.UrlReferrer.AbsolutePath;
            }
      
          ViewBag.Url = path;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPasswordAdmin(ResetPasswordModel model)
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            if (ModelState.IsValid)
            {
                var token = "";
                string UserName = model.UserName;
                //check user existance
               var user = _manageAdminService.GetUserByEmailandIndustryId(UserName, model.IndustryId); // Membership.GetUser(UserName);
                if (user == null)
                {
                    TempData["Message"] = "errorReset";
                    //TempData["Message"] = CustomMessages.UserNotExist;
                    return View();
                }
                else
                {
                    var pass = user.Password;
                    token = user.IndustryId.ToString();
                    //generate password token
                    //token = WebSecurity.GeneratePasswordResetToken(UserName);
                    //create url with above token
                    var url = "";

                    url = ConfigurationManager.AppSettings["SiteUrl"] + "/Admin/Account/ChangePasswordAdmin?token=" + token + "&Username=" + UserName;
                    #region strbody for email
                    string strBody = string.Empty;
                    strBody = "<body marginwidth='0' marginheight='0' offset='0' topmargin='0' leftmargin='0'>" +
                        "<center>" +
                        "<table id='bodyTable' width='100%' cellspacing='0' cellpadding='0' border='0' align='center' height='100%' style='background-color: #dee0e2;'>" +
                         "<tbody>" +
                "<tr>" +
                "<td id='bodyCell' valign='top' align='center' style='border-top: 4px solid #bbbbbb; padding: 20px;'>" +
                "<table cellspacing='0' cellpadding='0' border='0' style='border: 1px solid #bbbbbb; width: 600px;'>" +
                 "<tbody>" +
                        // GetHeaderString() +
                        "<tr>" +
                                    "<td valign='top' align='center'>" +
                                        "<table id='templateBody' width='100%' cellspacing='0' cellpadding='0' border='0' style='background-color: #f4f4f4; border-bottom: 1px solid #cccccc;  border-top: 1px solid #ffffff;'>" +
                                            "<tbody>" +
                                                "<tr>" +
                                                    "<td class='bodyContent' valign='top' mc:edit='body_content' style='color: #505050;font-family: Helvetica;font-size: 16px;line-height: 150%; padding: 20px; text-align: left;'>" +
                                                        "<h1 style='color: #202020 !important; font-size: 26px; line-height: 100%; margin: 0 0 10px; '>Hi " + user.FirstName + ",</h1>" +
                                                        "<br />" +
                    "Social Media Fuel recently received a request for a forgotten password. " + "<br />" +
                     "To change your Social Media Fuel password, please click on this link." + "<br />" +
                                                        "<br>" +
                                                        "<div style='background-color:#9ACD32; height: 50px; width: auto; border-radius:10px'>" +
                                                            "<p style='color:white; text-align:center; padding-top:12px; font-size:20px'><a href='" + url + "'>Reset Password</a></p>" +
                                                        "</div>" +
                                                        "<br />" +
                                                        "If you did not request this change, you do not need to do anything.<br/>" +
                                                         "This link will expire in 2 hours.<br/><br/>" +
                                                        "Regards,<br/>" +                                                       
                                                        "" + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + " Support" +
                                                   " </td>" +
                                                "</tr>" +
                                            "</tbody>" +
                                        "</table>" +
                                   " </td>" +
                                "</tr>" +
                        // GetFooterString() +
                    "</tbody>" +
         "</table>" +
         "</td>" +
         "</tr>" +
         "</tbody>" +
                 "</table>" +
                 "</center>" +
                 "</body>";
                    #endregion
                    CommonFunctions.SendEmail(UserName, "Forgot Password Link", strBody);
                    TempData["Message"] = "successReset";
                    //TempData["Message"] = "The reset password link has been sent to your email.";
                }              
            }
            return RedirectToAction("Login");
        }

        #endregion


        #region Reset Password SuperAdmin
        [AllowAnonymous]
        public ActionResult ResetPasswordSuperAdmin()
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            var path = "";
            if (Request.UrlReferrer != null)
            {
                path = Request.UrlReferrer.AbsolutePath;
            }

            ViewBag.Url = path;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPasswordSuperAdmin(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var token = "";
                string UserName = model.UserName;
                //check user existance
                var user = _manageAdminService.GetUserByEmailandIndustryId(UserName, model.IndustryId); // Membership.GetUser(UserName);
                if (user == null)
                {
                    TempData["Message"] = "errorReset";
                    //TempData["Message"] = CustomMessages.UserNotExist;
                    return View();
                }
                else
                {
                    var pass = user.Password;
                    token = user.IndustryId.ToString();
                    //generate password token
                    //token = WebSecurity.GeneratePasswordResetToken(UserName);
                    //create url with above token
                    var url = "";

                    url = ConfigurationManager.AppSettings["SiteUrl"] + "/Admin/Account/ChangePasswordAdmin?token=" + token + "&Username=" + UserName;
                    #region strbody for email
                    string strBody = string.Empty;
                    strBody = "<body marginwidth='0' marginheight='0' offset='0' topmargin='0' leftmargin='0'>" +
                        "<center>" +
                        "<table id='bodyTable' width='100%' cellspacing='0' cellpadding='0' border='0' align='center' height='100%' style='background-color: #dee0e2;'>" +
                         "<tbody>" +
                "<tr>" +
                "<td id='bodyCell' valign='top' align='center' style='border-top: 4px solid #bbbbbb; padding: 20px;'>" +
                "<table cellspacing='0' cellpadding='0' border='0' style='border: 1px solid #bbbbbb; width: 600px;'>" +
                 "<tbody>" +
                        // GetHeaderString() +
                        "<tr>" +
                                    "<td valign='top' align='center'>" +
                                        "<table id='templateBody' width='100%' cellspacing='0' cellpadding='0' border='0' style='background-color: #f4f4f4; border-bottom: 1px solid #cccccc;  border-top: 1px solid #ffffff;'>" +
                                            "<tbody>" +
                                                "<tr>" +
                                                    "<td class='bodyContent' valign='top' mc:edit='body_content' style='color: #505050;font-family: Helvetica;font-size: 16px;line-height: 150%; padding: 20px; text-align: left;'>" +
                                                        "<h1 style='color: #202020 !important; font-size: 26px; line-height: 100%; margin: 0 0 10px; '>Hi " + user.FirstName + ",</h1>" +
                                                        "<br />" +
                    "Social Media Fuel recently received a request for a forgotten password. " + "<br />" +
                     "To change your Social Media Fuel password, please click on this link." + "<br />" +
                                                        "<br>" +
                                                        "<div style='background-color:#9ACD32; height: 50px; width: auto; border-radius:10px'>" +
                                                            "<p style='color:white; text-align:center; padding-top:12px; font-size:20px'><a href='" + url + "'>Reset Password</a></p>" +
                                                        "</div>" +
                                                        "<br />" +
                                                        "If you did not request this change, you do not need to do anything.<br/>" +
                                                         "This link will expire in 2 hours.<br/><br/>" +
                                                        "Regards,<br/>" +
                                                        "" + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + " Support" +
                                                   " </td>" +
                                                "</tr>" +
                                            "</tbody>" +
                                        "</table>" +
                                   " </td>" +
                                "</tr>" +
                        // GetFooterString() +
                    "</tbody>" +
         "</table>" +
         "</td>" +
         "</tr>" +
         "</tbody>" +
                 "</table>" +
                 "</center>" +
                 "</body>";
                    #endregion
                    CommonFunctions.SendEmail(UserName, "Forgot Password Link", strBody);
                    TempData["Message"] = "successReset";
                    //TempData["Message"] = "The reset password link has been sent to your email.";
                }
            }
            return RedirectToAction("SuperAdminLogin", "Account");
        }

        #endregion




        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordAdmin()
        {
            ModelState.Clear();
            ChangePasswordModel model = new ChangePasswordModel();
            TempData["token"] = Request.QueryString["token"];
            TempData["username"] = Request.QueryString["Username"];
            Core.Domain.Users list = _manageAdminService.GetUserByEmailandIndustryId(TempData["username"].ToString(), Convert.ToInt32(Request.QueryString["token"]));
            var objuser = list;
            if (objuser.LastChangePasswordDate < DateTime.UtcNow)
            {
                TempData["Message"] = "Your forgot password link has been expire.";
                return View("LinkExpire");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordAdmin(ChangePasswordModel model)
        {
            string user = Request.QueryString["Username"];
            string UserName = Convert.ToString(TempData["usernames"]);
            string token = Convert.ToString(TempData["token"]);

            //List<Core.Domain.Users> list = _userService.GetUsersByEmail(model.UserName);
            Core.Domain.Users list = _manageAdminService.GetUserByEmailandIndustryId(model.UserName, model.InustryId);

            ///var user = _manageAdminService.GetUserByEmailandIndustryId(UserName, model.IndustryId);
            //var objuser = list.FirstOrDefault();
            var objuser = list;
            if (objuser.LastChangePasswordDate < DateTime.UtcNow)
            {
                TempData["Message"] = "Your forgot password link has been expire.";
                return RedirectToAction("Login");
            }
            if (objuser != null)
            {
                try
                {
                    objuser.Password = smartData.Common.CommonFunction.Encrypt(model.NewPassword);
                    objuser.ConfirmPassword = smartData.Common.CommonFunction.Encrypt(model.ConfirmPassword);
                    var addAdmin = _userService.UpdateUsers(objuser);
                    TempData["Message"] = CustomMessages.PasswordChanged;
                }
                catch (Exception ex)
                {
                    TempData["Message"] = CustomMessages.ErrorWhileChangingPassword + ex.Message;
                }
            }
            else
            {
                TempData["Message"] = "Email does not Exists";
            }

            //string token = Convert.ToString(TempData["token"]);
            //string UserName = Convert.ToString(TempData["username"]);
            //// string token = "iwoAq_I03EcLHVcQtNHDPA2";
            //// string UserName = "rohitgrover1990@gmail.com";
            //bool any = _userService.UpdatePassword(UserName, token);
            //bool response = false;
            //if (any == true)
            //{
            //    response = WebSecurity.ResetPassword(token, model.NewPassword);
            //    if (response == true)
            //    {
            //        try
            //        {
            //            //  Here Maintain Password History
            //            //  MembershipUser u = Membership.GetUser(WebSecurity.CurrentUserName, false);

            //            string RetPassword = HashData(model.NewPassword);
            //            SecUserPasswordHistory _secUserPasswordHistory = new SecUserPasswordHistory();
            //            byte[] array = Encoding.ASCII.GetBytes(RetPassword);

            //            _secUserPasswordHistory.PasswordHash256 = array;
            //            _secUserPasswordHistory.DeleteFlag = false;
            //            _secUserPasswordHistory.RowVersion = null;
            //            _secUserPasswordHistory.SecUserID = (WebSecurity.CurrentUserId);
            //            _userService.AddPasswordHistory(_secUserPasswordHistory);
            //            TempData["Message"] = CustomMessages.PasswordChanged;
            //            // return Content(CustomMessages.PasswordChanged);
            //        }
            //        catch (Exception ex)
            //        {
            //            TempData["Message"] = CustomMessages.ErrorWhileChangingPassword + ex.Message;
            //        }
            //    }
            //    else
            //    {
            //        TempData["Message"] = CustomMessages.HeyAvoidRandomRequest;
            //    }
            //}
            //else
            //{
            //    TempData["Message"] = CustomMessages.UserAndTokenNotMatch;
            //}
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult GetUserById(int id)
        {
            Core.Domain.Users Users = _userService.GetUserById(id);
            return Json(Users, JsonRequestBehavior.AllowGet);  
        }

        [HttpGet]
        public string GetUserByEmail(string email, int id)
        {
            List<Core.Domain.Users> Users = _userService.GetUsersByEmail(email);
            Core.Domain.Users _objuser = Users.FirstOrDefault();
            if (_objuser != null)
            {
                if (_objuser.UserId == id)
                {
                    return "";
                }
                else
                {
                    return "exists";
                }
            }
            else
            {
                return "";
            }           
        }
    }
}