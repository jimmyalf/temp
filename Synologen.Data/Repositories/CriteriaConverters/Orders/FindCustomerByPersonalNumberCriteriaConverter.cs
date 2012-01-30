using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class FindCustomerByPersonalNumberCriteriaConverter : NHibernateActionCriteriaConverter<FindCustomerByPersonalNumberCriteria,OrderCustomer>
	{
		public FindCustomerByPersonalNumberCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(FindCustomerByPersonalNumberCriteria source)
		{
			return Criteria.Add(Restrictions.Eq(Property(x => x.PersonalIdNumber), source.PersonalNumber));
		}
	}
}