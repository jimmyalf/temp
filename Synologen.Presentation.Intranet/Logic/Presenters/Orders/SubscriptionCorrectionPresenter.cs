using System;
using EnsureThat;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class SubscriptionCorrectionPresenter : OrderBasePresenter<ISubscriptionCorrectionView> 
	{
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly IRoutingService _routingService;
		private readonly ITransactionRepository _transactionRepository;

		public SubscriptionCorrectionPresenter(
			ISubscriptionCorrectionView view, 
			ISynologenMemberService synologenMemberService,
			ISubscriptionRepository subscriptionRepository,
			IRoutingService routingService,
			ITransactionRepository transactionRepository) : base(view, synologenMemberService)
		{
			_subscriptionRepository = subscriptionRepository;
			_routingService = routingService;
			_transactionRepository = transactionRepository;
			View.Load += Load;
			View.Submit += Submit;
		}
		
		public void Load(object sender, EventArgs e)
		{
			Ensure.That(RequestSubscriptionId).IsNotNull();
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			CheckAccess(subscription.Shop);
			var returnUrl = _routingService.GetPageUrl(View.ReturnPageId, new {subscription = subscription.Id});
			View.Model.Initialize(subscription, returnUrl);
		}

		public void Submit(object sender, SubmitCorrectionEventArgs e)
		{
			Ensure.That(RequestSubscriptionId).IsNotNull();
			var transaction = CreateNewTransaction(RequestSubscriptionId.Value, e);
			_transactionRepository.Save(transaction);
			var redirectUrl = _routingService.GetPageUrl(View.RedirectOnCreatePageId, new {subscription = RequestSubscriptionId.Value});
			HttpContext.Response.Redirect(redirectUrl);
		}

		public override void ReleaseView()
		{
			View.Submit -= Submit;
			View.Load -= Load;
		}

		private SubscriptionTransaction CreateNewTransaction(int subscriptionId, SubmitCorrectionEventArgs eventArgs)
		{
			return new SubscriptionTransaction
			{
				Amount = eventArgs.Amount,
				Reason = TransactionReason.Correction,
				Subscription = _subscriptionRepository.Get(subscriptionId),
				Type = eventArgs.Type
			};
		}

		private int? RequestSubscriptionId
		{
			get { return HttpContext.Request.Params["subscription"].ToNullableInt(); }
		}
	}
}