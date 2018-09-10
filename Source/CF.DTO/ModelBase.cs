using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.DTO
{
    public class RequestBase
    {
        public string StoreID { get; set; }
        public string EmployeeID { get; set; }
    }

    public class ResponseBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public Exception ExceptionError { get; set; }
    }
}
