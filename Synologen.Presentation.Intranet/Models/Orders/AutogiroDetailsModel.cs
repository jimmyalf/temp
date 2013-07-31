using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Enumerations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class AutogiroDetailsModel
    {
    	public AutogiroDetailsModel()
        {
            SelectedSubscriptionOption = SubscriptionType.ThreeMonths;
        }

    	public string CustomerName { get; set; }
    	public bool IsNewSubscription { get; set; }
    	public string BankAccountNumber { get; set; }
    	public string ClearingNumber { get; set; }
        public string ProductPrice { get; set; }
        public string FeePrice { get; set; }

    	public IEnumerable<SubscriptionType> SubscriptionOptions
    	{
    		get { return SubscriptionType.GetAll(); }
    	}
    	public SubscriptionType SelectedSubscriptionOption { get; set; }
		public int? CustomSubscriptionTime { get { return SelectedSubscriptionOption.SelectedCustomNumberOfWithdrawals; } }
    	public string TotalWithdrawal { get; set; }
    	public string Montly { get; set; }
    	public string CustomMonthlyProductAmount { get; set; }
    	public string CustomMonthlyFeeAmount { get; set; }
		public bool IsOngoingSubscription { get { return SelectedSubscriptionOption == SubscriptionType.Ongoing; } }
        public string Title { get; set; }

        public void Initialize(SubscriptionItem subscriptionItem)
        {
            BankAccountNumber = subscriptionItem.Subscription.BankAccountNumber;
            ClearingNumber = subscriptionItem.Subscription.ClearingNumber;
            ProductPrice = subscriptionItem.Value.Taxed.ToString("0.00");
            FeePrice = subscriptionItem.Value.TaxFree.ToString("0.00");
            TotalWithdrawal = subscriptionItem.Value.Total.ToString("0.00");
            Montly = subscriptionItem.MonthlyWithdrawal.Total.ToString("0.00");
            if (subscriptionItem.IsOngoing)
            {
                CustomMonthlyFeeAmount = subscriptionItem.MonthlyWithdrawal.TaxFree.ToString("0.00");
                CustomMonthlyProductAmount = subscriptionItem.MonthlyWithdrawal.Taxed.ToString("0.00");
            }

            var type = SubscriptionType.GetFromWithdrawalsLimit(subscriptionItem.WithdrawalsLimit);
            SelectedSubscriptionOption = type;
            Title = subscriptionItem.Title;
        }

        public void Initialize(AutogiroDetailsEventArgs args)
        {
            BankAccountNumber = args.BankAccountNumber;
            ClearingNumber = args.ClearingNumber;
            ProductPrice = args.ProductPrice.HasValue ? args.ProductPrice.Value.ToString("0.00") : null;
            FeePrice = args.FeePrice.HasValue ? args.FeePrice.Value.ToString("0.00") : null;
            TotalWithdrawal = (args.ProductPrice.HasValue && args.FeePrice.HasValue) ? (args.ProductPrice.Value + args.FeePrice.Value).ToString("0.00") : null;
            if (args.Type == SubscriptionType.Ongoing)
            {
                CustomMonthlyFeeAmount = args.MonthlyFee.HasValue ? args.MonthlyFee.Value.ToString("0.00") : null;
                CustomMonthlyProductAmount = args.MonthlyProduct.HasValue ? args.MonthlyProduct.Value.ToString("0.00") : null;
            }
            SelectedSubscriptionOption = args.Type;
            Title = args.Title;
        }
    }
}