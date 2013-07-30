using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.AssertionHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.FrameOrderTests
{
	[TestFixture, Category("EditFrameOrderPresenterTests")]
	public class Given_a_EditFrameOrderPresenter : WpcAssertionHelper
	{
		private EditFrameOrderPresenter _presenter;
		private IEditFrameOrderView<EditFrameOrderModel> _view;
	    private IFrameSupplierRepository _frameSupplierRepository;
		private IFrameRepository _frameRepository;
		private IFrameGlassTypeRepository _frameGlassTypeRepository;
		private IFrameOrderRepository _frameOrderRepository;
		private ISynologenMemberService _synologenMemberService;
		private IShopRepository _shopRepository;
		private readonly Func<EyeParameter, EyeParameter, bool> _eyeparameterEquality = (objectOne, objectTwo) => (objectOne.Left == objectTwo.Left && objectOne.Right == objectTwo.Right);
		private readonly Func<EyeParameter, NullableEyeParameter, bool> _nullableEyeparameterEquality = (objectOne, objectTwo) => (objectOne.Left == objectTwo.Left && objectOne.Right == objectTwo.Right) || (objectOne.Left == int.MinValue && objectTwo.Left == null && objectOne.Right == int.MinValue && objectTwo.Right == null);
		private ISynologenSettingsService _synologenSettingsService;
		private IRoutingService _routingservice;

		[SetUp]
		public void Context()
		{
            _frameSupplierRepository = RepositoryFactory.GetFrameSupplierRepository();
			_frameRepository = RepositoryFactory.GetFrameRepository();
			_frameGlassTypeRepository = RepositoryFactory.GetFrameGlassRepository();
			_frameOrderRepository = RepositoryFactory.GetFramOrderRepository();
			_shopRepository = RepositoryFactory.GetShopRepository();
			_synologenSettingsService = ServiceFactory.GetSynologenSettingsService();
			_synologenMemberService = ServiceFactory.GetSynologenMemberService();
			_view = A.Fake<IEditFrameOrderView<EditFrameOrderModel>>();
			_routingservice = A.Fake<IRoutingService>();
				//ViewsFactory.GetFrameOrderView();
			_presenter = new EditFrameOrderPresenter(_view, _frameRepository, _frameGlassTypeRepository, _frameOrderRepository, _shopRepository, _synologenMemberService, _synologenSettingsService,_frameSupplierRepository, _routingservice);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange
            const int expectedNumberOfSuppliers = 11;
			const int expectedNumberOfFrames = 1;
			const int expectedNumberOfPDs = 1;
			const int expectedNumberOfSpheres = 1;
			const int expectedNumberOfCylinders = 1;
			const int expectedNumberOfGlassTypes = 11;
			const int expectedNumberOfAdditions = 1;
			const int expectedNumberOfHeights = 1;
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			
			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			_presenter.HttpContext = mockedHttpContext.Object;
			_presenter.View_Load(null, new EventArgs());

			//Assert
            Expect(_view.Model.SelectedSupplierId, Is.EqualTo(0));
			Expect(_view.Model.SelectedFrameId, Is.EqualTo(0));
			Expect(_view.Model.SelectedGlassTypeId, Is.EqualTo(0));
			Expect(_view.Model.PupillaryDistance.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.PupillaryDistance.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Sphere.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Sphere.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Cylinder.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Cylinder.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.AxisSelectionLeft, Is.Null);
			Expect(_view.Model.AxisSelectionRight, Is.Null);
			Expect(_view.Model.Addition.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Addition.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Height.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Height.Selection.Right, Is.EqualTo(int.MinValue));
            Expect(_view.Model.SupplierList.Count(), Is.EqualTo(expectedNumberOfSuppliers));
            Expect(_view.Model.SupplierList.First().Id, Is.EqualTo(0));
            Expect(_view.Model.SupplierList.First().Name, Is.EqualTo("-- Välj leverantör --"));
			Expect(_view.Model.FramesList.Count(), Is.EqualTo(expectedNumberOfFrames));
			Expect(_view.Model.FramesList.First().Id, Is.EqualTo(0));
			Expect(_view.Model.FramesList.First().Name, Is.EqualTo("-- Välj båge --"));
			Expect(_view.Model.FrameRequiredErrorMessage, Is.EqualTo("Båge saknas"));
			Expect(_view.Model.GlassTypesList.Count(), Is.EqualTo(expectedNumberOfGlassTypes));
			Expect(_view.Model.GlassTypesList.First().Id, Is.EqualTo(0));
			Expect(_view.Model.GlassTypesList.First().Name, Is.EqualTo("-- Välj glastyp --"));
			Expect(_view.Model.GlassTypeRequiredErrorMessage, Is.EqualTo("Glastyp saknas"));
			Expect(_view.Model.PupillaryDistance.List.Count(), Is.EqualTo(expectedNumberOfPDs));
			Expect(_view.Model.PupillaryDistance.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(_view.Model.PupillaryDistance.List.First().Name, Is.EqualTo("-- Välj PD --"));
			Expect(_view.Model.PupillaryDistanceRequiredErrorMessage, Is.EqualTo("PD saknas"));
			Expect(_view.Model.Sphere.List.Count(), Is.EqualTo(expectedNumberOfSpheres));
			Expect(_view.Model.Sphere.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(_view.Model.Sphere.List.First().Name, Is.EqualTo("-- Välj Sfär --"));
			Expect(_view.Model.SphereRequiredErrorMessage, Is.EqualTo("Sfär saknas"));
			Expect(_view.Model.Cylinder.List.Count(), Is.EqualTo(expectedNumberOfCylinders));
			Expect(_view.Model.Cylinder.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(_view.Model.Cylinder.List.First().Name, Is.EqualTo("-- Välj Cylinder --"));
			//Expect(view.Model.CylinderRequiredErrorMessage, Is.EqualTo("Cylinder saknas"));
			Expect(_view.Model.Addition.List.Count(), Is.EqualTo(expectedNumberOfAdditions));
			Expect(_view.Model.Addition.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(_view.Model.Addition.List.First().Name, Is.EqualTo("-- Välj Addition --"));
			Expect(_view.Model.AdditionRequiredErrorMessage, Is.EqualTo("Addition saknas"));
			Expect(_view.Model.Height.List.Count(), Is.EqualTo(expectedNumberOfHeights));
			Expect(_view.Model.Height.List.First().Value, Is.EqualTo(int.MinValue.ToString("N2")));
			Expect(_view.Model.Height.List.First().Name, Is.EqualTo("-- Välj Höjd --"));
			Expect(_view.Model.HeightRequiredMessage, Is.EqualTo("Höjd saknas"));
			Expect(_view.Model.AxisRequiredMessage, Is.EqualTo("Axel saknas"));
			Expect(_view.Model.AxisRangeMessage, Is.EqualTo("Axel anges som ett heltal i intervallet 0-180"));
			Expect(_view.Model.NotSelectedIntervalValue, Is.EqualTo(int.MinValue));
			Expect(_view.Model.HeightParametersEnabled, Is.False);
			Expect(_view.Model.AdditionParametersEnabled, Is.False);
			Expect(_view.Model.AxisValueLeftIsRequired, Is.False);
			Expect(_view.Model.AxisValueRightIsRequired, Is.False);
			Expect(_view.Model.Reference, Is.Null);
		}

		[Test]
		public void When_View_Is_Loaded_With_Saved_FrameOrder_Model_Has_Expected_Values()
		{
			//Arrange
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection {{"frameorder", "5"}};
			var expectedFrameOrder = _frameOrderRepository.Get(5);
			const int expectedShopId = 10;
			
			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			_presenter.HttpContext = mockedHttpContext.Object;
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedShopId(expectedShopId);
			_presenter.View_Load(null, new EventArgs());

			//Assert
            Expect(_view.Model.SelectedSupplierId, Is.EqualTo(expectedFrameOrder.Frame.Supplier.Id));
			Expect(_view.Model.SelectedFrameId, Is.EqualTo(expectedFrameOrder.Frame.Id));
			Expect(_view.Model.SelectedGlassTypeId, Is.EqualTo(expectedFrameOrder.GlassType.Id));
			ExpectEqual(_view.Model.PupillaryDistance.Selection, expectedFrameOrder.PupillaryDistance, _eyeparameterEquality);
			ExpectEqual(_view.Model.Sphere.Selection, expectedFrameOrder.Sphere, _eyeparameterEquality);
			ExpectEqual(_view.Model.Cylinder.Selection, expectedFrameOrder.Cylinder, _nullableEyeparameterEquality);
			Expect(_view.Model.AxisSelectionLeft, Is.EqualTo(expectedFrameOrder.Axis.Left));
			Expect(_view.Model.AxisSelectionRight, Is.EqualTo(expectedFrameOrder.Axis.Right));
			ExpectEqual(_view.Model.Height.Selection, expectedFrameOrder.Height, _nullableEyeparameterEquality);
			ExpectEqual(_view.Model.Addition.Selection, expectedFrameOrder.Addition, _nullableEyeparameterEquality);
			Expect(_view.Model.HeightParametersEnabled, Is.EqualTo(expectedFrameOrder.GlassType.IncludeHeightParametersInOrder));
			Expect(_view.Model.AdditionParametersEnabled, Is.EqualTo(expectedFrameOrder.GlassType.IncludeAdditionParametersInOrder));
			Expect(_view.Model.Reference, Is.EqualTo(expectedFrameOrder.Reference));
			Expect(_view.Model.OrderHasBeenSent, Is.EqualTo(expectedFrameOrder.Sent.HasValue));
			Expect(_view.Model.UserDoesNotHaveAccessToThisOrder, Is.EqualTo(expectedFrameOrder.OrderingShop.Id != expectedShopId));
			Expect(_view.Model.OrderDoesNotExist, Is.EqualTo(expectedFrameOrder == null));
			Expect(_view.Model.AxisValueLeftIsRequired, Is.True);
			Expect(_view.Model.AxisValueRightIsRequired, Is.True);
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
				SelectedCylinder = new EyeParameter{ Left = -0.25M, Right = -1.75M}, 
				SelectedAxis = new EyeParameter<int>{Left = 20, Right = 179},
				SelectedAddition = new EyeParameter{Left = 1.25M, Right = 2.75M},
				SelectedHeight = new EyeParameter{Left = 19, Right = 27},
				Reference = "Skynda på"
			};
			const int expectedNumberOfFramesInList = 1;
			const int expectedNumberOfGlassTypesInList = 11;
			const int expectedNumberOfPDsInList = 22;
			const int expectedNumberOfSpheresInList = 50;
			//const int expectedNumberOfSpheresInList = 17;
			const int expectedNumberOfCylindersInList = 10;
			const int expectedNumberOfAdditionsInList = 10;
			const int expectedNumberOfHeightsInList = 12;
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			
			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			_presenter.HttpContext = mockedHttpContext.Object;
			_presenter.View_Load(null, new EventArgs());
			_presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(_view.Model.PupillaryDistance.List.Count(), Is.EqualTo(expectedNumberOfPDsInList));
			Expect(_view.Model.Sphere.List.Count(), Is.EqualTo(expectedNumberOfSpheresInList));
			Expect(_view.Model.Cylinder.List.Count(), Is.EqualTo(expectedNumberOfCylindersInList));
			Expect(_view.Model.Addition.List.Count(), Is.EqualTo(expectedNumberOfAdditionsInList));
			Expect(_view.Model.Height.List.Count(), Is.EqualTo(expectedNumberOfHeightsInList));
			Expect(_view.Model.GlassTypesList.Count(), Is.EqualTo(expectedNumberOfGlassTypesInList));
			Expect(_view.Model.FramesList.Count(), Is.EqualTo(expectedNumberOfFramesInList));

			Expect(_view.Model.SelectedFrameId, Is.EqualTo(frameSelectedEventArgs.SelectedFrameId));
			Expect(_view.Model.SelectedGlassTypeId, Is.EqualTo(frameSelectedEventArgs.SelectedGlassTypeId));
			Expect(_view.Model.PupillaryDistance.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedPupillaryDistance.Left));
			Expect(_view.Model.PupillaryDistance.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedPupillaryDistance.Right));
			Expect(_view.Model.Sphere.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedSphere.Left));
			Expect(_view.Model.Sphere.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedSphere.Right));
			Expect(_view.Model.Cylinder.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedCylinder.Left));
			Expect(_view.Model.Cylinder.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedCylinder.Right));
			Expect(_view.Model.AxisSelectionLeft, Is.EqualTo(frameSelectedEventArgs.SelectedAxis.Left));
			Expect(_view.Model.AxisSelectionRight, Is.EqualTo(frameSelectedEventArgs.SelectedAxis.Right));
			Expect(_view.Model.Addition.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Left));
			Expect(_view.Model.Addition.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Right));
			Expect(_view.Model.Height.Selection.Left, Is.EqualTo(frameSelectedEventArgs.SelectedHeight.Left));
			Expect(_view.Model.Height.Selection.Right, Is.EqualTo(frameSelectedEventArgs.SelectedHeight.Right));
			Expect(_view.Model.HeightParametersEnabled, Is.True);
			Expect(_view.Model.AdditionParametersEnabled, Is.True);
			Expect(_view.Model.AxisValueLeftIsRequired, Is.True);
			Expect(_view.Model.AxisValueRightIsRequired, Is.True);
			Expect(_view.Model.Reference, Is.EqualTo(frameSelectedEventArgs.Reference));
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
				SelectedAxis = new EyeParameter<int>{Left = 20, Right = 179},
				SelectedAddition = new EyeParameter{Left = 1.25M, Right = 2.75M},
				SelectedHeight = new EyeParameter{Left = 19, Right = 27},
			};
			const int expectedNumberOfAdditionsInList = 1;
			const int expectedNumberOfHeightsInList = 1;
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			
			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			_presenter.HttpContext = mockedHttpContext.Object;
			_presenter.View_Load(null, new EventArgs());
			_presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(_view.Model.Addition.List.Count(), Is.EqualTo(expectedNumberOfAdditionsInList));
			Expect(_view.Model.Height.List.Count(), Is.EqualTo(expectedNumberOfHeightsInList));
			Expect(_view.Model.Height.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Height.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.HeightParametersEnabled, Is.False);
			Expect(_view.Model.AdditionParametersEnabled, Is.False);
			Expect(_view.Model.AxisValueLeftIsRequired, Is.True);
			Expect(_view.Model.AxisValueRightIsRequired, Is.True);
		}

		[Test]
		public void When_Model_Is_Bound_Without_Cylinder_Or_Axis_Selected_Model_Has_Expected_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new EditFrameFormEventArgs
			{
				SelectedFrameId = 5, 
				SelectedGlassTypeId = 8,  // Returns a glasstype with both addition and height
				SelectedPupillaryDistance = new EyeParameter{Left = 22, Right = 33},
				SelectedSphere = new EyeParameter{Left = -5, Right = 2.25M},
				SelectedAddition = new EyeParameter{Left = 1.25M, Right = 2.75M},
				SelectedHeight = new EyeParameter{Left = 19, Right = 27},
				SelectedCylinder = new EyeParameter{Left = int.MinValue, Right = int.MinValue},
				SelectedAxis =  new  EyeParameter<int>{Left = int.MinValue, Right = int.MinValue},
			};
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			
			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			_presenter.HttpContext = mockedHttpContext.Object;
			_presenter.View_Load(null, new EventArgs());
			_presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(_view.Model.Cylinder.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Cylinder.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.AxisSelectionLeft, Is.Null);
			Expect(_view.Model.AxisSelectionRight, Is.Null);
			Expect(_view.Model.AxisValueLeftIsRequired, Is.False);
			Expect(_view.Model.AxisValueRightIsRequired, Is.False);
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
				SelectedCylinder = new EyeParameter{Left = -2.25M, Right = 3},
				SelectedAxis = new EyeParameter<int>{Left = -1, Right = 181},
				SelectedAddition = new EyeParameter{Left = 0.75M, Right = 3.75M},
				SelectedHeight = new EyeParameter{Left = 17, Right = 33},
				Reference = "Skynda på"
			};
			var mockedHttpContext = new Mock<HttpContextBase>();
			var requestParams = new NameValueCollection();
			

			//Act
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			_presenter.HttpContext = mockedHttpContext.Object;
			_presenter.View_Load(null, new EventArgs());
			_presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(_view.Model.SelectedFrameId, Is.EqualTo(1));
			Expect(_view.Model.SelectedGlassTypeId, Is.EqualTo(8));
			Expect(_view.Model.PupillaryDistance.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.PupillaryDistance.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Sphere.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Sphere.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Cylinder.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Cylinder.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.AxisSelectionLeft, Is.EqualTo(frameSelectedEventArgs.SelectedAxis.Left));
			Expect(_view.Model.AxisSelectionRight, Is.EqualTo(frameSelectedEventArgs.SelectedAxis.Right));
			Expect(_view.Model.Addition.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Addition.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Height.Selection.Left, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Height.Selection.Right, Is.EqualTo(int.MinValue));
			Expect(_view.Model.Reference, Is.EqualTo(frameSelectedEventArgs.Reference));
		}

		[Test]
		public void When_Form_Is_Submitted_For_New_Order_Saved_Item_Has_Expected_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new EditFrameFormEventArgs {
				SelectedFrameId = 5,
				SelectedGlassTypeId = 8,
				SelectedPupillaryDistance = new EyeParameter { Left = 22, Right = 33 },
				SelectedSphere = new EyeParameter { Left = -5, Right = 2.25M },
				SelectedCylinder = new EyeParameter { Left = 0.25M, Right = 1.75M },
				SelectedAxis = new EyeParameter<int> { Left = 20, Right = 179 },
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
			A.CallTo(() => _routingservice.GetPageUrl(A<int>.Ignored)).Returns(expectedRedirectUrl);
			

			//Act
			mockedHttpContext.SetupGet(x => x.Response).Returns(mockedHttpResponse.Object);
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedShopId(expectedShopId);
			A.CallTo(() => _routingservice.GetPageUrl(A<int>.Ignored)).Returns(expectedRedirectUrl);
			//((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedPageUrl(expectedRedirectUrl);
			((RepositoryFactory.MockedFrameOrderRepository) _frameOrderRepository).SetSavedId(expectedSavedItemId);
			_presenter.HttpContext = mockedHttpContext.Object;
			_presenter.View_Load(null, new EventArgs());
			_presenter.View.RedirectPageId = 5;
			_presenter.View_SumbitForm(null, frameSelectedEventArgs);
			var savedEntity = ((RepositoryFactory.MockedFrameOrderRepository) _frameOrderRepository).SavedItem;

			//Assert
			Expect(savedEntity, Is.Not.Null);
			Expect(savedEntity.Addition.Left, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Left));
			Expect(savedEntity.Addition.Right, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Right));
			Expect(savedEntity.Axis.Left, Is.EqualTo(frameSelectedEventArgs.SelectedAxis.Left));
			Expect(savedEntity.Axis.Right, Is.EqualTo(frameSelectedEventArgs.SelectedAxis.Right));
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
		public void When_Form_Is_Submitted_For_Existing_Order_Saved_Item_Has_Expected_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new EditFrameFormEventArgs {
				SelectedFrameId = 5,
				SelectedGlassTypeId = 8,
				SelectedPupillaryDistance = new EyeParameter { Left = 22, Right = 33 },
				SelectedSphere = new EyeParameter { Left = -5, Right = 2.25M },
				SelectedCylinder = new EyeParameter { Left = 0.25M, Right = 1.75M },
				SelectedAxis = new EyeParameter<int> { Left = 20, Right = 179 },
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
			var requestParams = new NameValueCollection{{"frameorder", expectedSavedItemId.ToString()}};
			A.CallTo(() => _routingservice.GetPageUrl(A<int>.Ignored)).Returns(expectedRedirectUrl);
			

			//Act
			mockedHttpContext.SetupGet(x => x.Response).Returns(mockedHttpResponse.Object);
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedShopId(expectedShopId);
			A.CallTo(() => _routingservice.GetPageUrl(A<int>.Ignored)).Returns(expectedRedirectUrl);
			//((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedPageUrl(expectedRedirectUrl);
			((RepositoryFactory.MockedFrameOrderRepository) _frameOrderRepository).SetSavedId(expectedSavedItemId);
			_presenter.HttpContext = mockedHttpContext.Object;
			_presenter.View_Load(null, new EventArgs());
			_presenter.View.RedirectPageId = 5;
			_presenter.View_SumbitForm(null, frameSelectedEventArgs);
			var savedEntity = ((RepositoryFactory.MockedFrameOrderRepository) _frameOrderRepository).SavedItem;

			//Assert
			Expect(savedEntity, Is.Not.Null);
			Expect(savedEntity.Addition.Left, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Left));
			Expect(savedEntity.Addition.Right, Is.EqualTo(frameSelectedEventArgs.SelectedAddition.Right));
			Expect(savedEntity.Axis.Left, Is.EqualTo(frameSelectedEventArgs.SelectedAxis.Left));
			Expect(savedEntity.Axis.Right, Is.EqualTo(frameSelectedEventArgs.SelectedAxis.Right));
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
			((ServiceFactory.MockedSessionProviderService)_synologenMemberService).SetShopHasAccess(true);
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			_presenter.HttpContext = mockedHttpContext.Object;
			
			//Act
			_presenter.View_Load(null, new EventArgs());

			//Assert
			Expect(_view.Model.ShopDoesNotHaveAccessToFrameOrders, Is.False);
			Expect(_view.Model.DisplayForm, Is.True);
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
			Expect(_view.Model.DisplayForm, Is.False);
		}
	}
}