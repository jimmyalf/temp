using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Integration.Test.CommonDataTestHelpers;

namespace Spinit.Wpc.Synologen.Integration.Test.LensSubscriptionData
{
	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_adding_a_customer : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToSave;

		public When_adding_a_customer()
		{
			Context = session =>
			{
				Shop shop = new ShopRepository(session).Get(158);
				Country country = new CountryRepository(session).Get(1);
				_customerToSave = Factories.CustomerFactory.Get(country, shop);
			};

			Because = repository => repository.Save(_customerToSave);
		}

		[Test]
		public void Should_save_the_customer()
		{
			AssertUsing(session =>
			{
				var savedCustomer = new CustomerRepository(session).Get(_customerToSave.Id);
				savedCustomer.ShouldBe(_customerToSave);
				savedCustomer.Address.ShouldBe(_customerToSave.Address);
				savedCustomer.Contact.ShouldBe(_customerToSave.Contact);
				savedCustomer.FirstName.ShouldBe(_customerToSave.FirstName);
				savedCustomer.LastName.ShouldBe(_customerToSave.LastName);
				savedCustomer.PersonalIdNumber.ShouldBe(_customerToSave.PersonalIdNumber);
				savedCustomer.Shop.ShouldBe(_customerToSave.Shop);
				savedCustomer.Subscriptions.Count().ShouldBe(_customerToSave.Subscriptions.Count());

			});
		}

	}

	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_editing_a_customer : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToEdit;

		public When_editing_a_customer()
		{
			Context = session =>
			{
				Shop shop = new ShopRepository(session).Get(158);
				Country country = new CountryRepository(session).Get(1);
				_customerToEdit = Factories.CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(_customerToEdit);
				_customerToEdit = Factories.CustomerFactory.Edit(_customerToEdit);

			};

			Because = repository => repository.Save(_customerToEdit);
		}

		[Test]
		public void Should_edit_the_customer()
		{
			AssertUsing(session =>
			{
				var savedCustomer = new CustomerRepository(session).Get(_customerToEdit.Id);
				savedCustomer.ShouldBe(_customerToEdit);
				savedCustomer.Address.ShouldBe(_customerToEdit.Address);
				savedCustomer.Contact.ShouldBe(_customerToEdit.Contact);
				savedCustomer.FirstName.ShouldBe(_customerToEdit.FirstName);
				savedCustomer.LastName.ShouldBe(_customerToEdit.LastName);
				savedCustomer.PersonalIdNumber.ShouldBe(_customerToEdit.PersonalIdNumber);
				savedCustomer.Shop.ShouldBe(_customerToEdit.Shop);
				savedCustomer.Subscriptions.Count().ShouldBe(_customerToEdit.Subscriptions.Count());
			});
		}

	}

	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_deleting_a_customer : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToDelete;

		public When_deleting_a_customer()
		{
			Context = (ISession session) =>
			{
				Shop shop = new ShopRepository(session).Get(158);
				Country country = new CountryRepository(session).Get(1);
				_customerToDelete = Factories.CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(_customerToDelete);
			};

			Because = repository => repository.Delete(_customerToDelete);
		}

		[Test]
		public void Should_delete_the_subscription()
		{
			AssertUsing(session =>
			{
				var fetchedCustomer = new CustomerRepository(session).Get(_customerToDelete.Id);
				fetchedCustomer.ShouldBe(null);
			});
		}
	}

	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_fetching_all_customers : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToAdd1;
		private Customer _customerToAdd2;

		public When_fetching_all_customers()
		{
			Context = session =>
			{
				Shop shop = new ShopRepository(session).Get(158);
				Country country = new CountryRepository(session).Get(1);
				_customerToAdd1 = Factories.CustomerFactory.Get(country, shop);
				_customerToAdd2 = Factories.CustomerFactory.Get(country, shop);
			};

			Because = repository =>
          	{
          		repository.Save(_customerToAdd1);
          		repository.Save(_customerToAdd2);
          	};
		}

		[Test]
		public void Should_get_all_customers()
		{
			AssertUsing(session =>
			{
				var savedCustomers = new CustomerRepository(session).GetAll();
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd1.Id);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd2.Id);
				savedCustomers.Count().ShouldBe(2);
			});
		}

	}

}
