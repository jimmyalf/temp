using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters
{
	public class ListFrameOrdersPresenter : Presenter<IListFrameOrdersView<ListFrameOrdersModel>>
	{
		private readonly IFrameOrderRepository _frameOrderRepository;
		private readonly ISynologenMemberService _synologenMemberService;

		public ListFrameOrdersPresenter(IListFrameOrdersView<ListFrameOrdersModel> view, IFrameOrderRepository frameOrderReposityory , ISynologenMemberService synologenMemberService) : base(view)
		{
			_frameOrderRepository = frameOrderReposityory;
			_synologenMemberService = synologenMemberService;
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
				View.Model.ViewPageUrl = _synologenMemberService.GetPageUrl(View.ViewPageId);
			}
			View.Model.ShopDoesNotHaveAccessToFrameOrders = !_synologenMemberService.ShopHasAccessTo(ShopAccess.SlimJim);
		}
	}
}