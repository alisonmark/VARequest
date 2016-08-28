using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAR_Report_Server
{
    public class ClientAuto
    {
        public string Username { get; set; }
        public bool Connected { get; set; }
        public bool Started { get; set; }
        public string TimeInfo { get; set; }
        public string Input { get; set; }

        public Queue<ClientCommand> Command { get; set; }

        public object Tag { get; set; }

        public ClientAuto()
        {
            Username = string.Empty;
            Connected = false;
            Started = false;
            TimeInfo = string.Empty;
            Input = string.Empty;
            Command = new Queue<ClientCommand>();
            Tag = null;
        }
    }

    public enum ClientCommand
    {
        None = -1,
        Start = 0,
        Stop = 1,
        UpdateTime = 2,
        UpdateInput = 3,
        ForceLogout = 4
    }
}
