using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class ShopSettlement : Entity
	{
		public virtual IEnumerable<ContractSale> ContractSales { get; set; }
		public virtual IEnumerable<Transaction> LensSubscriptionTransactions { get; set; }
		public virtual DateTime CreatedDate { get; set; }
	}
}