using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class Customer : Entity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string MobilePhone { get; set; }
		public IEnumerable<Subscription> Subscriptions { get; private set; }
		public string PersonalIdNumber { get; set; }
	}
}