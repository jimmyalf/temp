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
		public string LensSubscriptionsValueIncludingVAT { get; set; }
		public string LensSubscriptionTransactionsCount { get; set; }
		public IEnumerable<SettlementDetailedContractSaleListItemModel> DetailedContractSales { get; set; }
		public IEnumerable<SettlementSimpleContractSaleListItemModel> SimpleContractSales { get; set; }
		public IEnumerable<SettlementDetailedSubscriptionTransactionsListItemModel> DetailedSubscriptionTransactions { get; set; }
		public bool DisplayDetailedView{ get; set; }
		public bool DisplaySimpleView{ get { return !DisplayDetailedView; } }
		public string SwitchViewButtonText { get; set; }
		public bool MarkAsPayedButtonEnabled { get; set; }
	}
}