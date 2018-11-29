namespace Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("smLinkedInPostDetails")]
    public partial class smLinkedInPostDetails : BaseEntity
    {
        [Key]
        public int LId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string LIinkedInId { get; set; }
        public string LinkedInUrl { get; set; }
        public int Partner_Id { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
