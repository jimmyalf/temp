using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain;
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

    	public void Initialize(SubscriptionItem subscriptionItem)
    	{
            BankAccountNumber = subscriptionItem.Subscription.BankAccountNumber;
            ClearingNumber = subscriptionItem.Subscription.ClearingNumber; 
            ProductPrice = subscriptionItem.Value.Product.ToString("0.00");
            FeePrice = subscriptionItem.Value.Fee.ToString("0.00");
			TotalWithdrawal = subscriptionItem.Value.Total.ToString("0.00");
			Montly = subscriptionItem.MonthlyWithdrawal.Total.ToString("0.00");
			if(subscriptionItem.IsOngoing)
			{
				CustomMonthlyFeeAmount = subscriptionItem.MonthlyWithdrawal.Fee.ToString("0.00");
				CustomMonthlyProductAmount = subscriptionItem.MonthlyWithdrawal.Product.ToString("0.00");
			}

			var type = SubscriptionType.GetFromWithdrawalsLimit(subscriptionItem.WithdrawalsLimit);
			SelectedSubscriptionOption = type;    		
    	}

    	public void Initialize(AutogiroDetailsEventArgs args)
    	{
            BankAccountNumber = args.BankAccountNumber;
            ClearingNumber = args.ClearingNumber;
    		ProductPrice = args.ProductPrice.ToString("0.00");
            FeePrice = args.FeePrice.ToString("0.00");
			TotalWithdrawal = (args.ProductPrice + args.FeePrice).ToString("0.00");
			if(args.Type == SubscriptionType.Ongoing)
			{
				CustomMonthlyFeeAmount = args.MonthlyFee.ToString("0.00");
				CustomMonthlyProductAmount = args.MonthlyProduct.ToString("0.00");
			}
			SelectedSubscriptionOption = args.Type;      		
    	}

    	public string CustomerName { get; set; }
    	public bool IsNewSubscription { get; set; }
    	public string BankAccountNumber { get; set; }
    	public string ClearingNumber { get; set; }
        public string ProductPrice { get; set; }
        public string FeePrice { get; set; }

    	public IEnumerable<SubscriptionType> SubscriptionOptions
    	{
    		get { return Enumeration.GetAll<SubscriptionType>(); }
    	}

    	public SubscriptionType SelectedSubscriptionOption { get; set; }
		public int? CustomSubscriptionTime { get { return SelectedSubscriptionOption.SelectedCustomNumberOfWithdrawals; } }
		
    	public string TotalWithdrawal { get; set; }
    	public string Montly { get; set; }

    	public string CustomMonthlyProductAmount { get; set; }
    	public string CustomMonthlyFeeAmount { get; set; }
		public bool IsOngoingSubscription { get { return SelectedSubscriptionOption == SubscriptionType.Ongoing; } }
    }
}