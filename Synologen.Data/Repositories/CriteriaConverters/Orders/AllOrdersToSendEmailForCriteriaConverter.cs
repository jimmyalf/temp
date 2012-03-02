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
			var isRightShippingType = 
				Restrictions.Eq(Property(x => x.ShippingType), OrderShippingOption.ToCustomer) || 
				Restrictions.Eq(Property(x => x.ShippingType), OrderShippingOption.ToStore);
			var isNotAlreadySent = Restrictions.IsNull(Property(x => x.SpinitServicesEmailId));
			return Criteria
				.FilterEqual(x => x.Status, OrderStatus.Confirmed)
				.Add(Restrictions.And(isRightShippingType, isNotAlreadySent))
				.SetFetchMode(Property(x => x.Shop), FetchMode.Join)
				.SetFetchMode(Property(x => x.LensRecipe), FetchMode.Join)
				.SetFetchMode(Property(x => x.Article.Left), FetchMode.Join)
				.SetFetchMode(Property(x => x.Article.Right), FetchMode.Join)
				.SetFetchMode(Property(x => x.Article.Left.ArticleSupplier), FetchMode.Join)
				.SetFetchMode(Property(x => x.Article.Right.ArticleSupplier), FetchMode.Join)
				.SetFetchMode(Property(x => x.Customer), FetchMode.Join)
				.SetResultTransformer(new DistinctRootEntityResultTransformer());
		}
	}
}