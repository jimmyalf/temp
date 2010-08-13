using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	public class Given_A_FrameController : AssertionHelper
	{
		private IFrameRepository frameRepository;
		private IFrameColorRepository frameColorRepository;
		private IFrameBrandRepository frameBrandRepository;
		private FrameController controller;
		private ISettingsService settingsService;

		[SetUp]
		public void Context()
		{
			frameRepository = Factories.RepositoryFactory.GetFrameRepository();
			frameColorRepository = Factories.RepositoryFactory.GetFrameColorRepository();
			frameBrandRepository = Factories.RepositoryFactory.GetFrameBrandRepository();
			settingsService = Factories.ServiceFactory.GetSettingsService();
			controller = new FrameController(frameRepository, frameColorRepository, frameBrandRepository, settingsService);
		}

		[Test]
		public void When_Index_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			const string searchword = "TestSearchWord";
			var gridPageSortParameters = new GridPageSortParameters();

			//Act
			var result = (ViewResult) controller.Index(searchword, gridPageSortParameters);
			var model = (FrameListView) result.ViewData.Model;

			//Assert
			Expect(model.List.Count(), Is.EqualTo(10));
			Expect(model.SearchWord, Is.EqualTo(searchword));
		}

		[Test]
		public void When_Index_POST_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			const string searchword = "TestSearchWord";
			var viewModel = new FrameListView {SearchWord = searchword};
			var gridPageSortParameters = new GridPageSortParameters();

			//Act
			var result = (ViewResult) controller.Index(viewModel, gridPageSortParameters);
			var model = (FrameListView) result.ViewData.Model;

			//Assert
			Expect(model.List.Count(), Is.EqualTo(10));
			Expect(model.SearchWord, Is.EqualTo(searchword));
		}
	}
}