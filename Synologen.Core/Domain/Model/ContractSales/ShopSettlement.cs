using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class ShopSettlement
	{
		public virtual int Id { get; set; }
		public virtual Shop Shop { get; set; }
		public virtual IEnumerable<SaleItem> SaleItems { get; set;}
		public virtual decimal ContractSalesValueIncludingVAT { get; set; }
		public virtual decimal OldTransactionValueIncludingVAT { get; set; }
		public virtual decimal NewTransactionValueIncludingVAT { get; set; }
		public virtual IEnumerable<OldTransaction> OldTransactions { get; set; }
		public virtual IEnumerable<NewTransaction> NewTransactions { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public bool AllContractSalesHaveBeenMarkedAsPayed { get; set; }

		
	}
}