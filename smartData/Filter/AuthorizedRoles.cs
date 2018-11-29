using CoreEntities.enums;
using smartData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace smartData.Filter
{
    sealed class AuthorizedRoles : System.Web.Mvc.ActionFilterAttribute
    {
        public string Roles { get; set; }
        /// <summary>
        /// Authorization filter on the basis of roles
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = false;
            string[] roles = Roles.Split(',');
            var currentUserRoleId = SessionManager.LoggedInUser.UserType;
            var currentRoleName = "";
            switch (currentUserRoleId)
            {
                case 1:
                    currentRoleName = UserTypes.User.ToString();
                    break;
                case 2:
                    currentRoleName = UserTypes.Admin.ToString();
                    break;
                case 3:
                    currentRoleName = UserTypes.SuperAdmin.ToString();
                    break;
                default:
                    break;
            }
            //current user role is empty means unauthorized or its a sitevendor as role is 0 of the logged in user in the session.
            if (currentRoleName != "")
                foreach (var role in roles)
                {

                    if (role == "SuperAdmin" && currentRoleName == "Admin")
                    {
                        status = false;
                    }
                    else if (role.Contains(currentRoleName))
                    {
                        status = true;
                    }

                }

            if (status == false)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "NoPermissionAjaxRequest",
                        area = ""
                    }));
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "Logout",
                        area = "Users"


                        //     controller = "External",
                        //action = "NoPermission",
                    }));
                }

            }

            base.OnActionExecuting(filterContext);
        }

    }
}