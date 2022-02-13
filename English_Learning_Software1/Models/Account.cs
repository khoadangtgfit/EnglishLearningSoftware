namespace English_Learning_Software1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        public int Account_Code { get; set; }

        [StringLength(20)]
        public string Account_Name { get; set; }

        [StringLength(20)]
        public string Account_Password { get; set; }

        public bool? Account_Type_Code { get; set; }

        public int? Ownere_Code { get; set; }

        public virtual Ownere Ownere { get; set; }
    }
}
