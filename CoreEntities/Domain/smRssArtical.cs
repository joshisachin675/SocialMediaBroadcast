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
    [Table("smRssArtical")]
    public partial class smRssArtical : BaseEntity
    {
        [Key]
        public int RssId { get; set; }
        public int UserId { get; set; }   
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsIgnored { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
      
       
    
    }
}
