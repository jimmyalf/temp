using NHibernate;
using Synologen.Migration.AutoGiro2.Commands;
using OldShop = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Shop;
using NewShop = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Shop;
using OldSubscription = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;
using NewSubscription = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Subscription;
using OldCustomer = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Customer;
using NewCustomer = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.OrderCustomer;

namespace Synologen.Migration.AutoGiro2.Migrators
{
	public class SubscriptionMigrator : MigratorBase<OldSubscription, NewSubscription>
	{
		private readonly IMigrator<OldCustomer, NewCustomer> _customerMigrator;
		private readonly IMigrator<OldShop, NewShop> _shopMigrator;

		public SubscriptionMigrator(ISession session,
			IMigrator<OldCustomer,NewCustomer> customerMigrator,
			IMigrator<OldShop,NewShop> shopMigrator) : base(session)
		{
			_customerMigrator = customerMigrator;
			_shopMigrator = shopMigrator;
		}

		protected override NewSubscription Migrate(OldSubscription oldEntity)
		{
			return Execute(new MigrateSubscriptionCommand(oldEntity, _customerMigrator, _shopMigrator));
		}
	}

}