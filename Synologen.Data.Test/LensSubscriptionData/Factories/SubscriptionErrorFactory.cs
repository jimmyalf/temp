using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories
{
	public class SubscriptionErrorFactory
	{
		public static SubscriptionError[] GetList(Subscription subscription)
		{
			return new[]
			{
				new SubscriptionError
				{
					Type = SubscriptionErrorType.NoAccount,
					Code = ConsentInformationCode.AnswerToNewAccountApplication,
					CreatedDate = new DateTime(2010, 11, 1),
					HandledDate = new DateTime(2010, 11, 2),
					IsHandled = true,
					Subscription = subscription,
					BGConsentId = 5,
					BGErrorId = 6,
					BGPaymentId = 7
				},
				new SubscriptionError
				{
					Type = SubscriptionErrorType.NoCoverage,
					Code = ConsentInformationCode.InitiatedByPayer,
					CreatedDate = new DateTime(2010, 11, 2),
					IsHandled = false,
					Subscription = subscription,
					BGConsentId = 5,
					BGErrorId = 6,
					BGPaymentId = 7
				},
				new SubscriptionError
				{
					Type = SubscriptionErrorType.NotApproved,
					Code = ConsentInformationCode.InitiatedByPayersBank,
					CreatedDate = new DateTime(2010, 11, 3),
					HandledDate = new DateTime(2010, 11, 3),
					IsHandled = true,
					Subscription = subscription,
					BGConsentId = 5,
					BGErrorId = 6,
					BGPaymentId = 7
				},
				new SubscriptionError
				{
					Type = SubscriptionErrorType.NotDebitable,
					Code = ConsentInformationCode.InitiatedByPaymentRecipient,
					CreatedDate = new DateTime(2010, 11, 4),
					IsHandled = false,
					Subscription = subscription,
					BGConsentId = 5,
					BGErrorId = 6,
					BGPaymentId = 7
				},
				new SubscriptionError
				{
					Type = SubscriptionErrorType.ConsentMissing,
					Code = ConsentInformationCode.PaymentRecieversBankGiroAccountClosed,
					CreatedDate = new DateTime(2010, 11, 5),
					HandledDate = new DateTime(2010, 11, 5),
					IsHandled = true, 
					Subscription = subscription,
					BGConsentId = 5,
					BGErrorId = 6,
					BGPaymentId = 7
				},
				new SubscriptionError
				{
					Type = SubscriptionErrorType.CosentStopped,
					Code = ConsentInformationCode.AnswerToNewAccountApplication,
					CreatedDate = new DateTime(2010, 11, 6),
					IsHandled = false,
					Subscription = subscription,
					BGConsentId = null,
					BGErrorId = null,
					BGPaymentId = null
				}
			};
		}

		public static SubscriptionError Get(Subscription subscription)
		{
			return new SubscriptionError
			{
				Type = SubscriptionErrorType.NotApproved,
				Code = ConsentInformationCode.InitiatedByPaymentRecipient,
				CreatedDate = new DateTime(2010, 10, 10),
				HandledDate = new DateTime(2010, 11, 10),
				IsHandled = false,
				Subscription = subscription,
                BGConsentId = 5,
                BGErrorId = 6,
                BGPaymentId = 7
			};
		}

		public static SubscriptionError Edit(SubscriptionError subscriptionError)
		{
			subscriptionError.CreatedDate = subscriptionError.CreatedDate.AddDays(1);
			subscriptionError.HandledDate = SetHandledDate(subscriptionError.HandledDate);
			subscriptionError.IsHandled = !subscriptionError.IsHandled;
			subscriptionError.Type = subscriptionError.Type.Next();
			subscriptionError.Code = subscriptionError.Code.Next();
			subscriptionError.BGConsentId = null;
			subscriptionError.BGErrorId = null;
			subscriptionError.BGPaymentId = null;
			return subscriptionError;
		}

		private static DateTime SetHandledDate(DateTime? date)
		{
			if (date.HasValue)
				return date.Value.AddDays(1);
			{
				var myDate = DateTime.Now;
				return new DateTime(myDate.Year, myDate.Month, myDate.Day);
			}
		}
	}
}