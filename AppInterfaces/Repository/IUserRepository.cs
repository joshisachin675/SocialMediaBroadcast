using Core.Domain;
using System;
using System.Collections.Generic;

namespace AppInterfaces.Interfaces
{
    public interface IUserRepository
    {
        Users CreateUser(UserInsert user);
        Users AddUser(Users user);
        Users UpdateUsers(Users user);
        List<Users> GetAllUsers(int limit, int offset, string order, string sort, string firstName, string lastName, string email, out int total);

        Users GetUserById(int? id);

        String EditUser(Users user);

        bool DeleteUser(int id);

        List<Users> GetAllUsers();
       
        List<Roles> GetAllRoles();
        
        void CreateUserRoles(int userId,string roles);
        
        List<UserRoles> GetUserRolesById(int userId);
        
        bool DeleteUserRolesById(int userId, string ids);
        
        string  GetIsSuperAdmin(string tokenID);

        void MassUpdateUser(string userNames, string unCheckIds, int modifyBy);
        
        List<Users> GetUsersByUserID(int userId);
        bool IsUserPermitedtoDelete(long userID);

        bool UpdatePassword(string email, string passwordVerificationToken);

        void AddPasswordHistory(SecUserPasswordHistory secUserPasswordHistory);
        List<Users> GetUsersByEmail(string email);

        Users AuthenticateUser(string email, int industryId, string Password);

        Users AuthenticateSuperAdmin(string email, string Password);

        Users CheckExistingShortName(string shortname, int UserId);

        Users SaveTermsConditions(int UserId, string Ip, bool AcceptTerms);
        List<ProvinceState> getstate(int id);

        dynamic GetContentPreference(int USerId, int DayID);

        dynamic SaveAutoPreferenceDetails(int UserId, List<string> preferenceId, List<string> PreferenceName, bool Status,int DayId);

        dynamic SaveAutoLandPageDetail(int UserId, List<string> PageID, bool Status , int DayId);

        Users GetLeadManager(int USerId, int DayID);

        dynamic UpdateAutoPreference(int UserId, int DayId, List<string> SelectedPreference, List<string> PageIds, List<string> Time);

        dynamic DeleteAutoPreference(int UserId, int DayId, List<string> SelectedPreference, List<string> PageIds, List<string> Time);
    }
}
