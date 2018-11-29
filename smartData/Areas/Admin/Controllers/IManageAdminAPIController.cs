using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;

namespace smartData.Areas.Admin.Controllers
{
    public interface IManageAdminAPIController
    {
        bool DeleteAdminAccount(int id, int userId);
        List<Core.Domain.Users> GetUsersByEmail(string username);
        bool UpdateAdminStatus(int id, int userId, bool status);
        Core.Domain.Users GetUserByEmailandIndustry(string email, string indutry);
       
    }
}