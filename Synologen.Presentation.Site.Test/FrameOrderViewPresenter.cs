using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using Spinit.Wpc.Synologen.Presentation.Site.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test
{

	[TestFixture]
	public class Given_a_FrameOrderViewPresenter : AssertionHelper
	{
		private ViewFrameOrderPresenter presenter;
		private IViewFrameOrderView<ViewFrameOrderModel> view;
		//private IFrameRepository frameRepository;
		//private IFrameGlassTypeRepository frameGlassTypeRepository;
		//private IFrameOrderRepository frameOrderRepository;
		//private IFrameOrderSettingsService frameOrderSettingsService;
		//private ISynologenMemberService synologenMemberService;
		//private IShopRepository shopRepository;

		[SetUp]
		public void Context()
		{
			//frameRepository = RepositoryFactory.GetFrameRepository();
			//frameGlassTypeRepository = RepositoryFactory.GetFrameGlassRepository();
			//frameOrderRepository = RepositoryFactory.GetFramOrderRepository();
			//shopRepository = RepositoryFactory.GetShopRepository();
			//frameOrderSettingsService = ServiceFactory.GetFrameOrderSettingsService();
			//synologenMemberService = ServiceFactory.GetSessionProviderService();
			view = ViewsFactory.GetViewFrameOrderView();
			presenter = new ViewFrameOrderPresenter(view);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange
			var eventArgs = new EventArgs();

			//Act
			presenter.View_Load(null, eventArgs);

			//Assert
		}


		[Test]
		public void When_Form_Is_Submitted_Saved_Item_Has_Expected_Values()
		{
			//Arrange
			var eventArgs = new EventArgs();

			//Act
			presenter.View_Load(null, eventArgs);

			//Assert
		}
	}
}