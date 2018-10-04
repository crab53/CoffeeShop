using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.DTO.Asset
{
    public class StoreDTO
    {
        public string ID { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
