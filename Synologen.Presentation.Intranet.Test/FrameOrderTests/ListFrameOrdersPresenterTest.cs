using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.FrameOrderTests
{
	[TestFixture, Category("ListFrameOrdersPresenterTests")]
	public class Given_a_ListFrameOrdersPresenter : AssertionHelper
	{
		private IListFrameOrdersView<ListFrameOrdersModel> _view;
		private ListFrameOrdersPresenter _presenter;
		private IFrameOrderRepository _frameOrderRepository;
		private ISynologenMemberService _synologenMemberService;
		private IRoutingService _routingService;

		[SetUp]
		public void Context()
		{
			_frameOrderRepository = RepositoryFactory.GetFramOrderRepository();
			_view = A.Fake<IListFrameOrdersView<ListFrameOrdersModel>>();
			_synologenMemberService = ServiceFactory.GetSynologenMemberService();
			_routingService = A.Fake<IRoutingService>();
			_presenter = new ListFrameOrdersPresenter(_view, _frameOrderRepository, _synologenMemberService, _routingService);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange
			var eventArgs = new EventArgs();
			const int expectedShopId = 5;
			var expectedFirstItem = _frameOrderRepository.Get(1);
			const int editPageId = 5;
			const string expectedViewRedirectUrl = "/test/url/";
			

			//Act
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedShopId(expectedShopId);
			A.CallTo(() => _routingService.GetPageUrl(A<int>.Ignored)).Returns(expectedViewRedirectUrl);
			_view.ViewPageId = editPageId;
			_presenter.View_Load(null, eventArgs);
			var criteria = (AllFrameOrdersForShopCriteria) ((RepositoryFactory.MockedFrameOrderRepository) _frameOrderRepository).GivenFindByActionCriteria;

			//Assert
			Expect(criteria.ShopId, Is.EqualTo(expectedShopId));
			Expect(_view.Model.List.Count(), Is.EqualTo(10));
			Expect(_view.Model.List.First().Id, Is.EqualTo(expectedFirstItem.Id));
			Expect(_view.Model.List.First().FrameName, Is.EqualTo(expectedFirstItem.Frame.Name));
			Expect(_view.Model.List.First().Sent, Is.EqualTo(null));
			Expect(_view.Model.ViewPageUrl, Is.EqualTo(expectedViewRedirectUrl));
		}

		[Test]
		public void When_Shop_Has_Slim_Jim_Access_Ensure_Model_Has_Expected_values()
		{
			//Arrange
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			((ServiceFactory.MockedSessionProviderService)_synologenMemberService).SetShopHasAccess(true);
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			_presenter.HttpContext = mockedHttpContext.Object;
			
			//Act
			_presenter.View_Load(null, new EventArgs());

			//Assert
			Expect(_view.Model.ShopDoesNotHaveAccessToFrameOrders, Is.False);
			Expect(_view.Model.DisplayList, Is.True);
		}

		[Test]
		public void When_Shop_Does_Not_Have_Slim_Jim_Access_Ensure_Model_Has_Expected_values()
		{
			//Arrange
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			((ServiceFactory.MockedSessionProviderService)_synologenMemberService).SetShopHasAccess(false);
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			_presenter.HttpContext = mockedHttpContext.Object;
			
			//Act
			_presenter.View_Load(null, new EventArgs());

			//Assert
			Expect(_view.Model.ShopDoesNotHaveAccessToFrameOrders, Is.True);
			Expect(_view.Model.DisplayList, Is.False);
		}
		
	}
}