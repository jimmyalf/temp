using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.SendConsents
{
	public class Task : TaskBaseWithWebService
	{
		public Task(ILoggingService loggingService, IBGWebServiceClient bgWebServiceClient)
			: base("SendConsentsTask", loggingService, bgWebServiceClient) { }

		public override void Execute(ExecutingTaskContext context)
		{
			RunLoggedTask(() =>
			{
				var subscriptionRepository = context.Resolve<ISubscriptionRepository>();
				var subscriptions = subscriptionRepository.FindBy(new AllSubscriptionsToSendConsentsForCriteria()) ?? Enumerable.Empty<Subscription>();
				LogDebug("Fetched {0} subscriptions to send consents for", subscriptions.Count());
				subscriptions.Each(subscription =>
				{
					if (subscription.BankgiroPayerNumber == null) GetSubscriptionPayer(subscription);
					var consent = ConvertSubscription(subscription);
					BGWebServiceClient.SendConsent(consent);
					UpdateSubscriptionStatus(subscription, subscriptionRepository);
				});
			});
		}

		private void GetSubscriptionPayer(Subscription subscription) 
		{
			var customerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
			var payerNumber = BGWebServiceClient.RegisterPayer(customerName, AutogiroServiceType.LensSubscription);
			subscription.BankgiroPayerNumber = payerNumber;
		}

		protected virtual void UpdateSubscriptionStatus(Subscription subscription, ISubscriptionRepository subscriptionRepository)
		{
			subscription.ConsentStatus = SubscriptionConsentStatus.Sent;
			subscriptionRepository.Save(subscription);
			LogDebug("Consent for subscription with id \"{0}\" has been sent.", subscription.Id);
		}

		protected virtual ConsentToSend ConvertSubscription(Subscription subscription)
		{
			return new ConsentToSend
			{
				BankAccountNumber = subscription.PaymentInfo.AccountNumber,
				ClearingNumber = subscription.PaymentInfo.ClearingNumber,
				PersonalIdNumber = subscription.Customer.PersonalIdNumber,
				PayerNumber = subscription.BankgiroPayerNumber.Value,
                Type = ConsentType.New
			};
		}
	}
}