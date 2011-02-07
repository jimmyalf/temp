using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class ListSubscriptionErrorsPresenter : Presenter<IListSubscriptionErrorView>
	{
		private readonly ISubscriptionErrorRepository _subscriptionErrorRepository;
		private readonly ISubscriptionRepository _subscriptionRepository;

		public ListSubscriptionErrorsPresenter(IListSubscriptionErrorView view, 
												ISubscriptionErrorRepository subscriptionErrorRepository,
												ISubscriptionRepository subscriptionRepository)
			: base(view)
		{
			_subscriptionErrorRepository = subscriptionErrorRepository;
			_subscriptionRepository = subscriptionRepository;
			View.Load += View_Load;
			View.SetHandled += View_SetHandled;
		}

		public void View_Load(object sender, EventArgs e)
		{
			Func<SubscriptionError, SubscriptionErrorListItemModel> errorConverter = error => new SubscriptionErrorListItemModel
     		{
     			CreatedDate = error.CreatedDate.ToString("yyyy-MM-dd"),
     			TypeName = error.Type.GetEnumDisplayName(),
     			HandledDate = error.HandledDate.HasValue ? error.HandledDate.Value.ToString("yyyy-MM-dd") : string.Empty,
     			ErrorId = error.Id.ToString(),
				IsVisible = error.IsHandled ? false : true
			};
			var subscriptionId = HttpContext.Request.Params["subscription"];
			var subscription = _subscriptionRepository.Get(subscriptionId.ToIntOrDefault());
			View.Model.List = subscription.Errors.Select(errorConverter);
			View.Model.HasErrors = (subscription.Errors.Count() > 0); 
		}

		public void View_SetHandled(object sender, SetErrorHandledEventArgs args)
		{
			var subscriptionError = _subscriptionErrorRepository.Get(args.ErrorId);
			subscriptionError.HandledDate = DateTime.Now;
			subscriptionError.IsHandled = true;
			_subscriptionErrorRepository.Save(subscriptionError);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.SetHandled -= View_SetHandled;
		}
	}
}
