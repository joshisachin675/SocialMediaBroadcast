using AppInterfaces.Repository;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace ServiceLayer.Services
{
    public class ManageContentService : IManageContentService
    {
        IManageContentRepository _manageContentRepository = null;
        #region ctor
        public ManageContentService(IManageContentRepository manageContentRepository)
        {
            _manageContentRepository = manageContentRepository;
        }
        #endregion

        #region public methods
        public bool PostContent(string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, int SubIndustryId, string SubIndustryName, string contentsource, string imgs, string title, string heading, string GroupId)
        {
            return _manageContentRepository.PostContent(message, imageIds, url, userId, categoryId, socialMedia, categoryName, SubIndustryId, SubIndustryName, contentsource, imgs, title, heading, GroupId);
        }

        public bool DeleteContent(string id, int userId)
        {
            return _manageContentRepository.DeleteContent(id, userId);
        }

        public smContentLibrary EditContent(int id)
        {
            return _manageContentRepository.EditContent(id);
        }

        public bool UpdateContentStatus(string id, int userId, bool status)
        {
            return _manageContentRepository.UpdateContentStatus(id, userId, status);
        }

        public bool UpdateContent(int contentId, string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string CategoryName, int SubIndustryId, string SubIndustryName, string imgs, string title, string heading)
        {
            return _manageContentRepository.UpdateContent(contentId, message, imageIds, url, userId, categoryId, socialMedia, CategoryName, SubIndustryId, SubIndustryName, imgs, title, heading);
        }

        public List<smIndustry> GetCategories()
        {
            return _manageContentRepository.GetCategories();
        }
        public List<smSubIndustry> GetAllSubIndustry(int IndustryId)
        {
            return _manageContentRepository.GetAllSubIndustry(IndustryId);
        }

        public smIndustry GetIndustryById(int id)
        {
            return _manageContentRepository.GetIndustryById(id);
        }

        public List<smContentLibrary> CheckDuplicatecontent(string desc, string social, int IndustryId)
        {
            return _manageContentRepository.CheckDuplicatecontent(desc, social, IndustryId);
        }

        public smContentLibrary GetContentById(int Id)
        {
            return _manageContentRepository.GetContentById(Id);
        }

        public smScheduleEvents GetContentId(int id)
        {
            return _manageContentRepository.GetContentId(id);
        }

        public List<smContentLibrary> GetContentByTitle(string Title)
        {
            return _manageContentRepository.GetContentByTitle(Title);
        }

        public smContentLibrary UpdateContentOnSchedular(int contentId, int UserId, int ImpersonateId, int IndustryId)
        {
            return _manageContentRepository.UpdateContentOnSchedular(contentId, UserId, ImpersonateId, IndustryId);
        }
        public bool SaveTermsAndCondition(string termsHtml, int userId, string labelandtittle, string selectedIndustryID)
        {
            return _manageContentRepository.SaveTermsAndCondition(termsHtml, userId, labelandtittle, selectedIndustryID);
        }
        public bool UpdateTermsandConditionDeactive(int id, int userId)
        {
            return _manageContentRepository.UpdateTermsandConditionDeactive(id, userId);
        }
        public bool UpdateTermsandConditionactive(int id, int userId, int id_Industry)
        {
            return _manageContentRepository.UpdateTermsandConditionactive(id, userId, id_Industry);
        }
        public bool DeleteTermsandCondition(int id, int userId)
        {
            return _manageContentRepository.DeleteTermsandCondition(id, userId);
        }
        public bool UpdateTermsAndCondition(string termsHtml, string labelandtittle, string selectedIndustryID, int TermsAndConditionsId)
        {
            return _manageContentRepository.UpdateTermsAndCondition(termsHtml, labelandtittle, selectedIndustryID, TermsAndConditionsId);
        }
        public bool ArchiveContentEnable(string id, int userId, bool status)
        {
            return _manageContentRepository.ArchiveContentEnable(id, userId, status);
        }
        public dynamic EditLeadsAdmin(smHomeValue data)
        {

            return _manageContentRepository.EditLeadsAdmin(data);
        }

        #endregion
    }
}
