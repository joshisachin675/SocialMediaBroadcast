using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInterfaces.Repository
{
    public interface IManageCategoryRepository
    {
        bool AddCategory(string CategoryName,string shortname);
        bool EditCategory(smIndustry category);
        bool DeleteCategory(int id);
        bool UpdateCategoryStatus(int id, bool status);
        List<smIndustry> GetCategories();    
        bool AddSubIndustry(smSubIndustry subIndustry);
        List<smSubIndustry> GetSubCategoryList(int id);
        bool EditSubIndustry(smSubIndustry subIndustry);
        bool DeleteSubIndustry(int id);
        smIndustry GetIndustryById(int id);
        smIndustry GetIndustryByShortName(string shortName);
    }
}
