using smartData.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace smartData.Areas.Users.Controllers
{
    public class ExternalController : Controller
    {
        //
        // GET: /Users/External/
        public ActionResult Index()
        {
            return View();
        }

        #region No Permission
        public ActionResult NoPermission()
        {
            ViewBag.Message = CustomMessages.NotAuthorizedToView;

            return View();
        }

        //public ActionResult SessionExpire()
        //{
        //    ViewBag.Message = CustomMessages.YourSessionExpired;

        //    return RedirectToAction("Login", "Account");
        //}
        #endregion 
	}
}