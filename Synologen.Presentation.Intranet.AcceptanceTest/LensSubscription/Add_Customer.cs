using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.LensSubscription
{
	[TestFixture, Category("Feature: Adding customer")]
	public class When_adding_a_new_customer : PresenterBase
	{
		protected CreateCustomerPresenter presenter;
		private SaveCustomerEventArgs saveEventArgs;
		protected ICreateCustomerView view;
		public When_adding_a_new_customer()
		{
			Context = () =>
			{
				view = A.Fake<ICreateCustomerView>();
				presenter = ResolvePresenter<CreateCustomerPresenter,ICreateCustomerView>(view);
				saveEventArgs = new SaveCustomerEventArgs
				{
					AddressLineOne = "AddressLineOne",
					AddressLineTwo = "AddressLineTwo",
					City = "City",
					Email = "email@email.se",
					FirstName = "FirstName",
					LastName = "LastName",
					MobilePhone = "MobilePhone",
					Notes = "Notes",
					PersonalIdNumber = "197910071111",
					Phone = "031-123546",
					PostalCode = "43632",
				};
			};
			Because = () =>
			{
				presenter.View_Submit(this, saveEventArgs);
			};
		}

		[Test]
		public void A_customer_is_stored()
		{
			var lastCustomer = new CustomerRepository(GetSession()).GetAll().Last();

			lastCustomer.Address.AddressLineOne.ShouldBe(saveEventArgs.AddressLineOne);
			lastCustomer.Address.AddressLineTwo.ShouldBe(saveEventArgs.AddressLineTwo);
			lastCustomer.Address.City.ShouldBe(saveEventArgs.City);
			lastCustomer.Address.Country.Name.ShouldBe("Sverige");
			lastCustomer.Address.PostalCode.ShouldBe(saveEventArgs.PostalCode);
			lastCustomer.Contact.Email.ShouldBe(saveEventArgs.Email);
			lastCustomer.Contact.MobilePhone.ShouldBe(saveEventArgs.MobilePhone);
			lastCustomer.Contact.Phone.ShouldBe(saveEventArgs.Phone);
			lastCustomer.FirstName.ShouldBe(saveEventArgs.FirstName);
			lastCustomer.LastName.ShouldBe(saveEventArgs.LastName);
			lastCustomer.Notes.ShouldBe(saveEventArgs.Notes);
			lastCustomer.PersonalIdNumber.ShouldBe(saveEventArgs.PersonalIdNumber);
			lastCustomer.Shop.Id.ShouldBe(testShopId);
			lastCustomer.Subscriptions.Count().ShouldBe(0);
		}

	}
}