using System;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class SubscriptionPresenter : Presenter<ISubscriptionView> 
	{
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly ISubscriptionErrorRepository _subscriptionErrorRepository;
		private readonly IRoutingService _routingService;

		public SubscriptionPresenter(
			ISubscriptionView view, 
			ISubscriptionRepository subscriptionRepository, 
			ISubscriptionErrorRepository subscriptionErrorRepository,
			IRoutingService routingService
			) : base(view)
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
			UpdateViewModel(RequestSubscriptionId.Value);
		}


		public void Handle_Error(object sender, HandleErrorEventArgs handleErrorEventArgs)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var error = _subscriptionErrorRepository.Get(handleErrorEventArgs.ErrorId);
			error.HandledDate = DateTime.Now;
			_subscriptionErrorRepository.Save(error);
			UpdateViewModel(RequestSubscriptionId.Value);
		}

		public void Start_Subscription(object sender, EventArgs eventArgs)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			subscription.Active = true;
			_subscriptionRepository.Save(subscription);
			UpdateViewModel(RequestSubscriptionId.Value);
		}

		public void Stop_Subscription(object o, EventArgs eventArgs)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			subscription.Active = false;
			_subscriptionRepository.Save(subscription);
			UpdateViewModel(RequestSubscriptionId.Value);
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

		private void UpdateViewModel(int subscriptionId)
		{
			var subscription = _subscriptionRepository.Get(subscriptionId);
			var returnUrl = _routingService.GetPageUrl(View.ReturnPageId);
			var subscriptionItemDetailUrl = _routingService.GetPageUrl(View.SubscriptionItemDetailPageId);
			View.Model.Initialize(subscription, returnUrl, subscriptionItemDetailUrl);
		}
	}
}