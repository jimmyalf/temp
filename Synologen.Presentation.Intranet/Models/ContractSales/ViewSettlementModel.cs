using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.ContractSales
{
	public class ViewSettlementModel 
	{
		public ViewSettlementModel()
		{
			MarkAsPayedButtonEnabled = true;
		}
		public int SettlementId { get; set; }
		public string ShopNumber { get; set; }
		public string Period { get; set; }
		public string ContractSalesValueIncludingVAT { get; set; }
		public string OldTransactionsValueIncludingVAT { get; set; }
		public string NewTransactionsValueIncludingVAT { get; set; }
		public string OldTransactionsCount { get; set; }
		public string NewTransactionCount { get; set; }
		public string NewTransactionTaxedValue { get; set; }
		public string NewTransactionTaxFreeValue { get; set; }
		public IEnumerable<SettlementDetailedContractSaleListItemModel> DetailedContractSales { get; set; }
		public IEnumerable<SettlementSimpleContractSaleListItemModel> SimpleContractSales { get; set; }
		public IEnumerable<SettlementDetailedSubscriptionTransactionsListItemModel> OldTransactions { get; set; }
		public IEnumerable<SettlementDetailedSubscriptionTransactionsListItemModel> NewTransactions { get; set; }
		public bool DisplayDetailedView{ get; set; }
		public bool DisplaySimpleView{ get { return !DisplayDetailedView; } }
		public string SwitchViewButtonText { get; set; }
		public bool MarkAsPayedButtonEnabled { get; set; }
	}
}