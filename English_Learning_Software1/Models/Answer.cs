namespace English_Learning_Software1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        [Key]
        public int Answer_Code { get; set; }

        [StringLength(20)]
        public string Answer_Content { get; set; }

        public bool? Correct { get; set; }

        public int? Question_Code { get; set; }

        public virtual Question Question { get; set; }
    }
}
