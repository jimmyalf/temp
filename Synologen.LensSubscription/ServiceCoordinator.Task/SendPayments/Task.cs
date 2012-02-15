using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.SendPayments
{
	public class Task : TaskBaseWithWebService
	{
		private readonly IAutogiroPaymentService _autogiroPaymentService;

		public Task(ILoggingService loggingService, IBGWebServiceClient bgWebServiceClient, IAutogiroPaymentService autogiroPaymentService) 
			: base("SendPaymentsTask", loggingService, bgWebServiceClient)
		{
			_autogiroPaymentService = autogiroPaymentService;
		}

		public override void Execute(ExecutingTaskContext context)
		{
			RunLoggedTask(() =>
			{
				var subscriptionRepository = context.Resolve<ISubscriptionRepository>();
				var subscriptionPendingPaymentRepository = context.Resolve<ISubscriptionPendingPaymentRepository>();
				var subscriptions = subscriptionRepository.FindBy(new AllSubscriptionsToSendPaymentsForCriteria()) ?? Enumerable.Empty<Subscription>();
				LogDebug("Fetched {0} subscriptions to send payments for", subscriptions.Count());
				foreach (var subscription in subscriptions)
				{
					if (!subscription.AutogiroPayerId.HasValue) throw new ApplicationException(string.Format("Subscription {0} was missing autogiro payer id.", subscription.Id));
					var pendingPayment = CreatePendingPayment(subscriptionPendingPaymentRepository, subscription.SubscriptionItems);
					if (pendingPayment == null) continue;
					var payment = ConvertSubscription(subscription.AutogiroPayerId.Value, pendingPayment);
					BGWebServiceClient.SendPayment(payment);
					UpdateSubscriptionPaymentDate(subscription, subscriptionRepository);
				}
			});
		}

		private SubscriptionPendingPayment CreatePendingPayment(ISubscriptionPendingPaymentRepository subscriptionPendingPaymentRepository, IEnumerable<SubscriptionItem> subscriptionItems)
		{
			var activeSubscriptionItems = subscriptionItems.Where(x => x.IsActive).ToList();
			if (!activeSubscriptionItems.Any()) return null;
			var payment = new SubscriptionPendingPayment
			{
				Amount = activeSubscriptionItems.Sum(x => x.AmountForAutogiroWithdrawal),
				SubscriptionItems = activeSubscriptionItems
			};
			subscriptionPendingPaymentRepository.Save(payment);
			return payment;
		}

		private void UpdateSubscriptionPaymentDate(Subscription subscription, ISubscriptionRepository subscriptionRepository)
		{
			subscription.LastPaymentSent = _autogiroPaymentService.GetPaymentDate();
			subscriptionRepository.Save(subscription);
			LogDebug("Payment for subscription with id \"{0}\" has been sent.", subscription.Id);
		}

		private PaymentToSend ConvertSubscription(int autogiroPayerId, SubscriptionPendingPayment pendingPayment)
		{
			var payment = new PaymentToSend
			{
				Amount = pendingPayment.Amount,
				Reference = pendingPayment.Id.ToString(),
				Type = PaymentType.Debit,
				PayerNumber = autogiroPayerId,
				PaymentDate = _autogiroPaymentService.GetPaymentDate(),
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate
			};
			return payment;
		}
	}
}