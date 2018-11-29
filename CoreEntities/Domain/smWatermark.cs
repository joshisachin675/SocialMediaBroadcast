namespace Core.Domain
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("smWatermark")]
    public partial class smWatermark : BaseEntity
    {
        [Key]
        public int ImageId { get; set; }
        public string ImagePathLogo { get; set; }
        public string ImageText { get; set; }
        public int TextWidth { get; set; }
        public int TextSiz { get; set; }
        public string Textcolor { get; set; }
        public string TextBg { get; set; }
        public string Gravity { get; set; }
        public string Fontfamily { get; set; }
        public double Opacity { get; set; }
        public int Margin { get; set; }
        public string OutputWidth { get; set; }
        public string OutputHeight { get; set; }
        public string OutPutType { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public int UserID { get; set; }
    }

}