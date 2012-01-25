using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class FindShopByUserNameAndHashedPasswordCriteriaConverter : NHibernateActionCriteriaConverter<FindShopByUserNameAndHashedPasswordCriteria,Shop>
	{
		public FindShopByUserNameAndHashedPasswordCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(FindShopByUserNameAndHashedPasswordCriteria source)
		{
			return Criteria
				.Add(Restrictions.InsensitiveLike(Property(x => x.ExternalAccessUsername), source.UserName, MatchMode.Exact))
				.Add(Restrictions.Eq(Property(x => x.ExternalAccessHashedPassword), source.HashedPassword));
		}
	}
}