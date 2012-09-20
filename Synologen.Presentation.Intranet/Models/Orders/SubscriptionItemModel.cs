using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
	public class SubscriptionItemModel 
	{
		public SubscriptionItemModel() { }
		public void Initialize(SubscriptionItem subscriptionItem, Subscription subscription, string returnUrl)
		{
			Active = subscriptionItem.IsActive ? "Ja" : "Nej";
			ProductPrice = subscriptionItem.ProductPrice;
			FeePrice = subscriptionItem.FeePrice;
			WithdrawalsLimit = subscriptionItem.WithdrawalsLimit ?? 0;
			IsOngoing = !subscriptionItem.WithdrawalsLimit.HasValue;
			CreatedDate = subscriptionItem.CreatedDate.ToString("yyyy-MM-dd");
			NumerOfPerformedWithdrawals = subscriptionItem.PerformedWithdrawals;
			SubscriptionBankAccountNumber = subscription.BankAccountNumber;
			CustomerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
			ReturnUrl = returnUrl;
			MonthlyWithdrawalAmount = subscriptionItem.MonthlyWithdrawalAmount.ToString("C2");
		}
		public string Active { get; set; }
		public decimal ProductPrice { get; set; }
		public decimal FeePrice { get; set; }

		public decimal MonthlyProductPrice { get; set; }
		public decimal MonthlyFeePrice { get; set; }

		public int WithdrawalsLimit { get; set; }
		public string CreatedDate { get; set; }
		public int NumerOfPerformedWithdrawals { get; set; }
		public string SubscriptionBankAccountNumber { get; set; }
		public string CustomerName { get; set; }
		public string ReturnUrl { get; set; }
		public string MonthlyWithdrawalAmount { get; set; }
		public bool IsOngoing { get; set; }
	}
}