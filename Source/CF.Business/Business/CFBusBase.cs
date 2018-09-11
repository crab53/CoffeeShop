using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business
{
    public class CFBusBase
    {
        public void DebugLogInfo(string methodName)
        {
            Debug.WriteLine(string.Format("Method: {0}", methodName));
        }

        public void DebugLogInfo(string methodName, object obj)
        {
            Debug.WriteLine(string.Format("Method: {0}", methodName));
            Debug.WriteLine(string.Format("Json: {0}", JsonConvert.SerializeObject(obj)));
        }

        public void DebugLogError(string methodName, Exception ex)
        {
            Debug.WriteLine(string.Format("Method: {0}", methodName));
            Debug.WriteLine(string.Format("Error: {0}", JsonConvert.SerializeObject(ex)));
        }

        public void DebugLogError(string methodName, object obj, Exception ex)
        {
            Debug.WriteLine(string.Format("Method: {0}", methodName));
            Debug.WriteLine(string.Format("Json: {0}", JsonConvert.SerializeObject(obj)));
            Debug.WriteLine(string.Format("Error: {0}", JsonConvert.SerializeObject(ex)));
        }
    }
}
