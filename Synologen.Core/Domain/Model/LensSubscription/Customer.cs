using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class Customer : Entity
	{
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual Shop Shop { get; set; }
		public virtual CustomerContact Contact { get; set; }
		public virtual CustomerAddress Address { get; set; }
		public virtual IEnumerable<Subscription> Subscriptions { get; private set; }
		public virtual string PersonalIdNumber { get; set; }
	}
}