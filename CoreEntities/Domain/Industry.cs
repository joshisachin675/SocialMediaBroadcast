namespace Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smIndustry")]
    public partial class smIndustry : BaseEntity
    {
        [Key]
        public int IndustryId { get; set; }
        [Required(ErrorMessage = "Industry name is required")]
        public string IndustryName { get; set; }
         [Required(ErrorMessage = "IndustryShortName name is required")]
        public string IndustryShortName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
