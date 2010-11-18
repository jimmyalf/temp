using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("TransactionListPresenterTester")]
	public class When_loading_transaction_list_view : ListTransactionsTestbase
	{
		private readonly IList<SubscriptionTransaction> _transactionList;

		public When_loading_transaction_list_view()
		{
			const int customerId = 5;
			const int shopId = 5;
			var subscription = SubscriptionFactory.GetWithTransactions(CustomerFactory.Get(customerId, shopId));
			_transactionList = subscription.Transactions.ToArray();

			Context = () => MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscription);	

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			AssertUsing( view =>
			{
				view.Model.List.Count().ShouldBe(_transactionList.Count());
				view.Model.HasTransactions.ShouldBe(true);
				view.Model.List.For((index, transaction) =>
				{
					transaction.Amount.ShouldBe(_transactionList.ElementAt(index).Amount);
					transaction.CreatedDate.ShouldBe(_transactionList.ElementAt(index).CreatedDate.ToString("yyyy-MM-dd"));
					transaction.Reason.ShouldBe(_transactionList.ElementAt(index).Reason.GetEnumDisplayName());
					transaction.Type.ShouldBe(_transactionList.ElementAt(index).Type.GetEnumDisplayName());
				});
			});

		}
	}
}
