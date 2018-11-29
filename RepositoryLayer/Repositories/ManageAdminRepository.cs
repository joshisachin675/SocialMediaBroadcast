using AppInterfaces.Infrastructure;
using AppInterfaces.Repository;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RepositoryLayer.Repositories
{
    public class ManageAdminRepository : BaseRepository<Users>, IManageAdminRepository
    {
        #region ctor

        public ManageAdminRepository(IAppUnitOfWork uow)
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
        public bool DeleteAdminAccount(int id, int userId)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<Users>().Where(x => x.UserId == id).FirstOrDefault();
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

        public List<Users> GetUsersByEmail(string userName)
        {
            return GetAll().Where(x => x.IsDeleted == false && x.Email.ToLower() == userName.ToLower() && x.Active == true).ToList();
        }



        /// <summary>
        /// Change User status active/deactive
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool UpdateAdminStatus(int id, int userId, bool status)
        {
            bool stat = false;
            try
            {
                var objUser = Context.Set<Core.Domain.Users>().Where(x => x.UserId == id).FirstOrDefault();
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

        public Users GetUserByEmailandIndustry(string email, string indutry)
        {
            return GetAll().Where(x => x.IsDeleted == false && x.Email.ToLower() == email.ToLower() && x.Active == true && x.IndustryName.ToLower() == indutry.ToLower()).FirstOrDefault();
        }

        public Core.Domain.Users GetUserByEmailandIndustryId(string email, int industryId)
        {
            var userDataa = GetAll().Where(x => x.IsDeleted == false && x.Email.ToLower() == email.ToLower()
                && x.Active == true && x.IndustryId == industryId).Select(s => new
                {
                    s.LastChangePasswordDate
                }).FirstOrDefault();

            var userData = Context.Set<Users>().Where(x => x.IsDeleted == false && x.Email.ToLower() == email.ToLower() && x.Active == true && x.IndustryId == industryId).FirstOrDefault();

            if (userData != null)
            {
                userData.LastChangePasswordDate = DateTime.UtcNow.AddHours(2);
                userData.ConfirmPassword = userData.Password;
                Context.SaveChanges();
            }
            userData.LastChangePasswordDate = userDataa.LastChangePasswordDate;
            return userData;
        }
        public string SetSuperAdminEmail()
        {
            return Context.Set<Users>().Where(x => x.UserTypeId == 3).Select(x => x.Email).FirstOrDefault();
        }



        #endregion
    }
}
