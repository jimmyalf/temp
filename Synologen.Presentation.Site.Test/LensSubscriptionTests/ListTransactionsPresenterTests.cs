using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
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
			_transactionList = subscription.Transactions.ToList();
			Context = () => MockedTransactionRepository.Setup(x => x.FindBy(It.IsAny<TransactionsForSubscriptionMatchingCriteria>())).Returns(subscription.Transactions);	

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			Func<SubscriptionTransaction, SubscriptionTransactionListItemModel> transactionConverter = (transaction) =>
				new SubscriptionTransactionListItemModel
				{
					CreatedDate = transaction.CreatedDate.ToString("yyyy-MM-dd"),
					Amount = transaction.Amount,
					Reason = transaction.Reason.GetEnumDisplayName(),
					Type = transaction.Type.GetEnumDisplayName(),
					HasSettlement = (transaction.Settlement != null) ? "Ja" : String.Empty
				};

			var expectedtransactionListItems = _transactionList.Select(transactionConverter);

			AssertUsing( view =>
			{
				view.Model.List.Count().ShouldBe(_transactionList.Count());
				view.Model.HasTransactions.ShouldBe(true);
				view.Model.List.For((index, transactionListItem) =>
				{
					transactionListItem.Amount.ShouldBe(expectedtransactionListItems.ElementAt(index).Amount);
					transactionListItem.CreatedDate.ShouldBe(expectedtransactionListItems.ElementAt(index).CreatedDate);
					transactionListItem.Reason.ShouldBe(expectedtransactionListItems.ElementAt(index).Reason);
					transactionListItem.Type.ShouldBe(expectedtransactionListItems.ElementAt(index).Type);
					transactionListItem.HasSettlement.ShouldBe(expectedtransactionListItems.ElementAt(index).HasSettlement);
				});
			});

		}
	}
}
