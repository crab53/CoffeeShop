namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store")]
    public partial class Store
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(100)]
        public string ImageUrl { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string AddressStr { get; set; }

        public string Description { get; set; }
        
        public DateTime ExpiredDate { get; set; }

        public bool IsDelete { get; set; }
    }
}
