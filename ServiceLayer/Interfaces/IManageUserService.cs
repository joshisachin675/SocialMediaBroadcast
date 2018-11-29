using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IManageUserService
    {
        bool DeleteUserAccount(int id, int userId);
        bool UpdateUserStatus(int id, int userId, bool status);
        List<Core.Domain.Users> GetUsersByEmail(string userName);
        bool UpdateUserPassword(int UserId, string FirstName, string LastName, string UserName, int IndustryId, string IndustryName, string password);
        Core.Domain.Users GetPassword(int userId);
    }
}
