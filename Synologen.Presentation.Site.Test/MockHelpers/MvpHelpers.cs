using System;
using System.Collections.Specialized;
using System.Web;
using Moq;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.MockHelpers
{
	public static class MvpHelpers
	{
		public static Mock<TView> GetMockedView<TView,TModel>() 
			where TView : class, IView<TModel> 
			where TModel : class, new() 
		{
			var mockedView = new Mock<TView>();
			var mockedModel = new Mock<TModel>();
			mockedView.SetupGet(x => x.Model).Returns(mockedModel.Object);
			return mockedView;
		}

		public static Mock<HttpContextBase> GetMockedHttpContext()
		{
			var mockedHttpContext = new Mock<HttpContextBase>();
			var mockedHttpResponse = new Mock<HttpResponseBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection());
			mockedHttpContext.SetupGet(x => x.Response).Returns(mockedHttpResponse.Object);
			mockedHttpContext.SetupGet(x => x.Request.Url).Returns(new Uri("http://www.test.se/test/"));
			return mockedHttpContext;
		}
		public static Mock<HttpContextBase> SetupPathAndQuery(Mock<HttpContextBase> mockedHttpContext, string pathAndQueryUrl)
		{
			mockedHttpContext.SetupGet(x => x.Request.Url).Returns(new Uri("http://www.test.se/test/"));
			return mockedHttpContext;
		}
		public static Mock<HttpContextBase> SetupQueryString(this Mock<HttpContextBase> mockedHttpContext, NameValueCollection querystringValues)
		{
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(querystringValues);
			return mockedHttpContext;
		}
		public static Mock<HttpContextBase> SetupSingleQuery(this Mock<HttpContextBase> mockedHttpContext, string key, string value)
		{
			return SetupQueryString(mockedHttpContext, new NameValueCollection { { key, value } });
		}
	}
}