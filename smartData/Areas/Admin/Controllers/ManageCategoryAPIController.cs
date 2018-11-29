using Core.Domain;
using ServiceLayer.Interfaces;
using smartData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace smartData.Areas.Admin.Controllers
{
    [HandleException]
    public class ManageCategoryAPIController : ApiController, IManageCategoryAPIController
    {
        IManageCategoryService _manageCategoryService;

        public ManageCategoryAPIController(IManageCategoryService _ManageCategoryService)
        {
            _manageCategoryService = _ManageCategoryService;
        }

        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddCategory(string categoryName,string shortname)
        {
           return _manageCategoryService.AddCategory(categoryName,shortname);
        }

        /// <summary>
        /// Edit category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool EditCategory(smIndustry category)
        {
            return _manageCategoryService.EditCategory(category);
        }

        /// <summary>
        /// Delete the admin account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteCategory(int id)
        {
            return _manageCategoryService.DeleteCategory(id);
        }

        /// <summary>
        /// Update status of the content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool UpdateCategoryStatus(int id, bool status)
        {
            return _manageCategoryService.UpdateCategoryStatus(id, status);
        }


        [HttpPost]
        public bool AddSubIndustry(smSubIndustry subIndustry)
        {
            return _manageCategoryService.AddSubIndustry(subIndustry);
        }

        [HttpPost]
        public List<smSubIndustry> GetSubCategoryList(int id)
        {
            return _manageCategoryService.GetSubCategoryList(id);
        }



        [HttpPost]
        public bool EditSubIndustry(smSubIndustry subIndustry)
        {
            return _manageCategoryService.EditSubIndustry(subIndustry);
        }


        [HttpPost]
        public bool DeleteSubIndustry(int id)
        {
            return _manageCategoryService.DeleteSubIndustry(id);
        }

    }
}