using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface ILensSubscriptionViewService {
		IEnumerable<SubscriptionListItemView> GetSubscriptions(PageOfSubscriptionsMatchingCriteria criteria);
	}
}