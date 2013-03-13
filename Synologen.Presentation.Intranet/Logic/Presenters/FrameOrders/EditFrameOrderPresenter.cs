using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.FrameOrders
{
	public class EditFrameOrderPresenter : Presenter<IEditFrameOrderView<EditFrameOrderModel>>
	{
		private readonly IFrameRepository _frameRepository;
		private readonly IFrameGlassTypeRepository _frameGlassTypeRepository;
		private readonly IFrameOrderRepository _frameOrderRepository;
		private readonly IShopRepository _shopRepository;
        private readonly IFrameSupplierRepository _frameSupplierRepository;
		private readonly ISynologenMemberService _synologenMemberService;
		private readonly ISynologenSettingsService _synologenSettingsService;
		private readonly IRoutingService _routingService;
		private readonly IEnumerable<IntervalListItem> EmptyIntervalList = new List<IntervalListItem>();
		private readonly FrameListItem DefaultFrame = new FrameListItem {Id = 0, Name = "-- Välj båge --"};
        private readonly FrameSupplierListItem DefaultSupplier = new FrameSupplierListItem { Id = 0, Name = "-- Välj leverantör --" };
		private readonly AllOrderableFramesCriteria AllOrderableFramesCriteria = new AllOrderableFramesCriteria();

		public EditFrameOrderPresenter(
			IEditFrameOrderView<EditFrameOrderModel> view, 
			IFrameRepository repository, 
			IFrameGlassTypeRepository frameGlassTypeRepository, 
			IFrameOrderRepository frameOrderRepository, 
			IShopRepository shopRepository, 
			ISynologenMemberService sessionProviderService, 
			ISynologenSettingsService synologenSettingsService,
            IFrameSupplierRepository frameSupplierRepository,
			IRoutingService routingService) : base(view)
		{
			_frameRepository = repository;
			_frameGlassTypeRepository = frameGlassTypeRepository;
			_frameOrderRepository = frameOrderRepository;
			_shopRepository = shopRepository;
			_synologenMemberService = sessionProviderService;
			_synologenSettingsService = synologenSettingsService;
			_routingService = routingService;
		    _frameSupplierRepository = frameSupplierRepository;
			InitiateEventHandlers();
		}

		private void InitiateEventHandlers()
		{
			View.Load += View_Load;
			View.FrameSelected += View_BindModel;
			View.SubmitForm += View_SumbitForm;
			View.GlassTypeSelected += View_BindModel;
		}

		public void View_Load(object sender, EventArgs e)
		{
			InitializeModel();
		}

		public void View_BindModel(object sender, EditFrameFormEventArgs e)
		{
			UpdateModel(e);
		}

		public void View_SumbitForm(object sender, EditFrameFormEventArgs e) 
		{ 
			if(e.PageIsValid)
			{
				var frameOrderId = SaveUpdateFrameOrder(e);
				if (View.RedirectPageId > 0)
				{
					var url = _routingService.GetPageUrl(View.RedirectPageId);
					url = String.Concat(url, "?frameorder=", frameOrderId);
					HttpContext.Response.Redirect(url);
				}
			}
			else
			{
				UpdateModel(e);
			}
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.FrameSelected -= View_BindModel;
			View.SubmitForm -= View_SumbitForm;
			View.GlassTypeSelected -= View_BindModel;
		}

		public void InitializeModel()
		{

            View.Model.SupplierList = _frameSupplierRepository.GetAll().ToFrameSupplierList().InsertFirst(DefaultSupplier);
            View.Model.FramesList = _frameRepository.FindBy(AllOrderableFramesCriteria).ToFrameViewList().InsertFirst(DefaultFrame);
			View.Model.PupillaryDistance = EmptyIntervalList.CreateDefaultEyeParameter("PD");
			View.Model.Sphere = EmptyIntervalList.CreateDefaultEyeParameter("Sfär");
			View.Model.Cylinder = EmptyIntervalList.CreateDefaultEyeParameter("Cylinder");
			View.Model.GlassTypesList = _frameGlassTypeRepository.GetAll().ToFrameGlassTypeViewList().InsertFirst(new FrameGlassTypeListItem {Id = 0, Name = "-- Välj glastyp --"});
			View.Model.Addition = EmptyIntervalList.CreateDefaultEyeParameter("Addition");
			View.Model.Height = EmptyIntervalList.CreateDefaultEyeParameter("Höjd");
			View.Model.FrameRequiredErrorMessage = "Båge saknas";
			View.Model.GlassTypeRequiredErrorMessage = "Glastyp saknas";
			View.Model.PupillaryDistanceRequiredErrorMessage = "PD saknas";
			View.Model.SphereRequiredErrorMessage = "Sfär saknas";
			View.Model.AdditionRequiredErrorMessage = "Addition saknas";
			View.Model.HeightRequiredMessage = "Höjd saknas";
			View.Model.AxisRequiredMessage = "Axel saknas";
			View.Model.AxisRangeMessage = "Axel anges som ett heltal i intervallet 0-180";
			var frameOrderId = GetFrameOrderId();
			View.Model.ShopDoesNotHaveAccessToFrameOrders = !_synologenMemberService.ShopHasAccessTo(ShopAccess.SlimJim);
			if (frameOrderId <= 0) return;
			var frameOrder = _frameOrderRepository.Get(frameOrderId);
			UpdateModel(frameOrder);
		}

		public void UpdateModel(EditFrameFormEventArgs e)
		{
			if(e.SelectedFrameId>0){
				var frame = _frameRepository.Get(e.SelectedFrameId);
				View.Model.PupillaryDistance = e.GetEyeParameter(x => x.SelectedPupillaryDistance, frame.PupillaryDistance.GetList(), "PD");
			}

			FrameGlassType glassType = null;
			if(e.SelectedGlassTypeId>0)
			{
				glassType = _frameGlassTypeRepository.Get(e.SelectedGlassTypeId);
				View.Model.HeightParametersEnabled = glassType.IncludeHeightParametersInOrder;
				View.Model.AdditionParametersEnabled = glassType.IncludeAdditionParametersInOrder;
				View.Model.Sphere = e.GetEyeParameter(x => x.SelectedSphere, glassType.Sphere.GetList(), "Sfär");
				View.Model.Cylinder = e.GetEyeParameter(x => x.SelectedCylinder, glassType.Cylinder.GetList(), "Cylinder");
			}

			View.Model.AxisValueLeftIsRequired = e.SelectedCylinder.Left != int.MinValue;
			View.Model.AxisValueRightIsRequired = e.SelectedCylinder.Right != int.MinValue;
            View.Model.SelectedSupplierId = e.SelectedSupplierId;
			View.Model.SelectedFrameId = e.SelectedFrameId;
			View.Model.SelectedGlassTypeId = e.SelectedGlassTypeId;
			View.Model.AxisSelectionLeft = (e.SelectedAxis.Left == int.MinValue) ? (int?)null : e.SelectedAxis.Left;
			View.Model.AxisSelectionRight = (e.SelectedAxis.Right == int.MinValue) ? (int?)null : e.SelectedAxis.Right;
			View.Model.Reference = e.Reference;

			if(glassType != null && glassType.IncludeAdditionParametersInOrder)
			{
				View.Model.Addition = e.GetEyeParameter(x => x.SelectedAddition, _synologenSettingsService.Addition.GetList(), "Addition");
			}
			if(glassType != null && glassType.IncludeHeightParametersInOrder)
			{
				View.Model.Height = e.GetEyeParameter(x => x.SelectedHeight, _synologenSettingsService.Height.GetList(), "Höjd");
			}
		}

		public void UpdateModel(FrameOrder frameOrder)
		{
			if(frameOrder == null)
			{
				View.Model.OrderDoesNotExist = true;
				return;
			}
			View.Model.PupillaryDistance = frameOrder.GetEyeParameter(x => x.PupillaryDistance, frameOrder.Frame.PupillaryDistance.GetList(), "PD");
			View.Model.Sphere = frameOrder.GetEyeParameter(x => x.Sphere, frameOrder.GlassType.Sphere.GetList(), "Sfär");
			View.Model.Cylinder = frameOrder.GetEyeParameter(x => x.Cylinder, frameOrder.GlassType.Cylinder.GetList(), "Cylinder");
			View.Model.HeightParametersEnabled = frameOrder.GlassType.IncludeHeightParametersInOrder;
			View.Model.AdditionParametersEnabled = frameOrder.GlassType.IncludeAdditionParametersInOrder;
			View.Model.SelectedFrameId = frameOrder.Frame.Id;
            View.Model.SelectedSupplierId = frameOrder.Supplier.Id;
			View.Model.SelectedGlassTypeId = frameOrder.GlassType.Id;
			View.Model.AxisSelectionLeft = (frameOrder.Axis == null || frameOrder.Axis.Left == null) ? (int?)null : frameOrder.Axis.Left.Value;
			View.Model.AxisSelectionRight = (frameOrder.Axis == null || frameOrder.Axis.Right == null) ? (int?)null : frameOrder.Axis.Right.Value;
			View.Model.Reference = frameOrder.Reference;

			View.Model.AxisValueLeftIsRequired = View.Model.Cylinder.Selection.Left != int.MinValue;
			View.Model.AxisValueRightIsRequired = View.Model.Cylinder.Selection.Left != int.MinValue;

			if(frameOrder.GlassType.IncludeAdditionParametersInOrder)
			{
				View.Model.Addition = frameOrder.GetEyeParameter(x => x.Addition, _synologenSettingsService.Addition.GetList(), "Addition");
			}
			if(frameOrder.GlassType.IncludeHeightParametersInOrder)
			{
				View.Model.Height = frameOrder.GetEyeParameter(x => x.Height, _synologenSettingsService.Height.GetList(), "Höjd");
			}
			if(frameOrder.OrderingShop.Id != _synologenMemberService.GetCurrentShopId())
			{
				View.Model.UserDoesNotHaveAccessToThisOrder = true;
				return;
			}

			View.Model.OrderHasBeenSent = frameOrder.Sent.HasValue;
		}

		private int SaveUpdateFrameOrder(EditFrameFormEventArgs e) {
			var frameOrderId = GetFrameOrderId();
			FrameOrder frameOrder;
			if(frameOrderId>0)
			{
				var previouslySavedFrameOrder = _frameOrderRepository.Get(frameOrderId);
				var frame = _frameRepository.Get(e.SelectedFrameId);
				var glassType = _frameGlassTypeRepository.Get(e.SelectedGlassTypeId);
                var supplier = _frameSupplierRepository.Get(e.SelectedSupplierId);
				frameOrder = e.FillFrameOrder(frame, glassType, previouslySavedFrameOrder,supplier);
			}
			else
			{
			    var supplier = _frameSupplierRepository.Get(e.SelectedSupplierId);
                var frame = _frameRepository.Get(e.SelectedFrameId);
				var glassType = _frameGlassTypeRepository.Get(e.SelectedGlassTypeId);
				var shopId = _synologenMemberService.GetCurrentShopId();
				var shop = _shopRepository.Get(shopId);
				frameOrder = e.ToFrameOrder(frame, glassType, shop , supplier);
			}
			_frameOrderRepository.Save(frameOrder);
			return frameOrder.Id;
		}

		private int GetFrameOrderId()
		{
			int integerframeOrderId;
			var frameOrderId = HttpContext.Request.Params["frameorder"];
			if (frameOrderId == null) return -1;
			return Int32.TryParse(frameOrderId, out integerframeOrderId) ? integerframeOrderId : -1;
		}
	}
}