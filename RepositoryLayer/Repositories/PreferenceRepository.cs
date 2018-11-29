using AppInterfaces.Infrastructure;
using AppInterfaces.Repository;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace RepositoryLayer.Repositories
{
    public class PreferenceRepository : BaseRepository<smPreference>, IPreferenceRepository
    {

        #region
        public PreferenceRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }
        #endregion
        public List<smPreference> GetPreference(int userId)
        {
            List<smPreference> smPreference = new List<smPreference>();
            try
            {
                smPreference = Context.Set<smPreference>().Where(x => x.UserId == userId && x.IsDeleted==false).ToList();
                if (smPreference.Count > 0)
                {
                    foreach (var data in smPreference)
                    {
                        //data.PostDate = data.PostDate.ToLocalTime();
                        data.CreatedDate = data.CreatedDate.ToLocalTime();

                    }
                }

            }
            catch (Exception ex)
            {

            }
            return smPreference;
        }
        public List<smPreference> GetAllSocialPreference(int limit, int offset, string order, string sort, string Preference, int currentUserId, out int total)
        {
            AzureDBContext db = new AzureDBContext();

            var data = Context.Set<smPreference>().Where(x => x.UserId == currentUserId && x.IsDeleted==false);

            if (!string.IsNullOrEmpty(Preference))
            {
                data = data.Where(x => x.Preference.Contains(Preference));
            }

            //Order by
            GetSortedPreference(ref data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }


        public void GetSortedPreference(ref IQueryable<smPreference> obj, string Case)
        {
            switch (Case)
            {
                case "PreferenceASC":
                    obj = obj.OrderBy(x => x.Preference.ToLower());
                    break;
                case "PreferenceDESC":
                    obj = obj.OrderByDescending(x => x.Preference.ToLower());
                    break;

                case "CreatedDateDateASC":
                    obj = obj.OrderBy(x => x.CreatedDate.ToString());
                    break;
                case "PostDateDESC":
                    obj = obj.OrderByDescending(x => x.CreatedDate.ToString());
                    break;
                default:
                    obj = obj.OrderBy(x => x.CreatedDate);
                    break;
            }
        }
        public smPreference EditPreference(int id)
        {
            var objContent = Context.Set<smPreference>().Where(x => x.PreferenceId == id).FirstOrDefault();
            return objContent;
        }

        public bool UpdatePreference(int PreferenceId, string Preference, int userId,int ImpersonateUserId)
        {
            bool status = false;
            try
            {
                var objContent = Context.Set<smPreference>().Where(x => x.PreferenceId == PreferenceId).FirstOrDefault();
                if (objContent != null)
                {
                    objContent.Preference = Preference;
                    if (ImpersonateUserId != 0)
                    {
                        objContent.ModifiedBy = ImpersonateUserId;
                    }
                    else
                    {
                        objContent.ModifiedBy = userId;
                    }
                    objContent.ModifiedDate = DateTime.UtcNow;
                    Context.SaveChanges();
                    status = true;
                }
            }
            catch
            {
                status = false;
            }
            return status;
        }


        public bool DeletePreference(string name, int userId,int ImpersonateUserId)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<smPreference>().Where(x => x.UserId == userId && x.Preference == name).FirstOrDefault();
                objUser.IsDeleted = true;
                if (ImpersonateUserId != 0)
                {
                    objUser.DeletedBy = ImpersonateUserId;
                }
                else
                {
                    objUser.DeletedBy = userId;
                }
                objUser.DeletedDate = DateTime.UtcNow;
                //Context.Set<smPreference>().Remove(objUser);
                Context.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }

       public bool DeleteAllPreference(List<smSubIndustry> list, int userId, int ImpersonateUserId)
        {
            bool status = false;
            try
            {
                foreach (var item in list)
                {
                    var objUser = Context.Set<smPreference>().Where(x => x.UserId == userId && x.Preference == item.SubIndustryName).ToList();
                    foreach (var usr in objUser)
                    {
                        usr.IsDeleted = true;
                        if (ImpersonateUserId != 0)
                        {
                            usr.DeletedBy = ImpersonateUserId;
                        }
                        else
                        {
                            usr.DeletedBy = userId;
                        }
                        usr.DeletedDate = DateTime.UtcNow;
                        Context.Set<smPreference>().Remove(usr);
                    }                                 
                    Context.SaveChanges();
                    status = true;
                }               
            }
            catch
            {
                status = false;
            }
            return status;
        }

        public bool AddPreference(string Preference, int userId, int ImpersonateUserId)
        {
            bool status = false;
            try
            {
               // smPreference category = new smPreference();
               // category.Preference = Preference;
               // category.CreatedDate = DateTime.UtcNow;
               // //category.ModifiedDate = DateTime.UtcNow;
               // category.UserId = userId;
               // if (ImpersonateUserId != 0)
               // {
               //     category.CreatedBy = ImpersonateUserId;
               // }
               // else
               // {
               //     category.CreatedBy = userId;
               // }
               //Context.Set<smPreference>().Add(category);
               // Context.SaveChanges();


                var category = Context.Set<smPreference>().Where(x => x.Preference == Preference && x.UserId == userId).FirstOrDefault();

                if (category!=null)
                {
                    category.Preference = Preference;
                    category.CreatedDate = DateTime.UtcNow;
                    //category.ModifiedDate = DateTime.UtcNow;
                    category.UserId = userId;
                    if (ImpersonateUserId != 0)
                    {
                        category.CreatedBy = ImpersonateUserId;
                    }
                    else
                    {
                        category.CreatedBy = userId;
                    }
                    category.IsDeleted = false;

                    //  Context.Set<smPreference>().Add(category);
                    Context.SaveChanges();

                    status = true;
                }
                else
                {
                    smPreference categorys = new smPreference();
                    categorys.Preference = Preference;
                    categorys.CreatedDate = DateTime.UtcNow;
                    //category.ModifiedDate = DateTime.UtcNow;
                    categorys.UserId = userId;
                    if (ImpersonateUserId != 0)
                    {
                        categorys.CreatedBy = ImpersonateUserId;
                    }
                    else
                    {
                        categorys.CreatedBy = userId;
                    }
                    Context.Set<smPreference>().Add(categorys);
                    Context.SaveChanges();
                }


              
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }


        public bool AddAllPreference(List<smSubIndustry> list, int userId, int ImpersonateUserId)
        {
            bool status = false;
            try
            {
                foreach (var item in list)
                {
                    //smPreference category = new smPreference();
                    //category.Preference = item.SubIndustryName;
                    //category.CreatedDate = DateTime.UtcNow;
                    ////category.ModifiedDate = DateTime.UtcNow;
                    //category.UserId = userId;
                    //if (ImpersonateUserId != 0)
                    //{
                    //    category.CreatedBy = ImpersonateUserId;
                    //}
                    //else
                    //{
                    //    category.CreatedBy = userId;
                    //}
                    //Context.Set<smPreference>().Add(category);
                    //Context.SaveChanges();
                //    status = true;






                    var category = Context.Set<smPreference>().Where(x => x.Preference == item.SubIndustryName && x.UserId == userId).FirstOrDefault();
                    if (category!=null)
                    {
                        category.Preference = item.SubIndustryName;
                        category.CreatedDate = DateTime.UtcNow;
                        //category.ModifiedDate = DateTime.UtcNow;
                        category.UserId = userId;
                        if (ImpersonateUserId != 0)
                        {
                            category.CreatedBy = ImpersonateUserId;
                        }
                        else
                        {
                            category.CreatedBy = userId;
                        }
                        category.IsDeleted = false;

                        //  Context.Set<smPreference>().Add(category);
                        Context.SaveChanges();

                        status = true;
                    }
                    else
                    {
                        smPreference categorys = new smPreference();
                        categorys.Preference = item.SubIndustryName;
                        categorys.CreatedDate = DateTime.UtcNow;
                        //category.ModifiedDate = DateTime.UtcNow;
                        categorys.UserId = userId;
                        if (ImpersonateUserId != 0)
                        {
                            categorys.CreatedBy = ImpersonateUserId;
                        }
                        else
                        {
                            categorys.CreatedBy = userId;
                        }
                        Context.Set<smPreference>().Add(categorys);
                        Context.SaveChanges();
                        status = true;
                    }

                 

                }
              
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
    }
}

