using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
	public class EditSubscriptionForm
	{
		public string BankAccountNumber { get; set; }
		public string ClearingNumber { get; set; }
		public string ReturnUrl { get; set; }

		public void Initialize(Subscription subscription)
		{
			BankAccountNumber = subscription.BankAccountNumber;
			ClearingNumber = subscription.ClearingNumber;
		}
	}
}