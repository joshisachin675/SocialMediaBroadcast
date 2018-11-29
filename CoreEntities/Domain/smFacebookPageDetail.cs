

namespace Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("smFacebookPageDetail")]

    public partial class smFacebookPageDetail : BaseEntity
    {
        [Key]
        public int pId { get; set; }
        public string PageAccessToken { get; set; }
        public long PageId { get; set; }
        public string PageName { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public long UserFaceBookID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string category { get; set; }

    }
}
