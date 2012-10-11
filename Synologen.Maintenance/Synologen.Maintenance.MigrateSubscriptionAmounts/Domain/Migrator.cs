using System;
using System.Collections.Generic;
using System.Linq;
using Synologen.Maintenance.MigrateSubscriptionAmounts.App;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Commands;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries;
using log4net;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain
{
	public class Migrator
	{
		private bool _transactionsAreMigrated;
		private readonly ILog _log;

		public Migrator()
		{
			_log = Factory.CreateLogFor<Migrator>();
		}

		public void MigrateTransactions()
		{
			//TODO
			_transactionsAreMigrated = true;
		}

		public IEnumerable<IMigratedResult> MigrateOrders()
		{
			if(!_transactionsAreMigrated)
			{
				throw new ApplicationException("Transactions must have been migrated before orders are migrated.");
			}
			var orders = new FetchActiveOrdersQuery().Execute().ToList();
			_log.InfoFormat("Migrate Orders: Found {0} orders to migrate", orders.Count);
			foreach (var order in orders)
			{
				var command = new MigrateOrderCommand(order);
				var result = command.Execute();
				_log.Info(result);
				yield return result;
			}
		}
	}
}