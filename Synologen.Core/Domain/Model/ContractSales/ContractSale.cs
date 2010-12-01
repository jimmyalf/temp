using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class ContractSale : Entity
	{
		public virtual Shop Shop { get; set; }
		public virtual decimal TotalAmountIncludingVAT { get; set; }
		public virtual int StatusId { get; set; }
		public virtual IEnumerable<SaleItem> SaleItems { get; set;}
	}
}