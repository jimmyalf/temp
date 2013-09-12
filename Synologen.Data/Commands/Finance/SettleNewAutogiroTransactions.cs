using Spinit.Wpc.Synologen.Data.Queries.Finance;

namespace Spinit.Wpc.Synologen.Data.Commands.Finance
{
	public class SettleNewAutogiroTransactions : Command
	{
		private readonly int _settlementId;

		public SettleNewAutogiroTransactions(int settlementId)
		{
			_settlementId = settlementId;
		}

		public override void Execute()
		{
			var transactions = Query(new GetNewAutogiroTransactionsReadyForSettlement());
			foreach (var transaction in transactions)
			{
				transaction.SettlementId = _settlementId;
				Session.Save(transaction);
			}
		}
	}
}