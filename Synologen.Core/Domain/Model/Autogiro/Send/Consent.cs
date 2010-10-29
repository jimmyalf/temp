using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send
{
	public class Consent
	{
		public ConsentType Type { get; set; }
		public Payer Transmitter { get; set; }
		public PaymentReciever Reciever { get; set; }
		public string AccountAndClearingNumber { get; set; }
		public string PersonalIdNumber { get; set; }
		public string OrgNumber { get; set; }
	}
}