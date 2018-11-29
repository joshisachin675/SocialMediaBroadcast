using ServiceLayer.Interfaces;
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
    public class ManageUserAPIController : ApiController, IManageUserAPIController
    {
        IManageUserService _manageUserService;


        public ManageUserAPIController(IManageUserService _ManageUserService)
        {
            _manageUserService = _ManageUserService;
        }

        /// <summary>
        /// Delete the user account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteUserAccount(int id, int userId)
        {
            return _manageUserService.DeleteUserAccount(id, userId);
        }

        /// <summary>
        /// Change status of the user account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool UpdateUserStatus(int id, int userId, bool status)
        {
            return _manageUserService.UpdateUserStatus(id, userId, status);
        }

        public List<Core.Domain.Users> GetUsersByEmail(string userName)
        {
            return _manageUserService.GetUsersByEmail(userName);
        }

        public bool UpdateUserPassword(int UserId, string FirstName, string LastName, string UserName, int IndustryId, string IndustryName, string password)
        {
            return _manageUserService.UpdateUserPassword( UserId, FirstName, LastName, UserName,IndustryId, IndustryName, password);
        }

        public  Core.Domain.Users GetPassword(int userId)
        {
            return _manageUserService.GetPassword(userId);
        }
    }
}