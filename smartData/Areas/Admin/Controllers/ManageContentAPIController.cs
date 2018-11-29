using ServiceLayer.Interfaces;
using smartData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Domain;

namespace smartData.Areas.Admin.Controllers
{
    [HandleException]
    public class ManageContentAPIController : ApiController, IManageContentAPIController
    {
        IManageContentService _manageContentService;


        public ManageContentAPIController(IManageContentService _ManageContentService)
        {
            _manageContentService = _ManageContentService;
        }

        /// <summary>
        /// Post content to the content library
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool PostContent(string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, int SubIndustryId, string SubIndustryName, string contentsource, string imgs, string title, string heading , string GroupId)
        {
            return _manageContentService.PostContent(message, imageIds, url, userId, categoryId, socialMedia, categoryName, SubIndustryId, SubIndustryName, contentsource, imgs, title, heading, GroupId);
        }

        /// <summary>
        /// Delete the admin account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteContent(string id, int userId)
        {
            return _manageContentService.DeleteContent(id, userId);
        }

        /// <summary>
        /// Edit the content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public smContentLibrary EditContent(int id)
        {
            return _manageContentService.EditContent(id);
        }

        /// <summary>
        /// Update status of the content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool UpdateContentStatus(string id, int userId, bool status)
        {
            return _manageContentService.UpdateContentStatus(id, userId, status);
        }

        /// <summary>
        /// Update status of the content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool UpdateContent(int contentId, string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string CategoryName, int SubIndustryId, string SubIndustryName, string imgs, string title, string heading)
        {
            return _manageContentService.UpdateContent(contentId, message, imageIds, url, userId, categoryId, socialMedia, CategoryName, SubIndustryId, SubIndustryName, imgs, title, heading);
        }

        /// <summary>
        /// Get catgeories list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public List<smIndustry> GetCategories()
        {
            return _manageContentService.GetCategories();
        }

        public List<smSubIndustry> GetAllSubIndustry(int IndustryId)
        {
            return _manageContentService.GetAllSubIndustry(IndustryId);
        }

        [HttpPost]
        public smIndustry GetIndustryById(int id)
        {
            return _manageContentService.GetIndustryById(id);
        }

        public bool SaveTermsAndCondition(string termsHtml, int userId, string labelandtittle, string selectedIndustryID)
        {
            return _manageContentService.SaveTermsAndCondition(termsHtml, userId ,  labelandtittle,  selectedIndustryID);
        }

        public bool UpdateTermsandConditionDeactive(int id, int userId)
        {
            return _manageContentService.UpdateTermsandConditionDeactive(id, userId);
        }
        public bool UpdateTermsandConditionactive(int id, int userId, int id_Industry)
        {
            return _manageContentService.UpdateTermsandConditionactive(id, userId, id_Industry);
        }
        public bool DeleteTermsandCondition (int id , int userId)
        {
            return _manageContentService.DeleteTermsandCondition(id, userId);
        }
        public bool UpdateTermsAndCondition(string termsHtml, string labelandtittle, string selectedIndustryID, int TermsAndConditionsId)
        {
            return _manageContentService.UpdateTermsAndCondition( termsHtml,  labelandtittle,  selectedIndustryID,  TermsAndConditionsId);
        }

        public bool ArchiveContentEnable(string id, int userrId, bool Status)
        {
            return _manageContentService.ArchiveContentEnable(id, userrId, Status);
        }


    }
}
