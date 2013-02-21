using System;
using System.Net;
using System.IO;
using System.Text;
using System.Resources;

namespace GrnLiteAutoLogin
{
    internal class HttpAccesser
    {
        #region 构造函数
        internal HttpAccesser()
        {
        }
        #endregion

        #region 枚举
        internal enum ACCESS_METHOD
        {
            GET,
            POST
        }
        #endregion

        #region members
        private ACCESS_METHOD accessMethod;
        private string accessUrl;
        private string contentType;
        private string urlParam = string.Empty;
        private string responseText = string.Empty;
        private bool isUseCookie;
        private string reqEncoding = GrnLiteAutoLogin.Properties.Resources.UTF8;
        private string resEncoding = GrnLiteAutoLogin.Properties.Resources.UTF8;

        #endregion

        #region Propertis

        /// <summary>
        /// access method
        /// get or post
        /// default is "GET"
        /// </summary>
        internal ACCESS_METHOD AccessMethod
        {
            get
            {
                return accessMethod;
            }
            set
            {
                accessMethod = value;
            }
        }

        /// <summary>
        /// accessUrl
        /// </summary>
        internal string AccessUrl
        {
            get
            {
                return accessUrl;
            }
            set
            {
                accessUrl = value;
            }
        }

        /// <summary>
        /// urlParmam
        /// </summary>
        internal string UrlParam
        {
            get
            {
                return urlParam;
            }
            set
            {
                urlParam = value;
            }
        }

        /// <summary>
        /// contentType
        /// like:application/x-www-form-urlencoded or text/xml ect.
        /// </summary>
        internal string ContentType
        {
            get
            {
                return contentType;
            }
            set
            {
                contentType = value;
            }
        }

        /// <summary>
        /// response content
        /// </summary>
        internal string ResponseText
        {
            get
            {
                return responseText;
            }
        }

        /// <summary>
        /// set or get is using cookie
        /// </summary>
        internal bool IsUseCookie
        {
            get
            {
                return isUseCookie;
            }
            set
            {
                isUseCookie = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal string ReqEncoding
        {
            get
            {
                return reqEncoding;
            }
            set
            {
                reqEncoding = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal string ResEncoding
        {
            get
            {
                return resEncoding;
            }
            set
            {
                resEncoding = value;
            }
        }

        #endregion

        #region Method

        private void prepareSomething()
        {

        }

        /// <summary>
        /// 去访问网站
        /// </summary>
        internal void access()
        {
            if (accessUrl == null || accessUrl.Trim().Equals(string.Empty))
            {
                throw new UncompleteSettingException(Properties.Resources.URL_NOT_SET);
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(accessUrl);
            req.ContentType = this.contentType;
            if (isUseCookie)
            {
                req.CookieContainer = new CookieContainer();
            }

            if (accessMethod == ACCESS_METHOD.GET)
            {
                req.Method = "GET";
            }
            else if (accessMethod == ACCESS_METHOD.POST)
            {
                if (req.ContentType == null || req.ContentType.Equals(string.Empty))
                {
                    throw new UncompleteSettingException(Properties.Resources.CONTENT_TYPE_NOT_SET);
                }
                req.Method = "POST";
                byte[] bs = Encoding.GetEncoding(reqEncoding).GetBytes(urlParam);
                req.ContentLength = bs.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }
            }
            using (WebResponse res = req.GetResponse())
            {
                Stream receiveStream = res.GetResponseStream();
                Encoding encode = Encoding.GetEncoding(resEncoding);
                StreamReader sr = new StreamReader(receiveStream, encode);
                char[] readbuffer = new char[256];
                int n = sr.Read(readbuffer, 0, 256);
                while (n > 0)
                {
                    string str = new string(readbuffer, 0, n);
                    responseText += str;
                    n = sr.Read(readbuffer, 0, 256);
                }
            }
        }
        #endregion

        #region inner class
        internal class UncompleteSettingException : Exception
        {
            internal UncompleteSettingException(string message)
                : base(message)
            {
            }

            internal UncompleteSettingException()
            {
            }
        }
        #endregion

    }
}