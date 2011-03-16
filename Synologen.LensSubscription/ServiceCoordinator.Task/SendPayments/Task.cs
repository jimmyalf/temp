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
	public class Task : TaskBase
	{
		private readonly IBGWebService _bgWebService;

		public Task(ILoggingService loggingService, IBGWebService bgWebService) : base("SendPaymentsTask", loggingService)
		{
			_bgWebService = bgWebService;
		}

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
					_bgWebService.SendPayment(payment);
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
				Reference = subscription.Customer.PersonalIdNumber,
				Type = PaymentType.Debit,
				PayerNumber = subscription.BankgiroPayerNumber.Value,
                //Todo PaymentDate = ?,
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate
			};
			return payment;
		}
	}
}