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
    [Table("Countries")]
    public partial class Countries : BaseEntity
    {
        [Key]
        public int CountryId { get; set; }
        public string ISO { get; set; }
        public string Name  { get; set; }
        public string NiceName { get; set; }
        public string ISO3 { get; set; }
        public int NumCode { get; set; }
        public int PhoneCode { get; set; }
        public bool IsActive { get; set; }
 

       
    
    }
}
