using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CF.Api.FnB.Controllers
{
    public class TestDTO
    {
        public string Value { get; set; }
    }
    public class TestController : ApiController
    {
        [HttpPost, Route("api/test/gettest")]
        public async Task<TestDTO> GetTest(TestDTO request)
        {
            return request;
        }
    }
}
