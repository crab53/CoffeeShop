using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.DTO.Map
{
    public class TableDTO
    {
        public string ID { get; set; }
        public string ZoneID { get; set; }
        public string ZoneName { get; set; }
        public string Name { get; set; }
        public int Cover { get; set; }
        public int XPoint { get; set; }
        public int YPoint { get; set; }
        public int ViewMode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    #region Request

    public class GetListTableRequest : RequestBase
    {
        public bool IsShowActive { get; set; }
    }

    public class GetTableInfoRequest : RequestBase { }

    public class CreateTableRequest : RequestBase
    {
        public TableDTO Table { get; set; }
    }

    public class UpdateTableRequest : RequestBase
    {
        public TableDTO Table { get; set; }
    }

    public class DeleteTableRequest : RequestBase { }

    #endregion Request

    #region Response

    public class GetListTableResponse : ResponseBase
    {
        public List<TableDTO> ListTable { get; set; }

        public GetListTableResponse()
        {
            ListTable = new List<TableDTO>();
        }
    }

    public class GetTableInfoResponse : ResponseBase
    {
        public TableDTO Table { get; set; }
    }

    public class CreateTableResponse : ResponseBase { }

    public class UpdateTableResponse : ResponseBase { }

    public class DeleteTableResponse : ResponseBase { }

    #endregion Response
}
