using NHibernate;
using NHibernate.Transform;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
{
	public class AllSubscriptionsForShopCriteriaConverter : NHibernateActionCriteriaConverter<AllSubscriptionsForShopCriteria, Subscription>, IActionCriteria
	{
		public AllSubscriptionsForShopCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllSubscriptionsForShopCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.Customer)
				.CreateAlias(x => x.Customer.Shop)
				.CreateAlias(x => x.Transactions)
				.FilterEqual(x => x.Customer.Shop.Id, source.ShopId)
				.SetResultTransformer(Transformers.DistinctRootEntity);
		}
	}
}