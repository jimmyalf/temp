using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Synologen.Migration.AutoGiro2.Validators
{
	public class CustomerValidator : ValidatorBase<Customer,OrderCustomer>
	{
		public override void Validate(Customer oldItem, OrderCustomer newItem)
		{
			Validate(oldItem.Address.AddressLineOne, newItem.AddressLineOne, "address line one");
			Validate(oldItem.Address.AddressLineTwo, newItem.AddressLineTwo, "address line two");
			Validate(oldItem.Address.City, newItem.City, "city");
			Validate(oldItem.Address.PostalCode, newItem.PostalCode, "postal code");
			Validate(oldItem.Contact.Email, newItem.Email, "email");
			Validate(oldItem.Contact.MobilePhone, newItem.MobilePhone, "mobile phone");
			Validate(oldItem.Contact.Phone, newItem.Phone, "phone");
			Validate(oldItem.FirstName, newItem.FirstName, "first name");
			Validate(oldItem.LastName, newItem.LastName, "last name");
			Validate(oldItem.PersonalIdNumber, newItem.PersonalIdNumber, "personal id");
			Validate(oldItem.Shop.Id, newItem.Shop.Id, "shop id");
		}
	}
}