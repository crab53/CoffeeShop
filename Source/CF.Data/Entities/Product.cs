namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryID { get; set; }

        public int ProductType { get; set; }

        [StringLength(100)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string NameStr { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int NumberOfOrder { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }
    }
}
