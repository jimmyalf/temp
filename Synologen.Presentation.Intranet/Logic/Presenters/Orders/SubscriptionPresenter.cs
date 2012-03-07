using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class SubscriptionPresenter : OrderBasePresenter<ISubscriptionView> 
	{
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly ISubscriptionErrorRepository _subscriptionErrorRepository;
		private readonly IRoutingService _routingService;

		public SubscriptionPresenter(
			ISubscriptionView view, 
			ISubscriptionRepository subscriptionRepository, 
			ISubscriptionErrorRepository subscriptionErrorRepository,
			IRoutingService routingService,
			ISynologenMemberService synologenMemberService
			) : base(view, synologenMemberService)
		{
			_subscriptionRepository = subscriptionRepository;
			_subscriptionErrorRepository = subscriptionErrorRepository;
			_routingService = routingService;
			View.Load += View_Load;
			View.HandleError += Handle_Error;
			View.StartSubscription += Start_Subscription;
			View.StopSubscription += Stop_Subscription;
		}

		public void View_Load(object sender, EventArgs e)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			CheckAccess(subscription.Shop);
			UpdateViewModel(subscription);
		}


		public void Handle_Error(object sender, HandleErrorEventArgs handleErrorEventArgs)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var error = _subscriptionErrorRepository.Get(handleErrorEventArgs.ErrorId);
			error.HandledDate = DateTime.Now;
			_subscriptionErrorRepository.Save(error);
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			UpdateViewModel(subscription);
		}

		public void Start_Subscription(object sender, EventArgs eventArgs)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			subscription.Active = true;
			_subscriptionRepository.Save(subscription);
			UpdateViewModel(subscription);
		}

		public void Stop_Subscription(object o, EventArgs eventArgs)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			subscription.Active = false;
			_subscriptionRepository.Save(subscription);
			UpdateViewModel(subscription);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.HandleError -= Handle_Error;
			View.StartSubscription -= Start_Subscription;
			View.StopSubscription -= Stop_Subscription;
		}

    	private int? RequestSubscriptionId
    	{
    		get { return HttpContext.Request.Params["subscription"].ToNullableInt(); }
    	}

		private void UpdateViewModel(Subscription subscription)
		{
			var returnUrl = _routingService.GetPageUrl(View.ReturnPageId);
			var subscriptionItemUrl = _routingService.GetPageUrl(View.SubscriptionItemDetailPageId);
			var correctionUrl = _routingService.GetPageUrl(View.CorrectionPageId, new {subscription = subscription.Id});
			var resetUrl = _routingService.GetPageUrl(View.SubscriptionResetPageId, new {subscription = subscription.Id});
			View.Model.Initialize(subscription, returnUrl, subscriptionItemUrl, correctionUrl, resetUrl);
		}
	}
}