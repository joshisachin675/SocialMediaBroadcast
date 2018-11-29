namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smSocialMediaProfile")]
    public partial class smSocialMediaProfile : BaseEntity
    {
        [Key]
        public int Fid { get; set; }
        public int UserId { get; set; }
        public string Link { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public string SocialMediaId { get; set; }
        public bool IsActive { get; set; }
        public int Partner_Id { get; set; }
        public string SocialMedia { get; set; }
        public string AccessToken { get; set; }
        public string TokenSecret { get; set; }
        public bool IsAccountActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int DeletedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public int AccountActiveBy { get; set; }
        public int AccountDeactiveBy { get; set; }

    }
}

