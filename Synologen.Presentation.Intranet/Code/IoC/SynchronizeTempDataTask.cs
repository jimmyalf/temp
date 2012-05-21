using System;
using System.Web;
using System.Web.SessionState;
using Spinit.Wpc.Core.UI;
using Spinit.Wpc.Core.UI.Tasks;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code.IoC
{
	public class SynchronizeTempDataTask :  IWpcApplicationEventTask
	{
		private const string ItemKey = "__TempDataProvider";
		public void ApplicationStart(WpcBootstrapperContext context) { }

		public void ApplicationEnd(WpcBootstrapperContext context) { }

		public void ApplicationInit(WpcBootstrapperContext context)
		{
			HttpContext.Current.ApplicationInstance.PostAcquireRequestState += BeginRequest;
			HttpContext.Current.ApplicationInstance.PostRequestHandlerExecute += EndRequest;
		}

		private void BeginRequest(object sender, EventArgs e)
		{
			if(!IsRequestWithSession) return;
		    var tempData = new TempDataProvider();
		    tempData.Read(HttpContext.Current);
		    HttpContext.Current.Items[ItemKey] = tempData;
		}

		private void EndRequest(object sender, EventArgs e)
		{
			if(!IsRequestWithSession) return;
		    var tempData = HttpContext.Current.Items[ItemKey] as TempDataProvider;
		    if(tempData == null) return;
		    tempData.Write(HttpContext.Current);
		}

		private bool IsRequestWithSession
		{
			get { return HttpContext.Current.Handler is IRequiresSessionState && HttpContext.Current.Session != null; }
		}
	}
}