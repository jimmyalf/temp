using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ContractSales
{
	public class AllArticlesMatchingCriteriaConverter : NHibernateActionCriteriaConverter<AllArticlesMatchingCriteria, Article>
	{
		public AllArticlesMatchingCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllArticlesMatchingCriteria source)
		{
			return Criteria
				.Page(source.Page, source.PageSize)
				.Sort(source.OrderBy, source.SortAscending);
		}
	}
}