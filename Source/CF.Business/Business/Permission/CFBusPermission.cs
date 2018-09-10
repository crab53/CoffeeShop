using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Permission
{
    public class CFBusPermission
    {
        private static CFBusPermission instance;

        private CFBusPermission() { }

        public static CFBusPermission Instance
        {
            get
            {
                if (instance == null) instance = new CFBusPermission();
                return instance;
            }
        }
    }
}
