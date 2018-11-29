namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    [Table("smContentLibrary")]
    public partial class smContentLibrary : BaseEntity
    {
        [Key]
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public string SocialMedia { get; set; }
        public string Title { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Tags { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public int DeletedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubIndustryName { get; set; }
        public int SubIndustryId { get; set; }
        public string ContentSource { get; set; }
        public string TextDescription { get; set; }
        public string GroupId { get; set; }
        public bool Archive { get; set; }
        public string OriginalTitle { get; set; }
        [NotMapped]
        public SelectList categoryList { get; set; }
        [NotMapped]
        public string CreatedByName { get; set; }
        [NotMapped]
        public int? UserType { get; set; }
        [NotMapped]
        public string PostDescription { get; set; }
        [NotMapped]
        public List<smSocialMediaProfile> socialMediaProfile { get; set; }
     
    }
}
