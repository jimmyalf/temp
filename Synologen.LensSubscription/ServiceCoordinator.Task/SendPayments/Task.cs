using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.SendPayments
{
	public class Task : TaskBaseWithWebService
	{
		public Task(ILoggingService loggingService, IBGWebServiceClient bgWebServiceClient) 
			: base("SendPaymentsTask", loggingService, bgWebServiceClient) { }

		public override void Execute(ExecutingTaskContext context)
		{
			RunLoggedTask(() =>
			{
				var subscriptionRepository = context.Resolve<ISubscriptionRepository>();
				var subscriptions = subscriptionRepository.FindBy(new AllSubscriptionsToSendPaymentsForCriteria()) ?? Enumerable.Empty<Subscription>();
				LogDebug("Fetched {0} subscriptions to send payments for", subscriptions.Count());

				subscriptions.Each(subscription => 
				{
					var payment = ConvertSubscription(subscription);
					BGWebServiceClient.SendPayment(payment);
					UpdateSubscriptionPaymentDate(subscription, subscriptionRepository);
				});
			});
		}

		private void UpdateSubscriptionPaymentDate(Subscription subscription, ISubscriptionRepository subscriptionRepository)
		{
			subscription.PaymentInfo.PaymentSentDate = DateTime.Now;
			subscriptionRepository.Save(subscription);
			LogDebug("Payment for subscription with id \"{0}\" has been sent.", subscription.Id);
		}

		private static PaymentToSend ConvertSubscription(Subscription subscription)
		{
			var payment = new PaymentToSend
			{
				Amount = subscription.PaymentInfo.MonthlyAmount,
				Reference = null, //subscription.Customer.PersonalIdNumber,
				Type = PaymentType.Debit,
				PayerNumber = subscription.BankgiroPayerNumber.Value,
				PaymentDate = DateTime.Now, //FIX: PaymentDate
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate
			};
			return payment;
		}
	}
}