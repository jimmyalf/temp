using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Site.Models.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Site.Test.AssertionHelpers;
using Spinit.Wpc.Synologen.Presentation.Site.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test
{
	[TestFixture]
	public class Given_a_EditFrameOrderPresenter : WpcAssertionHelper
	{
		private EditFrameOrderPresenter presenter;
		private IEditFrameOrderView<EditFrameOrderModel> view;
		private IFrameRepository frameRepository;
		private IFrameGlassTypeRepository frameGlassTypeRepository;
		private IFrameOrderRepository frameOrderRepository;
		private IFrameOrderService frameOrderService;
		private ISynologenMemberService synologenMemberService;
		private IShopRepository shopRepository;
		private readonly Func<EyeParameter, EyeParameter, bool> EyeparameterEquality = (objectOne, objectTwo) => (objectOne.Left == objectTwo.Left && objectOne.Right == objectTwo.Right);
		private readonly Func<EyeParameter, NullableEyeParameter, bool> NullableEyeparameterEquality = (objectOne, objectTwo) => (objectOne.Left == objectTwo.Left && objectOne.Right == objectTwo.Right) || (objectOne.Left == int.MinValue && objectTwo.Left == null && objectOne.Right == int.MinValue && objectTwo.Right == null);
		private ISynologenSettingsService synologenSettingsService;

		[SetUp]
		public void Context()
		{
			frameRepository = RepositoryFactory.GetFrameRepository();
			frameGlassTypeRepository = RepositoryFactory.GetFrameGlassRepository();
			frameOrderRepository = RepositoryFactory.GetFramOrderRepository();
			shopRepository = RepositoryFactory.GetShopRepository();
			frameOrderService = ServiceFactory.GetFrameOrderSettingsService();
			synologenSettingsService = ServiceFactory.GetSynologenSettingsService();
			synologenMemberService = ServiceFactory.GetSynologenMemberService();
			view = ViewsFactory.GetFrameOrderView();
			presenter = new EditFrameOrderPresenter(view, frameRepository, frameGlassTypeRepository, frameOrderRepository, shopRepository, synologenMemberService, frameOrderService, synologenSettingsService);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange
			const int expectedNumberOfFrames = 11;
			const int expectedNumberOfPDs = 1;
			const int expectedNumberOfSpheres = 50;
			const int expectedNumberOfCylinders = 10;
			const int expectedNumberOfGlassTypes = 11;
			const int expectedNumberOfAdditions = 1;
			const int expectedNumberOfHeights = 1;
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			
			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			presenter.HttpContext = mockedHttpContext.Object;
			presenter.View_Load(null, new EventArgs());

			//Assert
			Expect(view.Model.SelectedFrameId, Is.EqualTo(0));
			Expect(view.Model.SelectedGlassTypeId, Is.EqualTo(0));
			Expect(view.Model.PupillaryDistance.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.PupillaryDistance.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.Sphere.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.Sphere.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.Cylinder.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.Cylinder.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.AxisSelectionLeft, Is.EqualTo(0));
			Expect(view.Model.AxisSelectionRight, Is.EqualTo(0));
			Expect(view.Model.Addition.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.Addition.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.Height.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.Height.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.FramesList.Count(), Is.EqualTo(expectedNumberOfFrames));
			Expect(view.Model.FramesList.First().Id, Is.EqualTo(0));
			Expect(view.Model.FramesList.First().Name, Is.EqualTo("-- Välj båge --"));
			Expect(view.Model.FrameRequiredErrorMessage, Is.EqualTo("Båge saknas"));
			Expect(view.Model.GlassTypesList.Count(), Is.EqualTo(expectedNumberOfGlassTypes));
			Expect(view.Model.GlassTypesList.First().Id, Is.EqualTo(0));
			Expect(view.Model.GlassTypesList.First().Name, Is.EqualTo("-- Välj glastyp --"));
			Expect(view.Model.GlassTypeRequiredErrorMessage, Is.EqualTo("Glastyp saknas"));
			Expect(view.Model.PupillaryDistance.List.Count(), Is.EqualTo(expectedNumberOfPDs));
			Expect(view.Model.PupillaryDistance.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(view.Model.PupillaryDistance.List.First().Name, Is.EqualTo("-- Välj PD --"));
			Expect(view.Model.PupillaryDistanceRequiredErrorMessage, Is.EqualTo("PD saknas"));
			Expect(view.Model.Sphere.List.Count(), Is.EqualTo(expectedNumberOfSpheres));
			Expect(view.Model.Sphere.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(view.Model.Sphere.List.First().Name, Is.EqualTo("-- Välj Sfär --"));
			Expect(view.Model.SphereRequiredErrorMessage, Is.EqualTo("Sfär saknas"));
			Expect(view.Model.Cylinder.List.Count(), Is.EqualTo(expectedNumberOfCylinders));
			Expect(view.Model.Cylinder.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(view.Model.Cylinder.List.First().Name, Is.EqualTo("-- Välj Cylinder --"));
			Expect(view.Model.CylinderRequiredErrorMessage, Is.EqualTo("Cylinder saknas"));
			Expect(view.Model.Addition.List.Count(), Is.EqualTo(expectedNumberOfAdditions));
			Expect(view.Model.Addition.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(view.Model.Addition.List.First().Name, Is.EqualTo("-- Välj Addition --"));
			Expect(view.Model.AdditionRequiredErrorMessage, Is.EqualTo("Addition saknas"));
			Expect(view.Model.Height.List.Count(), Is.EqualTo(expectedNumberOfHeights));
			Expect(view.Model.Height.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(view.Model.Height.List.First().Name, Is.EqualTo("-- Välj Höjd --"));
			Expect(view.Model.HeightRequiredMessage, Is.EqualTo("Höjd saknas"));
			Expect(view.Model.AxisRequiredMessage, Is.EqualTo("Axel saknas"));
			Expect(view.Model.AxisRangeMessage, Is.EqualTo("Axel anges som ett heltal i intervallet 0-180"));
			Expect(view.Model.NotSelectedIntervalValue, Is.EqualTo(int.MinValue));
			Expect(view.Model.HeightParametersEnabled, Is.False);
			Expect(view.Model.AdditionParametersEnabled, Is.False);
			Expect(view.Model.Reference, Is.Null);
		}

		[Test]
		public void When_View_Is_Loaded_With_Saved_FrameOrder_Model_Has_Expected_Values()
		{
			//Arrange
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection {{"frameorder", "5"}};
			var expectedFrameOrder = frameOrderRepository.Get(5);
			const int expectedShopId = 10;
			
			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			presenter.HttpContext = mockedHttpContext.Object;
			((ServiceFactory.MockedSessionProviderService) synologenMemberService).SetMockedShopId(expectedShopId);
			presenter.View_Load(null, new EventArgs());

			//Assert
			Expect(view.Model.SelectedFrameId, Is.EqualTo(expectedFrameOrder.Frame.Id));
			Expect(view.Model.SelectedGlassTypeId, Is.EqualTo(expectedFrameOrder.GlassType.Id));
			ExpectEqual(view.Model.PupillaryDistance.Selection, expectedFrameOrder.PupillaryDistance, EyeparameterEquality);
			ExpectEqual(view.Model.Sphere.Selection, expectedFrameOrder.Sphere, EyeparameterEquality);
			ExpectEqual(view.Model.Cylinder.Selection, expectedFrameOrder.Cylinder, EyeparameterEquality);
			Expect(view.Model.AxisSelectionLeft, Is.EqualTo(expectedFrameOrder.Axis.Left));
			Expect(view.Model.AxisSelectionRight, Is.EqualTo(expectedFrameOrder.Axis.Right));
			ExpectEqual(view.Model.Height.Selection, expectedFrameOrder.Height, NullableEyeparameterEquality);
			ExpectEqual(view.Model.Addition.Selection, expectedFrameOrder.Addition, NullableEyeparameterEquality);
			Expect(view.Model.HeightParametersEnabled, Is.EqualTo(expectedFrameOrder.GlassType.IncludeHeightParametersInOrder));
			Expect(view.Model.AdditionParametersEnabled, Is.EqualTo(expectedFrameOrder.GlassType.IncludeAdditionParametersInOrder));
			Expect(view.Model.Reference, Is.EqualTo(expectedFrameOrder.Reference));
			Expect(view.Model.OrderHasBeenSent, Is.EqualTo(expectedFrameOrder.Sent.HasValue));
			Expect(view.Model.UserDoesNotHaveAccessToThisOrder, Is.EqualTo(expectedFrameOrder.OrderingShop.Id != expectedShopId));
			Expect(view.Model.OrderDoesNotExist, Is.EqualTo(expectedFrameOrder == null));
		}

		[Test]
		public void When_Model_Is_Bound_Selected_Model_Has_Expected_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new EditFrameFormEventArgs
			{
				SelectedFrameId = 5, 
				SelectedGlassTypeId = 8,  // Returns a glasstype with both addition and height
				SelectedPupillaryDistance = new EyeParameter{Left = 22, Right = 33},
				SelectedSphere = new EyeParameter{Left = -5, Right = 2.25M},
                SelectedCylinder = new EyeParameter{ Left = 0.25M, Right = 1.75M}, 
                SelectedAxisLeft = 20,
				SelectedAxisRight = 179,
				SelectedAddition = new EyeParameter{Left = 1.25M, Right = 2.75M},
				SelectedHeight = new EyeParameter{Left = 19, Right = 27},
				Reference = "Skynda på"
			};
			const int expectedNumberOfFramesInList = 11;
			const int expectedNumberOfGlassTypesInList = 11;
			const int expectedNumberOfPDsInList = 22;
			const int expectedNumberOfSpheresInList = 50;
			const int expectedNumberOfCylindersInList = 10;
			const int expectedNumberOfAdditionsInList = 10;
			const int expectedNumberOfHeightsInList = 12;
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			
			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			presenter.HttpContext = mockedHttpContext.Object;
			presenter.View_Load(null, new EventArgs());
			presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(view.Model.PupillaryDistance.List.Count(), Is.EqualTo(expectedNumberOfPDsInList));
			Expect(view.Model.Sphere.List.Count(), Is.EqualTo(expectedNumberOfSpheresInList));
			Expect(view.Model.Cylinder.List.Count(), Is.EqualTo(expectedNumberOfCylindersInList));
			Expect(view.Model.Addition.List.Count(), Is.EqualTo(expectedNumberOfAdditionsInList));
			Expect(view.Model.Height.List.Count(), Is.EqualTo(expectedNumberOfHeightsInList));
			Expect(view.Model.GlassTypesList.Count(), Is.EqualTo(expectedNumberOfGlassTypesInList));
			Expect(view.Model.FramesList.Count(), Is.EqualTo(expectedNumberOfFramesInList));

			Expect(view.Model.SelectedFrameId, Is.EqualTo(frameSelectedEventArgs.SelectedFrameId));
			Expect(view.Model.SelectedGlassTypeId, Is.EqualTo(frameSelectedEventArgs.SelectedGlassTypeId));
			Expect(view.Model.PupillaryDistance.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedPupillaryDistance.Left));
			Expect(view.Model.PupillaryDistance.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedPupillaryDistance.Right));
			Expect(view.Model.Sphere.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedSphere.Left));
			Expect(view.Model.Sphere.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedSphere.Right));
			Expect(view.Model.Cylinder.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedCylinder.Left));
			Expect(view.Model.Cylinder.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedCylinder.Right));
			Expect(view.Model.AxisSelectionLeft, Is.EqualTo(frameSelectedEventArgs.SelectedAxisLeft));
			Expect(view.Model.AxisSelectionRight, Is.EqualTo(frameSelectedEventArgs.SelectedAxisRight));
			Expect(view.Model.Addition.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Left));
			Expect(view.Model.Addition.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Right));
			Expect(view.Model.Height.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedHeight.Left));
			Expect(view.Model.Height.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedHeight.Right));
			Expect(view.Model.HeightParametersEnabled, Is.True);
			Expect(view.Model.AdditionParametersEnabled, Is.True);
			Expect(view.Model.Reference, Is.EqualTo(frameSelectedEventArgs.Reference));
		}

		[Test]
		public void When_Model_Is_Bound_Without_Addition_Or_Height_Selected_Model_Has_Expected_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new EditFrameFormEventArgs
			{
				SelectedFrameId = 5, 
				SelectedGlassTypeId = 1, // Returns a glasstype without addition or height
				SelectedPupillaryDistance = new EyeParameter{Left = 22, Right = 33},
				SelectedSphere = new EyeParameter{Left = -5, Right = 2.25M},
                SelectedCylinder = new EyeParameter{ Left = 0.25M, Right = 1.75M}, 
                SelectedAxisLeft = 20,
				SelectedAxisRight = 179,
				SelectedAddition = new EyeParameter{Left = 1.25M, Right = 2.75M},
				SelectedHeight = new EyeParameter{Left = 19, Right = 27},
			};
			const int expectedNumberOfAdditionsInList = 1;
			const int expectedNumberOfHeightsInList = 1;
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			
			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			presenter.HttpContext = mockedHttpContext.Object;
			presenter.View_Load(null, new EventArgs());
			presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(view.Model.Addition.List.Count(), Is.EqualTo(expectedNumberOfAdditionsInList));
			Expect(view.Model.Height.List.Count(), Is.EqualTo(expectedNumberOfHeightsInList));
			Expect(view.Model.Height.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.Height.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.HeightParametersEnabled, Is.False);
			Expect(view.Model.AdditionParametersEnabled, Is.False);
		}

		[Test]
		public void When_Model_Is_Bound_Model_With_Invalid_Parameters_Get_Default_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new EditFrameFormEventArgs
			{
				SelectedFrameId = 1, 
				SelectedGlassTypeId = 8,
				SelectedPupillaryDistance = new EyeParameter{Left = 200, Right = -20},
				SelectedSphere = new EyeParameter{Left =10, Right = 42},
				SelectedCylinder = new EyeParameter{Left = -0.25M, Right = 3},
				SelectedAxisLeft = -1,
				SelectedAxisRight = 181,
				SelectedAddition = new EyeParameter{Left = 0.75M, Right = 3.75M},
				SelectedHeight = new EyeParameter{Left = 17, Right = 33},
				Reference = "Skynda på"
			};
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			

			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			presenter.HttpContext = mockedHttpContext.Object;
			presenter.View_Load(null, new EventArgs());
			presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(view.Model.SelectedFrameId, Is.EqualTo(1));
			Expect(view.Model.SelectedGlassTypeId, Is.EqualTo(8));
			Expect(view.Model.PupillaryDistance.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.PupillaryDistance.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.Sphere.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.Sphere.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.Cylinder.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.Cylinder.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.AxisSelectionLeft, Is.EqualTo(frameSelectedEventArgs.SelectedAxisLeft));
			Expect(view.Model.AxisSelectionRight, Is.EqualTo(frameSelectedEventArgs.SelectedAxisRight));
			Expect(view.Model.Addition.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.Addition.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.Height.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(view.Model.Height.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(view.Model.Reference, Is.EqualTo(frameSelectedEventArgs.Reference));
		}

		[Test]
		public void When_Form_Is_Submitted_Saved_Item_Has_Expected_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new EditFrameFormEventArgs {
				SelectedFrameId = 5,
				SelectedGlassTypeId = 8,
				SelectedPupillaryDistance = new EyeParameter { Left = 22, Right = 33 },
				SelectedSphere = new EyeParameter { Left = -5, Right = 2.25M },
				SelectedCylinder = new EyeParameter { Left = 0.25M, Right = 1.75M },
				SelectedAxisLeft = 20,
				SelectedAxisRight = 179,
				SelectedAddition = new EyeParameter { Left = 1.25M, Right = 2.75M },
				SelectedHeight = new EyeParameter { Left = 19, Right = 27 },
				Reference = "Skynda på",
                PageIsValid = true
			};
			const int expectedShopId = 5;
			const string expectedRedirectUrl = "/test/url/";
			const int expectedSavedItemId = 10;
			var expectedRedirectUrlWithQueryString = String.Concat(expectedRedirectUrl, "?frameorder=", expectedSavedItemId);
			var mockedHttpContext = new Mock<HttpContextBase>();
			var mockedHttpResponse = new Mock<HttpResponseBase>();
			var requestParams = new NameValueCollection();
			

			//Act
			mockedHttpContext.SetupGet(x => x.Response).Returns(mockedHttpResponse.Object);
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			((ServiceFactory.MockedSessionProviderService) synologenMemberService).SetMockedShopId(expectedShopId);
			((ServiceFactory.MockedSessionProviderService) synologenMemberService).SetMockedPageUrl(expectedRedirectUrl);
			((RepositoryFactory.MockedFrameOrderRepository) frameOrderRepository).SetSavedId(expectedSavedItemId);
			presenter.HttpContext = mockedHttpContext.Object;
			presenter.View_Load(null, new EventArgs());
			presenter.View.RedirectPageId = 5;
			presenter.View_SumbitForm(null, frameSelectedEventArgs);
			var savedEntity = ((RepositoryFactory.MockedFrameOrderRepository) frameOrderRepository).SavedItem;

			//Assert
			Expect(savedEntity, Is.Not.Null);
			Expect(savedEntity.Addition.Left, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Left));
			Expect(savedEntity.Addition.Right, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Right));
			Expect(savedEntity.Axis.Left, Is.EqualTo(frameSelectedEventArgs.SelectedAxisLeft));
			Expect(savedEntity.Axis.Right, Is.EqualTo(frameSelectedEventArgs.SelectedAxisRight));
			Expect(savedEntity.Created.ToString("yyyy-MM-dd HH:mm"), Is.EqualTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
			Expect(savedEntity.Cylinder.Left, Is.EqualTo(frameSelectedEventArgs.SelectedCylinder.Left));
			Expect(savedEntity.Cylinder.Right, Is.EqualTo(frameSelectedEventArgs.SelectedCylinder.Right));
			Expect(savedEntity.Frame.Id, Is.EqualTo(frameSelectedEventArgs.SelectedFrameId));
			Expect(savedEntity.GlassType.Id, Is.EqualTo(frameSelectedEventArgs.SelectedGlassTypeId));
			Expect(savedEntity.Height.Left, Is.EqualTo(frameSelectedEventArgs.SelectedHeight.Left));
			Expect(savedEntity.Height.Right, Is.EqualTo(frameSelectedEventArgs.SelectedHeight.Right));
			Expect(savedEntity.IsSent, Is.EqualTo(false));
			Expect(savedEntity.Reference, Is.EqualTo(frameSelectedEventArgs.Reference));
			Expect(savedEntity.OrderingShop.Id, Is.EqualTo(expectedShopId));
			Expect(savedEntity.PupillaryDistance.Left, Is.EqualTo(frameSelectedEventArgs.SelectedPupillaryDistance.Left));
			Expect(savedEntity.PupillaryDistance.Right, Is.EqualTo(frameSelectedEventArgs.SelectedPupillaryDistance.Right));
			Expect(savedEntity.Sent, Is.Null);
			Expect(savedEntity.Sphere.Left, Is.EqualTo(frameSelectedEventArgs.SelectedSphere.Left));
			Expect(savedEntity.Sphere.Right, Is.EqualTo(frameSelectedEventArgs.SelectedSphere.Right));
			mockedHttpResponse.Verify(x => x.Redirect(expectedRedirectUrlWithQueryString));
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
			Expect(view.Model.DisplayForm, Is.True);
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
			Expect(view.Model.DisplayForm, Is.False);
		}
	}
}