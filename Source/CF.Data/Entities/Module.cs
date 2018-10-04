namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Module")]
    public partial class Module
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string ParentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Code { get; set; }

        public bool IsActive { get; set; }
    }
}
