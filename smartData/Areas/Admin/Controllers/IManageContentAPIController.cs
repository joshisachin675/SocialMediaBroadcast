using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;

namespace smartData.Areas.Admin.Controllers
{
    public interface IManageContentAPIController
    {
        bool PostContent(string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, int SubIndustryId, string SubIndustryName, string contentsource, string imgs, string title, string heading, string GroupId);
        bool DeleteContent(string id, int userId);
        smContentLibrary EditContent(int id);
        bool UpdateContentStatus(string id, int userId, bool status);
        bool UpdateContent(int contentId, string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string CategoryName, int SubIndustryId, string SubIndustryName, string imgs, string title, string heading);
        List<smIndustry> GetCategories();
        smIndustry GetIndustryById(int id);
        List<smSubIndustry> GetAllSubIndustry(int IndustryId);


        bool SaveTermsAndCondition(string termsHtml, int userId ,string  labelandtittle, string selectedIndustryID);


        bool UpdateTermsandConditionactive(int id, int userId, int id_Industry);
        bool UpdateTermsandConditionDeactive(int id, int userId);

        bool DeleteTermsandCondition(int id, int userId);

        bool UpdateTermsAndCondition(string termsHtml, string labelandtittle, string selectedIndustryID, int TermsAndConditionsId);
        bool ArchiveContentEnable(string id, int userId,bool status);
    }
}