using System;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class EditLensSubscriptionPresenter : Presenter<IEditLensSubscriptionView>
	{
		private readonly ISubscriptionRepository _subscriptionRepository;

		public EditLensSubscriptionPresenter(IEditLensSubscriptionView view, ISubscriptionRepository subscriptionRepository) : base(view)
		{
			_subscriptionRepository = subscriptionRepository;
			View.Load += View_Load;
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
		public void View_Load(object sender, EventArgs e)
		{
			var subscriptionId = HttpContext.Request.Params["subscription"].ToIntOrDefault();
			var subscription = _subscriptionRepository.Get(subscriptionId);
			View.Model.AccountNumber = subscription.PaymentInfo.AccountNumber;
			View.Model.ActivatedDate = subscription.With(x => x.ActivatedDate).Return(x => x.Value.ToString("yyyy-MM-dd"), String.Empty);
			View.Model.ClearingNumber = subscription.PaymentInfo.ClearingNumber;
			View.Model.CreatedDate = subscription.CreatedDate.ToString("yyyy-MM-dd");
			View.Model.CustomerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
			View.Model.MonthlyAmount = subscription.PaymentInfo.MonthlyAmount;
			View.Model.Status = subscription.Status.GetEnumDisplayName();
		}
	}
}