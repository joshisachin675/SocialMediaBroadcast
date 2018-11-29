using AppInterfaces.Infrastructure;
using AppInterfaces.Interfaces;
using Core.Domain;
using Core.enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using WebMatrix.WebData;

namespace RepositoryLayer.Repositories
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        #region ctor
        public UserRepository(IAppUnitOfWork uow)
            : base(uow)
        {
        }
        #endregion
        public class datelist
        {
            public DateTime scheduleDate { get; set; }
            public DateTime LocalDate { get; set; }
        }
        #region public methods
        public Users CreateUser(UserInsert user)
        {
            //UOW.StartTransaction();
            var scopeOption = new TransactionOptions();
            scopeOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, scopeOption))
            {
                var userObject = AddUser(user);

                if (userObject != null && !string.IsNullOrEmpty(userObject.RoleIDs.ToString()))
                    AddUserRoles(userObject.UserId, userObject.RoleIDs.ToString());
                //WebSecurity.CreateAccount(user.Email, user.Password);
                //UOW.CommitTransaction();
                scope.Complete();
                return userObject;
            }
        }

        public Users AddUser(Users user)
        {
            var shortName = user.FirstName + user.LastName;
            shortName = shortName.Replace(" ", String.Empty);
            user.Shortname = "temp";
            Context.Set<Users>().Add(user);

            Context.SaveChanges();

            var existingUser = Context.Set<Users>().FirstOrDefault(x => x.Shortname == shortName);

            if (existingUser == null)
            {
                user.Shortname = shortName;
            }
            else
            {
                user.Shortname = shortName + user.UserId;
            }
            Context.SaveChanges();
            string industryShortName = Context.Set<smIndustry>().Where(x => x.IndustryId == user.IndustryId).Select(x => x.IndustryShortName).FirstOrDefault();
            user.IndustryName = industryShortName;
            return user;
        }

        public Users UpdateUsers(Users user)
        {
            var userobj = Context.Set<Users>().Where(x => x.UserId == user.UserId).FirstOrDefault();
            /// Got Error While Save Checkbox data into smLandingPagesForUsers  table   //  data automatically saved in pagecollection table 
            //var landin = Context.Set<smLandingPagesCollection>().ToList();
            //userobj.LandingPageList = landin;
            /// Got Error While Save Checkbox data into smLandingPagesForUsers  table
            Context.SaveChanges();
            foreach (var item in user.LandingPageList)
            {
                var objUser = Context.Set<smLandingPagesForUsers>().Where(x => x.id_user == user.UserId && x.id_landingpage == item.id).FirstOrDefault();

                if (objUser != null)
                {
                    objUser.IsActive = item.active;
                    Context.SaveChanges();
                }
                else
                {

                    smLandingPagesForUsers land = new smLandingPagesForUsers();
                    land.id_landingpage = item.id;
                    land.id_user = user.UserId;
                    land.IsActive = item.active;
                    Context.Set<smLandingPagesForUsers>().Add(land);
                    Context.SaveChanges();

                }

            }

            return userobj;
        }


        public Users CheckExistingShortName(string shortname, int UserId)
        {

            var currentUser = Context.Set<Users>().Where(x => x.UserId == UserId && x.Shortname == shortname).FirstOrDefault();
            if (currentUser != null)
            {
                var temp = Context.Set<Users>().Where(x => x.UserId == 0).FirstOrDefault();
                return temp;
            }
            var userobj = Context.Set<Users>().Where(x => x.Shortname.ToLower() == shortname.ToLower() && x.UserId != UserId).FirstOrDefault();
            if (userobj != null)
            {
                return userobj;
            }
            return userobj;




        }


        public void CreateUserRoles(int userID, string roleIds)
        {
            AddUserRoles(userID, roleIds);
        }
        public void EditUserRoles(int userId, string roleIds)
        {
            UOW.StartTransaction();
            UpdateUserRoles(userId, roleIds);
            UOW.CommitTransaction();
        }
        public String EditUser(Users user)
        {
            UOW.StartTransaction();
            var result = UpdateUser(user);
            UOW.CommitTransaction();

            return result;
        }
        public List<Users> GetAllUsers(int limit, int offset, string order, string sort, string firstName, string lastName, string email, out int total)
        {
            var data = Context.Set<Users>().Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(firstName))
            {
                data = data.Where(x => x.FirstName.StartsWith(firstName));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                data = data.Where(x => x.LastName.StartsWith(lastName));
            }
            if (!string.IsNullOrEmpty(email))
            {
                data = data.Where(x => x.Email.StartsWith(email));
            }

            GetSortedData(ref data, sort + order.ToUpper());

            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }
        public bool UpdatePassword(string email, string passwordVerificationToken)
        {
            return (from i in Context.Set<Users>()
                    join w in Context.Set<webpages_Membership>() on i.UserId equals w.UserId
                    where i.Email == email
                    && w.PasswordVerificationToken == passwordVerificationToken
                    && i.IsDeleted == false
                    select i).Any();

        }
        public void GetSortedData(ref IQueryable<Users> userQuery, string sortingOrder)
        {
            switch (sortingOrder)
            {
                case "FirstNameASC":
                    userQuery = userQuery.OrderBy(x => x.FirstName.ToLower());
                    break;
                case "LastNameASC":
                    userQuery = userQuery.OrderBy(x => x.LastName.ToLower());
                    break;
                case "EmailASC":
                    userQuery = userQuery.OrderBy(x => x.Email.ToLower());
                    break;
                case "FirstNameDESC":
                    userQuery = userQuery.OrderByDescending(x => x.FirstName.ToLower());
                    break;
                case "LastNameDESC":
                    userQuery = userQuery.OrderByDescending(x => x.LastName.ToLower());
                    break;
                case "EmailDESC":
                    userQuery = userQuery.OrderByDescending(x => x.Email.ToLower());
                    break;
                case "UserIdDESC":
                    userQuery = userQuery.OrderByDescending(x => x.UserId);
                    break;
                case "UserIdASC":
                    userQuery = userQuery.OrderBy(x => x.UserId);
                    break;
                default:
                    userQuery = userQuery.OrderByDescending(x => x.UserId);
                    break;
            }
        }
        public Users GetUserById(int? id)
        {
            Users objUser = Get(x => x.UserId == id && !x.IsDeleted && x.Active);
            List<smLandingPagesCollections> LandingPageList = (from p in Context.Set<smLandingPagesCollection>().Where(x => x.active == true && x.id_industry == objUser.IndustryId)
                                                               select new smLandingPagesCollections()
                                                               {
                                                                   id = p.id,
                                                                   id_industry = p.id_industry,
                                                                   label = p.label,
                                                                   sortorder = p.sortorder,
                                                                   url = p.url,

                                                               }).ToList(); ;




            List<smLandingPagesForUser> UserLandingPageLIst = (from d in Context.Set<smLandingPagesForUsers>().Where(x => x.id_user == objUser.UserId)
                                                               select new smLandingPagesForUser()
                                                               {
                                                                   id = d.id,
                                                                   id_landingpage = d.id_landingpage,
                                                                   id_user = d.id_user,
                                                                   IsActive = d.IsActive

                                                               }).ToList();
            //var LandingPageList =   Context.Set<smLandingPagesCollection>().Where(x => x.active == true && x.id_industry == objUser.IndustryId).ToList();
            //var UserLandingPageLIst = Context.Set<smLandingPagesForUsers>().Where(x => x.id_user == objUser.UserId).ToList();

            foreach (var item1 in LandingPageList)
            {
                item1.active = false;

                if (UserLandingPageLIst.Count() != 0)
                {
                    foreach (var item2 in UserLandingPageLIst)
                    {
                        if (item2.id_landingpage == item1.id)
                        {
                            item1.active = item2.IsActive;

                        }
                    }
                }

            }


            objUser.LandingPageList = LandingPageList;
            objUser.UserLandingPageLIst = UserLandingPageLIst;

            //var matchedlist = LandingPageList.Where(x => UserLandingPageLIst.Any(y => y.id_landingpage == x.id)).ToList();

            //matchedlist.Select(x => { x.active = false; return x; }).ToList();
            //objUser.LandingPageList = matchedlist;

            //objUser.LandingPageList = LandingPageList;

            var contryList = Context.Set<Countries>().ToList();

            objUser.countries = contryList;
            return objUser;
        }
        public bool DeleteUser(int id)
        {

            Users user = Get(x => x.UserId == id && !x.IsDeleted);
            user.IsDeleted = true;
            user.DeletedBy = 1;//TODO
            user.DeletedDate = DateTime.UtcNow;
            Context.SaveChanges();
            return true;

        }
        public IQueryable<T> SprocExecuteList<T>(string storeprocName, params object[] sqlDbParam) where T : BaseEntity, new()
        {
            return Context.ExecuteStoredProcedureList<T>(storeprocName, sqlDbParam).AsQueryable();
        }
        public int AuthenticateUser(string email, string password, ref Core.Domain.Users model)
        {
            int status = 0;
            var userData = Get(x => x.Email == email && x.Password == password);
            if (userData != null)
            {
                if (!userData.Active)
                {
                    //User is Inactive for Now
                    status = (int)UserLoginMessage.InActive;
                }

                if (userData.IsDeleted)
                {
                    //User has been deleted
                    status = (int)UserLoginMessage.IsDeleted;
                }

                else
                {
                    //User is valid to Login
                    status = (int)UserLoginMessage.ValidUser;
                    model.Email = email;
                    model.FirstName = userData.FirstName;
                    model.LastName = userData.LastName;
                    model.UserId = userData.UserId;
                }
            }

            return status;
        }


        public List<Users> GetAllUsers()
        {
            return GetAll().Where(x => x.IsDeleted == false).OrderByDescending(x => x.UserId).ToList();
        }
        public List<Roles> GetAllRoles()
        {
            return Context.Set<Roles>().Where(x => x.IsDeleted == false && x.Active == true).ToList();
        }
        public List<UserRoles> GetUserRolesById(int userId)
        {
            return Context.Set<UserRoles>().Where(x => x.UserId == userId && x.IsDeleted == false).ToList();

        }
        public bool DeleteUserRolesById(int userId, string ids)
        {
            string[] roleIds = ids.Split(',');
            if (roleIds.Length > 0)
            {
                for (int i = 0; i < roleIds.Length; i++)
                {
                    if (roleIds[i].Length > 0)
                    {
                        var uid = int.Parse(roleIds[i]);

                        var userRoles = Context.Set<UserRoles>().Where(s => s.RoleId == uid && s.UserId == userId && s.IsDeleted == false).FirstOrDefault();

                        if (userRoles != null)
                        {
                            userRoles.IsDeleted = true;
                            userRoles.DeletedBy = 1;
                            userRoles.DeletedDate = DateTime.UtcNow;
                        }
                    }
                }
                Context.SaveChanges();
            }
            return true;
        }
        public string GetIsSuperAdmin(string tokenID)
        {

            var query = (from U in Context.Set<Users>().AsEnumerable()
                         join log in Context.Set<smLoginAuthentication>().AsEnumerable() on U.UserId equals log.UserId
                         where log.Active == false && log.TokenId == Guid.Parse(tokenID.ToString())
                         select new { IsSuperAdmin = U.IsSuperAdmin }).Single();
            return query.IsSuperAdmin.ToString();

        }
        public void MassUpdateUser(string userNames, string unCheckIds, int modifyBy)
        {
            List<Roles> rolesList = new List<Roles>();
            if (!string.IsNullOrEmpty(userNames))
            {
                string tokenID = "";
                DbDataReader _reader;
                Context.Database.Initialize(force: false);
                Context.Database.Connection.Open();
                var _dbCmd = Context.Database.Connection.CreateCommand();
                _dbCmd.CommandText = "ssp_MassUpdateUser";
                _dbCmd.CommandType = CommandType.StoredProcedure;
                _dbCmd.CommandTimeout = 60 * 5;
                _dbCmd.Parameters.AddRange(new SqlParameter[] {
                       new SqlParameter("@UserNames", userNames),
                       new SqlParameter("@unCheckIds", unCheckIds),
                       new SqlParameter("@active", modifyBy)
                      // new SqlParameter("@CurrentPage", objEducationContentSearchArea.CurrentPage)
                  });
                _reader = _dbCmd.ExecuteReader();
                while (_reader.Read())
                {
                    tokenID = _reader["flag"].ToString();
                }
            }
        }
        public List<Users> GetUsersByUserID(int userId)
        {
            return GetAll(x => x.IsDeleted == false && x.UserId == userId).ToList();
        }
        public bool DeleteUserByID(int id)
        {
            bool flag = false;

            if (!string.IsNullOrEmpty(id.ToString()))
            {
                string TokenID = "";
                DbDataReader _reader;
                Context.Database.Initialize(force: false);
                Context.Database.Connection.Open();
                var _dbCmd = Context.Database.Connection.CreateCommand();
                _dbCmd.CommandText = "ssp_DeleteUserByID";
                _dbCmd.CommandType = CommandType.StoredProcedure;
                _dbCmd.CommandTimeout = 60 * 5;
                _dbCmd.Parameters.AddRange(new SqlParameter[] {
                       new SqlParameter("@id", id)
                      
                       //new SqlParameter("@active", ModifyBy)
                      // new SqlParameter("@CurrentPage", objEducationContentSearchArea.CurrentPage)
                  });
                _reader = _dbCmd.ExecuteReader();
                while (_reader.Read())
                {
                    TokenID = _reader["flag"].ToString();
                    flag = true;
                }
            }
            return flag;
        }
        public bool IsUserPermitedtoDelete(long userID)
        {
            return Context.Set<Users>().Where(x => x.UserId == userID).Select(x => x.IsSuperAdmin).FirstOrDefault();
        }
        #endregion


        #region private methods
        private Users AddUser(UserInsert user)
        {
            if (!Context.Set<Users>().Any(x => x.Email.ToLower() == user.Email.ToLower() && x.IsDeleted == false))
            {
                user.CreatedBy = 0;
                user.CreatedDate = DateTime.UtcNow;
                user.IsDeleted = false;

                user.UserTypeId = 1;//TODO

                Users userObject = new Users();
                userObject.Active = user.Active;
                userObject.AuthFacebookId = user.AuthFacebookId;
                userObject.ConfirmPassword = user.ConfirmPassword;
                userObject.CreatedBy = user.CreatedBy;
                userObject.CreatedDate = user.CreatedDate;
                userObject.DeletedBy = user.DeletedBy;
                userObject.DeletedDate = user.DeletedDate;
                userObject.Email = user.Email;
                userObject.FirstName = user.FirstName;
                userObject.IsDeleted = user.IsDeleted;
                userObject.LastName = user.LastName;
                userObject.Password = user.Password;
                userObject.RoleIDs = user.RoleIDs;
                userObject.UserId = user.UserId;
                userObject.UserTypeId = user.UserTypeId;
                userObject.IsSuperAdmin = user.IsSuperAdmin;
                Context.Set<Users>().Add(userObject);
                Context.SaveChanges();
                return userObject;
            }
            return null;
        }



        private void AddUserRoles(int userID, string roleIds)
        {
            List<UserRoles> userRoleList = new List<UserRoles>();
            string[] ids = roleIds.Split(',');
            if (ids.Length > 0)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i].Length > 0)
                    {
                        var uid = userID;
                        var rid = int.Parse(ids[i]);
                        if (!Context.Set<UserRoles>().Any(x => x.UserId == uid && x.RoleId == rid && x.IsDeleted == false))
                        {
                            UserRoles userRoles = new UserRoles();
                            userRoles.RoleId = int.Parse(ids[i]);
                            userRoles.UserId = userID;
                            userRoles.CreatedBy = 0;//TODO
                            userRoles.CreatedDate = DateTime.UtcNow;
                            userRoles.IsDeleted = false;
                            userRoleList.Add(userRoles);
                        }

                    }
                }
                Context.Set<UserRoles>().AddRange(userRoleList);
                Context.SaveChanges();
            }
        }

        private void UpdateUserRoles(int userId, string roleIds)
        {
            List<UserRoles> userRoleList = new List<UserRoles>();
            var alreadyAssignedRoles = Context.Set<UserRoles>().Where(x => x.UserId == userId && x.IsDeleted == false).ToList();

            foreach (var item in alreadyAssignedRoles)
            {
                Context.Set<UserRoles>().Remove(item);
            }
            Context.SaveChanges();

            string[] ids = roleIds.Split(',');
            if (ids.Length > 0)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i].Length > 0)
                    {
                        var uid = userId;
                        var rid = int.Parse(ids[i]);
                        if (!Context.Set<UserRoles>().Any(x => x.UserId == uid && x.RoleId == rid && x.IsDeleted == false))
                        {
                            UserRoles userRoles = new UserRoles();
                            userRoles.RoleId = int.Parse(ids[i]);
                            userRoles.UserId = userId;
                            userRoles.CreatedBy = 0;
                            userRoles.CreatedDate = DateTime.UtcNow;
                            userRoles.IsDeleted = false;
                            userRoleList.Add(userRoles);
                        }

                    }
                }
                Context.Set<UserRoles>().AddRange(userRoleList);
                Context.SaveChanges();
            }
        }


        private String UpdateUser(Users user)
        {
            var userExist = GetAll(x => x.Email.ToLower() == user.Email.ToLower() && x.UserId != user.UserId && !x.IsDeleted && x.Active).Any();
            if (userExist)
            {
                return "Error Email already exists";
            }

            var userObject = Get(x => x.UserId == user.UserId);
            userObject.FirstName = user.FirstName;
            userObject.LastName = user.LastName;
            userObject.Email = user.Email;
            userObject.Active = user.Active;
            userObject.CreatedDate = user.CreatedDate;
            userObject.ModifiedBy = user.ModifiedBy;
            userObject.ModifiedDate = user.CreatedDate;
            userObject.RoleIDs = user.RoleIDs;
            userObject.RolesList = user.RolesList;
            Context.SaveChanges();

            if (!string.IsNullOrEmpty((userObject.RoleIDs.ToString())))
            {
                UpdateUserRoles(userObject.UserId, userObject.RoleIDs.ToString());
            }
            return "true";
        }


        public void AddPasswordHistory(SecUserPasswordHistory secUserPasswordHistory)
        {
            Context.Set<SecUserPasswordHistory>().Add(secUserPasswordHistory);
        }

        public List<Users> GetUsersByEmail(string email)
        {
            return GetAll().Where(x => x.IsDeleted == false && x.Email.ToLower() == email.ToString().ToLower()).ToList();
        }

        public Users GetUserByEmailandIndustry(string email, string indusrty)
        {
            return GetAll().Where(x => x.IsDeleted == false && x.Email.ToLower() == email.ToLower() && x.Active == true && x.IndustryName.ToLower() == indusrty.ToLower()).FirstOrDefault();
        }

        public Users AuthenticateUser(string email, int industryId, string Password)
        {
            return GetAll().Where(x => x.IsDeleted == false && x.Email.ToLower() == email.ToLower() && x.Active == true && x.IndustryId == industryId && x.Password == Password).FirstOrDefault();
        }

        public Users AuthenticateSuperAdmin(string email, string Password)
        {
            return GetAll().Where(x => x.IsDeleted == false && x.Email.ToLower() == email.ToLower() && x.Active == true && x.Password == Password).FirstOrDefault();
        }

        public Users SaveTermsConditions(int UserId, string Ip, bool AcceptTerms)
        {

            Users user = Context.Set<Users>().FirstOrDefault(x => x.UserId == UserId);
            user.AcceptTerms = AcceptTerms;
            user.ConfirmPassword = user.Password;
            user.AcceptTermsIP = Ip;
            Context.SaveChanges();
            return user;

        }
        public List<ProvinceState> getstate(int id)
        {


            List<ProvinceState> state = new List<ProvinceState>();
            ProvinceState st = new ProvinceState();
            st.StateName = "---Select Province---";
            st.StateId = 0;
            st.CountryId = 0;
            state.Add(st);

            state = Context.Set<ProvinceState>().Where(x => x.CountryId == id).Select(x => x).ToList();
            return state;
        }

        public dynamic GetContentPreference(int UserId, int DayID)
        {
            var IndustryID = Context.Set<Users>().Where(x => x.UserId == UserId).Select(x => x.IndustryId).FirstOrDefault();
            var selected = Context.Set<smPreference>().Where(x => x.UserId == UserId && x.IsDeleted == false).Select(p => p.Preference).ToList();
            List<smSubIndustry> data = Context.Set<smSubIndustry>().Where(x => x.IsDeleted == false && x.IndustryId == IndustryID).ToList()
                  .Select(s =>
                  {

                      return new smSubIndustry
                      {
                          Preference = selected.Contains(s.SubIndustryName),
                          IsActive = s.IsActive,
                          IndustryName = s.IndustryName,
                          SubIndustryName = s.SubIndustryName,
                          SubIndustryId = s.SubIndustryId,

                      };
                  }).ToList();
            data = data.Where(x => x.Preference == true).ToList();
            var savePublishingtimne = Context.Set<smAutoPreference>().Where(x => x.UserId == UserId && x.IsDeleted == false && x.IsActive == true && x.Day == DayID).Select(x => x.SubindustryName).ToList();
            data = data.ToList()
                   .Select(s =>
                   {

                       return new smSubIndustry
                       {
                           Preference = savePublishingtimne.Contains(s.SubIndustryName),
                           IsActive = s.IsActive,
                           IndustryName = s.IndustryName,
                           SubIndustryName = s.SubIndustryName,
                           SubIndustryId = s.SubIndustryId,

                       };
                   }).ToList();
            return data;
        }
        public Users GetLeadManager(int UserId, int DayID)
        {
            Users objUser = Get(x => x.UserId == UserId && !x.IsDeleted && x.Active);
            List<smLandingPagesCollections> LandingPageList = (from p in Context.Set<smLandingPagesCollection>().Where(x => x.active == true && x.id_industry == objUser.IndustryId)
                                                               select new smLandingPagesCollections()
                                                               {
                                                                   id = p.id,
                                                                   id_industry = p.id_industry,
                                                                   label = p.label,
                                                                   sortorder = p.sortorder,
                                                                   url = p.url,

                                                               }).ToList(); ;




            List<smLandingPagesForUser> UserLandingPageLIst = (from d in Context.Set<smLandingPagesForUsers>().Where(x => x.id_user == objUser.UserId)
                                                               select new smLandingPagesForUser()
                                                               {
                                                                   id = d.id,
                                                                   id_landingpage = d.id_landingpage,
                                                                   id_user = d.id_user,
                                                                   IsActive = d.IsActive

                                                               }).ToList();

            LandingPageList = LandingPageList.Where(m => UserLandingPageLIst.Where(d => d.IsActive == true).Select(s => s.id_landingpage).Contains(m.id)).ToList();
            var autoSelectedPageList = Context.Set<smAutoselectedLandPage>().Where(x => x.UserId == UserId && x.IsActive == true && x.DayID == DayID).Select(x => x.LandingPageID).ToList();
            //  List<smLandingPagesCollections> SelectedLandingPageList = LandingPageList.Where(m => autoSelectedPageList.Contains(m.id)).ToList();
            List<smLandingPagesCollections> finalList = (from d in LandingPageList
                                                         select new smLandingPagesCollections()
                                                         {
                                                             id = d.id,
                                                             id_industry = d.id_industry,
                                                             label = d.label,
                                                             url = d.url,
                                                             active = autoSelectedPageList.Contains(d.id),

                                                         }).ToList();
            objUser.LandingPageList = finalList;
            objUser.UserLandingPageLIst = UserLandingPageLIst;

            return objUser;
        }


        public dynamic SaveAutoPreferenceDetails(int UserId, List<string> preferenceId, List<string> PreferenceName, bool Status, int DayId)
        {


            int SubindustryId = Convert.ToInt32(preferenceId[0]);
            string SubindustryName = PreferenceName[0].Trim();
            smAutoPreference preferences = new smAutoPreference();
            smAutoPreference preference = Context.Set<smAutoPreference>().Where(x => x.UserId == UserId && x.SubindustryID == SubindustryId && x.IsDeleted == false && x.Day == DayId).FirstOrDefault();
            if (preference != null)
            {
                preference.IsActive = Status;
                preference.SubindustryID = SubindustryId;
                preference.SubindustryName = SubindustryName;
                preference.UserId = UserId;
                preference.IsDeleted = false;
                preference.Day = DayId;
                Context.SaveChanges();
            }
            else
            {
                preferences.IsActive = Status;
                preferences.SubindustryID = SubindustryId;
                preferences.SubindustryName = SubindustryName;
                preferences.UserId = UserId;
                preferences.IsDeleted = false;
                preferences.Day = DayId;
                preferences.CreatedDate = DateTime.UtcNow;
                Context.Set<smAutoPreference>().Add(preferences);
                Context.SaveChanges();
            }



            return true;
        }
        public dynamic SaveAutoLandPageDetail(int UserId, List<string> PAgeID, bool Status, int DayId)
        {
            smAutoselectedLandPage AutolandingPAge = new smAutoselectedLandPage();
            smAutoselectedLandPage AddAutolandingPAge = new smAutoselectedLandPage();
            foreach (var item in PAgeID)
            {
                int landingpageeID = Convert.ToInt32(item);
                AutolandingPAge = Context.Set<smAutoselectedLandPage>().Where(x => x.UserId == UserId && x.LandingPageID == landingpageeID && x.DayID == DayId).FirstOrDefault();


                if (AutolandingPAge != null)
                {
                    AutolandingPAge.IsActive = Status;
                    AutolandingPAge.LandingPageID = landingpageeID;
                    AutolandingPAge.Isdeleted = false;
                    AutolandingPAge.UserId = UserId;
                    AutolandingPAge.DayID = DayId;
                    Context.SaveChanges();
                }
                else
                {
                    AddAutolandingPAge.IsActive = Status;
                    AddAutolandingPAge.LandingPageID = landingpageeID;
                    AddAutolandingPAge.Isdeleted = false;
                    AddAutolandingPAge.UserId = UserId;
                    AddAutolandingPAge.DayID = DayId;
                    Context.Set<smAutoselectedLandPage>().Add(AddAutolandingPAge);
                    Context.SaveChanges();
                }
            }


            return true;
        }
        public static DateTime FromUTCData(DateTime? dt, int timezoneOffset)
        {
            DateTime newDate = dt.Value + new TimeSpan(timezoneOffset / 60, timezoneOffset % 60, 0);
            return newDate;
        }

        public dynamic UpdateAutoPreference(int UserId, int DayId, List<string> SelectedPreference, List<string> PageIds, List<string> Time)
        {
            //=====================Update Publishing Time =========== Start==========

            try
            {
                /// ====================== Manage Time =============================
                #region
                var timea = Time[0];
                int TimeOffset = Convert.ToInt32(Time[1]);
                DateTime Stime = Convert.ToDateTime(timea);
                DateTime Postedtime = FromUTCData(Stime, TimeOffset);
                TimeSpan timesp = Postedtime.TimeOfDay;

                DateTime time = Convert.ToDateTime(timea);
                DateTime timenew = FromUTCData(time, TimeOffset);
                DateTime d = DateTime.Today;
                int offset = d.DayOfWeek - DayOfWeek.Monday;
                DateTime lastMonday = d.AddDays(-offset);
                DateTime nextMonday = lastMonday.AddDays(7);
                int dayNew = DayId;
                int dayToday = GetDay(DateTime.Now.DayOfWeek.ToString());
                DateTime resTime = DateTime.Now;
                int remaind = Math.Abs(dayToday - dayNew);
                DateTime nextDates = DateTime.Now.AddDays(remaind).Date;
                TimeSpan tcimen = timenew.TimeOfDay;
                resTime = nextDates + tcimen;
                #endregion
                ///==================================Manage Time End==================
                var content = Context.Set<smPublishingTime>().Where(x => x.DayId == DayId && x.UserId == UserId && x.IsActive == true).FirstOrDefault();
                DateTime Posteddatetime = getTime(content.Time, TimeOffset, DayId);
                DateTime Newtime = getTime(timea, TimeOffset, DayId);
                DateTime LocalDAte = FromUTCData(new DateTime?(Newtime), -TimeOffset);
                var newSelectedList = SelectedPreference.Select(int.Parse).ToList();
                var Preselected = Context.Set<smAutoPreference>().Where(x => x.IsDeleted == false && x.IsActive == true && x.Day == DayId && x.UserId == UserId).Select(x => x.SubindustryID).ToList();
                var LetestRemovedList = Preselected.Where(x => !newSelectedList.Contains(x)).Select(x => x);
                //var content = Context.Set<smPublishingTime>().Where(x => x.DayId == DayId && x.UserId == UserId && x.IsActive == true).FirstOrDefault();
                var OldTime = content.Time;
                var LetestRemovedListCheck = Preselected.Select(x => !newSelectedList.Contains(x));               
                smPublishingTime autoPrefe = new smPublishingTime();
                if (content != null)
                {
                    content.Time = timea.ToString();
                    content.CreatedBy = UserId;
                    content.CreatedDate = DateTime.UtcNow;
                    content.TimeStampPosted = timesp;
                    Context.SaveChanges();
                }
                DateTime LocalTime = Newtime.Date + Stime.TimeOfDay;
                //=====================Update Publishing Time ================ End =========


                //========================COnvert to actual time to post ====================   
             
                //+===========================Content of Selected Prefereces Selected=====================================+
                List<int> SelectedPreferences = SelectedPreference.Select(int.Parse).ToList();
                int aSubIndustryId = LetestRemovedList.Select(x => x).FirstOrDefault();
                var IndustryId = Context.Set<smSubIndustry>().Where(x => x.SubIndustryId.Equals(aSubIndustryId)).Select(x => x.IndustryId).FirstOrDefault();
                var list = Context.Set<smSubIndustry>().Where(x => SelectedPreferences.Contains(x.SubIndustryId)).ToList();
                //-------------------------------------
                //// Get data from content library on behalf of social media that is activated by user
                var myvar = from a in Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive == true && !x.IsDeleted)
                            join b in Context.Set<smSocialMediaProfile>().Where(x => x.UserId == UserId && x.IsActive == true) on a.SocialMedia equals b.SocialMedia
                            select new { a };
                List<smContentLibrary> list2 = new List<smContentLibrary>();
                foreach (var item in myvar)
                {
                    list2.Add(item.a);
                }
                //------------------------------------------

                ///////////// Filter COntent Library Data According to selected preference
                List<smContentLibrary> source = new List<smContentLibrary>();
                if (list.Count != 0)
                {
                    var preferenceList = list.Select(x => x.SubIndustryName).ToList();
                    source = list2.Where(m => preferenceList.Contains(m.SubIndustryName)).ToList();  /// All List of according to new selected preferences
                }

                //+===========================Content of Selected Prefereces Selected=====================================+

                //--------------------------------------------------------------------------------------------------------------

                //+===========================Content of Selected Prefereces Pre Selected=====================================+

                var listPre = Context.Set<smSubIndustry>().Where(x => Preselected.Contains(x.SubIndustryId)).ToList();
                //-------------------------------------
                //// Get data from content library on behalf of social media that is activated by user
                var myvarPre = from a in Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive == true && !x.IsDeleted)
                               join b in Context.Set<smSocialMediaProfile>().Where(x => x.UserId == UserId && x.IsActive == true) on a.SocialMedia equals b.SocialMedia
                               select new { a };
                List<smContentLibrary> list2Pre = new List<smContentLibrary>();
                foreach (var item in myvar)
                {
                    list2Pre.Add(item.a);
                }
                //------------------------------------------

                /// Create date list  === Start
                /// 

                DateTime LocalTimes = Convert.ToDateTime(timea);
                DateTime localtime = resTime.Date + LocalTimes.TimeOfDay;
                List<datelist> DateList = new List<datelist>()           
                                 {
                                     new datelist{ LocalDate=localtime, scheduleDate=Posteddatetime},
                                     new datelist{ LocalDate=localtime.AddDays(7), scheduleDate=Posteddatetime.AddDays(7)},
                                     new datelist{ LocalDate=localtime.AddDays(14), scheduleDate=Posteddatetime.AddDays(14)},
                                     new datelist{ LocalDate=localtime.AddDays(21), scheduleDate=Posteddatetime.AddDays(21)},
                                };
                /// Create date list  == End
                /// 
                ///////////// Filter COntent Library Data According to selected preference
                List<smContentLibrary> sourcePre = new List<smContentLibrary>();
                if (listPre.Count != 0)
                {
                    var preferenceListPre = listPre.Select(x => x.SubIndustryName).ToList();
                    sourcePre = list2Pre.Where(m => preferenceListPre.Contains(m.SubIndustryName)).ToList();  /// All List of according to new selected preferences
                }

                //+===========================Content of Selected Prefereces Selected End =====================================+

                //+========================================= Update post date===================================================+
            
                int dayInc = 0;
                foreach (var item in DateList)
                {
                    var SavedPost = Context.Set<smPost>().Where(x => x.ContentCreatedId == 2 && x.UserId == UserId &&  x.PostDate.Year == item.scheduleDate.Year && x.PostDate.Month == item.scheduleDate.Month && x.PostDate.Day == item.scheduleDate.Day).ToList();                          
                    Newtime = Newtime.AddDays(dayInc);

                    if (SavedPost.Count() != 0)
                    {
                        SavedPost.ForEach(x => { x.PostDate = Newtime; x.ModifiedBy = UserId; x.ModifiedDate = DateTime.UtcNow; });
                        Context.SaveChanges();
                       

                    }



                    var SavedEvent = Context.Set<smScheduleEvents>().Where(x => x.UserId == UserId && x.ContentCreatedId == 2 && x.ScheduleTime.Year == item.scheduleDate.Year && x.ScheduleTime.Month == item.scheduleDate.Month && x.ScheduleTime.Day == item.scheduleDate.Day).ToList();
                    if (SavedEvent.Count() != 0)
                    {
                        SavedEvent.ForEach(x => { x.ScheduleTime = item.scheduleDate; x.LocalTime = item.LocalDate; });
                        Context.SaveChanges();

                    }
                    dayInc = dayInc + 7;
                }
                
                //+========================================= Update Schedule Event date===================================================+
               


                //+========================================= Update/delete post detail according to preferences date===================================================+
                var SavedPostToRemove = Context.Set<smPost>().Where(x => x.ContentCreatedId == 2 && x.UserId == UserId && x.PostDate.Date == Posteddatetime.Date).ToList();  
                var MatchedResult = from p in sourcePre where source.Any(x => x.ContentId != p.ContentId) select p;
                var PostListTobeRemoved = from s in SavedPostToRemove where MatchedResult.Any(x => x.ContentId == s.ContentId) select s.PostId;
                if (PostListTobeRemoved.Count() != 0)
                { 
                    Context.Set<smPost>().Where(x => PostListTobeRemoved.Contains(x.PostId)).ToList()
                    .ForEach(x => { x.IsDeleted = true; x.DeletedBy = UserId; x.DeletedDate = DateTime.UtcNow; });
                    Context.SaveChanges();
                }

                //+========================================= Update/delete schedule Event detail according to preferences date===================================================+

                var SchedulePost = Context.Set<smScheduleEvents>().Where(x => x.ScheduleTime.Date == Postedtime.Date && UserId == 3 && x.ContentCreatedId == 2).ToList();


                if (SchedulePost.Count()!=0)
                {
                    //SchedulePost.ForEach(x => { x.LocalTime = LocalDAte; });                    
                    Context.Set<smScheduleEvents>().Where(x => PostListTobeRemoved.Contains(x.ContentId) && x.ScheduleTime.Date == Postedtime.Date && UserId == 3 && x.ContentCreatedId == 2).ToList()
                        .ForEach(x => { x.IsDeleted = true; x.DeletedDate = DateTime.UtcNow; x.DeletedBy = UserId; });
                    Context.SaveChanges();
                }
                //+========================================= Update/delete schedule Event detail according to preferences date===================================================+
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            //---=============Not Selected Preference


        }

        public DateTime getTime(string times, int timeoffsets, int dayId)
        {
            DateTime time = Convert.ToDateTime(times);
            DateTime timenew = FromUTCData(time, timeoffsets);
            //int dt = GetDay(DateTime.Now.DayOfWeek.ToString());
            //int nedt = iten.DayId;
            //int remainday = nedt - dt;

            //DateTime date = DateTime.Now.Date.AddDays(Math.Abs(remainday));
            //TimeSpan time = iten.TimeStampPosted.TimeOfDay;
            //DateTime result = date + time;

            // lastMonday is always the Monday before nextSunday.
            // When today is a Sunday, lastMonday will be tomorrow. 
            DateTime d = DateTime.Today;
            int offset = d.DayOfWeek - DayOfWeek.Monday;
            DateTime lastMonday = d.AddDays(-offset);
            DateTime nextMonday = lastMonday.AddDays(7);
            int dayNew = dayId;
            int dayToday = GetDay(DateTime.Now.DayOfWeek.ToString());
            DateTime resTime = DateTime.Now;
            if (dayToday == dayNew) // schedule on same day
            {
                if (time.TimeOfDay > DateTime.Now.TimeOfDay)
                {
                    DateTime date = DateTime.UtcNow.Date;
                    TimeSpan timen = timenew.TimeOfDay;
                    resTime = date + timen;

                }
                else // schedule in next week
                {
                    if (dayNew == 1)
                    {
                        DateTime date = nextMonday.Date;
                        TimeSpan timen = timenew.TimeOfDay;
                        resTime = date + timen;
                    }
                    else if (dayNew == 2)
                    {
                        DateTime date = nextMonday.AddDays(1).Date;
                        TimeSpan timen = timenew.TimeOfDay;
                        resTime = date + timen;
                    }
                    else if (dayNew == 3)
                    {
                        DateTime date = nextMonday.AddDays(2).Date;
                        TimeSpan timen = timenew.TimeOfDay;
                        resTime = date + timen;
                    }
                    else if (dayNew == 4)
                    {
                        DateTime date = nextMonday.AddDays(3).Date;
                        TimeSpan timen = timenew.TimeOfDay;
                        resTime = date + timen;
                    }
                    else if (dayNew == 5)
                    {
                        DateTime date = nextMonday.AddDays(4).Date;
                        TimeSpan timen = timenew.TimeOfDay;
                        resTime = date + timen;

                    }
                    else if (dayNew == 6)
                    {
                        DateTime date = nextMonday.AddDays(5).Date;
                        TimeSpan timen = timenew.TimeOfDay;
                        resTime = date + timen;

                    }
                    else if (dayNew == 7)
                    {
                        DateTime date = nextMonday.AddDays(6).Date;
                        TimeSpan timen = timenew.TimeOfDay;
                        resTime = date + timen;
                    }
                }
            }
            else if (dayToday < dayNew) //schedule in that week 
            {
                int remain = Math.Abs(dayToday - dayNew);
                DateTime nextDate = DateTime.Now.AddDays(remain).Date;
                TimeSpan timen = timenew.TimeOfDay;
                resTime = nextDate + timen;
            }
            else //schedule in next week
            {
                if (dayNew == 1)
                {
                    DateTime date = nextMonday.Date;
                    TimeSpan timen = timenew.TimeOfDay;
                    resTime = date + timen;
                }
                else if (dayNew == 2)
                {
                    DateTime date = nextMonday.AddDays(1).Date;
                    TimeSpan timen = timenew.TimeOfDay;
                    resTime = date + timen;
                }
                else if (dayNew == 3)
                {
                    DateTime date = nextMonday.AddDays(2).Date;
                    TimeSpan timen = timenew.TimeOfDay;
                    resTime = date + timen;
                }
                else if (dayNew == 4)
                {
                    DateTime date = nextMonday.AddDays(3).Date;
                    TimeSpan timen = timenew.TimeOfDay;
                    resTime = date + timen;
                }
                else if (dayNew == 5)
                {
                    DateTime date = nextMonday.AddDays(4).Date;
                    TimeSpan timen = timenew.TimeOfDay;
                    resTime = date + timen;

                }
                else if (dayNew == 6)
                {
                    DateTime date = nextMonday.AddDays(5).Date;
                    TimeSpan timen = timenew.TimeOfDay;
                    resTime = date + timen;

                }
                else if (dayNew == 7)
                {
                    DateTime date = nextMonday.AddDays(6).Date;
                    TimeSpan timen = timenew.TimeOfDay;
                    resTime = date + timen;
                }
            }
            return resTime;
        }
        public int GetDay(string day)
        {
            int Day = 0;
            int Day2 = 0;
            if (day == "Mon" || day == "Monday")
            {
                Day = 1;
                Day2 = 7;
            }
            if (day == "Tue" || day == "Tuesday")
            {
                Day = 2;
                Day2 = 8;
            }
            if (day == "Wed" || day == "Wednesday")
            {
                Day = 3;
                Day2 = 9;
            }
            if (day == "Thr" || day == "Thursday")
            {
                Day = 4;
                Day2 = 10;
            }
            if (day == "Fri" || day == "Friday")
            {
                Day = 5;
                Day2 = 11;
            }
            if (day == "Sat" || day == "Saturday")
            {
                Day = 6;
                Day2 = 12;
            }
            if (day == "Sun" || day == "Sunday")
            {
                Day = 7;
                Day2 = 13;
            }
            return Day;
        }
        //  ===========================================Delete Selected Preferences (Start) =========================================================//
        public dynamic DeleteAutoPreference(int UserId, int DayId, List<string> SelectedPreference, List<string> PageIds, List<string> Time)
        {
            try
            {
                var time = Time[0];
                int TimeOffset = Convert.ToInt32(Time[1]);
                DateTime Stime = Convert.ToDateTime(time);
                DateTime Postedtime = FromUTCData(Stime, TimeOffset);
                TimeSpan timesp = Postedtime.TimeOfDay;
                /// -=========  Delete saved publishtime   list for the day ==========
                var content = Context.Set<smPublishingTime>().Where(x => x.DayId == DayId && x.UserId == UserId ).ToList();
                if (content != null)
                {
                    Context.Set<smPublishingTime>().RemoveRange(content);
                    Context.SaveChanges();
                }
                //========================Set  actual time to post ====================
                DateTime d = DateTime.Today;
                DateTime lastMonday = d.AddDays(TimeOffset);
                DateTime nextMonday = lastMonday.AddDays(7);
                int dayNew = DayId;
                DateTime date = nextMonday.Date;
                int daysUntilT = (((int)DayOfWeek.Monday - (int)d.DayOfWeek + 7) % 7) + DayId == 1 ? 0 : DayId;
                DateTime next = d.AddDays(daysUntilT);
              // DateTime edittedDate = next + timesp;  //// Time to match with actual post 
                DateTime edittedDate = getTime(time, TimeOffset, DayId);
                List<DateTime> DateList = new List<DateTime>()           
                                 {
                                    edittedDate,
                                   edittedDate.AddDays(7),
                                     edittedDate.AddDays(14),
                                    edittedDate.AddDays(21),
                                };
                /// -=========  Delete saved  post detail   for the day ==========
                var SavedPost = Context.Set<smPost>().Where(x => x.ContentCreatedId == 2 && x.UserId == UserId  && x.PostType == 2).ToList();
               // SavedPost = SavedPost.Where(x => DateList.Select(y => y.scheduleDate).Contains(x.PostDate))).ToList();
                SavedPost = SavedPost.Where(x => DateList.Any(y => y.Year == x.PostDate.Year && y.Month == x.PostDate.Month && y.Day == x.PostDate.Day)).ToList();
                if (SavedPost.Count() != 0)
                {
                    SavedPost.ToList().ForEach(x => { x.IsActive = false; x.IsDeleted = true; ;x.DeletedBy = UserId; x.DeletedDate = DateTime.UtcNow; });
                    Context.SaveChanges();
                }
                /// -=========  Delete saved scheduled time  list for the day ==========
                var SavedScheEvent = Context.Set<smScheduleEvents>().Where(x => x.ContentCreatedId == 2 && x.UserId == UserId).ToList();
                SavedScheEvent = SavedScheEvent.Where(x => DateList.Any(y => y.Year == x.ScheduleTime.Year && y.Month == x.ScheduleTime.Month && y.Day == x.ScheduleTime.Day)).ToList();

                if (SavedScheEvent.Count() != 0)
                {
                    SavedScheEvent.ToList().ForEach(x => { x.IsActive = false; x.IsDeleted = true; ;x.DeletedBy = UserId; x.DeletedDate = DateTime.UtcNow; });
                    Context.SaveChanges();
                }

                var SavedsmAutoPreferenc = Context.Set<smAutoPreference>().Where(x => x.Day == DayId && x.UserId == UserId).ToList();
                /// -=========  Delete saved preference  list for the day ==========
                if (SavedsmAutoPreferenc.Count() != 0)
                {
                    Context.Set<smAutoPreference>().RemoveRange(SavedsmAutoPreferenc);
                    Context.SaveChanges();

                }

                /// -=========  Delete saved landing page list for the day ==========
                var SavedsmAutoselectedLandPage = Context.Set<smAutoselectedLandPage>().Where(x => x.DayID == DayId && x.UserId == UserId).ToList();
                if (SavedsmAutoselectedLandPage.Count() != 0)
                {
                    Context.Set<smAutoselectedLandPage>().RemoveRange(SavedsmAutoselectedLandPage);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //  ===========================================Delete Selected Preferences (End) =========================================================//
        #endregion
    }

}
