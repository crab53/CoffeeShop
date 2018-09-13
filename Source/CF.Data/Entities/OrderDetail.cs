namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderID { get; set; }

        [StringLength(50)]
        public string ProductID { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public bool IsSend { get; set; }

        public bool IsTakeAway { get; set; }

        public double Promotion { get; set; }

        public double Discount { get; set; }

        public double ServiceCharge { get; set; }

        public double Tax { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string DiscountID { get; set; }

        public double DiscountValue { get; set; }

        public int DiscountType { get; set; }

        [StringLength(50)]
        public string PromotionID { get; set; }

        public bool IsSpend { get; set; }

        public bool IsEarn { get; set; }

        public bool IsDelete { get; set; }
    }
}
