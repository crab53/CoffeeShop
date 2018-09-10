using CF.Data.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Support
{
    public class CFBusTest
    {
        private static CFBusTest instance;

        private CFBusTest() { }

        public static CFBusTest Instance
        {
            get
            {
                if (instance == null) instance = new CFBusTest();
                return instance;
            }
        }

        public void GetTest()
        {
            try
            {
                using (var _db = new CfDb())
                {
                    var cus = _db.Customers.ToList();
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
    }
}
