using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class AutogiroDetailsModel
    {
        public AutogiroDetailsModel()
        {
            SubscriptionOptions = GetSubscriptionOptions();
            SelectedSubscriptionOption = 3;
        }

        public string CustomerName { get; set; }
    	public bool IsNewSubscription { get; set; }
    	//public bool EnableContinuousWithdrawals { get; set; }
    	//public bool EnableCustomNumberOfWithdrawals { get; set; }
    	public string SelectedArticleName { get; set; }
    	public bool EnableAutoWithdrawal { get; set; }
    	public string BankAccountNumber { get; set; }
    	public string ClearingNumber { get; set; }
        public string TaxedAmount { get; set; }
        public string TaxfreeAmount { get; set; }
        public IEnumerable<ListItem> SubscriptionOptions { get; set; }
        public int? SelectedSubscriptionOption { get; set; }

        public IEnumerable<ListItem> GetSubscriptionOptions()
        {
            var options = new List<ListItem>();
            options.Add(new ListItem { Text = "3 månader", Value = 3.ToString() });
            options.Add(new ListItem { Text = "6 månader", Value = 6.ToString() });
            options.Add(new ListItem { Text = "12 månader", Value = 12.ToString() });
            options.Add(new ListItem { Text = "Löpande", Value = 0.ToString() });
            options.Add(new ListItem { Text = "Valfritt", Value = (-1).ToString() });

            return options;
        }
    }
}