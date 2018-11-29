using AppInterfaces.Infrastructure;
using AppInterfaces.Repository;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class ManageUserRepository : BaseRepository<Users>, IManageUserRepository
    {
        #region ctor

        public ManageUserRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }
        #endregion

        #region public methods
        /// <summary>
        /// Delete clinic staff
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool DeleteUserAccount(int id, int userId)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<Users>().Where(x => x.UserId == id).FirstOrDefault();
                objUser.Password = objUser.Password;
                objUser.ConfirmPassword = objUser.Password;
                objUser.IsDeleted = true;
                objUser.DeletedBy = userId;
                objUser.DeletedDate = DateTime.UtcNow;
                Context.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }

        /// <summary>
        /// Change User status active/deactive
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool UpdateUserStatus(int id, int userId, bool status)
        {
            bool stat = false;
            try
            {
                var objUser = Context.Set<Core.Domain.Users>().Where(x => x.UserId == id).FirstOrDefault();
                objUser.ConfirmPassword = objUser.Password;
                objUser.Active = status;
                Context.SaveChanges();
                stat = true;
            }
            catch
            {
                stat = false;
            }
            return stat;
        }

        public List<Users> GetUsersByEmail(string userName)
        {
            return GetAll().Where(x => x.IsDeleted == false && x.Email.ToLower() == userName.ToLower() && x.Active == true).ToList();
        }

        public bool UpdateUserPassword(int UserId, string FirstName, string LastName, string UserName, int IndustryId, string IndustryName, string password)
        {
            Users users = Context.Set<Users>().Where(x => x.UserId == UserId).FirstOrDefault();
            
            try
            {
                users.FirstName = FirstName;
                users.LastName = LastName;
                users.Email = UserName;
                users.IndustryId = IndustryId;
                users.IndustryName = IndustryName;
                users.Password = password;
                users.ConfirmPassword = password;
                base.Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Users GetPassword(int userId)
        {
            var user = Context.Set<Users>().Where(x => x.UserId == userId).FirstOrDefault();
            return user;
        }
        #endregion

    }
}
