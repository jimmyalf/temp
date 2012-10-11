using System;
using System.Linq;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Commands;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain
{
	public class Migrator
	{
		private bool _transactionsAreMigrated;
		public void MigrateTransactions()
		{
			//TODO
			_transactionsAreMigrated = true;
		}

		public void MigrateOrders()
		{
			if(!_transactionsAreMigrated)
			{
				throw new ApplicationException("Transactions must have been migrated before orders are migrated.");
			}
			var orders = new FetchActiveOrdersQuery().Execute().ToList();
			foreach (var order in orders)
			{
				var command = new MigrateOrderCommand(order);
				var result = command.Execute();
				Console.WriteLine(result);
			}
		}
	}
}