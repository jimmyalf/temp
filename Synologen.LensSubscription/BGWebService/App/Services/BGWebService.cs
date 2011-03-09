using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Extensions;
using AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.AutogiroServiceType;

namespace Synologen.LensSubscription.BGWebService.App.Services
{
	public class BGWebService : IBGWebService
	{
		private readonly IAutogiroPayerRepository _autogiroPayerRepository;
		private readonly IBGConsentToSendRepository _consentToSendRepository;
		private readonly IBGPaymentToSendRepository _bgPaymentToSendRepository;
		private readonly IBGReceivedPaymentRepository _bgReceivedPaymentRepository;
		private readonly IBGReceivedErrorRepository _bgReceivedErrorRepository;
		private readonly IBGWebServiceDTOParser _bgWebServiceDtoParser;

		public BGWebService(IAutogiroPayerRepository autogiroPayerRepository, 
			IBGConsentToSendRepository consentToSendRepository, 
			IBGPaymentToSendRepository bgPaymentToSendRepository,
			IBGReceivedPaymentRepository bgReceivedPaymentRepository,
			IBGReceivedErrorRepository bgReceivedErrorRepository,
			IBGWebServiceDTOParser bgWebServiceDtoParser)
		{
			_autogiroPayerRepository = autogiroPayerRepository;
			_consentToSendRepository = consentToSendRepository;
			_bgPaymentToSendRepository = bgPaymentToSendRepository;
			_bgReceivedPaymentRepository = bgReceivedPaymentRepository;
			_bgReceivedErrorRepository = bgReceivedErrorRepository;
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
		public void SendConsent(ConsentToSend consentToSend)
		{
			var payer = _autogiroPayerRepository.Get(consentToSend.PayerNumber);
			if (payer == null) throw new ArgumentException("Cannot find Payer matching Consent payer number", "consentToSend");
			var consent = _bgWebServiceDtoParser.ParseConsent(consentToSend, payer);
			_consentToSendRepository.Save(consent);
		}
		public void SendPayment(PaymentToSend paymentToSend)
		{
			var payer = _autogiroPayerRepository.Get(paymentToSend.PayerNumber);
			if (payer == null) throw new ArgumentException("Cannot find Payer matching Payment payer number", "paymentToSend");
			var payment = _bgWebServiceDtoParser.ParsePayment(paymentToSend, payer);
			_bgPaymentToSendRepository.Save(payment);
		}

		public ReceivedConsent[] GetConsents(AutogiroServiceType serviceType) { throw new NotImplementedException(); }

		public ReceivedPayment[] GetPayments(AutogiroServiceType serviceType)
		{
			var internalServiceType = _bgWebServiceDtoParser.ParseServiceType(serviceType);
			var payments = _bgReceivedPaymentRepository.FindBy(new AllNewReceivedBGPaymentsCriteria(internalServiceType));
			return payments.IsNullOrEmpty() 
				? new ReceivedPayment[]{ } 
				: payments.Select(payment => _bgWebServiceDtoParser.ParsePayment(payment)).ToArray();
		}
		public RecievedError[] GetErrors(AutogiroServiceType serviceType)
		{
			var internalServiceType = _bgWebServiceDtoParser.ParseServiceType(serviceType);
			var errors = _bgReceivedErrorRepository.FindBy(new AllNewReceivedBGErrorsCriteria(internalServiceType));
			return errors.IsNullOrEmpty()
				? new RecievedError[] { }
				: errors.Select(error => _bgWebServiceDtoParser.ParseError(error)).ToArray();
		}
		public void SetConsentHandled(ReceivedConsent consent) { throw new NotImplementedException(); }
		public void SetPaymentHandled(ReceivedPayment payment) { throw new NotImplementedException(); }
		public void SetErrorHandled(RecievedError error) { throw new NotImplementedException(); }
	}


}