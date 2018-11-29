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
    public class ManageCategoryRepository : BaseRepository<smIndustry>, IManageCategoryRepository
    {
        #region ctor

        public ManageCategoryRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }
        #endregion

        #region public methods
        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool AddCategory(string industryName,string shortname)
        {
            bool status = false;
            try
            {
                smIndustry industry = new smIndustry();
                industry.IndustryName = industryName;
                industry.IndustryShortName = shortname;
                industry.IsActive = true;
                industry.IsDeleted = false;
                Context.Set<smIndustry>().Add(industry);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool EditCategory(smIndustry category)
        {
            bool status = false;
            try
            {
                var objCategory = Context.Set<smIndustry>().Where(x => x.IndustryId == category.IndustryId).FirstOrDefault();
                objCategory.IndustryName = category.IndustryName;
                objCategory.IndustryShortName = category.IndustryShortName;
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        /// <summary>
        /// Delete content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool DeleteCategory(int id)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<smIndustry>().Where(x => x.IndustryId == id).FirstOrDefault();
                objUser.IsDeleted = true;
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
        public bool UpdateCategoryStatus(int id, bool status)
        {
            bool stat = false;
            try
            {
                var objUser = Context.Set<Core.Domain.smIndustry>().Where(x => x.IndustryId == id).FirstOrDefault();
                objUser.IsActive = status;
                Context.SaveChanges();
                stat = true;
            }
            catch
            {
                stat = false;
            }
            return stat;
        }

        /// <summary>
        /// Change User status active/deactive
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public List<smIndustry> GetCategories()
        {
            List<smIndustry> category = new List<smIndustry>();
            try
            {
                category = Context.Set<Core.Domain.smIndustry>().Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x=>x.IndustryName).ToList();
                return category;
            }
            catch
            {
    
            }
            return category;
        }


       

        public bool AddSubIndustry(smSubIndustry subIndustry)
        {
            bool status = false;
            try
            {
                smSubIndustry subindustryobj = new smSubIndustry();
                subindustryobj.SubIndustryName = subIndustry.SubIndustryName;
                subindustryobj.IndustryName = subIndustry.IndustryName;
                subindustryobj.IndustryId = subIndustry.IndustryId;
                subindustryobj.IsActive = true;
                subindustryobj.IsDeleted = false;
                subindustryobj.CreatedDate = DateTime.UtcNow;
                Context.Set<smSubIndustry>().Add(subindustryobj);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }



       public smIndustry GetIndustryByShortName(string shortName)
        {
            smIndustry list = Context.Set<Core.Domain.smIndustry>().Where(x => x.IndustryShortName.ToLower() == shortName.ToLower()).FirstOrDefault();
            return list;

        }

       public List<smSubIndustry> GetSubCategoryList(int id)
        {
            //bool stat = false;
            List<smSubIndustry> list = Context.Set<Core.Domain.smSubIndustry>().Where(x => x.IndustryId == id).ToList();
                //objUser.IsActive = true;
                //Context.SaveChanges();
                //stat = true;           
            return list;
        }



        public  bool EditSubIndustry(smSubIndustry subIndustry)
        {
            bool status = false;
            try
            {
                var objCategory = Context.Set<smSubIndustry>().Where(x => x.SubIndustryId == subIndustry.SubIndustryId).FirstOrDefault();
                objCategory.SubIndustryName = subIndustry.SubIndustryName;
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        /// <summary>
        /// Delete content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
       public bool DeleteSubIndustry(int id)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<smSubIndustry>().Where(x => x.SubIndustryId == id).FirstOrDefault();
                objUser.IsDeleted = true;
                Context.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }

       public smIndustry GetIndustryById(int id)
       {
           var industry = Context.Set<smIndustry>().Where(x => x.IndustryId == id).FirstOrDefault();
           return industry;
       }

   
        #endregion
    }
}
