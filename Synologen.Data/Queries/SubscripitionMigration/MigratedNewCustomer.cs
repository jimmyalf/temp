using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Queries.SubscripitionMigration
{
	public class MigratedNewCustomer : Query<OrderCustomer>
	{
		private readonly string _personalIdNumber;
		private readonly int _shopId;

		public MigratedNewCustomer(string personalIdNumber, int shopId)
		{
			_personalIdNumber = personalIdNumber;
			_shopId = shopId;
		}

		public override OrderCustomer Execute()
		{
			return Session.CreateCriteria<OrderCustomer>()
				.CreateAlias("Shop", "Shop")
				.Add(Restrictions.Eq("PersonalIdNumber", _personalIdNumber))
				.Add(Restrictions.Eq("Shop.Id", _shopId))
				.UniqueResult<OrderCustomer>();
		}
	}
}