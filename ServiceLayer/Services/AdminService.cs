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
    public class AdminService : IAdminService
    {
        IAdminRepository _adminRepository = null;

        #region ctor
        public AdminService(IAdminRepository homeRepository)
        {
            _adminRepository = homeRepository;
        }
        #endregion

        #region public methods
        public List<Users> GetUsersList(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, string Photo, out int total)
        {
            return _adminRepository.GetUserList(limit, offset, order, sort, FirstName, LastName, EmailId, Photo, out  total);
        }

        public List<Users> GetEndUsersList(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, string Photo, int userType, int Role, int industryId, out int total)
        {
            return _adminRepository.GetEndUserList(limit, offset, order, sort, FirstName, LastName, EmailId, Photo, userType, Role, industryId, out  total);
        }

        public List<smContentLibrary> GetContentLists(int limit, int offset, string order, string sort, string socialMedia, string description, string FirstName, string LastName, string CategoryName, DateTime CreatedDate, int UserType, int IndustryId, string PostedBy, DateTime? DateFrom, DateTime? DateTo, string IsArchive, out int total)
        {
            return _adminRepository.GetContentLists(limit, offset, order, sort, socialMedia, description, FirstName, LastName, CategoryName, CreatedDate, UserType, IndustryId, PostedBy, DateFrom, DateTo,IsArchive, out  total);
        }

        public List<smPost> GetPostList(int limit, int offset, string order, string sort, string SocialMedia, string FirstName, string LastName, string Description, out int total, int userType, int industry)
        {
            return _adminRepository.GetPostList(limit, offset, order, sort, SocialMedia, FirstName, LastName, Description, out  total, userType, industry);
        }

        public List<smIndustry> GetCategories(int limit, int offset, string order, string sort, string categoryName, out int total)
        {
            return _adminRepository.GetCategories(limit, offset, order, sort, categoryName, out total);
        }

        public List<smRssFeeds> GetFeeds(int limit, int offset, string order, string sort, int userType, string search, out int total)
        {
            return _adminRepository.GetFeeds(limit, offset, order, sort, userType,  search,out total);
        }


        public List<smSubIndustry> GetAllSubCategoryList(int limit, int offset, string order, string sort, string subIndustry, int IndustryId, int userType, out int total)
        {
            return _adminRepository.GetAllSubCategoryList(limit, offset, order, sort, subIndustry, IndustryId, userType, out total);
        }

        public List<smActivityLog> GetActivityList(int limit, int offset, string order, string sort, string Name, string Email, int userType, int Role, out int total)
        {
            return _adminRepository.GetActivityList(limit, offset, order, sort, Name, Email, userType, Role, out total);
        }

        public List<CustomTermsAndCondition> GetTermsandCondition(int limit, int offset, string order, string sort, out int total)
        {
            return _adminRepository.GetTermsandCondition( limit,  offset,  order,  sort, out  total);
        }
        public List<CustomTermsAndCondition> GetTermsandConditionUser(int userID, int IndustryID)
        {
            return _adminRepository.GetTermsandConditionUser( userID,  IndustryID);
        }
        #endregion
    }
}
