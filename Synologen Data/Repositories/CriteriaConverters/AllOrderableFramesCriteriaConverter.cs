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
			return Criteria
				.FilterEqual(x => x.AllowOrders, true);
		}
	}
}
