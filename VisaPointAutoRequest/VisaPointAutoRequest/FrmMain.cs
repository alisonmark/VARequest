﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisaPointAutoRequest
{
    public partial class FrmMain : Form
    {
        String stringSocks = "";
        public String captcha;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void startRequest()
        {
            clearLog();
            // Create processor object
            VisapointProcessor vp = new VisapointProcessor(stringSocks, false);
            // Request disclaimer to get cookie session
            log("Requesting to https://visapoint.eu/disclaimer");
            vp.URL = "https://visapoint.eu/disclaimer";
            vp.requestType = "GET";
            vp.reference = String.Empty;
            vp.postData = String.Empty;
            vp.SendRequest();
            showRes("Response From https://visapoint.eu/disclaimer", vp.response);
            log(vp.message);

            #region STEP 1 + 2 + 3 COMMON
            // Click accept button on disclaimer page
            log("Getting validation data...");
            String viewState = vp.GetViewState();
            String eventValidation = vp.GetEventValidation();
            String generator = vp.GetGenerator();
            String rsm1 = vp.GetRsm1();

            log("Posting to https://visapoint.eu/disclaimer");
            vp.URL = "https://visapoint.eu/disclaimer";
            vp.autoRedirect = true;
            vp.postData = String.Format("rsm1_TSM={0}&__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={1}&__VIEWSTATEGENERATOR={2}&__VIEWSTATEENCRYPTED=&__EVENTVALIDATION={3}"
                                            + "&ctl00%24ddLocale=English+%28United+Kingdom%29"
                                            + "&ctl00_ddLocale_ClientState=&ctl00_cp1_btnDecline_ClientState="
                                            + "%7B%22text%22%3A%22Decline%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                            + "&ctl00$cp1$btnAccept=Accept&ctl00_cp1_btnAccept_ClientState="
                                            + "%7B%22text%22%3A%22Accept%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                            + "&ctl00_cp1_rbClose_ClientState="
                                            + "%7B%22text%22%3A%22OK%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                            + "&ctl00_cp1_rttDecline_ClientState="
                                            , rsm1, viewState, generator, eventValidation);
            vp.requestType = "POST";
            vp.SendRequest();
            showRes("Response From https://visapoint.eu/disclaimer", vp.response);
            log(vp.message);

            // Click make a new appointment in action page
            log("Getting validation data...");
            viewState = vp.GetViewState();
            eventValidation = vp.GetEventValidation();
            generator = vp.GetGenerator();

            log("Posting to https://visapoint.eu/action");
            vp.URL = "https://visapoint.eu/action";
            vp.postData = String.Format("rsm1_TSM=&__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}&__VIEWSTATEGENERATOR={1}&__VIEWSTATEENCRYPTED=&__EVENTVALIDATION={2}"
                                            + "&ctl00%24cp1%24btnNewAppointment_input==Make+a+New+Appointment&ctl00_cp1_btnNewAppointment_ClientState=&ctl00_cp1_btnModifyAppointment_ClientState="
                                            , viewState, generator, eventValidation);
            vp.requestType = "POST";
            vp.SendRequest();
            showRes("Response From https://visapoint.eu/action", vp.response);
            log(vp.message);

            // Post to form page your citizenship data
            log("Getting validation data...");
            viewState = vp.GetViewState();
            eventValidation = vp.GetEventValidation();
            generator = vp.GetGenerator();
            rsm1 = vp.GetRsm1();

            log("Requesting to https://visapoint.eu/form");
            vp.URL = "https://visapoint.eu/form";
            vp.requestType = "POST";
            //String citizenShip = "Vietnam+%28Vi%C3%AA%CC%A3t+Nam%29";
            String citizenShip = "Albania%20(Shqip%C3%ABria)";
            String embassy = "Please+select+the+Czech+Embassy%2FConsulate+you+wish+to+visit.";
            String residence = "Select+your+country+of+residence";
            String citizenIndex = "0";
            vp.postData = String.Format("rsm1_TSM={0}&__EVENTTARGET=ctl00%24cp1%24ddCitizenship&__EVENTARGUMENT=%7B%22Command%22%3A%22Select%22%2C%22Index%22%3A{7}%7D&__VIEWSTATE={1}&__VIEWSTATEGENERATOR={2}&__VIEWSTATEENCRYPTED="
                                            + "&__EVENTVALIDATION={3}&ctl00$ddLocale=English+%28United+Kingdom%29&ctl00_ddLocale_ClientState=&ctl00$cp1$ddCitizenship={4}"
                                            + "&ctl00_cp1_ddCitizenship_ClientState=%7B%22logEntries%22%3A%5B%5D%2C%22value%22%3A%2226%22%2C%22text%22%3A%22{4}%22%2C%22enabled%22%3Atrue%2C%22checkedIndices%22%3A%5B%5D%2C%22checkedItemsTextOverflows%22%3Afalse%7D&ctl00$cp1$ddCountryOfResidence={5}&ctl00_cp1_ddCountryOfResidence_ClientState="
                                            + "&ctl00$cp1$ddEmbassy={6}&ctl00_cp1_ddEmbassy_ClientState=&ctl00$cp1$ddVisaType=Residence+permit&ctl00_cp1_ddVisaType_ClientState="
                                            + "&ctl00_cp1_btnNext_ClientState=%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                            , rsm1, viewState, generator, eventValidation, citizenShip, residence, embassy, citizenIndex);
            vp.SendRequest();
            showRes("Response From https://visapoint.eu/form", vp.response);
            log(vp.message);

            // Post to form page your visa type data
            log("Getting validation data...");
            viewState = vp.GetViewState();
            eventValidation = vp.GetEventValidation();
            generator = vp.GetGenerator();
            rsm1 = vp.GetRsm1();

            log("Requesting to https://visapoint.eu/form");
            vp.URL = "https://visapoint.eu/form";
            vp.requestType = "POST";
            //embassy = "Vietnam+%28Vi%C3%AA%CC%A3t+Nam%29+-+Hanoj";
            //residence = "Vietnam+%28Vi%C3%AA%CC%A3t+Nam%29";
            embassy = "Albania%20(Shqip%C3%ABria)%20-%20Tirana";
            residence = "Albania%20(Shqip%C3%ABria)";
            vp.postData = String.Format("rsm1_TSM={0}&__EVENTTARGET=ctl00%24cp1%24ddVisaType&__EVENTARGUMENT=%7B%22Command%22%3A%22Select%22%2C%22Index%22%3A{7}%7D&__VIEWSTATE={1}&__VIEWSTATEGENERATOR={2}&__VIEWSTATEENCRYPTED="
                                            + "&__EVENTVALIDATION={3}&ctl00$ddLocale=English+%28United+Kingdom%29&ctl00_ddLocale_ClientState=&ctl00$cp1$ddCitizenship={4}"
                                            + "&ctl00_cp1_ddCitizenship_ClientState=&ctl00$cp1$ddCountryOfResidence={5}&ctl00_cp1_ddCountryOfResidence_ClientState="
                                            + "&ctl00$cp1$ddEmbassy={6}&ctl00_cp1_ddEmbassy_ClientState=&ctl00$cp1$ddVisaType=Application+for+Permanent+Residence+Permit&ctl00_cp1_ddVisaType_ClientState=%7B%22logEntries%22%3A%5B%5D%2C%22value%22%3A%227%22%2C%22text%22%3A%22Application+for+Permanent+Residence+Permit%22%2C%22enabled%22%3Atrue%2C%22checkedIndices%22%3A%5B%5D%2C%22checkedItemsTextOverflows%22%3Afalse%7D"
                                            + "&ctl00_cp1_btnNext_ClientState=%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                            , rsm1, viewState, generator, eventValidation, citizenShip, residence, embassy, citizenIndex);
            vp.SendRequest();
            showRes("Response From https://visapoint.eu/form", vp.response);
            log(vp.message);
            #endregion

            #region STEP 3 CAPTCHA
            // Post to form page your applicant with captcha
            log("Getting validation data...");
            viewState = vp.GetViewState();
            eventValidation = vp.GetEventValidation();
            rsm1 = vp.GetRsm1();
            captcha = vp.RefreshCaptcha();

            log("Requesting to https://visapoint.eu/form");
            vp.URL = "https://visapoint.eu/form";
            vp.requestType = "POST";
            //embassy = "Vietnam+%28Vi%C3%AA%CC%A3t+Nam%29+-+Hanoj";
            //residence = "Vietnam+%28Vi%C3%AA%CC%A3t+Nam%29";
            embassy = "Albania%20(Shqip%C3%ABria)%20-%20Tirana";
            residence = "Albania%20(Shqip%C3%ABria)";
            String clientState = vp.EncodeDataString(string.Format("{{\"enabled\":true,\"emptyMessage\":\"\",\"validationText\":\"{0}\",\"valueAsString\":\"{0}\",\"valueWithPromptAndLiterals\":\"{0}\",\"lastSetTextBoxValue\":\"{0}\"}}", captcha));
            vp.postData = MakePostDataCaptcha(rsm1, viewState, eventValidation, vp.response, clientState, vp.lastCaptchaURL, citizenShip, embassy, residence);
            vp.SendRequest();
            showRes("Response From https://visapoint.eu/form", vp.response);
            //log(vp.message + "\n" + vp.postData);
            log(vp.message);
            #endregion

            #region STEP 4  Apointment
            // Post to form with applicant Apointment
            log("Getting validation data...");
            viewState = vp.GetViewState();
            eventValidation = vp.GetEventValidation();
            rsm1 = vp.GetRsm1();
            generator = vp.GetGenerator();
            string appDate = vp.getAppointDate();
            
            log("Requesting to https://visapoint.eu/form");
            vp.URL = "https://visapoint.eu/form";
            vp.requestType = "POST";

            vp.postData = string.Format("rsm1_TSM={0}"
                                        + "&__EVENTTARGET=ctl00%24cp1%24btnNext"
                                        + "&__EVENTARGUMENT={1}"
                                        + "&__VIEWSTATE={2}"
                                        + "&__VIEWSTATEGENERATOR={3}"
                                        + "&__VIEWSTATEENCRYPTED={4}"
                                        + "&__EVENTVALIDATION={5}"
                                        + "&ctl00%24ddLocale%3DEnglish%20(United%20Kingdom)"
                                        + "&ctl00_ddLocale_ClientState%3D"
                                        + "&ctl00%24cp1%24rblDate={6}"
                                        + "&ctl00_cp1_btnPrev_ClientState=%7B%22text%22%3A%22Previous%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22prev%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                        + "&ctl00_cp1_btnNext_ClientState=%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                        , rsm1
                                        , string.Empty
                                        , viewState
                                        , generator
                                        , string.Empty
                                        , eventValidation
                                        , appDate);

            vp.SendRequest();
            showRes("Response From https://visapoint.eu/form", vp.response);
            //log(vp.message + "\n" + vp.postData);
            log(vp.message);
            #endregion

            #region STEP 5 Candidate Information
            // Post to form with Information
            log("Getting validation data...");
            viewState = vp.GetViewState();
            eventValidation = vp.GetEventValidation();
            rsm1 = vp.GetRsm1();
            generator = vp.GetGenerator();
            List<string> infoCandidate = vp.getInfomation();
            DateTime today = DateTime.Today;
            DateTime minDate = today.AddYears(-120);
            string calendarSDStr = string.Format("[[{0},{1},{2}]]", today.Year.ToString(), today.Month.ToString(), "1");
            string calendarADStr = string.Format("[[{0},{1},{2}],[{3},{4},{5}],[{3},{4},{5}]]"
                                            , minDate.Year.ToString()
                                            , minDate.Month.ToString()
                                            , minDate.Day.ToString()
                                            , today.Year.ToString()
                                            , today.Month.ToString()
                                            , today.Day.ToString());

            log("Requesting to https://visapoint.eu/form");
            vp.URL = "https://visapoint.eu/form";
            vp.requestType = "POST";

            vp.postData = string.Format("rsm1_TSM={0}"
                                        + "&__LASTFOCUS="
                                        + "&__EVENTTARGET=ctl00%24cp1%24btnNext"
                                        + "&__EVENTARGUMENT={1}"
                                        + "&__VIEWSTATE={2}"
                                        + "&__VIEWSTATEGENERATOR={3}"
                                        + "&__VIEWSTATEENCRYPTED={4}"
                                        + "&__EVENTVALIDATION={5}"
                                        + "&ctl00%24ddLocale%3DEnglish%20(United%20Kingdom)"
                                        + "&ctl00_ddLocale_ClientState%3D"
                                        + "&ctl00%24cp1%24txtFirstName%3D{6}"
                                        + "&ctl00_cp1_txtFirstName_ClientState%3D%7B%22enabled%22%3Atrue%2C%22emptyMessage%22%3A%22Fill%20in%20your%20first%20name(s)%22%2C%22validationText%22%3A%22{6}%22%2C%22valueAsString%22%3A%22{6}%22%2C%22lastSetTextBoxValue%22%3A%22{6}%22%7D"
                                        + "&ctl00%24cp1%24txtFamilyName%3D{7}"
                                        + "&ctl00_cp1_txtFamilyName_ClientState%3D%7B%22enabled%22%3Atrue%2C%22emptyMessage%22%3A%22Fill%20in%20your%20Family%20Name(s)%22%2C%22validationText%22%3A%22{7}%22%2C%22valueAsString%22%3A%22{7}%22%2C%22lastSetTextBoxValue%22%3A%22{7}%22%7D"
                                        + "&ctl00%24cp1%24txtBirthDate%3D{8:yyyy-MM-dd}"
                                        + "&ctl00%24cp1%24txtBirthDate%24dateInput%3D{8:dd/MM/yyyy}"
                                        + "&ctl00_cp1_txtBirthDate_dateInput_ClientState%3D%7B%22enabled%22%3Atrue%2C%22emptyMessage%22%3A%22%22%2C%22validationText%22%3A%22{8:yyyy-MM-dd}-00-00-00%22%2C%22valueAsString%22%3A%22{8:yyyy-MM-dd}-00-00-00%22%2C%22minDateStr%22%3A%22{10:yyyy-MM-dd}-00-00%22%2C%22maxDateStr%22%3A%22{9:yyyy-MM-dd}-00-00-00%22%2C%22lastSetTextBoxValue%22%3A%22{8:dd/MM/yyyy}%22%7D"
                                        + "&ctl00_cp1_txtBirthDate_calendar_SD%3D{11}"
                                        + "&ctl00_cp1_txtBirthDate_calendar_AD%3D{12}"
                                        + "&ctl00_cp1_txtBirthDate_ClientState%3D%7B%22minDateStr%22%3A%22{10:yyyy-MM-dd}-00-00-00%22%2C%22maxDateStr%22%3A%22{9:yyyy-MM-dd}-00-00-00%22%7D"
                                        + "&ctl00%24cp1%24ddBirthCountry%3D{13}"
                                        + "&ctl00_cp1_ddBirthCountry_ClientState%3D{14}"
                                        + "&ctl00%24cp1%24ddSex%3D{15}"
                                        + "&ctl00_cp1_ddSex_ClientState%3D{16}"
                                        + "&ctl00_cp1_btnPrev_ClientState%3D%7B%22text%22%3A%22Previous%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22prev%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                        + "&ctl00_cp1_btnNext_ClientState%3D%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                        , rsm1
                                        , string.Empty
                                        , viewState
                                        , generator
                                        , string.Empty
                                        , eventValidation
                                        , infoCandidate[0]
                                        , infoCandidate[1]
                                        , vp.dateOfBirth
                                        , today
                                        , minDate
                                        , calendarSDStr
                                        , calendarADStr
                                        , infoCandidate[3]
                                        , string.Empty
                                        , infoCandidate[4]
                                        , string.Empty);

            vp.SendRequest();
            showRes("Response From https://visapoint.eu/form", vp.response);
            //log(vp.message + "\n" + vp.postData);
            log(vp.message);
            #endregion

            #region STEP 6 Passport + Email
            // Post to form with Passport + Email
            log("Getting validation data...");
            viewState = vp.GetViewState();
            eventValidation = vp.GetEventValidation();
            rsm1 = vp.GetRsm1();
            generator = vp.GetGenerator();
            infoCandidate = vp.getInfomation();
            log("Requesting to https://visapoint.eu/form");
            vp.URL = "https://visapoint.eu/form";
            vp.requestType = "POST";

            vp.postData = string.Format("rsm1_TSM={0}"
                                        + "&__LASTFOCUS="
                                        + "&__EVENTTARGET=ctl00%24cp1%24btnNext"
                                        + "&__EVENTARGUMENT={1}"
                                        + "&__VIEWSTATE={2}"
                                        + "&__VIEWSTATEGENERATOR={3}"
                                        + "&__VIEWSTATEENCRYPTED={4}"
                                        + "&__EVENTVALIDATION={5}"
                                        + "&ctl00%24ddLocale%3DEnglish%20(United%20Kingdom)"
                                        + "&ctl00_ddLocale_ClientState%3D"
                                        + "&ctl00%24cp1%24txtPassportNumber%3D{6}"
                                        + "&ctl00_cp1_txtPassportNumber_ClientState%3D%7B%22enabled%22%3Atrue%2C%22emptyMessage%22%3A%22Fill%20in%20your%20passport%20number%22%2C%22validationText%22%3A%22{6}%22%2C%22valueAsString%22%3A%22{6}%22%2C%22lastSetTextBoxValue%22%3A%22{6}%22%7D"
                                        + "&ctl00%24cp1%24txtEmail%3D{7}"
                                        + "&ctl00_cp1_txtEmail_ClientState%3D%7B%22enabled%22%3Atrue%2C%22emptyMessage%22%3A%22Fill%20in%20valid%20email%22%2C%22validationText%22%3A%22{7}%22%2C%22valueAsString%22%3A%22{7}%22%2C%22lastSetTextBoxValue%22%3A%22{7}%22%7D"
                                        + "&ctl00%24cp1%24txtPhone%3D%2B(111)%20111111111_"
                                        + "&ctl00_cp1_txtPhone_ClientState%3D%7B%22enabled%22%3Atrue%2C%22emptyMessage%22%3A%22Fill%20in%20your%20phone%20number%22%2C%22validationText%22%3A%22%2B(111)%20111111111%22%2C%22valueAsString%22%3A%22%2B(111)%20111111111_%22%2C%22valueWithPromptAndLiterals%22%3A%22%2B(111)%20111111111_%22%2C%22lastSetTextBoxValue%22%3A%22%2B(111)%20111111111_%22%7D"
                                        + "&ctl00_cp1_btnPrev_ClientState%3D%7B%22text%22%3A%22Previous%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22prev%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                        + "&ctl00_cp1_btnNext_ClientState%3D%7B%22text%22%3A%22Next%22%2C%22value%22%3A%22%22%2C%22checked%22%3Afalse%2C%22target%22%3A%22%22%2C%22navigateUrl%22%3A%22%22%2C%22commandName%22%3A%22next%22%2C%22commandArgument%22%3A%22%22%2C%22autoPostBack%22%3Atrue%2C%22selectedToggleStateIndex%22%3A0%2C%22validationGroup%22%3Anull%2C%22readOnly%22%3Afalse%7D"
                                        , rsm1
                                        , string.Empty
                                        , viewState
                                        , generator
                                        , string.Empty
                                        , eventValidation
                                        , infoCandidate[5]
                                        , infoCandidate[6]
                                        , "+(111) 111111111_"
                                        , "+(111) 111111111");

            vp.SendRequest();
            showRes("Response From https://visapoint.eu/form", vp.response);
            //log(vp.message + "\n" + vp.postData);
            log(vp.message);
            #endregion
        }

        private String MakePostDataCaptcha(String rsm1, String viewState, String eventValidation, String content, String clientState, String captchaURL, String citizen, String embassy, String residence)
        {
            string t = Regex.Match(captchaURL, "t=(?<value>[^&]{10,})").Groups["value"].ToString();
            string wrapper = Regex.Match(content, "ctl00_cp1_(?<value>[^_]+)_wrapper").Groups["value"].ToString();

            string postData = string.Format("rsm1_TSM={0}"
                                        + "&__EVENTTARGET=ctl00%24cp1%24btnNext"
                                        + "&__EVENTARGUMENT={3}"
                                        + "&__VIEWSTATE={1}"
                                        + "&__VIEWSTATEENCRYPTED="
                                        + "&__EVENTVALIDATION={2}"
                                        + "&ctl00%24ddLocale=English+%28United+Kingdom%29"
                                        + "&ctl00_ddLocale_ClientState="
                                        + "&ctl00%24cp1%24ddCitizenship={4}"
                                        + "&ctl00_cp1_ddCitizenship_ClientState="
                                        + "&ctl00%24cp1%24ddCountryOfResidence={5}"
                                        + "&ctl00_cp1_ddCountryOfResidence_ClientState="
                                        + "&ctl00%24cp1%24ddEmbassy={6}"
                                        + "&ctl00_cp1_ddEmbassy_ClientState="
                                        + "&ctl00%24cp1%24ddVisaType={7}"
                                        + "&ctl00_cp1_ddVisaType_ClientState="
                                        + "&ctl00%24cp1%24{11}={8}"
                                        + "&ctl00_cp1_{11}_ClientState={9}"
                                        + "&LBD_VCID_c_pages_form_cp1_captcha1={10}"
                                        + "&ctl00_cp1_btnNext_ClientState="
                                        , rsm1
                                        , viewState
                                        , eventValidation
                                        , string.Empty
                                        , citizen
                                        , residence
                                        , embassy
                                        , "Application+for+Permanent+Residence+Permit"
                                        , captcha
                                        , clientState
                                        , t
                                        , wrapper);

            return postData;
        }

        private void typeCaptcha(String nothing)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<String>(typeCaptcha), new object[] { nothing });
                return;
            }
            InputBox input = new InputBox();
            input.Text = "Captcha";
            input.parent = this;
            input.ShowDialog(this);
        }

        private void showRes(String title, String content)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate () { showRes(title, content); }));
                return;
            }
            Response step1Res = new Response();
            step1Res.Text = title;
            step1Res.setRes(content);
            step1Res.Show();
        }

        private void log(String message)
        {
            bw.ReportProgress(0, message + Environment.NewLine);
        }

        private void clearLog()
        {
            setTextBox("");
        }

        public void setTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(setTextBox), new object[] { value });
                return;
            }
            txtLog.Text = value;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            startRequest();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtLog.Text += e.UserState as String;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
        }
    }
}