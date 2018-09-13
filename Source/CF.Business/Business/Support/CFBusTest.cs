using CF.Data.Context;
using System;
using System.Diagnostics;
using System.Linq;

namespace CF.Business.Business.Support
{
    public class CFBusTest
    {
        private static CFBusTest instance;

        private CFBusTest()
        {
        }

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
                    Log.Logger.Info("Test", cus);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
    }
}