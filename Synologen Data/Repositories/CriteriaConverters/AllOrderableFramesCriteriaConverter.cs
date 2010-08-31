using NHibernate;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class AllOrderableFramesCriteriaConverter : IActionCriteriaConverter<AllOrderableFramesCriteria, ICriteria>
	{
		private readonly ISession _session;
		public AllOrderableFramesCriteriaConverter(ISession session) { _session = session; }

		public ICriteria Convert(AllOrderableFramesCriteria source)
		{
			return _session.CreateCriteria<Frame>()
				.FilterEqual<Frame, bool>(x => x.AllowOrders, true);
		}
	}
}
