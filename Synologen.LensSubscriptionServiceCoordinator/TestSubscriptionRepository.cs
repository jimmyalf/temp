using System;
using System.Collections.Generic;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator
{
	public class TestSubscriptionRepository : ISubscriptionRepository
	{
		public Subscription Get(int id) { throw new NotImplementedException(); }
		public IEnumerable<Subscription> GetAll() { throw new NotImplementedException(); }
		public IEnumerable<Subscription> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { throw new NotImplementedException(); }
		public void Save(Subscription entity) { throw new NotImplementedException(); }
		public void Delete(Subscription entity) { throw new NotImplementedException(); }
	}
}