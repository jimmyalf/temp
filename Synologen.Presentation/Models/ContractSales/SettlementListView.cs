using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class SettlementListView
	{
		public SettlementListView(IEnumerable<Settlement> settlements)
		{
			Settlements = settlements.Select(Parse);
		}

		private SettlementListViewItem Parse(Settlement settlement)
		{
			return new SettlementListViewItem
			{
				Id = settlement.Id,
				NumberOfContractSalesInSettlement = settlement.ContractSales.Count(),
				NumberOfOldTransactionsInSettlement = settlement.OldTransactions.Count(),
				NumberOfNewTransactionsInSettlement = settlement.NewTransactions.Count(),
				CreatedDate = settlement.CreatedDate.ToString("yyyy-MM-dd HH:mm")
			};
		}

		public int NumberOfContractSalesReadyForInvocing { get; set; }
		public int NumberOfOldTransactionsReadyForInvocing { get; set; }
		public int NumberOfNewTransactionsReadyForInvocing { get; set; }
		public IEnumerable<SettlementListViewItem> Settlements { get; set; }
		public bool DisplayCreateSettlementsButton
		{
			get { return (
				NumberOfContractSalesReadyForInvocing + 
				NumberOfOldTransactionsReadyForInvocing +
				NumberOfNewTransactionsReadyForInvocing > 0); }
		}
	}
}