﻿using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class TransactionsForSubscriptionMatchingCriteria : IActionCriteria
	{
		public int SubscriptionId { get; set; }
	}
}
