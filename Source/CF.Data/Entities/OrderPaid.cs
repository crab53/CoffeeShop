namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderPaid")]
    public partial class OrderPaid
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderID { get; set; }

        public int PayType { get; set; }

        public double Value { get; set; }

        public double Change { get; set; }

        [StringLength(50)]
        public string VoucherID { get; set; }

        public bool IsDelete { get; set; }
    }
}
