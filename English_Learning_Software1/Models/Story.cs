namespace English_Learning_Software1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Story")]
    public partial class Story
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Story()
        {
            Story_Detail = new HashSet<Story_Detail>();
        }

        [Key]
        public int Story_Code { get; set; }

        [StringLength(50)]
        public string Story_Name { get; set; }

        [StringLength(1000)]
        public string Story_EN_Content { get; set; }

        [StringLength(1000)]
        public string Story_VN_Content { get; set; }

        [StringLength(50)]
        public string Story_Audio { get; set; }

        [Column(TypeName = "image")]
        public byte[] Story_Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Story_Detail> Story_Detail { get; set; }
    }
}
