using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class AllArticlesWithSupplierCriteriaConverter : NHibernateActionCriteriaConverter<AllArticlesWithSupplierCriteria,Article>
	{
		public AllArticlesWithSupplierCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllArticlesWithSupplierCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.ArticleSupplier)
				.FilterEqual(x => x.ArticleSupplier.Id, source.SupplierId);
		}
	}
}