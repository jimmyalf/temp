using Spinit.Data.SqlClient.SqlBuilder;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries
{
	public class FetchSubscriptionItemsInPendingPaymentQuery : PersistenceBase
	{
		private readonly int _pendingPaymentId;

		public FetchSubscriptionItemsInPendingPaymentQuery(int pendingPaymentId)
		{
			_pendingPaymentId = pendingPaymentId;
		}

		public void Execute()
		{
			var query = QueryBuilder.Build("");
		}

		/*SELECT * FROM SynologenOrderSubscriptionItem
INNER JOIN SynologenOrderSubscriptionPendingPayment_SynologenOrderSubscriptionItem ON SynologenOrderSubscriptionPendingPayment_SynologenOrderSubscriptionItem.SubscriptionItemId = SynologenOrderSubscriptionItem.Id
WHERE SynologenOrderSubscriptionPendingPayment_SynologenOrderSubscriptionItem.SubscriptionPendingPaymentId = @PendingPaymentId*/
	}
}