using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisaPointAutoRequest
{
    class Purpose
    {

        public string getPostDataPurpose01(string rsm1, string viewState, string generator, string eventValidation, string citizenShip, string residence, string embassy, string citizenIndex)
        {
            return String.Format("rsm1_TSM={0}&__EVENTTARGET=ctl00%24cp1%24ddVisaType&__EVENTARGUMENT=%7B%22Command%22%3A%22Select%22%2C%22Index%22%3A{7}%7D&__VIEWSTATE={1}&__VIEWSTATEGENERATOR={2}&__VIEWSTATEENCRYPTED="
                                            + "&__EVENTVALIDATION={3}&ctl00$ddLocale=English+%28United+Kingdom%29&ctl00_ddLocale_ClientState=&ctl00$cp1$ddCitizenship={4}"
                                            + "&ctl00_cp1_ddCitizenship_ClientState=&ctl00$cp1$ddCountryOfResidence={5}&ctl00_cp1_ddCountryOfResidence_ClientState="
                                            + "&ctl00$cp1$ddEmbassy={6}&ctl00_cp1_ddEmbassy_ClientState=&ctl00$cp1$ddVisaType=Application+for+Permanent+Residence+Permit&ctl00_cp1_ddVisaType_ClientState=%7B%22logEntries%22%3A%5B%5D%2C%22value%22%3A%227%22%2C%22text%22%3A%22Application%20for%20Permanent%20Residence%20Permit%22%2C%22enabled%22%3Atrue%2C%22checkedIndices%22%3A%5B%5D%2C%22checkedItemsTextOverflows%22%3Afalse%7D"
                                            + "&ctl00_cp1_btnNext_ClientState=%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                            , rsm1, viewState, generator, eventValidation, citizenShip, residence, embassy, citizenIndex);
        }

        public string getPostDataPurpose02(string rsm1, string viewState, string generator, string eventValidation, string citizenShip, string residence, string embassy, string citizenIndex)
        {
            return String.Format("rsm1_TSM={0}&__EVENTTARGET=ctl00%24cp1%24ddVisaType&__EVENTARGUMENT=%7B%22Command%22%3A%22Select%22%2C%22Index%22%3A{7}%7D&__VIEWSTATE={1}&__VIEWSTATEGENERATOR={2}&__VIEWSTATEENCRYPTED="
                                + "&__EVENTVALIDATION={3}&ctl00$ddLocale=English+%28United+Kingdom%29&ctl00_ddLocale_ClientState=&ctl00$cp1$ddCitizenship={4}"
                                + "&ctl00_cp1_ddCitizenship_ClientState=&ctl00$cp1$ddCountryOfResidence={5}&ctl00_cp1_ddCountryOfResidence_ClientState="
                                + "&ctl00$cp1$ddEmbassy={6}&ctl00_cp1_ddEmbassy_ClientState=&ctl00$cp1$ddVisaType=Residence+permit&ctl00_cp1_ddVisaType_ClientState=%7B%22logEntries%22%3A%5B%5D%2C%22value%22%3A%2220%22%2C%22text%22%3A%22Long-term%20residence%20permit%22%2C%22enabled%22%3Atrue%2C%22checkedIndices%22%3A%5B%5D%2C%22checkedItemsTextOverflows%22%3Afalse%7D"
                                + "&ctl00_cp1_btnNext_ClientState=%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                , rsm1, viewState, generator, eventValidation, citizenShip, residence, embassy, citizenIndex);
        }

        public string getPostDataPurpose03(string rsm1, string viewState, string generator, string eventValidation, string citizenShip, string residence, string embassy, string citizenIndex)
        {
            return String.Format("rsm1_TSM={0}&__EVENTTARGET=ctl00%24cp1%24ddVisaType&__EVENTARGUMENT=%7B%22Command%22%3A%22Select%22%2C%22Index%22%3A{7}%7D&__VIEWSTATE={1}&__VIEWSTATEGENERATOR={2}&__VIEWSTATEENCRYPTED="
                                + "&__EVENTVALIDATION={3}&ctl00$ddLocale=English+%28United+Kingdom%29&ctl00_ddLocale_ClientState=&ctl00$cp1$ddCitizenship={4}"
                                + "&ctl00_cp1_ddCitizenship_ClientState=&ctl00$cp1$ddCountryOfResidence={5}&ctl00_cp1_ddCountryOfResidence_ClientState="
                                + "&ctl00$cp1$ddEmbassy={6}&ctl00_cp1_ddEmbassy_ClientState=&ctl00$cp1$ddVisaType=Residence+permit&ctl00_cp1_ddVisaType_ClientState=%7B%22logEntries%22%3A%5B%5D%2C%22value%22%3A%2218%22%2C%22text%22%3A%22Long-term%20visa%22%2C%22enabled%22%3Atrue%2C%22checkedIndices%22%3A%5B%5D%2C%22checkedItemsTextOverflows%22%3Afalse%7D"
                                + "&ctl00_cp1_btnNext_ClientState=%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                , rsm1, viewState, generator, eventValidation, citizenShip, residence, embassy, citizenIndex);
        }

        public string getPostDataPurpose04(string rsm1, string viewState, string generator, string eventValidation, string citizenShip, string residence, string embassy, string citizenIndex)
        {
            return String.Format("rsm1_TSM={0}&__EVENTTARGET=ctl00%24cp1%24ddVisaType&__EVENTARGUMENT=%7B%22Command%22%3A%22Select%22%2C%22Index%22%3A{7}%7D&__VIEWSTATE={1}&__VIEWSTATEGENERATOR={2}&__VIEWSTATEENCRYPTED="
                                + "&__EVENTVALIDATION={3}&ctl00$ddLocale=English+%28United+Kingdom%29&ctl00_ddLocale_ClientState=&ctl00$cp1$ddCitizenship={4}"
                                + "&ctl00_cp1_ddCitizenship_ClientState=&ctl00$cp1$ddCountryOfResidence={5}&ctl00_cp1_ddCountryOfResidence_ClientState="
                                + "&ctl00$cp1$ddEmbassy={6}&ctl00_cp1_ddEmbassy_ClientState=&ctl00$cp1$ddVisaType=Residence+permit&ctl00_cp1_ddVisaType_ClientState=%7B%22logEntries%22%3A%5B%5D%2C%22value%22%3A%223%22%2C%22text%22%3A%22Long-term%20visa%20for%20Business%22%2C%22enabled%22%3Atrue%2C%22checkedIndices%22%3A%5B%5D%2C%22checkedItemsTextOverflows%22%3Afalse%7D"
                                + "&ctl00_cp1_btnNext_ClientState=%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                , rsm1, viewState, generator, eventValidation, citizenShip, residence, embassy, citizenIndex);
        }

        public string getPostDataPurpose05(string rsm1, string viewState, string generator, string eventValidation, string citizenShip, string residence, string embassy, string citizenIndex)
        {
            return String.Format("rsm1_TSM={0}&__EVENTTARGET=ctl00%24cp1%24ddVisaType&__EVENTARGUMENT=%7B%22Command%22%3A%22Select%22%2C%22Index%22%3A{7}%7D&__VIEWSTATE={1}&__VIEWSTATEGENERATOR={2}&__VIEWSTATEENCRYPTED="
                                + "&__EVENTVALIDATION={3}&ctl00$ddLocale=English+%28United+Kingdom%29&ctl00_ddLocale_ClientState=&ctl00$cp1$ddCitizenship={4}"
                                + "&ctl00_cp1_ddCitizenship_ClientState=&ctl00$cp1$ddCountryOfResidence={5}&ctl00_cp1_ddCountryOfResidence_ClientState="
                                + "&ctl00$cp1$ddEmbassy={6}&ctl00_cp1_ddEmbassy_ClientState=&ctl00$cp1$ddVisaType=Residence+permit&ctl00_cp1_ddVisaType_ClientState=%7B%22logEntries%22%3A%5B%5D%2C%22value%22%3A%224%22%2C%22text%22%3A%22Long-term%20visa%20for%20study%22%2C%22enabled%22%3Atrue%2C%22checkedIndices%22%3A%5B%5D%2C%22checkedItemsTextOverflows%22%3Afalse%7D"
                                + "&ctl00_cp1_btnNext_ClientState=%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                , rsm1, viewState, generator, eventValidation, citizenShip, residence, embassy, citizenIndex);
        }
    }
}
