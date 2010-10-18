using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using Spinit.Wpc.Synologen.Presentation.Site.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.FrameOrderTests
{
	[TestFixture]
	public class Given_a_ListFrameOrdersPresenter : AssertionHelper
	{
		private IListFrameOrdersView<ListFrameOrdersModel> view;
		private ListFrameOrdersPresenter presenter;
		private IFrameOrderRepository frameOrderRepository;
		private ISynologenMemberService synologenMemberService;

		[SetUp]
		public void Context()
		{
			frameOrderRepository = RepositoryFactory.GetFramOrderRepository();
			view = ViewsFactory.GetListFrameOrdersPresenterView();
			synologenMemberService = ServiceFactory.GetSynologenMemberService();
			presenter = new ListFrameOrdersPresenter(view, frameOrderRepository, synologenMemberService);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange
			var eventArgs = new EventArgs();
			const int expectedShopId = 5;
			var expectedFirstItem = frameOrderRepository.Get(1);
			const int editPageId = 5;
			const string expectedViewRedirectUrl = "/test/url/";
			

			//Act
			((ServiceFactory.MockedSessionProviderService) synologenMemberService).SetMockedShopId(expectedShopId);
			((ServiceFactory.MockedSessionProviderService) synologenMemberService).SetMockedPageUrl(expectedViewRedirectUrl);
			view.ViewPageId = editPageId;
			presenter.View_Load(null, eventArgs);
			var criteria = (AllFrameOrdersForShopCriteria) ((RepositoryFactory.MockedFrameOrderRepository) frameOrderRepository).GivenFindByActionCriteria;

			//Assert
			Expect(criteria.ShopId, Is.EqualTo(expectedShopId));
			Expect(view.Model.List.Count(), Is.EqualTo(10));
			Expect(view.Model.List.First().Id, Is.EqualTo(expectedFirstItem.Id));
			Expect(view.Model.List.First().FrameName, Is.EqualTo(expectedFirstItem.Frame.Name));
			Expect(view.Model.List.First().Sent, Is.EqualTo(null));
			Expect(view.Model.ViewPageUrl, Is.EqualTo(expectedViewRedirectUrl));
		}

		[Test]
		public void When_Shop_Has_Slim_Jim_Access_Ensure_Model_Has_Expected_values()
		{
			//Arrange
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			((ServiceFactory.MockedSessionProviderService)synologenMemberService).SetShopHasAccess(true);
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			presenter.HttpContext = mockedHttpContext.Object;
			
			//Act
			presenter.View_Load(null, new EventArgs());

			//Assert
			Expect(view.Model.ShopDoesNotHaveAccessToFrameOrders, Is.False);
			Expect(view.Model.DisplayList, Is.True);
		}

		[Test]
		public void When_Shop_Does_Not_Have_Slim_Jim_Access_Ensure_Model_Has_Expected_values()
		{
			//Arrange
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			((ServiceFactory.MockedSessionProviderService)synologenMemberService).SetShopHasAccess(false);
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			presenter.HttpContext = mockedHttpContext.Object;
			
			//Act
			presenter.View_Load(null, new EventArgs());

			//Assert
			Expect(view.Model.ShopDoesNotHaveAccessToFrameOrders, Is.True);
			Expect(view.Model.DisplayList, Is.False);
		}
		
	}
}