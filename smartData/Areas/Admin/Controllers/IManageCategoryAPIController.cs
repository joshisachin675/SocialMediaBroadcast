using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Areas.Admin.Controllers
{
    public interface IManageCategoryAPIController
    {
        bool AddCategory(string CategoryName, string shortname);
        bool EditCategory(smIndustry category);
        bool DeleteCategory(int id);
        bool UpdateCategoryStatus(int id, bool status);
        bool AddSubIndustry(smSubIndustry subIndustry);
        List<smSubIndustry> GetSubCategoryList(int id);
        bool EditSubIndustry(smSubIndustry subIndustry);
        bool DeleteSubIndustry(int id);
       
    }
}