

namespace Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("smFacebookDefaultPreference")]

    public partial class smFacebookDefaultPreference : BaseEntity
    {
        [Key]
        public int PreferenceId { get; set; }
        public int userID { get; set; }
        public long PageId { get; set; }
        public int Type { get; set; }    
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
   

    }
}
