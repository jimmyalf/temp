using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class ShopSettlement
	{
		public virtual int Id { get; set; }
		public virtual Shop Shop { get; set; }
		public virtual IEnumerable<SaleItem> SaleItems { get; set;}
		public virtual IEnumerable<Transaction> LensSubscriptionTransactions { get; set; }
		public virtual DateTime CreatedDate { get; set; }
	}
}