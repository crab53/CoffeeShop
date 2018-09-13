using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Log
{
    public class Logger
    {
        public static void Info(string message)
        {
            var mess = $">>> {message}\n";
            mess += "[ ".PadRight(98, '-') + " ]";
            WriterLog(mess, EventType.INFO);
        }

        public static void Info(string message, object obj)
        {
            var mess = $">>> {message}\n";
            mess += "[ ".PadRight(98, '-') + " ]\n";
            mess += JsonConverter.Serialize(obj) + "\n";
            mess += "[ ".PadRight(98, '-') + " ]";
            WriterLog(mess, EventType.INFO);
        }

        public static void Info(string message, object objRequest, object objResponse)
        {
            var mess = $">>> {message}\n";
            mess += "[ ".PadRight(98, '-') + " ]\n";
            mess += JsonConverter.Serialize(objRequest) + "\n";
            mess += JsonConverter.Serialize(objResponse) + "\n";
            mess += "[ ".PadRight(98, '-') + " ]";
            WriterLog(mess, EventType.INFO);
        }

        public static void Warning(string message)
        {
            var mess = $">>> {message}\n";
            mess += "[ ".PadRight(98, '-') + " ]";
            WriterLog(mess, EventType.WARNING);
        }

        public static void Warning(string message, object obj)
        {
            var mess = $">>> {message}\n";
            mess += "[ ".PadRight(98, '-') + " ]\n";
            mess += JsonConverter.Serialize(obj);
            mess += "[ ".PadRight(98, '-') + " ]";
            WriterLog(mess, EventType.WARNING);
        }

        public static void Warning(string message, object objRequest, object objResponse)
        {
            var mess = $">>> {message}\n";
            mess += "[ ".PadRight(98, '-') + " ]\n";
            mess += JsonConverter.Serialize(objRequest) + "\n";
            mess += JsonConverter.Serialize(objResponse) + "\n";
            mess += "[ ".PadRight(98, '-') + " ]";
            WriterLog(mess, EventType.WARNING);
        }

        public static void Error(string message, Exception ex)
        {
            var mess = $"<<< {message}\n";
            mess += "[ ".PadRight(98, '-') + " ]\n";
            mess += JsonConverter.Serialize(ex) + "\n";
            mess += "[ ".PadRight(98, '-') + " ]";
            WriterLog(mess, EventType.ERROR);
        }

        public static void Error(string message, object obj, Exception ex)
        {
            var mess = $"<<< {message}\n";
            mess += "[ ".PadRight(98, '-') + " ]\n";
            mess += JsonConverter.Serialize(obj) + "\n";
            mess += JsonConverter.Serialize(ex) + "\n";
            mess += "[ ".PadRight(98, '-') + " ]";
            WriterLog(mess, EventType.ERROR);
        }

        private enum EventType
        {
            ERROR,
            WARNING,
            INFO
        }

        private static void WriterLog(string message, EventType type)
        {
            var config = (Config)ConfigurationManager.GetSection("customLog");
            if (config == null) config = new Config();
            DateTime datetime = DateTime.Now;

            string logPath = Path.Combine(GetMapPath(), config.Direction.Value);
            string logFile = string.Format("{0}.{1}", datetime.ToString(config.FileNameFormat.Value), config.Extension.Value);
            string logFilePath = string.Format("{0}\\{1}", logPath, logFile);

            message = string.Format("{0} - Thread:[{1}] - Level:{2} - Message:\n{3}\n", datetime.ToString(config.DateTimeFormat.Value), Thread.CurrentThread.ManagedThreadId, type.ToString(), message);

            // Check log folder
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            bool isNew = true;
            if (File.Exists(logFilePath))
            {
                FileInfo fileInfo = new FileInfo(logFilePath);
                if (fileInfo.Length >= (config.MaximumFileSize.Value * 1024 * 1024))
                {
                    List<string> allFile = ToList(Directory.GetFiles(logPath));
                    List<string> files = new List<string>();
                    foreach (var file in allFile)
                    {
                        if (file.Contains(logFile))
                            files.Add(file);
                    }
                    files.Sort();

                    File.Move(fileInfo.FullName, string.Format("{0}.{1}", fileInfo.FullName, files.Count));
                }
                else
                    isNew = false;
            }

            if (isNew)
            {
                using (StreamWriter sw = File.CreateText(logFilePath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    sw.WriteLine(message);
                }
            }
        }

        private static List<T> ToList<T>(T[] array)
        {
            List<T> listT = new List<T>();
            if (array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                    listT.Add(array[i]);
            }
            return listT;
        }

        private static string GetMapPath()
        {
            if (System.Web.HttpContext.Current == null)
                return Directory.GetCurrentDirectory();
            else
                return System.Web.HttpContext.Current.Server.MapPath("~");
        }
    }
}