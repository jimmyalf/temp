using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.Tasks
{
	public class ReceiveConsentsTask : TaskBase
	{
		private readonly IBGWebService _bgWebService;
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly ISubscriptionErrorRepository _subscriptionErrorRepository;

		public ReceiveConsentsTask(IBGWebService bgWebService, ISubscriptionRepository subscriptionRepository, ISubscriptionErrorRepository subscriptionErrorRepository, ILoggingService loggingService)
			: base("ReceiveConsentsTask", loggingService)
		{
			_bgWebService = bgWebService;
			_subscriptionRepository = subscriptionRepository;
			_subscriptionErrorRepository = subscriptionErrorRepository;
		}

		public override void Execute()
		{
			RunLoggedTask(() =>
          	{
          		RecievedConsent[] consents = _bgWebService.GetConsents();
          		LogDebug("Fetched {0} consent replies from bgc server", consents.Length);

          		foreach (var consent in consents)
          		{
          			var subscription = _subscriptionRepository.Get(consent.SubscriptionId);
          			SubscriptionErrorType errorTypeCode = SubscriptionErrorType.Unknown;

          			bool isConsented = GetSubscriptionErrorType(consent.CommentCode, ref errorTypeCode);

          			if (isConsented)
          			{
          				UpdateSubscription(subscription, consent, true);
          				LogDebug("Consent for subscription with id \"{0}\" was accepted.", subscription.Id);
          			}
          			else
          			{
          				SaveSubscriptionError(subscription, errorTypeCode, consent);
          				UpdateSubscription(subscription, consent, false);
          				LogDebug("Consent for subscription with id \"{0}\" was denied.", subscription.Id);
          			}
					_bgWebService.SetConsentHandled(consent.ConsentId);
          		}
          	});
		}

		private void SaveSubscriptionError(Subscription subscription, SubscriptionErrorType errorTypeCode, RecievedConsent consent)
		{
			var subscriptionError = new SubscriptionError
					                        	{
					                        		Subscription = subscription,
					                        		Type = errorTypeCode,
					                        		Code = consent.InformationCode,
					                        		CreatedDate = DateTime.Now
					                        	};
			_subscriptionErrorRepository.Save(subscriptionError);
		}

		private void UpdateSubscription(Subscription subscription, RecievedConsent consent, bool isAccepted)
		{
			subscription.ConsentStatus = isAccepted ? SubscriptionConsentStatus.Accepted : SubscriptionConsentStatus.Denied;
			if (isAccepted)
				subscription.ActivatedDate = consent.ConsentValidForDate.Value;
			_subscriptionRepository.Save(subscription);
		}

		private static bool GetSubscriptionErrorType(ConsentCommentCode commentCode, ref SubscriptionErrorType errorTypeCode)
		{
			bool isConsented;
			switch (commentCode)
			{
				case ConsentCommentCode.NewConsent:
					isConsented = true;
					break;
				case ConsentCommentCode.ConsentTurnedDownByBank:
					errorTypeCode = SubscriptionErrorType.ConsentTurnedDownByBank;
					isConsented = false;
					break;
				case ConsentCommentCode.ConsentTurnedDownByPayer:
					errorTypeCode = SubscriptionErrorType.ConsentTurnedDownByPayer;
					isConsented = false;
					break;
				case ConsentCommentCode.AccountTypeNotApproved:
					errorTypeCode = SubscriptionErrorType.AccountTypeNotApproved;
					isConsented = false;
					break;
				case ConsentCommentCode.ConsentMissingInBankgiroConsentRegister:
					isConsented = false;
					errorTypeCode = SubscriptionErrorType.ConsentMissingInBankgiroConsentRegister;
					break;
				case ConsentCommentCode.IncorrectAccountOrPersonalIdNumber:
					errorTypeCode = SubscriptionErrorType.IncorrectAccountOrPersonalIdNumber;
					isConsented = false;
					break;
				case ConsentCommentCode.ConsentCanceledByBankgiro:
					errorTypeCode = SubscriptionErrorType.ConsentCanceledByBankgiro;
					isConsented = false;
					break;
				case ConsentCommentCode.ConsentCanceledByBankgiroBecauseOfMissingStatement:
					errorTypeCode = SubscriptionErrorType.ConsentCanceledByBankgiroBecauseOfMissingStatement;
					isConsented = false;
					break;
				case ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration:
					errorTypeCode = SubscriptionErrorType.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation;
					isConsented = false;
					break;
				case ConsentCommentCode.ConsentTemporarilyStoppedByPayer:
					errorTypeCode = SubscriptionErrorType.ConsentTemporarilyStoppedByPayer;
					isConsented = false;
					break;
				case ConsentCommentCode.TemporaryConsentStopRevoked:
					errorTypeCode = SubscriptionErrorType.TemporaryConsentStopRevoked;
					isConsented = false;
					break;
				case ConsentCommentCode.IncorrectPersonalIdNumber:
					errorTypeCode = SubscriptionErrorType.IncorrectPersonalIdNumber;
					isConsented = false;
					break;
				case ConsentCommentCode.IncorrectPayerNumber:
					errorTypeCode = SubscriptionErrorType.IncorrectPayerNumber;
					isConsented = false;
					break;
				case ConsentCommentCode.IncorrectAccountNumber:
					errorTypeCode = SubscriptionErrorType.IncorrectAccountNumber;
					isConsented = false;
					break;
				case ConsentCommentCode.MaxAmountNotAllowed:
					errorTypeCode = SubscriptionErrorType.MaxAmountNotAllowed;
					isConsented = false;
					break;
				case ConsentCommentCode.IncorrectPaymentReceiverBankgiroNumber:
					errorTypeCode = SubscriptionErrorType.IncorrectPaymentReceiverBankgiroNumber;
					isConsented = false;
					break;
				case ConsentCommentCode.PaymentReceiverBankgiroNumberMissing:
					errorTypeCode = SubscriptionErrorType.PaymentReceiverBankgiroNumberMissing;
					isConsented = false;
					break;
				case ConsentCommentCode.Canceled:
					errorTypeCode = SubscriptionErrorType.Canceled;
					isConsented = false;
					break;
				default:
					errorTypeCode = SubscriptionErrorType.Unknown;
					isConsented = false;
					break;
			}
			return isConsented;
		}
	}
}
