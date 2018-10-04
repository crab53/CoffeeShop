using CF.Business.Core;
using CF.Data.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;

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

        public static string RemoveSign4VietnameseString(string str, bool isMinify = false)
        {
            for (int i = 1; i < Constants.VietnameseSigns.Length; i++)
                for (int j = 0; j < Constants.VietnameseSigns[i].Length; j++)
                    str = str.Replace(Constants.VietnameseSigns[i][j], Constants.VietnameseSigns[0][i - 1]);

            if (isMinify)
                str = MinifyString(str);

            return str.Trim().ToLower();
        }

        private static string MinifyString(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            sb.Replace(" ", string.Empty);
            sb.Replace(Environment.NewLine, string.Empty);
            sb.Replace("\\t", string.Empty);
            sb.Replace("`", string.Empty);
            sb.Replace("~", string.Empty);
            sb.Replace("!", string.Empty);
            sb.Replace("@", string.Empty);
            sb.Replace("#", string.Empty);
            sb.Replace("$", string.Empty);
            sb.Replace("%", string.Empty);
            sb.Replace("^", string.Empty);
            sb.Replace("&", string.Empty);
            sb.Replace("*", string.Empty);
            sb.Replace("(", string.Empty);
            sb.Replace(")", string.Empty);
            sb.Replace("-", string.Empty);
            sb.Replace("_", string.Empty);
            sb.Replace("=", string.Empty);
            sb.Replace("+", string.Empty);
            sb.Replace("[", string.Empty);
            sb.Replace("]", string.Empty);
            sb.Replace("{", string.Empty);
            sb.Replace("}", string.Empty);
            sb.Replace("\\", string.Empty);
            sb.Replace("|", string.Empty);
            sb.Replace(";", string.Empty);
            sb.Replace(":", string.Empty);
            sb.Replace("'", string.Empty);
            sb.Replace("\"", string.Empty);
            sb.Replace(",", string.Empty);
            sb.Replace("<", string.Empty);
            sb.Replace(">", string.Empty);
            sb.Replace(".", string.Empty);
            sb.Replace("?", string.Empty);
            return sb.ToString();
        }

        public static string ToBase64String(HttpPostedFile cvFile)
        {
            byte[] fileInBytes = new byte[cvFile.ContentLength];
            using (BinaryReader theReader = new BinaryReader(cvFile.InputStream))
            {
                fileInBytes = theReader.ReadBytes(cvFile.ContentLength);
            }
            string fileAsString = Convert.ToBase64String(fileInBytes);
            return fileAsString;
        }

        public static bool UploadImage(string imageString, ref string fileName)
        {
            try
            {
                byte[] data = Convert.FromBase64String(imageString);
                string fileExextension = string.Empty;

                Image image;

                //check image  Exextension
                using (var stream = new MemoryStream(data, 0, data.Length))
                {
                    image = Image.FromStream(stream);
                    var extension = "";
                    if (System.Drawing.Imaging.ImageFormat.Jpeg.Equals(image.RawFormat))
                    {
                        extension = System.Drawing.Imaging.ImageFormat.Jpeg.ToString().ToLower();
                    }
                    else if (System.Drawing.Imaging.ImageFormat.Png.Equals(image.RawFormat))
                    {
                        extension = System.Drawing.Imaging.ImageFormat.Png.ToString().ToLower();
                    }
                    else if (System.Drawing.Imaging.ImageFormat.Gif.Equals(image.RawFormat))
                    {
                        extension = System.Drawing.Imaging.ImageFormat.Gif.ToString().ToLower();
                    }

                    fileName = Guid.NewGuid().ToString() + "." + extension;
                }

                // create path save image 
                string path = System.Web.Hosting.HostingEnvironment.MapPath(Constants._PostImages);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                image.Save(path + fileName);
                Log.Logger.Info("UploadImage", fileName);
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("UploadImage", ex);
                return false;
            }
        }

        public static string GetFileExtension(string imageString)
        {
            try
            {
                string ret = "";

                byte[] data = Convert.FromBase64String(imageString);
                Image image;

                //check image  Exextension
                using (var stream = new MemoryStream(data, 0, data.Length))
                {
                    image = Image.FromStream(stream);
                    if (System.Drawing.Imaging.ImageFormat.Jpeg.Equals(image.RawFormat))
                    {
                        ret = System.Drawing.Imaging.ImageFormat.Jpeg.ToString().ToLower();
                    }
                    else if (System.Drawing.Imaging.ImageFormat.Png.Equals(image.RawFormat))
                    {
                        ret = System.Drawing.Imaging.ImageFormat.Png.ToString().ToLower();
                    }
                    else if (System.Drawing.Imaging.ImageFormat.Gif.Equals(image.RawFormat))
                    {
                        ret = System.Drawing.Imaging.ImageFormat.Gif.ToString().ToLower();
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("GetFileExtension", ex);
                return "";
            }
        }

    }
}