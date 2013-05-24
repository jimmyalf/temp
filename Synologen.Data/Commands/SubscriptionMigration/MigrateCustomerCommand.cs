using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Shop = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Shop;

namespace Spinit.Wpc.Synologen.Data.Commands.SubscriptionMigration
{
	public class MigrateCustomerCommand : Command<OrderCustomer>
	{
		private readonly Customer _oldEntity;
		public MigrateCustomerCommand(Customer oldEntity)
		{
			_oldEntity = oldEntity;
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
				Shop = Session.Get<Shop>(oldCustomer.Shop.Id)
			};
		}

		public override void Execute()
		{
			var newCustomer = Parse(_oldEntity);
			Session.Save(newCustomer);
			Result = newCustomer;
		}
	}
}