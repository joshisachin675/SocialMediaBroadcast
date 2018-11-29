using AppInterfaces.Repository;
using Core.Domain;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ManageAdminService : IManageAdminService
    {
        IManageAdminRepository _manageAdminRepository = null;
        #region ctor
        public ManageAdminService(IManageAdminRepository manageAdminRepository)
        {
            _manageAdminRepository = manageAdminRepository;
        }
        #endregion

        #region public methods

        public bool DeleteAdminAccount(int id, int userId)
        {
            return _manageAdminRepository.DeleteAdminAccount(id, userId);
        }

        public List<Core.Domain.Users> GetUsersByEmail(string userName)
        {
            return _manageAdminRepository.GetUsersByEmail(userName);
        }

        public bool UpdateAdminStatus(int id, int userId, bool status)
        {
            return _manageAdminRepository.UpdateAdminStatus(id, userId, status);
        }

        public Users GetUserByEmailandIndustry(string email, string indutry)
        {
            return _manageAdminRepository.GetUserByEmailandIndustry(email,indutry);
        }

        public Core.Domain.Users GetUserByEmailandIndustryId(string email, int industryId)
        {
            return _manageAdminRepository.GetUserByEmailandIndustryId(email, industryId);
        }
        public string SetSuperAdminEmail()
        {
            return _manageAdminRepository.SetSuperAdminEmail();
        }


     
        #endregion
    }
}
