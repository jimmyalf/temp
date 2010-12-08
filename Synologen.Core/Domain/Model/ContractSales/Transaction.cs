using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class Transaction : Entity
	{
		public virtual decimal Amount { get; set; }
		public virtual Subscription Subscription { get; set; }
		public virtual DateTime CreatedDate { get; set; }
	}
}