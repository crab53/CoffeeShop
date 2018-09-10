using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Permission
{
    public class CFBusEmployee
    {
        private static CFBusEmployee instance;

        private CFBusEmployee() { }

        public static CFBusEmployee Instance
        {
            get
            {
                if (instance == null) instance = new CFBusEmployee();
                return instance;
            }
        }
    }
}
