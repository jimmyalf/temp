using System;
using System.Collections.Specialized;
using System.Web;
using Moq;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.MockHelpers
{
	public class HttpContextMock : Mock<HttpContextBase>
	{
		private readonly string domainAddress = "http://www.test.se/";
		public HttpContextMock()
		{
			MockedHttpResponse = new Mock<HttpResponseBase>();
			MockedHttpRequest = new Mock<HttpRequestBase>();
			MockedHttpSessionState = new Mock<HttpSessionStateBase>();
			MockedHttpRequest.SetupGet(x => x.Params).Returns(new NameValueCollection());
			MockedHttpRequest.SetupGet(x => x.Url).Returns(new Uri(domainAddress));
			SetupGet(x => x.Response).Returns(MockedHttpResponse.Object);
			SetupGet(x => x.Request).Returns(MockedHttpRequest.Object);
			SetupGet(x => x.Session).Returns(MockedHttpSessionState.Object);
		}

		public Mock<HttpResponseBase> MockedHttpResponse { get; set; }
		public Mock<HttpRequestBase> MockedHttpRequest { get; set; }
		public Mock<HttpSessionStateBase> MockedHttpSessionState { get; set; }

		public HttpContextMock SetupCurrentPathAndQuery(string pathAndQueryUrl)
		{
			var fullUrl = domainAddress.AppendUrl(pathAndQueryUrl);
			MockedHttpRequest.SetupGet(x => x.Url).Returns(new Uri(fullUrl));
			return this;
		}
		public HttpContextMock SetupQueryString(NameValueCollection querystringValues)
		{
			MockedHttpRequest.SetupGet(x => x.Params).Returns(querystringValues);
			return this;
		}
		public HttpContextMock SetupSingleQuery(string key, string value)
		{
			return SetupQueryString(new NameValueCollection { { key, value } });
		}

		public HttpContextMock SetupSessionValue(string key, object value)
		{
			MockedHttpSessionState.SetupGet(x => x[key]).Returns(value);
			return this;
		}

		public void VerifyRedirect(string pathAndQuery)
		{
			MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(pathAndQuery))));
		}
		public void VerifyRedirect(string format, params object[] parameters)
		{
			var expectedUrl = string.Format(format, parameters);
			MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(expectedUrl))));
		}
	}
}