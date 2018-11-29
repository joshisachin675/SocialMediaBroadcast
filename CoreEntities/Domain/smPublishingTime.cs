
namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smPublishingTime")]
    public partial class smPublishingTime : BaseEntity
    {
        [Key]
        public int PublishingTimeId { get; set; }
        public int DayId { get; set; }
        public TimeSpan TimeStampPosted { get; set; }
        public string Time { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }      
        public int DeletedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        [NotMapped]
        public string Day { get; set; }
    }

}