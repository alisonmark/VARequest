using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAR_Report_Server
{
    public class User
    {
        public string Username { get; set; }
        public string IPAddress { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime CheckinTime { get; set; }
        public bool IsOnline { get; set; }
        public string LoginHash { get; set; }
        public int SubmitCount { get; set; }
        public int SuccessCount { get; set; }
        public string Success
        {
            get { return string.Format("{0}/{1}", SuccessCount, SubmitCount); }
        }

        public string Input { get; set; }
        public DateTime LastModified { get; set; }

        public User()
        {
            Username = string.Empty;
            IPAddress = string.Empty;
            LoginTime = DateTime.MinValue;
            CheckinTime = DateTime.MinValue;
            IsOnline = false;
            LoginHash = string.Empty;
            SubmitCount = 0;
            SuccessCount = 0;
            Input = string.Empty;
            LastModified = DateTime.MinValue;
        }
    }
}
