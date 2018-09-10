using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Inventory
{
    public class CFBusCategory
    {
        private static CFBusCategory instance;

        private CFBusCategory() { }

        public static CFBusCategory Instance
        {
            get
            {
                if (instance == null) instance = new CFBusCategory();
                return instance;
            }
        }
    }
}
