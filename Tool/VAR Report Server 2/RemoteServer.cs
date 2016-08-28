using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteContract;

namespace VAR_Report_Server
{
    public delegate string FpUserLogin(string username);
    public delegate void FpSendResult(Report result);
    public delegate string FpUserCheckin(string loginHash);
    public delegate void FpUserLogout(string username);

    public class ResultServer : RemoteContract.ResultObj
    {
        public override void SendReport(Report result)
        {
            result.SentTime = DateTime.Now;
            FormReport.AddResult(result);
        }

        public override string Login(string username)
        {
            FormClientManagement.LoginClient(username);
            return FormReport.UserLogin(username);
        }

        public override string Checkin(string username)
        {
            if (username.EndsWith("started") || username.EndsWith("stopped"))
            {
                bool isStarted = username.EndsWith("started");
                username = username.Substring(0, username.LastIndexOf(":"));
                FormClientManagement.CheckinClient(username, isStarted);
            }
            return FormReport.UserCheckin(username);
        }

        public override void Logout(string username)
        {
            FormClientManagement.LogoutClient(username);
            FormReport.UserLogout(username);
        }

        public override Input GetInput(string username, DateTime lastUpdateTime)
        {
            User user = UserBusiness.GetUser(username);
            FormReport.UserCheckin(user.Username);
            if (lastUpdateTime < user.LastModified)
                return new Input { InputString = user.Input, UpdateTime = DateTime.Now };
            return null;
        }

        public override List<BotInfo> GetBotInfo(string username)
        {
            return FormClientManagement.GetBotInfo(username);
        }

        public override List<Input> GetInputList(string username)
        {
            return FormClientManagement.GetInputList(username);
        }

        public override int SendCommand(string username)
        {
            return FormClientManagement.SendCommand(username);
        }

        public override void ReLogin(string username, bool isStarted)
        {
            FormClientManagement.ReLoginClient(username, isStarted);
        }
    }
}
