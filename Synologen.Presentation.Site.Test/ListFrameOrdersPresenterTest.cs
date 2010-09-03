using System;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using Spinit.Wpc.Synologen.Presentation.Site.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test
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
			Expect(view.Model.List.First().Sent, Is.EqualTo(expectedFirstItem.Sent.HasValue));
			Expect(view.Model.ViewPageUrl, Is.EqualTo(expectedViewRedirectUrl));
		}
		
	}
}