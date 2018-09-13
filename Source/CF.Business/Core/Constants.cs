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
        public static readonly string[] VietnameseSigns = new string[] { "aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ" };

        public enum EModule
        {

        }

        public enum ESetting
        {

        }

        public enum EStoreStatus
        {
            Trial = 0,
            Active = 1,
            Expired = 2,
        }

        public enum EPayType
        {
            Cash = 0,
            Voucher = 9,
        }

        public enum EProductType
        {
            Dish = 1,
            Modifier = 2,
        }

        public enum EPeriodType
        {
            Day = 1,
            Week = 2,
            Month = 3,
            Year = 4,
        }

    }
}
