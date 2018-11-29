using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using smartData.Filter;
using smartData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;
using Core.Domain;
using CoreEntities.Helper;

namespace smartData.Areas.Admin.Controllers
{
    [CheckSession]
    [AuthorizedRoles(Roles = "SuperAdmin,Admin")]
    public class ManageUserController : Controller
    {
        #region Global Variables
        IManageUserService _manageUserService;
        IManageUserAPIController _manageUserAPIController;
        IManageContentAPIController _manageContentApi;
        IManageContentService _manageContentService;
        IManageCategoryService _manageCategoryService;
        IUserService _userService;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        #endregion

        #region constructor
        public ManageUserController(ManageUserService manageUserService, IManageUserAPIController manageUserAPIController, ManageContentService manageContentService, IManageContentAPIController manageContentAPIController, IUserService userService, IManageCategoryService manageCategoryService)
        {
            _manageUserService = manageUserService;
            _manageUserAPIController = manageUserAPIController;
            _manageContentApi = manageContentAPIController;
            _manageContentService = manageContentService;
            _userService = userService;
            _manageCategoryService = manageCategoryService;

        }
        #endregion

        //
        // GET: /Admin/ManageUser/
        public ActionResult Index()
        {
            if (smartData.Common.SessionManager.LoggedInUser.UserType == 3)
            {
                List<smIndustry> newlist = _manageContentApi.GetCategories();
                ViewBag.IndustryName = new SelectList(newlist, "IndustryId", "IndustryName");
            }
            else
            {
                smIndustry newlist = _manageContentService.GetIndustryById(smartData.Common.SessionManager.LoggedInUser.IndustryId);
                ViewBag.IndustryId = newlist.IndustryId.ToString();
                ViewBag.IndustryName = newlist.IndustryName.ToString();
            }

            return View();
        }

        /// <summary>
        /// Delete the User admin by superadmin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Delete user account", Message = "Delete user account")]
        public JsonResult DeleteUserAccount(int id)
        {
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageUserAPIController.DeleteUserAccount(id, userId);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Update user status", Message = "Update user status to active")]
        public JsonResult UpdateUserStatusActive(int id)
        {
            bool stat = true;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageUserAPIController.UpdateUserStatus(id, userId, stat);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Update user status", Message = "Update user status to deactive")]
        public JsonResult UpdateUserStatusDeactive(int id)
        {
            bool stat = false;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            ////userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageUserAPIController.UpdateUserStatus(id, userId, stat);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create the User admin by superadmin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AuditLog(Event = "Create admin account", Message = "Create admin by superadmin")]
        public JsonResult CreateAdmin(AdminRegisterViewModel model)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    if (smartData.Common.SessionManager.LoggedInUser.UserType == 2)
                    {
                        model.IndustryName = smartData.Common.SessionManager.LoggedInUser.IndustryId;
                    }                   

                    List<Core.Domain.Users> list = _manageUserAPIController.GetUsersByEmail(model.UserName.ToString());
                    var industry = _manageCategoryService.GetIndustryById(Convert.ToInt32(model.IndustryName));
                    Core.Domain.Users userobj = new Core.Domain.Users();
                    userobj.FirstName = model.FirstName;
                    userobj.LastName = model.LastName;
                    userobj.IndustryId = Convert.ToInt32(model.IndustryName);
                    userobj.IndustryName = industry.IndustryName;
                    userobj.IsDeleted = false;
                    if (smartData.Common.SessionManager.LoggedInUser.UserType == 2)
                    {
                        userobj.UserTypeId = 1;//admin
                    }
                    else
                    {
                        userobj.UserTypeId = model.UserType;
                    }
                    string pass = "";
                    Random random = new Random();
                    int length = 8;
                    for (int i = 0; i < length; i++)
                    {
                        if (random.Next(0, 3) == 0)                     //if random.Next() == 0 then we generate a random character
                        {
                            pass += ((char)random.Next(65, 91)).ToString();
                        }
                        else                                            //if random.Next() == 0 then we generate a random digit
                        {
                            pass += random.Next(0, 9);
                        }
                    }


                    userobj.Password = smartData.Common.CommonFunction.Encrypt(pass);
                    userobj.ConfirmPassword = smartData.Common.CommonFunction.Encrypt(pass);
                    userobj.Active = true;
                    userobj.CreatedBy = smartData.Common.SessionManager.LoggedInUser.UserID;
                    userobj.CreatedDate = DateTime.UtcNow;
                    userobj.Email = model.UserName;
                    var addAdmin = _userService.AddUser(userobj);
                    var Role="";
                    if(userobj.UserTypeId==1)
                    {
                        Role="User";
                    }
                    else
                    {
                          Role="Admin";
                    }
                    #region strbody for email
                    var ReturnUrl = "";
                    if (model.UserType==1)
                    {
                         ReturnUrl = "http://sm4y.cc/" + addAdmin.IndustryName + "/login";    
                    }
                  else
                    {
                         ReturnUrl = "http://sm4y.cc/admin";
                    }

                    
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
                                                        "<h1 style='color: #202020 !important; font-size: 26px; line-height: 100%; margin: 0 0 10px; '>" + model.FirstName + " " + model.LastName + ",</h1>" +
                                                        "<br />" +
                                      "Your " + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + " account was setup successfully. " + "<br />" +
                                       "To access your account go to <a href='" + ReturnUrl + "'>" + ReturnUrl + "</a> and use the following credentials: " + "<br /><br />" +
                                         "<strong>Username :</strong> " + "  " + "" + model.UserName + "<br />" +
                                           "<strong>Password :</strong> " + "  " + "" +pass + "<br /><br/>" +
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
                    CommonFunctions.SendEmail(model.UserName, "Welcome to " + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + "", strBody);

                    //int _userID = WebSecurity.GetUserId(model.UserName);                   
                    //   if (list.Count == 0 && _userID > 0)
                    //{
                    //    ((SimpleMembershipProvider)Membership.Provider).DeleteUser(model.UserName.ToString(), true); // deletes record from webpages_Membership table
                    //}
                    //WebSecurity.CreateUserAndAccount(model.UserName, model.Password, propertyValues: new { FirstName = model.FirstName, LastName = model.LastName, UserType = 2,    IndustryName = model.IndustryName });
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
        /// Update the admin password by superadmin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AuditLog(Event = "Update password", Message = "Update password")]
        public JsonResult UpdateAdminPassword(AdminEditPasswordModel model)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    string password = smartData.Common.CommonFunction.Encrypt(model.Password);
                    var industry = _manageContentService.GetIndustryById(model.IndustryName);
                    status = _manageUserAPIController.UpdateUserPassword(model.UserId, model.FirstName,model.LastName,model.UserName,industry.IndustryId,industry.IndustryName, password);
                }
                catch (Exception ex)
                {
                    status = false;
                }
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Update the admin password by superadmin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AuditLog(Event = "Show password", Message = "Show password")]
        public JsonResult GetPassword(int id)
        {
            bool status = false;
            string password = string.Empty;
            try
            {
                Core.Domain.Users user = _manageUserAPIController.GetPassword(id);
                password = smartData.Common.CommonFunction.Decrypt(user.Password);
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(new { status = status, password = password }, JsonRequestBehavior.AllowGet);
        }
    }
}