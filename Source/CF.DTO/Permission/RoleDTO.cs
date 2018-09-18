using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.DTO.Permission
{
    public class RoleDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<PermissionDTO> ListPermission { get; set; }
    }

    #region Request

    public class GetListRoleRequest : RequestBase
    {
        public bool IsShowActive { get; set; }
    }

    public class GetRoleInfoRequest : RequestBase { }

    public class CreateRoleRequest : RequestBase
    {
        public RoleDTO Role { get; set; }
    }

    public class UpdateRoleRequest : RequestBase
    {
        public RoleDTO Role { get; set; }
    }

    public class DeleteRoleRequest : RequestBase { }

    #endregion Request

    #region Response

    public class GetListRoleResponse : ResponseBase
    {
        public List<RoleDTO> ListRole { get; set; }

        public GetListRoleResponse()
        {
            ListRole = new List<RoleDTO>();
        }
    }

    public class GetRoleInfoResponse : ResponseBase
    {
        public RoleDTO Role { get; set; }
    }

    public class CreateRoleResponse : ResponseBase { }

    public class UpdateRoleResponse : ResponseBase { }

    public class DeleteRoleResponse : ResponseBase { }

    #endregion Response
}
