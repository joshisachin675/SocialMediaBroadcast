
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
    [Table("smContentstatus")]
    public partial class smContentstatus : BaseEntity
    {
        [Key]
        public int ConId { get; set; }
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int DeletedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
    }
}

