using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

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
					if (subscription.AutogiroPayerId == null)
					{
						subscription.AutogiroPayerId = CreateAutogiroPayerNumber(subscription);
					}
					var consent = ConvertSubscription(subscription);
					BGWebServiceClient.SendConsent(consent);
					UpdateSubscriptionStatus(subscription, subscriptionRepository);
				});
			});
		}

		private int CreateAutogiroPayerNumber(Subscription subscription) 
		{
			var customerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
			var payerNumber = BGWebServiceClient.RegisterPayer(customerName, AutogiroServiceType.SubscriptionVersion2);
			return payerNumber;
		}

		protected virtual void UpdateSubscriptionStatus(Subscription subscription, ISubscriptionRepository subscriptionRepository)
		{
			subscription.ConsentStatus = SubscriptionConsentStatus.Sent;
			subscriptionRepository.Save(subscription);
			LogDebug("Consent for subscription with id \"{0}\" has been sent.", subscription.Id);
		}

		protected virtual ConsentToSend ConvertSubscription(Subscription subscription)
		{
			if(!subscription.AutogiroPayerId.HasValue)
			{
				var errorMessage = string.Format("No autogiro payer id has been set for subscription {0} !", subscription.Id);
				throw new ApplicationException(errorMessage);
			}
			return new ConsentToSend
			{
				BankAccountNumber = subscription.BankAccountNumber,
				ClearingNumber = subscription.ClearingNumber,
				PersonalIdNumber = subscription.Customer.PersonalIdNumber,
				PayerNumber = subscription.AutogiroPayerId.Value,
                Type = ConsentType.New
			};
		}
	}
}