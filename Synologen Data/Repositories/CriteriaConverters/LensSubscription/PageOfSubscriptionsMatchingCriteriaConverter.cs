using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
{
	public class PageOfSubscriptionsMatchingCriteriaConverter: NHibernateActionCriteriaConverter<PageOfSubscriptionsMatchingCriteria, Subscription>
	{
		private const string CustomerShopAlias = "CustomerShop";
		public PageOfSubscriptionsMatchingCriteriaConverter(ISession session) : base(session) {}


		public override ICriteria Convert(PageOfSubscriptionsMatchingCriteria source) 
		{
			var orderBy = GetOrderBy(source.OrderBy);

			return Criteria
				.CreateAlias(x => x.Customer)
				.CreateAlias(x => x.Customer.Shop, CustomerShopAlias)
				.Page(source.Page, source.PageSize)
				.Sort(orderBy, source.SortAscending);
		}

		private string GetOrderBy(string orderBy)
		{
			return orderBy == null ? null : orderBy.Replace(Property(x => x.Customer.Shop), CustomerShopAlias);
		}
	}
}

