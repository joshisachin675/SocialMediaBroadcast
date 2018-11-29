
namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smRssFeeds")]
    public partial class smRssFeeds : BaseEntity
    {
        [Key]
        public int FeedId { get; set; }
        public int UserId { get; set; }
        public string FeedName { get; set; }
        public string FeedUrl { get; set; }
        public bool IsActive { get; set; }
        public int Limit { get; set; }
        public bool IsApproved { get; set; }
        public string UserType { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int DeletedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public Nullable<DateTime> DateProcess { get; set; }

    }

}