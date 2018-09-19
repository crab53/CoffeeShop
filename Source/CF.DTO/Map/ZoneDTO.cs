using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.DTO.Map
{
    public class ZoneDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsActive { get; set; }
    }

    #region Request

    public class GetListZoneRequest : RequestBase
    {
        public bool IsShowActive { get; set; }
    }

    public class GetZoneInfoRequest : RequestBase { }

    public class CreateOrUpdateZoneRequest : RequestBase
    {
        public ZoneDTO Zone { get; set; }
    }
    
    public class DeleteZoneRequest : RequestBase { }

    #endregion Request

    #region Response

    public class GetListZoneResponse : ResponseBase
    {
        public List<ZoneDTO> ListZone { get; set; }

        public GetListZoneResponse()
        {
            ListZone = new List<ZoneDTO>();
        }
    }

    public class GetZoneInfoResponse : ResponseBase
    {
        public ZoneDTO Zone { get; set; }
    }

    public class CreateOrUpdateZoneResponse : ResponseBase { }
    
    public class DeleteZoneResponse : ResponseBase { }

    #endregion Response
}
