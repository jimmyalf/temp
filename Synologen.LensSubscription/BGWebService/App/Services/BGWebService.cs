using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.AutogiroServiceType;

namespace Synologen.LensSubscription.BGWebService.App.Services
{
	public class BGWebService : IBGWebService
	{
		private readonly IAutogiroPayerRepository _autogiroPayerRepository;
		private readonly IBGWebServiceDTOParser _bgWebServiceDtoParser;

		public BGWebService(IAutogiroPayerRepository autogiroPayerRepository, IBGWebServiceDTOParser bgWebServiceDtoParser)
		{
			_autogiroPayerRepository = autogiroPayerRepository;
			_bgWebServiceDtoParser = bgWebServiceDtoParser;
		}

		public bool TestConnection()
		{
			return true;
		}
		public int RegisterPayer(string name, AutogiroServiceType serviceType)
		{
			var payer = _bgWebServiceDtoParser.GetAutogiroPayer(name, serviceType);
			return _autogiroPayerRepository.Save(payer);
		}
		public void SendConsent(ConsentToSend consent) { throw new NotImplementedException(); }
		public void SendPayment(PaymentToSend payment) { throw new NotImplementedException(); }
		public ReceivedConsent[] GetConsents(AutogiroServiceType serviceType) { throw new NotImplementedException(); }
		public ReceivedPayment[] GetPayments(AutogiroServiceType serviceType) { throw new NotImplementedException(); }
		public RecievedError[] GetErrors(AutogiroServiceType serviceType) { throw new NotImplementedException(); }
		public void SetConsentHandled(ReceivedConsent consent) { throw new NotImplementedException(); }
		public void SetPaymentHandled(ReceivedPayment payment) { throw new NotImplementedException(); }
		public void SetErrorHandled(RecievedError error) { throw new NotImplementedException(); }
	}


}