using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Map
{
    public class CFBusZone
    {
        private static CFBusZone instance;

        private CFBusZone() { }

        public static CFBusZone Instance
        {
            get
            {
                if (instance == null) instance = new CFBusZone();
                return instance;
            }
        }
    }
}
