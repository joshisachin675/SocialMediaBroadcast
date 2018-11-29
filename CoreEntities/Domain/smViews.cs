
namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smViews")]
    public partial class smViews : BaseEntity
    {

        [Key]
        public int ViewId { get; set; }    
        public int UserId { get; set; }    
        public string UniquePostId { get; set; }      
        public int Postid { get; set; }
        public DateTime DateEntered { get; set; }
       
    }
}