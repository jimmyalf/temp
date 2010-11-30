using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
{
	public class CustomersForShopMatchingCriteriaConverter : NHibernateActionCriteriaConverter<CustomersForShopMatchingCriteria, Customer>
	{
		public CustomersForShopMatchingCriteriaConverter(ISession session) : base(session)
		{
		}

		public override ICriteria Convert(CustomersForShopMatchingCriteria source)
		{
			return Criteria.CreateAlias(x => x.Shop)
				.FilterEqual(x => x.Shop.Id, source.ShopId)
				.FilterByAny(filter => {
					filter.By(x => x.FirstName);
					filter.By(x => x.LastName);
					filter.By(x => x.PersonalIdNumber);

				}, source.SearchTerm);
		}
	}
}
