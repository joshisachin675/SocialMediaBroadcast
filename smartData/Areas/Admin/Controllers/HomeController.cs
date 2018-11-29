using Core.Domain;
using Newtonsoft.Json;
using ServiceLayer.Services;
using smartData.Common;
using smartData.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace smartData.Areas.Admin.Controllers
{


    //[CheckSession]
    [AuthorizedRoles(Roles = "SuperAdmin, Admin")]
    public class HomeController : Controller
    {
        #region Global Variables
        ServiceLayer.Interfaces.IHomeService _homeService;
        IHomeAPIController _homeAPIController;
        public List<ScreenPermissionList> obj;
        public List<ScreenPermissionList> objScreenPermissionList = null;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        #endregion

        #region constructor
        public HomeController(HomeService homeService, IHomeAPIController homeAPIController)
        {
            _homeService = homeService;
            _homeAPIController = homeAPIController;
        }
        #endregion


        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}