namespace CF.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Setting")]
    public partial class Setting
    {
        [Key]
        [StringLength(50)]
        public string StoreID { get; set; }
        
        public string JsonSetting { get; set; }
        
        public bool IsDelete { get; set; }
    }
}
