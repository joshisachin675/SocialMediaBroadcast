
namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smPreference")]
    public partial class smPreference : BaseEntity
    {
        [Key]
        public int PreferenceId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Preference { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public Nullable<DateTime> ModifiedDate { get; set; }
         public int CreatedBy { get; set; }
         public int DeletedBy { get; set; }
         public Nullable<DateTime> DeletedDate { get; set; }
         public int ModifiedBy { get; set; }


    }

}