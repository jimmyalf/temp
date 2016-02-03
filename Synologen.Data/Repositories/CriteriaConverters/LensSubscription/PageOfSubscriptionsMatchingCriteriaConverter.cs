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
			var orderBy = TranslateProperty(source.OrderBy);
			var customerShopName = TranslateProperty(Property(x => x.Customer.Shop.Name));
			return Criteria
				.CreateAlias(x => x.Customer)
				.CreateAlias(x => x.Customer.Shop, CustomerShopAlias)
				.FilterByAny(filter =>
				{
					filter.By(x => x.Customer.LastName);
					filter.By(x => x.Customer.FirstName);
					filter.By(x => x.Customer.PersonalIdNumber);
					filter.By(customerShopName);
				}, source.SearchTerm)
				.Page(source.Page, source.PageSize)
				.Sort(orderBy, source.SortAscending);
		}

		private string TranslateProperty(string orderBy)
		{
			return orderBy == null ? null : orderBy.Replace(Property(x => x.Customer.Shop), CustomerShopAlias);
		}
	}
}

