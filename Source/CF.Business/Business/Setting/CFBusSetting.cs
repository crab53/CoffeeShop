using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Business.Setting
{
    public class CFBusSetting
    {
        private static CFBusSetting instance;

        private CFBusSetting() { }

        public static CFBusSetting Instance
        {
            get
            {
                if (instance == null) instance = new CFBusSetting();
                return instance;
            }
        }
    }
}
