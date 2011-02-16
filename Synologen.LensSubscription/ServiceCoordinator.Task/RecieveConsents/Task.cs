using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using ConsentInformationCode=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.ConsentInformationCode;
using Enumerable=System.Linq.Enumerable;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.RecieveConsents
{
	public class Task : TaskBase
	{
		private readonly IBGWebService _bgWebService;
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly ISubscriptionErrorRepository _subscriptionErrorRepository;

		public Task(IBGWebService bgWebService, ISubscriptionRepository subscriptionRepository, ISubscriptionErrorRepository subscriptionErrorRepository, ILoggingService loggingService)
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
				var consents = _bgWebService.GetConsents() ?? Enumerable.Empty<RecievedConsent>();
				LogDebug("Fetched {0} consent replies from bgc server", consents.Count());

				consents.Each(consent =>
				{
					SaveConsent(consent);
					_bgWebService.SetConsentHandled(consent.ConsentId);
				});
			});
		}

		private void SaveConsent(RecievedConsent consent)
		{
			var subscription = _subscriptionRepository.Get(consent.PayerNumber);
			var errorTypeCode = SubscriptionErrorType.Unknown;

			var isConsented = GetSubscriptionErrorType(consent.CommentCode, ref errorTypeCode);

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
		}

		private void SaveSubscriptionError(Subscription subscription, SubscriptionErrorType errorTypeCode, RecievedConsent consent)
		{
			var subscriptionError = new SubscriptionError
			{
				Subscription = subscription,
				Type = errorTypeCode,
				Code = GetSubscriptionErrorInformationCode(consent.InformationCode),
				CreatedDate = DateTime.Now
			};
			_subscriptionErrorRepository.Save(subscriptionError);
		}

		private static ConsentInformationCode? GetSubscriptionErrorInformationCode(Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode? code)
		{
			switch (code)
			{
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.AnswerToNewAccountApplication:
					return ConsentInformationCode.AnswerToNewAccountApplication;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.InitiatedByPayer:
					return ConsentInformationCode.InitiatedByPayer;
				case  Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.InitiatedByPayersBank:
					return ConsentInformationCode.InitiatedByPayersBank;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.InitiatedByPaymentRecipient:
					return ConsentInformationCode.InitiatedByPaymentRecipient;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.PaymentRecieversBankGiroAccountClosed:
					return ConsentInformationCode.PaymentRecieversBankGiroAccountClosed;
			}
			throw new ArgumentOutOfRangeException("code");
		}

		private void UpdateSubscription(Subscription subscription, RecievedConsent consent, bool isAccepted)
		{
			subscription.ConsentStatus = isAccepted ? SubscriptionConsentStatus.Accepted : SubscriptionConsentStatus.Denied;
			if (isAccepted)
				subscription.ActivatedDate = consent.ConsentValidForDate.Value;
			_subscriptionRepository.Save(subscription);
		}

		private static bool GetSubscriptionErrorType(ConsentCommentCode commentType, ref SubscriptionErrorType errorTypeCode)
		{
			bool isConsented;
			switch (commentType)
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
					throw new ArgumentOutOfRangeException("errorTypeCode");
			}
			return isConsented;
		}
	}
}