
namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    [Table("smAutoselectedLandPage")]
    public partial class smAutoselectedLandPage : BaseEntity
    {
        [Key]
        public int AutoID { get; set; }
        public int UserId { get; set; }
        public int LandingPageID { get; set; }
        public bool IsActive { get; set; }
        public bool Isdeleted { get; set; }
         public int DayID { get; set; }
    

    }
}
