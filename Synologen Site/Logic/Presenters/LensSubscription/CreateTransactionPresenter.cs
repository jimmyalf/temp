using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class CreateTransactionPresenter : Presenter<ICreateTransactionView>
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly ISubscriptionRepository _subscriptionRepository;

		public CreateTransactionPresenter(ICreateTransactionView view, ITransactionRepository transactionRepository, ISubscriptionRepository subscriptionRepository) : base(view)
		{
			_transactionRepository = transactionRepository;
			_subscriptionRepository = subscriptionRepository;
			View.Load += View_Load;
			View.Submit += View_Submit;
			View.SetReasonToCorrection += View_SetReason;
			View.SetReasonToWithdrawal += View_SetReason;
			View.Cancel += View_Cancel;
			View.FormUpdate += Form_Update;
		}

		public void Form_Update(object sender, UpdateTransactionModelEventArgs e) 
		{
			View.Model.Amount = e.Amount;
			View.Model.SelectedTransactionType = e.TransactionType;
		}

		public void View_Load(object sender, EventArgs e)
		{
			var reasonId = HttpContext.Request.Params["reason"];
			if (string.IsNullOrEmpty(reasonId)) return;
			if (reasonId == TransactionReason.Withdrawal.ToNumberString())
			{
				View.Model.Reason = TransactionReason.Withdrawal;
				View.Model.Type = TransactionType.Withdrawal;
			}
			else if (reasonId == TransactionReason.Correction.ToNumberString())
			{
				View.Model.Reason = TransactionReason.Correction;
			}
		}

		public void View_SetReason(object sender, TransactionReasonEventArgs args)
		{
			RedirectToCurrentPage(args.Reason.ToNumberString());
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.Submit -= View_Submit;
			View.SetReasonToCorrection -= View_SetReason;
			View.SetReasonToWithdrawal -= View_SetReason;
			View.Cancel -= View_Cancel;
			View.FormUpdate -= Form_Update;
		}

		public void View_Submit(object sender, SaveTransactionEventArgs args)
		{
			var subscriptionId = HttpContext.Request.Params["subscription"].ToIntOrDefault();
			if (subscriptionId <= 0) return;
			var subscription = _subscriptionRepository.Get(subscriptionId);
			var transactionToSave = new SubscriptionTransaction
			{
				Amount = args.Amount,
				Type = (TransactionType) Enum.Parse(typeof (TransactionType), args.TransactionType),
				Reason = (TransactionReason) Enum.Parse(typeof (TransactionReason), args.TransactionReason),
				CreatedDate = DateTime.Now,
				Subscription = subscription
			};
			_transactionRepository.Save(transactionToSave);

			RedirectToCurrentPage(subscriptionId);
			
		}

		public void View_Cancel(object sender, EventArgs e)
		{
			var subscriptionId = HttpContext.Request.Params["subscription"].ToIntOrDefault();
			if (subscriptionId <= 0) return;
			RedirectToCurrentPage(subscriptionId);
		}

		public void RedirectToCurrentPage()
		{
			HttpContext.Response.Redirect(HttpContext.Request.Url.PathAndQuery);
		}

		public void RedirectToCurrentPage(string reasonId)
		{
			HttpContext.Response.Redirect(string.Format(HttpContext.Request.Url.PathAndQuery + "&reason={0}", reasonId));
		}

		public void RedirectToCurrentPage(int subscriptionId)
		{
			HttpContext.Response.Redirect(String.Format(HttpContext.Request.Url.AbsolutePath + "?subscription={0}", subscriptionId));
		}
		
	}
}
