using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Auto_VAR
{
    public partial class VarObject
    {
        public KeyValueItem Citizenship { get; set; }
        public KeyValueItem Embassy { get; set; }
        public KeyValueItem PurposeOfStay { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public KeyValueItem CountryOfBirth { get; set; }
        public string Passport { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Bitmap CaptchaImg { get; set; }
        public string CaptchaText { get; set; }

        private string _status;

        public string Status
        {
            get { lock (_lock) { return _status; } }
            set { lock (_lock) { _status = value; } }
        }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public string NotOpenTime { get; set; } // Thời gian được server trả về nếu [Result=Not Open]
        public string OpenTime { get; set; }
        public string ErrorMsg { get; set; }

        private object _lock = new object();

        public VarObject()
        {
#if TESTMODE
            Citizenship = new KeyValueItem("31", "Indonesia (Indonesia)");
            Embassy = new KeyValueItem("140", "Indonesia (Indonesia) - Jakarta");
#else
            Citizenship = new KeyValueItem("39", "Vietnam (Việt Nam)");
            Embassy = new KeyValueItem("129", "Vietnam (Việt Nam) - Hanoj");
#endif
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}"
                , PurposeOfStay.Key, Name, FamilyName, DateOfBirth, Gender, CountryOfBirth.Key, Passport, Email, Phone, Status);
        }

        public static VarObject FromString(string strData)
        {
            try
            {
                string[] data = strData.Trim().Split(';');
                return new VarObject
                {
                    PurposeOfStay = new KeyValueItem(data[0], VarItem.ListPurposeOfStayValue[VarItem.ListPurposeOfStayKey.IndexOf(int.Parse(data[0]))].ToString()),
                    Name = data[1],
                    FamilyName = data[2],
                    DateOfBirth = data[3],
                    Gender = data[4],
                    CountryOfBirth = new KeyValueItem(data[5], VarItem.ListCountryValue[VarItem.ListCountryKey.IndexOf(int.Parse(data[5]))].ToString()),
                    Passport = data[6], Email = data[7], Phone = data[8],
                    Status = data[9]
                };
            }
            catch 
            {
                return null;
            }
        }
    }
}
