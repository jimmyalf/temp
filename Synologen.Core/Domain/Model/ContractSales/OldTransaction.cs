using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class OldTransaction : Entity
	{
		public virtual decimal Amount { get; set; }
		public virtual OldSubscription Subscription { get; set; }
		public virtual DateTime CreatedDate { get; set; }
	}

	public class NewTransaction : Entity
	{
		public virtual decimal Amount { get; set; }
		public virtual NewSubscription Subscription { get; set; }
		public virtual DateTime CreatedDate { get; set; }
	}
}