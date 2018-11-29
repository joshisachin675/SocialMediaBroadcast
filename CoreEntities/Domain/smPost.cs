
namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smPost")]
    public partial class smPost : BaseEntity
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int SocialMediaProfileId { get; set; }
        public string SocialMedia { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public int EventId { get;  set; }

        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPosted { get; set; }
        public int? PostType { get; set; }
        public DateTime PostDate { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public string LikesNames { get; set; }
        public string CommentsText { get; set; } 
        //public string FBId { get; set; }
        // public string FBPostID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int DeletedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string UniquePostId { get; set; }
        public int ContentId { get; set; }
        public int? ContentCreatedId { get; set; }
        
        [NotMapped]
        public string FirstName { get; set; }
        [NotMapped]
        public string LastName { get; set; }
        [NotMapped]
        public string CreatedByName { get; set; }
        [NotMapped]
        public int? RoleType { get; set; }
    }

}