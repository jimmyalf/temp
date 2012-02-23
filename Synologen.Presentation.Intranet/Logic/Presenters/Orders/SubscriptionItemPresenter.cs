using System;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class SubscriptionItemPresenter : Presenter<ISubscriptionItemView> 
	{
		private readonly ISubscriptionItemRepository _subscriptionItemRepository;
		private readonly IRoutingService _routingService;

		public SubscriptionItemPresenter(
			ISubscriptionItemView view, 
			ISubscriptionItemRepository subscriptionItemRepository,
			IRoutingService routingService
			) : base(view)
		{
			_subscriptionItemRepository = subscriptionItemRepository;
			_routingService = routingService;
			View.Load += View_Load;
			View.Submit += View_Submit;
		}

		public void View_Load(object sender, EventArgs args)
		{
			if(!RequestSubScriptionItem.HasValue) return;
			UpdateViewModel(RequestSubScriptionItem.Value);
		}

		public void View_Submit(object sender, SubmitSubscriptionItemEventArgs args)
		{
			if(!RequestSubScriptionItem.HasValue) return;
			var subscriptionItem = _subscriptionItemRepository.Get(RequestSubScriptionItem.Value);
			subscriptionItem.TaxFreeAmount = args.TaxFreeAmount;
			subscriptionItem.TaxedAmount = args.TaxedAmount;
			subscriptionItem.WithdrawalsLimit = args.WithdrawalsLimit;
			_subscriptionItemRepository.Save(subscriptionItem);
			UpdateViewModel(RequestSubScriptionItem.Value);
		}

		private void UpdateViewModel(int subscriptionItemId)
		{
			var subscriptionItem = _subscriptionItemRepository.Get(subscriptionItemId);
			var subscription = subscriptionItem.Subscription;
			var returnUrl = _routingService.GetPageUrl(View.ReturnPageId) + "?subscription=" + subscription.Id;
			View.Model.Initialize(subscriptionItem, subscriptionItem.Subscription, returnUrl);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.Submit -= View_Submit;
		}

		private int? RequestSubScriptionItem
		{
			get { return HttpContext.Request.Params["subscription-item"].ToNullableInt(); }
		}
	}
}