namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("smUserActiveSocialMedia")]
    public partial class smUserActiveSocialMedia : BaseEntity
    {
        [Key]
        public int SId { get; set; }
        public int UserId { get; set; }
        public string SocialMedia { get; set; }
        public bool IsActive { get; set; }
        public int Partner_Id { get; set; }
    }
}
