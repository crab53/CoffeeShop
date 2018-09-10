using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Core
{
    public class Constants
    {
        public const string KeyChar = "abcdefghijklmnopqrstuvwxyz0123456789";

        public enum EModule
        {

        }

        public enum ESetting
        {

        }

        public enum EStoreStatus
        {

        }

        public enum EPayType
        {
            Trial = 0,
            Active = 1,
            Expired = 2,
        }

        public enum EProductType
        {
            Dish = 1,
            Modifier = 2,
        }

    }
}
