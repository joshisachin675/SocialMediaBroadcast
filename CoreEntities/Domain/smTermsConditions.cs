
namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smTermsConditions")]
    public partial class smTermsConditions : BaseEntity
    {
        [Key]
        public int id { get; set; }
        public string TermsConditions { get; set; }
        public DateTime cratedDate { get; set; }
        public int createdBy { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string  labelandtittle  {get;set;}
        public int id_Industry { get; set; }

        [NotMapped]
        public string IndustryName { get; set; }

    }
}