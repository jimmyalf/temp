using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class EditSubscriptionPresenter : OrderBasePresenter<IEditSubscriptionView> 
	{
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly IRoutingService _routingService;

		public EditSubscriptionPresenter(
			IEditSubscriptionView view,
			ISubscriptionRepository subscriptionRepository,
			ISynologenMemberService synologenMemberService,
			IRoutingService routingService) : base(view, synologenMemberService)
		{
			_subscriptionRepository = subscriptionRepository;
			_routingService = routingService;
			View.Load += Load;
			View.ResetSubscription += ResetSubscription;
		}

		public void Load(object sender, EventArgs e)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			CheckAccess(subscription.Shop);
			View.Model.Initialize(subscription);
			View.Model.ReturnUrl = _routingService.GetPageUrl(View.ReturnPageId, new {subscription = subscription.Id});
		}

		public void ResetSubscription(object sender, ResetSubscriptonEventArgs e)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			subscription.BankAccountNumber = e.BankAccountNumber;
			subscription.ClearingNumber = e.ClearingNumber;
			subscription.ConsentStatus = SubscriptionConsentStatus.NotSent;
			_subscriptionRepository.Save(subscription);
			var url = _routingService.GetPageUrl(View.ReturnPageId, new {subscription = subscription.Id});
			HttpContext.Response.Redirect(url);
		}

		public override void ReleaseView()
		{
			View.Load -= Load;
		}

		private int? RequestSubscriptionId
		{
			get { return HttpContext.Request.Params["subscription"].ToNullableInt(); }
		}
	}
}