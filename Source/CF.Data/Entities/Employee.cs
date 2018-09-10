namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreID { get; set; }

        [StringLength(50)]
        public string RoleID { get; set; }

        public bool IsSA { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(100)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime HiredDate { get; set; }

        public bool IsDelete { get; set; }
    }
}
