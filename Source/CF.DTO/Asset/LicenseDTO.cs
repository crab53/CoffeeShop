using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.DTO.Asset
{
    public class LicenseDTO
    {
    }

    #region Request

    public class RegisterNewStoreRequest : RequestBase
    {
        public OwnerDTO Owner { get; set; }
        public StoreDTO Store { get; set; }
    }

    #endregion Request

    #region Response

    public class RegisterNewStoreResponse : ResponseBase { }

    #endregion Response
}
