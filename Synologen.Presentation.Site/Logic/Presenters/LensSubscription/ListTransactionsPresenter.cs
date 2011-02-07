using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class ListTransactionsPresenter : Presenter<IListTransactionView>
	{
		private readonly ITransactionRepository _transactionRepository;

		public ListTransactionsPresenter(IListTransactionView view, 
										ITransactionRepository transactionRepository)
			: base(view)
		{
			_transactionRepository = transactionRepository;
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			Func<SubscriptionTransaction, SubscriptionTransactionListItemModel> transactionConverter = transaction => new SubscriptionTransactionListItemModel
			{
				CreatedDate = transaction.CreatedDate.ToString("yyyy-MM-dd"),
				Amount = transaction.Amount,
				Reason = transaction.Reason.GetEnumDisplayName(),
				Type = transaction.Type.GetEnumDisplayName(),
				HasSettlement = (transaction.Settlement != null) ? "Ja" : String.Empty
			};
			var subscriptionId = HttpContext.Request.Params["subscription"].ToIntOrDefault();
			
			var criteria = new TransactionsForSubscriptionMatchingCriteria { SubscriptionId = subscriptionId };
			View.Model.List = _transactionRepository.FindBy(criteria).Select(transactionConverter);
			View.Model.HasTransactions = (View.Model.List.Count() > 0);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}
