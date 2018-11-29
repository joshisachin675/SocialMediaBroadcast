using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Areas.Admin.Controllers
{
    public interface IManageUserAPIController
    {
        bool DeleteUserAccount(int id, int userId);
        bool UpdateUserStatus(int id, int userId, bool status);
        List<Core.Domain.Users> GetUsersByEmail(string username);
        bool UpdateUserPassword(int UserId, string FirstName, string LastName, string UserName, int IndustryId, string IndustryName, string password);
        Core.Domain.Users GetPassword(int userId);
    }
}