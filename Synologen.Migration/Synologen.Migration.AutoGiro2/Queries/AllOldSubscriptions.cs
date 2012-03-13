using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using OldSubscription = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;
using NewSubscription = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Subscription;

namespace Synologen.Migration.AutoGiro2.Queries
{
	public class AllOldSubscriptions : Query<IEnumerable<OldSubscription>>
	{
		public override IEnumerable<OldSubscription> Execute()
		{
			return Session
				.CreateCriteria<OldSubscription>()
				.SetFetchMode("Customer",FetchMode.Join)
				.SetFetchMode("Transactions",FetchMode.Join)
				.SetFetchMode("Errors",FetchMode.Join)
				.SetResultTransformer(new DistinctRootEntityResultTransformer())
				.List<OldSubscription>();
		}
	}
}