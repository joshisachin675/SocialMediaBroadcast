using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInterfaces.Repository
{
    public interface IManageUserRepository
    {
        bool DeleteUserAccount(int id, int userId);
        bool UpdateUserStatus(int id, int userId, bool status);
        List<Core.Domain.Users> GetUsersByEmail(string userName);
        bool UpdateUserPassword(int UserId, string FirstName, string LastName, string UserName, int IndustryId, string IndustryName, string password);
        Users GetPassword(int userId);
     
    }
}
