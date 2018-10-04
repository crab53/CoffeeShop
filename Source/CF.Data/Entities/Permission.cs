namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permission")]
    public partial class Permission
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string RoleID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ModuleID { get; set; }

        public bool IsAction { get; set; }

        public bool IsView { get; set; }

        public bool IsDelete { get; set; }
    }
}
