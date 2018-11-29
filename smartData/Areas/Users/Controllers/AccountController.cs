using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using smartData.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using smartData.Common;
using Core.Domain;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;
using System.Security.Cryptography;
using System.Text;
using ServiceLayer.Services;
using smartData.Messages;
using ServiceLayer.Interfaces;
using ServiceLayer.Email;
using CoreEntities.Helper;
using System.Configuration;
using CoreEntities.CustomModels;
using System.Linq;
using smartData.Filter;
using smartData.Areas.Admin.Controllers;
using System.IO;


namespace smartData.Areas.Users.Controllers
{


    [Authorize]
    //[AuthorizedRoles(Roles = "Admin")]
    public class AccountController : Controller
    {
        private LoggedInUserDetails _LoggedDetail = null;
        IEmailService _emailService;
        IUserService _userService;
        IManageContentService _manageContentService;
        IManageAdminService _manageAdminService;
        IManageAdminAPIController _manageAdminAPIController;
        IManageCategoryService _manageCategoryService;
        IScreenPermissionService _screenPermissionService;
        public AccountController(IScreenPermissionService screenPermissionService, IEmailService emailService, IUserService userService, IManageContentService manageContentService, IManageAdminService manageAdminService, IManageAdminAPIController manageAdminAPIController, IManageCategoryService manageCategoryService)
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())),
            new EmailService(new Config(), new HtmlMessageFormatter(new Config())
                , new EmailMessageSender(new Config())), screenPermissionService, userService, manageContentService, manageAdminService, manageAdminAPIController, manageCategoryService)
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager, IEmailService emailService, IScreenPermissionService screenPermissionService, IUserService userService, IManageContentService manageContentService, IManageAdminService manageAdminService, IManageAdminAPIController manageAdminAPIController, IManageCategoryService manageCategoryService)
        {
            _emailService = emailService;
            _userService = userService;
            UserManager = userManager;
            _screenPermissionService = screenPermissionService;
            _manageContentService = manageContentService;
            _manageAdminService = manageAdminService;
            _manageAdminAPIController = manageAdminAPIController;
            _manageCategoryService = manageCategoryService;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //   
        // GET: /Account/Login
        [AllowAnonymous]

        public ActionResult Login(string realtor)
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            TempData["IndustryId"] = null;
            if (realtor != null && realtor != "Account")
            {
                ViewBag.industry = "true";
                ViewBag.Realtor = realtor;
                var industryid = _manageCategoryService.GetIndustryByShortName(realtor);
                if (industryid != null)
                {
                    TempData["IndustryId"] = industryid.IndustryId;
                }
            }
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            //ViewBag.ReturnUrl = returnUrl;
            LoginViewModel eTracLogin = new LoginViewModel();
            if (Request.Cookies["LoginCookie"] != null)
            {
                try
                {
                    eTracLogin.UserName = Request.Cookies["LoginCookie"]["UserName"];
                    eTracLogin.Password = Request.Cookies["LoginCookie"]["pwd"];
                    //Cryptography.GetDecryptedData(Request.Cookies["LoginCookie"]["pwd"], true); ;
                    eTracLogin.IndustryId = Convert.ToInt32(Request.Cookies["LoginCookie"]["Industry"]);
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

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            Session["Temp"] = true;
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            int currentUser = 0;
            int indus = Convert.ToInt32(TempData["IndustryId"]);
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
                    if (_objuser.Active == false)
                    {

                        TempData["Message"] = "DeactivatedUser";
                        //ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);
                        return View(model);
                    }

                    var pass = smartData.Common.CommonFunction.Encrypt(model.Password);
                    var user = new Core.Domain.Users();
                    if (indus != null && indus != 0)
                    {
                        user = _userService.AuthenticateUser(model.UserName, indus, pass);
                    }
                    else
                    {
                        user = _userService.AuthenticateUser(model.UserName, model.IndustryId, pass);
                    }



                    //var user = _userService.AuthenticateUser(model.UserName, model.IndustryId, pass);

                    if (user != null && user.UserTypeId == 1)
                    {
                        int cID = WebSecurity.GetUserId(model.UserName);
                        //string TokenID = _screenPermissionService.GetAuthorizeToken(Convert.ToInt32(cID));
                        //Session["TokenID"] = TokenID;
                        //if (Session["TokenID"].ToString() == "")
                        //{
                        //    TokenID = _screenPermissionService.GetAuthorizeToken(Convert.ToInt32(cID));
                        //    Session["TokenID"] = TokenID;
                        //}
                        if (model.RememberMe)
                        {
                            CreateAuthenticateFormsTicket(model);
                        }
                        CreateAuthenticateFormsTicket(model);
                        if (returnUrl != null && returnUrl != "/")
                        {
                            return Redirect(returnUrl);
                        }
                        var HasProfileImage = _objuser.Photo == null ? false : true;
                        // Fill User Session
                        SessionManager.FillSession(_objuser.UserId, _objuser.FirstName, _objuser.LastName, _objuser.Email, Convert.ToInt32(_objuser.UserTypeId), HasProfileImage, user.IndustryId, currentUser);
                        ViewData.Add("FullName", list[0].FirstName + " " + list[0].LastName);
                        GlobalVar.Insudtry = smartData.Common.SessionManager.LoggedInUser.IndustryId;
                        GlobalVar.userType = smartData.Common.SessionManager.LoggedInUser.UserType;
                        if (!user.AcceptTerms.HasValue)
                        {
                            return RedirectToAction("TermsConditions");
                        }

                        //return RedirectToRoute("UserDashboardRoute");
                        return RedirectToAction("Index", "Home", new { @Area = "Users" });
                    }
                    //if (WebSecurity.Login(model.UserName, model.Password))
                    //{

                    //}
                    else
                    {
                        TempData["Message"] = "errorInvalidUser";
                        //ModelState.AddModelError("", CustomMessages.InvalidUserOrPass);

                        int IndustryId = model.IndustryId;
                        if (model.IndustryId == 0)
                        {
                            string IndustryName = newlist.Where(x => x.IndustryId == indus).Select(x => x.IndustryShortName).FirstOrDefault();
                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                            return Redirect("" + baseUrl + IndustryName + "/login/");

                        }
                        else
                        {
                            return View(model);

                        }






                        // return View(model);
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

        [AllowAnonymous]
        public ActionResult TermsConditions()
        {

            return View();
        }


        [AllowAnonymous]

        public ActionResult AcceptConditions(bool AcceptTerms)
        {
            //SessionManager.LoggedInUser.UserID;
            string myIP = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
            _userService.SaveTermsConditions(SessionManager.LoggedInUser.UserID, myIP, AcceptTerms);

            return RedirectToAction("Index", "Home", new { @Area = "Users" });
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
                    FormsCookie["Industry"] = eTracLogin.IndustryId.ToString();

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
        public ActionResult Register(string realtor)
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();


            if (realtor != null && realtor != "Account")
            {
                ViewBag.industry = "true";
                ViewBag.Realtor = realtor;
                var industryid = _manageCategoryService.GetIndustryByShortName(realtor);
                if (industryid != null)
                {
                    TempData["IndustryId"] = industryid.IndustryId;
                }
            }
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }

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
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            //ServiceLayer.Services.ResetPasswordService _ResetPasswordService = new ServiceLayer.Services.ResetPasswordService();
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    int indus = Convert.ToInt32(TempData["IndustryId"]);
                    // string name = model.Name;
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

                    smIndustry industry;
                    Core.Domain.Users userobj = new Core.Domain.Users();
                    userobj.DisplayName = string.Empty;
                    userobj.Title = string.Empty;
                    userobj.CompanyName = string.Empty;
                    userobj.FirstName = model.FirstName;
                    userobj.LastName = model.LastName;
                    if (indus != null && indus != 0)
                    {
                        userobj.IndustryId = indus;
                        industry = _manageCategoryService.GetIndustryById(indus);
                    }
                    else
                    {
                        userobj.IndustryId = Convert.ToInt32(model.IndustryName);
                        industry = _manageCategoryService.GetIndustryById(model.IndustryName);
                    }


                    userobj.IndustryName = industry.IndustryName;
                    userobj.IsDeleted = false;
                    userobj.UserTypeId = 1;//user
                    userobj.Password = smartData.Common.CommonFunction.Encrypt(model.Password);
                    userobj.ConfirmPassword = smartData.Common.CommonFunction.Encrypt(model.ConfirmPassword);
                    userobj.Active = true;
                    userobj.CreatedBy = smartData.Common.SessionManager.LoggedInUser.UserID;
                    userobj.CreatedDate = DateTime.UtcNow;
                    userobj.Email = model.UserName;
                    userobj.Shortname = model.ShortName;
                    var addAdmin = _userService.AddUser(userobj);



                    //int _userID = WebSecurity.GetUserId(model.UserName);
                    //if (list.Count == 0 && _userID > 0)
                    //{
                    //    ((SimpleMembershipProvider)Membership.Provider).DeleteUser(model.UserName.ToString(), true); // deletes record from webpages_Membership table
                    //}
                    //WebSecurity.CreateUserAndAccount(model.UserName, model.Password, propertyValues: new { FirstName = firstname, LastName = lastname, UserType = 1, Industry = model.IndustryName });
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
                    // Create mail body


                    // var  urlLInked = 
                    #region strbody for email
                   // url = ConfigurationManager.AppSettings["SiteUrl"] + "/Account/ChangePassword?token=" + token + "&Username=" + UserName;
                    var ReturnUrl = "" + ConfigurationManager.AppSettings["SiteUrl"] + "/" + addAdmin.IndustryName + "/login";
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
                                                        "<h1 style='color: #202020 !important; font-size: 26px; line-height: 100%; margin: 0 0 10px; '> " + model.FirstName + " " + model.LastName + ",</h1>" +
                                                        "<br />" +
                                      "Your " + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + " account was setup successfully. " + "<br />" +
                                       "To access your account go to <a href='" + ReturnUrl + "'>" + ReturnUrl + "</a> and use the following credentials: " + "<br /><br />" +
                                         "<strong>Username :</strong> " + "  " + "" + model.UserName + "<br />" +
                                           "<strong>Password :</strong> " + "  " + "" + model.Password + "<br /><br/>" +
                                     "Thank you for your business.<br />" +
                                       "We look forward to connect your Social Media audience with valuable information.<br />" +
                                                         "<br />" +
                                                        "Regards," +
                                                        "<br />" +
                                                        "" + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + " Team" +
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
                    CommonFunctions.SendEmail(model.UserName, "Welcome to " + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + " ", strBody);
                    //TempData["Message"] = "Your account has been registered please login to continue.";
                    TempData["Message"] = "success";
                    //return RedirectToAction("Login");

                    if (indus != null && indus != 0)
                    {
                        string url = string.Format("~/{0}/login", industry.IndustryShortName);
                        return Redirect(url);

                    }
                    else
                    {

                        return RedirectToAction("Login", "Account", new { @Area = "Users" });
                    }



                    status = true;

                }
                catch (Exception ex)
                {
                    TempData["Message"] = "error";
                    ModelState.AddModelError("", "User already exist..");
                    status = false;
                    return View();
                }
            }
            TempData["Message"] = "error";
            ModelState.AddModelError("", "User already exist..");
            // If we got this far, something failed, redisplay form
            //return Json(new { status = status }, JsonRequestBehavior.AllowGet);
            return View();
        }


        //
        // Logout
        [AuditLog(Event = "LogOut", Message = "User LogOut")]
        public ActionResult LogOut()
        {

            int IndustryId = GlobalVar.Insudtry;
            List<smIndustry> newlist = _manageContentService.GetCategories();
            string IndustryName = newlist.Where(x => x.IndustryId == IndustryId).Select(x => x.IndustryShortName).FirstOrDefault();
            WebSecurity.Logout();
            //  return RedirectToAction("Login", IndustryName);
            //  Response.Redirect("http://localhost:55038/realestate/Login?LogOff");
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            if (GlobalVar.userType == 1)
            {
                return Redirect("" + baseUrl + IndustryName + "/login/?logoff");
            }
            else if (GlobalVar.userType == 2)
            {
                return Redirect("" + baseUrl + "admin/?logoff");
            }
            else if (GlobalVar.userType == 3)
            {
                return Redirect("" + baseUrl + "superadmin/?logoff");
            }
            else
            {
                return Redirect("" + baseUrl + "login/?logoff");
            }


        }

        //
        // POST: /Account/Disassociate
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        //{
        //    ManageMessageId? message = null;
        //    IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
        //    if (result.Succeeded)
        //    {
        //        message = ManageMessageId.RemoveLoginSuccess;
        //    }
        //    else
        //    {
        //        message = ManageMessageId.Error;
        //    }
        //    return RedirectToAction("Manage", new { Message = message });
        //}

        //
        // GET: /Account/Manage
        //public ActionResult Manage(ManageMessageId? message)
        //{
        //    ViewBag.StatusMessage =
        //        message == ManageMessageId.ChangePasswordSuccess ? CustomMessages.YourPassHasBeenChanged
        //        : message == ManageMessageId.SetPasswordSuccess ? CustomMessages.YourPassHasBeenSet
        //        : message == ManageMessageId.RemoveLoginSuccess ? CustomMessages.ExtLoginWasRemoved
        //        : message == ManageMessageId.Error ? CustomMessages.ErrorOccured
        //        : "";
        //    ViewBag.HasLocalPassword = HasPassword();
        //    ViewBag.ReturnUrl = Url.Action("Manage");
        //    return View();
        //}

        //
        // POST: /Account/Manage
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Manage(ManageUserViewModel model)
        //{

        //    bool hasPassword = HasPassword();
        //    ViewBag.HasLocalPassword = hasPassword;
        //    ViewBag.ReturnUrl = Url.Action("Manage");
        //    if (hasPassword)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
        //            }
        //            else
        //            {
        //                AddErrors(result);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // User does not have a password so remove any validation errors caused by a missing OldPassword field
        //        ModelState state = ModelState["OldPassword"];
        //        if (state != null)
        //        {
        //            state.Errors.Clear();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
        //            }
        //            else
        //            {
        //                AddErrors(result);
        //            }
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        //
        // GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var user = await UserManager.FindAsync(loginInfo.Login);
        //    if (user != null)
        //    {
        //        await SignInAsync(user, isPersistent: false);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    else
        //    {
        //        // If the user does not have an account, then prompt the user to create an account
        //        ViewBag.ReturnUrl = returnUrl;
        //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
        //    }
        //}

        //
        // POST: /Account/LinkLogin
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LinkLogin(string provider)
        //{
        //    // Request a redirect to the external login provider to link a login for the current user
        //    return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        //}

        //
        // GET: /Account/LinkLoginCallback
        //public async Task<ActionResult> LinkLoginCallback()
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //    }
        //    var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Manage");
        //    }
        //    return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //}

        //
        // POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser() { UserName = model.UserName };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInAsync(user, isPersistent: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut();
        //    return RedirectToAction("Index", "Home");
        //}

        //
        // GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        //[ChildActionOnly]
        //public ActionResult RemoveAccountList()
        //{
        //    var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
        //    ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
        //    return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && UserManager != null)
        //    {
        //        UserManager.Dispose();
        //        UserManager = null;
        //    }
        //    base.Dispose(disposing);
        //}

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            // var user = WebSecurity.GetUserId(User.Identity.GetUserName());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion



        #region Login Verification

        /// <summary>
        /// Create cookie for authentication
        /// </summary>
        /// <param name="model"></param>
        private void CreateAuthorizedCookiesFronEndUser(Core.Domain.Users model)
        {

            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.UserId = model.UserId;
            serializeModel.FirstName = model.FirstName;
            serializeModel.Email = model.Email;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //serialize user data and add to cookie
            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1,
                model.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false, userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);

            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

        /// <summary>
        /// LogOut User
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        #endregion

        #region ForgotPassword
        // [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ManageUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OldPassword != model.NewPassword)
                {
                    //ServiceLayer.Services.ResetPasswordService _ResetPasswordService = new ServiceLayer.Services.ResetPasswordService();
                    var token = "";
                    string UserName = WebSecurity.CurrentUserName;
                    //check user existance


                    var user = Membership.GetUser(UserName);

                    bool changePasswordSucceeded;
                    changePasswordSucceeded = user.ChangePassword(model.OldPassword, model.NewPassword);

                    if (!changePasswordSucceeded)
                    {
                        return Content(CustomMessages.CurrentPassNotCorrect);
                    }

                    if (user == null)
                    {
                        TempData["Message"] = CustomMessages.UserNotExist;
                    }
                    else
                    {
                        //generate password token
                        token = WebSecurity.GeneratePasswordResetToken(UserName);
                        //create url with above token
                    }
                    bool any = _userService.UpdatePassword(UserName, token);
                    bool response = false;
                    if (any == true)
                    {
                        response = WebSecurity.ResetPassword(token, model.NewPassword);
                        if (response == true)
                        {
                            try
                            {
                                //  Here Maintain Password History
                                //  MembershipUser u = Membership.GetUser(WebSecurity.CurrentUserName, false);

                                string RetPassword = HashData(model.NewPassword);
                                SecUserPasswordHistory _secUserPasswordHistory = new SecUserPasswordHistory();
                                byte[] array = Encoding.ASCII.GetBytes(RetPassword);

                                _secUserPasswordHistory.PasswordHash256 = array;
                                _secUserPasswordHistory.DeleteFlag = false;
                                _secUserPasswordHistory.RowVersion = null;
                                _secUserPasswordHistory.SecUserID = (WebSecurity.CurrentUserId);
                                _userService.AddPasswordHistory(_secUserPasswordHistory);
                                //TempData["Message"] = CustomMessages.PasswordChanged;
                                return Content(CustomMessages.PasswordChanged);
                            }
                            catch (Exception ex)
                            {
                                TempData["Message"] = CustomMessages.ErrorWhileChangingPassword + ex.Message;
                            }
                        }
                        else
                        {
                            TempData["Message"] = CustomMessages.HeyAvoidRandomRequest;
                        }
                    }
                    else
                    {
                        TempData["Message"] = CustomMessages.UserAndTokenNotMatch;
                    }

                }
                else
                {
                    return Content(CustomMessages.PasswordsMustbeDiff);
                }

            }
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

        #endregion

        #region Reset Password
        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            List<smIndustry> newlist = _manageContentService.GetCategories();
            ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            if (ModelState.IsValid)
            {
                var token = "";
                string UserName = model.UserName;
                var user = _manageAdminService.GetUserByEmailandIndustryId(UserName, model.IndustryId); // Membership.GetUser(UserName);
                if (user == null)
                {
                    TempData["Message"] = "errorReset";
                    return View();
                }

                else
                {
                    var pass = user.Password;
                    token = user.IndustryId.ToString();
                    //generate password token
                    // token = WebSecurity.GeneratePasswordResetToken(UserName);
                    //create url with above token
                    var url = "";

                    url = ConfigurationManager.AppSettings["SiteUrl"] + "/Account/ChangePassword?token=" + token + "&Username=" + UserName;
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
                                                        "Regards," +                                                       
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

                    var SuperAdminEmail = _manageAdminService.SetSuperAdminEmail();
                    CommonFunctions.SendEmail(UserName, "Forgot Password Link", strBody, SuperAdminEmail);
                    TempData["Message"] = "successReset";
                    //TempData["Message"] = "The reset password link has been sent to your email.";
                }
            }
            return RedirectToAction("Login");
        }

        #endregion


        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public ActionResult ChangePassword()
        {
            ModelState.Clear();
            ChangePasswordModel model = new ChangePasswordModel();
            TempData["token"] = Request.QueryString["token"];
            TempData["username"] = Request.QueryString["Username"];
            Core.Domain.Users list = _manageAdminService.GetUserByEmailandIndustryId(TempData["username"].ToString(), Convert.ToInt32(TempData["token"]));// _userService.GetUsersByEmail(model.UserName);
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
        [AuditLog]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            string user = Request.QueryString["Username"];
            string UserName = Convert.ToString(TempData["usernames"]);
            string token = Convert.ToString(TempData["token"]);

            Core.Domain.Users list = _manageAdminService.GetUserByEmailandIndustryId(model.UserName, Convert.ToInt32(token));// _userService.GetUsersByEmail(model.UserName);
            var objuser = list;
            if (objuser.LastChangePasswordDate  < DateTime.UtcNow)
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
            //           // return Content(CustomMessages.PasswordChanged);
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


        //[HttpGet]
        //public string GetUserByEmailandIndustryInAccount(string email, string industry)
        //{
        //    var user = _manageAdminAPIController.GetUserByEmailandIndustry(email, industry);
        //    if (user != null)
        //    {
        //        return "exists";
        //    }
        //    else
        //    {
        //        return "not exists";
        //    }
        //}
        public ActionResult KeepAlive()
        {
            Session["Temp"] = true;
            return Json(new { status = "status" }, JsonRequestBehavior.AllowGet);


        }
    }
}