using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Visa_Appointment_Request
{
    public class LogUtils
    {
        #region Writelog

        private static object _lockHistory = new object();
        private static object _lockSubmit = new object();

        public static void WriteLog(string message, string type)
        {
            try
            {
            if (!Directory.Exists(@"Log"))
            {
                Directory.CreateDirectory(@"Log");
            }

            using (System.IO.StreamWriter w = System.IO.File.AppendText("LogError\\" + LogFilename))
            {
                w.Write(string.Format("{0} - {1} : {2} \r\n", DateTime.Now, type, message));
                w.Flush(); w.Close();
            }
            }
            catch { }
        }

        public static void WriteLogHistory(string message, string type)
        {
            lock (_lockHistory)
            {
                try
                {
                    if (!Directory.Exists(@"LogHistory"))
                    {
                        Directory.CreateDirectory(@"LogHistory");
                    }

                    using (System.IO.StreamWriter w = System.IO.File.AppendText("LogHistory\\" + LogHistoryFilename))
                    {
                        w.Write(string.Format("{0} - {1} : {2} \r\n", DateTime.Now, type, message));
                        w.Flush(); w.Close();
                    }
                }
                catch { }
            }
        }

        public static void WriteLogSubmit(string name, string message)
        {
            lock (_lockSubmit)
            {
                try
                {
                    if (!Directory.Exists(@"LogSubmit"))
                    {
                        Directory.CreateDirectory(@"LogSubmit");
                    }

                    DateTime now = DateTime.Now;
                    string _logFilename = string.Format("{0}_{1}.txt", now.ToString("yyyyMMdd"), name);

                    using (System.IO.StreamWriter w = System.IO.File.AppendText("LogSubmit\\" + _logFilename))
                    {
                        w.Write(string.Format("{0} - {1} \r\n", DateTime.Now,  message));
                        w.Flush(); w.Close();
                    }
                }
                catch { }
            }
        }

        protected static string LogHistoryFilename
        {
            get
            {
                DateTime now = DateTime.Now;
                return string.Format("{0}{1}.txt", _logHistoryFilename, now.ToString("yyyyMMdd"));
            }
        }

        protected static string LogFilename
        {
            get
            {
                DateTime now = DateTime.Now;
                return string.Format("{0}{1}.log", _logFilename, now.ToString("yyyyMMdd"));
            }
        }
        private static string _logFilename = System.Configuration.ConfigurationManager.AppSettings["LogFilename"] ??
            "log";

        private static string _logHistoryFilename = System.Configuration.ConfigurationManager.AppSettings["LogHistoryFilename"] ??
            "history";

        #endregion
    }
}
