using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
	public class SubscriptionModel
	{
		public SubscriptionModel() { }
		public string BankAccountNumber { get; set; }
		public string ClearingNumber { get; set; }
		public IEnumerable<SubscriptionItemListItem> SubscriptionItems { get; set; }
		public IEnumerable<TransactionListItem> Transactions { get; set; }
		public IEnumerable<ErrorListItem> Errors { get; set; }
		public bool HasErrors { get { return Errors.Any(); } }
		public string CustomerName { get; set; }
		public string Status { get; set; }
		public string Consented { get; set; }
		public string CreatedDate { get; set; }
		public bool ShowStopButton { get; set; }
		public bool ShowStartButton { get; set; }
        public string TaxedBalance { get; set; }
        public string TaxFreeBalance { get; set; }
		public string ReturnUrl { get; set; }
		public string CorrectionUrl { get; set; }
		public string ResetSubscriptionUrl { get; set; }
		public bool ShowResetDisplayUrl { get; set; }

        public void Initialize(Subscription subscription, string returnUrl, string subscriptionItemUrl, string correctionUrl, string resetUrl)
        {
            SubscriptionItems = subscription.SubscriptionItems.OrEmpty().Select(x => new SubscriptionItemListItem(x, subscriptionItemUrl));
            Transactions = subscription.Transactions.OrderByDescending(x => x.CreatedDate).OrEmpty().Select(x => new TransactionListItem(x));
            Errors = subscription.Errors.OrEmpty().Select(x => new ErrorListItem(x));
            BankAccountNumber = subscription.BankAccountNumber;
            ClearingNumber = subscription.ClearingNumber;
            CustomerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
            Status = subscription.Active ? "Aktivt" : "Vilande autogiro";
            Consented = GetConsentText(subscription);
            CreatedDate = subscription.CreatedDate.ToString("yyyy-MM-dd");
            ReturnUrl = returnUrl;
            CorrectionUrl = correctionUrl;
            ShowStartButton = !subscription.Active;
            ShowStopButton = subscription.Active;
            TaxFreeBalance = subscription.GetCurrentAccountBalance().TaxFree.ToString("C2");
            TaxedBalance = subscription.GetCurrentAccountBalance().Taxed.ToString("C2");

            // Temp döljning till Live-deldeploy av Per.
            TaxedBalance = (subscription.GetCurrentAccountBalance().TaxFree +
                subscription.GetCurrentAccountBalance().Taxed)
                .ToString("C2");
            

            ResetSubscriptionUrl = resetUrl;
            ShowResetDisplayUrl = subscription.ConsentStatus == SubscriptionConsentStatus.Denied;
        }

	    private string GetConsentText(Subscription subscription)
		{
			if(subscription.ConsentedDate.HasValue)
			{
				return "Medgivet " + subscription.ConsentedDate.Value.ToString("yyyy-MM-dd");
			}
			return subscription.ConsentStatus.GetEnumDisplayName();
		}
	}

	public class SubscriptionItemListItem
	{
		public SubscriptionItemListItem() { }
		public SubscriptionItemListItem(SubscriptionItem subscriptionItem, string subscriptionItemDetailUrl)
		{
			MontlyAmount = subscriptionItem.MonthlyWithdrawal.Total.ToString("C2");
			PerformedWithdrawals = GetPerformedWithdrawals(subscriptionItem);
		    Status = subscriptionItem.Status.GetEnumDisplayName();
			SubscriptionItemDetailUrl = subscriptionItemDetailUrl + "?subscription-item=" + subscriptionItem.Id;
			CreatedDate = subscriptionItem.CreatedDate.ToString("yyyy-MM-dd");
		    Title = subscriptionItem.Title;
		}

        public string Status { get; set; }
        public string PerformedWithdrawals { get; set; }
        public string MontlyAmount { get; set; }
        public string SubscriptionItemDetailUrl { get; set; }
        public string CreatedDate { get; set; }
	    public string Title { get; set; }

	    protected virtual string GetPerformedWithdrawals(SubscriptionItem subscriptionItem)
		{
			return subscriptionItem.IsOngoing 
				? subscriptionItem.PerformedWithdrawals.ToString() 
				: "{0}/{1}".FormatWith(subscriptionItem.PerformedWithdrawals, subscriptionItem.WithdrawalsLimit);
		}
	}

	public class TransactionListItem
	{
		public TransactionListItem() { }
		public TransactionListItem(SubscriptionTransaction transaction)
		{
			Amount = GetFormattedAmount(transaction);
			Description = transaction.Reason.GetEnumDisplayName();
			Date = transaction.CreatedDate.ToString("yyyy-MM-dd");
			IsPayed = transaction.SettlementId.HasValue  ? "Ja" : "Nej";
		}

		public string Amount { get; set; }
		public string Description { get; set; }
		public string Date { get; set; }
		public string IsPayed { get; set; }

        private string GetFormattedAmount(SubscriptionTransaction transaction)
        {
            var amount = transaction.Type == TransactionType.Deposit
                ? transaction.GetAmount().Total.ToString("C2")
                : (transaction.GetAmount().Total * -1).ToString("C2");
            switch (transaction.Reason)
            {
                case TransactionReason.Payment: return amount;
                case TransactionReason.Withdrawal: return amount;
                case TransactionReason.Correction: return amount;
                case TransactionReason.PaymentFailed: return "(" + amount + ")";
                default: throw new ArgumentOutOfRangeException();
            }
        }
	}

	public class ErrorListItem
	{
		public ErrorListItem() { }
		public ErrorListItem(SubscriptionError error)
		{
			Id = error.Id;
			IsHandled = error.IsHandled;
			Type = error.Type.GetEnumDisplayName();
			Created = error.CreatedDate.ToString("yyyy-MM-dd");
			Handled = error.HandledDate.HasValue ? error.HandledDate.Value.ToString("yyyy-MM-dd") : null;
		}
		public bool IsHandled { get; set; }
		public string Type { get; set; }
		public string Created { get; set; }
		public string Handled { get; set; }
		public int Id { get; set; }
	}
}
