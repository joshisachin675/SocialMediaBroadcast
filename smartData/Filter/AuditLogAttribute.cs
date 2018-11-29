using Core.Domain;
using smartData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AppInterfaces.Infrastructure;
using RepositoryLayer;

namespace smartData.Filter
{
    public class AuditLogAttribute : ActionFilterAttribute
    {
        public string Event { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Check the user activity
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           int Created = 0;
            int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            if (ImpersonateUserId != 0)
            {
                Created = ImpersonateUserId;
            }
            else
            {
                Created = SessionManager.LoggedInUser.UserID;
            }
            //Stores the Request in an Accessible object
            var request = filterContext.HttpContext.Request;
            string hostName = Dns.GetHostName();
            string myIP = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                     HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
            // Generate an Audit
            smActivityLog activity = new smActivityLog()
            {
                UserId = SessionManager.LoggedInUser.UserID,
                IpAddress = myIP,
                UserName = (request.IsAuthenticated) ?
                          filterContext.HttpContext.User.Identity.Name : "Anonymous",
                //The URL that was accessed
                AreaAccessed = request.RawUrl,
                //Creates our Timestamp
                TimeStamp = DateTime.UtcNow,
                Event = Event,
                Message = Message,
                CreatedBy = Created
            };

            AzureDBContext db = new AzureDBContext();
            db.smActivityLogs.Add(activity);
            db.SaveChanges();

            base.OnActionExecuting(filterContext);
        }
    }
}