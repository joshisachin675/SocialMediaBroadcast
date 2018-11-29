using Core.Domain;
using ServiceLayer.Services;
using smartData.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace smartData.Areas.Admin.Controllers
{
    public class ManageCategoryController : Controller
    {
        #region Global Variables
        ServiceLayer.Interfaces.IManageCategoryService _manageCategoryService;
        IManageCategoryAPIController _manageCategoryAPIController;
        IManageContentAPIController _manageContentService;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        #endregion

        #region constructor
        public ManageCategoryController(ManageCategoryService manageContentService, IManageCategoryAPIController manageCategoryAPIController, IManageContentAPIController manageContentApiService)
        {
            _manageCategoryService = manageContentService;
            _manageCategoryAPIController = manageCategoryAPIController;
            _manageContentService = manageContentApiService;
        }
        #endregion

        /// <summary>
        /// Show category list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuditLog(Event = "Add industry", Message = "Add industry")]
        public ActionResult AddCategory(smIndustry industry)
        {
            bool status = _manageCategoryAPIController.AddCategory(industry.IndustryName, industry.IndustryShortName);
            return Json(new { status = status });
        }

        /// <summary>
        /// Edit industry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Edit industry", Message = "Edit industry")]
        public JsonResult EditCategory(smIndustry industry)
        {
            bool status = _manageCategoryAPIController.EditCategory(industry);
            return Json(new { result = status });
        }

        /// <summary>
        /// Delete the category by superadmin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Delete industry", Message = "Delete industry")]
        public JsonResult DeleteCategory(int id)
        {
            bool status = _manageCategoryAPIController.DeleteCategory(id);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Update industry status", Message = "Update industry status to active")]
        public JsonResult UpdateCategoryStatusActive(int id)
        {
            bool stat = true;
            bool status = _manageCategoryAPIController.UpdateCategoryStatus(id, stat);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Update industry status", Message = "Update industry status to deactive")]
        public JsonResult UpdateCategoryStatusDeactive(int id)
        {
            bool stat = false;
            bool status = _manageCategoryAPIController.UpdateCategoryStatus(id, stat);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add sub industry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuditLog(Event = "Add sub industry", Message = "Add sub industry specific ")]
        public ActionResult AddSubIndustry(smSubIndustry subIndustry)
        {
            subIndustry.CreatedBy = smartData.Common.SessionManager.LoggedInUser.UserID;
            if (subIndustry.IndustryId == 0)
            {
                subIndustry.IndustryId = smartData.Common.SessionManager.LoggedInUser.IndustryId;
            }
            smIndustry smIndustry = _manageContentService.GetIndustryById(subIndustry.IndustryId);
            subIndustry.IndustryName = smIndustry.IndustryName;
            bool status = _manageCategoryAPIController.AddSubIndustry(subIndustry);
            return Json(new { status = status });
        }

        /// <summary>
        /// Get sub industries
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuditLog(Event = "Get sub categories", Message = "Get sub categories")]
        public ActionResult GetAllSubCategory()
        {
            // List<smSubIndustry> listsubIndustry = _manageCategoryAPIController.GetAllSubCategoryList();
            return View();
        }

        /// <summary>
        /// Edit sub indusrty
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Edit sub industry", Message = "Edit sub industry")]
        public JsonResult EditSubIndustry(smSubIndustry subIndustry)
        {
            bool status = _manageCategoryAPIController.EditSubIndustry(subIndustry);
            return Json(new { result = status });
        }

        /// <summary>
        /// Delete sub industry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Delete sub industry", Message = "Delete sub industry")]
        public JsonResult DeleteSubIndustry(int id)
        {
            bool status = _manageCategoryAPIController.DeleteSubIndustry(id);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        public string CheckDuplicateSubIndustry(int Indusrtyid, string subIndustryName)
        {
            var list = _manageCategoryAPIController.GetSubCategoryList(Indusrtyid).Where(x => x.SubIndustryName.ToLower() == subIndustryName.ToLower()).ToList();
            if (list.Count > 0)
            {
                return "exists";
            }
            else
            {
                return "notexists";
            }           
        }


        [HttpGet]
        public string CheckDuplicateIndustry(string IndustryName)
        {
            var list = _manageContentService.GetCategories().Where(x => x.IndustryName.ToLower() == IndustryName.ToLower()).ToList();
            if (list.Count > 0)
            {
                return "exists";
            }
            else
            {
                return "notexists";
            }           
        }

        [HttpGet]
        public string CheckDuplicateShortIndustry(string shortName)
        {
            var list = _manageContentService.GetCategories().Where(x => x.IndustryShortName.ToLower() == shortName.ToLower()).ToList();
            if (list.Count > 0)
            {
                return "exists";
            }
            else
            {
                return "notexists";
            }
        }

    }
}