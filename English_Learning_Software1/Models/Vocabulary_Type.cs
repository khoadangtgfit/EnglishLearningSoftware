namespace English_Learning_Software1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vocabulary_Type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vocabulary_Type()
        {
            Quiz_Detail = new HashSet<Quiz_Detail>();
            Vocabularies = new HashSet<Vocabulary>();
        }

        [Key]
        public int Vocabulary_Type_Code { get; set; }

        [StringLength(25)]
        public string Vocabulary_Type_EN_Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quiz_Detail> Quiz_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vocabulary> Vocabularies { get; set; }
    }
}
