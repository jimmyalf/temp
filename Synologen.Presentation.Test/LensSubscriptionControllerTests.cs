using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;
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
		private readonly int _subscriptionId;
		private readonly Subscription _subscription;

		public When_loading_subscription_view()
		{
			_subscriptionId = 5;
			_subscription = SubscriptionFactory.GetFull(_subscriptionId);

			Context = () => MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_subscription);

			Because = controller => controller.ViewSubscription(_subscriptionId);
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
			ViewModel.CustomerName.ShouldBe(String.Concat(_subscription.Customer.FirstName," ",_subscription.Customer.LastName));
			ViewModel.PersonalIdNumber.ShouldBe(expectedPersonalIdNumber);
			ViewModel.ShopName.ShouldBe(_subscription.Customer.Shop.Name);
			ViewModel.AccountNumber.ShouldBe(_subscription.PaymentInfo.AccountNumber);
			ViewModel.ClearingNumber.ShouldBe(_subscription.PaymentInfo.ClearingNumber);
			ViewModel.MonthlyAmount.ShouldBe(_subscription.PaymentInfo.MonthlyAmount.ToString("C2", new CultureInfo("sv-SE")));
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

	
}