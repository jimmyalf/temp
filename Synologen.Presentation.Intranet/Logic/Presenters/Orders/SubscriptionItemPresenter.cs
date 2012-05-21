using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class SubscriptionItemPresenter : OrderBasePresenter<ISubscriptionItemView> 
	{
		private readonly ISubscriptionItemRepository _subscriptionItemRepository;
		private readonly IRoutingService _routingService;

		public SubscriptionItemPresenter(
			ISubscriptionItemView view, 
			ISubscriptionItemRepository subscriptionItemRepository,
			IRoutingService routingService,
			ISynologenMemberService synologenMemberService
			) : base(view, synologenMemberService)
		{
			_subscriptionItemRepository = subscriptionItemRepository;
			_routingService = routingService;
			View.Load += View_Load;
			View.Submit += View_Submit;
		}

		public void View_Load(object sender, EventArgs args)
		{
			if(!RequestSubScriptionItem.HasValue) return;
			var subscriptionItem = _subscriptionItemRepository.Get(RequestSubScriptionItem.Value);
			CheckAccess(subscriptionItem.Subscription.Shop);
			UpdateViewModel(subscriptionItem);
		}

		public void View_Submit(object sender, SubmitSubscriptionItemEventArgs args)
		{
			if(!RequestSubScriptionItem.HasValue) return;
			var subscriptionItem = _subscriptionItemRepository.Get(RequestSubScriptionItem.Value);
			subscriptionItem.ProductPrice = args.ProductAmount;
			subscriptionItem.FeePrice = args.FeeAmount;
			subscriptionItem.WithdrawalsLimit = args.WithdrawalsLimit;
			_subscriptionItemRepository.Save(subscriptionItem);
			UpdateViewModel(subscriptionItem);
		}

		private void UpdateViewModel(SubscriptionItem subscriptionItem)
		{
			var subscription = subscriptionItem.Subscription;
			var returnUrl = _routingService.GetPageUrl(View.ReturnPageId, new {subscription = subscription.Id});
			View.Model.Initialize(subscriptionItem, subscription, returnUrl);
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