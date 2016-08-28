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
        private static int _retryCheckinCount = 0;
        public static bool Connected { get; set; }

        public static string StartRemotingClient()
        {
            Connected = false;
            string serverAddr = Setting.ReportServerAddr;
            int serverPort = Setting.ReportServerPort;
            if (string.IsNullOrEmpty(serverAddr))
                return "Chưa cài đặt kết nối đến server !";

            _resultObj = (ResultObj)Activator.GetObject(typeof(ResultObj), string.Format("tcp://{0}:{1}/{2}", serverAddr, serverPort, RemoteSetting.REMOTE_SERVER_NAME));

            string loginRes = _resultObj.Login(Username);

            if (loginRes == "stopped")
            {
                return "Không kết nối được với máy chủ. ";
            }

            if (loginRes.ToLower() != Username.ToLower())
                return loginRes;

            

            Setting.LastUsername = Username = loginRes;
            Connected = true;
            return string.Empty;
        }

        public static string Checkin()
        {
            try
            {
                string res =  _resultObj.Checkin(Username);
                if (res == "stopped")
                {
                    Connected = false;
                    _retryCheckinCount = int.MaxValue - 1;
                    throw new Exception();
                }
                _retryCheckinCount = 0;
                Connected = true;
                return res;
            }
            catch (Exception ex)
            {
                if (++_retryCheckinCount <= Setting.MaxRetryCheckin && Connected)
                {
                    return string.Empty;
                }
                else
                {
                    Connected = false;
                    throw new Exception("Mất kết nối với máy chủ !");
                }
            }
        }

        public static void StopRemotingClient()
        {
            _resultObj.Logout(Username);
        }

        public static void SendResult(Report r)
        {
            _resultObj.SendReport(r);
        }
    }
}
