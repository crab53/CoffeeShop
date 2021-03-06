﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.DTO.Permission
{
    public class RoleDTO
    {
        public string ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên quyền")]
        [MaxLength(250, ErrorMessage = "Tên nhân viên tối đa 250 kí tự")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<PermissionDTO> ListPermission { get; set; }
        public RoleDTO()
        {
            IsActive = true;
            ListPermission = new List<PermissionDTO>();
        }
    }

    public class PermissionDTO
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool IsAction { get; set; }
        public bool IsView { get; set; }
    }

    #region Request

    public class GetListRoleRequest : RequestBase
    {
        public bool IsShowActive { get; set; }
    }

    public class GetRoleInfoRequest : RequestBase { }

    public class CreateOrUpdateRoleRequest : RequestBase
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

    public class CreateOrUpdateRoleResponse : ResponseBase { }

    public class DeleteRoleResponse : ResponseBase { }

    #endregion Response
}
