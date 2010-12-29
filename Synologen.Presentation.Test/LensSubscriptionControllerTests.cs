using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.ShouldlyExtensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Test.Factories;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_subscription_list : LensSubscriptionTestbase<SubscriptionListView>
	{
		private readonly IList<Subscription> _subscriptions;
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;

		public When_loading_subscription_list()
		{
			_defaultPageSize = 33;
			_subscriptions = SubscriptionFactory.GetList().ToList();

			Context = () => 
			{
				MockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
				MockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<PageOfSubscriptionsMatchingCriteria>())).Returns(_subscriptions);
			};
			
			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = null,
				Direction = SortDirection.Ascending,
				Page = 1,
				PageSize = null
			};
			Because = controller => controller.Index(null, _gridPageSortParameters);
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			ViewModel.List.Count().ShouldBe(_subscriptions.Count());
			ViewModel.SearchTerm.ShouldBe(null);
			ViewModel.List.For((index, subscription) =>
			{
				subscription.CustomerName.ShouldBe(_subscriptions.ElementAt(index).Customer.FirstName + " " + _subscriptions.ElementAt(index).Customer.LastName);
				subscription.ShopName.ShouldBe(_subscriptions.ElementAt(index).Customer.Shop.Name);
				subscription.Status.ShouldBe(_subscriptions.ElementAt(index).Status.GetEnumDisplayName());
				subscription.SubscriptionId.ShouldBe(_subscriptions.ElementAt(index).Id);
			});
		}

		[Test]
		public void Controller_constructs_expected_criteria()
		{
			MockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria =>
				Equals(criteria.OrderBy, _gridPageSortParameters.Column) &&
				Equals(criteria.Page, _gridPageSortParameters.Page) &&
				Equals(criteria.PageSize, _defaultPageSize) &&
				Equals(criteria.SortAscending, _gridPageSortParameters.Direction == SortDirection.Ascending) &&
				Equals(criteria.SearchTerm, null)
			)));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_subscription_list_with_sort_order_and_paging_selected : LensSubscriptionTestbase<SubscriptionListView>
	{
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;

		public When_loading_subscription_list_with_sort_order_and_paging_selected()
		{
			_defaultPageSize = 33;
			var subscriptions = SubscriptionFactory.GetList().ToArray();

			Context = () => 
			{
				MockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<PageOfSubscriptionsMatchingCriteria>())).Returns(subscriptions);
				MockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
			};

			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = "Customer.LastName",
				Direction = SortDirection.Ascending,
				Page = 2,
				PageSize = 10
			};

			Because = controller => controller.Index(null, _gridPageSortParameters);
		}

		[Test]
		public void Controller_constructs_expected_criteria()
		{
			MockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => 
				Equals(criteria.OrderBy, _gridPageSortParameters.Column) &&
				Equals(criteria.Page, _gridPageSortParameters.Page) &&
				Equals(criteria.PageSize, _gridPageSortParameters.PageSize) &&
				Equals(criteria.SortAscending, _gridPageSortParameters.Direction == SortDirection.Ascending) &&
				Equals(criteria.SearchTerm, null)
			)));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_searching_subscription_list : LensSubscriptionTestbase<SubscriptionListView>
	{
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;
		private readonly string _searchTerm;

		public When_searching_subscription_list()
		{
			_defaultPageSize = 33;
			_searchTerm = "Adam A";
			var subscriptions = SubscriptionFactory.GetList().ToArray();
			Context = () => 
			{
				MockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
				MockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<PageOfSubscriptionsMatchingCriteria>())).Returns(subscriptions);
			};

			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = null,
				Direction = SortDirection.Ascending,
				Page = 1,
				PageSize = null
			};
			Because = controller => controller.Index(_searchTerm, _gridPageSortParameters);
		}


		[Test]
		public void Controller_constructs_expected_criteria()
		{
			MockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => 
				Equals(criteria.OrderBy, _gridPageSortParameters.Column) &&
				Equals(criteria.Page, _gridPageSortParameters.Page) &&
				Equals(criteria.PageSize, _defaultPageSize) &&
				Equals(criteria.SortAscending, _gridPageSortParameters.Direction == SortDirection.Ascending) &&
				Equals(criteria.SearchTerm, _searchTerm)
			)));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_searching_subscription_list_with_non_english_letters : LensSubscriptionTestbase<SubscriptionListView>
	{
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;
		private readonly string _searchTerm;

		public When_searching_subscription_list_with_non_english_letters()
		{
			// Arrange
			_defaultPageSize = 33;
			_searchTerm = "едц";
			var encodedSearchTerm = _searchTerm.UrlEncode();
			var subscriptions = SubscriptionFactory.GetList().ToArray();

			Context = () =>
			{
				MockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
				MockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<PageOfSubscriptionsMatchingCriteria>())).Returns(subscriptions);
			};

			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = null,
				Direction = SortDirection.Ascending,
				Page = 1,
				PageSize = null
			};
			Because = controller => controller.Index(encodedSearchTerm, _gridPageSortParameters);
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			ViewModel.SearchTerm.ShouldBe(_searchTerm);
		}

		[Test]
		public void Controller_constructs_expected_criteria()
		{
			MockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.SearchTerm.Equals(_searchTerm))));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_subscription_view : LensSubscriptionTestbase<SubscriptionView>
	{
		private readonly Subscription _subscription;

		public When_loading_subscription_view()
		{
			int subscriptionId = 5;
			_subscription = SubscriptionFactory.GetFull(subscriptionId);

			Context = () => MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_subscription);

			Because = controller => controller.ViewSubscription(subscriptionId);
		}

		[Test]
		public void ViewModel_should_have_expected_customer_values()
		{
			var expectedPersonalIdNumber = String.Concat(
				_subscription.Customer.PersonalIdNumber.Substring(0, 8), "-", _subscription.Customer.PersonalIdNumber.Substring(8, 4));

		    ViewModel.Activated.ShouldBe(_subscription.ActivatedDate.Value.ToString("yyyy-MM-dd"));
		    ViewModel.AddressLineOne.ShouldBe(_subscription.Customer.Address.AddressLineOne);
		    ViewModel.AddressLineTwo.ShouldBe(_subscription.Customer.Address.AddressLineTwo);
		    ViewModel.City.ShouldBe(_subscription.Customer.Address.City);
		    ViewModel.Country.ShouldBe(_subscription.Customer.Address.Country.Name);
			ViewModel.PostalCode.ShouldBe(_subscription.Customer.Address.PostalCode);
			ViewModel.Email.ShouldBe(_subscription.Customer.Contact.Email);
			ViewModel.MobilePhone.ShouldBe(_subscription.Customer.Contact.MobilePhone);
			ViewModel.Phone.ShouldBe(_subscription.Customer.Contact.Phone);
			ViewModel.FirstName.ShouldBe("Adam");
			ViewModel.LastName.ShouldBe("Bertil");
			ViewModel.CustomerId.ShouldBe(_subscription.Customer.Id);
			ViewModel.PersonalIdNumber.ShouldBe(expectedPersonalIdNumber);
			ViewModel.ShopName.ShouldBe(_subscription.Customer.Shop.Name);
			ViewModel.AccountNumber.ShouldBe(_subscription.PaymentInfo.AccountNumber);
			ViewModel.ClearingNumber.ShouldBe(_subscription.PaymentInfo.ClearingNumber);
			ViewModel.MonthlyAmount.ShouldBe(_subscription.PaymentInfo.MonthlyAmount.ToString("F", new CultureInfo("sv-SE")));
			ViewModel.Status.ShouldBe(_subscription.Status.GetEnumDisplayName());
			ViewModel.CustomerNotes.ShouldBe(_subscription.Customer.Notes);
			ViewModel.SubscriptionNotes.ShouldBe(_subscription.Notes);
		}

		[Test]
		public void ViewModel_should_have_expected_transactions()
		{
			ViewModel.TransactionList.ForBoth(_subscription.Transactions.ToList(), (viewModelTransaction,transaction) =>
			{
				var transactionAmount = transaction.Amount.ToString("C2", new CultureInfo("sv-SE"));
				var invertedTransactionAmount = transaction.Amount.Invert().ToString("C2", new CultureInfo("sv-SE"));
				viewModelTransaction.DepositAmount.ShouldBe(transaction.Type.Equals(TransactionType.Deposit) ?  transactionAmount: String.Empty);
				viewModelTransaction.WithdrawalAmount.ShouldBe(transaction.Type.Equals(TransactionType.Withdrawal) ? invertedTransactionAmount : String.Empty);
				viewModelTransaction.Date.ShouldBe(transaction.CreatedDate.ToString("yyyy-MM-dd"));
				viewModelTransaction.Reason.ShouldBe(transaction.Reason.GetEnumDisplayName());
			});
		}

		[Test]
		public void ViewModel_should_have_expected_errors()
		{
			ViewModel.ErrorList.ForBoth(_subscription.Errors.ToList(), (viewModelError, error) =>
			{
				viewModelError.Type.ShouldBe(error.Type.GetEnumDisplayName());
				viewModelError.CreatedDate.ShouldBe(error.CreatedDate.ToString("yyyy-MM-dd"));
				viewModelError.HandledDate.ShouldBe(error.HandledDate.Return(x => x.Value.ToString("yyyy-MM-dd"), String.Empty));
			});
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_transaction_article_list : LensSubscriptionTestbase<TransactionArticleListView>
	{
		private int _defaultPageSize;
		private IList<TransactionArticle> _articles;
		private readonly GridPageSortParameters _gridPageSortParameters;

		public When_loading_transaction_article_list()
		{
			Context = () => 
			{
				_defaultPageSize = 33;
				_articles = TransactionArticleFactory.GetList();
				MockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
				MockedTransactionArticleRepository.Setup(x => x.FindBy(It.IsAny<PageOfTransactionArticlesMatchingCriteria>())).Returns(_articles);
			};
			
			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = null,
				Direction = SortDirection.Ascending,
				Page = 1,
				PageSize = null
			};
			Because = controller => controller.TransactionArticles(null, _gridPageSortParameters);
		}

		[Test]
		public void ViewModel_has_no_search_term()
		{

			ViewModel.SearchTerm.ShouldBe(null);
		}

		[Test]
		public void ViewModel_has_expected_articles()
		{
			ViewModel.Articles.ShouldBeSameLengthAs(_articles);
			ViewModel.SearchTerm.ShouldBe(null);
			ViewModel.Articles.ForBoth(_articles, (viewArticle, domainArticle) =>
			{
				viewArticle.ArticleId.ShouldBe(domainArticle.Id);
				viewArticle.Active.ShouldBe(domainArticle.Active);
				viewArticle.Name.ShouldBe(domainArticle.Name);
				viewArticle.NumberOfConnectedTransactions.ShouldBe(domainArticle.NumberOfConnectedTransactions);
				viewArticle.AllowDelete.ShouldBe(domainArticle.NumberOfConnectedTransactions == 0);
			});
		}

		[Test]
		public void Controller_constructs_expected_criteria_to_retrieve_articles()
		{
			MockedTransactionArticleRepository.Verify(x => x.FindBy(It.Is<PageOfTransactionArticlesMatchingCriteria>(criteria =>
				Equals(criteria.OrderBy, _gridPageSortParameters.Column) &&
				Equals(criteria.Page, _gridPageSortParameters.Page) &&
				Equals(criteria.PageSize, _defaultPageSize) &&
				Equals(criteria.SortAscending, _gridPageSortParameters.Direction == SortDirection.Ascending) &&
				Equals(criteria.SearchTerm, null)
			)));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_transaction_article_list_with_sort_order_and_paging_selected : LensSubscriptionTestbase<TransactionArticleListView>
	{

		private int _defaultPageSize;
		private IList<TransactionArticle> _articles;
		private readonly GridPageSortParameters _gridPageSortParameters;

		public When_loading_transaction_article_list_with_sort_order_and_paging_selected()
		{
			Context = () => 
			{
				_defaultPageSize = 33;
				_articles = TransactionArticleFactory.GetList();
				MockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
				MockedTransactionArticleRepository.Setup(x => x.FindBy(It.IsAny<PageOfTransactionArticlesMatchingCriteria>())).Returns(_articles);
			};
			
			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = "Id",
				Direction = SortDirection.Ascending,
				Page = 2,
				PageSize = 20
			};
			Because = controller => controller.TransactionArticles(null, _gridPageSortParameters);
		}

		[Test]
		public void Controller_constructs_expected_criteria()
		{
			MockedTransactionArticleRepository.Verify(x => x.FindBy(It.Is<PageOfTransactionArticlesMatchingCriteria>(criteria => 
				Equals(criteria.OrderBy, _gridPageSortParameters.Column) &&
				Equals(criteria.Page, _gridPageSortParameters.Page) &&
				Equals(criteria.PageSize, _gridPageSortParameters.PageSize) &&
				Equals(criteria.SortAscending, _gridPageSortParameters.Direction == SortDirection.Ascending) &&
				Equals(criteria.SearchTerm, null)
			)));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_searching_transaction_article_list : LensSubscriptionTestbase<TransactionArticleListView>
	{
		private readonly GridPageSortParameters _gridPageSortParameters;
		private int _defaultPageSize;
		private string _searchTerm;

		public When_searching_transaction_article_list()
		{
			Context = () => 
			{
				_defaultPageSize = 33;
				_searchTerm = "abc едц";
				MockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
			};

			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = null,
				Direction = SortDirection.Ascending,
				Page = 1,
				PageSize = null,
			};
			Because = controller =>
			{
				var encodedSearchTerm = _searchTerm.UrlEncode();
				return controller.TransactionArticles(encodedSearchTerm, _gridPageSortParameters);
			};
		}


		[Test]
		public void Controller_constructs_expected_criteria()
		{
			MockedTransactionArticleRepository.Verify(x => x.FindBy(It.Is<PageOfTransactionArticlesMatchingCriteria>(criteria => 
				Equals(criteria.OrderBy, _gridPageSortParameters.Column) &&
				Equals(criteria.Page, _gridPageSortParameters.Page) &&
				Equals(criteria.PageSize, _defaultPageSize) &&
				Equals(criteria.SortAscending, _gridPageSortParameters.Direction == SortDirection.Ascending) &&
				Equals(criteria.SearchTerm, _searchTerm)
			)));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_edit_transaction_article_view : LensSubscriptionTestbase<TransactionArticleModel>
	{
		private TransactionArticle _expectedTransactionArticle;

		public When_loading_edit_transaction_article_view()
		{
			Context = () =>
			{
				_expectedTransactionArticle = TransactionArticleFactory.Get(77);
				MockedTransactionArticleRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedTransactionArticle);
			};
			Because = controller => controller.EditTransactionArticle(_expectedTransactionArticle.Id);
		}

		[Test]
		public void ViewModel_has_expected_values()
		{
			ViewModel.Id.ShouldBe(_expectedTransactionArticle.Id);
			ViewModel.Name.ShouldBe(_expectedTransactionArticle.Name);
			ViewModel.Active.ShouldBe(_expectedTransactionArticle.Active);
			ViewModel.FormLegend.ShouldBe("Redigera transaktionsartikel");
		}

		[Test]
		public void Controller_fetches_expected_transaction_article_from_repository()
		{
			MockedTransactionArticleRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(_expectedTransactionArticle.Id))));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_posting_edit_transaction_article_view : LensSubscriptionTestbase<RedirectToRouteResult>
	{
		private TransactionArticleModel _expectedViewModel;
		private TransactionArticle _expectedTransactionArticle;
		private readonly string _expectedActionMessage;

		public When_posting_edit_transaction_article_view()
		{
			Context = () =>
			{
				_expectedViewModel = TransactionArticleFactory.GetViewModel(77);
				_expectedTransactionArticle = TransactionArticleFactory.Get(77);
				MockedTransactionArticleRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedTransactionArticle);
			};
			Because = controller => controller.EditTransactionArticle(_expectedViewModel);
			_expectedActionMessage = "Transaktionsartikeln har uppdaterats";
		}

		[Test]
		public void Controller_fetches_expected_item_to_update()
		{
			MockedTransactionArticleRepository.Verify(x => x.Get(It.Is<int>( id =>
				id.Equals(_expectedTransactionArticle.Id)
			)));
		}
		
		[Test]
		public void Controller_saves_item_with_expected_values()
		{
			MockedTransactionArticleRepository.Verify(x => x.Save(It.Is<TransactionArticle>( article =>
				article.Id.Equals(_expectedViewModel.Id) &&
				article.Name.Equals(_expectedViewModel.Name) && 
				article.Active.Equals(_expectedViewModel.Active)
			)));
		}

		[Test]
		public void Controller_redirects_to_list()
		{
			ViewModel.RouteValues["action"].ShouldBe("TransactionArticles");
		}

		[Test]
		public void Action_message_has_been_set()
		{
			ActionMessages.Count.ShouldBe(1);
			ActionMessages.First().Message.ShouldBe(_expectedActionMessage);
			ActionMessages.First().Type.ShouldBe(WpcActionMessageType.Success);
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_posting_edit_transaction_article_view_with_validation_errors : LensSubscriptionTestbase<TransactionArticleModel>
	{
		private TransactionArticleModel _expectedViewModel;

		public When_posting_edit_transaction_article_view_with_validation_errors()
		{
			Context = () =>
			{
				_expectedViewModel = TransactionArticleFactory.GetViewModel(77);
			};
			Because = controller =>
			{
				controller.ModelState.AddModelError("test","errorMessage");
				return controller.EditTransactionArticle(_expectedViewModel);
			};
		}

		[Test]
		public void Controller_returns_expected_invalid_model()
		{
			ViewModel.FormLegend.ShouldBe(_expectedViewModel.FormLegend);
			ViewModel.Active.ShouldBe(_expectedViewModel.Active);
			ViewModel.Id.ShouldBe(_expectedViewModel.Id);
			ViewModel.Name.ShouldBe(_expectedViewModel.Name);
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_add_transaction_article_view : LensSubscriptionTestbase<TransactionArticleModel>
	{
		public When_loading_add_transaction_article_view()
		{
			Context = () => {};
			Because = controller => controller.AddTransactionArticle();
		}

		[Test]
		public void ViewModel_has_expected_default_values()
		{
			ViewModel.Id.ShouldBe(0);
			ViewModel.Name.ShouldBe(null);
			ViewModel.Active.ShouldBe(true);
			ViewModel.FormLegend.ShouldBe("Skapa transaktionsartikel");
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_posting_add_transaction_article_view : LensSubscriptionTestbase<RedirectToRouteResult>
	{
		private TransactionArticleModel _expectedViewModel;
		private string _expectedActionMessage;

		public When_posting_add_transaction_article_view()
		{
			Context = () =>
			{
				_expectedActionMessage = "Transaktionsartikeln har sparats";
				_expectedViewModel = TransactionArticleFactory.GetViewModel(0);
			};
			Because = controller => controller.AddTransactionArticle(_expectedViewModel);
		}

		[Test]
		public void Controller_saves_item_with_expected_values()
		{
			MockedTransactionArticleRepository.Verify(x => x.Save(It.Is<TransactionArticle>( article =>
				article.Id.Equals(_expectedViewModel.Id) &&
				article.Name.Equals(_expectedViewModel.Name) && 
				article.Active.Equals(_expectedViewModel.Active)
			)));
		}

		[Test]
		public void Controller_redirects_to_list()
		{
			ViewModel.RouteValues["action"].ShouldBe("TransactionArticles");
		}

		[Test]
		public void Action_message_has_been_set()
		{
			ActionMessages.Count.ShouldBe(1);
			ActionMessages.First().Message.ShouldBe(_expectedActionMessage);
			ActionMessages.First().Type.ShouldBe(WpcActionMessageType.Success);
		}

	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_posting_add_transaction_article_view_with_validation_errors : LensSubscriptionTestbase<TransactionArticleModel>
	{
		private TransactionArticleModel _expectedViewModel;

		public When_posting_add_transaction_article_view_with_validation_errors()
		{
			Context = () =>
			{
				_expectedViewModel = TransactionArticleFactory.GetViewModel(0);
			};
			Because = controller =>
			{
				controller.ModelState.AddModelError("test","errorMessage");
				return controller.AddTransactionArticle(_expectedViewModel);
			};
		}

		[Test]
		public void Controller_returns_expected_invalid_model()
		{
			ViewModel.FormLegend.ShouldBe(_expectedViewModel.FormLegend);
			ViewModel.Active.ShouldBe(_expectedViewModel.Active);
			ViewModel.Id.ShouldBe(_expectedViewModel.Id);
			ViewModel.Name.ShouldBe(_expectedViewModel.Name);
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_posting_delete_transaction_article : LensSubscriptionTestbase<RedirectToRouteResult>
	{
		private TransactionArticle _expectedArticle;
		private string _expectedActionMessage;

		public When_posting_delete_transaction_article()
		{
			Context = () =>
			{
				_expectedActionMessage = "Transaktionsartikeln har raderats";
				_expectedArticle = TransactionArticleFactory.Get(77);
				MockedTransactionArticleRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedArticle);
			};
			
			Because = controller => controller.DeleteTransactionArticle(_expectedArticle.Id);
		}

		[Test]
		public void Controller_fetches_and_deletes_expected_transaction_article()
		{
			MockedTransactionArticleRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(_expectedArticle.Id))));
			MockedTransactionArticleRepository.Verify(x => x.Delete(It.Is<TransactionArticle>(article => article.Id.Equals(_expectedArticle.Id))));
		}

		[Test]
		public void Controller_redirects_to_list()
		{
			ViewModel.RouteValues["action"].ShouldBe("TransactionArticles");
		}

		[Test]
		public void Action_message_has_been_set()
		{
			ActionMessages.Count.ShouldBe(1);
			ActionMessages.First().Message.ShouldBe(_expectedActionMessage);
			ActionMessages.First().Type.ShouldBe(WpcActionMessageType.Success);
		}

	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_posting_delete_transaction_article_with_invalid_state : LensSubscriptionTestbase<RedirectToRouteResult>
	{
		private TransactionArticle _expectedArticle;
		private string _expectedActionMessage;

		public When_posting_delete_transaction_article_with_invalid_state()
		{
			Context = () =>
			{
				_expectedActionMessage = "Transaktionsartikeln kunde inte raderas";
				_expectedArticle = TransactionArticleFactory.Get(77);
			};
			
			Because = controller =>
			{
				controller.ModelState.AddModelError("key","errorMessage");
				return controller.DeleteTransactionArticle(_expectedArticle.Id);
			};
		}

		[Test]
		public void Controller_redirects_to_list()
		{
			ViewModel.RouteValues["action"].ShouldBe("TransactionArticles");
		}

		[Test]
		public void Action_error_message_has_been_set()
		{
			ActionMessages.Count.ShouldBe(1);
			ActionMessages.First().Message.ShouldBe(_expectedActionMessage);
			ActionMessages.First().Type.ShouldBe(WpcActionMessageType.Error);
		}

	}
	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_editing_subscription_view : LensSubscriptionTestbase<RedirectToRouteResult>
	{

		private readonly Subscription _subscription;
		private readonly SubscriptionView _savedSubscriptionView;
		private readonly string _expectedActionMessage; 

		public When_editing_subscription_view()
		{
			int subscriptionId = 5;
			int customerId = 4;
			_expectedActionMessage = "Abonnemanget har sparats";
			_subscription = SubscriptionFactory.GetFull(subscriptionId);
			_savedSubscriptionView = ViewModelFactory.GetSubscriptionView(subscriptionId, customerId);

			Context = () => MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_subscription);

			Because = controller => controller.Edit(_savedSubscriptionView, subscriptionId);
		}


		[Test]
		public void Saved_subscription_should_have_expected_values()
		{

			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.ActivatedDate.Equals(_subscription.ActivatedDate))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.CreatedDate.Equals(_subscription.CreatedDate))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Errors.Count().Equals(_subscription.Errors.Count()))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Notes.Equals(_savedSubscriptionView.SubscriptionNotes))));
			
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.PaymentInfo.AccountNumber.Equals(_savedSubscriptionView.AccountNumber))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.PaymentInfo.ClearingNumber.Equals(_savedSubscriptionView.ClearingNumber))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.PaymentInfo.MonthlyAmount.Equals(_savedSubscriptionView.MonthlyAmount.ToDecimalOrDefault()))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Status.Equals(_subscription.Status))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Transactions.Count().Equals(_subscription.Transactions.Count()))));

			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Address.AddressLineOne.Equals(_savedSubscriptionView.AddressLineOne))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Address.AddressLineTwo.Equals(_savedSubscriptionView.AddressLineTwo))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Address.City.Equals(_savedSubscriptionView.City))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Address.PostalCode.Equals(_savedSubscriptionView.PostalCode))));
	
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Contact.Email.Equals(_savedSubscriptionView.Email))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Contact.MobilePhone.Equals(_savedSubscriptionView.MobilePhone))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Contact.Phone.Equals(_savedSubscriptionView.Phone))));

			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.FirstName.Equals(_savedSubscriptionView.FirstName))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.LastName.Equals(_savedSubscriptionView.LastName))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Notes.Equals(_savedSubscriptionView.CustomerNotes))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.PersonalIdNumber.Equals(_savedSubscriptionView.PersonalIdNumber))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Shop.Equals(_subscription.Customer.Shop))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(y => y.Customer.Subscriptions.Equals(_subscription.Customer.Subscriptions))));

		}

		[Test]
		public void Controller_redirects_to_list()
		{
			ViewModel.RouteValues["action"].ShouldBe("Index");
		}

		[Test]
		public void Action_message_has_been_set()
		{
			ActionMessages.Count.ShouldBe(1);
			ActionMessages.First().Message.ShouldBe(_expectedActionMessage);
			ActionMessages.First().Type.ShouldBe(WpcActionMessageType.Success);
		}

	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_posting_edit_subscription_view_with_validation_errors : LensSubscriptionTestbase<SubscriptionView>
	{
		private SubscriptionView _expectedViewModel;
		private Subscription _subscription;

		public When_posting_edit_subscription_view_with_validation_errors()
		{
			const int subscriptionId = 5;
			const int customerId = 4;

			Context = () =>
			{
				_subscription = SubscriptionFactory.GetFull(subscriptionId);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_subscription);
				_expectedViewModel = ViewModelFactory.GetSubscriptionView(subscriptionId, customerId);
			};
			Because = controller =>
			{
				controller.ModelState.AddModelError("test", "errorMessage");
				return controller.Edit(_expectedViewModel, subscriptionId);
			};
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			ViewModel.Activated.ShouldBe(_subscription.ActivatedDate.Value.ToString("yyyy-MM-dd"));
			ViewModel.AddressLineOne.ShouldBe(_expectedViewModel.AddressLineOne);
			ViewModel.AddressLineTwo.ShouldBe(_expectedViewModel.AddressLineTwo);
			ViewModel.City.ShouldBe(_expectedViewModel.City);
			ViewModel.Country.ShouldBe(_subscription.Customer.Address.Country.Name);
			ViewModel.PostalCode.ShouldBe(_expectedViewModel.PostalCode);
			ViewModel.Email.ShouldBe(_expectedViewModel.Email);
			ViewModel.MobilePhone.ShouldBe(_expectedViewModel.MobilePhone);
			ViewModel.Phone.ShouldBe(_expectedViewModel.Phone);
			ViewModel.FirstName.ShouldBe(_expectedViewModel.FirstName);
			ViewModel.LastName.ShouldBe(_expectedViewModel.LastName);
			ViewModel.CustomerId.ShouldBe(_subscription.Customer.Id);
			ViewModel.PersonalIdNumber.ShouldBe(_expectedViewModel.PersonalIdNumber);
			ViewModel.AccountNumber.ShouldBe(_expectedViewModel.AccountNumber);
			ViewModel.ClearingNumber.ShouldBe(_expectedViewModel.ClearingNumber);
			ViewModel.MonthlyAmount.ShouldBe(_expectedViewModel.MonthlyAmount);
			ViewModel.Status.ShouldBe(_subscription.Status.GetEnumDisplayName());
			ViewModel.CustomerNotes.ShouldBe(_expectedViewModel.CustomerNotes);
			ViewModel.SubscriptionNotes.ShouldBe(_expectedViewModel.SubscriptionNotes);
		}

		[Test]
		public void ViewModel_should_have_expected_transactions()
		{
			ViewModel.TransactionList.ForBoth(_subscription.Transactions.ToList(), (viewModelTransaction, transaction) =>
			{
				var transactionAmount = transaction.Amount.ToString("C2", new CultureInfo("sv-SE"));
				var invertedTransactionAmount = transaction.Amount.Invert().ToString("C2", new CultureInfo("sv-SE"));
				viewModelTransaction.DepositAmount.ShouldBe(transaction.Type.Equals(TransactionType.Deposit) ? transactionAmount : String.Empty);
				viewModelTransaction.WithdrawalAmount.ShouldBe(transaction.Type.Equals(TransactionType.Withdrawal) ? invertedTransactionAmount : String.Empty);
				viewModelTransaction.Date.ShouldBe(transaction.CreatedDate.ToString("yyyy-MM-dd"));
				viewModelTransaction.Reason.ShouldBe(transaction.Reason.GetEnumDisplayName());
			});
		}

		[Test]
		public void ViewModel_should_have_expected_errors()
		{
			ViewModel.ErrorList.ForBoth(_subscription.Errors.ToList(), (viewModelError, error) =>
			{
				viewModelError.Type.ShouldBe(error.Type.GetEnumDisplayName());
				viewModelError.CreatedDate.ShouldBe(error.CreatedDate.ToString("yyyy-MM-dd"));
				viewModelError.HandledDate.ShouldBe(error.HandledDate.Return(x => x.Value.ToString("yyyy-MM-dd"), String.Empty));
			});
		}


	}

	
}