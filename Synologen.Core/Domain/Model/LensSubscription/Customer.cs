using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class Customer : Entity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public CustomerContact Contact { get; set; }
		public CustomerAddress Address { get; set; }
		public IEnumerable<Subscription> Subscriptions { get; private set; }
		public string PersonalIdNumber { get; set; }
	}
}