using ServiceLayer.Interfaces;
using smartData.Filter;
using smartData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace smartData.Areas.Admin.Controllers
{
    [HandleException]
    public class ManageAdminAPIController : ApiController, IManageAdminAPIController
    {
        IManageAdminService _manageAdminService;


        public ManageAdminAPIController(IManageAdminService _ManageAdminService)
        {
            _manageAdminService = _ManageAdminService;
        }

        /// <summary>
        /// Delete the admin account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteAdminAccount(int id, int userId)
        {
            return _manageAdminService.DeleteAdminAccount(id, userId);
        }

        public List<Core.Domain.Users> GetUsersByEmail(string userName)
        {
            return _manageAdminService.GetUsersByEmail(userName);
        }

        public bool UpdateAdminStatus(int id, int userId, bool status)
        {
            return _manageAdminService.UpdateAdminStatus(id, userId, status);
        }

        public Core.Domain.Users GetUserByEmailandIndustry(string email, string industry)
        {
            return _manageAdminService.GetUserByEmailandIndustry(email, industry);
        }


      

    }
}