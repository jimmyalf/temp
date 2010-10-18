using System.Linq;
using NHibernate;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Integration.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Integration.Test.LensSubscriptionData.Factories;

namespace Spinit.Wpc.Synologen.Integration.Test.LensSubscriptionData
{
	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_adding_a_subscription : BaseRepositoryTester<LensSubscriptionRepository>
	{
		private Subscription _subscriptionToSave;

		public When_adding_a_subscription()
		{
			Context = (ISession session) =>
			{
				var customer = CustomerFactory.Get();
				new LensSubscriptionCustomerRepository(session).Save(customer);
				_subscriptionToSave = SubscriptionFactory.Get(customer);
			};

			Because = (LensSubscriptionRepository repository) => repository.Save(_subscriptionToSave);
		}

		[Test]
		public void Should_save_the_subscription()
		{
			AssertUsing(session => 
			{
				var savedSubscription = new LensSubscriptionRepository(session).Get(_subscriptionToSave.Id);
				savedSubscription.ShouldBe(_subscriptionToSave);
				savedSubscription.ActivatedDate.ShouldBe(_subscriptionToSave.ActivatedDate);
				savedSubscription.CreatedDate.ShouldBe(_subscriptionToSave.CreatedDate);
				savedSubscription.Customer.ShouldBe(_subscriptionToSave.Customer);
				savedSubscription.PaymentInfo.ShouldBe(_subscriptionToSave.PaymentInfo);
				savedSubscription.Status.ShouldBe(_subscriptionToSave.Status);
				savedSubscription.Transactions.Count().ShouldBe(_subscriptionToSave.Transactions.Count());
			});
		}
	}

	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_editing_a_subscription : BaseRepositoryTester<LensSubscriptionRepository>
	{
		private Subscription _subscriptionToEdit;

		public When_editing_a_subscription()
		{
			Context = (ISession session) =>
			{
				var customer = CustomerFactory.Get();
				new LensSubscriptionCustomerRepository(session).Save(customer);
				var subscriptionToSave = SubscriptionFactory.Get(customer);
				new LensSubscriptionRepository(session).Save(subscriptionToSave);
				_subscriptionToEdit = SubscriptionFactory.Edit(subscriptionToSave);
			};

			Because = (LensSubscriptionRepository repository) => repository.Save(_subscriptionToEdit);
		}

		[Test]
		public void Should_edit_the_subscription()
		{
			AssertUsing(session => 
			{
				var fetchedSubscription = new LensSubscriptionRepository(session).Get(_subscriptionToEdit.Id);
				fetchedSubscription.ShouldBe(_subscriptionToEdit);
				fetchedSubscription.ShouldBe(_subscriptionToEdit);
				fetchedSubscription.ActivatedDate.ShouldBe(_subscriptionToEdit.ActivatedDate);
				fetchedSubscription.CreatedDate.ShouldBe(_subscriptionToEdit.CreatedDate);
				fetchedSubscription.Customer.ShouldBe(_subscriptionToEdit.Customer);
				fetchedSubscription.PaymentInfo.ShouldBe(_subscriptionToEdit.PaymentInfo);
				fetchedSubscription.Status.ShouldBe(_subscriptionToEdit.Status);
				fetchedSubscription.Transactions.Count().ShouldBe(_subscriptionToEdit.Transactions.Count());
			});
		}
	}

	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_deleting_a_subscription : BaseRepositoryTester<LensSubscriptionRepository>
	{
		private Subscription _subscriptionToDelete;

		public When_deleting_a_subscription()
		{
			Context = (ISession session) =>
			{
				var customer = CustomerFactory.Get();
				new LensSubscriptionCustomerRepository(session).Save(customer);
				_subscriptionToDelete = SubscriptionFactory.Get(customer);
				new LensSubscriptionRepository(session).Save(_subscriptionToDelete);
			};

			Because = (LensSubscriptionRepository repository) => repository.Delete(_subscriptionToDelete);
		}

		[Test]
		public void Should_delete_the_subscription()
		{
			AssertUsing(session => 
			{
				var fetchedSubscription = new LensSubscriptionRepository(session).Get(_subscriptionToDelete.Id);
				fetchedSubscription.ShouldBe(null);
			});
		}
	}
}