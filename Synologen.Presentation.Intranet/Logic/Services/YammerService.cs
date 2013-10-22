using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code.Yammer;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
    public class YammerService : WebClient, IYammerService
    {
        private const string MessagesPath = "https://www.yammer.com/api/v1/messages.json";
        private const string AuthenticationPath = "https://www.yammer.com/{0}/dialog/authenticate?client_id={1}";

        public CookieContainer CookieContainer { get; set; }

        public YammerService()
        {
            Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/13.0.782.220 Safari/535.1");
            CookieContainer = new CookieContainer();
			Encoding = Encoding.UTF8;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = CookieContainer;
            }
            return request;
        }

        public void Authenticate(string network, string clientId, string email, string password)
        {
            var form = DownloadString(String.Format(AuthenticationPath, network, clientId));

            var action = YammerParserService.GetFormAction(form);
            var nameValueCollection = YammerParserService.GetPostCollection(form, email, password);

            var response = PostAuthentication(action, nameValueCollection);
            CookieContainer.Add(response.Cookies);
        }

        public string GetJson(int limit, string threaded, int newerThan, int olderThan)
        {
            var url = GetUrl(limit, threaded, newerThan, olderThan);
            return DownloadString(url);
        }

        public string GetJson(int limit, string threaded, int newerThan)
        {
            var url = GetUrl(limit, threaded, newerThan, 0);
            return DownloadString(url);
        }

        public string FetchImage(string imageUrl, string fileName, string extension)
        {
            extension = extension ?? String.Empty;

            var storagePath = Globals.YammerStoragePath.EndsWith(@"\") ? Globals.YammerStoragePath : Globals.YammerStoragePath + @"\";
            var urlStoragePath = Globals.YammerUrlStoragePath.EndsWith("/") ? Globals.YammerUrlStoragePath : Globals.YammerUrlStoragePath + "/";

            if (!Directory.Exists(storagePath))
            {
                return String.Empty;
            }

            if (File.Exists(storagePath + fileName + extension))
            {
                return urlStoragePath + fileName + extension;
            }

            var attachment = DownloadData(imageUrl);
            var path = storagePath + fileName + extension;
            File.WriteAllBytes(path, attachment);

            return urlStoragePath + Path.GetFileName(path);
        }

        private HttpWebResponse PostAuthentication(string action, NameValueCollection postCollection)
        {
            var encoding = new ASCIIEncoding();
            var postData = GetPostParameters(postCollection);
            var data = encoding.GetBytes(postData);

            var formRequest = (HttpWebRequest)WebRequest.Create(@"https://www.yammer.com" + action);
            formRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.864.0 Safari/535.2";
            formRequest.Method = "POST";
            formRequest.ContentType = "application/x-www-form-urlencoded";
            formRequest.ContentLength = data.Length;
            formRequest.AllowAutoRedirect = true;
            formRequest.CookieContainer = CookieContainer;
            var stream = formRequest.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            return (HttpWebResponse)formRequest.GetResponse();
        }

        private static string GetPostParameters(NameValueCollection collection)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < collection.Count; i++)
            {
                sb.AppendFormat("{0}={1}", collection.Keys[i], HttpUtility.UrlEncode(collection[i]));
                if (i < collection.Count - 1)
                {
                    sb.Append("&");
                }
            }
            return sb.ToString();
        }


        private static string GetUrl(int limit, string threaded, int newerThan, int olderThan)
        {
            var collection = new NameValueCollection();
            if (limit > 0)
            {
                collection.Add("limit", limit.ToString());
            }
            if (!String.IsNullOrEmpty(threaded))
            {
                collection.Add("threaded", threaded);
            }
            if (olderThan > 0)
            {
                collection.Add("older_than", olderThan.ToString());
            }
            if (newerThan > 0)
            {
                collection.Add("newer_than", newerThan.ToString());
            }

            var parameters = GetPostParameters(collection);
            if (parameters.Length > 0)
            {
                return MessagesPath + "?" + parameters;
            }
            return MessagesPath;
        }
    }
}
