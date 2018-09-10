using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.POS
{
    public class CFBusOrder
    {
        private static CFBusOrder instance;

        private CFBusOrder() { }

        public static CFBusOrder Instance
        {
            get
            {
                if (instance == null) instance = new CFBusOrder();
                return instance;
            }
        }
    }
}
