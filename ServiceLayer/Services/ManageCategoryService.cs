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
    public class ManageCategoryService : IManageCategoryService
    {
        IManageCategoryRepository _manageCategoryRepository = null;
        #region ctor
        public ManageCategoryService(IManageCategoryRepository manageContentRepository)
        {
            _manageCategoryRepository = manageContentRepository;
        }
        #endregion

        #region public methods
        public bool AddCategory(string categoryName,string shortname)
        {
            return _manageCategoryRepository.AddCategory(categoryName,shortname);
        }

        public bool EditCategory(smIndustry category)
        {
            return _manageCategoryRepository.EditCategory(category);
        }

        public bool DeleteCategory(int id)
        {
            return _manageCategoryRepository.DeleteCategory(id);
        }

        public bool UpdateCategoryStatus(int id, bool status)
        {
            return _manageCategoryRepository.UpdateCategoryStatus(id, status);
        }

        public bool AddSubIndustry(smSubIndustry subIndustry)
        {
            return _manageCategoryRepository.AddSubIndustry(subIndustry);
        }

        public smIndustry GetIndustryByShortName(string shortName)
        {
            return _manageCategoryRepository.GetIndustryByShortName(shortName);
        }

        public List<smSubIndustry> GetSubCategoryList(int id)
        {
            return _manageCategoryRepository.GetSubCategoryList(id);
        }

        public bool EditSubIndustry(smSubIndustry subIndustry)
        {
            return _manageCategoryRepository.EditSubIndustry(subIndustry);
        }

        public bool DeleteSubIndustry(int id)
        {
            return _manageCategoryRepository.DeleteSubIndustry(id);
        }

        public smIndustry GetIndustryById(int id)
        {
            return _manageCategoryRepository.GetIndustryById(id);
        }

        
        #endregion
    }
}
