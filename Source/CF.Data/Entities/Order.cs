namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreID { get; set; }

        [StringLength(50)]
        public string TableID { get; set; }

        [StringLength(50)]
        public string EmployeeID { get; set; }

        [StringLength(50)]
        public string CashierID { get; set; }

        [StringLength(50)]
        public string CustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNo { get; set; }

        public bool IsPaid { get; set; }

        public double Subtotal { get; set; }

        public double Promotion { get; set; }

        public double Discount { get; set; }

        public double ServiceCharge { get; set; }

        public double Tax { get; set; }

        public double Rounding { get; set; }

        public double TotalBill { get; set; }

        public double Remaining { get; set; }
    }
}
