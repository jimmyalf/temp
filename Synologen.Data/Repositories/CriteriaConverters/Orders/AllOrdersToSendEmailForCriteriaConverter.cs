using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Order = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Order;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class AllOrdersToSendEmailForCriteriaConverter : NHibernateActionCriteriaConverter<AllOrdersToSendEmailForCriteria,Order>
	{
		public AllOrdersToSendEmailForCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllOrdersToSendEmailForCriteria source)
		{
			var isRightShippingType = Restrictions.Not(Restrictions.Eq(Property(x => x.ShippingType), OrderShippingOption.NoOrder));
			var isNotAlreadySent = Restrictions.IsNull(Property(x => x.SpinitServicesEmailId));
			var isConfirmed = Restrictions.Eq(Property(x => x.Status), OrderStatus.Confirmed);
			return Criteria
				.Add(Restrictions.And(Restrictions.And(isRightShippingType, isNotAlreadySent), isConfirmed))
				.SetFetchMode(Property(x => x.Shop), FetchMode.Join)
				.SetFetchMode(Property(x => x.LensRecipe), FetchMode.Join)
				.SetFetchMode(Property(x => x.LensRecipe.Article.Left), FetchMode.Join)
				.SetFetchMode(Property(x => x.LensRecipe.Article.Right), FetchMode.Join)
				.SetFetchMode(Property(x => x.LensRecipe.ArticleSupplier), FetchMode.Join)
				.SetFetchMode(Property(x => x.Customer), FetchMode.Join)
				.SetResultTransformer(new DistinctRootEntityResultTransformer());
		}
	}
}