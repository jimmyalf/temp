using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class AutogiroDetailsModel
    {
    	public const int UseCustomNumberOfWithdrawalsId = -1;
		public const int OngoingSubscription = -2;

    	public AutogiroDetailsModel()
        {
            SelectedSubscriptionOption = 3;
        }

        public string CustomerName { get; set; }
    	public bool IsNewSubscription { get; set; }
    	public string BankAccountNumber { get; set; }
    	public string ClearingNumber { get; set; }
        public string ProductPrice { get; set; }
        public string FeePrice { get; set; }
		public IEnumerable<ListItem> SubscriptionOptions { get { return GetSubscriptionOptions(); } }
        public int? SelectedSubscriptionOption { get; set; }
        public int? CustomSubscriptionTime { get; set; }
		
    	public string TotalWithdrawal { get; set; }
    	public string Montly { get; set; }

    	public string CustomMonthlyProductAmount { get; set; }
    	public string CustomMonthlyFeeAmount { get; set; }

    	private IEnumerable<ListItem> GetSubscriptionOptions()
        {
			return new List<ListItem>
            {
				new ListItem("L�pande", OngoingSubscription),
            	new ListItem("3 m�nader", 3), 
				new ListItem("6 m�nader", 6), 
				new ListItem("12 m�nader", 12),  
				new ListItem("Valfritt", UseCustomNumberOfWithdrawalsId)
            };
        }
    }
}