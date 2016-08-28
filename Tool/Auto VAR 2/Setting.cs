using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;

namespace Auto_VAR
{
    public static class Setting
    {
        public static int DelayEachStep
        {
            get { return LoadConfigInt32("DelayEachStep", 0); }
            set { SaveConfig("DelayEachStep", value); }
        }
        public static int DelaySubmit
        {
            get { return LoadConfigInt32("DelaySubmit", 1000); }
            set { SaveConfig("DelaySubmit", value); }
        }
        public static int RetrySubmitCount
        {
            get { return LoadConfigInt32("RetrySubmitCount", 3); }
            set { SaveConfig("RetrySubmitCount", value); }
        }
        public static int CaptchaRefreshTime
        {
            get { return LoadConfigInt32("CaptchaRefreshTime", 18); }
            set { SaveConfig("CaptchaRefreshTime", value); }
        }
        public static int PrepareTime
        {
            get { return LoadConfigInt32("PrepareTime", 15000); }
            set { SaveConfig("PrepareTime", value); }
        }
        public static int SessionTimeout
        {
            get { return LoadConfigInt32("SessionTimeout", 10); }
            set { SaveConfig("SessionTimeout", value); }
        }
        public static int SessionTime
        {
            get { return LoadConfigInt32("SessionTime", 10); }
            set { SaveConfig("SessionTime", value); }
        }
        public static string UsernameDbc
        {
            get { return LoadConfigString("UsernameDbc", "lanlan"); }
            set { SaveConfig("UsernameDbc", value); }
        }
        public static string PasswordDbc
        {
            get { return LoadConfigString("PasswordDbc", "haNgan321"); }
            set { SaveConfig("PasswordDbc", value); }
        }
        public static string UsernameDeCaptcher
        {
            get { return LoadConfigString("UsernameDeCaptcher", "lanlan"); }
            set { SaveConfig("UsernameDeCaptcher", value); }
        }
        public static string PasswordDeCaptcher
        {
            get { return LoadConfigString("PasswordDeCaptcher", "haNgan321"); }
            set { SaveConfig("PasswordDeCaptcher", value); }
        }
        public static string UsernameImagetyperz
        {
            get { return LoadConfigString("UsernameImagetyperz", "lanlan452"); }
            set { SaveConfig("UsernameImagetyperz", value); }
        }
        public static string PasswordImagetyperz
        {
            get { return LoadConfigString("PasswordImagetyperz", "haNgan321"); }
            set { SaveConfig("PasswordImagetyperz", value); }
        }
        public static string UsernameShanibpo
        {
            get { return LoadConfigString("UsernameShanibpo", "lanlan"); }
            set { SaveConfig("UsernameShanibpo", value); }
        }
        public static string PasswordShanibpo
        {
            get { return LoadConfigString("PasswordShanibpo", "haNgan321"); }
            set { SaveConfig("PasswordShanibpo", value); }
        }
        public static int DecaptchaTimeout
        {
            get { return LoadConfigInt32("DecaptchaTimeout", 15000); }
            set { SaveConfig("DecaptchaTimeout", value); }
        }

        public static string LastUsername
        {
            get { return LoadConfigString("LastUsername", string.Empty); }
            set { SaveConfig("LastUsername", value); }
        }

        public static string ReportServerAddr
        {
            get { return LoadConfigString("ReportServerAddr", "127.0.0.1"); }
            set { SaveConfig("ReportServerAddr", value); }
        }

        public static int ReportServerPort
        {
            get { return LoadConfigInt32("ReportServerPort", 11200); }
            set { SaveConfig("ReportServerPort", value); }
        }

        public static int MaxRetryCheckin
        {
            get { return LoadConfigInt32("MaxRetryCheckin", 0); }
            set { SaveConfig("MaxRetryCheckin", value); }
        }

        public static int MaxCaptchaManual
        {
            get { return LoadConfigInt32("MaxCaptchaManual", 3); }
            set { SaveConfig("MaxCaptchaManual", value); }
        }

        public static int CaptchaManual
        {
            get { return LoadConfigInt32("CaptchaManual", 0); }
            set { SaveConfig("CaptchaManual", value); }
        }

        public static int ForceSubmitDelay
        {
            get { return LoadConfigInt32("ForceSubmitDelay", 100); }
            set { SaveConfig("ForceSubmitDelay", value); }
        }

        public static int ForceSubmitCount
        {
            get { return LoadConfigInt32("ForceSubmitCount", 3); }
            set { SaveConfig("ForceSubmitCount", value); }
        }

        public static void SaveConfig(string key, object value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var data = config.AppSettings.Settings;
            if (data[key] == null) data.Add(key, string.Empty);
            data[key].Value = value.ToString();
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static Int32 LoadConfigInt32(string key, Int32 defaultValue)
        {
            int temp = 0;
            return int.TryParse(ConfigurationManager.AppSettings[key], out temp) ? temp : defaultValue;
        }

        public static string LoadConfigString(string key, string defaultValue)
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        public static bool UseProxy { get; set; }

        private static List<WebProxy> _proxyList;
        public static List<WebProxy> ProxyList
        {
            get 
            {
                if (_proxyList == null)
                {
                    string str = LoadConfigString("ProxyList", string.Empty);
                    _proxyList = new List<WebProxy>();
                    string[] data = str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string d in data)
                    {
                        _proxyList.Add(new WebProxy(d.Trim()));
                    }
                }

                return _proxyList; 
            }
            set
            {
                _proxyList = value;
                if (_proxyList == null)
                    SaveConfig("ProxyList", string.Empty);
                else
                {
                    string str = string.Empty;
                    foreach (WebProxy proxy in value)
                    {
                        str += string.Format("{0}:{1};", proxy.Address.Host, proxy.Address.Port);
                    }
                    SaveConfig("ProxyList", str);
                }
            }
        }
    }

    public static class VarState
    {
        public static string Running = "Running";
        public static string Ready = "Ready";
        public static string Success = "Success";
        public static string Failed = "Failed";
        public static string Error = "Error";
        public static string NotOpen = "NotOpen";
        public static string NotSubmit = "NotSubmit";
        public static string InvalidCaptcha = "InvalidCaptcha";
        public static string ForceStopped = "ForceStopped";
    }
}
