
namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public class PaymentToSend
	{
		public int PayerNumber { get; set; }
		public PaymentType Type { get; set; }
		public decimal Amount { get; set; }
		public string Reference { get; set; }
	}
}