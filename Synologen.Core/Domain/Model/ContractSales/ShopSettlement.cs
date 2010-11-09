using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class ShopSettlement
	{
		public virtual int Id { get; private set; }
		public virtual IEnumerable<ContractSale> ContractSales { get; private set; }
		public virtual IEnumerable<Transaction> LensSubscriptionTransactions { get; private set; }
		public virtual DateTime CreatedDate { get; private set; }
	}
}