using System.Diagnostics;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Synologen.Migration.AutoGiro2.Migrators;
using OldShop = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Shop;
using NewShop = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Shop;

namespace Synologen.Migration.AutoGiro2.Commands
{
	public class MigrateCustomerCommand : Command<OrderCustomer>
	{
		private readonly Customer _oldEntity;
		private readonly IMigrator<OldShop,NewShop> _shopMigrator;

		public MigrateCustomerCommand(Customer oldEntity, IMigrator<OldShop,NewShop> shopMigrator)
		{
			_oldEntity = oldEntity;
			_shopMigrator = shopMigrator;
		}

		private OrderCustomer Parse(Customer oldCustomer)
		{
			return new OrderCustomer
			{
				AddressLineOne = oldCustomer.Address.AddressLineOne,
				AddressLineTwo = oldCustomer.Address.AddressLineTwo,
				City = oldCustomer.Address.City,
				Email = oldCustomer.Contact.Email,
				FirstName = oldCustomer.FirstName,
				LastName = oldCustomer.LastName,
				MobilePhone = oldCustomer.Contact.MobilePhone,
				Notes = oldCustomer.Notes,
				PersonalIdNumber = oldCustomer.PersonalIdNumber,
				Phone = oldCustomer.Contact.Phone,
				PostalCode = oldCustomer.Address.PostalCode,
				Shop = _shopMigrator.GetNewEntity(oldCustomer.Shop)
			};
		}

		public override void Execute()
		{
			var newCustomer = Parse(_oldEntity);
			Session.Save(newCustomer);
			Result = newCustomer;
			Debug.WriteLine("Migrated customer {0} into customer {1}", _oldEntity.Id, newCustomer.Id);
		}
	}
}