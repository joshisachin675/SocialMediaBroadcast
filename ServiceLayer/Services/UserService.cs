using Core.Domain;
using System;
using System.Collections.Generic;
using AppInterfaces.Interfaces;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository = null;
        #region ctor
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region public methods
        public Users CreateUser(UserInsert user)
        {
            Users _usersObject = _userRepository.CreateUser(user);
            return _usersObject;
        }
        public List<Users> GetAllUsers(int limit, int offset, string order, string sort, string firstName, string lastName, string email, out int total)
        {
            return _userRepository.GetAllUsers(limit, offset, order, sort, firstName, lastName, email, out total);
        }
        public Users GetUserById(int? id)
        {
            return _userRepository.GetUserById(id);
        }
        public String EditUser(Users user)
        {
            user.Active = true;
            user.CreatedDate = DateTime.Now;
            return _userRepository.EditUser(user);
        }
        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }
        public List<Users> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public List<Roles> GetAllRoles()
        {
            return _userRepository.GetAllRoles();
        }

        public List<UserRoles> GetUserRolesById(int userId)
        {
            return _userRepository.GetUserRolesById(userId);
        }

        public bool DeleteUserRolesById(int userId, string Ids)
        {
            return _userRepository.DeleteUserRolesById(userId, Ids);
        }
        public void CreateUserRoles(int userId, string roleIds)
        {
            _userRepository.CreateUserRoles(userId, roleIds);
        }
        public string GetIsSuperAdmin(string tokenId)
        {
            return _userRepository.GetIsSuperAdmin(tokenId);
        }
        public void MassUpdateUser(string userNames, string unCheckIds, int modifyBy)
        {
            _userRepository.MassUpdateUser(userNames, unCheckIds, modifyBy);
        }
        public List<Users> GetUsersByUserID(int userId)
        {
            return _userRepository.GetUsersByUserID(userId);
        }
        public bool IsUserPermitedtoDelete(long userID)
        {
            return _userRepository.IsUserPermitedtoDelete(userID);
        }
        public bool UpdatePassword(string un, string rt)
        {
            return _userRepository.UpdatePassword(un, rt);
        }
        public void AddPasswordHistory(SecUserPasswordHistory secUserPasswordHistory)
        {
            _userRepository.AddPasswordHistory(secUserPasswordHistory);
        }
        public List<Users> GetUsersByEmail(string email)
        {
            return _userRepository.GetUsersByEmail(email);
        }

        public Users AddUser(Users user)
        {
            Users _usersObject = _userRepository.AddUser(user);
            return _usersObject;
        }

       public Users UpdateUsers(Users user)
        {
            Users _usersObject = _userRepository.UpdateUsers(user);
            return _usersObject;
        }

       public Users CheckExistingShortName(string shortname , int UserID )
       {
           return _userRepository.CheckExistingShortName(shortname , UserID);      
       }

        public Users AuthenticateUser(string email, int industryId, string Password)
        {
            return _userRepository.AuthenticateUser(email, industryId, Password);
        }

        public Users AuthenticateSuperAdmin(string email, string Password)
        {
            return _userRepository.AuthenticateSuperAdmin(email, Password);
        }
        

        public Users SaveTermsConditions(int UserId,string Ip,bool AcceptTerms)
        {
            return _userRepository.SaveTermsConditions(UserId, Ip, AcceptTerms);
        }

        public     List<ProvinceState> getstate(int id)
        {

            List<ProvinceState> state = new List<ProvinceState>();
            foreach (var  aPart in state)
            {
                aPart.CountryId = 0;
                aPart.StateId = 0;
                aPart.StateName = "---Select State---";
            }


            return _userRepository.getstate(id);
        }
        public dynamic GetContentPreference(int USerId, int DayID)
        {
            return _userRepository.GetContentPreference(USerId,  DayID);
        }
        public Users GetLeadManager(int USerId, int DayID)
        {
            return _userRepository.GetLeadManager(USerId,  DayID);
        }
        public dynamic SaveAutoPreferenceDetails(int UserId, List<string> preferenceId, List<string> PreferenceName, bool Status , int DayId)
        {
           return _userRepository.SaveAutoPreferenceDetails(UserId, preferenceId, PreferenceName , Status ,DayId);
        }
        public  dynamic SaveAutoLandPageDetail(int UserId, List<string> PageID, bool Status , int DayId)
        {
              return _userRepository.SaveAutoLandPageDetail(UserId, PageID , Status ,DayId);
        }
        public dynamic UpdateAutoPreference(int UserId, int DayId, List<string> SelectedPreference, List<string> PageIds, List<string> Time)
        {
            return _userRepository.UpdateAutoPreference(UserId, DayId, SelectedPreference, PageIds, Time);
        }
        public   dynamic DeleteAutoPreference(int UserId, int DayId, List<string> SelectedPreference, List<string> PageIds, List<string> Time)
        {
             return _userRepository.DeleteAutoPreference(UserId, DayId, SelectedPreference, PageIds, Time);
        }
    }
    
        #endregion

    }

