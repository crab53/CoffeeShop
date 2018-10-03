using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.DTO.Permission
{
    public class EmployeeDTO
    {
        public string ID { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên nhân viên")]
        [MaxLength(250, ErrorMessage = "Tên nhân viên tối đa 250 kí tự")]
        public string Name { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Vui lòng nhập số")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập e-mail")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail không hợp lệ")]
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? HiredDate { get; set; }
        public bool IsActive { get; set; }
        public EmployeeDTO()
        {
            IsActive = true;
        }
    }

    #region Request

    public class GetListEmployeeRequest : RequestBase
    {
        public bool IsShowActive { get; set; }
    }

    public class GetEmployeeInfoRequest : RequestBase { }

    public class CreateOrUpdateEmployeeRequest : RequestBase
    {
        public EmployeeDTO Employee { get; set; }
    }
    
    public class DeleteEmployeeRequest : RequestBase { }

    #endregion Request

    #region Response

    public class GetListEmployeeResponse : ResponseBase
    {
        public List<EmployeeDTO> ListEmployee { get; set; }

        public GetListEmployeeResponse()
        {
            ListEmployee = new List<EmployeeDTO>();
        }
    }

    public class GetEmployeeInfoResponse : ResponseBase
    {
        public EmployeeDTO Employee { get; set; }
    }

    public class CreateOrUpdateEmployeeResponse : ResponseBase { }
    
    public class DeleteEmployeeResponse : ResponseBase { }

    #endregion Response
}
