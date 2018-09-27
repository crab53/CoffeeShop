using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.DTO
{
    public class RequestBase
    {
        public string ID { get; set; }
        public string StoreID { get; set; }
        public string EmployeeID { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public RequestBase()
        {
            PageSize = 9999;
            PageIndex = 0;
            StoreID = "";
        }
    }

    public class ResponseBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public Exception ExceptionError { get; set; }

        public ResponseBase()
        {
            Success = false;
            Message = "";
            Description = "";
            ExceptionError = null;
        }
    }
}
