using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Queries.SubscripitionMigration
{
	public class MigratedNewCustomer : Query<OrderCustomer>
	{
		private readonly int _shopId;
		private readonly Customer _oldCustomer;

		public MigratedNewCustomer(int shopId, Customer oldCustomer)
		{
			_shopId = shopId;
			_oldCustomer = oldCustomer;
		}

		public override OrderCustomer Execute()
		{
			return Session.CreateCriteria<OrderCustomer>()
				.CreateAlias("Shop", "Shop")
				.Add(Restrictions.Eq("PersonalIdNumber", _oldCustomer.PersonalIdNumber))
				.Add(Restrictions.Eq("FirstName", _oldCustomer.FirstName))
				.Add(Restrictions.Eq("LastName", _oldCustomer.LastName))
				.Add(Restrictions.Eq("AddressLineOne", _oldCustomer.Address.AddressLineOne))
				.Add(Restrictions.Eq("City", _oldCustomer.Address.City))
				.Add(Restrictions.Eq("PostalCode", _oldCustomer.Address.PostalCode))
				.Add(Restrictions.Eq("Shop.Id", _shopId))
				.UniqueResult<OrderCustomer>();
		}
	}
}