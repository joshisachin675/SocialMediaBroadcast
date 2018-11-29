using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IManageAdminService
    {
        bool DeleteAdminAccount(int id, int userId);
        List<Core.Domain.Users> GetUsersByEmail(string userName);
        bool UpdateAdminStatus(int id, int userId, bool status);
        Users GetUserByEmailandIndustry(string email, string indutry);
        Core.Domain.Users GetUserByEmailandIndustryId(string email, int industryId);
        string SetSuperAdminEmail();
    }
}
