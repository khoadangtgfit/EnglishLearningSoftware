namespace English_Learning_Software1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vocabulary")]
    public partial class Vocabulary
    {
        [Key]
        public int Vocabulary_Code { get; set; }

        [StringLength(25)]
        public string English_Vocabulary { get; set; }

        [StringLength(25)]
        public string Vietnamese_Vocabulary { get; set; }

        public int? Vocabulary_Type_Code { get; set; }

        [Column(TypeName = "image")]
        public byte[] Vocabulary_Image { get; set; }

        public virtual Vocabulary_Type Vocabulary_Type { get; set; }
    }
}
