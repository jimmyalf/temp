using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public class PaymentToSend
	{
		public int PayerId { get; set; }
		public PaymentType Type { get; set; }
		public decimal Amount { get; set; }
		public string Reference { get; set; }
	}
}