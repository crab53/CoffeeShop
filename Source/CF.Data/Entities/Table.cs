namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Table")]
    public partial class Table
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string ZoneID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Cover { get; set; }

        public int XPoint { get; set; }

        public int YPoint { get; set; }

        public int ViewMode { get; set; }

        public string Description { get; set; }

        public bool IsDelete { get; set; }
    }
}
