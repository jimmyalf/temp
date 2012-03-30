using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class Settlement : Entity
	{
		public virtual IEnumerable<ContractSale> ContractSales { get; set; }
		public virtual IEnumerable<OldTransaction> OldTransactions { get; set; }
		public virtual IEnumerable<NewTransaction> NewTransactions { get; set; }
		public virtual DateTime CreatedDate { get; set; }
	}
}