using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories
{
	public class ShopRepository : NHibernateReadonlyRepository<Shop>, IShopRepository
	{
		public ShopRepository(ISession session) : base(session) { }
	}
}
