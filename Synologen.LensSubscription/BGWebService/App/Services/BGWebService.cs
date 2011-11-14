using System;
using System.Linq;
using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGWebService.App.Services
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	public class BGWebService : IBGWebService
	{
		private readonly IAutogiroPayerRepository _autogiroPayerRepository;
		private readonly IBGConsentToSendRepository _consentToSendRepository;
		private readonly IBGPaymentToSendRepository _bgPaymentToSendRepository;
		private readonly IBGReceivedPaymentRepository _bgReceivedPaymentRepository;
		private readonly IBGReceivedErrorRepository _bgReceivedErrorRepository;
		private readonly IBGReceivedConsentRepository _bgReceivedConsentRepository;
		private readonly IBGWebServiceDTOParser _bgWebServiceDtoParser;
		private readonly ILoggingService _loggingService;

		public BGWebService(IAutogiroPayerRepository autogiroPayerRepository, 
			IBGConsentToSendRepository consentToSendRepository, 
			IBGPaymentToSendRepository bgPaymentToSendRepository,
			IBGReceivedPaymentRepository bgReceivedPaymentRepository,
			IBGReceivedErrorRepository bgReceivedErrorRepository,
			IBGReceivedConsentRepository bgReceivedConsentRepository,
			IBGWebServiceDTOParser bgWebServiceDtoParser,
			ILoggingService loggingService)
		{
			_autogiroPayerRepository = autogiroPayerRepository;
			_consentToSendRepository = consentToSendRepository;
			_bgPaymentToSendRepository = bgPaymentToSendRepository;
			_bgReceivedPaymentRepository = bgReceivedPaymentRepository;
			_bgReceivedErrorRepository = bgReceivedErrorRepository;
			_bgReceivedConsentRepository = bgReceivedConsentRepository;
			_bgWebServiceDtoParser = bgWebServiceDtoParser;
			_loggingService = loggingService;
		}

		public bool TestConnection()
		{
			_loggingService.LogDebug("TestConnection called");
			return true;
		}
		public int RegisterPayer(string name, AutogiroServiceType serviceType)
		{
			_loggingService.LogDebug("RegisterPayer called for {0}", name);
			var payer = new AutogiroPayer { Name = name, ServiceType = serviceType };
			var payerId =  _autogiroPayerRepository.Save(payer);
			_loggingService.LogInfo("Created Payer with id {0}", payerId);
			return payerId;
		}

		public void SendConsent(ConsentToSend consentToSend)
		{
			_loggingService.LogDebug("SendConsent called for payer {0}", consentToSend.PayerNumber);
			var payer = _autogiroPayerRepository.Get(consentToSend.PayerNumber);
			if (payer == null) throw new ArgumentException("Cannot find Payer matching Consent payer number", "consentToSend");
			var consent = _bgWebServiceDtoParser.ParseConsent(consentToSend, payer);
			_consentToSendRepository.Save(consent);
			_loggingService.LogInfo("Created Consent with id {0}", consent.Id);
		}

		public void SendPayment(PaymentToSend paymentToSend)
		{
			_loggingService.LogDebug("SendPayment called for payer {0}", paymentToSend.PayerNumber);
			var payer = _autogiroPayerRepository.Get(paymentToSend.PayerNumber);
			if (payer == null) throw new ArgumentException("Cannot find Payer matching Payment payer number", "paymentToSend");
			var payment = _bgWebServiceDtoParser.ParsePayment(paymentToSend, payer);
			_bgPaymentToSendRepository.Save(payment);
			_loggingService.LogInfo("Payment with id {0}", payment.Id);
		}

		public ReceivedConsent[] GetConsents(AutogiroServiceType serviceType)
		{
			_loggingService.LogDebug("GetConsents called");
			var consents = _bgReceivedConsentRepository.FindBy(new AllNewReceivedBGConsentsMatchingServiceTypeCriteria(serviceType));
			return consents.IsNullOrEmpty() 
				? new ReceivedConsent[]{ } 
				: consents.Select(payment => _bgWebServiceDtoParser.ParseConsent(payment)).ToArray();
		}

		public ReceivedPayment[] GetPayments(AutogiroServiceType serviceType)
		{
			_loggingService.LogDebug("GetPayments called");
			var payments = _bgReceivedPaymentRepository.FindBy(new AllNewReceivedBGPaymentsMatchingServiceTypeCriteria(serviceType));
			return payments.IsNullOrEmpty() 
				? new ReceivedPayment[]{ } 
				: payments.Select(payment => _bgWebServiceDtoParser.ParsePayment(payment)).ToArray();
		}

		public RecievedError[] GetErrors(AutogiroServiceType serviceType)
		{
			_loggingService.LogDebug("GetErrors called");
			var errors = _bgReceivedErrorRepository.FindBy(new AllNewReceivedBGErrorsMatchingServiceTypeCriteria(serviceType));
			return errors.IsNullOrEmpty()
				? new RecievedError[] { }
				: errors.Select(error => _bgWebServiceDtoParser.ParseError(error)).ToArray();
		}

		public void SetConsentHandled(ReceivedConsent consentToUpdate)
		{
			_loggingService.LogDebug("SetConsentHandled called for consent {0}", consentToUpdate.ConsentId);
			var consent = _bgReceivedConsentRepository.Get(consentToUpdate.ConsentId);
			if (consent == null)
			{
				_loggingService.LogError("No consent with id {0} was found", consentToUpdate.ConsentId);
				throw new ArgumentException(string.Format("Consent with id {0} could not be found", consentToUpdate.ConsentId), "consentToUpdate");
			}
			consent.SetHandled();
			_bgReceivedConsentRepository.Save(consent);
			_loggingService.LogInfo("Consent with id {0} was set as handled", consent.Id);
		}

		public void SetPaymentHandled(ReceivedPayment paymentToUpdate)
		{
			_loggingService.LogDebug("SetPaymentHandled called for payment {0}", paymentToUpdate.PaymentId);
			var payment = _bgReceivedPaymentRepository.Get(paymentToUpdate.PaymentId);
			if (payment == null)
			{
				_loggingService.LogError("No payment with id {0} was found", paymentToUpdate.PaymentId);
				throw new ArgumentException(string.Format("Payment with id {0} could not be found", paymentToUpdate.PaymentId), "paymentToUpdate");
			}
			payment.SetHandled();
			_bgReceivedPaymentRepository.Save(payment);
			_loggingService.LogInfo("Payment with id {0} was set as handled", payment.Id);
		}

		public void SetErrorHandled(RecievedError errorToUpdate)
		{
			_loggingService.LogDebug("SetErrorHandled called for error {0}", errorToUpdate.ErrorId);
			var error = _bgReceivedErrorRepository.Get(errorToUpdate.ErrorId);
			if (error == null)
			{
				_loggingService.LogError("No error with id {0} was found", errorToUpdate.ErrorId);
				throw new ArgumentException(string.Format("Error with id {0} could not be found", errorToUpdate.ErrorId), "errorToUpdate");
			}
			error.SetHandled();
			_bgReceivedErrorRepository.Save(error);
			_loggingService.LogInfo("Error with id {0} was set as handled", error.Id);
		}
	}


}