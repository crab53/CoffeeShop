using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Asset
{
    public class CFBusLicense
    {
        private static CFBusLicense instance;

        private CFBusLicense() { }

        public static CFBusLicense Instance
        {
            get
            {
                if (instance == null) instance = new CFBusLicense();
                return instance;
            }
        }
    }
}
