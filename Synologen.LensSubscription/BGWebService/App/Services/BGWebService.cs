using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGWebService.App.Services
{
	public class BGWebService : IBGWebService
	{
		private readonly IAutogiroPayerRepository _autogiroPayerRepository;
		private readonly IBGConsentToSendRepository _consentToSendRepository;
		private readonly IBGPaymentToSendRepository _bgPaymentToSendRepository;
		private readonly IBGReceivedPaymentRepository _bgReceivedPaymentRepository;
		private readonly IBGReceivedErrorRepository _bgReceivedErrorRepository;
		private readonly IBGReceivedConsentRepository _bgReceivedConsentRepository;
		private readonly IBGWebServiceDTOParser _bgWebServiceDtoParser;

		public BGWebService(IAutogiroPayerRepository autogiroPayerRepository, 
			IBGConsentToSendRepository consentToSendRepository, 
			IBGPaymentToSendRepository bgPaymentToSendRepository,
			IBGReceivedPaymentRepository bgReceivedPaymentRepository,
			IBGReceivedErrorRepository bgReceivedErrorRepository,
			IBGReceivedConsentRepository bgReceivedConsentRepository,
			IBGWebServiceDTOParser bgWebServiceDtoParser)
		{
			_autogiroPayerRepository = autogiroPayerRepository;
			_consentToSendRepository = consentToSendRepository;
			_bgPaymentToSendRepository = bgPaymentToSendRepository;
			_bgReceivedPaymentRepository = bgReceivedPaymentRepository;
			_bgReceivedErrorRepository = bgReceivedErrorRepository;
			_bgReceivedConsentRepository = bgReceivedConsentRepository;
			_bgWebServiceDtoParser = bgWebServiceDtoParser;
		}

		public bool TestConnection()
		{
			return true;
		}
		public int RegisterPayer(string name, AutogiroServiceType serviceType)
		{
			var payer = new AutogiroPayer { Name = name, ServiceType = serviceType };
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

		public ReceivedConsent[] GetConsents(AutogiroServiceType serviceType)
		{
			var consents = _bgReceivedConsentRepository.FindBy(new AllNewReceivedBGConsentsMatchingServiceTypeCriteria(serviceType));
			return consents.IsNullOrEmpty() 
				? new ReceivedConsent[]{ } 
				: consents.Select(payment => _bgWebServiceDtoParser.ParseConsent(payment)).ToArray();
		}

		public ReceivedPayment[] GetPayments(AutogiroServiceType serviceType)
		{
			var payments = _bgReceivedPaymentRepository.FindBy(new AllNewReceivedBGPaymentsMatchingServiceTypeCriteria(serviceType));
			return payments.IsNullOrEmpty() 
				? new ReceivedPayment[]{ } 
				: payments.Select(payment => _bgWebServiceDtoParser.ParsePayment(payment)).ToArray();
		}

		public RecievedError[] GetErrors(AutogiroServiceType serviceType)
		{
			var errors = _bgReceivedErrorRepository.FindBy(new AllNewReceivedBGErrorsMatchingServiceTypeCriteria(serviceType));
			return errors.IsNullOrEmpty()
				? new RecievedError[] { }
				: errors.Select(error => _bgWebServiceDtoParser.ParseError(error)).ToArray();
		}

		public void SetConsentHandled(ReceivedConsent consentToUpdate)
		{
		    var consent = _bgReceivedConsentRepository.Get(consentToUpdate.ConsentId);
            if (consent == null) throw new ArgumentException(string.Format("Consent with id {0} could not be found", consentToUpdate.ConsentId));
            consent.SetHandled();
            _bgReceivedConsentRepository.Save(consent);
		}

		public void SetPaymentHandled(ReceivedPayment paymentToUpdate)
		{
			var payment = _bgReceivedPaymentRepository.Get(paymentToUpdate.PaymentId);
			if (payment == null) throw new ArgumentException(string.Format("Payment with id {0} could not be found", paymentToUpdate.PaymentId), "paymentToUpdate");
			payment.SetHandled();
			_bgReceivedPaymentRepository.Save(payment);
		}

		public void SetErrorHandled(RecievedError errorToUpdate)
		{
			var error = _bgReceivedErrorRepository.Get(errorToUpdate.ErrorId);
			if (error == null) throw new ArgumentException(string.Format("Error with id {0} could not be found", errorToUpdate.ErrorId), "errorToUpdate");
			error.SetHandled();
			_bgReceivedErrorRepository.Save(error);
		}
	}


}