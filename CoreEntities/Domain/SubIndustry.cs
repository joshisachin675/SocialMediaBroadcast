namespace Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smSubIndustry")]
    public partial class smSubIndustry : BaseEntity
    {
        [Key]
        public int SubIndustryId { get; set; }
        [Required(ErrorMessage = "Industry name is required")]
        public string SubIndustryName { get; set; }
        public int IndustryId { get; set; }
        public string IndustryName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> CreatedDate{get;set;}
        public int CreatedBy {get;set;}
        public Nullable<DateTime> DeletedDate{get; set;}
        public int DeletedBy { get; set; }

        [NotMapped]
        public bool Preference { get; set; }
        
        [NotMapped]
        public List<smSubIndustry> dataList { get; set; }

    }
}

