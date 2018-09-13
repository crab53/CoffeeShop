namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("License")]
    public partial class License
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeID { get; set; }

        [StringLength(50)]
        public string StoreID { get; set; }

        [StringLength(10)]
        public string Key { get; set; }

        public int Period { get; set; }

        public int PeriodType { get; set; }

        public bool IsUsed { get; set; }

        public bool IsDelete { get; set; }
    }
}
