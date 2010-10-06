using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters
{
	public class EditFrameOrderPresenter : Presenter<IEditFrameOrderView<EditFrameOrderModel>>
	{
		private readonly IFrameRepository _frameRepository;
		private readonly IFrameGlassTypeRepository _frameGlassTypeRepository;
		private readonly IFrameOrderRepository _frameOrderRepository;
		private readonly IShopRepository _shopRepository;
		private readonly ISynologenMemberService _synologenMemberService;
		private readonly IFrameOrderService _frameOrderService;
		private readonly ISynologenSettingsService _synologenSettingsService;
		private readonly IEnumerable<IntervalListItem> EmptyIntervalList = new List<IntervalListItem>();
		private readonly FrameListItem DefaultFrame = new FrameListItem {Id = 0, Name = "-- V�lj b�ge --"};
		private readonly AllOrderableFramesCriteria AllOrderableFramesCriteria = new AllOrderableFramesCriteria();

		public EditFrameOrderPresenter(IEditFrameOrderView<EditFrameOrderModel> view, IFrameRepository repository, IFrameGlassTypeRepository frameGlassTypeRepository, IFrameOrderRepository frameOrderRepository, IShopRepository shopRepository, ISynologenMemberService sessionProviderService, IFrameOrderService frameOrderService, ISynologenSettingsService synologenSettingsService) : base(view)
		{
			_frameRepository = repository;
			_frameGlassTypeRepository = frameGlassTypeRepository;
			_frameOrderRepository = frameOrderRepository;
			_shopRepository = shopRepository;
			_synologenMemberService = sessionProviderService;
			_frameOrderService = frameOrderService;
			_synologenSettingsService = synologenSettingsService;
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
					var url = _synologenMemberService.GetPageUrl(View.RedirectPageId);
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
			View.Model.FramesList = _frameRepository.FindBy(AllOrderableFramesCriteria).ToFrameViewList().InsertFirst(DefaultFrame);
			View.Model.PupillaryDistance = EmptyIntervalList.CreateDefaultEyeParameter("PD");
			View.Model.GlassTypesList = _frameGlassTypeRepository.GetAll().ToFrameGlassTypeViewList().InsertFirst(new FrameGlassTypeListItem {Id = 0, Name = "-- V�lj glastyp --"});
			View.Model.Sphere = _synologenSettingsService.Sphere.GetList().CreateDefaultEyeParameter("Sf�r");
			View.Model.Cylinder = _synologenSettingsService.Cylinder.GetList().CreateDefaultEyeParameter("Cylinder");
			View.Model.Addition = EmptyIntervalList.CreateDefaultEyeParameter("Addition");
			View.Model.Height = EmptyIntervalList.CreateDefaultEyeParameter("H�jd");
			View.Model.FrameRequiredErrorMessage = "B�ge saknas";
			View.Model.GlassTypeRequiredErrorMessage = "Glastyp saknas";
			View.Model.PupillaryDistanceRequiredErrorMessage = "PD saknas";
			View.Model.SphereRequiredErrorMessage = "Sf�r saknas";
			View.Model.CylinderRequiredErrorMessage = "Cylinder saknas";
			View.Model.AdditionRequiredErrorMessage = "Addition saknas";
			View.Model.HeightRequiredMessage = "H�jd saknas";
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
			if(e.SelectedGlassTypeId>0){
				glassType = _frameGlassTypeRepository.Get(e.SelectedGlassTypeId);
				View.Model.HeightParametersEnabled = glassType.IncludeHeightParametersInOrder;
				View.Model.AdditionParametersEnabled = glassType.IncludeAdditionParametersInOrder;
			}

			View.Model.SelectedFrameId = e.SelectedFrameId;
			View.Model.SelectedGlassTypeId = e.SelectedGlassTypeId;
			View.Model.Sphere = e.GetEyeParameter(x => x.SelectedSphere, _synologenSettingsService.Sphere.GetList(), "Sf�r");
			View.Model.Cylinder = e.GetEyeParameter(x => x.SelectedCylinder, _synologenSettingsService.Cylinder.GetList(), "Cylinder");
			View.Model.AxisSelectionLeft = e.SelectedAxisLeft;
			View.Model.AxisSelectionRight = e.SelectedAxisRight;
			View.Model.Reference = e.Reference;

			if(glassType != null && glassType.IncludeAdditionParametersInOrder)
			{
				View.Model.Addition = e.GetEyeParameter(x => x.SelectedAddition, _synologenSettingsService.Addition.GetList(), "Addition");
			}
			if(glassType != null && glassType.IncludeHeightParametersInOrder)
			{
				View.Model.Height = e.GetEyeParameter(x => x.SelectedHeight, _synologenSettingsService.Height.GetList(), "H�jd");
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
			View.Model.HeightParametersEnabled = frameOrder.GlassType.IncludeHeightParametersInOrder;
			View.Model.AdditionParametersEnabled = frameOrder.GlassType.IncludeAdditionParametersInOrder;
			View.Model.SelectedFrameId = frameOrder.Frame.Id;
			View.Model.SelectedGlassTypeId = frameOrder.GlassType.Id;
			View.Model.Sphere = frameOrder.GetEyeParameter(x => x.Sphere, _synologenSettingsService.Sphere.GetList(), "Sf�r");
			View.Model.Cylinder = frameOrder.GetEyeParameter(x => x.Cylinder, _synologenSettingsService.Cylinder.GetList(), "Cylinder");
			View.Model.AxisSelectionLeft = Convert.ToInt32(frameOrder.Axis.Left);
			View.Model.AxisSelectionRight = Convert.ToInt32(frameOrder.Axis.Right);
			View.Model.Reference = frameOrder.Reference;

			if(frameOrder.GlassType.IncludeAdditionParametersInOrder)
			{
				View.Model.Addition = frameOrder.GetEyeParameter(x => x.Addition, _synologenSettingsService.Addition.GetList(), "Addition");
			}
			if(frameOrder.GlassType.IncludeHeightParametersInOrder)
			{
				View.Model.Height = frameOrder.GetEyeParameter(x => x.Height, _synologenSettingsService.Height.GetList(), "H�jd");
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
				frameOrder = e.FillFrameOrder(previouslySavedFrameOrder);
			}
			else{
				var frame = _frameRepository.Get(e.SelectedFrameId);
				var glassType = _frameGlassTypeRepository.Get(e.SelectedGlassTypeId);
				var shopId = _synologenMemberService.GetCurrentShopId();
				var shop = _shopRepository.Get(shopId);
				frameOrder = e.ToFrameOrder(frame, glassType, shop);
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