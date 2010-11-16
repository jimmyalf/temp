using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("TransactionListPresenterTester")]
	public class When_loading_transaction_list_view
	{

		private readonly IListTransactionView _view;
		private readonly Mock<ISubscriptionRepository> _subscriptionRepository;
		private readonly SubscriptionTransaction[] _transactionList;

		public When_loading_transaction_list_view()
		{
			// Arrange
			const int customerId = 5;
			const int shopId = 5;
			const int subscriptionId = 5;
			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection { { "subscription", subscriptionId.ToString() } });

			var subscription = SubscriptionFactory.GetWithTransactions(CustomerFactory.Get(customerId, shopId));
			_transactionList = subscription.Transactions.ToArray();

			var view = new Mock<IListTransactionView>();
			view.SetupGet(x => x.Model).Returns(new ListTransactionModel());
			_view = view.Object;

			_subscriptionRepository = new Mock<ISubscriptionRepository>();
			_subscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscription);

			var presenter = new ListTransactionsPresenter(view.Object, _subscriptionRepository.Object) { HttpContext = mockedHttpContext.Object };

			//Act
			presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			Func<SubscriptionTransaction, SubscriptionTransactionListItemModel> transactionConverter = transaction =>
				new SubscriptionTransactionListItemModel
				{
					CreatedDate = transaction.CreatedDate.ToString("yyyy-MM-dd"),
					Amount = transaction.Amount,
					Reason = transaction.Reason.GetEnumDisplayName(),
					Type = transaction.Type.GetEnumDisplayName()
				};

			var  modelListItems = _transactionList.Select(transactionConverter);

			_view.Model.List.Count().ShouldBe(4);
			for (var i = 0; i < _transactionList.Length; i++)
			{
				_view.Model.List.ToArray()[i].Amount.ShouldBe(modelListItems.ToArray()[i].Amount);
				_view.Model.List.ToArray()[i].CreatedDate.ShouldBe(modelListItems.ToArray()[i].CreatedDate);
				_view.Model.List.ToArray()[i].Reason.ShouldBe(modelListItems.ToArray()[i].Reason);
				_view.Model.List.ToArray()[i].Type.ShouldBe(modelListItems.ToArray()[i].Type);
			}
		}


	}
}
