using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class PageOfOrdersMatchingCriteriaConverter : NHibernateActionCriteriaConverter<PageOfOrdersMatchingCriteria, Core.Domain.Model.Orders.Order>
	{
		public PageOfOrdersMatchingCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(PageOfOrdersMatchingCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.Customer)
				.CreateAlias(x => x.Shop)
				.SynologenFilterByAny(filter =>
				{
					filter.IfInt(source.SearchTerm, parsedValue => filter.Equal(x => x.Id, parsedValue));
					filter.Like(x => x.Customer.FirstName);
					filter.Like(x => x.Customer.LastName);
					filter.Like(x => x.Customer.PersonalIdNumber);
					filter.Like(x => x.Shop.Name);
				}, source.SearchTerm)
				.Page(source.Page, source.PageSize)
				.Sort(source.OrderBy, source.SortAscending);
		}
	}
}