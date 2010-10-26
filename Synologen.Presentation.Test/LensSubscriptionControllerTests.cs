using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_loading_subscription_list_action
	{
		private readonly SubscriptionListView _viewModel;
		private readonly Subscription[] _subscriptions;

		public When_loading_subscription_list_action()
		{
			// Arrange
			_subscriptions = SubscriptionFactory.GetList().ToArray();
			var mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			mockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<PageOfSubscriptionsMatchingCriteria>())).Returns(_subscriptions);
			var viewService = new LensSubscriptionViewService(mockedSubscriptionRepository.Object);
			
			var controller = new LensSubscriptionController(viewService);

			//Act
			var view = (ViewResult) controller.Index();
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
	}
}