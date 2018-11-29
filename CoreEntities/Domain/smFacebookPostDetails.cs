namespace Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("smFacebookPostDetails")]
    public partial class smFacebookPostDetails : BaseEntity
    {
        [Key]
        public int FId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string FBId { get; set; }
        public string FBPostId { get; set; }
        public string Type { get; set; }
        public DateTime AddedDate { get; set; }

    }
}
