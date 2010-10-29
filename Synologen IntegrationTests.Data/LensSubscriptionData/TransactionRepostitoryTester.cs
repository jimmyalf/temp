using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Shouldly;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData
{
	[TestFixture]
	[Category("TransactionRepositoryTester")]
	public class When_adding_a_transaction : BaseRepositoryTester<TransactionRepository>
	{

		private SubscriptionTransaction _transactionToSave;

		public When_adding_a_transaction()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);
				var subscription = SubscriptionFactory.Get(customer);
				new SubscriptionRepository(session).Save(subscription);
				_transactionToSave = TransactionFactory.Get(subscription);
			};

			Because = repository => repository.Save(_transactionToSave);
		}

		[Test]
		public void Should_save_the_transaction()
		{
			AssertUsing(session =>
			{
				var savedTransaction = new TransactionRepository(session).Get(_transactionToSave.Id);
				savedTransaction.ShouldBe(_transactionToSave);
				savedTransaction.Amount.ShouldBe(_transactionToSave.Amount);
				savedTransaction.CreatedDate.ShouldBe(_transactionToSave.CreatedDate);
				savedTransaction.Subscription.ShouldBe(_transactionToSave.Subscription);
				savedTransaction.Reason.ShouldBe(_transactionToSave.Reason);
				savedTransaction.Type.ShouldBe(_transactionToSave.Type);
			});
		}
	}

}
