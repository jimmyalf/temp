using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class AllOrderableFramesCriteriaConverter : NHibernateActionCriteriaConverter<AllOrderableFramesCriteria, Frame>
	{
		public AllOrderableFramesCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(AllOrderableFramesCriteria source)
		{
			var criteria = Criteria
                .FilterEqual(x => x.AllowOrders, true)
                .CreateAlias(x => x.Supplier)
                .Sort(source.SortExpression, true);

            if (source.SupplierId.HasValue)
            {
                criteria = criteria.FilterEqual(x => x.Supplier.Id, source.SupplierId);
            }

            return criteria;
		}
	}
}
