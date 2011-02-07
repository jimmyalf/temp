using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class AllFrameOrdersForShopCriteriaConverter: NHibernateActionCriteriaConverter<AllFrameOrdersForShopCriteria, FrameOrder>
	{
		public AllFrameOrdersForShopCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllFrameOrdersForShopCriteria source) 
		{
			return Criteria
				.CreateAlias(x => x.OrderingShop)
				.CreateAlias(x => x.Frame)
				.FilterEqual(x => x.OrderingShop.Id, source.ShopId);
		}
	}
}