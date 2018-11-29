
namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smDays")]
    public partial class smDays : BaseEntity
    {
        [Key]
        public int DayId { get; set; }
        public string DayName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }

}
