using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator
{
	public class TestBgWebServiceClient : IBGWebService
	{
		public void SendConsent(ConsentToSend consent) { throw new NotImplementedException(); }
	}
}