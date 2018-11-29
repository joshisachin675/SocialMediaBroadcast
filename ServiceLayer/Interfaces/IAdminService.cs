using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAdminService
    {
        List<Users> GetUsersList(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, string Photo, out int total);
        List<Users> GetEndUsersList(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, string Photo, int userType,int Role,int industryId, out int total);
        List<smContentLibrary> GetContentLists(int limit, int offset, string order, string sort, string socialMedia, string description, string FirstName, string LastName, string CategoryName, DateTime CreatedDate, int UserType, int IndustryId, string PostedBy, DateTime? DateFrom, DateTime? DateTo, string IsArchive, out int total);
        List<smPost> GetPostList(int limit, int offset, string order, string sort, string SocialMedia, string FirstName, string LastName, string Description, out int total, int userType, int industry);
        List<smIndustry> GetCategories(int limit, int offset, string order, string sort, string categoryName, out int total);
        List<smRssFeeds> GetFeeds(int limit, int offset, string order, string sort, int userType, string search, out int total);
        List<smSubIndustry> GetAllSubCategoryList(int limit, int offset, string order, string sort, string subIndustry, int IndustryId, int userType, out int total);
        List<smActivityLog> GetActivityList(int limit, int offset, string order, string sort, string Name, string Email, int userType, int Role, out int total);

        List<CustomTermsAndCondition> GetTermsandCondition(int limit, int offset, string order, string sort, out int total);

        List<CustomTermsAndCondition> GetTermsandConditionUser(int userID, int IndustryID);
    }
}
