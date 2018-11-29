namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smAutoPreference")]
    public partial class smAutoPreference : BaseEntity
    {
        [Key]
        public int AutoPreferenceId { get; set; }
        public int SubindustryID { get; set; }
        public string SubindustryName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int Day { get; set; }

    
    }
}
