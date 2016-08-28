using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;

namespace VAR_Report_Server
{
    public static class Setting
    {
        public static string MailSenderUsername
        {
            get { return LoadConfigString("MailSenderUsername", ""); }
            set { SaveConfig("MailSenderUsername", value); }
        }
        public static string MailSenderPassword
        {
            get { return LoadConfigString("MailSenderPassword", ""); }
            set { SaveConfig("MailSenderPassword", value); }
        }

        public static string MailReceiverUsername
        {
            get { return LoadConfigString("MailReceiverUsername", ""); }
            set { SaveConfig("MailReceiverUsername", value); }
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
    }
}
