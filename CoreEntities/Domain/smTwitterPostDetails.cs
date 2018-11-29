namespace Core.Domain
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    [Table("smTwitterPostDetails")]
    public partial class smTwitterPostDetails : BaseEntity
    {

        [Key]
        public int TId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string TwitterId { get; set; }
        public int Partner_Id { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
