using NHibernate;
using Synologen.Migration.AutoGiro2.Commands;
using OldShop = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Shop;
using NewShop = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Shop;
using OldCustomer = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Customer;
using NewCustomer = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.OrderCustomer;

namespace Synologen.Migration.AutoGiro2.Migrators
{
	public class CustomerMigrator : MigratorBase<OldCustomer, NewCustomer>
	{
		private readonly IMigrator<OldShop, NewShop> _shopMigrator;

		public CustomerMigrator(ISession session, IMigrator<OldShop,NewShop> shopMigrator) : base(session)
		{
			_shopMigrator = shopMigrator;
		}

		protected override NewCustomer Migrate(OldCustomer oldEntity)
		{
			return Execute(new MigrateCustomerCommand(oldEntity, _shopMigrator));
		}
	}
}