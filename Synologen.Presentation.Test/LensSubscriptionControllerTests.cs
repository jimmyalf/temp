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
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_subscription_list_action
	{
		private readonly SubscriptionListView _viewModel;
		private readonly Subscription[] _subscriptions;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;

		public When_loading_subscription_list_action()
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
			var view = (ViewResult) controller.Index(_gridPageSortParameters);
			_viewModel = (SubscriptionListView) view.ViewData.Model;
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			_viewModel.List.Count().ShouldBe(_subscriptions.Count());
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
		}
	}

	[TestFixture]
	[Category("LensSubscriptionControllerTests")]
	public class When_loading_subscription_list_action_with_sort_order_and_paging_selected
	{
		private readonly Subscription[] _subscriptions;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly GridPageSortParameters _gridPageSortParameters;
		private readonly int _defaultPageSize;

		public When_loading_subscription_list_action_with_sort_order_and_paging_selected()
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
			controller.Index(_gridPageSortParameters);
		}

		[Test]
		public void Controller_constructs_expected_criteria()
		{
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => Equals(criteria.OrderBy, _gridPageSortParameters.Column))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.Page.Equals(_gridPageSortParameters.Page))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.PageSize.Equals(_gridPageSortParameters.PageSize))));
			_mockedSubscriptionRepository.Verify(x => x.FindBy(It.Is<PageOfSubscriptionsMatchingCriteria>(criteria => criteria.SortAscending.Equals(_gridPageSortParameters.Direction == SortDirection.Ascending))));
		}
	}
}