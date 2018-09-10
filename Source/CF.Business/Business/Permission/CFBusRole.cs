using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Permission
{
    public class CFBusRole
    {
        private static CFBusRole instance;

        private CFBusRole() { }

        public static CFBusRole Instance
        {
            get
            {
                if (instance == null) instance = new CFBusRole();
                return instance;
            }
        }
    }
}
