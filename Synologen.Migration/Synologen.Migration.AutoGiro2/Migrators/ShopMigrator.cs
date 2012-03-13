using NHibernate;
using Synologen.Migration.AutoGiro2.Queries;
using OldShop = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Shop;
using NewShop = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Shop;

namespace Synologen.Migration.AutoGiro2.Migrators
{
	public class ShopMigrator : MigratorBase<OldShop, NewShop>
	{
		public ShopMigrator(ISession session) : base(session) { }

		protected override NewShop Migrate(OldShop oldEntity)
		{
			return Query(new GetById<NewShop>(oldEntity.Id));
		}
	}
}