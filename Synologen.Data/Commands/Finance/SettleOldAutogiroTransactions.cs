using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Data.Queries.Finance;

namespace Spinit.Wpc.Synologen.Data.Commands.Finance
{
	public class SettleOldAutogiroTransactions : Command
	{
		private readonly int _settlementId;

		public SettleOldAutogiroTransactions(int settlementId)
		{
			_settlementId = settlementId;
		}

		public override void Execute()
		{
			var settlement = Session.Get<Settlement>(_settlementId);
			var transactions = Query(new GetOldAutogiroTransactionsReadyForSettlement());
			foreach (var transaction in transactions)
			{
				transaction.Settlement = settlement;
				Session.Save(transaction);
			}
		}
	}
}