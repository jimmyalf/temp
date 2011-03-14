using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.ReceiveConsents
{
	public class Task : TaskBase
	{
		private readonly IBGWebService _bgWebService;

		public Task(IBGWebService bgWebService, ILoggingService loggingService) : base("ReceiveConsentsTask", loggingService)
		{
			_bgWebService = bgWebService;
		}

		public override void Execute(ExecutingTaskContext context)
		{
			RunLoggedTask(() =>
			{
				var subscriptionRepository = context.Resolve<ISubscriptionRepository>();
				var subscriptionErrorRepository = context.Resolve<ISubscriptionErrorRepository>();
				var consents = _bgWebService.GetConsents(AutogiroServiceType.LensSubscription) ?? Enumerable.Empty<ReceivedConsent>();
				LogDebug("Fetched {0} consent replies from bgc server", consents.Count());

				consents.Each(consent =>
				{
					SaveConsent(consent, subscriptionRepository, subscriptionErrorRepository);
					_bgWebService.SetConsentHandled(consent);
				});
			});
		}

		private void SaveConsent(ReceivedConsent consent, ISubscriptionRepository subscriptionRepository, ISubscriptionErrorRepository subscriptionErrorRepository)
		{
			var subscription = subscriptionRepository.GetByBankgiroPayerId(consent.PayerNumber);
			var errorTypeCode = SubscriptionErrorType.Unknown;

			var isConsented = GetSubscriptionErrorType(consent.CommentCode, ref errorTypeCode);

			if (isConsented)
			{
				UpdateSubscription(subscription, consent, true, subscriptionRepository);
				LogDebug("Consent for subscription with id \"{0}\" was accepted.", subscription.Id);
			}
			else
			{
				SaveSubscriptionError(subscription, errorTypeCode, consent, subscriptionErrorRepository);
				UpdateSubscription(subscription, consent, false, subscriptionRepository);
				LogDebug("Consent for subscription with id \"{0}\" was denied.", subscription.Id);
			}
		}

		private static void SaveSubscriptionError(Subscription subscription, SubscriptionErrorType errorTypeCode, ReceivedConsent consent, ISubscriptionErrorRepository subscriptionErrorRepository)
		{
			var subscriptionError = new SubscriptionError
			{
				Subscription = subscription,
				Type = errorTypeCode,
				Code = consent.InformationCode,
				CreatedDate = DateTime.Now
			};
			subscriptionErrorRepository.Save(subscriptionError);
		}

		//private static ConsentInformationCode? GetSubscriptionErrorInformationCode(Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode? code)
		//{
		//    switch (code)
		//    {
		//        case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.AnswerToNewAccountApplication:
		//            return ConsentInformationCode.AnswerToNewAccountApplication;
		//        case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.InitiatedByPayer:
		//            return ConsentInformationCode.InitiatedByPayer;
		//        case  Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.InitiatedByPayersBank:
		//            return ConsentInformationCode.InitiatedByPayersBank;
		//        case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.InitiatedByPaymentRecipient:
		//            return ConsentInformationCode.InitiatedByPaymentRecipient;
		//        case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.PaymentRecieversBankGiroAccountClosed:
		//            return ConsentInformationCode.PaymentRecieversBankGiroAccountClosed;
		//    }
		//    throw new ArgumentOutOfRangeException("code");
		//}

		private static void UpdateSubscription(Subscription subscription, ReceivedConsent consent, bool isAccepted, ISubscriptionRepository subscriptionRepository)
		{
			subscription.ConsentStatus = isAccepted ? SubscriptionConsentStatus.Accepted : SubscriptionConsentStatus.Denied;
			if (isAccepted)
				subscription.ActivatedDate = consent.ConsentValidForDate.Value;
			subscriptionRepository.Save(subscription);
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