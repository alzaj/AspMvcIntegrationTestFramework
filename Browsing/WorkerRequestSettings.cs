using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace MvcIntegrationTestFramework.Browsing
{
    public class WorkerRequestSettings
    {
        public WorkerRequestSettings(string urlRelativeToAppRoot, httpRequestMethods method)
        {
            if (urlRelativeToAppRoot == null) throw new ArgumentNullException("url");


            // Fix up URLs that incorrectly start with / or ~/
            if (urlRelativeToAppRoot.StartsWith("~/"))
                urlRelativeToAppRoot = urlRelativeToAppRoot.Substring(2);
            else if (urlRelativeToAppRoot.StartsWith("/"))
                urlRelativeToAppRoot = urlRelativeToAppRoot.Substring(1);

            // Parse out the querystring if provided
            string query = "";
            int querySeparatorIndex = urlRelativeToAppRoot.IndexOf("?");
            if (querySeparatorIndex >= 0)
            {
                query = urlRelativeToAppRoot.Substring(querySeparatorIndex + 1);
                urlRelativeToAppRoot = urlRelativeToAppRoot.Substring(0, querySeparatorIndex);
            }

            this._url = urlRelativeToAppRoot;
            this._queryString = query;
            this._httpMethodName = Enum.GetName(typeof(httpRequestMethods), method);
        }

        private string _url;
        public string Url
        {
            get { return _url; }
        }

        private string _queryString;
        public string queryString
        {
            get { return _queryString; }
        }

        private string _httpMethodName;
        public string httpMethodName
        {
            get { return _httpMethodName; }
        }

        private string _clientIpAddress;
        public string clientIpAddress
        {
            get { return _clientIpAddress; }
            set { _clientIpAddress = value; }
        }

        private HttpCookieCollection _cookies = new HttpCookieCollection();
        /// <summary>
        /// F.e. : cookies.Add = new HttpCookie("lastVisit", DateTime.Now.ToString());
        /// </summary>
        public HttpCookieCollection cookies {
            get { return _cookies; }
            set { _cookies = value; }
        }

        private NameValueCollection _formValues = new NameValueCollection();
        /// <summary>
        /// F.e.: formValues = new NameValueCollection {{"someFormField", "theValueOfThisField"}, {"currencyTextBox", "EUR"}};
        /// </summary>
        public NameValueCollection formValues
        {
            get { return _formValues; }
            set { _formValues = value; }
        }

        private NameValueCollection _headers = new NameValueCollection();
        /// <summary>
        /// F.e.: headers = new NameValueCollection {{"someHeaderName", "theValueOfThisHeader"}, {"X-Powered-By", "ASP.NET"}};
        /// </summary>
        public NameValueCollection headers
        {
            get { return _headers; }
            set { _headers = value; }
        }

        public enum httpRequestMethods
        {
            GET,
            POST
        };
    }
}
