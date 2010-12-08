using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class ShopSettlement
	{
		public virtual int Id { get; set; }
		public virtual Shop Shop { get; set; }
		public virtual IEnumerable<SaleItem> SaleItems { get; set;}
		public virtual decimal ContractSalesValueIncludingVAT { get; set; }
		public virtual decimal LensSubscriptionsValueIncludingVAT { get; set; }
		public virtual IEnumerable<Transaction> LensSubscriptionTransactions { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public bool AllContractSalesHaveBeenMarkedAsPayed { get; set; }
	}
}