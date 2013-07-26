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
    }

    public class SubscriptionListItemModel
    {
        public SubscriptionListItemModel() { }
        public SubscriptionListItemModel(Subscription subscription, int selectedItem)
        {
            SubscriptionItems = subscription.SubscriptionItems
                .Select(x => new SubscriptionItemListItemModel(x))
                .ToList();
            Title = "{AccountNumber} ({ConsentStatus})".ReplaceWith(new { AccountNumber = subscription.BankAccountNumber, ConsentStatus = subscription.ConsentStatus.GetEnumDisplayName() });
            Id = subscription.Id;
            CheckedStatement = subscription.Id == selectedItem ? "checked=\"checked\"" : null;
        }

        public string CheckedStatement { get; protected set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<SubscriptionItemListItemModel> SubscriptionItems { get; set; }
    }

    public class SubscriptionItemListItemModel
    {
        public SubscriptionItemListItemModel() { }
        public SubscriptionItemListItemModel(SubscriptionItem subscriptionItem)
        {
            Name = subscriptionItem.Title ?? "Namnlös";
            Created = subscriptionItem.CreatedDate.ToShortDateString();
            Withdrawals = subscriptionItem.IsOngoing 
                ? subscriptionItem.PerformedWithdrawals.ToString()
                : string.Format("{0}/{1}", subscriptionItem.PerformedWithdrawals, subscriptionItem.WithdrawalsLimit);
        }

        public string Name { get; set; }
        public string Created { get; set; }
        public string Withdrawals { get; set; }
    }
}