using System;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Commands;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain
{
	public class Migrator
	{
		 public void MigrateOrders()
		 {
		 	var orders = new FetchOrdersQuery().Execute();
		 	foreach (var order in orders)
		 	{
		 		var command = new MigrateOrderCommand(order);
				//TODO: command.Execute();
		 		Console.WriteLine(order);
		 	}
		 }
	}
}