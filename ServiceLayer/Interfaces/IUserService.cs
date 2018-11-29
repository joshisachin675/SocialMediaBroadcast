using Core.Domain;
using System;
using System.Collections.Generic;


namespace ServiceLayer.Interfaces
{
    public interface IUserService
    {
        Users CreateUser(UserInsert user);
        Users AddUser(Users user);
        Users UpdateUsers(Users user);
        List<Users> GetAllUsers(int limit, int offset, string order, string sort, string FirstName, string LastName, string Email, out int total);
        Users GetUserById(int? id);
        String EditUser(Users user);
        bool DeleteUser(int id);
        List<Users> GetAllUsers();
        List<Roles> GetAllRoles();
        List<UserRoles> GetUserRolesById(int UserId);
        bool DeleteUserRolesById(int UserId, string Ids);
        void CreateUserRoles(int UserId, string RoleIds);
        string GetIsSuperAdmin(string TokenID);
        void MassUpdateUser(string UserNames, string UnCheckIds, int ModifyBy);
        List<Users> GetUsersByUserID(int UserId);
        bool IsUserPermitedtoDelete(long UserID);
        bool UpdatePassword(string un, string rt);
        void AddPasswordHistory(SecUserPasswordHistory secUserPasswordHistory);
        List<Users> GetUsersByEmail(string email);
        Users AuthenticateUser(string email, int industryId, string Password);
        Users AuthenticateSuperAdmin(string email, string Password);
        Users CheckExistingShortName(string shortname, int UserId);
        Users SaveTermsConditions(int UserId, string Ip, bool AcceptTerms);
        List<ProvinceState> getstate(int id);
        dynamic GetContentPreference(int UserId, int DayID);
        dynamic SaveAutoPreferenceDetails(int UserId, List<string> preferenceId, List<string> PreferenceName, bool Status, int DayID);
        dynamic SaveAutoLandPageDetail(int UserId, List<string> list, bool Status, int DayId);
        Users GetLeadManager(int UserId, int DayID);        
        dynamic UpdateAutoPreference(int UserId, int DayId, List<string> SelectedPreference, List<string> PageIds, List<string> Time);
        dynamic DeleteAutoPreference(int UserId, int DayId, List<string> SelectedPreference, List<string> PageIds, List<string> Time);
    }
}
