using System;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
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

		public SubscriptionPresenter(
			ISubscriptionView view, 
			ISubscriptionRepository subscriptionRepository, 
			ISubscriptionErrorRepository subscriptionErrorRepository
			) : base(view)
		{
			_subscriptionRepository = subscriptionRepository;
			_subscriptionErrorRepository = subscriptionErrorRepository;
			View.Load += View_Load;
			View.HandleError += Handle_Error;
		}

		public void View_Load(object sender, EventArgs e)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var subscription = _subscriptionRepository.Get(RequestSubscriptionId.Value);
			View.Model.Initialize(subscription);
		}

		public void Handle_Error(object sender, HandleErrorEventArgs handleErrorEventArgs)
		{
			var error = _subscriptionErrorRepository.Get(handleErrorEventArgs.ErrorId);
			error.HandledDate = DateTime.Now;
			_subscriptionErrorRepository.Save(error);
			View_Load(sender, new EventArgs());
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.HandleError -= Handle_Error;
		}

    	private int? RequestSubscriptionId
    	{
    		get { return HttpContext.Request.Params["subscription"].ToNullableInt(); }
    	}
	}
}