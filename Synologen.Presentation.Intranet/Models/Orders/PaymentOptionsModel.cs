using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class PaymentOptionsModel
    {
    	public PaymentOptionsModel()
    	{
            SubscriptionsItems = new List<SubscriptionItemListModel>();
    	}

        public IList<SubscriptionItemListModel> SubscriptionsItems { get; set; }
    	public string CustomerName { get; set; }
        public int SelectedOption { get; set; }

        public string RenderChecked(Func<PaymentOptionsModel, bool> predicate)
        {
            return RenderChecked(predicate(this));
        }

        public string RenderChecked(bool predicate)
        {
            return predicate ? "checked" : string.Empty;
        }
    }

    public class SubscriptionItemListModel
    {
        public SubscriptionItemListModel()
        {
            RowSpan = 1;
            IsDefault = true;
            Title = "Skapa nytt konto";
            SubscriptionId = 0;
            IsFirstInList = true;
        }

        public SubscriptionItemListModel(Subscription subscription, SubscriptionItem subscriptionItem, int index) : this()
        {
            Name = subscriptionItem.Title;
            Created = subscriptionItem.CreatedDate.ToShortDateString();
            Withdrawals = subscriptionItem.IsOngoing
                ? subscriptionItem.PerformedWithdrawals.ToString()
                : string.Format("{0}/{1}", subscriptionItem.PerformedWithdrawals, subscriptionItem.WithdrawalsLimit);
            IsFirstInList = index == 0;
            SubscriptionId = subscription.Id;
            RowSpan = subscription.SubscriptionItems.Count();
            IsDefault = false;
            Title = "{AccountNumber} ({ConsentStatus})".ReplaceWith(new { AccountNumber = subscription.BankAccountNumber, ConsentStatus = subscription.ConsentStatus.GetEnumDisplayName() });
        }

        public string Title { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }
        public string Withdrawals { get; set; }
        public int SubscriptionId { get; set; }
        public int RowSpan { get; set; }
        public bool IsFirstInList { get; set; }
        public bool IsDefault { get; set; }
    }
}