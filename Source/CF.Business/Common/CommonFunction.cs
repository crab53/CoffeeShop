using CF.Business.Core;
using CF.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CF.Business.Common
{
    public class CommonFunction
    {
        public static string GenerateKey(int length = 8)
        {
            Random random = new Random();
            string key = "";
            List<string> listKeyExist = new List<string>();

            try
            {
                using (var _db = new CfDb())
                {
                    listKeyExist = _db.Licenses.Where(o => !string.IsNullOrEmpty(o.Key)).Select(o => o.Key).ToList();
                }
            }
            catch (Exception ex) { }

            do
            {
                Thread.Sleep(20);
                key = new string(Enumerable.Repeat(Constants.KeyChar, length).Select(s => s[random.Next(s.Length)]).ToArray());
            } while (listKeyExist.Contains(key));
            return key;
        }
    }
}
