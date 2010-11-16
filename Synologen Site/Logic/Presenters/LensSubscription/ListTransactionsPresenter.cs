using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class ListTransactionsPresenter : Presenter<IListTransactionView>
	{
		private readonly ISubscriptionRepository _subscriptionRepository;

		public ListTransactionsPresenter(IListTransactionView view, 
										ISubscriptionRepository subscriptionRepository)
			: base(view)
		{
			_subscriptionRepository = subscriptionRepository;
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			Func<SubscriptionTransaction, SubscriptionTransactionListItemModel> transactionConverter = (transaction) =>
				new SubscriptionTransactionListItemModel
				{
					CreatedDate = transaction.CreatedDate.ToString("yyyy-MM-dd"),
					Amount = transaction.Amount,
					Reason = transaction.Reason.GetEnumDisplayName(),
					Type = transaction.Type.GetEnumDisplayName()
				};
			var subscriptionId = HttpContext.Request.Params["subscription"].ToIntOrDefault();
			var subscription = _subscriptionRepository.Get(subscriptionId);
			View.Model.List = subscription.Transactions.Select(transactionConverter);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}
