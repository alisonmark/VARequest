using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visa_Appointment_Request
{
    public delegate void FpEventHandler<T>(object sender, T e);

    public class RequestResult
    {
        public string RequestUrl { get; set; }
        public string ResponseUrl { get; set; }
        public string Content { get; set; }
        public long Duration { get; set; }
        public string Method { get; set; }

        public override string ToString()
        {
            return string.Format("Send {0}: RequestUrl = {1}, ResponseUrl = {2}, Duration = {3}", Method, RequestUrl, ResponseUrl, Duration);
        }
    }
}
