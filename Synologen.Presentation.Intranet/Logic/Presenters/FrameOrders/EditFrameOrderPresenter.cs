using System;
using System.Collections.Generic;
using System.Linq;
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
		protected readonly IEnumerable<IntervalListItem> EmptyIntervalList = new List<IntervalListItem>();
        protected readonly EntityListItem DefaultFrame = new EntityListItem { Id = 0, Name = "-- Välj båge --" };
        protected readonly EntityListItem DefaultSupplier = new EntityListItem { Id = 0, Name = "-- Välj leverantör --" };
        protected readonly EntityListItem DefaultGlassType = new EntityListItem { Id = 0, Name = "-- Välj glastyp --" };
        private readonly IFrameRepository _frameRepository;
        private readonly IFrameGlassTypeRepository _frameGlassTypeRepository;
        private readonly IFrameOrderRepository _frameOrderRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IFrameSupplierRepository _frameSupplierRepository;
        private readonly ISynologenMemberService _synologenMemberService;
        private readonly ISynologenSettingsService _synologenSettingsService;
        private readonly IRoutingService _routingService;

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

        public void Supplier_Selected(object sender, ISupplierSelectedEventArgs e)
        {
            if (e.SelectedSupplierId <= 0)
            {
                return;
            }

            View.Model.SelectedSupplierId = e.SelectedSupplierId;
            View.Model.SelectedFrameId = 0;
            View.Model.SelectedGlassTypeId = 0;

            View.Model.FramesList = GetFrames(e.SelectedSupplierId);
            View.Model.GlassTypesList = GetGlassTypes(e.SelectedSupplierId);
        }

	    public void GlassType_Selected(object sender, IGlassTypeSelectedEventArgs e)
	    {
	        if (e.SelectedGlassTypeId <= 0)
	        {
	            return;
	        }

	        var glassType = _frameGlassTypeRepository.Get(e.SelectedGlassTypeId);
	        View.Model.SelectedGlassTypeId = e.SelectedGlassTypeId;
	        View.Model.HeightParametersEnabled = glassType.IncludeHeightParametersInOrder;
	        View.Model.AdditionParametersEnabled = glassType.IncludeAdditionParametersInOrder;
	        View.Model.Sphere.Set(e.SelectedSphere, glassType.Sphere.GetList(), "Sfär");
	        View.Model.Cylinder.Set(e.SelectedCylinder, glassType.Cylinder.GetList(), "Cylinder");

	        if (glassType.IncludeAdditionParametersInOrder)
	        {
	            View.Model.Addition.Set(e.SelectedAddition, _synologenSettingsService.Addition.GetList(), "Addition");
	        }

	        if (glassType.IncludeHeightParametersInOrder)
	        {
	            View.Model.Height.Set(e.SelectedHeight, _synologenSettingsService.Height.GetList(), "Höjd");
	        }
	    }

	    public void Frame_Selected(object sender, IFrameSelectedEventArgs e)
	    {
	        if (e.SelectedFrameId <= 0)
	        {
	            return;
	        }

	        var frame = _frameRepository.Get(e.SelectedFrameId);
	        View.Model.SelectedFrameId = e.SelectedFrameId;
	        View.Model.PupillaryDistance.Set(e.SelectedPupillaryDistance, frame.PupillaryDistance.GetList(), "PD");
	    }

	    public void View_Load(object sender, EventArgs e)
	    {
	        View.Model.SupplierList = GetSuppliers();
            View.Model.FramesList = new List<Frame>().ToFrameViewList().InsertFirst(DefaultFrame);
            View.Model.PupillaryDistance = EmptyIntervalList.CreateDefaultEyeParameter("PD");
            View.Model.Sphere = EmptyIntervalList.CreateDefaultEyeParameter("Sfär");
            View.Model.Cylinder = EmptyIntervalList.CreateDefaultEyeParameter("Cylinder");
            View.Model.GlassTypesList = new List<FrameGlassType>().ToFrameGlassTypeViewList().InsertFirst(DefaultGlassType);
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

	        if (frameOrderId <= 0)
	        {
	            return;
	        }

	        var frameOrder = _frameOrderRepository.Get(frameOrderId);
	        UpdateModel(frameOrder);
		}

		public void View_SumbitForm(object sender, EditFrameFormEventArgs e) 
		{
		    if (!e.PageIsValid)
		    {
		        return;
		    }

		    var frameOrderId = SaveUpdateFrameOrder(e);
		    if (View.RedirectPageId > 0)
		    {
		        var url = _routingService.GetPageUrl(View.RedirectPageId);
		        url = string.Concat(url, "?frameorder=", frameOrderId);
		        HttpContext.Response.Redirect(url);
		    }
		}

		public override void ReleaseView()
		{
		}

        protected IEnumerable<EntityListItem> GetFrames(int selectedSupplierId)
        {
            return _frameRepository
                .FindBy(new AllOrderableFramesCriteria(selectedSupplierId))
                .ToFrameViewList()
                .InsertFirst(DefaultFrame);
        }

        protected IEnumerable<EntityListItem> GetGlassTypes(int selectedSupplierId)
        {
            return _frameGlassTypeRepository
                .GetAll()
                .Where(x => x.Supplier.Id == selectedSupplierId)
                .OrderBy(x => x.Name).ToFrameGlassTypeViewList()
                .InsertFirst(DefaultGlassType);
        }

        protected IEnumerable<EntityListItem> GetSuppliers()
        {
            return _frameSupplierRepository
                .GetAll().ToFrameSupplierList()
                .OrderBy(x => x.Name)
                .InsertFirst(DefaultSupplier);
        }

        protected void InitiateEventHandlers()
        {
            View.Load += View_Load;
            View.SupplierSelected += Supplier_Selected;

            View.FrameSelected += Supplier_Selected;
            View.FrameSelected += GlassType_Selected;
            View.FrameSelected += Frame_Selected;

            View.GlassTypeSelected += Supplier_Selected;
            View.GlassTypeSelected += GlassType_Selected;
            View.GlassTypeSelected += Frame_Selected;

            View.SubmitForm += View_SumbitForm;
        }

        protected void UpdateModel(FrameOrder frameOrder)
        {
            if (frameOrder == null)
            {
                View.Model.OrderDoesNotExist = true;
                return;
            }

            View.Model.FramesList = GetFrames(frameOrder.Frame.Supplier.Id);
            View.Model.GlassTypesList = GetGlassTypes(frameOrder.Supplier.Id);
            View.Model.PupillaryDistance = frameOrder.GetEyeParameter(x => x.PupillaryDistance, frameOrder.Frame.PupillaryDistance.GetList(), "PD");
            View.Model.Sphere = frameOrder.GetEyeParameter(x => x.Sphere, frameOrder.GlassType.Sphere.GetList(), "Sfär");
            View.Model.Cylinder = frameOrder.GetEyeParameter(x => x.Cylinder, frameOrder.GlassType.Cylinder.GetList(), "Cylinder");
            View.Model.HeightParametersEnabled = frameOrder.GlassType.IncludeHeightParametersInOrder;
            View.Model.AdditionParametersEnabled = frameOrder.GlassType.IncludeAdditionParametersInOrder;
            View.Model.SelectedFrameId = frameOrder.Frame.Id;
            View.Model.SelectedSupplierId = frameOrder.Frame.Supplier.Id;
            View.Model.SelectedGlassTypeId = frameOrder.GlassType.Id;
            View.Model.AxisSelectionLeft = (frameOrder.Axis == null || frameOrder.Axis.Left == null) ? (int?)null : frameOrder.Axis.Left.Value;
            View.Model.AxisSelectionRight = (frameOrder.Axis == null || frameOrder.Axis.Right == null) ? (int?)null : frameOrder.Axis.Right.Value;
            View.Model.Reference = frameOrder.Reference;

            View.Model.AxisValueLeftIsRequired = View.Model.Cylinder.Selection.Left != int.MinValue;
            View.Model.AxisValueRightIsRequired = View.Model.Cylinder.Selection.Left != int.MinValue;

            if (frameOrder.GlassType.IncludeAdditionParametersInOrder)
            {
                View.Model.Addition = frameOrder.GetEyeParameter(x => x.Addition, _synologenSettingsService.Addition.GetList(), "Addition");
            }

            if (frameOrder.GlassType.IncludeHeightParametersInOrder)
            {
                View.Model.Height = frameOrder.GetEyeParameter(x => x.Height, _synologenSettingsService.Height.GetList(), "Höjd");
            }

            if (frameOrder.OrderingShop.Id != _synologenMemberService.GetCurrentShopId())
            {
                View.Model.UserDoesNotHaveAccessToThisOrder = true;
                return;
            }

            View.Model.OrderHasBeenSent = frameOrder.Sent.HasValue;
        }

		protected int SaveUpdateFrameOrder(EditFrameFormEventArgs e) 
        {
			var frameOrderId = GetFrameOrderId();
		    FrameOrder frameOrder;
		    if (frameOrderId > 0)
		    {
		        var previouslySavedFrameOrder = _frameOrderRepository.Get(frameOrderId);
				var frame = _frameRepository.Get(e.SelectedFrameId);
				var glassType = _frameGlassTypeRepository.Get(e.SelectedGlassTypeId);               
				frameOrder = e.FillFrameOrder(frame, glassType, previouslySavedFrameOrder);
			}
			else
			{
                var frame = _frameRepository.Get(e.SelectedFrameId);
				var glassType = _frameGlassTypeRepository.Get(e.SelectedGlassTypeId);
				var shopId = _synologenMemberService.GetCurrentShopId();
				var shop = _shopRepository.Get(shopId);
				frameOrder = e.ToFrameOrder(frame, glassType, shop);
			}

			_frameOrderRepository.Save(frameOrder);
			return frameOrder.Id;
		}

		protected int GetFrameOrderId()
		{
			int integerframeOrderId;
		    var frameOrderId = HttpContext.Request.Params["frameorder"];
		    if (frameOrderId == null)
		    {
		        return -1;
		    }

		    return int.TryParse(frameOrderId, out integerframeOrderId) ? integerframeOrderId : -1;
		}
	}
}