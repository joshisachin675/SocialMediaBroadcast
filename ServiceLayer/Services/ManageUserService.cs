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
    public class ManageUserService:IManageUserService
    {
        IManageUserRepository _manageUserRepository = null;
        #region ctor
        public ManageUserService(IManageUserRepository manageUserRepository)
        {
            _manageUserRepository = manageUserRepository;
        }
        #endregion

        #region public methods
        public bool DeleteUserAccount(int id, int userId)
        {
            return _manageUserRepository.DeleteUserAccount(id, userId);
        }

        public bool UpdateUserStatus(int id, int userId, bool status)
        {
            return _manageUserRepository.UpdateUserStatus(id, userId, status);
        }

        public List<Core.Domain.Users> GetUsersByEmail(string userName)
        {
            return _manageUserRepository.GetUsersByEmail(userName);
        }

        public bool UpdateUserPassword(int UserId, string FirstName, string LastName, string UserName, int IndustryId, string IndustryName, string password)
        {
            return _manageUserRepository.UpdateUserPassword(UserId, FirstName, LastName, UserName, IndustryId, IndustryName, password);

        }

        public Users GetPassword(int userId)
        {
            return _manageUserRepository.GetPassword(userId);
        }
        #endregion
    }
}
