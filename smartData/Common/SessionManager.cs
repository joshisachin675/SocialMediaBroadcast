using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreEntities.CustomModels;

namespace smartData.Common
{
    public class SessionManager
    {
        public static LoggedInUserDetails LoggedInUser
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                if (HttpContext.Current.Session["LoggedInUser"] == null)
                {
                    HttpContext.Current.Session["LoggedInUser"] = new LoggedInUserDetails();
                }
                return (LoggedInUserDetails)HttpContext.Current.Session["LoggedInUser"];
            }
            set { HttpContext.Current.Session["LoggedInUser"] = value; }
        }

        /// <summary>
        /// Session of the users
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="fName"></param>
        /// <param name="lname"></param>
        /// <param name="email"></param>
        /// <param name="usertype"></param>
        /// <param name="roleId"></param>
        /// <param name="clinicId"></param>
        /// <param name="hasProfileImage"></param>
        public static void FillSession(int userid, string fName, string lname, string email, int usertype, bool hasProfileImage,int industryId,int currentUser)
        {
            SessionManager.LoggedInUser.UserID = userid;
            SessionManager.LoggedInUser.FirstName = fName;
            SessionManager.LoggedInUser.LastName = lname;
            SessionManager.LoggedInUser.Email = email;
            SessionManager.LoggedInUser.UserType = usertype;
           // SessionManager.LoggedInUser.UserRole = roleId;
           // SessionManager.LoggedInUser.ClinicId = clinicId;
            SessionManager.LoggedInUser.ProfileImage = hasProfileImage;
            SessionManager.LoggedInUser.IndustryId = industryId;
            SessionManager.LoggedInUser.ImpersonateUserId = currentUser;
            
        }

        /// <summary>
        /// Session of the admin of the site(Vendor)
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="fName"></param>
        /// <param name="lname"></param>
        /// <param name="email"></param>
        /// <param name="usertype"></param>
        /// <param name="hasProfileImage"></param>
        /// <param name="isSuperAdmin"></param>
        public static void FillAdminSession(int userid, string fName, string lname, string email, int usertype, bool ProfileImage, bool isSuperAdmin,int industryId)
        {

            SessionManager.LoggedInUser.UserID = userid;
            SessionManager.LoggedInUser.FirstName = fName;
            SessionManager.LoggedInUser.LastName = lname;
            SessionManager.LoggedInUser.Email = email;
            SessionManager.LoggedInUser.UserType = usertype;
            SessionManager.LoggedInUser.ProfileImage = ProfileImage;
            SessionManager.LoggedInUser.IsSuperAdmin = isSuperAdmin;
            SessionManager.LoggedInUser.IndustryId = industryId;
        }
    }
}