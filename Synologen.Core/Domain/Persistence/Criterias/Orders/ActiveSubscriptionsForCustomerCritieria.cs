using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class ActiveSubscriptionsForCustomerCritieria : IActionCriteria
	{
		public int CustomerId { get; set; }

		public ActiveSubscriptionsForCustomerCritieria(int customerId)
		{
			CustomerId = customerId;
		}
	}
}