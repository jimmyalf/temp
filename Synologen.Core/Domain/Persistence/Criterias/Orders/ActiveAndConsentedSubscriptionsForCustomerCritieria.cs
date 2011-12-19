using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class ActiveAndConsentedSubscriptionsForCustomerCritieria : IActionCriteria
	{
		public int CustomerId { get; set; }

		public ActiveAndConsentedSubscriptionsForCustomerCritieria(int customerId)
		{
			CustomerId = customerId;
		}
	}
}