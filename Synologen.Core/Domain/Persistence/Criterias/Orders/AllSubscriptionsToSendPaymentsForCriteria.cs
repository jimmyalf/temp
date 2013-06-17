using System;
using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class AllSubscriptionsToSendPaymentsForCriteria : IActionCriteria
	{
		public DateTime CutOffDateTime { get; set; }

		public AllSubscriptionsToSendPaymentsForCriteria(DateTime cutOffDateTime)
		{
			CutOffDateTime = cutOffDateTime;
		}
	}
}
