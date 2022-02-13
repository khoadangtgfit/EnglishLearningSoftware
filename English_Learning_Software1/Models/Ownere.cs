namespace English_Learning_Software1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ownere")]
    public partial class Ownere
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ownere()
        {
            Accounts = new HashSet<Account>();
            Quiz_Detail = new HashSet<Quiz_Detail>();
            Story_Detail = new HashSet<Story_Detail>();
            Test_Detail = new HashSet<Test_Detail>();
            Video_Detail = new HashSet<Video_Detail>();
        }

        [Key]
        public int Ownere_Code { get; set; }

        [StringLength(40)]
        public string Ownere_FullName { get; set; }

        [StringLength(12)]
        public string Ownere_PhoneNumber { get; set; }

        [StringLength(35)]
        public string Ownere_Email { get; set; }

        public DateTime? Ownere_Birthday { get; set; }

        [Column(TypeName = "image")]
        public byte[] Ownere_Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quiz_Detail> Quiz_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Story_Detail> Story_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test_Detail> Test_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Video_Detail> Video_Detail { get; set; }
    }
}
