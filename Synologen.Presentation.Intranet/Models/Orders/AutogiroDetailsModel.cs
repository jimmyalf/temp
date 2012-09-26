using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
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
			Montly = GetMonthlyAmountFromEvent(args);
			SelectedSubscriptionOption = args.Type;      		
    	}

		protected string GetMonthlyAmountFromEvent(AutogiroDetailsEventArgs args)
		{
			if(args.Type == SubscriptionType.Ongoing)
			{
				return (args.MonthlyFee + args.MonthlyProduct).ToString("0.00");
			}

			if(args.Type.HasCustomNumberOfWithdrawals)
			{
				return Math.Round((args.ProductPrice + args.FeePrice) / args.Type.SelectedCustomNumberOfWithdrawals.Value, 2).ToString("0.00");		
			}
			return Math.Round((args.ProductPrice + args.FeePrice) / args.Type.Value, 2).ToString("0.00");		
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

    	//private IEnumerable<SubscriptionType> GetSubscriptionOptions()
		//{
		//    return Enumeration.GetAll<SubscriptionType>();
		//    //return new List<SubscriptionType>
		//    //{
		//    //    new ListItem("Löpande", OngoingSubscription),
		//    //    new ListItem("3 månader", 3), 
		//    //    new ListItem("6 månader", 6), 
		//    //    new ListItem("12 månader", 12),  
		//    //    new ListItem("Valfritt", UseCustomNumberOfWithdrawalsId)
		//    //};
		//}
		
    }

	public class SubscriptionType : Enumeration
	{
		public static SubscriptionType Ongoing = new SubscriptionType(-2, "Löpande");
		public static SubscriptionType ThreeMonths = new SubscriptionType(3, "3 Månader");
		public static SubscriptionType SixMonths = new SubscriptionType(6, "6 Månader");
		public static SubscriptionType TwelveMonths = new SubscriptionType(12, "12 månader");
		public static SubscriptionType CustomNumberOfWithdrawals = new SubscriptionType(-1, "Valfritt");

		public SubscriptionType() { }
		public SubscriptionType(int value, string displayName) :base(value,displayName)
		{
		}

		public SubscriptionType SetCustomNumberOfWithdrawals(int numberOfWithdrawals)
		{
			SelectedCustomNumberOfWithdrawals = numberOfWithdrawals;
			return this;
		}

		public static SubscriptionType GetFromWithdrawalsLimit(int? numberOfWithdrawals)
		{
			if(numberOfWithdrawals == null) return Ongoing;
			foreach (var type in GetAll<SubscriptionType>())
			{
				if (type.Value == numberOfWithdrawals) return type;
			}
			return CustomNumberOfWithdrawals.SetCustomNumberOfWithdrawals(numberOfWithdrawals.Value);
		}

		public int? SelectedCustomNumberOfWithdrawals { get; private set; }

		public int GetNumberOfWithdrawals()
		{
			if(SelectedCustomNumberOfWithdrawals != null) return SelectedCustomNumberOfWithdrawals.Value;
			if(Value <= 0) throw new ApplicationException("Cannot retrieve a valid number of withdrawals");
			return Value;
		}

		public bool HasCustomNumberOfWithdrawals { get { return SelectedCustomNumberOfWithdrawals.HasValue; } }
	}
}