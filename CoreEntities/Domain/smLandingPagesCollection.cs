namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smLandingPagesCollection")]
    public partial class smLandingPagesCollection
    {
        [Key]
        public int id { get; set; }
        public int id_industry { get; set; }
        public string label { get; set; }
        public string url { get; set; }
        public int sortorder { get; set; }
        public bool active { get; set; }
    }
}
