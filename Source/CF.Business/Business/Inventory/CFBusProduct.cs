using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Inventory
{
    public class CFBusProduct
    {
        private static CFBusProduct instance;

        private CFBusProduct() { }

        public static CFBusProduct Instance
        {
            get
            {
                if (instance == null) instance = new CFBusProduct();
                return instance;
            }
        }
    }
}
