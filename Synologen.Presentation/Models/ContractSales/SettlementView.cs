using System.Collections.Generic;
using System.ComponentModel;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class SettlementView
	{
		[DisplayName("Id")]
		public int Id { get; set; }

		[DisplayName("Skapad")]
		public string CreatedDate { get; set; }

		[DisplayName("Period")]
		public string Period { get; set; }

		[DisplayName("Utbetalas inkl moms")]
		public string SumAmountIncludingVAT { get; set; }
		
		//[DisplayName("Utbetalas exkl moms")]
		//public string SumAmountExcludingVAT { get; set; }

		public IEnumerable<ShopSettlementItem> SettlementItems { get; set; }
	}
}