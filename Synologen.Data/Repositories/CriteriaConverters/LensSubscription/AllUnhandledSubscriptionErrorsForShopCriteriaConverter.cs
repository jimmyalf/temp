using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
{
	public class AllUnhandledSubscriptionErrorsForShopCriteriaConverter :NHibernateActionCriteriaConverter<AllUnhandledSubscriptionErrorsForShopCriteria,SubscriptionError>
	{
		public AllUnhandledSubscriptionErrorsForShopCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(AllUnhandledSubscriptionErrorsForShopCriteria source) 
		{
			return Criteria
				.CreateAlias(x => x.Subscription)
				.Sort(x => x.CreatedDate, false)
				.CreateAlias("Subscription.Customer", "SubscriptionCustomer")
				.CreateAlias("SubscriptionCustomer.Shop", "SubscriptionCustomerShop")
				.FilterEqual("SubscriptionCustomerShop.Id", source.ShopId)
				.Add(Restrictions.IsNull(Property(x => x.HandledDate)));
		}
	}
}