using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Site.Code.Yammer;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Services
{
    public class YammerService : WebClient, IYammerService
    {
        private const string MessagesPath = "https://www.yammer.com/api/v1/messages.json?limit={0}";
        private const string AuthenticationPath = "https://www.yammer.com/{0}/dialog/authenticate?client_id={1}";

        public CookieContainer CookieContainer { get; set; }

        public YammerService()
        {
            Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.864.0 Safari/535.2");
            CookieContainer = new CookieContainer();
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

        public string GetJson(int limit)
        {
            return DownloadString(String.Format(MessagesPath, limit));
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
    }
}
