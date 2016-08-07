using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Diagnostics;
using log4net;

namespace HttpRequestHelper
{
    public class HttpHelper
    {
        // Logger
        private static readonly ILog _log = LogManager.GetLogger(typeof(HttpHelper));

        public WebProxy Proxy = null;

        const int RECV_BUFF_SIZE = 16384;
        protected CookieCollection _cookieCollection = new CookieCollection();
        protected CookieContainer _cookiesContainer = new CookieContainer();
        protected readonly object _lockCookie = new object();
        private string _lastUrl = string.Empty;
        //public FpEventHandler<RequestResult> HttpRequestCompleted;

        public string LastUrl
        {
            get { return _lastUrl; }
            set { _lastUrl = value; }
        }

        public virtual string FetchHttpGet(string url, string referer, int timeout = 20000)
        {
            try
            {
                Stopwatch st = Stopwatch.StartNew();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:5.0) Gecko/20100101 Firefox/5.0";
                request.Method = "GET";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
                request.Headers["Accept-Language"] = "en-US,en;q=0.8,vi;q=0.6";
                request.Headers["Accept-Encoding"] = "gzip,deflate";

                request.AllowAutoRedirect = true;
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                request.KeepAlive = true;

                // request.Pipelined = true;
                // request.ProtocolVersion = new Version("1.1");

                request.Proxy = Proxy;

                request.Timeout = timeout;
                request.ContentType = @"text/html; charset=utf-8";
                if (referer.Length > 0)
                    request.Referer = referer;
                request.CookieContainer = _cookiesContainer;

                // Get the response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    _lastUrl = response.ResponseUri.AbsoluteUri;

                    // Add cookie
                    lock (_lockCookie)
                    {
                        _cookieCollection.Add(response.Cookies);
                        _cookiesContainer.Add(_cookieCollection);
                    }

                    #region Read response from the stream
                    char[] chars = new char[RECV_BUFF_SIZE];
                    using (BufferedStream buffer = new BufferedStream(response.GetResponseStream(), RECV_BUFF_SIZE))
                    {
                        // Read the response from the stream
                        using (StreamReader responseStream = new StreamReader(buffer, Encoding.UTF8, true, RECV_BUFF_SIZE))
                        {
                            StringBuilder result = new StringBuilder();
                            int charRead = responseStream.Read(chars, 0, RECV_BUFF_SIZE);
                            while (charRead > 0)
                            {
                                result.Append(chars, 0, charRead);
                                charRead = responseStream.Read(chars, 0, RECV_BUFF_SIZE);
                            }

                            st.Stop();
                            //if (HttpRequestCompleted != null)
                            //    HttpRequestCompleted(this, new RequestResult { Method = "HttpGet", RequestUrl = url, ResponseUrl = _lastUrl, Content = result.ToString(), Duration = st.ElapsedMilliseconds });
                            return result.ToString();
                        }
                    }
                    #endregion
                }


            }
            catch (Exception e)
            {
                //LogUtils.WriteLog(string.Format("FetchHttpGet Error: {0}\r\n\tUrl = {1}\r\n\tRefer = {2}", e.Message, url, referer), "Err");
                _log.Error(e);
                throw;
            }
        }
        public virtual string FetchHttpPost(string url, string referer, string postData, int timeout = 20000)
        {
            try
            {
                Stopwatch st = Stopwatch.StartNew();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:5.0) Gecko/20100101 Firefox/5.0";
                request.Method = "POST";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
                request.Headers["Accept-Language"] = "en-US,en;q=0.8,vi;q=0.6";
                request.Headers["Accept-Encoding"] = "gzip,deflate";

                request.AllowAutoRedirect = true;
                request.Proxy = Proxy;

                request.KeepAlive = true;

                // request.Pipelined = true;
                // request.ProtocolVersion = new Version("1.1");

                request.ContentType = @"application/x-www-form-urlencoded";
                request.Timeout = timeout;
                request.Referer = referer;
                request.CookieContainer = _cookiesContainer;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                // Prepare Post Data
                byte[] postBuffer = Encoding.GetEncoding(1252).GetBytes(postData);
                request.ContentLength = postBuffer.Length;
                Stream postDataStream = request.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();

                // Get the response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    _lastUrl = response.ResponseUri.AbsoluteUri;

                    // Add cookies 
                    lock (_lockCookie)
                    {
                        _cookieCollection.Add(response.Cookies);
                        _cookiesContainer.Add(_cookieCollection);
                    }

                    #region Read the response from the stream
                    char[] chars = new char[RECV_BUFF_SIZE];
                    using (BufferedStream buffer = new BufferedStream(response.GetResponseStream(), RECV_BUFF_SIZE))
                    {
                        // Read the response from the stream
                        using (StreamReader responseStream = new StreamReader(buffer, Encoding.UTF8, true, RECV_BUFF_SIZE))
                        {
                            StringBuilder result = new StringBuilder();
                            int charRead = responseStream.Read(chars, 0, RECV_BUFF_SIZE);
                            while (charRead > 0)
                            {
                                result.Append(chars, 0, charRead);
                                charRead = responseStream.Read(chars, 0, RECV_BUFF_SIZE);
                            }

                            st.Stop();
                            //if (HttpRequestCompleted != null)
                            //    HttpRequestCompleted(this, new RequestResult { Method = "HttpPost", RequestUrl = url, ResponseUrl = _lastUrl, Content = result.ToString(), Duration = st.ElapsedMilliseconds });
                            return result.ToString();
                        }
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                //LogUtils.WriteLog(string.Format("FetchHttpGet Error: {0}\r\n\tUrl = {1}\r\n\tRefer = {2}\r\n\tPostData = {3}", e.Message, url, referer, postData), "Err");
                _log.Error(e);
                throw;
            }
        }
        public virtual string FetchHttpGetAjax(string url, string referer, int timeout = 20000)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:5.0) Gecko/20100101 Firefox/5.0";
                request.Method = "GET";
                request.Accept = "Accept: text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01";
                request.Headers["Accept-Language"] = "en-US,en;q=0.8,vi;q=0.6";
                request.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
                request.Headers["X-Requested-With"] = "XMLHttpRequest";

                request.AllowAutoRedirect = true;
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                request.KeepAlive = false;

                // request.Pipelined = true;
                // request.ProtocolVersion = new Version("1.1");

                request.Proxy = Proxy;

                request.Timeout = timeout;
                request.ContentType = @"text/html; charset=utf-8";
                if (referer.Length > 0)
                    request.Referer = referer;
                request.CookieContainer = _cookiesContainer;

                // Get the response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    _lastUrl = response.ResponseUri.AbsoluteUri;

                    // Add cookie
                    lock (_lockCookie)
                    {
                        _cookieCollection.Add(response.Cookies);
                        _cookiesContainer.Add(_cookieCollection);
                    }

                    #region Read response from the stream
                    char[] chars = new char[RECV_BUFF_SIZE];
                    using (BufferedStream buffer = new BufferedStream(response.GetResponseStream(), RECV_BUFF_SIZE))
                    {
                        // Read the response from the stream
                        using (StreamReader responseStream = new StreamReader(buffer, Encoding.UTF8, true, RECV_BUFF_SIZE))
                        {
                            StringBuilder result = new StringBuilder();
                            int charRead = responseStream.Read(chars, 0, RECV_BUFF_SIZE);
                            while (charRead > 0)
                            {
                                result.Append(chars, 0, charRead);
                                charRead = responseStream.Read(chars, 0, RECV_BUFF_SIZE);
                            }

                            return result.ToString();
                        }
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                //LogUtils.WriteLog(string.Format("FetchHttpGetAjax Error: {0}\r\n\tUrl = {1}\r\n\tRefer = {2}", e.Message, url, referer), "Err");
                _log.Error(e);
                throw;
            }
        }
        public virtual Bitmap FetchHttpImage(string url, string referer, int timeout = 20000)
        {
            try
            {
                Stopwatch st = Stopwatch.StartNew();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:5.0) Gecko/20100101 Firefox/5.0";
                request.Method = "GET";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
                request.Headers["Accept-Language"] = "en-US,en;q=0.8,vi;q=0.6";
                request.Headers["Accept-Encoding"] = "gzip,deflate,sdch";

                request.AllowAutoRedirect = true;
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                request.KeepAlive = true;

                // request.Pipelined = true;
                // request.ProtocolVersion = new Version("1.1");

                request.Proxy = Proxy;


                request.Timeout = timeout;
                request.ContentType = @"text/html; charset=utf-8";
                if (referer.Length > 0)
                    request.Referer = referer;
                request.CookieContainer = _cookiesContainer;

                // Get the response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    _lastUrl = response.ResponseUri.AbsoluteUri;

                    // Add cookie
                    lock (_lockCookie)
                    {
                        _cookieCollection.Add(response.Cookies);
                        _cookiesContainer.Add(_cookieCollection);
                    }

                    #region Read response from the stream
                    char[] chars = new char[RECV_BUFF_SIZE];
                    using (BufferedStream buffer = new BufferedStream(response.GetResponseStream(), RECV_BUFF_SIZE))
                    {
                        return new Bitmap(buffer);
                    }
                    #endregion
                }


            }
            catch (Exception e)
            {
                //LogUtils.WriteLog(string.Format("FetchHttpGet Error: {0}\r\n\tUrl = {1}\r\n\tRefer = {2}", e.Message, url, referer), "Err");
                _log.Error(e);
                throw;
            }
        }

        public virtual string FetchHttpPostSubmit(string url, string referer, string postData, out bool success)
        {
            try
            {
                Stopwatch st = Stopwatch.StartNew();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:5.0) Gecko/20100101 Firefox/5.0";
                request.Method = "POST";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
                request.Headers["Accept-Language"] = "en-US,en;q=0.8,vi;q=0.6";
                request.Headers["Accept-Encoding"] = "gzip,deflate";

                request.AllowAutoRedirect = true;
                request.Proxy = Proxy;

                request.KeepAlive = true;

                // request.Pipelined = true;
                // request.ProtocolVersion = new Version("1.1");

                request.ContentType = @"application/x-www-form-urlencoded";
                request.Timeout = 20000;
                request.Referer = referer;
                request.CookieContainer = _cookiesContainer;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                // Prepare Post Data
                byte[] postBuffer = Encoding.GetEncoding(1252).GetBytes(postData);
                request.ContentLength = postBuffer.Length;
                Stream postDataStream = request.GetRequestStream();
                postDataStream.Write(postBuffer, 0, postBuffer.Length);
                postDataStream.Close();

                // Get the response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    _lastUrl = response.ResponseUri.AbsoluteUri;
                    success = _lastUrl.Contains("confirm");

                    // Add cookies 
                    lock (_lockCookie)
                    {
                        _cookieCollection.Add(response.Cookies);
                        _cookiesContainer.Add(_cookieCollection);
                    }

                    #region Read the response from the stream
                    char[] chars = new char[RECV_BUFF_SIZE];
                    using (BufferedStream buffer = new BufferedStream(response.GetResponseStream(), RECV_BUFF_SIZE))
                    {
                        // Read the response from the stream
                        using (StreamReader responseStream = new StreamReader(buffer, Encoding.UTF8, true, RECV_BUFF_SIZE))
                        {
                            StringBuilder result = new StringBuilder();
                            int charRead = responseStream.Read(chars, 0, RECV_BUFF_SIZE);
                            while (charRead > 0)
                            {
                                result.Append(chars, 0, charRead);
                                charRead = responseStream.Read(chars, 0, RECV_BUFF_SIZE);
                            }

                            st.Stop();
                            //if (HttpRequestCompleted != null)
                            //    HttpRequestCompleted(this, new RequestResult { Method = "HttpPost", RequestUrl = url, ResponseUrl = _lastUrl, Content = result.ToString(), Duration = st.ElapsedMilliseconds });
                            return result.ToString();
                        }
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                //LogUtils.WriteLog(string.Format("FetchHttpGet Error: {0}\r\n\tUrl = {1}\r\n\tRefer = {2}\r\n\tPostData = {3}", e.Message, url, referer, postData), "Err");
                _log.Error(e);
                throw;
            }
        }

        public virtual void AddCookies(Cookie cookie)
        {
            _cookiesContainer.Add(cookie);
        }

        public void ResetCookie()
        {
            _cookiesContainer = new CookieContainer();
            _cookieCollection = new CookieCollection();
        }
    }
}
