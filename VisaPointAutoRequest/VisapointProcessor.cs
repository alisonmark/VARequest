using HtmlAgilityPack;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace VisaPointAutoRequest
{

    class VisapointProcessor
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(FrmMain));
        WebRequest req = null;
        Stream dataStream = null;
        private String response_rsm1;
        public String lastCaptchaURL { get; set; }
        public Socks socks { get; set; }
        public String requestType { get; set; }
        public String response { get; set; }
        public String URL { get; set; }
        public String message { get; set; }
        public String reference { get; set; }
        public String postData { get; set; }
        public Boolean autoRedirect { get; set; }
        public Bitmap sessionCaptcha { get; set; }

        private CookieCollection cookies { get; set; }
        private String _rgViewState = "<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=\"(?<value>.*?)\" />";
        private String _rgEventValidation = "<input type=\"hidden\" name=\"__EVENTVALIDATION\" id=\"__EVENTVALIDATION\" value=\"(?<value>.*?)\" />";
        private String _rgGenerator = "<input type=\"hidden\" name=\"__VIEWSTATEGENERATOR\" id=\"__VIEWSTATEGENERATOR\" value=\"(?<value>.*?)\" />";
        private String _captcha = "<img class=\".*?\" id=\".*?\" src=\"(?<value>.*?)\" alt=\"CAPTCHA\" />";
        private String _lbdVcid = "<input type=\"hidden\" name=\"LBD_VCID_c_pages_form_cp1_captcha1\" id=\"LBD_VCID_c_pages_form_cp1_captcha1\" value=\"(?<value>.*?)\" />";

        // Add new
        private Random _rand = new Random();
        private List<KeyValueItem> _appointDate = new List<KeyValueItem>();
        private List<string> _infoLst = new List<string>();
        public string strInfo { get; set; }
        public DateTime dateOfBirth { get; set; }
        // End add new

        public VisapointProcessor()
        {
            return;
        }

        public VisapointProcessor(String stringSocks, bool isSocks)
        {
            this.autoRedirect = false;
            if (!String.IsNullOrEmpty(stringSocks.Trim()))
            {
                this.socks = new Socks(stringSocks, isSocks);
            }
            else
            {
                this.socks = null;
            }
        }

        public void SendRequest(bool isImage = false)
        {
            if (!validate())
            {
                return;
            }

            if (socks != null && socks.isSocks)
            {
                message = "Socks is not supported yet.";
                return;
            }
            else
            {
                byte[] byteArray = null;
                req = (HttpWebRequest)WebRequest.Create(URL);
                req.Method = requestType;
                ((HttpWebRequest)req).UserAgent = "Opera/9.80 (J2ME/MIDP; Opera Mini/4.2.15410/870; U; en) Presto/2.4.15";
                ((HttpWebRequest)req).Host = "visapoint.eu";
                ((HttpWebRequest)req).Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                ((HttpWebRequest)req).Referer = reference;

                if (isImage)
                {
                    ((HttpWebRequest)req).Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
                    ((HttpWebRequest)req).Headers["Accept-Language"] = "en-US,en;q=0.8,vi;q=0.6";
                    ((HttpWebRequest)req).Headers["Accept-Encoding"] = "gzip,deflate,sdch";
                    ((HttpWebRequest)req).AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                    ((HttpWebRequest)req).KeepAlive = true;
                    ((HttpWebRequest)req).ContentType = @"text/html; charset=utf-8";
                }

                ((HttpWebRequest)req).AllowAutoRedirect = autoRedirect;
                if (autoRedirect)
                {
                    ((HttpWebRequest)req).MaximumAutomaticRedirections = 1;
                }
                if (socks != null)
                {
                    //req.Proxy = new WebProxy();
                    //req.Proxy.
                    Uri newUri = new Uri("http://" + socks.host + ":" + socks.port);
                    WebProxy myProxy = new WebProxy(newUri);
                    //Uri newUri = new Uri("http://" + socks.host + ":" + socks.port);
                    // Associate the newUri object to 'myProxy' object so that new myProxy settings can be set.
                    //myProxy.Address = newUri;
                    // Create a NetworkCredential object and associate it with the
                    // Proxy property of request object.
                    myProxy.Credentials = new NetworkCredential("lanlan451@gmail.com", "vArDF2ssvkpU");
                    socks.myProxy = myProxy;
                    req.Proxy = socks.myProxy;
                }
                ((HttpWebRequest)req).CookieContainer = new CookieContainer();

                if (cookies != null && cookies.Count > 0)
                {
                    foreach (Cookie cookie in cookies)
                    {
                        ((HttpWebRequest)req).CookieContainer.Add(cookie);
                    }
                }
                req.Headers.Add("cache-control", "no-cache");
                if (!String.IsNullOrEmpty(postData))
                {
                    byteArray = Encoding.UTF8.GetBytes(postData.Replace("$", "%24"));
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = byteArray.Length;

                    dataStream = req.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }

                // If get image
                if (isImage)
                {
                    using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                    {
                        char[] chars = new char[16384];
                        using (BufferedStream buffer = new BufferedStream(response.GetResponseStream(), 16384))
                        {
                            sessionCaptcha = new Bitmap(buffer);
                        }
                    }

                    return;
                }

                WebResponse res = req.GetResponse();
                dataStream = res.GetResponseStream();

                if (!URL.Contains("Telerik") && ((HttpWebResponse)res).Cookies.Count > 0)
                {
                    foreach (Cookie cookie in ((HttpWebResponse)res).Cookies)
                    {
                        if (cookies == null)
                        {
                            cookies = new CookieCollection();
                        }
                        cookies.Add(cookie);
                    }
                    reference = URL;
                }

                StreamReader reader = new StreamReader(dataStream);
                message = "Request " + requestType + " to " + URL + " Complete";

                if (URL.Contains("Telerik"))
                {
                    response_rsm1 = reader.ReadToEnd();
                }
                else
                {
                    response = reader.ReadToEnd();
                }

                reader.Close();
                res.Close();
            }
        }

        #region CLONE
        public String RefreshCaptcha()
        {
            Bitmap bmp = null;
            if (!string.IsNullOrEmpty(this.response))
            {
                string rgCaptcha = "<img[^>]*id=\"c_pages_form_cp1_captcha1_CaptchaImage\" [^>]*src=\"(?<value>[^\"]+)\"[^>]*>";
                Match m = Regex.Match(this.response, rgCaptcha);
                if (m.Success)
                {
                    this.URL = "https://visapoint.eu" + m.Groups["value"].ToString();
                    this.requestType = "GET";
                    this.postData = String.Empty;
                    this.SendRequest(true);
                    bmp = this.sessionCaptcha;
                    lastCaptchaURL = this.URL;
                    return getDecaptcha(bmp);
                }
                else
                {
                    bmp = new Bitmap(250, 50);
                }
            }
            else
            {
                if (Regex.IsMatch(lastCaptchaURL, "d=\\d+"))
                    lastCaptchaURL = Regex.Replace(lastCaptchaURL, "d=\\d+", "d=" + DateTime.Now.Ticks);
                else
                    lastCaptchaURL += "&d=" + DateTime.Now.Ticks;
                string rgCaptcha = "<img[^>]*id=\"c_pages_form_cp1_captcha1_CaptchaImage\" [^>]*src=\"(?<value>[^\"]+)\"[^>]*>";
                Match m = Regex.Match(this.response, rgCaptcha);
                this.URL = "https://visapoint.eu" + m.Groups["value"].ToString();
                this.requestType = "GET";
                this.postData = String.Empty;
                this.SendRequest(true);
                bmp = this.sessionCaptcha;
                return getDecaptcha(bmp);
            }

            return String.Empty;
        }
        #endregion CLONE

        #region Get AppointDate
        public string getAppointDate()
        {
            string strAppointDate = "";

            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(response);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//table[@id='cp1_rblDate']//input[@type='radio']");
                if (nodes != null)
                {
                    int count = 0;
                    _appointDate.Clear();
                    foreach (HtmlNode node in nodes)
                    {
                        if (count > 6) break;
                        KeyValueItem item = new KeyValueItem(node.Attributes["value"].Value, node.ParentNode.InnerText.Replace("&nbsp;", " "));
                        _appointDate.Add(item);
                    }
                }
                KeyValueItem selectedItem = null;
                int randomIndex = _rand.Next(0, _appointDate.Count);
                if (_appointDate.Count > 0)
                {
                    selectedItem = _appointDate[randomIndex];
                }

                if (selectedItem == null)
                {
                    _log.Info("Lỗi khi chọn appointment date");
                }
                strAppointDate = selectedItem.Value;
                Console.WriteLine("Chọn appointment date OK ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi chọn appointment date : " + ex);
            }

            return strAppointDate;
        }
        #endregion

        #region GET INFOMATION
        public List<string> getInfomation()
        {
            string[] elementInStringInfo = strInfo.Split(new char[] { ',', ':', '-' });
            _infoLst.Clear();
            foreach (string s1 in elementInStringInfo)
            {
                _infoLst.Add(s1);
            }
            dateOfBirth = Convert.ToDateTime(_infoLst[2]);
            //string[] elementInStringDateOfBirth = _infoLst[2].Split(new char[] { '/' });
            //foreach (string s1 in elementInStringDateOfBirth)
            //{
            //    _infoLst.Add(s1);
            //}
            return _infoLst;
        }
        #endregion

        public string getDecaptcha(Bitmap bmp)
        {
            bmp.Save(@".\\captcha.bmp");
            string captcha = "";
            captcha = CaptchaUtils.DeathByCaptchaDecaptcher.Decaptcha(bmp, "alisonmark", "china2902");
            return RefineCaptchaString(captcha);
        }

        private string RefineCaptchaString(string captcha)
        {
            string text = captcha.Trim();
            string s = string.Empty;
            for (int i = 1; i < text.Length; i++)
                if (text[i] != ' ' && text[i - 1] != ' ')
                    s += text[i - 1].ToString() + '+';
                else
                    s += text[i - 1];
            if (text.Length > 0)
                s += text[text.Length - 1];
            s = s.ToUpper();
            return s;
        }

        public String getCaptcha()
        {
            return "http://visapoint.eu" + Regex.Match(response, _captcha).Groups["value"].ToString();
        }

        private String readCaptcha(String content)
        {
            StringBuilder sb = new StringBuilder(content);

            foreach (Match match in Regex.Matches(content, _captcha))
            {
                sb.Insert(match.Groups["value"].Index, "http://visapoint.eu").Replace("amp;", "");
            }

            return sb.ToString();
        }

        private bool validate()
        {
            bool rs = true;
            Uri uriResult;

            if (String.IsNullOrEmpty(URL))
            {
                rs = false;
                message = "URL is empty, where do you go?";
            }
            if (String.IsNullOrEmpty(requestType))
            {
                rs = false;
                message = "Request type is empty, how do you go?";
            }
            bool result = Uri.TryCreate(URL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!result)
            {
                rs = false;
                message = "URL is not valid.";
            }
            if (requestType != "GET" && requestType != "POST")
            {
                rs = false;
                message = "Request type is not valid.";
            }

            return rs;
        }

        public String GetRsm1()
        {
            String link = "https://visapoint.eu" + Regex.Match(response, "\"(?<value>/Telerik.Web.UI.WebResource.axd[^\"]*PublicKeyToken[^\"]*)\"").Groups["value"].ToString();
            link = link.Replace("amp;", "");
            URL = link;
            requestType = "GET";
            postData = String.Empty;
            SendRequest();

            String rsm1 = EncodeDataString(Regex.Match(response_rsm1, @"hf\.value \+= '(?<value>[^']*)';").Groups["value"].ToString());
            if (!rsm1.Contains("en-US"))
            {
                rsm1 = rsm1.Replace("%3Aen%3A", "%3Aen-US%3A");
            }
            return rsm1;
        }

        public String GetViewState()
        {
            String result = String.Empty;
            String text = Regex.Match(response, _rgViewState).Groups["value"].ToString();
            String subText = text;

            while (subText.Length > 32766)
            {
                result += EncodeDataString(subText.Substring(0, 32766));
                subText = subText.Substring(32766, subText.Length - 32766);
            }

            result += EncodeDataString(subText);

            return result;
        }

        public String getLbdVcid()
        {
            return Regex.Match(response, _lbdVcid).Groups["value"].ToString();
        }

        public String GetEventValidation()
        {
            return EncodeDataString(Regex.Match(response, _rgEventValidation).Groups["value"].ToString());
        }

        public String GetGenerator()
        {
            return Regex.Match(response, _rgGenerator).Groups["value"].ToString();
        }

        public String EncodeDataString(String source)
        {
            return Uri.EscapeDataString(source).Replace("%20", "+").Replace("(", "%28").Replace(")", "%29");
        }
    }
}
