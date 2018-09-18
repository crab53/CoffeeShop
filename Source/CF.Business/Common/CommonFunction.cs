using CF.Business.Core;
using CF.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace CF.Business.Common
{
    public class CommonFunction
    {
        public static string GetSHA512(string text)
        {
            UnicodeEncoding encode = new UnicodeEncoding();
            byte[] message = encode.GetBytes(text);

            SHA512 hashString = new SHA512Managed();
            string hex = string.Empty;

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
                hex += x.ToString("x2");

            return hex;
        }

        public static string GenerateKey(Constants.EKey eKey, int length = 8)
        {
            Random random = new Random();
            string key = "";
            List<string> listKeyExist = new List<string>();

            switch (eKey)
            {
                case Constants.EKey.Code:
                    try
                    {
                        using (var _db = new CfDb())
                        {
                            listKeyExist = _db.Licenses.Where(o => !string.IsNullOrEmpty(o.Key)).Select(o => o.Key).ToList();
                        }
                    }
                    catch (Exception ex) { Log.Logger.Error("ErrorGenerateKey", ex); }
                    break;
                case Constants.EKey.Password:
                    break;
            }

            do
            {
                Thread.Sleep(20);
                key = new string(Enumerable.Repeat(Constants.KeyChar, length).Select(s => s[random.Next(s.Length)]).ToArray());
            } while (listKeyExist.Contains(key));
            return key;
        }

        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < Constants.VietnameseSigns.Length; i++)
                for (int j = 0; j < Constants.VietnameseSigns[i].Length; j++)
                    str = str.Replace(Constants.VietnameseSigns[i][j], Constants.VietnameseSigns[0][i - 1]);

            return str.Trim();
        }


    }
}