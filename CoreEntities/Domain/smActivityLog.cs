namespace Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smActivityLog")]
    public partial class smActivityLog : BaseEntity
    {
      
      

        [Key]
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public string UserName { get; set; }
        public string AreaAccessed { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Event { get; set; }
        public string Message { get; set; }
        public int CreatedBy { get; set; }
        [NotMapped]
        public string FirstName { get; set; }
        [NotMapped]
        public string LastName { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public int? Role { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string CreatedByEmail { get; set; }
    }
}

