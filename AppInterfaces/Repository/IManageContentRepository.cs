using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace AppInterfaces.Repository
{
    public interface IManageContentRepository
    {
        bool PostContent(string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, int SubIndustryId, string SubIndustryName, string contentsource, string imgs, string title, string heading, string GroupId);
        bool DeleteContent(string id, int userId);
        smContentLibrary EditContent(int id);
        bool UpdateContentStatus(string id, int userId, bool status);
        bool UpdateContent(int contentId, string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string CategoryName, int SubIndustryId, string SubIndustryName, string imgs, string title, string heading);
        smIndustry GetIndustryById(int id);
        List<smIndustry> GetCategories();  
        List<smSubIndustry> GetAllSubIndustry(int IndustryId);
        List<smContentLibrary> CheckDuplicatecontent(string desc, string social, int IndustryId);
        smContentLibrary GetContentById(int Id);

        smScheduleEvents GetContentId(int id);
        smContentLibrary UpdateContentOnSchedular(int contentId, int UserId, int ImpersonateId, int IndustryId);

        List<smContentLibrary> GetContentByTitle(string Title);

        bool SaveTermsAndCondition(string termsHtml, int userId,string  labelandtittle, string selectedIndustryID);
        bool UpdateTermsandConditionDeactive(int id, int userId);
        bool UpdateTermsandConditionactive(int id, int userId, int id_Industry);

        bool DeleteTermsandCondition(int id, int userId);
        bool UpdateTermsAndCondition(string termsHtml, string labelandtittle, string selectedIndustryID, int TermsAndConditionsId);
        bool ArchiveContentEnable(string id, int userId, bool status);


        dynamic EditLeadsAdmin(smHomeValue data);
    }
}
