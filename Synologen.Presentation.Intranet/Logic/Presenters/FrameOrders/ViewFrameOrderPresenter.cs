using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.FrameOrders
{
	public class ViewFrameOrderPresenter : Presenter<IViewFrameOrderView<ViewFrameOrderModel>>
	{
		private readonly IFrameOrderRepository _frameOrderRepository;
		private readonly ISynologenMemberService _synologenMemberService;
		private readonly IFrameOrderService _frameOrderService;
		private readonly IRoutingService _routingService;

		public ViewFrameOrderPresenter(
			IViewFrameOrderView<ViewFrameOrderModel> view, 
			IFrameOrderRepository frameOrderRepository, 
			ISynologenMemberService synologenMemberService, 
			IFrameOrderService frameOrderService,
			IRoutingService routingService) : base(view)
		{
			_frameOrderRepository = frameOrderRepository;
			_synologenMemberService = synologenMemberService;
			_frameOrderService = frameOrderService;
			_routingService = routingService;
			InitiateEventHandlers();
		}

		private void InitiateEventHandlers()
		{
			View.Load += View_Load;
			View.SendOrder += View_SendOrder;
		}


		public void View_Load(object sender, EventArgs e)
		{
			InitializeModel();
		}

		public void View_SendOrder(object sender, EventArgs e) 
		{
			var frameOrderId = GetFrameOrderId();
			var frameOrder = _frameOrderRepository.Get(frameOrderId);
			_frameOrderService.SendOrder(frameOrder);
			SetFrameOrderSent(frameOrder);
			if (View.RedirectAfterSentOrderPageId > 0)
			{
				var url = _routingService.GetPageUrl(View.RedirectAfterSentOrderPageId);
				HttpContext.Response.Redirect(url);
			}
		}


		private void InitializeModel() {
			var frameOrderId = GetFrameOrderId();
			var frameOrder = _frameOrderRepository.Get(frameOrderId);
			View.Model.ShopDoesNotHaveAccessToFrameOrders = !_synologenMemberService.ShopHasAccessTo(ShopAccess.SlimJim);
			if(frameOrder == null)
			{
				View.Model.OrderDoesNotExist = true;
				return;
			}
			if(frameOrder.OrderingShop.Id != _synologenMemberService.GetCurrentShopId())
			{
				View.Model.UserDoesNotHaveAccessToThisOrder = true;
				return;
			}
			if(frameOrder.Addition != null){
				View.Model.AdditionLeft = frameOrder.Addition.Left;
				View.Model.AdditionRight = frameOrder.Addition.Right;
			}
			if(frameOrder.Axis != null)
			{
				View.Model.AxisSelectionLeft = frameOrder.Axis.Left;
				View.Model.AxisSelectionRight = frameOrder.Axis.Right;	
			}
			View.Model.CreatedDate = frameOrder.Created.ToString("yyyy-MM-dd HH:mm");
			if(frameOrder.Cylinder != null)
			{
				View.Model.CylinderLeft = frameOrder.Cylinder.Left;
				View.Model.CylinderRight = frameOrder.Cylinder.Right;
			}
		    View.Model.SupplierName = frameOrder.Frame.Supplier.Name;
			View.Model.FrameArticleNumber = frameOrder.Frame.ArticleNumber;
			View.Model.FrameColor = frameOrder.Frame.Color.Name;
			View.Model.FrameBrand = frameOrder.Frame.Brand.Name;
			View.Model.FrameName = frameOrder.Frame.Name;
			View.Model.GlassTypeName = frameOrder.GlassType.Name;
			if (frameOrder.Height != null)
			{
				View.Model.HeightLeft = frameOrder.Height.Left;
				View.Model.HeightRight = frameOrder.Height.Right;
			}
			View.Model.Reference = frameOrder.Reference;
			View.Model.PupillaryDistanceLeft = frameOrder.PupillaryDistance.Left;
			View.Model.PupillaryDistanceRight = frameOrder.PupillaryDistance.Right;
			View.Model.SentDate = frameOrder.Sent.HasValue ? frameOrder.Sent.Value.ToString("yyyy-MM-dd HH:mm") : null;
			View.Model.ShopCity = frameOrder.OrderingShop.Address.City;
			View.Model.ShopName = frameOrder.OrderingShop.Name;
			View.Model.SphereLeft = frameOrder.Sphere.Left;
			View.Model.SphereRight = frameOrder.Sphere.Right;
			View.Model.OrderHasBeenSent = (frameOrder.Sent.HasValue);
			if(View.EditPageId>0)
			{
				var url = _routingService.GetPageUrl(View.EditPageId);
				View.Model.EditPageUrl = String.Concat(url, "?frameorder=", frameOrderId);
			}
		}

		private void SetFrameOrderSent(FrameOrder frameOrder)
		{
			frameOrder.Sent = DateTime.Now;
			_frameOrderRepository.Save(frameOrder);
		}


		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.SendOrder -= View_SendOrder;
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