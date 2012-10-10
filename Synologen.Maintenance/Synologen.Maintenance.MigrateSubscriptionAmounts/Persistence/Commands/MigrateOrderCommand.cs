using System;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Commands
{
	public class MigrateOrderCommand
	{
		private readonly Order _order;

		public MigrateOrderCommand(Order order)
		{
			_order = order;
		}

		public OrderMigratedResult Execute()
		{
			throw new NotImplementedException();
		}
	}
}