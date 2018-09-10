using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Map
{
    public class CFBusTable
    {
        private static CFBusTable instance;

        private CFBusTable() { }

        public static CFBusTable Instance
        {
            get
            {
                if (instance == null) instance = new CFBusTable();
                return instance;
            }
        }
    }
}
