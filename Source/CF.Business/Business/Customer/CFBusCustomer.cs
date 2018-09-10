using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Customer
{
    public class CFBusCustomer
    {
        private static CFBusCustomer instance;

        private CFBusCustomer() { }

        public static CFBusCustomer Instance
        {
            get
            {
                if (instance == null) instance = new CFBusCustomer();
                return instance;
            }
        }
    }
}
