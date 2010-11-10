using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_subscription_list
	{
		private readonly SubscriptionListView _viewModel;
		private readonly Subscription[] _subscriptions;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;

		public When_loading_subscription_list()
		{
			// Arrange
			_defaultPageSize = 33;
			_subscriptions = SubscriptionFactory.GetList().ToArray();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			var mockedAdminSettingsService = new Mock<IAdminSettingsService>();
			mockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
			_mockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<PageOfSubscriptionsMatchingCriteria>())).Returns(_subscriptions);
			var viewService = new LensSubscriptionViewService(_mockedSubscriptionRepository.Object);
			
			var controller = new LensSubscriptionController(viewService, mockedAdminSettingsService.Object);

			//Act
			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = null,
				Direction = SortDirection.Ascending,
				Page = 1,
				PageSize = null
			};
			var view = (ViewResult) controller.Index(null, _gridPageSortParameters);
			_viewModel = (SubscriptionListView) view.ViewData.Model;
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			_viewModel.List.Count().ShouldBe(_subscriptions.Count());
			_viewModel.SearchTerm.ShouldBe(null);
			var firstItem = _viewModel.List.First();
			firstItem.CustomerName.ShouldBe(_subscriptions[0].Customer.FirstName + " " + _subscriptions[0].Customer.LastName);
			firstItem.ShopName.ShouldBe(_subscriptions[0].Customer.Shop.Name);
			firstItem.Status.ShouldBe(_subscriptions[0].Status.GetEnumDisplayName());
			firstItem.SubscriptionId.ShouldBe(_subscriptions[0].Id);
		}

		[Test]
		public void Controller_constructs_expected_criteria()
		{
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => Equals(criteria.OrderBy, _gridPageSortParameters.Column))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.Page.Equals(_gridPageSortParameters.Page))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.PageSize.Equals(_defaultPageSize))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.SortAscending.Equals(_gridPageSortParameters.Direction == SortDirection.Ascending))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => Equals(criteria.SearchTerm, null))));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_subscription_list_with_sort_order_and_paging_selected
	{
		private readonly Subscription[] _subscriptions;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;

		public When_loading_subscription_list_with_sort_order_and_paging_selected()
		{
			// Arrange
			_defaultPageSize = 33;
			_subscriptions = SubscriptionFactory.GetList().ToArray();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<PageOfSubscriptionsMatchingCriteria>())).Returns(_subscriptions);

			var mockedAdminSettingsService = new Mock<IAdminSettingsService>();
			mockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
			var viewService = new LensSubscriptionViewService(_mockedSubscriptionRepository.Object);
			
			var controller = new LensSubscriptionController(viewService, mockedAdminSettingsService.Object);

			//Act
			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = "Customer.LastName",
				Direction = SortDirection.Ascending,
				Page = 2,
				PageSize = 10
			};
			controller.Index(null, _gridPageSortParameters);
		}

		[Test]
		public void Controller_constructs_expected_criteria()
		{
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => Equals(criteria.OrderBy, _gridPageSortParameters.Column))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.Page.Equals(_gridPageSortParameters.Page))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.PageSize.Equals(_gridPageSortParameters.PageSize))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.SortAscending.Equals(_gridPageSortParameters.Direction == SortDirection.Ascending))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => Equals(criteria.SearchTerm, null))));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_searching_subscription_list
	{
		private readonly SubscriptionListView _viewModel;
		private readonly Subscription[] _subscriptions;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;
		private readonly string _searchTerm;

		public When_searching_subscription_list()
		{
			// Arrange
			_defaultPageSize = 33;
			_searchTerm = "Adam A";
			_subscriptions = SubscriptionFactory.GetList().ToArray();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			var mockedAdminSettingsService = new Mock<IAdminSettingsService>();
			mockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
			_mockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<PageOfSubscriptionsMatchingCriteria>())).Returns(_subscriptions);
			var viewService = new LensSubscriptionViewService(_mockedSubscriptionRepository.Object);
			
			var controller = new LensSubscriptionController(viewService, mockedAdminSettingsService.Object);

			//Act
			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = null,
				Direction = SortDirection.Ascending,
				Page = 1,
				PageSize = null
			};
			var view = (ViewResult) controller.Index(_searchTerm, _gridPageSortParameters);
			_viewModel = (SubscriptionListView) view.ViewData.Model;
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			_viewModel.List.Count().ShouldBe(_subscriptions.Count());
			_viewModel.SearchTerm.ShouldBe(_searchTerm);
			var firstItem = _viewModel.List.First();
			firstItem.CustomerName.ShouldBe(_subscriptions[0].Customer.FirstName + " " + _subscriptions[0].Customer.LastName);
			firstItem.ShopName.ShouldBe(_subscriptions[0].Customer.Shop.Name);
			firstItem.Status.ShouldBe(_subscriptions[0].Status.GetEnumDisplayName());
			firstItem.SubscriptionId.ShouldBe(_subscriptions[0].Id);
		}

		[Test]
		public void Controller_constructs_expected_criteria()
		{
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => Equals(criteria.OrderBy, _gridPageSortParameters.Column))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.Page.Equals(_gridPageSortParameters.Page))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.PageSize.Equals(_defaultPageSize))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.SortAscending.Equals(_gridPageSortParameters.Direction == SortDirection.Ascending))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.SearchTerm.Equals(_searchTerm))));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_searching_subscription_list_with_non_english_letters
	{
		private readonly SubscriptionListView _viewModel;
		private readonly Subscription[] _subscriptions;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;
		private readonly string _searchTerm;

		public When_searching_subscription_list_with_non_english_letters()
		{
			// Arrange
			_defaultPageSize = 33;
			_searchTerm = "едц";
			var encodedSearchTerm = _searchTerm.UrlEncode();
			_subscriptions = SubscriptionFactory.GetList().ToArray();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			var mockedAdminSettingsService = new Mock<IAdminSettingsService>();
			mockedAdminSettingsService.Setup(x => x.GetDefaultPageSize()).Returns(_defaultPageSize);
			_mockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<PageOfSubscriptionsMatchingCriteria>())).Returns(_subscriptions);
			var viewService = new LensSubscriptionViewService(_mockedSubscriptionRepository.Object);
			
			var controller = new LensSubscriptionController(viewService, mockedAdminSettingsService.Object);

			//Act
			_gridPageSortParameters = new GridPageSortParameters
			{
				Column = null,
				Direction = SortDirection.Ascending,
				Page = 1,
				PageSize = null
			};
			var view = (ViewResult) controller.Index(encodedSearchTerm, _gridPageSortParameters);
			_viewModel = (SubscriptionListView) view.ViewData.Model;
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			_viewModel.SearchTerm.ShouldBe(_searchTerm);
		}

		[Test]
		public void Controller_constructs_expected_criteria()
		{
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.SearchTerm.Equals(_searchTerm))));
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_subscription_view
	{
		private readonly int _subscriptionId;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly SubscriptionView _viewModel;
		private readonly Subscription _subscription;

		public When_loading_subscription_view()
		{
			// Arrange
			_subscriptionId = 5;
			_subscription = SubscriptionFactory.GetFull(_subscriptionId);
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_subscription);
			var mockedAdminSettingsService = new Mock<IAdminSettingsService>();
			var viewService = new LensSubscriptionViewService(_mockedSubscriptionRepository.Object);
			
			var controller = new LensSubscriptionController(viewService, mockedAdminSettingsService.Object);

			var view = (ViewResult) controller.ViewSubscription(_subscriptionId);
			_viewModel = (SubscriptionView) view.ViewData.Model;
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			var expectedPersonalIdNumber = String.Concat(_subscription.Customer.PersonalIdNumber.Substring(0, 8), "-",
			                                             _subscription.Customer.PersonalIdNumber.Substring(8, 4));

		    _viewModel.Activated.ShouldBe(_subscription.ActivatedDate.Value.ToString("yyyy-MM-dd"));
		    _viewModel.AddressLineOne.ShouldBe(_subscription.Customer.Address.AddressLineOne);
		    _viewModel.AddressLineTwo.ShouldBe(_subscription.Customer.Address.AddressLineTwo);
		    _viewModel.City.ShouldBe(_subscription.Customer.Address.City);
		    _viewModel.Country.ShouldBe(_subscription.Customer.Address.Country.Name);
			_viewModel.PostalCode.ShouldBe(_subscription.Customer.Address.PostalCode);
			_viewModel.Email.ShouldBe(_subscription.Customer.Contact.Email);
			_viewModel.MobilePhone.ShouldBe(_subscription.Customer.Contact.MobilePhone);
			_viewModel.Phone.ShouldBe(_subscription.Customer.Contact.Phone);
			_viewModel.CustomerName.ShouldBe(String.Concat(_subscription.Customer.FirstName," ",_subscription.Customer.LastName));
			_viewModel.PersonalIdNumber.ShouldBe(expectedPersonalIdNumber);
			_viewModel.ShopName.ShouldBe(_subscription.Customer.Shop.Name);
			_viewModel.AccountNumber.ShouldBe(_subscription.PaymentInfo.AccountNumber);
			_viewModel.ClearingNumber.ShouldBe(_subscription.PaymentInfo.ClearingNumber);
			_viewModel.MonthlyAmount.ShouldBe(_subscription.PaymentInfo.MonthlyAmount.ToString("C2", new CultureInfo("sv-SE")));
			_viewModel.Status.ShouldBe(_subscription.Status.GetEnumDisplayName());
			_subscription.Transactions.For((index, transaction) =>
			{
				if(transaction.Type.Equals(TransactionType.Deposit))
				{
					_viewModel.TransactionList.ElementAt(index).DepositAmount.ShouldBe(transaction.Amount.ToString("C2", new CultureInfo("sv-SE")));	
				}
				else
				{
					_viewModel.TransactionList.ElementAt(index).WithdrawalAmount.ShouldBe(transaction.Amount.Invert().ToString("C2", new CultureInfo("sv-SE")));					
				}
				_viewModel.TransactionList.ElementAt(index).Date.ShouldBe(transaction.CreatedDate.ToString("yyyy-MM-dd"));
				_viewModel.TransactionList.ElementAt(index).Reason.ShouldBe(transaction.Reason.GetEnumDisplayName());
			});
			_subscription.Errors.For((index, error) =>
         		{
         			_viewModel.ErrorList.ElementAt(index).Type.ShouldBe(error.Type.GetEnumDisplayName());
					_viewModel.ErrorList.ElementAt(index).CreatedDate.ShouldBe(error.CreatedDate.ToString("yyyy-MM-dd"));
					if (error.HandledDate.HasValue)
						_viewModel.ErrorList.ElementAt(index).HandledDate.ShouldBe(error.HandledDate.Value.ToString("yyyy-MM-dd"));
					else
						_viewModel.ErrorList.ElementAt(index).HandledDate.ShouldBe(string.Empty);
         		}
			);
		}
	}

	
}