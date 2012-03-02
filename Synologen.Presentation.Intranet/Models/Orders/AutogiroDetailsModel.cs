using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class AutogiroDetailsModel
    {
    	public const int UseCustomNumberOfWithdrawalsId = -1;

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

    	private IEnumerable<ListItem> GetSubscriptionOptions()
        {
			return new List<ListItem>
            {
            	new ListItem("3 månader", 3), 
				new ListItem("6 månader", 6), 
				new ListItem("12 månader", 12),  
				new ListItem("Valfritt", UseCustomNumberOfWithdrawalsId)
            };
        }
    }
}