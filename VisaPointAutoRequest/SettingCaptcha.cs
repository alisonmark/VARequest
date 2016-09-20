using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisaPointAutoRequest
{
    class SettingCaptcha
    {        
        public string account { get; set; }
        public string password { get; set; }

        public SettingCaptcha(string account, string password)
        {
            this.account = account;
            this.password = password;
        }
    }
}
