namespace English_Learning_Software1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Test_Detail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ownere_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Test_Code { get; set; }

        public double? Test_Score { get; set; }

        public DateTime? CompletionTime { get; set; }

        public virtual Ownere Ownere { get; set; }

        public virtual Test Test { get; set; }
    }
}
