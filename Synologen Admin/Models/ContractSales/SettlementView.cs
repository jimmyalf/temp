using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class SettlementView
	{
		public int Id { get; set; }
		public string CreatedDate { get; set; }
		public string Period { get; set; }
		public string SumAmountIncludingVAT { get; set; }
		public string SumAmountExcludingVAT { get; set; }
		public IEnumerable<ShopSettlementItem> SettlementItems { get; set; }
	}
}