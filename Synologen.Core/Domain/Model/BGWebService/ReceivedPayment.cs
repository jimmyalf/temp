
namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public class ReceivedPayment
	{
		public int PaymentId { get; set; }
		public int PayerId { get; set; }
		public decimal Amount { get; set; }
		public PaymentResult Result { get; set; }
	}
}