namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smLandingPagesForUsers")]
    public partial class smLandingPagesForUsers
    {

         [Key]
        public int id {get ; set;}
        public int id_user {get ; set;}
        public int id_landingpage {get;set;}
        public bool IsActive { get; set; }
    }
}
