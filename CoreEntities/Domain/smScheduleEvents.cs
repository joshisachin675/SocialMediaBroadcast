namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smScheduleEvents")]
    public partial class smScheduleEvents : BaseEntity
    {
        public int ContentId { get;  set; }
        public int CreatedBy { get;  set; }
        public DateTime CreatedDate { get;  set; }
        public int DeletedBy { get;  set; }
        public DateTime DeletedDate { get;  set; }
        [Key]
        public int EventId { get;  set; }
        public string Evnt_Id { get;  set; }
        public bool IsActive { get;  set; }
        public bool IsDeleted { get;  set; }
        public bool IsFacebook { get;  set; }
        public bool IsLinkedIn { get;  set; }
        public bool IsPosted { get;  set; }
        public bool IsTwitter { get;  set; }
        public DateTime LocalTime { get;  set; }
        public DateTime ScheduleTime { get;  set; }
        public string Title { get;  set; }
        public int UserId { get;  set; }
        public int ContentCreatedId { get; set; }
        

    }

}
