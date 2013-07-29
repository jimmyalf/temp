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
    		Subscriptions = new List<SubscriptionListItemModel>();
    	}

        public IList<SubscriptionListItemModel> Subscriptions { get; set; }
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

    public class SubscriptionListItemModel
    {
        public SubscriptionListItemModel() { }
        public SubscriptionListItemModel(Subscription subscription)
        {
            SubscriptionItems = subscription.SubscriptionItems
                .Select((x, index) => new SubscriptionItemListItemModel(x, index))
                .ToList();
            Title = "{AccountNumber} ({ConsentStatus})".ReplaceWith(new { AccountNumber = subscription.BankAccountNumber, ConsentStatus = subscription.ConsentStatus.GetEnumDisplayName() });
            Id = subscription.Id;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public IList<SubscriptionItemListItemModel> SubscriptionItems { get; set; }
    }

    public class SubscriptionItemListItemModel
    {
        public SubscriptionItemListItemModel() { }
        public SubscriptionItemListItemModel(SubscriptionItem subscriptionItem, int index)
        {
            Name = subscriptionItem.Title ?? "Namnl�s";
            Created = subscriptionItem.CreatedDate.ToShortDateString();
            Withdrawals = subscriptionItem.IsOngoing 
                ? subscriptionItem.PerformedWithdrawals.ToString()
                : string.Format("{0}/{1}", subscriptionItem.PerformedWithdrawals, subscriptionItem.WithdrawalsLimit);
            Index = index;
        }

        public string Name { get; set; }
        public string Created { get; set; }
        public string Withdrawals { get; set; }
        public int Index { get; protected set; }
    }
}