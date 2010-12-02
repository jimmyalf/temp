using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.ContractSales
{
	public class ViewSettlementModel 
	{
		public int SettlementId { get; set; }
		public string ShopNumber { get; set; }
		public string Period { get; set; }
		public string ContractSalesValueIncludingVAT { get; set; }
		public IEnumerable<SettlementDetailedContractSaleListItemModel> DetailedContractSales { get; set; }
		public IEnumerable<SettlementSimpleContractSaleListItemModel> SimpleContractSales { get; set; }
		public bool DisplayDetailedView{ get; set; }
		public bool DisplaySimpleView{ get { return !DisplayDetailedView; } }
		public string SwitchViewButtonText { get; set; }
	}
}