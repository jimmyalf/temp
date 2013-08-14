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
			ISynologenMemberService synologenMemberService) 
            : base(view, synologenMemberService)
		{
			_subscriptionItemRepository = subscriptionItemRepository;
			_routingService = routingService;
			View.Load += View_Load;
			View.Submit += View_Submit;
		    View.Start += StartSubscriptionItem;
            View.Stop += StopSubscriptionItem;
		}

        private int? RequestSubScriptionItem
        {
            get { return HttpContext.Request.Params["subscription-item"].ToNullableInt(); }
        }

		public void View_Load(object sender, EventArgs args)
		{
		    if (!RequestSubScriptionItem.HasValue)
		    {
		        return;
		    }

		    var subscriptionItem = _subscriptionItemRepository.Get(RequestSubScriptionItem.Value);
		    CheckAccess(subscriptionItem.Subscription.Shop);
			UpdateViewModel(subscriptionItem);
		}

		public void View_Submit(object sender, SubmitSubscriptionItemEventArgs args)
		{
		    if (!RequestSubScriptionItem.HasValue)
		    {
		        return;
		    }

		    var subscriptionItem = _subscriptionItemRepository.Get(RequestSubScriptionItem.Value);
		    if (subscriptionItem.IsOngoing)
			{
				subscriptionItem.Setup(args.CustomMonthlyProductAmount.Value, args.CustomMonthlyFeeAmount.Value, args.ProductAmount, args.FeeAmount);
			}
			else
			{
				subscriptionItem.Setup(args.WithdrawalsLimit.Value, args.ProductAmount, args.FeeAmount);	
			}

		    subscriptionItem.Title = args.Title;
			_subscriptionItemRepository.Save(subscriptionItem);
			UpdateViewModel(subscriptionItem);
		}

        public void StartSubscriptionItem(object sender, EventArgs args)
        {
            if (!RequestSubScriptionItem.HasValue)
            {
                return;
            }

            var subscriptionItem = _subscriptionItemRepository.Get(RequestSubScriptionItem.Value);
            subscriptionItem.Start();
            _subscriptionItemRepository.Save(subscriptionItem);
            UpdateViewModel(subscriptionItem);
        }

        public void StopSubscriptionItem(object sender, EventArgs args)
        {
            if (!RequestSubScriptionItem.HasValue)
            {
                return;
            }

            var subscriptionItem = _subscriptionItemRepository.Get(RequestSubScriptionItem.Value);
            subscriptionItem.Stop();
            _subscriptionItemRepository.Save(subscriptionItem);
            UpdateViewModel(subscriptionItem);
        }

        public override void ReleaseView()
        {
            View.Load -= View_Load;
            View.Submit -= View_Submit;
            View.Start -= StartSubscriptionItem;
            View.Stop -= StopSubscriptionItem;
        }

		private void UpdateViewModel(SubscriptionItem subscriptionItem)
		{
			var subscription = subscriptionItem.Subscription;
			var returnUrl = _routingService.GetPageUrl(View.ReturnPageId, new { subscription = subscription.Id });
			View.Model.Initialize(subscriptionItem, subscription, returnUrl);
		}
	}
}