using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales
{
	public class AllContractSalesMatchingCriteria : IActionCriteria
	{
		public int ContractSaleStatus { get; set; }
		//public long? InvoiceNumber { get; set; }
	}
}