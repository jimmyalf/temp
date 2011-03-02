using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.SendConsents
{
	public class Task : TaskBase
	{
		private readonly IBGWebService _bgWebService;

		public Task(IBGWebService bgWebService, ILoggingService loggingService, ITaskRepositoryResolver taskRepositoryResolver)
			: base("SendConsentsTask", loggingService, taskRepositoryResolver)
		{
			_bgWebService = bgWebService;
		}

		public override void Execute()
		{
			RunLoggedTask(repositoryResolver =>
			{
				var subscriptionRepository = repositoryResolver.GetRepository<ISubscriptionRepository>();
				var subscriptions = subscriptionRepository.FindBy(new AllSubscriptionsToSendConsentsForCriteria()) ?? Enumerable.Empty<Subscription>();
				LogDebug("Fetched {0} subscriptions to send consents for", subscriptions.Count());
				subscriptions.Each(subscription =>
				{					
					var consent = ConvertSubscription(subscription);
					_bgWebService.SendConsent(consent);
					UpdateSubscriptionStatus(subscription, subscriptionRepository);
				});
			});
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
				PayerNumber = subscription.Id
			};
		}
	}
}