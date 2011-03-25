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
			subscription.PaymentInfo.PaymentSentDate = _autogiroPaymentService.GetPaymentDate();;
			subscriptionRepository.Save(subscription);
			LogDebug("Payment for subscription with id \"{0}\" has been sent.", subscription.Id);
		}

		private PaymentToSend ConvertSubscription(Subscription subscription)
		{
			var payment = new PaymentToSend
			{
				Amount = subscription.PaymentInfo.MonthlyAmount,
				Reference = null, //subscription.Customer.PersonalIdNumber,
				Type = PaymentType.Debit,
				PayerNumber = subscription.BankgiroPayerNumber.Value,
				PaymentDate = _autogiroPaymentService.GetPaymentDate(),
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate
			};
			return payment;
		}
	}
}