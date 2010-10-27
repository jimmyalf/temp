using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class LensSubscriptionViewService : ILensSubscriptionViewService
	{
		private readonly ISubscriptionRepository _subscriptionRepository;
		public LensSubscriptionViewService(ISubscriptionRepository subscriptionRepository) { _subscriptionRepository = subscriptionRepository; }

		public IEnumerable<SubscriptionListItemView> GetSubscriptions(PageOfSubscriptionsMatchingCriteria criteria)
		{
			var subscriptions = _subscriptionRepository.FindBy(criteria);
			Func<Subscription, SubscriptionListItemView> converter = subscription => new SubscriptionListItemView
			{
				CustomerName = subscription.With(x => x.Customer).ParseName(x => x.FirstName, x => x.LastName),
				ShopName = subscription.With(x => x.Customer).With(x => x.Shop).Return(x => x.Name, String.Empty),
                Status = subscription.Status.GetEnumDisplayName(),
                SubscriptionId = subscription.Id
			};
			return (subscriptions == null)? new SubscriptionListItemView[]{} : subscriptions.ConvertSortedPagedList(converter);
		}
	}
}