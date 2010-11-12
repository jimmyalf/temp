using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class SettlementListView
	{
		public int NumberOfContractSalesReadyForInvocing { get; set; }
		public IEnumerable<SettlementListViewItem> Settlements { get; set; }
		public bool DisplayCreateSettlementsButton
		{
			get { return (NumberOfContractSalesReadyForInvocing > 0); }
		}
	}
}