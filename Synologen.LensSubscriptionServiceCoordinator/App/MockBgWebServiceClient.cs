using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.App
{
	public class MockBgWebServiceClient : ClientBase<IBGWebService>, IBGWebService
	{
		public void SendConsent(ConsentToSend consent) { return; }
		public RecievedError[] GetNewErrors() { return new RecievedError[]{}; }
	}
}