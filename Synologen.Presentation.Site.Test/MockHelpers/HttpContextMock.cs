using System;
using System.Collections.Specialized;
using System.Web;
using Moq;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.MockHelpers
{
	public class HttpContextMock : Mock<HttpContextBase>
	{
		public HttpContextMock()
		{
			MockedHttpResponse = new Mock<HttpResponseBase>();
			MockedHttpRequest = new Mock<HttpRequestBase>();
			MockedHttpRequest.SetupGet(x => x.Params).Returns(new NameValueCollection());
			MockedHttpRequest.SetupGet(x => x.Url).Returns(new Uri("http://www.test.se/test/"));
			SetupGet(x => x.Response).Returns(MockedHttpResponse.Object);
			SetupGet(x => x.Request).Returns(MockedHttpRequest.Object);
		}

		public Mock<HttpResponseBase> MockedHttpResponse { get; set; }
		public Mock<HttpRequestBase> MockedHttpRequest { get; set; }

		public HttpContextMock SetupRelativePathAndQuery(string pathAndQueryUrl)
		{
			var fullUrl = "http://www.test.se".AppendUrl(pathAndQueryUrl);
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
	}
}