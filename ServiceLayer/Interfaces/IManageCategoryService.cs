using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IManageCategoryService
    {
        bool AddCategory(string categoryName, string shortname);
        bool EditCategory(smIndustry category);
        bool DeleteCategory(int id);
        bool UpdateCategoryStatus(int id, bool status);
        bool AddSubIndustry(smSubIndustry subIndustry);
        smIndustry GetIndustryByShortName(string shortName);
        List<smSubIndustry> GetSubCategoryList(int id);
        bool EditSubIndustry(smSubIndustry subIndustry);
        bool DeleteSubIndustry(int id);
        smIndustry GetIndustryById(int id);
    }
}
