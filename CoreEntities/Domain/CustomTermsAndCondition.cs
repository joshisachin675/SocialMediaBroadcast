using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class CustomTermsAndCondition
    {
        public int id { get; set; }
        public string TermsConditions { get; set; }
        public DateTime cratedDate { get; set; }
        public int createdBy { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string labelandtittle { get; set; }
        public int id_Industry { get; set; }      
        public string IndustryName { get; set; }
    }
    public class CustomModelForPage
    {
        public string category { get; set; }
        public List<smFacebookPageDetail> dataList { get; set; }
        
    }
    
    public class datalist
    {
        public int pId { get; set; }
        public string PageAccessToken { get; set; }
        public long PageId { get; set; }
        public string PageName { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public long UserFaceBookID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    

}
