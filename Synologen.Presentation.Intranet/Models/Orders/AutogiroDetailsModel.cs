namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class AutogiroDetailsModel
    {
    	public string CustomerName { get; set; }
    	public bool IsNewSubscription { get; set; }
    	//public bool EnableContinuousWithdrawals { get; set; }
    	//public bool EnableCustomNumberOfWithdrawals { get; set; }
    	public string SelectedArticleName { get; set; }
    	public bool EnableAutoWithdrawal { get; set; }
    	public string BankAccountNumber { get; set; }
    	public string ClearingNumber { get; set; }
    }
}