using RemoteContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Auto_VAR
{
    class Client
    {
        private static ResultObj _resultObj;
        public static string Username { get; set; }
        private static bool IsClientStarted = false;
        private static Thread _threadCheckin;

        public static string StartRemotingClient()
        {
            if (IsClientStarted)
                return string.Empty;
            string serverAddr = Setting.ReportServerAddr;
            int serverPort = Setting.ReportServerPort;
            if (string.IsNullOrEmpty(serverAddr))
                return "Chưa cài đặt kết nối đến server !";

            try
            {
                _resultObj = (ResultObj)Activator.GetObject(typeof(ResultObj), string.Format("tcp://{0}:{1}/{2}", serverAddr, serverPort, RemoteSetting.REMOTE_SERVER_NAME));
            }
            catch
            {
                throw new Exception("Không thể kết nối đến máy chủ !");
            }

            try
            {
                string loginRes = _resultObj.Login(Username);
                if (loginRes.ToLower() != Username.ToLower())
                {
                    return loginRes;
                }

                Username = loginRes;
                IsClientStarted = true;
                return string.Empty;
            }
            catch (Exception ex)
            {
                IsClientStarted = false;
                return ex.Message;
            }
        }

        public static string StopRemotingClient()
        {
            try
            {
                _resultObj.Logout(Username);
            }
            catch (Exception ex) { return ex.Message; }
            IsClientStarted = false;
            return string.Empty;
        }

        public static string Checkin()
        {
            return _resultObj.Checkin(Username);
        }

        public static void SendResult(Report report)
        {
            _resultObj.SendReport(report);
        }
    }
}
