using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.FrameOrders
{
	public class ListFrameOrdersPresenter : Presenter<IListFrameOrdersView<ListFrameOrdersModel>>
	{
		private readonly IFrameOrderRepository _frameOrderRepository;
		private readonly ISynologenMemberService _synologenMemberService;
		private readonly IRoutingService _routingService;

		public ListFrameOrdersPresenter(
			IListFrameOrdersView<ListFrameOrdersModel> view, 
			IFrameOrderRepository frameOrderReposityory , 
			ISynologenMemberService synologenMemberService,
			IRoutingService routingService) : base(view)
		{
			_frameOrderRepository = frameOrderReposityory;
			_synologenMemberService = synologenMemberService;
			_routingService = routingService;
			InitiateEventHandlers();
		}

		private void InitiateEventHandlers()
		{
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e) {
			InitializeModel();
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}

		private void InitializeModel() {
			var shopId = _synologenMemberService.GetCurrentShopId();
			var criteria = new AllFrameOrdersForShopCriteria { ShopId = shopId };
			var frameOrders = _frameOrderRepository.FindBy(criteria);
			View.Model.List = frameOrders.ToFrameOrderListItems();
			if(View.ViewPageId > 0)
			{
				View.Model.ViewPageUrl = _routingService.GetPageUrl(View.ViewPageId);
			}
			View.Model.ShopDoesNotHaveAccessToFrameOrders = !_synologenMemberService.ShopHasAccessTo(ShopAccess.SlimJim);
		}
	}
}