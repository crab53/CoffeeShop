using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Core
{
    public class Constants
    {
        public static DateTime MinDate = new DateTime(1900, 01, 01, 12, 00, 00);
        public static DateTime MaxDate = new DateTime(9999, 12, 31, 12, 00, 00);
        public static string _PublicImages = ConfigurationManager.AppSettings["PublicImages"];
        public static string _PostImages = ConfigurationManager.AppSettings["PostImages"];

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

        public enum EKey
        {
            Code = 0,
            Password = 1,
        }
    }
}
