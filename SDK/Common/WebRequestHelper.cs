using MarketingPlatform.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace ConsoleAppTest
{
    public class FormItem
    {
        public string Key { set; get; }

        public string Value { set; get; }

        public bool IsFile
        {
            get
            {
                if (FileData == null || FileData.Length == 0)
                    return false;

                return true;
            }
        }

        public string FileName { set; get; }

        public byte[] FileData { set; get; }
    }


    public class WebRequestHelper
    {
        public static bool LogEnabled { get; set; } = false;

        public static string Get(string url, string param = "", Encoding encoding = null)
        {
            string result = string.Empty;

            var requestUrl = string.Format(string.IsNullOrEmpty(param) ? "{0}" : "{0}?{1}", url, param);
            //Logger.Log("Begin Get. " + requestUrl);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(requestUrl);

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            req.Method = "Get";

            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    result = reader.ReadToEnd();
                }
                //Logger.Log($"End Get. {requestUrl}{Environment.NewLine}{result}");
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static T Get<T>(string url, string param, Dictionary<string, string> header, string cookieDomain, Dictionary<string, string> cookie)
        {
            string json = string.Empty;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(string.Format(string.IsNullOrEmpty(param) ? "{0}" : "{0}?{1}", url, param));
            Encoding encoding = Encoding.UTF8;
            req.Method = "Get";

            if (header != null)
            {
                foreach (string key in header.Keys)
                {
                    req.Headers.Add(key, header[key]);
                }
            }

            if (cookie != null)
            {
                req.CookieContainer = new CookieContainer();
                foreach (var item in cookie)
                {
                    var value = item.Value.Split(',');
                    foreach (var v in value)
                    {
                        req.CookieContainer.Add(new Cookie(item.Key, v, "/", cookieDomain));
                    }
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    json = reader.ReadToEnd();

                    if (LogEnabled)
                    {
                        Logger.LogMessage(json);
                    }
                }
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Get 请求封装
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static T Get<T>(string url, string param = "")
        {
            string json = string.Empty;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(string.Format(string.IsNullOrEmpty(param) ? "{0}" : "{0}?{1}", url, param));
            Encoding encoding = Encoding.UTF8;

            req.Method = "Get";

            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    json = reader.ReadToEnd();
                }

            }

            return JsonConvert.DeserializeObject<T>(json);
        }


        public static T Post<T>(string url, string param)
        {
            return Post<T>(url, param, null);
        }


        public static T Post<T>(string url, object model, bool isParamLowercase = false)
        {
            var param = GetParam(model, isParamLowercase, true);
            return Post<T>(url, param, null);
        }

        public static T PostGb2312<T>(string url, object model, bool isParamLowercase = false, Encoding responseEncoding = null)
        {
            var encoding = Encoding.GetEncoding("GB2312");
            var param = GetParam(model, true, isParamLowercase, encoding);
            return Post<T>(url, param, null, "application/x-www-form-urlencoded;charset=GB2312", encoding, responseEncoding);
        }

        public static T PostGbk<T>(string url, object model, bool isParamLowercase = false, Encoding responseEncoding = null)
        {
            var encoding = Encoding.GetEncoding("GBK");
            var param = GetParam(model, true, isParamLowercase, encoding);
            return Post<T>(url, param, null, "application/x-www-form-urlencoded;charset=GBK", encoding, responseEncoding);
        }

        public static T PostJson<T>(string url, object model)
        {
            var param = JsonConvert.SerializeObject(model);
            return Post<T>(url, param, null, "application/json");
        }

        public static T Post<T>(string url, object model, Dictionary<string, string> headers)
        {
            var param = GetParam(model);
            var response = Request("Post", url, param, headers);

            if (typeof(T) == typeof(string))
            {
                return (T)(object)response;
            }

            return JsonConvert.DeserializeObject<T>(response);
        }

        public static T Post<T>(string url, string param, Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", Encoding requestEncoding = null, Encoding responseEncoding = null)
        {
            var response = Request("Post", url, param, headers, contentType, requestEncoding, responseEncoding);

            if (typeof(T) == typeof(string))
            {
                return (T)(object)response;
            }

            return JsonConvert.DeserializeObject<T>(response);
        }

        public static T Delete<T>(string url, string param, Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", Encoding requestEncoding = null, Encoding responseEncoding = null)
        {
            var response = Request("Delete", url, param, headers, contentType, requestEncoding, responseEncoding);

            if (typeof(T) == typeof(string))
            {
                return (T)(object)response;
            }

            return JsonConvert.DeserializeObject<T>(response);
        }


        public static string PostForm(string url, List<FormItem> formItems, Dictionary<string, string> headers = null, CookieContainer cookieContainer = null, string refererUrl = null, Encoding responseEncoding = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            if (!string.IsNullOrEmpty(refererUrl))
                request.Referer = refererUrl;
            if (cookieContainer != null)
                request.CookieContainer = cookieContainer;

            SetHttpRequestHeaders(request, headers);

            string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符  
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            //请求流  
            var postStream = new MemoryStream();


            //是否用Form上传文件  
            var formUploadFile = formItems != null && formItems.Count > 0;
            if (formUploadFile)
            {
                //文件数据模板  
                string fileFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                    "\r\nContent-Type: application/octet-stream" +
                    "\r\n\r\n";
                //文本数据模板  
                string dataFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                    "\r\n\r\n{1}";
                foreach (var item in formItems)
                {
                    string formdata = null;
                    if (item.IsFile)
                    {
                        if (string.IsNullOrWhiteSpace(item.FileName))
                        {
                            throw new ArgumentException("上传文件时，FileName 不能为空！");
                        }

                        //上传文件  
                        formdata = string.Format(
                            fileFormdataTemplate,
                            item.Key, //表单键  
                            item.FileName);
                    }
                    else
                    {
                        //上传文本  
                        formdata = string.Format(
                            dataFormdataTemplate,
                            item.Key,
                            item.Value);
                    }

                    //统一处理  
                    byte[] formdataBytes = null;
                    //第一行不需要换行  
                    if (postStream.Length == 0)
                        formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                    else
                        formdataBytes = Encoding.UTF8.GetBytes(formdata);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);

                    //写入文件内容  
                    if (item.FileData != null && item.FileData.Length > 0)
                    {
                        postStream.Write(item.FileData, 0, item.FileData.Length);
                    }
                }
                //结尾  
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }

            try
            {
                request.ContentLength = postStream.Length;
                if (postStream != null)
                {
                    postStream.Position = 0;
                    //直接写入流  
                    Stream requestStream = request.GetRequestStream();

                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    postStream.Close();//关闭文件访问  
                }


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (cookieContainer != null)
                {
                    response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, responseEncoding ?? Encoding.UTF8))
                    {
                        string retString = myStreamReader.ReadToEnd();
                        return retString;
                    }
                }
            }
            catch (WebException wex)
            {
                if (wex.Response == null)
                {
                    if (LogEnabled)
                    {
                        var msg = $"Exception. Url: {url} exceptionDetails: {Environment.NewLine + Logger.GetExceptionDetails(wex)}";
                        Logger.Log(msg);
                    }
                }
                else
                {
                    var contentTypeResponse = wex.Response.ContentType;

                    var responseEncoding2 = responseEncoding;
                    if (responseEncoding2 == null)
                    {
                        var encodingResponseIndex = contentTypeResponse.IndexOf("charset=", StringComparison.OrdinalIgnoreCase);
                        var encodingResponse = "utf-8";
                        if (encodingResponseIndex > -1)
                        {
                            encodingResponse = contentTypeResponse.Substring(encodingResponseIndex + "charset=".Length);
                        }
                        responseEncoding2 = Encoding.GetEncoding(encodingResponse);
                    }

                    var response = "";

                    using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream(), responseEncoding2))
                    {
                        response = reader.ReadToEnd();
                    }

                    if (LogEnabled)
                    {
                        Logger.Log($"Exception. Url: {url} response: {Environment.NewLine + response}");
                    }
                }

                throw new Exception("网络开小差了，请稍后再试试吧");
            }
            catch (Exception ex)
            {
                if (LogEnabled)
                {
                    Logger.LogException(ex);
                }
                throw new Exception("网络开小差了，请稍后再试试吧");
            }

        }


        public static string Request(string method, string url, string param, Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", Encoding requestEncoding = null, Encoding responseEncoding = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var result = "";

            if (requestEncoding == null)
            {
                requestEncoding = Encoding.UTF8;
            }

            SetHttpRequestHeaders(request, headers);

            byte[] bs = requestEncoding.GetBytes(param);
            request.Method = method;

            request.ContentType = contentType;
            request.ContentLength = bs.Length;
            request.Timeout = 10 * 60 * 1000;

            try
            {
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }

                var response = request.GetResponse();
                var contentTypeResponse = response.ContentType;

                var responseEncoding2 = responseEncoding;
                if (responseEncoding2 == null)
                {
                    var encodingResponseIndex = contentTypeResponse.IndexOf("charset=", StringComparison.OrdinalIgnoreCase);
                    var encodingResponse = "utf-8";
                    if (encodingResponseIndex > -1)
                    {
                        encodingResponse = contentTypeResponse.Substring(encodingResponseIndex + "charset=".Length);
                    }
                    responseEncoding2 = Encoding.GetEncoding(encodingResponse);
                }

                using (StreamReader reader = new StreamReader(response.GetResponseStream(), responseEncoding2))
                {
                    result = reader.ReadToEnd();
                    if (LogEnabled)
                    {
                        var log = $"url: {url}{Environment.NewLine}param: {param}{Environment.NewLine}response: {result}";
                        Logger.Log(log);
                    }
                }
            }
            catch (WebException wex)
            {
                if (wex.Response == null)
                {
                    if (LogEnabled)
                    {
                        var msg = $"Exception. Url: {url} param: {param} exceptionDetails: {Environment.NewLine + Logger.GetExceptionDetails(wex)}";
                        Logger.Log(msg);
                    }
                }
                else
                {
                    var contentTypeResponse = wex.Response.ContentType;

                    var responseEncoding2 = responseEncoding;
                    if (responseEncoding2 == null)
                    {
                        var encodingResponseIndex = contentTypeResponse.IndexOf("charset=", StringComparison.OrdinalIgnoreCase);
                        var encodingResponse = "utf-8";
                        if (encodingResponseIndex > -1)
                        {
                            encodingResponse = contentTypeResponse.Substring(encodingResponseIndex + "charset=".Length);
                        }
                        responseEncoding2 = Encoding.GetEncoding(encodingResponse);
                    }

                    var response = "";

                    using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream(), responseEncoding2))
                    {
                        response = reader.ReadToEnd();
                    }

                    if (LogEnabled)
                    {
                        Logger.Log($"Exception. Url: {url} param: {param} response: {Environment.NewLine + response}");
                    }
                }

                throw new Exception("网络开小差了，请稍后再试试吧");
            }
            catch (Exception ex)
            {
                if (LogEnabled)
                {
                    Logger.LogException(ex);
                }
                throw new Exception("网络开小差了，请稍后再试试吧");
            }

            return result;
        }

        public static string GetParam(object model)
        {
            return GetParam(model, false, true, null);
        }

        public static string GetParam(object model, Encoding encoding)
        {
            return GetParam(model, false, true, encoding);
        }

        public static string GetParam(object model, bool isParamLowercase)
        {
            return GetParam(model, isParamLowercase, true, null);
        }

        public static string GetParam(object model, bool isParamLowercase, bool useUrlEncode)
        {
            return GetParam(model, isParamLowercase, useUrlEncode, null);
        }

        public static string GetParam(object model, bool isParamLowercase, bool useUrlEncode, Encoding encoding)
        {
            List<String> list = new List<String>();
            var list2 = new List<string>();

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            if (model is JObject jObject)
            {
                foreach (var item in jObject)
                {
                    var value = item.Value.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        var name = isParamLowercase ? item.Key.ToLower() : item.Key;
                        if (useUrlEncode)
                        {
                            list.Add($"{HttpUtility.UrlEncode(name, encoding)}={HttpUtility.UrlEncode(value, encoding)}");
                        }
                        else
                        {
                            list.Add($"{name}={value}");
                        }
                        list2.Add($"{name}:{value}");
                    }
                }
            }
            else
            {
                PropertyInfo[] propertyInfo = model.GetType().GetProperties();

                foreach (PropertyInfo info in propertyInfo)
                {
                    var value = info.GetValue(model)?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        var name = isParamLowercase ? info.Name.ToLower() : info.Name;
                        if (useUrlEncode)
                        {
                            list.Add($"{HttpUtility.UrlEncode(name, encoding)}={HttpUtility.UrlEncode(value, encoding)}");
                        }
                        else
                        {
                            list.Add($"{name}={value}");
                        }
                        list2.Add($"{name}:{value}");
                    }
                }
            }

            var debugInfo = string.Join("\r\n", list2);
            if (LogEnabled)
            {
                Logger.Log(debugInfo);
            }

            return string.Join("&", list);
        }

        static void SetHttpRequestHeaders(HttpWebRequest request, Dictionary<string, string> headers)
        {
            if (headers == null)
            {
                return;
            }

            foreach (var item in headers)
            {
                if (item.Key == "User-Agent")
                {
                    request.UserAgent = item.Value;
                }
                else
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
        }
    }
}
